using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiltySystem
{
    public partial class Menus : System.Web.UI.Page
    {
        #region Members

        int loginid;

        #endregion

        #region Properties

        public int LoginID
        {
            get
            {
                if (Request.QueryString["lid"] != string.Empty && Request.QueryString["lid"] != null)
                {
                    loginid = Convert.ToInt32(Request.QueryString["lid"].ToString());
                }
                return loginid;

            }
        }

        #endregion

        #region Helper Methods

        private void FillDropDown(DataTable dt, DropDownList _ddl, string _ddlValue, string _ddlText, string _ddlDefaultText)
        {
            if (dt.Rows.Count > 0)
            {
                _ddl.DataSource = dt;

                _ddl.DataValueField = _ddlValue;
                _ddl.DataTextField = _ddlText;

                _ddl.DataBind();

                ListItem item = new ListItem();

                item.Text = _ddlDefaultText;
                item.Value = _ddlDefaultText;

                _ddl.Items.Insert(0, item);
                _ddl.SelectedIndex = 0;
            }
        }

        #endregion

        #region Page Load

        protected void Page_Load(object sender, EventArgs e)
        {
            notification("", "");
            if (!IsPostBack)
            {
                if (LoginID > 0)
                {
                    this.Title = "Menus";
                    GetMenus();
                }
            }
        }

        #endregion

        #region Custom Methods

        public void notification()
        {
            try
            {
                divNotification.InnerHtml = string.Empty;
            }
            catch (Exception ex)
            {
                divNotification.InnerHtml = "<div class=\"alert alert-danger\">" + ex.Message + "</div>";
            }
        }

        public void notification(string type, string msg)
        {
            try
            {
                if (type == "Error")
                {
                    divNotification.InnerHtml = "<div class=\"alert alert-danger\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                }
                else if (type == "Success")
                {
                    divNotification.InnerHtml = "<div class=\"alert alert-success\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                }
                else if (type == string.Empty)
                {
                    divNotification.InnerHtml = string.Empty;
                }
            }
            catch (Exception ex)
            {
                divNotification.InnerHtml = "<div class=\"alert alert-danger\">" + ex.Message + "</div>";
            }
        }

        public void subMenuNotification()
        {
            try
            {
                divSubMenuNotification.InnerHtml = string.Empty;
            }
            catch (Exception ex)
            {
                divSubMenuNotification.InnerHtml = "<div class=\"alert alert-danger\">" + ex.Message + "</div>";
            }
        }

        public void subMenuNotification(string type, string msg)
        {
            try
            {
                if (type == "Error")
                {
                    divSubMenuNotification.InnerHtml = "<div class=\"alert alert-danger\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                }
                else if (type == "Success")
                {
                    divSubMenuNotification.InnerHtml = "<div class=\"alert alert-success\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                }
                else if (type == string.Empty)
                {
                    divSubMenuNotification.InnerHtml = string.Empty;
                }
            }
            catch (Exception ex)
            {
                divSubMenuNotification.InnerHtml = "<div class=\"alert alert-danger\">" + ex.Message + "</div>";
            }
        }

        public void GetMenus()
        {
            try
            {
                MenuDML dml = new MenuDML();
                DataTable dtMenus = txtSearch.Text == string.Empty ? dml.GetMenu() : dml.GetMenu(txtSearch.Text.Trim());
                if (dtMenus.Rows.Count > 0)
                {
                    gvResult.DataSource = dtMenus;
                }
                else
                {
                    gvResult.DataSource = null;
                }
                gvResult.DataBind();
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting/binding menus, due to: " + ex.Message);
            }
        }

        private void ResetFields()
        {
            try
            {
                hfEditID.Value = string.Empty;
                txtMenuName.Text = string.Empty;
                txtIcon.Text = string.Empty;
                txtMenuDescription.Text = string.Empty;
            }
            catch (Exception ex)
            {
                notification("Error", "Error resetting fields, due to: " + ex.Message);
            }
        }

        int GetColumnIndexByName(GridViewRow row, string columnName)
        {
            int columnIndex = 0;
            foreach (DataControlFieldCell cell in row.Cells)
            {
                if (cell.ContainingField is BoundField)
                    if (((BoundField)cell.ContainingField).DataField.Equals(columnName))
                        break;
                columnIndex++;
            }
            return columnIndex;
        }

        public void ConfirmModal(string Title, string Action)
        {
            try
            {
                modalConfirm.Show();
                lblModalTitle.Text = Title;
                hfConfirmAction.Value = Action;
            }
            catch (Exception ex)
            {
                notification("Error", "Error confirming, due to: " + ex.Message);
            }
        }

        #endregion

        #region Events

        protected void lnkCloseView_Click(object sender, EventArgs e)
        {
            try
            {
                pnlView.Visible = false;
            }
            catch (Exception ex)
            {
                notification("Error", "Error closing view panel, due to: " + ex.Message);
            }
        }

        protected void lnkSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMenuName.Text == string.Empty)
                {
                    txtMenuName.Focus();
                    notification("Error", "Please provide Menu Name");
                }
                else
                {
                    string Menu = txtMenuName.Text.Trim();
                    MenuDML dml = new MenuDML();
                    DataTable dtDuplicate = dml.GetDuplicateMenu(Menu);

                    if (hfEditID.Value == string.Empty)
                    {
                        if (dtDuplicate.Rows.Count > 0)
                        {
                            notification("Error", "Another active menu with same name exists, try to change Menu Name");
                            txtMenuName.Focus();
                        }
                        else
                        {
                            ConfirmModal("Are you sure want to save?", "Save");
                        }
                    }
                    else
                    {                        
                        if (dtDuplicate.Rows.Count > 1)
                        {
                            notification("Error", "Another active menu with same name exists, try to change Menu Name");
                            txtMenuName.Focus();
                        }
                        else
                        {
                            ConfirmModal("Are you sure want to save?", "Update");
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                notification("Error", "Error saving Menu, due to: " + ex.Message);
            }
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (hfEditID.Value == string.Empty)
                {
                    ResetFields();
                    pnlInput.Visible = false;
                }
                else
                {
                    ConfirmModal("You are going to delete record, are you sure?", "Delete");
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error deleting record, due to: " + ex.Message);
            }
        }

        protected void lnkCancelSearch_Click(object sender, EventArgs e)
        {
            try
            {
                txtSearch.Text = string.Empty;
                GetMenus();
            }
            catch (Exception ex)
            {
                notification("Error", "Error canceling search, due to: " + ex.Message);
            }
        }

        protected void lnkSearch_Click(object sender, EventArgs e)
        {
            GetMenus();
        }

        protected void lnkAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                pnlInput.Visible = true;
                ResetFields();
                pnlView.Visible = false;
            }
            catch (Exception ex)
            {
                notification("Error", "Error opening add new input, due to: " + ex.Message);
            }
        }

        protected void gvResult_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView rowView = (DataRowView)e.Row.DataItem;
                    bool Active = Convert.ToBoolean(rowView["Active"].ToString() == string.Empty ? "false" : rowView["Active"]);
                    LinkButton lnkActive = e.Row.FindControl("lnkActive") as LinkButton;
                    if (Active == true)
                    {
                        lnkActive.CssClass = "fas fa-toggle-on";
                        lnkActive.ForeColor = Color.DodgerBlue;
                        lnkActive.ToolTip = "Switch to Deactivate";

                    }
                    else
                    {
                        lnkActive.CssClass = "fas fa-toggle-off";
                        lnkActive.ForeColor = Color.Maroon;
                        lnkActive.ToolTip = "Switch to Activate";
                        e.Row.BackColor = Color.LightPink;
                    }

                    Int64 MenuID = Convert.ToInt64(gvResult.DataKeys[e.Row.RowIndex]["MenuID"]);
                    GridView gvSubMenus = e.Row.FindControl("gvSubMenus") as GridView;
                    NavMenuDML dml = new NavMenuDML();
                    DataTable dtSubMenu = dml.GetSubMenu(MenuID);
                    if (dtSubMenu.Rows.Count > 0)
                    {
                        gvSubMenus.DataSource = dtSubMenu;
                    }
                    else
                    {
                        gvSubMenus.DataSource = null;
                    }
                    gvSubMenus.DataBind();
                    gvSubMenus.Visible = false;
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Binding Menus to grid, due to: " + ex.Message);
            }
        }

        protected void gvResult_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Active")
                {

                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvResult.Rows[index];
                    Int64 MenuID = Convert.ToInt64(gvResult.DataKeys[index]["MenuID"]);
                    string Active = gvResult.DataKeys[index]["Active"].ToString() == string.Empty ? "False" : gvResult.DataKeys[index]["Active"].ToString();
                    MenuDML dml = new MenuDML();

                    if (Active == "True")
                    {
                        dml.DeactivateMenu(MenuID, LoginID);
                    }
                    else
                    {
                        dml.ActivateMenu(MenuID, LoginID);
                    }
                    GetMenus();
                }
                else if (e.CommandName == "Change")
                {

                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvResult.Rows[index];
                    Int64 MenuID = Convert.ToInt64(gvResult.DataKeys[index]["MenuID"]);
                    hfEditID.Value = MenuID.ToString();

                    MenuDML dml = new MenuDML();
                    DataTable dtMenu = dml.GetMenu(MenuID);
                    if (dtMenu.Rows.Count > 0)
                    {
                        txtMenuName.Text = dtMenu.Rows[0]["MenuName"].ToString();
                        txtMenuDescription.Text = dtMenu.Rows[0]["Description"].ToString();
                        txtIcon.Text = dtMenu.Rows[0]["Icon"].ToString();
                        pnlInput.Visible = true;
                        pnlView.Visible = false;
                    }
                    else
                    {
                        notification("Error", "No such record found");
                    }                    
                    GetMenus();
                }
                else if (e.CommandName == "View")
                {

                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvResult.Rows[index];
                    Int64 MenuID = Convert.ToInt64(gvResult.DataKeys[index]["MenuID"]);
                    hfEditID.Value = MenuID.ToString();

                    MenuDML dml = new MenuDML();
                    DataTable dtMenu = dml.GetMenu(MenuID);
                    if (dtMenu.Rows.Count > 0)
                    {
                        lblMenuName.Text = dtMenu.Rows[0]["MenuName"].ToString();
                        lblMenuDescription.Text = dtMenu.Rows[0]["Description"].ToString();
                        lblIcon.Text = dtMenu.Rows[0]["Icon"].ToString();
                        pnlInput.Visible = false;
                        pnlView.Visible = true;
                    }
                    else
                    {
                        notification("Error", "No such record found");
                    }
                    GetMenus();
                }
                else if (e.CommandName == "SubMenu")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvResult.Rows[index];
                    Int64 MenuID = Convert.ToInt64(gvResult.DataKeys[index]["MenuID"]);
                    hfEditID.Value = MenuID.ToString();
                    LinkButton lnkSubMenus = gvr.FindControl("lnkSubMenus") as LinkButton;
                    if (lnkSubMenus.CssClass == "fas fa-minus-square")
                    {
                        lnkSubMenus.CssClass = "fas fa-plus-square";
                        lnkSubMenus.ForeColor = Color.DodgerBlue;
                        GridView gvSubMenu = gvr.FindControl("gvSubMenus") as GridView;
                        gvSubMenu.Visible = false;
                        LinkButton lnkAddNewSubMenu = gvr.FindControl("lnkAddNewSubMenu") as LinkButton;
                        lnkAddNewSubMenu.Visible = false;
                    }
                    else
                    {
                        for (int i = 0; i < gvResult.Rows.Count; i++)
                        {
                            GridViewRow gvrs = gvResult.Rows[i];

                            LinkButton lnkSubMenuss = gvrs.FindControl("lnkSubMenus") as LinkButton;
                            lnkSubMenuss.CssClass = "fas fa-plus-square";
                            lnkSubMenuss.ForeColor = Color.DodgerBlue;
                            GridView gvSubMenus = gvrs.FindControl("gvSubMenus") as GridView;
                            gvSubMenus.Visible = false;
                            LinkButton lnkAddNewSubMenus = gvrs.FindControl("lnkAddNewSubMenu") as LinkButton;
                            lnkAddNewSubMenus.Visible = false;
                        }
                        lnkSubMenus.CssClass = "fas fa-minus-square";
                        lnkSubMenus.ForeColor = Color.Red;
                        GridView gvSubMenu = gvr.FindControl("gvSubMenus") as GridView;
                        gvSubMenu.Visible = true;
                        LinkButton lnkAddNewSubMenu = gvr.FindControl("lnkAddNewSubMenu") as LinkButton;
                        lnkAddNewSubMenu.Visible = true;
                    }
                }
                else if (e.CommandName == "AddNewSubMenu")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvResult.Rows[index];
                    Int64 MenuID = Convert.ToInt64(gvResult.DataKeys[index]["MenuID"]);
                    hfEditID.Value = MenuID.ToString();
                    modalSubMenu.Show();
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error commanding row, due to: " + ex.Message);
            }
        }

        protected void lnkConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                string Action = hfConfirmAction.Value;
                
                //else 
                string Menu = txtMenuName.Text;
                string Description = txtMenuDescription.Text;
                string Icon = txtIcon.Text;
                if (Action == "Save")
                {
                    try
                    {
                        MenuDML dml = new MenuDML();
                        
                        Int64 MenuID = dml.InsertMenu(Menu, Description, Icon, LoginID);
                        if (MenuID > 0)
                        {
                            pnlInput.Visible = false;
                            notification("Success", "Submited Successfully");
                            ResetFields();
                            GetMenus();
                        }
                        else
                        {
                            notification("Error", "Menu not saved, try again or  contact IT Team.");
                        }
                        modalConfirm.Hide();
                    }
                    catch (Exception ex)
                    {
                        notification("Error", "Error saving, due to: " + ex.Message);
                        modalConfirm.Hide();
                    }
                }
                else if (Action == "Update")
                {
                    try
                    {
                        Int64 MenuID = Convert.ToInt64(hfEditID.Value);
                        MenuDML dml = new MenuDML();
                        dml.UpdateMenu(MenuID, Menu, Description, Icon, LoginID);
                        pnlInput.Visible = false;
                        notification("Success", "Updated Successfully");
                        ResetFields();
                        GetMenus();
                        modalConfirm.Hide();
                    }
                    catch (Exception ex)
                    {
                        notification("Error", "Error saving, due to: " + ex.Message);
                        modalConfirm.Hide();
                    }
                }
                else if (Action == "Delete")
                {
                    try
                    {
                        Int64 MenuID = Convert.ToInt64(hfEditID.Value);
                        if (MenuID > 0)
                        {
                            MenuDML dml = new MenuDML();
                            dml.DeleteMenu(MenuID);
                            notification("Success", "Deleted successfully");

                            ResetFields();

                            GetMenus();
                            pnlInput.Visible = false;
                        }
                        else
                        {
                            notification("Error", "No record selected to delete");
                        }
                    }
                    catch (Exception ex)
                    {
                        notification("Error", "Error deleting record, due to: " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error confirming, due to: " + ex.Message);
            }
        }

        protected void lnkCloseInput_Click(object sender, EventArgs e)
        {
            try
            {
                pnlInput.Visible = false;
                ResetFields();
            }
            catch (Exception ex)
            {
                notification("Error", "Error disabling add new input, due to: " + ex.Message);
            }
        }

        protected void gvSubMenus_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "ChangeSub")
                {
                    string[] Argue = e.CommandArgument.ToString().Split(',');
                    int index = Convert.ToInt32(Argue[0]);
                    Int64 FormID = Convert.ToInt32(Argue[1]);
                    for (int i = 0; i < gvResult.Rows.Count; i++)
                    {
                        GridView gvSubMenus = gvResult.Rows[i].FindControl("gvSubMenus") as GridView;
                        if (gvSubMenus.Visible == true)
                        {
                            GridViewRow gvr = gvSubMenus.Rows[index];
                            hfSubMenuEditID.Value = FormID.ToString();
                            NavMenuDML dml = new NavMenuDML();
                            DataTable dtSubMenu = dml.GetSubMenuByID(FormID);


                            if (dtSubMenu.Rows.Count > 0)
                            {
                                txtSubMenuName.Text = dtSubMenu.Rows[0]["FormName"].ToString();
                                txtSubMenuLink.Text = dtSubMenu.Rows[0]["Url"].ToString();
                                ddlTarget.ClearSelection();
                                ddlTarget.Items.FindByText(dtSubMenu.Rows[0]["formTarget"].ToString() == "METHOD" ? "_Blank" : dtSubMenu.Rows[0]["formTarget"].ToString()).Selected = true;
                            }
                            else
                            {
                                notification("Error", "No such record found");
                            }
                        }
                    }
                    lnkDeleteSubMenu.Visible = true;
                    modalSubMenu.Show();
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error commanding row, due to: " + ex.Message);
            }
        }

        protected void lnkSaveSubMenu_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSubMenuName.Text.Trim() == string.Empty)
                {
                    subMenuNotification("Error", "Please enter Submenu Name");
                    modalSubMenu.Show();
                    txtSubMenuName.Focus();
                }
                else if (txtSubMenuLink.Text.Trim() == string.Empty)
                {
                    subMenuNotification("Error", "Please enter Submenu Link");
                    modalSubMenu.Show();
                    txtSubMenuLink.Focus();
                }
                else
                {
                    Int64 MenuID = Convert.ToInt64(hfEditID.Value);
                    string Name = txtSubMenuName.Text.Trim();
                    string Url = txtSubMenuLink.Text.Trim();
                    string Target = ddlTarget.SelectedItem.Text;
                    NavMenuDML dml = new NavMenuDML();
                    DataTable dtDuplicate = dml.GetSubMenuByName(Name);

                    if (hfSubMenuEditID.Value == string.Empty)
                    {
                        if (dtDuplicate.Rows.Count > 0)
                        {
                            subMenuNotification("Error", "Another submenu with same name already exist");
                            modalSubMenu.Show();
                        }
                        else
                        {
                            Int64 SubMenuID = dml.InsertMenu(Name, Url, Target, MenuID, LoginID);
                            if (SubMenuID > 0)
                            {
                                notification("Success", "Sub Menu saved successfully");
                                txtSubMenuName.Text = string.Empty;
                                txtSubMenuLink.Text = string.Empty;
                                ddlTarget.ClearSelection();
                                DataTable dtSubMenus = dml.GetSubMenu(MenuID);

                                for (int i = 0; i < gvResult.Rows.Count; i++)
                                {
                                    GridView gvSubMenu = gvResult.Rows[i].FindControl("gvSubMenus") as GridView;
                                    if (gvSubMenu.Visible == true)
                                    {
                                        if (dtSubMenus.Rows.Count > 0)
                                        {
                                            gvSubMenu.DataSource = dtSubMenus;
                                        }
                                        else
                                        {
                                            gvSubMenu.DataSource = null;
                                        }
                                        gvSubMenu.DataBind();
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (dtDuplicate.Rows.Count > 1)
                        {
                            subMenuNotification("Error", "Another submenu with same name already exist");
                            modalSubMenu.Show();
                        }
                        else
                        {
                            Int64 SubMenuID = Convert.ToInt64(hfSubMenuEditID.Value);
                            dml.UpdateNavMenu(SubMenuID, Name, Url, Target, LoginID);

                            notification("Success", "Sub Menu saved successfully");
                            txtSubMenuName.Text = string.Empty;
                            txtSubMenuLink.Text = string.Empty;
                            ddlTarget.ClearSelection();

                            DataTable dtSubMenus = dml.GetSubMenu(MenuID);

                            for (int i = 0; i < gvResult.Rows.Count; i++)
                            {
                                GridView gvSubMenu = gvResult.Rows[i].FindControl("gvSubMenus") as GridView;
                                if (gvSubMenu.Visible == true)
                                {
                                    if (dtSubMenus.Rows.Count > 0)
                                    {
                                        gvSubMenu.DataSource = dtSubMenus;
                                    }
                                    else
                                    {
                                        gvSubMenu.DataSource = null;
                                    }
                                    gvSubMenu.DataBind();
                                }
                            }
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                subMenuNotification("Error", "Error saving submenu, due to: " + ex.Message);
                modalSubMenu.Show();
            }
        }

        protected void lnkDeleteSubMenu_Click(object sender, EventArgs e)
        {
            try
            {
                if (hfSubMenuEditID.Value == string.Empty)
                {
                    notification("Error", "No such record found");
                    modalSubMenu.Show();
                }
                else
                {
                    Int64 MenuID = Convert.ToInt64(hfEditID.Value);
                    Int64 SubMenuID = Convert.ToInt64(hfSubMenuEditID.Value);
                    NavMenuDML dml = new NavMenuDML();
                    dml.DeleteNavMenu(SubMenuID);
                    DataTable dtSubMenus = dml.GetSubMenu(MenuID);

                    for (int i = 0; i < gvResult.Rows.Count; i++)
                    {
                        GridView gvSubMenu = gvResult.Rows[i].FindControl("gvSubMenus") as GridView;
                        if (gvSubMenu.Visible == true)
                        {
                            if (dtSubMenus.Rows.Count > 0)
                            {
                                gvSubMenu.DataSource = dtSubMenus;
                            }
                            else
                            {
                                gvSubMenu.DataSource = null;
                            }
                            gvSubMenu.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                subMenuNotification("Error", "Error deleting submenu, due to: " + ex.Message);
                modalSubMenu.Show();
            }
        }

        #endregion
    }
}