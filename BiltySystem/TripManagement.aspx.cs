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
    public partial class TripManagement : System.Web.UI.Page
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
            Expensesnotification();
            if (!IsPostBack)
            {
                if (LoginID > 0)
                {
                    this.Title = "Trip Management";
                    GetCompany();
                    GetExpenses();
                    GetVehicle();
                    GetTrip();
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
        public void Expensesnotification()
        {
            try
            {
                divExpensesNotification.InnerHtml = string.Empty;
            }
            catch (Exception ex)
            {
                divExpensesNotification.InnerHtml = "<div class=\"alert alert-danger\">" + ex.Message + "</div>";
            }
        }

        public void Expensesnotification(string type, string msg)
        {
            try
            {
                if (type == "Error")
                {
                    divExpensesNotification.InnerHtml = "<div class=\"alert alert-danger\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                }
                else if (type == "Success")
                {
                    divExpensesNotification.InnerHtml = "<div class=\"alert alert-success\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                }
                else if (type == string.Empty)
                {
                    divExpensesNotification.InnerHtml = string.Empty;
                }
            }
            catch (Exception ex)
            {
                divExpensesNotification.InnerHtml = "<div class=\"alert alert-danger\">" + ex.Message + "</div>";
            }
        }

        public void GetTrip()
        {
            try
            {
                TripDML dml = new TripDML();
                DataTable dt = dml.GetTrip();
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
                notification("Error", "Error getting Trip, due to: " + ex.Message);
            }
        }

        public void ClearFields()
        {
            try
            {
                hfEditID.Value = string.Empty;
                ddlCustomer.ClearSelection();
                txtTripStart.Text = string.Empty;
                txtTripEnd.Text = string.Empty;
                txtPick.Text = string.Empty;
                txtDrop.Text = string.Empty;
                ddlVehicle.ClearSelection();
                txtFreight.Text = string.Empty;
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
        public void GetCompany()
        {
            try
            {
                CompanyDML dml = new CompanyDML();
                DataTable dt = dml.GetCompany();
                if (dt.Rows.Count > 0)
                {
                    FillDropDown(dt, ddlCustomer, "CompanyID", "CompanyName", "-Select Customer-");
                    FillDropDown(dt, ddlCustomerSearch, "CompanyID", "CompanyName", "-Select Customer-");

                }
                else
                {

                    notification("Error", "No Customer found, please add Group first.");
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting/populating Customer, due to: " + ex.Message);
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
                    FillDropDown(dt, ddlExpensesType, "ExpensesTypeID", "ExpensesTypeName", "-Select Expenses-");
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

        public void ClearExpensesFields()
        {
            try
            {
                ddlExpensesType.ClearSelection();
                txtAmount.Text = string.Empty;
                txtRemarks.Text = string.Empty;
            }
            catch (Exception ex)
            {
                Expensesnotification("Error", "Error clearing Expenses, due to: " + ex.Message);
            }
            finally
            {
                modalExpenses.Show();
            }

        }
        public void GetExpense(Int64 TripID)
        {
            try
            {
                TripDML dml = new TripDML();
                DataTable dt = dml.GetExpenses(TripID);
                if (dt.Rows.Count > 0)
                {
                    Int64 Total = 0;
                    foreach (DataRow _drExpenses in dt.Rows)
                    {
                        Total += Convert.ToInt64(_drExpenses["Amount"]);
                    }
                    gvExpenses.DataSource = dt;
                    lblTotalExpenses.Text = Total.ToString();
                }
                else
                {
                    gvExpenses.DataSource = null;
                }
                gvExpenses.DataBind();
            }
            catch (Exception ex)
            {
                Expensesnotification("Error", "Error getting Expenses, due to: " + ex.Message);
            }
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
                if (ddlCustomer.SelectedIndex == 0)
                {
                    notification("Error", "Please Select Customer");
                    ddlCustomer.Focus();
                }
               else if (txtTripStart.Text == string.Empty)
                {
                    notification("Error", "Please enter Trip Start");
                    txtTripStart.Focus();
                }
                else if (txtTripEnd.Text == string.Empty)
                {
                    notification("Error", "Please enter Trip End");
                    txtTripEnd.Focus();
                }
                else if (txtPick.Text == string.Empty)
                {
                    notification("Error", "Please enter Pick");
                    txtTripEnd.Focus();
                }
                else if (txtDrop.Text == string.Empty)
                {
                    notification("Error", "Please enter Drop");
                    txtDrop.Focus();
                }
                else if (ddlVehicle.SelectedIndex == 0)
                {
                    notification("Error", "Please Select Vehicle Reg#");
                    ddlVehicle.Focus();
                }
                else if (txtFreight.Text == string.Empty)
                {
                    notification("Error", "Please enter Freight");
                    txtFreight.Focus();
                }

                else
                {
                    TripDML dml = new TripDML();
                    DataTable dt = dml.GetTrip();


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
        //        GetTrip();
        //    }
        //    catch (Exception ex)
        //    {

        //        notification("Error", ex.Message);
        //    }
        //}

        //protected void lnkSearch_Click(object sender, EventArgs e)
        //{
        //    string keyword = txtSearch.Text.Trim();
        //    TripDML dm = new TripDML();
        //    DataTable dt = dm.GetTrip(keyword);
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
                        TripDML dml = new TripDML();
                        dml.DeleteTrip(ID);

                        ClearFields();
                        pnlInput.Visible = false;
                        GetTrip();

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
                        Int64 CompanyID = ddlCustomer.SelectedIndex == 0 ? 0 : Convert.ToInt64(ddlCustomer.SelectedValue);
                        string TripStart = txtTripStart.Text.Trim();
                        string TripEnd = txtTripEnd.Text.Trim();
                        string Pickup = txtPick.Text.Trim();
                        string Dropoff = txtDrop.Text.Trim();
                        Int64 VehicleRegNo = ddlVehicle.SelectedIndex == 0 ? 0 : Convert.ToInt64(ddlVehicle.SelectedValue);
                        Int64 Freight = txtFreight.Text == string.Empty ? 0 : Convert.ToInt64(txtFreight.Text.Trim());
                        TripDML dml = new TripDML();
                        dml.InsertTrip(CompanyID, TripStart, TripEnd, Pickup, Dropoff, VehicleRegNo, Freight, LoginID);
                        pnlInput.Visible = false;
                        notification("Success", "Submited Successfully");
                        ClearFields();
                        GetTrip();
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
                        Int64 CompanyID = ddlCustomer.SelectedIndex == 0 ? 0 : Convert.ToInt64(ddlCustomer.SelectedValue);
                        string TripStart = txtTripStart.Text.Trim();
                        string TripEnd = txtTripEnd.Text.Trim();
                        string Pickup = txtPick.Text.Trim();
                        string Dropoff = txtDrop.Text.Trim();
                        Int64 VehicleRegNo = ddlVehicle.SelectedIndex == 0 ? 0 : Convert.ToInt64(ddlVehicle.SelectedValue);
                        Int64 Freight = txtFreight.Text == string.Empty ? 0 : Convert.ToInt64(txtFreight.Text.Trim());
                        Int64 ID = Convert.ToInt64(hfEditID.Value);
                        TripDML dml = new TripDML();
                        dml.UpdateTrip(ID, CompanyID, TripStart, TripEnd, Pickup, Dropoff, VehicleRegNo, Freight, LoginID);
                        pnlInput.Visible = false;
                        notification("Success", "Updated Successfully");
                        ClearFields();
                        GetTrip();
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

                TripDML dml = new TripDML();
                DataTable dt = dml.GetTrip(ID);
                if (dt.Rows.Count > 0)
                {
                    ddlCustomer.ClearSelection();
                    ddlCustomer.Items.FindByText(dt.Rows[0]["CompanyName"].ToString()).Selected = true;

                    txtTripStart.Text = dt.Rows[0]["TripStart"].ToString();
                    txtTripEnd.Text = dt.Rows[0]["TripEnd"].ToString();
                    txtPick.Text = dt.Rows[0]["Pickup"].ToString();
                    txtDrop.Text = dt.Rows[0]["Dropoff"].ToString();
                    ddlVehicle.ClearSelection();
                    ddlVehicle.Items.FindByText(dt.Rows[0]["RegNo"].ToString()).Selected = true;
                    txtFreight.Text = dt.Rows[0]["Freight"].ToString();
                    
                    lnkDelete.Visible = true;
                    GetTrip();
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

                TripDML dml = new TripDML();
                DataTable dt = dml.GetTrip(ID);
                if (dt.Rows.Count > 0)
                {
                    lblCustomer.Text = dt.Rows[0]["CompanyName"].ToString();
                    lblTripStart.Text = dt.Rows[0]["TripStart"].ToString();
                    lblTripEnd.Text = dt.Rows[0]["TripEnd"].ToString();
                    lblPick.Text = dt.Rows[0]["Pickup"].ToString();
                    lblDrop.Text = dt.Rows[0]["Dropoff"].ToString();
                    lblVehicle.Text = dt.Rows[0]["VehicleRegNo"].ToString();
                    lblFreight.Text = dt.Rows[0]["Freight"].ToString();

                }
            }
            else if(e.CommandName == "TripExpenses")
            {
                modalExpenses.Show();
                int index = Convert.ToInt32(e.CommandArgument);
                
               // GridViewRow gvr = gvExpenses.Rows[index];
                Int64 TripID = Convert.ToInt64(gvResult.DataKeys[index]["ID"]);
                hfTripID.Value = TripID.ToString();
                //lnkAddExpenses.Enabled = true;
                //lnkAddExpenses.ToolTip = "Click to Add new Expenses";
                GetExpense(TripID);
                


            }
            else if (e.CommandName == "IsActive")
            {

                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow gvr = gvResult.Rows[index];
                Int64 ID = Convert.ToInt64(gvResult.DataKeys[index]["ID"]);
                string IsActive = gvResult.DataKeys[index]["IsActive"].ToString() == string.Empty ? "False" : gvResult.DataKeys[index]["IsActive"].ToString();
                TripDML dml = new TripDML();

                hfEditID.Value = ID.ToString();

                if (IsActive == "True")
                {
                    dml.DeactivateTrip(ID, LoginID);
                }
                else
                {
                    dml.ActivateTrip(ID, LoginID);
                }
                GetTrip();
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

                        string[] StartDate = e.Row.Cells[GetColumnIndexByName(e.Row, "TripStart")].Text.Split(' ');
                    e.Row.Cells[GetColumnIndexByName(e.Row, "TripStart")].Text = StartDate[0].ToString();


                    string[] EndDate = e.Row.Cells[GetColumnIndexByName(e.Row, "TripEnd")].Text.Split(' ');
                    e.Row.Cells[GetColumnIndexByName(e.Row, "TripEnd")].Text = EndDate[0].ToString();
                }
                }
                catch (Exception ex)
                {
                    notification("Error", "Binding data to grid, due to: " + ex.Message);
                }
        }

        #endregion

        #region Modal Expenses
        protected void lnkCloseExpenses_Click(object sender, EventArgs e)
        {
            try
            {
                modalExpenses.Hide();
                //pnlExpensesInput.Visible = false;
                ClearExpensesFields();
                //hfSelectedOrder.Value = string.Empty;

                modalExpenses.Hide();
            }
            catch (Exception ex)
            {
                Expensesnotification("Error", "Error closing Advances panel, due to: " + ex.Message);
                modalExpenses.Show();
            }
        }

        protected void lnkCancel_Click(object sender, EventArgs e)
        {
            try
            {
                ClearExpensesFields();
                //pnlExpensesInput.Visible = false;
            }
            catch (Exception ex)
            {
                Expensesnotification("Error", "Error cancelling Advance input panel, due to: " + ex.Message);
            }
            finally
            {
                modalExpenses.Show();
            }
        }

        protected void lnkSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlExpensesType.SelectedIndex == 0)
                {
                    Expensesnotification("Error", "Please Select Expenses");
                    ddlExpensesType.Focus();
                }
                else if (txtAmount.Text == string.Empty)
                {
                    Expensesnotification("Error", "Please Select Amount");
                    txtAmount.Focus();
                }
                else
                {
                    Int64 TripId = Convert.ToInt64(hfTripID.Value);
                    Int64 ExpensesTypeID = ddlExpensesType.SelectedIndex == 0 ? 0 : Convert.ToInt64(ddlExpensesType.SelectedValue);
                    Int64 Amount = Convert.ToInt64(txtAmount.Text.Trim());
                    string Remarks = txtRemarks.Text.Trim();
                    TripDML dml = new TripDML();

                    dml.InsertExpenses(TripId, ExpensesTypeID, Amount, Remarks, LoginID);
                    Expensesnotification("Success", "Submited Successfully");
                    GetExpense(TripId);
                    ClearExpensesFields();
                   // pnlExpensesInput.Visible = false;
                    GetTrip();


                }

            }
            catch (Exception ex)
            {

                Expensesnotification("Error", ex.Message);
            }
            finally
            {
                modalExpenses.Show();

            }
        }


        //protected void lnkAddExpenses_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        pnlExpensesInput.Visible = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Expensesnotification("Error", "Error enabling add new advances input, due to: " + ex.Message);
        //    }
        //    finally
        //    {
        //        modalExpenses.Show();
        //    }

        //}
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

                throw;
            }

        }

        protected void lnkTripSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtStartSearch.Text == string.Empty)
                {
                    notification("Error", "Please enter Start Date");
                    txtStartSearch.Focus();
                }
                else if (txtEndSearch.Text == string.Empty)
                {
                    notification("Error", "Please enter End Date");
                    txtEndSearch.Focus();
                }

                string TripStart = txtStartSearch.Text.Trim();
                string TripEnd = txtEndSearch.Text.Trim();
                string RegNo = ddlVehicleSearch.SelectedItem.Text;
                string Customer = ddlCustomerSearch.SelectedItem.Text;
                string Pickup = txtPickSearch.Text.Trim();
                string Dropoff = txtDropSearch.Text.Trim();
                TripDML dml = new TripDML();

                string QueryConditions = string.Empty;

                if (ddlVehicleSearch.SelectedIndex != 0)
                {
                    QueryConditions += " AND v.RegNo = '" + RegNo + "'";
                }
                else if (ddlCustomerSearch.SelectedIndex != 0)
                {
                    QueryConditions += " AND c.CompanyName = '" + Customer + "'";
                }
                else if (txtPickSearch.Text != string.Empty)
                {
                    QueryConditions += " AND t.Pickup like '%" + Pickup + "%'";
                }
                else if (txtDropSearch.Text != string.Empty)
                {
                    QueryConditions += " AND t.Dropoff like '%" + Dropoff + "%'";
                }

                //if(ddlVehicleSearch.SelectedIndex != 0 || ddlCustomerSearch.SelectedIndex != 0 || txtPickSearch.Text != string.Empty || txtDropSearch.Text != string.Empty)
                //{
                //    QueryConditions = QueryConditions.Remove(QueryConditions.Length - 5);
                //}


                DataTable dt = dml.SearchTrip(TripStart, TripEnd, QueryConditions);
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

                throw;
            }

        }
        #endregion

        #endregion


    }
}