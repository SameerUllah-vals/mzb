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
    public partial class Department : System.Web.UI.Page
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
        protected void Page_Load(object sender, EventArgs e)
        {
            notification("", "");
            if (!IsPostBack)
            {
                if (LoginID != 0 && LoginID != null)
                {

                    GetDepartment();

                    GroupDML gp = new GroupDML();
                    DataTable dtgp = gp.Getgroups();
                    if (dtgp.Rows.Count > 0)
                    {
                        FillDropDown(dtgp, ddlgroup, "GroupID", "GroupName", "Select Group");
                    }

                    CompanyDML dml = new CompanyDML();
                    DataTable dtcomp = dml.GetCompany();
                    if (dtcomp.Rows.Count > 0)
                    {
                        FillDropDown(dtcomp, ddlcompany, "CompanyID", "CompanyName", "Select Company");
                    }
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }


            }
        }
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



        public void GetDepartment()
        {
            DepartmentDML dml = new DepartmentDML();

            DataTable dtdept = dml.GetDepartment();
            if (dtdept.Rows.Count > 0)
            {
                gvResult.DataSource = dtdept;
            }
            else
            {
                gvResult.DataSource = null;

            }

            gvResult.DataBind();
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
        private void clearfield()
        {
            hfEditID.Value = string.Empty;
            txtCode.Text = "";
            txtContact.Text = "";
            txtEmail.Text = "";
            txtName.Text = "";
            txtOtherContact.Text = "";
            txtWebsite.Text = "";
            Address.Text = "";
            Description.Text = "";
            ddlcompany.ClearSelection();
            ddlgroup.ClearSelection();
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
                    notification("Error", "Please enter code");
                    txtCode.Focus();
                }
                else if (txtName.Text.Trim() == string.Empty)
                {
                    notification("Error", "Please enter name");
                    txtName.Focus();
                }
                else if (ddlgroup.SelectedIndex == 0)
                {
                    notification("Error", "Please Select Group");
                    txtName.Focus();
                }
                else if (ddlcompany.SelectedIndex == 0)
                {
                    notification("Error", "Please Select Company");
                    txtName.Focus();
                }
                else
                {

                    string Code = txtCode.Text;
                    string Name = txtName.Text;
                    DepartmentDML dm = new DepartmentDML();
                    DataTable dt = dm.GetDepartment(Code, Name);
                    if (hfEditID.Value.ToString() == string.Empty)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            notification("Error", "Another Department with same name or code exists in database, try to change name or code");
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
                            notification("Error", "Another department with same name or code exists in database, try to change name or code");
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
                notification("Error", "Error deleting , due to: " + ex.Message);
            }
        }

        protected void lnkCancelSearch_Click(object sender, EventArgs e)
        {
            try
            {
                GetDepartment();
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
            try
            {

                if (e.CommandName == "Change")
                {
                    pnlInput.Visible = true;
                    pnlView.Visible = false;
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvResult.Rows[index];
                    Int64 DeptID = Convert.ToInt64(gvResult.DataKeys[index]["DepartID"]);
                    hfEditID.Value = DeptID.ToString();


                    DepartmentDML dml = new DepartmentDML();
                    DataTable dtdept = dml.GetDepartment(DeptID);
                    if (dtdept.Rows.Count > 0)
                    {
                        txtCode.Text = dtdept.Rows[0]["DepartCode"].ToString();
                        txtName.Text = dtdept.Rows[0]["DepartName"].ToString();
                        txtEmail.Text = dtdept.Rows[0]["EmailAdd"].ToString();
                        txtContact.Text = dtdept.Rows[0]["contact"].ToString();
                        txtOtherContact.Text = dtdept.Rows[0]["ContactOther"].ToString();
                        Address.Text = dtdept.Rows[0]["Address"].ToString();
                        Description.Text = dtdept.Rows[0]["Description"].ToString();
                        txtWebsite.Text = dtdept.Rows[0]["WebAdd"].ToString();

                        ddlcompany.ClearSelection();
                        ddlcompany.Items.FindByValue(dtdept.Rows[0]["CompanyID"].ToString()).Selected = true;

                        ddlgroup.ClearSelection();
                        ddlgroup.Items.FindByValue(dtdept.Rows[0]["GroupID"].ToString()).Selected = true;
                        //txtWebsite.Text = dtdept.Rows[0]["Website"].ToString();
                        lnkDelete.Visible = true;



                        //ddlcompany.ClearSelection();
                        //ddlcompany.Items.FindByValue(dtdept.Rows[0]["Company"].ToString()).Selected = true;


                        //ddlgroup.ClearSelection();
                        //ddlgroup.Items.FindByValue(dtdept.Rows[0]["Group"].ToString()).Selected = true;


                    }
                }
                else if (e.CommandName == "View")
                {

                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                    //reset();
                    pnlInput.Visible = false;
                    pnlView.Visible = true;


                    int index2 = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr2 = gvResult.Rows[index2];
                    Int64 DeptID2 = Convert.ToInt64(gvResult.DataKeys[index2]["DepartID"]);



                    DepartmentDML dml2 = new DepartmentDML();
                    DataTable dtdept2 = dml2.GetDepartment(DeptID2);
                    if (dtdept2.Rows.Count > 0)
                    {
                        txtCodeModal.Text = dtdept2.Rows[0]["DepartCode"].ToString();
                        txtNameModal.Text = dtdept2.Rows[0]["DepartName"].ToString();
                        txtEmailModal.Text = dtdept2.Rows[0]["EmailAdd"].ToString();
                        txtContactModal.Text = dtdept2.Rows[0]["contact"].ToString();
                        txtOtherContactModal.Text = dtdept2.Rows[0]["ContactOther"].ToString();
                        txtAddressModal.Text = dtdept2.Rows[0]["Address"].ToString();
                        txtDescriptionModal.Text = dtdept2.Rows[0]["Description"].ToString();


                        txtCompanyModal.Text = dtdept2.Rows[0]["CompanyName"].ToString();



                        txtGroupModal.Text = dtdept2.Rows[0]["Groups"].ToString();
                        txtWebsiteModal.Text = dtdept2.Rows[0]["WebAdd"].ToString();

                    }

                }

             else if (e.CommandName == "Active")
                {

                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvResult.Rows[index];
                    Int64 ID = Convert.ToInt64(gvResult.DataKeys[index]["DepartID"]);
                    string Active = gvResult.DataKeys[index]["isActive"].ToString() == string.Empty ? "False" : gvResult.DataKeys[index]["isActive"].ToString();
                    DepartmentDML dml = new DepartmentDML();

                    hfEditID.Value = ID.ToString();

                    if (Active == "True")
                    {
                        dml.DeactivateDepart(ID, LoginID);
                    }
                    else
                    {
                        dml.ActivateDepart(ID, LoginID);
                    }
                    GetDepartment();
                    clearfield();

                }
            }
            catch (Exception ex)
            {
                notification("Error", ex.Message);
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
            DepartmentDML dm = new DepartmentDML();
            DataTable dt = dm.GetDepartment(keyword);
            if (dt.Rows.Count > 0)
            {
                gvResult.DataSource = dt;
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

                        Int64 id = Convert.ToInt64(hfEditID.Value);
                        DepartmentDML dml = new DepartmentDML();
                        dml.DeleteDepartment(id);
                        clearfield();
                        lnkDelete.Visible = false;
                        notification("Success", "Deleted Successfully");
                        GetDepartment();
                        clearfield();
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
                        string Code = txtCode.Text;
                        string Name = txtName.Text;
                        string Email = txtEmail.Text;
                        Int64 Group = Convert.ToInt64(ddlgroup.SelectedValue);
                        Int64 Company = Convert.ToInt64(ddlcompany.SelectedValue);
                        string Website = txtWebsite.Text;
                        Int64 ContactNo = txtContact.Text == string.Empty ? 0 : Convert.ToInt64(txtContact.Text);
                        Int64 OtherContact = txtOtherContact.Text == string.Empty ? 0 : Convert.ToInt64(txtOtherContact.Text);
                        string address = Address.Text;
                        string description = Description.Text;

                        DepartmentDML dm = new DepartmentDML();
                        dm.InsertDepartment(Code, Name, Email, Group, Company, Website, ContactNo, OtherContact, address, description, LoginID);
                        pnlInput.Visible = false;
                        notification("Success", "Submited Successfully");
                        GetDepartment();
                        clearfield();
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
                        string Name = txtName.Text;
                        string Email = txtEmail.Text;
                        Int64 Group = Convert.ToInt64(ddlgroup.SelectedValue);
                        Int64 Company = Convert.ToInt64(ddlcompany.SelectedValue);
                        string Website = txtWebsite.Text;
                        Int64 ContactNo = txtContact.Text == string.Empty ? 0 : Convert.ToInt64(txtContact.Text);
                        Int64 OtherContact = txtOtherContact.Text == string.Empty ? 0 : Convert.ToInt64(txtOtherContact.Text);
                        string address = Address.Text;
                        string description = Description.Text;

                        DepartmentDML dm = new DepartmentDML();
                        Int64 id = Convert.ToInt64(hfEditID.Value);
                        dm.UpdateDepartment(id, Code, Name, Email, Group, Company, Website, ContactNo, OtherContact, address, description, LoginID);
                        pnlInput.Visible = false;
                        notification("Success", "Updated Successfully");
                        GetDepartment();
                        clearfield();
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

        protected void lnkCloseInput_Click1(object sender, EventArgs e)
        {

        }
    }
}
