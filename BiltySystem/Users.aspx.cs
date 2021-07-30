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
    public partial class Users : System.Web.UI.Page
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

        private void FillCheckBoxLIst(DataTable dt, CheckBoxList _cbl, string _ddlValue, string _ddlText)
        {
            if (dt.Rows.Count > 0)
            {
                _cbl.DataSource = dt;

                _cbl.DataValueField = _ddlValue;
                _cbl.DataTextField = _ddlText;

                _cbl.DataBind();
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
                    this.Title = "Users";
                    GetUsers();
                    GetAndPopulateGroup();
                    GetAndPopulateDesignation();
                    GetAllMenus();
                    //GetAndPopulateRole();


                }
            }
        }

        private void GetAllMenus()
        {
            NaviDML menu = new NaviDML();
            DataTable dt = menu.GetSubMenusWithoutID();
            if (dt.Rows.Count > 0)
            {
                FillCheckBoxLIst(dt, cbNavMenu, "FormID", "FormName");
            }


        }

        #endregion

        #region Custom Methods

        public void GetAndPopulateGroup()
        {
            try
            {
                OwnGroupDML dml = new OwnGroupDML();
                DataTable dtProvince = dml.GetActiveGroups();
                if (dtProvince.Rows.Count > 0)
                {
                    FillDropDown(dtProvince, ddlGroup, "GroupID", "GroupName", "-Select Group-");
                }
                else
                {
                    ddlGroup.Items.Clear();
                    notification("Error", "No group found, please add group first.");
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting/populating groups, due to: " + ex.Message);
            }
        }

        public void GetAndPopulateCompany(Int64 GroupID)
        {
            try
            {
                OwnCompanyDML dml = new OwnCompanyDML();
                DataTable dtProvince = dml.GetCompanyByGroup(GroupID);
                if (dtProvince.Rows.Count > 0)
                {
                    FillDropDown(dtProvince, ddlCompany, "CompanyID", "CompanyName", "-Select Company-");
                    ddlCompany.Enabled = true;
                }
                else
                {
                    ddlCompany.Items.Clear();
                    ddlCompany.Enabled = false;

                    ddlDepartment.Items.Clear();
                    ddlDepartment.Enabled = false;
                    notification("Error", "No Company found, please add company first.");
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting/populating company, due to: " + ex.Message);
            }
        }

        public void GetAndPopulateDepartment(Int64 CompanyID, Int64 GroupID)
        {
            try
            {
                OwnDepartmentDML dml = new OwnDepartmentDML();
                DataTable dtDepartment = dml.GetDepartmentByGroupAndCompany(CompanyID, GroupID);
                if (dtDepartment.Rows.Count > 0)
                {
                    FillDropDown(dtDepartment, ddlDepartment, "DepartID", "DepartName", "-Select Department-");
                    ddlDepartment.Enabled = true;
                }
                else
                {
                    ddlGroup.Items.Clear();
                    ddlDepartment.Enabled = false;
                    notification("Error", "No Department found, please add Department first.");
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting/populating Department, due to: " + ex.Message);
            }
        }

        public void GetAndPopulateDesignation()
        {
            try
            {
                UsersDML dml = new UsersDML();
                DataTable dtDesignation = dml.GetDesignations();
                if (dtDesignation.Rows.Count > 0)
                {
                    FillDropDown(dtDesignation, ddlDesignation, "DesignationID", "DesignationName", "-Select Designation-");
                }
                else
                {
                    ddlGroup.Items.Clear();
                    notification("Error", "No Designation found, please add Designation first.");
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting/populating Designation, due to: " + ex.Message);
            }
        }

        //public void GetAndPopulateRole()
        //{
        //    try
        //    {
        //        UsersDML dml = new UsersDML();
        //        DataTable dtRole = dml.GetRoles();
        //        if (dtRole.Rows.Count > 0)
        //        {
        //            FillDropDown(dtRole, ddlRole, "RoleID", "RoleName", "-Select Roles-");
        //        }
        //        else
        //        {
        //            ddlGroup.Items.Clear();
        //            notification("Error", "No Roles found, please add Roles first.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notification("Error", "Error getting/populating Roles, due to: " + ex.Message);
        //    }
        //}

        public void GetUsers()
        {
            try
            {
                UsersDML dml = new UsersDML();
                DataTable dtUsers = txtSearch.Text == string.Empty ? dml.GetUsers() : dml.GetUsers(txtSearch.Text.Trim());
                if (dtUsers.Rows.Count > 0)
                {
                    gvResult.DataSource = dtUsers;
                }
                else
                {
                    gvResult.DataSource = null;
                }
                gvResult.DataBind();
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting/populating Roles, due to: " + ex.Message);
            }
        }

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

        private void ResetFields()
        {
            try
            {
                hfTempPassword.Value = string.Empty;
                hfEditID.Value = string.Empty;
                txtUserName.Text = string.Empty;
                txtPassword.Text = string.Empty;
                ddlGroup.ClearSelection();

                ddlCompany.ClearSelection();
                ddlCompany.Items.Clear();

                ddlDepartment.ClearSelection();
                ddlDepartment.Items.Clear();

                ddlDesignation.ClearSelection();
                //ddlRole.ClearSelection();
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

        #endregion

        #region Events

        protected void lnkSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUserName.Text == string.Empty)
                {
                    notification("Error", "Please enter username");
                    txtUserName.Focus();
                }
                else if (ddlDesignation.SelectedIndex == 0)
                {
                    notification("Error", "Please select designation");
                    ddlDesignation.Focus();
                }
                //else if (ddlRole.SelectedIndex == 0)
                //{
                //    notification("Error", "Please select Role");
                //    ddlRole.Focus();
                //}
                else
                {
                    UsersDML dml = new UsersDML();
                    DataTable dtUser = dml.GetUsers(txtUserName.Text, Convert.ToInt64(ddlDepartment.SelectedItem.Value));
                    if (hfEditID.Value == string.Empty)
                    {
                        if (txtPassword.Text == string.Empty)
                        {
                            notification("Error", "Please enter password");
                            txtPassword.Focus();
                        }
                        else
                        {

                            hfTempPassword.Value = txtPassword.Text;
                            if (dtUser.Rows.Count > 0)
                            {
                                notification("Error", "Another user exists");
                            }
                            else
                            {
                                ConfirmModal("Are you sure you want to save User?", "Save");
                            }
                        }
                    }
                    else
                    {
                        if (dtUser.Rows.Count > 1)
                        {
                            notification("Error", "Another user exists");
                        }
                        else
                        {
                            Int64 UserID = Convert.ToInt64(hfEditID.Value);
                            ConfirmModal("Are you sure you want to save User?", "Update");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error saving User, due to: " + ex.Message);
            }
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            try
            {
                ConfirmModal("Are you sure want to Delete?", "Delete");
            }
            catch (Exception ex)
            {
                notification("Error", "Error deleting User, due to: " + ex.Message);
            }
        }

        protected void lnkCloseInput_Click(object sender, EventArgs e)
        {
            try
            {
                ResetFields();
                pnlInput.Visible = false;
            }
            catch (Exception ex)
            {
                notification("Error", "Error closing input panel, due to: " + ex.Message);
            }
        }

        protected void lnkCancelSearch_Click(object sender, EventArgs e)
        {
            try
            {
                txtSearch.Text = string.Empty;
                GetUsers();
            }
            catch (Exception ex)
            {
                notification("Error", "Error canceling search, due to: " + ex.Message);
            }
        }

        protected void lnkSearch_Click(object sender, EventArgs e)
        {
            try
            {
                GetUsers();
            }
            catch (Exception ex)
            {
                notification("Error", "Error searching, due to: " + ex.Message);
            }
        }

        protected void lnkAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                pnlInput.Visible = true;
                txtUserName.Focus();
            }
            catch (Exception ex)
            {
                notification("Error", "Error enabling input panel, due to: " + ex.Message);
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
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error binding row, due to: " + ex.Message);
            }
        }

        protected void gvResult_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Change")
                {

                    pnlInput.Visible = true;
                    pnlView.Visible = false;
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvResult.Rows[index];
                    Int64 UserID = Convert.ToInt64(gvResult.DataKeys[index]["UserID"]);
                    hfEditID.Value = UserID.ToString();


                    UsersDML dml = new UsersDML();
                    DataTable dtUser = dml.GetUsers(UserID);
                    if (dtUser.Rows.Count > 0)
                    {
                        hfEditID.Value = dtUser.Rows[0]["UserID"].ToString();
                        txtUserName.Text = dtUser.Rows[0]["UserName"].ToString();


                        ddlGroup.ClearSelection();


                        ddlGroup.Items.FindByValue(dtUser.Rows[0]["GroupID"].ToString()).Selected = true;




                        Int64 GroupID = Convert.ToInt64(ddlGroup.SelectedValue);
                        GetAndPopulateCompany(GroupID);
                        ddlCompany.ClearSelection();
                        ddlCompany.Items.FindByValue(dtUser.Rows[0]["CompanyID"].ToString()).Selected = true;
                        ;


                        Int64 CompanyID = Convert.ToInt64(ddlCompany.SelectedItem.Value);
                        GetAndPopulateDepartment(CompanyID, GroupID);
                        ddlDepartment.ClearSelection();
                        ddlDepartment.Items.FindByValue(dtUser.Rows[0]["DepartmentID"].ToString()).Selected = true;

                        ddlDesignation.ClearSelection();
                        ddlDesignation.Items.FindByValue(dtUser.Rows[0]["DesignationID"].ToString()).Selected = true;


                        char[] Roles = dtUser.Rows[0]["Roles"].ToString().ToCharArray();
                        for (int i = 0; i < Roles.Length; i++)
                        {
                            if (Roles[i].ToString() == "1")
                            {
                                cbRoles.Items[i].Selected = true;
                            }
                            else
                            {
                                cbRoles.Items[i].Selected = false;
                            }
                        }


                        lnkDelete.Visible = true;
                    }
                    else
                    {
                        notification("Error", "No such area found for update");
                    }
                }
                else if (e.CommandName == "View")
                {

                    pnlInput.Visible = true;
                    pnlView.Visible = false;
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvResult.Rows[index];
                    Int64 UserID = Convert.ToInt64(gvResult.DataKeys[index]["UserID"]);
                    hfEditID.Value = UserID.ToString();


                    UsersDML dml = new UsersDML();
                    DataTable dtUser = dml.GetUsers(UserID);
                    if (dtUser.Rows.Count > 0)
                    {
                        lblUserName.Text = dtUser.Rows[0]["UserName"].ToString();
                        lblGroup.Text = dtUser.Rows[0]["GroupName"].ToString();
                        lblCompany.Text = dtUser.Rows[0]["CompanyName"].ToString();
                        lblDepartment.Text = dtUser.Rows[0]["DepartName"].ToString();
                        lblDesignation.Text = dtUser.Rows[0]["DesignationName"].ToString();
                        lblRole.Text = dtUser.Rows[0]["RoleName"].ToString();

                        pnlInput.Visible = false;
                        pnlView.Visible = true;
                    }
                    else
                    {
                        notification("Error", "No such area found for update");
                    }
                }
                else if (e.CommandName == "Active")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvResult.Rows[index];
                    Int64 UserID = Convert.ToInt64(gvResult.DataKeys[index]["UserID"]);
                    string Active = gvResult.DataKeys[index]["Active"].ToString() == string.Empty ? "False" : gvResult.DataKeys[index]["Active"].ToString();
                    UsersDML dml = new UsersDML();

                    hfEditID.Value = UserID.ToString();

                    if (Active == "True")
                    {
                        dml.DeactivateUser(UserID, LoginID);
                    }
                    else
                    {
                        dml.ActivateUser(UserID, LoginID);
                    }
                    GetUsers();
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
                if (Action == "Delete")
                {
                    try
                    {
                        Int64 UserID = Convert.ToInt64(hfEditID.Value);
                        if (UserID > 0)
                        {
                            UsersDML dml = new UsersDML();
                            dml.DeleteUser(UserID);
                            notification("Success", "User deleted successfully");

                            ResetFields();

                            GetUsers();
                            pnlInput.Visible = false;
                        }
                        else
                        {
                            notification("Error", "No area selected to delete");
                        }
                    }
                    catch (Exception ex)
                    {
                        notification("Error", ex.Message);
                    }
                }
                else if (Action == "Save")
                {
                    try
                    {
                        string UserName = txtUserName.Text.Trim();
                        string Password = hfTempPassword.Value;

                        Int64 GroupID = Convert.ToInt64(ddlGroup.SelectedItem.Value);
                        Int64 CompanyID = Convert.ToInt64(ddlCompany.SelectedItem.Value);
                        Int64 DepartmentID = Convert.ToInt64(ddlDepartment.SelectedItem.Value);
                        Int64 DesignationID = Convert.ToInt64(ddlDesignation.SelectedItem.Value);
                        // Int64 RoleID = Convert.ToInt64(ddlRole.SelectedItem.Value);
                        string Roles = string.Empty;
                        foreach (ListItem _cb in cbRoles.Items)
                        {
                            Roles += _cb.Selected == true ? "1" : "0";
                        }

                        UsersDML dml = new UsersDML();
                        Int64 UserID = dml.InsertUser(UserName, Password, GroupID, CompanyID, DepartmentID, DesignationID, Roles, LoginID);
                        if (UserID > 0)
                        {
                            pnlInput.Visible = false;
                            notification("Success", "User saved sucessfully");
                            ResetFields();
                            GetUsers();
                        }
                        else
                        {
                            notification("Success", "User not saved sucessfully, Contact IT Team");
                        }
                    }
                    catch (Exception ex)
                    {
                        notification("Error", "Error saving area, due to: " + ex.Message);
                        modalConfirm.Hide();
                    }
                }
                else if (Action == "Update")
                {
                    try
                    {
                        string UserName = txtUserName.Text.Trim();

                        Int64 GroupID = Convert.ToInt64(ddlGroup.SelectedItem.Value);
                        Int64 CompanyID = Convert.ToInt64(ddlCompany.SelectedItem.Value);
                        Int64 DepartmentID = Convert.ToInt64(ddlDepartment.SelectedItem.Value);
                        Int64 DesignationID = Convert.ToInt64(ddlDesignation.SelectedItem.Value);
                        //   Int64 RoleID = Convert.ToInt64(ddlRole.SelectedItem.Value);
                        Int64 UserID = Convert.ToInt64(hfEditID.Value);
                        string Roles = string.Empty;
                        foreach (ListItem _cb in cbRoles.Items)
                        {
                            Roles += _cb.Selected == true ? "1" : "0";
                        }
                        UsersDML dml = new UsersDML();
                        dml.UpdateUser(UserID, UserName, GroupID, CompanyID, DepartmentID, DesignationID, Roles, LoginID);

                        pnlInput.Visible = false;
                        notification("Success", "User updated sucessfully");
                        ResetFields();
                        GetUsers();
                    }
                    catch (Exception ex)
                    {
                        notification("Error", "Error updating area, due to: " + ex.Message);
                        modalConfirm.Hide();
                    }
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error confirming, due to: " + ex.Message);
            }
        }

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



        #endregion

        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlGroup.SelectedIndex == 0)
                {
                    ddlCompany.Items.Clear();
                    ddlDepartment.Items.Clear();

                    ddlCompany.Enabled = false;
                    ddlDepartment.Enabled = false;
                }
                else
                {
                    Int64 GroupID = Convert.ToInt64(ddlGroup.SelectedValue);
                    GetAndPopulateCompany(GroupID);
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error selecting group, due to: " + ex.Message);
            }
        }

        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlCompany.SelectedIndex == 0)
                {
                    ddlDepartment.Items.Clear();
                    ddlDepartment.Enabled = false;
                }
                else
                {
                    Int64 GroupID = Convert.ToInt64(ddlGroup.SelectedItem.Value);
                    Int64 CompanyID = Convert.ToInt64(ddlCompany.SelectedItem.Value);

                    GetAndPopulateDepartment(CompanyID, GroupID);
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error selecting company, due to: " + ex.Message);
            }
        }
    }
}