using BLL;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiltySystem.Reports
{
    public partial class Drivers : System.Web.UI.Page
    {
        Random rnd = new Random();

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

                //this.Title = "Search Bilties";
                GetReports("DESC");
                DataTable dtresultcolumns = new DataTable();
                Session.Add("dtActiveColumns", dtresultcolumns);


                //Getting/Binding Active Brokers
                try
                {
                    BrokersDML dml = new BrokersDML();
                    DataTable dtBrokers = dml.GetActiveBroker();
                    if (dtBrokers.Rows.Count > 0)
                    {
                        FillDropDown(dtBrokers, ddlSearchBroker, "ID", "Name", "-Select-");
                    }
                }
                catch (Exception ex)
                {
                    notification("Error", "Error getting/binding Brokers, due to: " + ex.Message);
                }
            }
        }

        public void GetReport()
        {
            try
            {
                DataTable data = (DataTable)Session["dtData"];
                //DataTable dtReport = data.Clone();
                DataTable dtReport = new DataTable();

                dtReport.Columns.Add("BorkerID");
                dtReport.Columns.Add("OrderID");
                dtReport.Columns.Add("Broker");
                dtReport.Columns.Add("BookingDate");
                dtReport.Columns.Add("VehicleRegNo");
                dtReport.Columns.Add("Rate");
                dtReport.Columns.Add("EmptyContainerDropLocation");
                dtReport.Columns.Add("TotalAdvances");
                dtReport.Columns.Add("TotalExpenses");
                dtReport.Columns.Add("AdvanceFreight");
                dtReport.Columns.Add("FactoryAdvance");
                dtReport.Columns.Add("LiftOffLiftOn");
                dtReport.Columns.Add("WeighmentCharges");
                dtReport.Columns.Add("Remaining");
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    string BrokerID = data.Rows[i]["BrokerID"].ToString();
                    string OrderID = data.Rows[i]["OrderID"].ToString();
                    string Broker = data.Rows[i]["Broker"].ToString();
                    string BookingDate = data.Rows[i]["BookingDate"].ToString();
                    string VehicleRegNo = data.Rows[i]["VehicleRegNo"].ToString();
                    double Rate = Convert.ToDouble(data.Rows[i]["Rate"]);
                    string EmptyContainerDropLocation = data.Rows[i]["EmptyContainerDropLocation"].ToString();
                    double TotalAdvances = Convert.ToDouble(data.Rows[i]["TotalAdvances"]);
                    double TotalExpenses = Convert.ToDouble(data.Rows[i]["TotalExpenses"]);
                    string AdvanceFreight = "0";
                    string FactoryAdvance = "0";
                    string LiftOffLiftOn = "0";
                    string WeighmentCharges = "0";
                    if (data.Columns.Contains("AdvanceFreight"))
                    {
                        AdvanceFreight = data.Rows[i]["AdvanceFreight"].ToString() == string.Empty ? "0" : data.Rows[i]["AdvanceFreight"].ToString();
                    }
                    if (data.Columns.Contains("FactoryAdvance"))
                    {
                        FactoryAdvance = data.Rows[i]["FactoryAdvance"].ToString() == string.Empty ? "0" : data.Rows[i]["FactoryAdvance"].ToString();
                    }
                    if (data.Columns.Contains("LiftOffLiftOn"))
                    {
                        LiftOffLiftOn = data.Rows[i]["LiftOffLiftOn"].ToString() == string.Empty ? "0" : data.Rows[i]["LiftOffLiftOn"].ToString();
                    }
                    if (data.Columns.Contains("WeighmentCharges"))
                    {
                        WeighmentCharges = data.Rows[i]["WeighmentCharges"].ToString() == string.Empty ? "0" : data.Rows[i]["WeighmentCharges"].ToString();
                    }
                    double Remaining = ((Rate + TotalExpenses) - TotalAdvances);
                    dtReport.Rows.Add(BrokerID, OrderID, Broker, BookingDate, VehicleRegNo, Rate, EmptyContainerDropLocation, TotalAdvances, TotalExpenses, AdvanceFreight, FactoryAdvance, LiftOffLiftOn, WeighmentCharges, Remaining);

                }



                rvDrivers.LocalReport.DataSources.Add(new ReportDataSource("VendorReportDataSet", dtReport));
                rvDrivers.LocalReport.ReportPath = Server.MapPath("~/Reports/VendorReport.rdlc");
                rvDrivers.LocalReport.EnableHyperlinks = true;

                pnlGrid.Visible = false;
                pnlReport.Visible = true;

                lnkGenerateReport.Visible = false;
                lnkCloseReport.Visible = true;
            }
            catch (Exception ex)
            {

                notification("Error", "Error Occured Due To : " + ex.Message);
            }
        }

        #endregion

        #region Custom Methods

        #region Notifications

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

        #endregion

        public void ConfirmModal(string Title, string Action)
        {
            try
            {
                //modalConfirm.Show();
                //lblModalTitle.Text = Title;
                //hfConfirmAction.Value = Action;
                //lblConfirmAction.Text = Action;

            }
            catch (Exception ex)
            {
                notification("Error", "Error confirming, due to: " + ex.Message);
            }
        }

        public void GetReports(string SortState)
        {
            try
            {
                ReportsDML dml = new ReportsDML();
                DataTable dtReports = dml.GetBookedVehiclesByBrokers(SortState);
                DataTable dtExtended = new DataTable();

                //foreach (DataColumn _dcReports in dtReports.Columns)
                //{
                //    if (_dcReports.ColumnName == "BookingDate")
                //    {
                //        dtExtended.Columns.Add(_dcReports.ColumnName, typeof(DateTime));
                //    }
                //    else
                //    {
                //        dtExtended.Columns.Add(_dcReports.ColumnName);
                //    }
                //}

                //foreach (DataRow _drReports in dtReports.Rows)
                //{
                //    //_drReports.
                //    dtExtended.Rows.Add();

                //    for (int i = 0; i < _drReports.ItemArray.Length; i++)
                //    {
                //        dtExtended.Rows[dtExtended.Rows.Count - 1][i] = dtReports.Rows[dtReports.Rows.Count - 1][i];
                //    }
                //}

                //DataTable dtExtended = dtReports.Copy();
                //dtExtended = dtReports.Copy();
                for (int i = 0; i < dtExtended.Rows.Count; i++)
                {
                    for (int j = 0; j < dtExtended.Columns.Count; j++)
                    {
                        if (dtExtended.Columns[j].ColumnName == "OrderID")
                        {
                            Int64 OrderID = Convert.ToInt64(dtExtended.Rows[i][j]);
                            DataTable dtAdvances = dml.GetOrderAdvances(OrderID);
                            if (dtAdvances.Rows.Count > 0)
                            {
                                foreach (DataRow _drAdvances in dtAdvances.Rows)
                                {
                                    string AdvanceAgainst = _drAdvances["AdvanceAgainst"].ToString();
                                    bool isExtraColumnExist = false;
                                    foreach (DataColumn _drCols in dtExtended.Columns)
                                    {
                                        if (_drCols.ColumnName == AdvanceAgainst)
                                        {
                                            isExtraColumnExist = true;
                                        }
                                    }

                                    if (isExtraColumnExist == false)
                                    {
                                        dtExtended.Columns.Add(AdvanceAgainst, typeof(double));
                                    }
                                    dtExtended.Rows[i][AdvanceAgainst] = _drAdvances["AdvanceAmount"].ToString();
                                }
                            }
                        }
                    }
                    //if (dtExtended.Rows.Column)
                    //{

                    //}
                }

                gvResult.DataSource = dtExtended.Rows.Count > 0 ? dtExtended : null;
                gvResult.DataBind();

            }
            catch (Exception ex)
            {
                notification("Error", "Error getting reports, due to: " + ex.Message);
            }
        }

        public void SearchReports()
        {
            try
            {
                string DateFrom = txtSearchDateFrom.Text;
                string DateTo = txtSearchDateTo.Text;
                string Broker = ddlSearchBroker.SelectedIndex == 0 ? string.Empty : ddlSearchBroker.SelectedItem.Text;
                string BrokerID = ddlSearchBroker.SelectedIndex == 0 ? string.Empty : ddlSearchBroker.SelectedItem.Value;
                string VehicleRegNo = txtSearchVehicleRegNo.Text;
                string BiltyNo = txtSearchBiltyNo.Text;

                string Query = "WHERE ";

                Query += "";
                if (txtSearchDateFrom.Text != string.Empty || txtSearchDateTo.Text != string.Empty)
                {
                    if (txtSearchDateFrom.Text != string.Empty && txtSearchDateTo.Text == string.Empty)
                    {
                        Query += "Convert(date, o.RecordedDate) = CONVERT(date, ''" + DateFrom + "'') AND ";
                    }
                    else if (txtSearchDateFrom.Text == string.Empty && txtSearchDateTo.Text != string.Empty)
                    {
                        Query += "Convert(date, o.RecordedDate) = CONVERT(date, ''" + DateTo + "'') AND ";
                    }
                    else if (txtSearchDateFrom.Text != string.Empty && txtSearchDateTo.Text != string.Empty)
                    {
                        Query += "Convert(date, o.RecordedDate) BETWEEN CONVERT(date, ''" + DateFrom + "'') AND CONVERT(date, ''" + DateTo + "'') AND ";
                    }
                }

                if (ddlSearchBroker.SelectedIndex != 0)
                {
                    Query += "b.Name = ''" + Broker + "'' AND ";
                }

                if (txtSearchVehicleRegNo.Text != string.Empty)
                {
                    Query += "ov.VehicleRegNo = ''" + VehicleRegNo + "'' AND ";
                }

                if (txtSearchBiltyNo.Text != string.Empty)
                {
                    Query += "o.OrderNo = ''" + BiltyNo + "'' AND ";
                }

                Query = Query.Remove(Query.Length - 5);

                ReportsDML dml = new ReportsDML();
                DataTable dtReports = dml.SearchBookedVehiclesByBrokers(Query, BrokerID);


                DataTable dtresultcolumns = (DataTable)Session["dtActiveColumns"];


                dtresultcolumns.Rows.Add();

                DataTable dtExtended = dtReports.Copy();
                for (int j = 0; j < dtExtended.Columns.Count; j++)
                {
                    for (int i = 0; i < dtExtended.Rows.Count; i++)
                    {

                        if (dtresultcolumns.Columns.Contains(dtExtended.Columns[j].ColumnName.Replace(" ", string.Empty)) == false)
                        {

                            dtresultcolumns.Columns.Add(dtExtended.Columns[j].ColumnName.Replace(" ", string.Empty));
                        }

                        //dtresultcolumns.Rows[0][gvResult.Columns[_gvc.RowIndex].HeaderText] = e.Row.Cells[_gvc.RowIndex].Text != string.Empty ? "1" : string.Empty;
                        //gvResult.Columns[3].HeaderText
                        if (dtExtended.Rows[i][j].ToString() != string.Empty)
                        {
                            dtresultcolumns.Rows[0][j] = "1";
                        }




                        //if (dtExtended.Columns[j].ColumnName == "OrderID")
                        //{
                        //    Int64 OrderID = Convert.ToInt64(dtExtended.Rows[i][j]);
                        //    DataTable dtAdvances = dml.GetOrderAdvances(OrderID);
                        //    if (dtAdvances.Rows.Count > 0)
                        //    {
                        //        foreach (DataRow _drAdvances in dtAdvances.Rows)
                        //        {
                        //            string AdvanceAgainst = _drAdvances["AdvanceAgainst"].ToString();
                        //            bool isExtraColumnExist = false;
                        //            foreach (DataColumn _drCols in dtExtended.Columns)
                        //            {
                        //                if (_drCols.ColumnName == AdvanceAgainst)
                        //                {
                        //                    isExtraColumnExist = true;
                        //                }
                        //            }

                        //            if (isExtraColumnExist == false)
                        //            {
                        //                dtExtended.Columns.Add(AdvanceAgainst, typeof(double));
                        //            }
                        //            dtExtended.Rows[i][AdvanceAgainst] = _drAdvances["AdvanceAmount"].ToString();
                        //        }
                        //    }
                        //}



                    }
                    //if (dtExtended.Rows.Column)
                    //{

                    //}
                    dtExtended.Columns[j].ColumnName = dtExtended.Columns[j].ColumnName.Replace(" ", string.Empty);
                }

                //foreach (DataRow _dc in dtExtended.Columns)
                //{
                //    dtresultcolumns.Columns.Add(gvResult.Columns[_dc.RowIndex].HeaderText);

                //    //dtresultcolumns.Rows[0][gvResult.Columns[_gvc.RowIndex].HeaderText] = e.Row.Cells[_gvc.RowIndex].Text != string.Empty ? "1" : string.Empty;
                //    //gvResult.Columns[3].HeaderText
                //    if (e.Row.Cells[_dc.RowIndex].Text != string.Empty)
                //    {
                //        dtresultcolumns.Rows[0][gvResult.Columns[_gvc.RowIndex].HeaderText] = "1";
                //    }
                //}



                foreach (DataColumn _dcresultcolumns in dtresultcolumns.Columns)
                {
                    if (dtresultcolumns.Rows[0][_dcresultcolumns.ColumnName].ToString() != "1")
                    {
                        dtExtended.Columns.Remove(_dcresultcolumns.ColumnName);
                    }
                    //dtExtended.ColumnName = _dcresultcolumns.ColumnName.Replace(" ", string.Empty);
                }
                Session.Add("dtData", dtExtended);
                gvResult.DataSource = dtExtended.Rows.Count > 0 ? dtExtended : null;
                gvResult.DataBind();


            }
            catch (Exception ex)
            {
                notification("Error", "Error getting reports, due to: " + ex.Message);
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

        private void FillCheckBox(DataTable dt, CheckBoxList _cbl, string _ddlValue, string _ddlText)
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

        #region Events

        #region Gridview

        protected void gvResult_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gvResult_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    BoundField field = (BoundField)((DataControlFieldCell)e.Row.Cells[2]).ContainingField;
                    if (field.HeaderText == "Date")
                    {
                        string[] date = e.Row.Cells[2].Text.Split(' ');
                        e.Row.Cells[2].Text = date[0].ToString();
                    }
                    TableCell tbl = new TableCell();

                    tbl.Text = "Turk";
                    //tbl.head
                    e.Row.Cells.Add(tbl);


                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error binding result, due to: " + ex.Message);
            }
        }

        #endregion

        #region LinkButton

        protected void lnkSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSearchDateFrom.Text == string.Empty && txtSearchDateTo.Text == string.Empty && ddlSearchBroker.SelectedIndex == 0 && txtSearchVehicleRegNo.Text == string.Empty && txtSearchBiltyNo.Text == string.Empty)
                {
                    notification("Error", "Please provide any search filter");
                    txtSearchDateFrom.Focus();
                }
                else if (txtSearchDateFrom.Text.Trim() == string.Empty)
                {
                    notification("Error", "Please select from date");
                    txtSearchDateFrom.Focus();
                }
                else if (txtSearchDateTo.Text.Trim() == string.Empty)
                {
                    notification("Error", "Please select to date");
                    txtSearchDateTo.Focus();
                }
                else if (ddlSearchBroker.SelectedIndex == 0)
                {
                    notification("Error", "Please select broker");
                    ddlSearchBroker.Focus();
                }
                else
                {
                    SearchReports();
                    pnlGrid.Visible = true;
                    lnkGenerateReport.Visible = true;
                    pnlReport.Visible = false;
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error searching, due to: " + ex.Message);
            }
        }

        protected void lnkClearFrom_Click(object sender, EventArgs e)
        {
            try
            {
                txtSearchDateFrom.Text = string.Empty;
            }
            catch (Exception ex)
            {
                notification("Error", "Error clearing from date, due to: " + ex.Message);
            }
        }

        protected void lnkClearTo_Click(object sender, EventArgs e)
        {
            try
            {
                txtSearchDateTo.Text = string.Empty;
            }
            catch (Exception ex)
            {
                notification("Error", "Error clearing to date, due to: " + ex.Message);
            }
        }

        protected void lnkClearBroker_Click(object sender, EventArgs e)
        {
            try
            {
                ddlSearchBroker.ClearSelection();
            }
            catch (Exception ex)
            {
                notification("Error", "Error clearing broker, due to: " + ex.Message);
            }
        }

        protected void lnkClearVehicleRegNo_Click(object sender, EventArgs e)
        {
            try
            {
                txtSearchVehicleRegNo.Text = string.Empty;
            }
            catch (Exception ex)
            {
                notification("Error", "Error clearing Vehicle, due to: " + ex.Message);
            }
        }

        protected void lnkClearBiltyNo_Click(object sender, EventArgs e)
        {

        }

        protected void lnkClearFilterAll_Click(object sender, EventArgs e)
        {
            try
            {
                txtSearchDateFrom.Text = string.Empty;
                txtSearchDateTo.Text = string.Empty;
                ddlSearchBroker.ClearSelection();
                txtSearchVehicleRegNo.Text = string.Empty;
                txtSearchBiltyNo.Text = string.Empty;

                lnkGenerateReport.Visible = false;
                lnkCloseReport.Visible = false;
            }
            catch (Exception ex)
            {
                notification("Error", "Error clearing all filters, due to: " + ex.Message);
            }
        }

        #endregion

        #endregion

        #region Working Area

        #endregion

        protected void lnkGenerateReport_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSearchDateFrom.Text == string.Empty && txtSearchDateTo.Text == string.Empty && ddlSearchBroker.SelectedIndex == 0 && txtSearchVehicleRegNo.Text == string.Empty && txtSearchBiltyNo.Text == string.Empty)
                {
                    notification("Error", "Please provide any Report filter");
                    txtSearchDateFrom.Focus();
                }
                else
                {
                    GetReport();

                    pnlGrid.Visible = false;
                    pnlReport.Visible = true;
                }

            }
            catch (Exception ex)
            {
                notification("Error", "Error generating report, due to: " + ex.Message);
            }
        }

        protected void lnkCloseReport_Click(object sender, EventArgs e)
        {
            try
            {
                pnlGrid.Visible = false;
                pnlReport.Visible = false;

                lnkGenerateReport.Visible = false;
                lnkCloseReport.Visible = false;
            }
            catch (Exception ex)
            {
                notification("Error", "Error closing report, due to: " + ex.Message);
            }
        }
    }
}