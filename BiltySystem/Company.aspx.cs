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
    public partial class WebForm2 : System.Web.UI.Page
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
          
            notification();
            if (!IsPostBack)
            {
                if (LoginID > 0)
                {
                    this.Title = "Company";
                    GroupDML dml = new GroupDML();
                    DataTable dtgroup = dml.Getgroups();
                    if (dtgroup.Rows.Count > 0)
                    {
                        FillDropDown(dtgroup, ddlgroup, "GroupID", "GroupName", "-Select Group-");
                    }
                    GetCompany();
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }

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

        public void GetCompany()
        {
            try
            {
                CompanyDML dml = new CompanyDML();
                DataTable dtcomp = dml.GetCompany();
                if (dtcomp.Rows.Count > 0)
                {
                    gvResult.DataSource = dtcomp;
                }
                else
                {
                    gvResult.DataSource = null;
                }
                gvResult.DataBind();
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting Company, due to: " + ex.Message);
            }
        }

        public void ClearFields()
        {
            try
            {
                hfEditID.Value = string.Empty;
                txtCode.Text = string.Empty;
                txtName.Text = string.Empty;
                txtemail.Text = string.Empty;
                ddlgroup.ClearSelection();
                txtcontact.Text = string.Empty;
                txtOtherContact.Text = string.Empty;
              
                txtDescription.Text = string.Empty;
                txtAddress.Text = string.Empty;

                pnlInput.Visible = false;
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
                    _dt.Columns.Add(_gv.HeaderRow.Cells[i].Text);
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
                        _dr[j] = row.Cells[j].Text;
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
        
        protected void lnkSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCode.Text == string.Empty)
                {
                    notification("Error", "Please enter Company Code");
                    txtCode.Focus();
                }
                else if (txtName.Text == string.Empty)
                {
                    notification("Error", "Please enter Company Name");
                    txtName.Focus();
                }
                else if (ddlgroup.SelectedIndex == 0)
                {
                    notification("Error", "Please Select Group");
                    ddlgroup.Focus();
                }
                else
                {
                    string Code = txtCode.Text.Trim();
                    string Name = txtName.Text.Trim();

                    CompanyDML dml = new CompanyDML();
                    DataTable dt = dml.GetCompany(Code, Name);


                    if (hfEditID.Value.ToString() == string.Empty)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            notification("Error", "Another Company with same name or code exists in database, try to change name or code");
                        }
                        else
                        {
                            ConfirmModal("Are you sure want to Save?", "Save");
                          

                        }
                    }
                    else
                    {
                        if (dt.Rows.Count > 1)
                        {
                            notification("Error", "Another Company with same name or code exists in database, try to change name or code");
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

                notification("Error", ex.Message);
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
                notification("Error", "Error delete, due to: " + ex.Message);
            }
        }

        protected void lnkCancelSearch_Click(object sender, EventArgs e)
        {
            try
            {
                txtSearch.Text = string.Empty;
                GetCompany();
            }
            catch (Exception ex)
            {

                notification("Error", ex.Message);
            }
        }

        protected void lnkSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();
            CompanyDML dm = new CompanyDML();
            DataTable dtcomp = dm.GetCompany(keyword);
            if (dtcomp.Rows.Count > 0)
            {
                gvResult.DataSource = dtcomp;
            }
            else
            {
                gvResult.DataSource = null;
            }
            gvResult.DataBind();
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
                Int64 compID = Convert.ToInt64(gvResult.DataKeys[index]["CompanyID"]);
                hfEditID.Value = compID.ToString();


                CompanyDML dml = new CompanyDML();
                DataTable dtcomp = dml.GetCompany(compID);
                if (dtcomp.Rows.Count > 0)
                {
                    txtCode.Text = dtcomp.Rows[0]["CompanyCode"].ToString();
                    txtName.Text = dtcomp.Rows[0]["CompanyName"].ToString();
                    txtemail.Text = dtcomp.Rows[0]["CompanyEmail"].ToString();
                    txtcontact.Text = dtcomp.Rows[0]["Contact"].ToString();
                    txtOtherContact.Text = dtcomp.Rows[0]["OtherContact"].ToString();
                    txtAddress.Text = dtcomp.Rows[0]["Address"].ToString();
                    txtDescription.Text = dtcomp.Rows[0]["Description"].ToString();
                    txtWebsite.Text = dtcomp.Rows[0]["CompanyWebsite"].ToString();






                    ddlgroup.ClearSelection();
                    ddlgroup.Items.FindByValue(dtcomp.Rows[0]["GroupID"].ToString()).Selected = true;

                   

                    lnkDelete.Visible = true;
                    GetCompany();
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
                Int64 compID = Convert.ToInt64(gvResult.DataKeys[index]["CompanyID"]);

                CompanyDML dml = new CompanyDML();
                DataTable dtcomp = dml.GetCompany(compID);
                if (dtcomp.Rows.Count > 0)
                {
                    txtCodeModal.Text = dtcomp.Rows[0]["CompanyCode"].ToString();
                    txtNameModal.Text = dtcomp.Rows[0]["CompanyName"].ToString();
                    txtEmailModal.Text = dtcomp.Rows[0]["CompanyEmail"].ToString();
                    txtContactModal.Text = dtcomp.Rows[0]["Contact"].ToString();
                    txtOtherModal.Text = dtcomp.Rows[0]["OtherContact"].ToString();
                    txtAddressModal.Text = dtcomp.Rows[0]["Address"].ToString();
                    txtDescriptionModal.Text = dtcomp.Rows[0]["Description"].ToString();
                    txtGroupModal.Text = dtcomp.Rows[0]["GroupName"].ToString();
                    lblWebsite.Text = dtcomp.Rows[0]["CompanyWebsite"].ToString();






                }
            }
            else if (e.CommandName == "Active")
            {

                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow gvr = gvResult.Rows[index];
                Int64 CompId = Convert.ToInt64(gvResult.DataKeys[index]["CompanyID"]);
                string Active = gvResult.DataKeys[index]["Active"].ToString() == string.Empty ? "False" : gvResult.DataKeys[index]["Active"].ToString();
                CompanyDML dml = new CompanyDML();

                hfEditID.Value = CompId.ToString();

                if (Active == "True")
                {
                    dml.DeactivateCompany(CompId, LoginID);
                }
                else
                {
                    dml.ActivateCompany(CompId, LoginID);
                }
                GetCompany();
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
                notification("Error", "Binding data to grid, due to: " + ex.Message);
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
                        Int64 ID = Convert.ToInt64(hfEditID.Value);
                        CompanyDML dml = new CompanyDML();
                        dml.DeleteCompany(ID);

                        ClearFields();
                        pnlInput.Visible = false;
                        GetCompany();

                        notification("Success", "Deleted successfully");
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
                        string Code = txtCode.Text.Trim();
                        string Name = txtName.Text.Trim();
                        Int64 Phone = txtcontact.Text == string.Empty ? 0 : Convert.ToInt64(txtcontact.Text.Trim());
                        Int64 OtherPhone = txtOtherContact.Text == string.Empty ? 0 : Convert.ToInt64(txtOtherContact.Text.Trim());
                        string Email = txtemail.Text;
                        Int64 group = ddlgroup.SelectedIndex == 0 ? 0 : Convert.ToInt64(ddlgroup.SelectedValue);
                        string Address = txtAddress.Text.Trim();
                        string Description = txtDescription.Text.Trim();
                        string website = txtWebsite.Text;
                        string NTN = txtNTN.Text;

                        CompanyDML dml = new CompanyDML();

                        dml.InsertCompany(Code, Name, Email, group, Phone, OtherPhone, website, NTN, Address, Description, LoginID);
                        pnlInput.Visible = false;
                        notification("Success", "Submited Successfully");
                        ClearFields();
                        GetCompany();
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
                        string Code = txtCode.Text.Trim();
                        string Name = txtName.Text.Trim();
                        Int64 Phone = txtcontact.Text == string.Empty ? 0 : Convert.ToInt64(txtcontact.Text.Trim());
                        Int64 OtherPhone = txtOtherContact.Text == string.Empty ? 0 : Convert.ToInt64(txtOtherContact.Text.Trim());
                        string Email = txtemail.Text;
                        Int64 group = ddlgroup.SelectedIndex == 0 ? 0 : Convert.ToInt64(ddlgroup.SelectedValue);
                        string Address = txtAddress.Text.Trim();
                        string Description = txtDescription.Text.Trim();
                        string website = txtWebsite.Text;

                        string NTN = txtNTN.Text;
                        Int64 id = Convert.ToInt64(hfEditID.Value);
                        CompanyDML dml = new CompanyDML();
                        dml.UpdateCompany(id, Code, Name, Email, group, Phone, OtherPhone, website, NTN, Address, Description, LoginID);
                        pnlInput.Visible = false;
                        notification("Success", "Updated Successfully");
                        ClearFields();
                        GetCompany();
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