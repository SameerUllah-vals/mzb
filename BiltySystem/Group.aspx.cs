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
    public partial class WebForm3 : System.Web.UI.Page
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

        #region Page Load

        protected void Page_Load(object sender, EventArgs e)
        {

            notification("", "");
            if (!IsPostBack)
            {
                if (LoginID != 0 && LoginID != null)
                {
                    GetGroup();
                    OrganizationDML dmlOrganization = new OrganizationDML();
                    DataTable dtOrganization = dmlOrganization.GetOrganization();
                    if (dtOrganization.Rows.Count > 0)
                    {
                        List<string> listContents = new List<string>();// Create a List of String Elements  
                        for (int i = 0; i < dtOrganization.Rows.Count; i++)
                        {
                            listContents.Add(dtOrganization.Rows[i]["Name"].ToString());
                        }

                        cbOrganizations.DataSource = listContents;//Set Datasource to CheckBox List  
                        cbOrganizations.DataBind();
                    }
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }

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

        private void FillCheckboxList(DataTable _dt, CheckBoxList _cbl, string _ddlValue, string _ddlText)
        {
            if (_dt.Rows.Count > 0)
            {
                _cbl.DataSource = _dt;

                _cbl.DataValueField = _ddlValue;
                _cbl.DataTextField = _ddlText;

                //_cbl.DataBind();

                //ListItem item = new ListItem();

                //item.Text = _ddlDefaultText;
                //item.Value = _ddlDefaultText;

                //_ddl.Items.Insert(0, item);
                //_ddl.SelectedIndex = 0;
            }
        }

        #endregion

        #region Custom Methods

        public void GetGroup()
        {
            try
            {
                GroupDML dml = new GroupDML();
                DataTable dtgp = dml.Getgroups();
                if (dtgp.Rows.Count > 0)
                {
                    gvResult.DataSource = dtgp;
                }
                else
                {
                    gvResult.DataSource = null;
                }
                gvResult.DataBind();
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting Group, due to: " + ex.Message);
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

        public void ClearFields()
        {
            try
            {
                hfEditID.Value = string.Empty;
                txtCode.Text = "";
                txtAddress.Text = "";
                txtContact.Text = "";
                txtDescription.Text = "";
                txtemail.Text = "";
                txtGroup.Text = "";
                txtOtherContact.Text = "";
                txtWebsite.Text = "";
                cbOrganizations.ClearSelection();
                txtCompanyAccess.Text = string.Empty;



                //pnlInput.Visible = false;
            }
            catch (Exception ex)
            {
                notification("Error", "Error clearing fields, due to: " + ex.Message);
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

        #endregion


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
                    notification("Error", "Please enter Code.");
                    txtCode.Focus();
                }
                else if (txtGroup.Text.Trim() == string.Empty)
                {
                    notification("Error", "Please enter Group.");
                    txtCode.Focus();
                }

                string Code = txtCode.Text;
                string group = txtGroup.Text;

                string organization = string.Empty;


                for (int i = 0; i < cbOrganizations.Items.Count; i++)
                {
                    if (cbOrganizations.Items[i].Selected == true)
                    {
                        organization += cbOrganizations.Items[i].Text + ", ";
                    }
                }

                organization = organization != string.Empty ? organization.Remove(organization.Length - 2) : string.Empty;




                GroupDML gp = new GroupDML();
                DataTable dt = gp.GetGroup(Code, group);
                if (hfEditID.Value.ToString() == string.Empty)
                {
                    if (dt.Rows.Count > 0)
                    {
                        notification("Error", "Another Group with same name or code exists in database, try to change name or code");
                    }
                    else
                    {
                        ConfirmModal("Are you sure want to save?", "Save");

                    }
                }
                else
                {
                    if (dt.Rows.Count > 1)
                    {
                        notification("Error", "Another Group with same name or code exists in database, try to change name or code");
                    }
                    else
                    {
                        ConfirmModal("Are you sure want to Update?", "Update");

                    }
                }

            }
            catch (Exception ex)
            {

                notification("Error", ex.Message);
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

            GetGroup();
            pnlInput.Visible = false;
        }

        protected void lnkCancelSearch_Click(object sender, EventArgs e)
        {
            try
            {
                GetGroup();
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
                pnlInput.Visible = true;
                pnlView.Visible = false;
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow gvr = gvResult.Rows[index];
                Int64 ID = Convert.ToInt64(gvResult.DataKeys[index]["GroupID"]);
                hfEditID.Value = ID.ToString();


                GroupDML dml = new GroupDML();
                DataTable dtcomp = dml.GetGroup(ID);
                if (dtcomp.Rows.Count > 0)
                {
                    txtCode.Text = dtcomp.Rows[0]["GroupCode"].ToString();
                    txtGroup.Text = dtcomp.Rows[0]["GroupName"].ToString();
                    txtemail.Text = dtcomp.Rows[0]["EmailAdd"].ToString();
                    txtWebsite.Text = dtcomp.Rows[0]["WebAdd"].ToString();
                    txtContact.Text = dtcomp.Rows[0]["Contact"].ToString();
                    txtOtherContact.Text = dtcomp.Rows[0]["ContactOther"].ToString();
                    txtAddress.Text = dtcomp.Rows[0]["Address"].ToString();
                    txtDescription.Text = dtcomp.Rows[0]["Description"].ToString();
                    txtCompanyAccess.Text = dtcomp.Rows[0]["CompanyAccess"].ToString();

                    //cbOrganizations.ClearSelection();
                    //cbOrganizations.Items.FindByValue(dtcomp.Rows[0]["Organization"].ToString()).Selected = true;




                    lnkDelete.Visible = true;
                    GetGroup();
                }
            }
            else if (e.CommandName == "View")
            {

                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                ClearFields();
                pnlInput.Visible = false;
                pnlView.Visible = true;


                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow gvr = gvResult.Rows[index];
                Int64 ID = Convert.ToInt64(gvResult.DataKeys[index]["GroupID"]);



                GroupDML dml = new GroupDML();
                DataTable dtcomp = dml.GetGroup(ID);
                if (dtcomp.Rows.Count > 0)
                {
                    txtCodeModal.Text = dtcomp.Rows[0]["GroupCode"].ToString();
                    txtGroupModal.Text = dtcomp.Rows[0]["GroupName"].ToString();
                    txtEmailModal.Text = dtcomp.Rows[0]["EmailAdd"].ToString();
                    txtWebsiteModal.Text = dtcomp.Rows[0]["WebAdd"].ToString();
                    txtContactModal.Text = dtcomp.Rows[0]["Contact"].ToString();
                    txtOtherModal.Text = dtcomp.Rows[0]["ContactOther"].ToString();
                    txtAddressModal.Text = dtcomp.Rows[0]["Address"].ToString();
                    txtDescriptionModal.Text = dtcomp.Rows[0]["Description"].ToString();
                    //txtOrganiModal.Text = dtcomp.Rows[0]["Organization"].ToString();
                }

            }
            else if (e.CommandName == "Active")
            {

                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow gvr = gvResult.Rows[index];
                Int64 CatId = Convert.ToInt64(gvResult.DataKeys[index]["GroupID"]);
                string Active = gvResult.DataKeys[index]["isActive"].ToString() == string.Empty ? "False" : gvResult.DataKeys[index]["isActive"].ToString();
                GroupDML dml = new GroupDML();

                hfEditID.Value = CatId.ToString();

                if (Active == "True")
                {
                    dml.DeactivateGroup(CatId, LoginID);
                }
                else
                {
                    dml.ActivateGroup(CatId, LoginID);
                }
                GetGroup();
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
            GroupDML dm = new GroupDML();
            DataTable dtgrp = dm.GetGroup(keyword);
            if (dtgrp.Rows.Count > 0)
            {
                gvResult.DataSource = dtgrp;
            }
            else
            {
                gvResult.DataSource = null;
            }
            gvResult.DataBind(); ;
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
                        Int64 gpID = Convert.ToInt64(hfEditID.Value);
                        if (gpID > 0)
                        {
                            GroupDML dml = new GroupDML();
                            dml.DeleteGroup(gpID);
                            notification("Success", "Group deleted successfully");

                            ClearFields();
                            GetGroup();
                            pnlInput.Visible = false;
                            lnkDelete.Visible = false;
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
                        string Code = txtCode.Text;
                        string group = txtGroup.Text;
                        string email = txtemail.Text;
                        string website = txtWebsite.Text;
                        string contact = txtContact.Text;
                        string othercontact = txtOtherContact.Text;
                        string organization = string.Empty;
                        string address = txtAddress.Text;
                        string CompanyAccess = txtCompanyAccess.Text;

                        for (int i = 0; i < cbOrganizations.Items.Count; i++)
                        {
                            if (cbOrganizations.Items[i].Selected == true)
                            {
                                organization += cbOrganizations.Items[i].Text + ", ";
                            }
                        }

                        organization = organization != string.Empty ? organization.Remove(organization.Length - 2) : string.Empty;


                        string description = txtDescription.Text;

                        GroupDML gp = new GroupDML();
                        gp.InsertGroup(Code, group, email, website, contact, othercontact, organization, address, description, LoginID, CompanyAccess);
                        pnlInput.Visible = false;
                        notification("Success", "Group inserted successfully");

                        ClearFields();
                        GetGroup();
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
                        string Code = txtCode.Text;
                        string group = txtGroup.Text;
                        string email = txtemail.Text;
                        string website = txtWebsite.Text;
                        string contact = txtContact.Text;
                        string othercontact = txtOtherContact.Text;
                        string organization = string.Empty;
                        string address = txtAddress.Text;
                        string CompanyAccess = txtCompanyAccess.Text;

                        for (int i = 0; i < cbOrganizations.Items.Count; i++)
                        {
                            if (cbOrganizations.Items[i].Selected == true)
                            {
                                organization += cbOrganizations.Items[i].Text + ", ";
                            }
                        }

                        organization = organization != string.Empty ? organization.Remove(organization.Length - 2) : string.Empty;


                        string description = txtDescription.Text;

                        GroupDML gp = new GroupDML();
                        Int64 id = Convert.ToInt64(hfEditID.Value);
                        gp.UpdateGroup(id, Code, group, email, website, contact, othercontact, organization, address, description, LoginID, CompanyAccess);
                        pnlInput.Visible = false;
                        notification("Success", "Group updated successfully");

                        ClearFields();
                        GetGroup();
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
    }
}