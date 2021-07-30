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
    public partial class DepartmentPerson : System.Web.UI.Page
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

                    GetDepartmentPerson();

                    GroupDML gp = new GroupDML();
                    DataTable dtgp = gp.Getgroups();
                    if (dtgp.Rows.Count > 0)
                    {
                        FillDropDown(dtgp, ddlGroup, "GroupID", "GroupName", "Select Group");
                    }

                    CompanyDML dml = new CompanyDML();
                    DataTable dtcomp = dml.GetCompany();
                    if (dtcomp.Rows.Count > 0)
                    {
                        FillDropDown(dtcomp, ddlcompany, "CompanyID", "CompanyName", "Select Company");
                    }

                    DepartmentDML dp = new DepartmentDML();
                    DataTable dtdep = dp.GetDepartment();
                    if (dtdep.Rows.Count > 0)
                    {
                        FillDropDown(dtdep, ddlDepartment, "DepartID", "DepartName", "Select Department");
                    }
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


        public void GetDepartmentPerson()
        {
            DepartmentPersonDML dml = new DepartmentPersonDML();

            DataTable dtdept = dml.GetDepartmentPerson();
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



        private void clearfield()
        {
            hfEditID.Value = string.Empty;
            txtCode.Text = "";
            txtBusiness.Text = "";
            txtEmail.Text = "";
            txtName.Text = "";
            txtDesignation.Text = "";
            txtCellNo.Text = "";
            txtLandline.Text = "";
            txtOtherContact.Text = "";
            txtAddOther.Text = "";
            txtAddOffice.Text = "";
            Description.Text = "";
            ddlcompany.ClearSelection();
            ddlDepartment.ClearSelection();
            ddlGroup.ClearSelection();

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
                }

                else if (ddlGroup.SelectedIndex == 0)
                {
                    notification("Error", "Please select group");
                    ddlGroup.Focus();
                }
                else if (ddlcompany.SelectedIndex == 0)
                {
                    notification("Error", "Please enter company");
                    ddlcompany.Focus();
                }
                else if (ddlDepartment.SelectedIndex == 0)
                {
                    notification("Error", "Please enter department");
                    ddlDepartment.Focus();
                }

                else
                {

                    string Code = txtCode.Text;
                    string Name = txtName.Text;


                    DepartmentPersonDML dm = new DepartmentPersonDML();
                    DataTable dt = dm.GetDepartmentPerson(Code, Name);
                    if (hfEditID.Value.ToString() == string.Empty)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            notification("Error", "Another Department Person with same name or code exists in database, try to change name or code");
                        }
                        else
                        {
                            ConfirmModal("Are you sure want to Save?","Save");

                        }
                    }
                    else
                    {
                        if (dt.Rows.Count > 1)
                        {
                            notification("Error", "Another department person with same name or code exists in database, try to change name or code");
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
                ConfirmModal("Are you sure want to delete?", "Delete");

            }
            catch (Exception ex)
            {

                notification("error", "Error with Deleting due to:" + ex.Message);
            }

            GetDepartmentPerson();
            pnlInput.Visible = false;
        }

        protected void lnkCancelSearch_Click(object sender, EventArgs e)
        {
            try
            {
                GetDepartmentPerson();
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
                Int64 DeptID = Convert.ToInt64(gvResult.DataKeys[index]["PersonalID"]);
                hfEditID.Value = DeptID.ToString();


                DepartmentPersonDML dml = new DepartmentPersonDML();
                DataTable dtdept = dml.GetDepartmentPerson(DeptID);
                if (dtdept.Rows.Count > 0)
                {
                    txtCode.Text = dtdept.Rows[0]["Code"].ToString();
                    txtName.Text = dtdept.Rows[0]["Name"].ToString();
                    txtEmail.Text = dtdept.Rows[0]["Email"].ToString();
                    txtBusiness.Text = dtdept.Rows[0]["BusinessEmail"].ToString();
                    txtDesignation.Text = dtdept.Rows[0]["Designation"].ToString();
                    txtCellNo.Text = dtdept.Rows[0]["Cell"].ToString();
                    txtLandline.Text = dtdept.Rows[0]["PhoneNo"].ToString();
                    txtOtherContact.Text = dtdept.Rows[0]["OtherContact"].ToString();
                    txtAddOffice.Text = dtdept.Rows[0]["AddressOffice"].ToString();
                    txtAddOther.Text = dtdept.Rows[0]["AddressOther"].ToString();
                    Description.Text = dtdept.Rows[0]["Description"].ToString();

                    ddlcompany.ClearSelection();
                    ddlcompany.Items.FindByValue(dtdept.Rows[0]["CompanyID"].ToString()).Selected = true;

                    ddlGroup.ClearSelection();
                    ddlGroup.Items.FindByValue(dtdept.Rows[0]["GroupID"].ToString()).Selected = true;

                    ddlDepartment.ClearSelection();
                    ddlDepartment.Items.FindByValue(dtdept.Rows[0]["DepartmentID"].ToString()).Selected = true;

                    bool indiv = Convert.ToBoolean(dtdept.Rows[0]["IsIndividual"]);
                    if (indiv == true)
                    {
                        cbIsIndividual.Checked = true;
                    }
                    else
                    {
                        cbIsIndividual.Checked = false;
                    }
                    lnkDelete.Visible = true;

                }



            }
            else if (e.CommandName == "View")
            {
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                //ResetFie();
                pnlInput.Visible = false;
                pnlView.Visible = true;

                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow gvr = gvResult.Rows[index];
                Int64 DeptID = Convert.ToInt64(gvResult.DataKeys[index]["PersonalID"]);



                DepartmentPersonDML dml = new DepartmentPersonDML();
                DataTable dtdept = dml.GetDepartmentPerson(DeptID);
                if (dtdept.Rows.Count > 0)
                {
                    txtCodeModal.Text = dtdept.Rows[0]["Code"].ToString();
                    txtNameModal.Text = dtdept.Rows[0]["Name"].ToString();
                    txtEmailModal.Text = dtdept.Rows[0]["Email"].ToString();
                    lblbusines.Text = dtdept.Rows[0]["BusinessEmail"].ToString();
                    lblDesignation.Text = dtdept.Rows[0]["Designation"].ToString();
                    lblCell.Text = dtdept.Rows[0]["Cell"].ToString();
                    lblLandline.Text = dtdept.Rows[0]["PhoneNo"].ToString();
                    lblOther.Text = dtdept.Rows[0]["OtherContact"].ToString();
                    lblAddressOffice.Text = dtdept.Rows[0]["AddressOffice"].ToString();
                    lblAddressOther.Text = dtdept.Rows[0]["AddressOther"].ToString();
                    lblDescription.Text = dtdept.Rows[0]["Description"].ToString();


                    lblCompany.Text = dtdept.Rows[0]["CompanyName"].ToString();


                    lblGroup.Text = dtdept.Rows[0]["Groups"].ToString();


                    lblDepartment.Text = dtdept.Rows[0]["Department"].ToString();
                }
            }
            else if (e.CommandName == "Active")
            {

                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow gvr = gvResult.Rows[index];
                Int64 CatId = Convert.ToInt64(gvResult.DataKeys[index]["PersonalID"]);
                string Active = gvResult.DataKeys[index]["Active"].ToString() == string.Empty ? "False" : gvResult.DataKeys[index]["Active"].ToString();
                DepartmentPersonDML dml = new DepartmentPersonDML();

                hfEditID.Value = CatId.ToString();

                if (Active == "True")
                {
                    dml.DeactivatePerson(CatId, LoginID);
                }
                else
                {
                    dml.ActivatePerson(CatId, LoginID);
                }
                GetDepartmentPerson();
                clearfield();

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
            DepartmentPersonDML dm = new DepartmentPersonDML();
            DataTable dtdept = dm.GetDepartmentPerson(keyword);
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
                        DepartmentPersonDML dml = new DepartmentPersonDML();
                        dml.DeleteDepartmentPerson(id);
                        clearfield();
                        lnkDelete.Visible = false;
                        notification("Success", "Deleted Successfully");
                        GetDepartmentPerson();
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
                        string business = txtBusiness.Text;
                        Int64 Group = Convert.ToInt64(ddlGroup.SelectedValue);
                        Int64 Company = Convert.ToInt64(ddlcompany.SelectedValue);
                        Int64 Department = Convert.ToInt64(ddlDepartment.SelectedValue);
                        string designation = txtDesignation.Text;
                        Int64 CellNo = txtCellNo.Text == string.Empty ? 0 : Convert.ToInt64(txtCellNo.Text);
                        Int64 Landline = txtLandline.Text == string.Empty ? 0 : Convert.ToInt64(txtLandline.Text);
                        Int64 OtherContact = txtOtherContact.Text == string.Empty ? 0 : Convert.ToInt64(txtOtherContact.Text);
                        string addressoffice = txtAddOffice.Text;
                        string addressother = txtAddOther.Text;
                        string description = Description.Text;
                        int individual = cbIsIndividual.Checked == true ? 1 : 0;

                        DepartmentPersonDML dm = new DepartmentPersonDML();
                        dm.InsertDepartmentPerson(Code, Name, Email, business, Group, Company, Department, designation, CellNo, Landline, OtherContact, addressoffice, addressother, description, individual, LoginID);
                        pnlInput.Visible = false;
                        notification("Success", "Submited Successfully");
                        GetDepartmentPerson();
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
                        string business = txtBusiness.Text;
                        Int64 Group = Convert.ToInt64(ddlGroup.SelectedValue);
                        Int64 Company = Convert.ToInt64(ddlcompany.SelectedValue);
                        Int64 Department = Convert.ToInt64(ddlDepartment.SelectedValue);
                        string designation = txtDesignation.Text;
                        Int64 CellNo = txtCellNo.Text == string.Empty ? 0 : Convert.ToInt64(txtCellNo.Text);
                        Int64 Landline = txtLandline.Text == string.Empty ? 0 : Convert.ToInt64(txtLandline.Text);
                        Int64 OtherContact = txtOtherContact.Text == string.Empty ? 0 : Convert.ToInt64(txtOtherContact.Text);
                        string addressoffice = txtAddOffice.Text;
                        string addressother = txtAddOther.Text;
                        string description = Description.Text;
                        int individual = cbIsIndividual.Checked == true ? 1 : 0;

                        DepartmentPersonDML dm = new DepartmentPersonDML();
                        Int64 id = Convert.ToInt64(hfEditID.Value);
                        dm.UpdateDepartmentPerson(id, Code, Name, Email, business, Group, Company, Department, designation, CellNo, Landline, OtherContact, addressoffice, addressother, description, individual, LoginID);
                        pnlInput.Visible = false;
                        notification("Success", "Updated Successfully");
                        GetDepartmentPerson();
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
    }
}