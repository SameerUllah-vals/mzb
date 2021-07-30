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
    public partial class Voucher : System.Web.UI.Page
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

        #region HelpMethods
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
            notification();

            if (!IsPostBack)
            {
                if (LoginID > 0)
                {
                    this.Title = "Voucher";

                    GetExpenses();
                    GetVehicle();
                    GetVehicleExpenses();
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


        public void GetVehicleExpenses()
        {
            try
            {
                VoucherDML dml = new VoucherDML();
                DataTable dt = dml.GetVehicleExpenses();
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
            catch (Exception ex)
            {
                notification("Error", "Error getting Vehicle Expenses, due to: " + ex.Message);
            }
        }

        public void ClearFields()
        {
            try
            {
                hfEditID.Value = string.Empty;
                ddlExpense.ClearSelection();
                ddlVehicle.ClearSelection();
                //lblVoucherNo.Text = string.Empty;
                txtVendor.Text = string.Empty;
                txtAmount.Text = string.Empty;
                txtRemarks.Text = string.Empty;
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
        public void GetVehicle()
        {
            try
            {
                VehicleDML dml = new VehicleDML();
                DataTable dt = dml.GetVehicle();
                if (dt.Rows.Count > 0)
                {
                    FillDropDown(dt, ddlVehicle, "VehicleID", "RegNo", "-Select Vehicle-");
                    FillDropDown(dt, ddlVehicleSearch, "VehicleID", "RegNo", "-Select Vehicle-");

                }
                else
                {

                    notification("Error", "");
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting/populating Vehicle, due to: " + ex.Message);
            }
        }

        public void GetExpenses()
        {
            try
            {
                ExpensesTypeDML dml = new ExpensesTypeDML();
                DataTable dt = dml.GetExpensesType();
                if (dt.Rows.Count > 0)
                {
                    FillDropDown(dt, ddlExpense, "ExpensesTypeID", "ExpensesTypeName", "-Select Expenses-");
                    FillDropDown(dt, ddlExpenseSearch, "ExpensesTypeID", "ExpensesTypeName", "-Select Expenses-");

                }
                else
                {

                    notification("Error", "No Expenses found, please add Expenses first.");
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting/populating Expenses, due to: " + ex.Message);
            }
        }
        public string GetAuto()
        {
            VoucherDML dml = new VoucherDML();
            DataTable dt = dml.GetAutoVoucher();
            string lastVoucher = dt.Rows[0]["Voucher"].ToString();
            return lastVoucher;

        }



        #endregion

        #region Event

        #region LinkButton
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

        protected void lnkSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDate.Text == string.Empty)
                {
                    notification("Error", "Please Select Date");
                    txtDate.Focus();
                }
                //else if (txtVoucherNo.Text == string.Empty)
                // {
                //     notification("Error", "Please enter Voucher");
                //     txtVoucherNo.Focus();
                // }
                else if (ddlVehicle.SelectedIndex == 0)
                {
                    notification("Error", "Please Select Vehicle");
                    ddlVehicle.Focus();
                }
                else if (ddlExpense.SelectedIndex == 0)
                {
                    notification("Error", "Please Select Expense");
                    ddlExpense.Focus();
                }
                else if (txtVendor.Text == string.Empty)
                {
                    notification("Error", "Please enter Vendor");
                    txtVendor.Focus();
                }

                else
                {
                    VoucherDML dml = new VoucherDML();
                    DataTable dt = dml.GetVehicleExpenses();

                    if (hfEditID.Value.ToString() == string.Empty)
                    {
                        //if (dt.Rows.Count > 0)
                        //{
                            ConfirmModal("Are you sure want to Save?", "Save");
                        //}

                    }
                    else
                    {
                        //if (dt.Rows.Count > 1)
                        //{
                            ConfirmModal("Are you sure want to Update?", "Update");
                        //}

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

        //protected void lnkCancelSearch_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        txtSearch.Text = string.Empty;
        //        GetVehicleExpenses();
        //    }
        //    catch (Exception ex)
        //    {

        //        notification("Error", ex.Message);
        //    }
        //}

        //protected void lnkSearch_Click(object sender, EventArgs e)
        //{
        //    string keyword = txtSearch.Text.Trim();
        //    VoucherDML dm = new VoucherDML();
        //    DataTable dt = dm.GetVehicleExpenses(keyword);
        //    if (dt.Rows.Count > 0)
        //    {
        //        gvResult.DataSource = dt;
        //    }
        //    else
        //    {
        //        gvResult.DataSource = null;
        //    }
        //    gvResult.DataBind();
        //}


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
                        VoucherDML dml = new VoucherDML();
                        dml.DeleteVehicleExpense(ID);

                        ClearFields();
                        pnlInput.Visible = false;
                        GetVehicleExpenses();

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
                        string Date = txtDate.Text.Trim();
                        string Voucher = "";
                        Int64 Vehicle = ddlVehicle.SelectedIndex == 0 ? 0 : Convert.ToInt64(ddlVehicle.SelectedValue);
                        Int64 Expenses = ddlExpense.SelectedIndex == 0 ? 0 : Convert.ToInt64(ddlExpense.SelectedValue);
                        string Vendor = txtVendor.Text.Trim();
                        double Amount = txtAmount.Text == string.Empty ? 0 : Convert.ToDouble(txtAmount.Text.Trim());
                        string Remarks = txtRemarks.Text.Trim();
                        VoucherDML dml = new VoucherDML();

                        dml.InsertVehicleExpense(Voucher, Vehicle, Expenses, Vendor, Date, Amount, Remarks, LoginID);
                        pnlInput.Visible = false;
                        notification("Success", "Submited Successfully");
                        ClearFields();
                        GetVehicleExpenses();
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
                        string Date = txtDate.Text.Trim();
                        string Voucher = "";
                        Int64 Vehicle = ddlVehicle.SelectedIndex == 0 ? 0 : Convert.ToInt64(ddlVehicle.SelectedValue);
                        Int64 Expenses = ddlExpense.SelectedIndex == 0 ? 0 : Convert.ToInt64(ddlExpense.SelectedValue);
                        string Vendor = txtVendor.Text.Trim();
                        double Amount = txtAmount.Text == string.Empty ? 0 : Convert.ToDouble(txtAmount.Text.Trim());
                        string Remarks = txtRemarks.Text.Trim();
                        Int64 ID = Convert.ToInt64(hfEditID.Value);
                        VoucherDML dml = new VoucherDML();

                        dml.UpdateVehicleExpense(ID, Voucher, Vehicle, Expenses, Vendor, Date, Amount, Remarks, LoginID);
                        pnlInput.Visible = false;
                        notification("Success", "Updated Successfully");
                        ClearFields();
                        GetVehicleExpenses();
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

        #region GridView
        protected void gvResult_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Change")
            {
                pnlInput.Visible = true;
                pnlView.Visible = false;
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow gvr = gvResult.Rows[index];
                Int64 ID = Convert.ToInt64(gvResult.DataKeys[index]["ID"]);
                hfEditID.Value = ID.ToString();

                VoucherDML dml = new VoucherDML();
                DataTable dt = dml.GetVehicleExpenses(ID);
                if (dt.Rows.Count > 0)
                {
                    txtDate.Text = dt.Rows[0]["Date"].ToString();
                    //lblVoucherNo.Text = dt.Rows[0]["VoucherNo"].ToString();
                    ddlVehicle.ClearSelection();
                    ddlVehicle.Items.FindByText(dt.Rows[0]["RegNo"].ToString()).Selected = true;
                    ddlExpense.ClearSelection();
                    ddlExpense.Items.FindByText(dt.Rows[0]["ExpensesTypeName"].ToString()).Selected = true;
                    txtVendor.Text = dt.Rows[0]["Vendor"].ToString();
                    txtAmount.Text = dt.Rows[0]["Amount"].ToString();
                    txtRemarks.Text = dt.Rows[0]["Remarks"].ToString();

                    lnkDelete.Visible = true;
                    GetVehicleExpenses();
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
                Int64 ID = Convert.ToInt64(gvResult.DataKeys[index]["ID"]);

                VoucherDML dml = new VoucherDML();
                DataTable dt = dml.GetVehicleExpenses(ID);
                if (dt.Rows.Count > 0)
                {
                    lblVoucher.Text = dt.Rows[0]["VoucherNo"].ToString();
                    lblVehicle.Text = dt.Rows[0]["RegNo"].ToString();
                    lblDate.Text = dt.Rows[0]["Date"].ToString();
                    lblVendor.Text = dt.Rows[0]["Vendor"].ToString();
                    lblAmount.Text = dt.Rows[0]["Amount"].ToString();
                    lblRemarks.Text = dt.Rows[0]["Remarks"].ToString();

                }
            }
            else if (e.CommandName == "IsActive")
            {

                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow gvr = gvResult.Rows[index];
                Int64 ID = Convert.ToInt64(gvResult.DataKeys[index]["ID"]);
                string IsActive = gvResult.DataKeys[index]["IsActive"].ToString() == string.Empty ? "False" : gvResult.DataKeys[index]["IsActive"].ToString();
                VoucherDML dml = new VoucherDML();
                hfEditID.Value = ID.ToString();

                if (IsActive == "True")
                {
                    dml.DeactivateVehicleExpense(ID, LoginID);
                }
                else
                {
                    dml.ActivateVehicleExpense(ID, LoginID);
                }
                GetVehicleExpenses();
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


                    string[] StartDate = e.Row.Cells[GetColumnIndexByName(e.Row, "Date")].Text.Split(' ');
                    e.Row.Cells[GetColumnIndexByName(e.Row, "Date")].Text = StartDate[0].ToString();

                    bool IsActive = Convert.ToBoolean(rowView["IsActive"].ToString() == string.Empty ? "false" : rowView["IsActive"]);
                    LinkButton lnkActive = e.Row.FindControl("lnkActive") as LinkButton;
                    if (IsActive == true)
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

        #endregion

        #region Search LinkButton
        protected void lnkCloseSearch_Click(object sender, EventArgs e)
        {
            try
            {
                pnlSearch.Visible = false;
            }
            catch (Exception ex)
            {

                notification("Error", ex.Message);
            }

        }

        protected void lnkVoucherSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtStartDate.Text == string.Empty)
                {
                    notification("Error", "Please enter Start Date");
                    txtStartDate.Focus();
                }
                else if (txtEndDate.Text == string.Empty)
                {
                    notification("Error", "Please enter End Date");
                    txtEndDate.Focus();
                }

                string StartDate = txtStartDate.Text.Trim();
                string EndDate = txtEndDate.Text.Trim();
                string RegNo = ddlVehicleSearch.SelectedItem.Text;
                string Expense = ddlExpenseSearch.SelectedItem.Text;
                string Vendor = txtVendor.Text.Trim();

                VoucherDML dml = new VoucherDML();

                string QueryConditions = string.Empty;

                if (ddlVehicleSearch.SelectedIndex != 0)
                {
                    QueryConditions += " AND v.RegNo = '" + RegNo + "'";
                }
                else if (ddlExpenseSearch.SelectedIndex != 0)
                {
                    QueryConditions += " AND et.ExpensesTypeName = '" + Expense + "'";
                }
                else if (txtVendorSearch.Text != string.Empty)
                {
                    QueryConditions += " AND tve.Vendor like '%" + Vendor + "%'";
                }


                //if(ddlVehicleSearch.SelectedIndex != 0 || ddlCustomerSearch.SelectedIndex != 0 || txtPickSearch.Text != string.Empty || txtDropSearch.Text != string.Empty)
                //{
                //    QueryConditions = QueryConditions.Remove(QueryConditions.Length - 5);
                //}


                DataTable dt = dml.SearchVoucher(StartDate, EndDate, QueryConditions);
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
            catch (Exception ex)
            {

                throw;
            }
        }

        protected void lnkSearchNew_Click(object sender, EventArgs e)
        {
            try
            {
                pnlSearch.Visible = true;
            }
            catch (Exception ex)
            {

                notification("Error", ex.Message);
            }

        }
        #endregion

        #endregion


    }
}