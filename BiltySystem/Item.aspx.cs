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
    public partial class Item : System.Web.UI.Page
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
            txtCode.Focus();
            if (!IsPostBack)
            {
                if (LoginID != 0 && LoginID != null)
                {

                    ItemDML dml = new ItemDML();

                    GetItems();


                    //Getting Populating Owners
                    //try
                    //{
                    //    DataTable dtOwners = dml.GetOwners();
                    //    if (dtOwners.Rows.Count > 0)
                    //    {
                    //        FillDropDown(dtOwners, ddlOwner, "OwnerID", "OwnerName", "-Select Owner-");
                    //    }
                    //}
                    //catch (Exception ex)
                    //{
                    //    notification("Error", "Error populating Onwer's dropdown, due to: " + ex.Message);
                    //}
                }
                else
                {
                    Response.Redirect("Login.aspx");
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

        public void ClearFields()
        {
            try
            {
                hfEditID.Value = string.Empty;

                txtCode.Text = string.Empty;
                txtName.Text = string.Empty;
                txtDescription.Text = string.Empty;
                //ddlOwner.SelectedIndex = 0;
                cbGeneral.Checked = false;
            }
            catch (Exception ex)
            {
                notification("Error", "Error clearing fields, due to: " + ex.Message);
            }
        }

        public void GetItems()
        {
            try
            {
                ItemDML dml = new ItemDML();

                DataTable dtItems = dml.GetItem();
                if (dtItems.Rows.Count > 0)
                {
                    gvResult.DataSource = dtItems;
                }
                else
                {
                    gvResult.DataSource = null;
                }
                gvResult.DataBind();
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting/populating items to grid, due to: " + ex.Message);
            }
        }

        public void ExportToExcel(DataTable _dt, string FileName)
        {
            try
            {
                string attachment = "attachment; filename=" + FileName + "_" + DateTime.Now + ".xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.ms-excel";
                string tab = "";

                foreach (DataColumn dc in _dt.Columns)
                {
                    Response.Write(tab + dc.ColumnName);
                    tab = "\t";
                }

                Response.Write("\n");

                int i;
                foreach (DataRow dr in _dt.Rows)
                {
                    tab = "";
                    for (i = 0; i < _dt.Columns.Count; i++)
                    {
                        Response.Write(tab + dr[i].ToString());
                        tab = "\t";


                    }
                    Response.Write("\n");
                }

                Response.End();
            }
            catch (Exception ex)
            {
                notification("Error", "Error exporting excel, due to: " + ex.Message);
            }
        }

        public DataTable GridToDT(GridView _gv)
        {
            DataTable _dt = new DataTable();
            for (int i = 0; i < _gv.HeaderRow.Cells.Count; i++)
            {
                if (_gv.HeaderRow.Cells[i].Text == "&nbsp;" || _gv.HeaderRow.Cells[i].Text == string.Empty)
                {

                }
                else
                {
                    _dt.Columns.Add(_gv.HeaderRow.Cells[i].Text.Replace("&nbsp;", string.Empty));
                }
            }

            foreach (GridViewRow row in _gv.Rows)
            {
                DataRow _dr = _dt.NewRow();
                for (int j = 0; j < _gv.HeaderRow.Cells.Count; j++)
                {
                    if (_gv.HeaderRow.Cells[j].Text == "&nbsp;" || _gv.HeaderRow.Cells[j].Text == string.Empty)
                    {

                    }
                    else
                    {
                        _dr[j] = row.Cells[j].Text.Replace("&nbsp;", string.Empty);
                    }
                }
                _dt.Rows.Add(_dr);
            }
            return _dt;
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

        protected void lnkCloseView_Click(object sender, EventArgs e)
        {
            try
            {
                pnlView.Visible = false;
            }
            catch (Exception ex)
            {

                notification("Error", ex.Message);
            }
        }

        protected void lnkSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCode.Text.Trim() == string.Empty)
                {
                    txtCode.Focus();
                    notification("Error", "Please enter Item's Code");
                }
                else if (txtName.Text.Trim() == string.Empty)
                {
                    txtName.Focus();
                    notification("Error", "Please enter Item name");
                }


                else
                {

                    ItemDML dml = new ItemDML();
                    Random rnd = new Random();

                    string code = txtCode.Text == string.Empty ? rnd.Next().ToString() : txtCode.Text.Trim();
                    string name = txtName.Text.Trim();


                    if (hfEditID.Value == string.Empty)
                    {
                        DataTable dtITem = dml.GetItem(code, name);
                        if (dtITem.Rows.Count > 0)
                        {
                            notification("Error", "Another item with same code or name exist, try to change name or code");
                        }
                        else
                        {
                            ConfirmModal("Are you sure want to Save?", "Save");
                        }

                    }
                    else
                    {
                        DataTable dtITem = dml.GetItem(code, name);
                        if (dtITem.Rows.Count > 1)
                        {
                            notification("Error", "Another item with same code or name exist, try to change name or code");
                        }
                        else
                        {
                            ConfirmModal("Are you sure want to Update?", "Update");

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error saving Item, due to: " + ex.Message);
            }
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            try
            {
                ConfirmModal("Are you sure want to delete?", "Delete");

            }
            catch (Exception ex)
            {

                notification("error", "Error with Deleting due to:" + ex.Message);
            }

            GetItems();
            pnlInput.Visible = false;
        }

        protected void lnkCancelSearch_Click(object sender, EventArgs e)
        {
            try
            {
                GetItems();
                txtSearch.Text = string.Empty;
            }
            catch (Exception ex)
            {

                notification("Error", ex.Message);
            }

        }

        protected void lnkAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                pnlInput.Visible = true;
            }
            catch (Exception ex)
            {

                notification("Error", ex.Message);
            }
        }

        protected void gvResult_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Change")
            {
                pnlView.Visible = false;
                pnlInput.Visible = true;
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow gvr = gvResult.Rows[index];
                Int64 ItemID = Convert.ToInt64(gvResult.DataKeys[index]["ItemID"]);
                hfEditID.Value = ItemID.ToString();
                ItemDML dml = new ItemDML();
                DataTable dtItem = dml.GetItem(ItemID);
                txtCode.Text = dtItem.Rows[0][1].ToString();
                //LinkButton myButton = (LinkButton)gvr.Cells[1].Controls[0];
                txtName.Text = dtItem.Rows[0][2].ToString();
                txtDescription.Text = dtItem.Rows[0][9].ToString();
                //ddlOwner.ClearSelection();
                //ddlOwner.Items.FindByValue(gvResult.DataKeys[index]["OwnerID"].ToString()).Selected = true;
                string general = dtItem.Rows[0][11].ToString();
                if (general == "True")
                {
                    cbGeneral.Checked = true;
                }
                else
                {
                    cbGeneral.Checked = false;
                }
                lnkDelete.Visible = true;
                pnlInput.Visible = true;

            }
            else if (e.CommandName == "View")
            {
                pnlView.Visible = true;
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow gvr = gvResult.Rows[index];
                Int64 ItemID = Convert.ToInt64(gvResult.DataKeys[index]["ItemID"]);
                hfEditID.Value = ItemID.ToString();

                ItemDML dml = new ItemDML();
                DataTable dtItem = dml.GetItem(ItemID);

                lblCode.Text = dtItem.Rows[0][1].ToString();
                lblName.Text = dtItem.Rows[0][2].ToString();
                lblDescription.Text = dtItem.Rows[0][9].ToString();
                //lblOwner.Text = dtItem.Rows[0]["OwnerName"].ToString();
                lblgeneral.Text = dtItem.Rows[0]["isGeneralItem"].ToString();
            }
            else if (e.CommandName == "Active")
            {

                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow gvr = gvResult.Rows[index];
                Int64 CatId = Convert.ToInt64(gvResult.DataKeys[index]["ItemID"]);
                string Active = gvResult.DataKeys[index]["isActive"].ToString() == string.Empty ? "False" : gvResult.DataKeys[index]["isActive"].ToString();
                ItemDML dml = new ItemDML();

                hfEditID.Value = CatId.ToString();

                if (Active == "True")
                {
                    dml.DeactivateItem(CatId, LoginID);
                }
                else
                {
                    dml.ActivateItem(CatId, LoginID);
                }
                GetItems();
                ClearFields();

            }
        }

        protected void gvResult_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView rowView = (DataRowView)e.Row.DataItem;
                    bool Active = Convert.ToBoolean(rowView["isActive"].ToString() == string.Empty ? "false" : rowView["isActive"]);
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
                notification("Error", "Binding data to grid, due to: " + ex.Message);
            }
        }

        protected void lnkCloseInput_Click(object sender, EventArgs e)
        {
            try
            {
                pnlInput.Visible = false;
            }
            catch (Exception ex)
            {

                notification("Error", ex.Message);
            }
        }

        protected void lnkSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();
            ItemDML dm = new ItemDML();
            DataTable dtitem = dm.GetItem(keyword);
            if (dtitem.Rows.Count > 0)
            {
                gvResult.DataSource = dtitem;
            }
            else
            {
                gvResult.DataSource = null;
            }
            gvResult.DataBind();
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
                        Int64 ItemID = Convert.ToInt64(hfEditID.Value);

                        ItemDML dml = new ItemDML();
                        dml.DeleteItemByID(ItemID);
                        notification("Success", "Item deleted successfully");

                        lnkDelete.Visible = false;
                        ClearFields();
                        GetItems();
                        pnlInput.Visible = false;
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
                        ItemDML dml = new ItemDML();
                        Random rnd = new Random();

                        string code = txtCode.Text == string.Empty ? rnd.Next().ToString() : txtCode.Text.Trim();
                        string name = txtName.Text.Trim();
                        //Int64 OwnerID = Convert.ToInt64(ddlOwner.SelectedValue);
                        string description = txtDescription.Text.Trim();
                        string isGeneral = cbGeneral.Checked ? "True" : "False";
                        if (dml.InsertItem(code, name, isGeneral, description, LoginID) > 0)
                        {
                            notification("Success", "Item inserted Succesfully");

                            txtCode.Focus();
                            pnlInput.Visible = false;
                            ClearFields();
                            GetItems();
                        }
                        else
                        {
                            notification("Error", "Item not inserted");


                        }
                    }
                    catch (Exception ex)
                    {

                        notification("Error", ex.Message);
                    }

                }
                else if (Action == "Update")
                {
                    try
                    {
                        ItemDML dml = new ItemDML();
                        Random rnd = new Random();

                        string code = txtCode.Text == string.Empty ? rnd.Next().ToString() : txtCode.Text.Trim();
                        string name = txtName.Text.Trim();
                        //Int64 OwnerID = Convert.ToInt64(ddlOwner.SelectedValue);
                        string description = txtDescription.Text.Trim();
                        string isGeneral = cbGeneral.Checked ? "True" : "False";
                        Int64 itemID = Convert.ToInt64(hfEditID.Value);

                        dml.UpdateItem(itemID, code, name, isGeneral, description, LoginID);

                        notification("Success", "Item updated successfully");



                        pnlInput.Visible = false;

                        ClearFields();
                        GetItems();
                    }
                    catch (Exception ex)
                    {

                        notification("Error", ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error confirming, due to: " + ex.Message);
            }
        }

        #endregion
    }
}