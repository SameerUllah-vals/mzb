using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiltySystem
{
    public partial class Invoices : System.Web.UI.Page
    {
        #region Members

        int loginid;

        Random rnd = new Random();

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

        private void CheckBoxList(DataTable dt, CheckBoxList _cbl, string _cblValue, string _cblText, string _cblDefaultText)
        {
            if (dt.Rows.Count > 0)
            {
                _cbl.DataSource = dt;

                _cbl.DataValueField = _cblValue;
                _cbl.DataTextField = _cblText;

                _cbl.DataBind();
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

        #region Page Load

        protected void Page_Load(object sender, EventArgs e)
        {
            notification("", "");
            if (!IsPostBack)
            {
                this.Title = "Invoice";
                GetInvoices();
                //try
                //{
                //    InvoiceDML dmlInvoice = new InvoiceDML();
                //    DataTable dtInvoice = dmlInvoice.GetCompletedContainers();
                //    if (dtInvoice.Rows.Count > 0)
                //    {
                //        gvAllContainers.DataSource = dtInvoice;
                //    }
                //    else
                //    {
                //        gvAllContainers.DataSource = null;
                //    }
                //    gvAllContainers.DataBind();
                //}
                //catch (Exception ex)
                //{
                //    notification("Error", "Error getting completed containers, due to: " + ex.Message);
                //}

                //Gettting/Populating Damage Items
                try
                {
                    CompanyDML dml = new CompanyDML();
                    DataTable dtCompanies = dml.GetCompaniesForBilty();
                    FillDropDown(dtCompanies, ddlBilToCustomer, "CompanyID", "Company", "- Select -");
                }
                catch (Exception ex)
                {
                    notification("Error", "Error getting/populating Damage items, due to: " + ex.Message);
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

        public void GetInvoices()
        {
            try
            {
                InvoiceDML dml = new InvoiceDML();
                DataTable dtInvoices = dml.GetInvoices();
                if (dtInvoices.Rows.Count > 0)
                {
                    gvInvoices.DataSource = dtInvoices;
                }
                else
                {
                    gvInvoices.DataSource = null;
                }
                gvInvoices.DataBind();
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting/binding invoices, due to: " + ex.Message);
            }
        }

        #endregion

        #region Events

        #region Linkbutton's Click

        protected void lnkSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlBilToCustomer.SelectedIndex == 0)
                {
                    notification("Error", "Please Bill to customer");
                    ddlBilToCustomer.Focus();
                }
                else
                {
                    Int64 CustomerCompanyID = Convert.ToInt64(ddlBilToCustomer.SelectedItem.Value);
                    string Query = string.Empty;
                    Query += " AND o.CustomerCompanyID = " + CustomerCompanyID;

                    if (txtContainerNo.Text != string.Empty)
                    {
                        string ContainerNo = txtContainerNo.Text.Trim();
                        Query += " AND oc.ContainerNo = '" + ContainerNo + "'";
                    }
                    if (txtOrderDate.Text != string.Empty)
                    {
                        DateTime Date = Convert.ToDateTime(txtOrderDate.Text);
                        Query += " AND CONVERT(date, o.RecordedDate) = CONVERT(date, '" + Date.ToString("yyyy-MM-dd") + "')";
                    }
                    if (txtOrderNo.Text != string.Empty)
                    {
                        string OrderNo = txtOrderNo.Text;
                        Query += " AND o.OrderNo = '" + OrderNo + "'";
                    }

                    Query += " AND ce.ExpenseTypeID = (SELECT ExpensesTypeID FROM ExpensesType WHERE ExpensesTypeName = 'Weighment Charges')";

                    InvoiceDML dml = new InvoiceDML();

                    DataTable dtContainers = dml.GetCompletedContainers(Query);
                    if (dtContainers.Rows.Count > 0)
                    {
                        gvAllContainers.DataSource = dtContainers;
                    }
                    else
                    {
                        gvAllContainers.DataSource = null;
                    }
                    gvAllContainers.DataBind();

                    if (gvSelectedContainers.Rows.Count > 0)
                    {
                        if (gvSelectedContainers.DataKeys[0]["CustomerCompanyID"].ToString() != ddlBilToCustomer.SelectedItem.Value)
                        {
                            gvSelectedContainers.DataSource = null;
                            gvSelectedContainers.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error searching/binding completed containers, due to: " + ex.Message);
            }
        }

        protected void lnkCloseInvoices_Click(object sender, EventArgs e)
        {
            try
            {
                modalInvoice.Hide();
            }
            catch (Exception ex)
            {
                notification("Error", "Error closing invoice, due to: " + ex.Message);
            }
        }

        protected void lnkGenerateInvoice_Click(object sender, EventArgs e)
        {

            try
            {
                ConfirmModal("Are you sure you want to generate invoice?", "GenInvoice");
            }
            catch (Exception ex)
            {
                notification("Error", "Error generating inoive, due to: " + ex.Message);
            }
            //try
            //{
            //    containersList.InnerHtml = string.Empty;
            //    double ContainerRate = 0;
            //    int ContainersQty = 0;
            //    int containerSize = 0;

            //    string[] LocationString = gvSelectedContainers.Rows[0].Cells[5].Text.Split('|');
            //    string descriptionLocation = "from " + LocationString[0].ToString();
            //    double TotalInvoiceBill = 0;
            //    tblContainerExpense.InnerHtml = string.Empty;
            //    tblCotainerWeighment.InnerHtml = string.Empty;

            //    DataTable dtWeighmentBreakUp = new DataTable();
            //    dtWeighmentBreakUp.Columns.Add("Amount", typeof(double));
            //    dtWeighmentBreakUp.Columns.Add("Count", typeof(int));

            //    DataTable dtLolo = new DataTable();
            //    dtLolo.Columns.Add("Amount", typeof(double));
            //    dtLolo.Columns.Add("Count", typeof(int));

            //    for (int i = 0; i < gvSelectedContainers.Rows.Count; i++)
            //    {
            //        OrderDML dml = new OrderDML();
            //        Int64 OrderContainerID = Convert.ToInt64(gvSelectedContainers.DataKeys[i].Values["OrderConsignmentID"]);
            //        DataTable dtContainerExpense = dml.GetExpenses(OrderContainerID);
            //        if (dtContainerExpense.Rows.Count > 0)
            //        {   
            //            foreach (DataRow _dr in dtContainerExpense.Rows)
            //            {
            //                double ExpenseAmount = Convert.ToDouble(_dr["Amount"]);
            //                TotalInvoiceBill += ExpenseAmount;
            //                if (_dr["ExpensesTypeName"].ToString() == "Weighment Charges")
            //                {
            //                    if (dtWeighmentBreakUp.Rows.Count > 0)
            //                    {
            //                        int breakIndex = 0;
            //                        bool isExists = false;

            //                        for (int drBreakUp = 0; drBreakUp < dtWeighmentBreakUp.Rows.Count; drBreakUp++)
            //                        {
            //                            if (dtWeighmentBreakUp.Rows[drBreakUp]["Amount"].ToString() == ExpenseAmount.ToString())
            //                            {
            //                                isExists = true;
            //                                breakIndex = drBreakUp;
            //                            }
            //                        }

            //                        if (isExists == true)
            //                        {
            //                            dtWeighmentBreakUp.Rows[breakIndex]["Count"] = (Convert.ToInt32(dtWeighmentBreakUp.Rows[breakIndex]["Count"]) + 1).ToString();
            //                        }
            //                        else
            //                        {
            //                            dtWeighmentBreakUp.Rows.Add(ExpenseAmount, 1);
            //                        }
            //                    }
            //                    else
            //                    {
            //                        dtWeighmentBreakUp.Rows.Add(ExpenseAmount, 1);
            //                    }
            //                }
            //                else if (_dr["ExpensesTypeName"].ToString() == "Empty Lift On Charges")
            //                {
            //                    if (dtLolo.Rows.Count > 0)
            //                    {
            //                        int breakIndex = 0;
            //                        bool isExists = false;

            //                        for (int drBreakUp = 0; drBreakUp < dtLolo.Rows.Count; drBreakUp++)
            //                        {
            //                            if (dtLolo.Rows[drBreakUp]["Amount"].ToString() == ExpenseAmount.ToString())
            //                            {
            //                                isExists = true;
            //                                breakIndex = drBreakUp;
            //                            }
            //                        }

            //                        if (isExists == true)
            //                        {
            //                            dtLolo.Rows[breakIndex]["Count"] = (Convert.ToInt32(dtLolo.Rows[breakIndex]["Count"]) + 1).ToString();
            //                        }
            //                        else
            //                        {
            //                            dtLolo.Rows.Add(ExpenseAmount, 1);
            //                        }
            //                    }
            //                    else
            //                    {
            //                        dtLolo.Rows.Add(ExpenseAmount, 1);
            //                    }
            //                }
            //                else
            //                {
            //                    tblContainerExpense.InnerHtml += "<tr>";
            //                    tblContainerExpense.InnerHtml += "<td>&nbsp;</td>";
            //                    tblContainerExpense.InnerHtml += "<td>" + _dr["ExpensesTypeName"].ToString() + " on " + _dr["ContainerNo"].ToString() + "</td>";
            //                    tblContainerExpense.InnerHtml += "<td>" + _dr["Amount"].ToString() + "</td>";
            //                    tblContainerExpense.InnerHtml += "<td>" + _dr["Amount"].ToString() + "</td>";
            //                    tblContainerExpense.InnerHtml += "</tr>";
            //                }

            //            }
            //        }

            //        //TotalInvoiceBill += Convert.ToDouble(gvSelectedContainers.DataKeys[i].Values["WeighmentCharges"]);

            //        containersList.InnerHtml += "<tr>";
            //        containersList.InnerHtml += "<td class=\"text-center\">" + Convert.ToDateTime(gvSelectedContainers.Rows[0].Cells[1].Text).ToString("dd-MM-yyyy") + "</td>";
            //        containersList.InnerHtml += "<td class=\"text-center\">" + gvSelectedContainers.Rows[i].Cells[3].Text + "</td>";
            //        containersList.InnerHtml += "<td class=\"text-center\">" + gvSelectedContainers.Rows[i].Cells[0].Text + "</td>";
            //        containersList.InnerHtml += "</tr>";

            //        ContainerRate += Convert.ToDouble(gvSelectedContainers.Rows[i].Cells[4].Text);
            //        ContainersQty++;
            //        containerSize = 20;
            //        string[] Location = gvSelectedContainers.Rows[i].Cells[6].Text.Split();
            //        descriptionLocation += " to " + Location[0].ToString();

            //        TotalInvoiceBill += Convert.ToDouble(gvSelectedContainers.Rows[i].Cells[4].Text);
            //    }
            //    if (dtLolo.Rows.Count > 0)
            //    {
            //        for (int i = 0; i < dtLolo.Rows.Count; i++)
            //        {
            //            tblCotainerWeighment.InnerHtml += "<tr>";
            //            tblCotainerWeighment.InnerHtml += "<td>&nbsp;</td>";
            //            tblCotainerWeighment.InnerHtml += "<td>" + (i > 0 ? string.Empty : "Empty lift on Charges") + "</td>";
            //            tblCotainerWeighment.InnerHtml += "<td>" + dtLolo.Rows[i]["Amount"].ToString() + " X " + dtLolo.Rows[i]["Count"] + "</td>";
            //            tblCotainerWeighment.InnerHtml += "<td>" + (Convert.ToDouble(dtLolo.Rows[i]["Amount"].ToString()) * Convert.ToDouble(dtLolo.Rows[i]["Count"])).ToString() + "</td>";
            //            tblCotainerWeighment.InnerHtml += "</tr>";
            //        }
            //    }

            //    if (dtWeighmentBreakUp.Rows.Count > 0)
            //    {
            //        for (int i = 0; i < dtWeighmentBreakUp.Rows.Count; i++)
            //        {
            //            tblCotainerWeighment.InnerHtml += "<tr>";
            //            tblCotainerWeighment.InnerHtml += "<td>&nbsp;</td>";
            //            tblCotainerWeighment.InnerHtml += "<td>" + (i > 0 ? string.Empty : "Weighment Charges") + " </td>";
            //            tblCotainerWeighment.InnerHtml += "<td>" + dtWeighmentBreakUp.Rows[i]["Amount"].ToString() + " X " + dtWeighmentBreakUp.Rows[i]["Count"] + "</td>";
            //            tblCotainerWeighment.InnerHtml += "<td>" + (Convert.ToDouble(dtWeighmentBreakUp.Rows[i]["Amount"].ToString()) * Convert.ToDouble(dtWeighmentBreakUp.Rows[i]["Count"])).ToString() + "</td>";
            //            tblCotainerWeighment.InnerHtml += "</tr>";
            //        }
            //    }
            //    string[] BillTo = ddlBilToCustomer.SelectedItem.Text.Split('|');
            //    lblBilltoCustomer.Text = BillTo[1].ToString();

            //    lblInvoiceDescription.Text = ContainersQty + "X" + containerSize + " for Export " + descriptionLocation;
            //    lblInvoiceContainerRate.Text = ContainerRate.ToString();
            //    lblInvoicecontainerTotal.Text = ContainerRate.ToString();

            //    lblInvoiceGrandTotal.Text = TotalInvoiceBill.ToString();
            //    modalInvoice.Show();
            //}
            //catch (Exception ex)
            //{
            //    notification("Error", "Error generating invoice, due to: " + ex.Message);
            //}
        }

        protected void lnkAddContainers_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtSelectedContainers = new DataTable();
                dtSelectedContainers.Columns.Add("OrderConsignmentID");
                dtSelectedContainers.Columns.Add("RecordedDate");
                dtSelectedContainers.Columns.Add("AssignedVehicle");
                dtSelectedContainers.Columns.Add("ContainerNo");
                dtSelectedContainers.Columns.Add("EmptyContainerPickLocation");
                dtSelectedContainers.Columns.Add("EmptyContainerDropLocation");
                dtSelectedContainers.Columns.Add("OrderNo");

                if (gvSelectedContainers.Rows.Count > 0)
                {
                    foreach (GridViewRow _gvrSelectedContainers in gvSelectedContainers.Rows)
                    {
                        string ContainerID = gvSelectedContainers.DataKeys[_gvrSelectedContainers.RowIndex]["OrderConsignmentID"].ToString();
                        string ContainerNo = _gvrSelectedContainers.Cells[0].Text;
                        string Date = _gvrSelectedContainers.Cells[1].Text;
                        string OrderNo = _gvrSelectedContainers.Cells[2].Text;
                        string VehicleRegNo = _gvrSelectedContainers.Cells[3].Text;
                        string Pickup = _gvrSelectedContainers.Cells[4].Text;
                        string Dropoff = _gvrSelectedContainers.Cells[5].Text;
                        dtSelectedContainers.Rows.Add(ContainerID, Date, VehicleRegNo, ContainerNo, Pickup, Dropoff, OrderNo);
                    }
                }

                foreach (GridViewRow _gvrAllContainers in gvAllContainers.Rows)
                {
                    bool flag = false;
                    string ContainerNo = _gvrAllContainers.Cells[1].Text;
                    if (gvSelectedContainers.Rows.Count > 0)
                    {

                        foreach (GridViewRow _gvrSelectedControls in gvSelectedContainers.Rows)
                        {
                            string SelectedContainerNo = _gvrSelectedControls.Cells[0].Text;
                            if (ContainerNo == SelectedContainerNo)
                            {
                                flag = true;
                            }
                        }
                    }

                    if (flag == false)
                    {
                        string ContainerID = gvAllContainers.DataKeys[_gvrAllContainers.RowIndex]["OrderConsignmentID"].ToString();
                        //string ContainerNo = _gvrSelectedContainers.Cells[0].ToString();
                        string Date = _gvrAllContainers.Cells[2].Text;
                        string OrderNo = _gvrAllContainers.Cells[3].Text;
                        string VehicleRegNo = _gvrAllContainers.Cells[4].Text;
                        string Pickup = _gvrAllContainers.Cells[5].Text;
                        string Dropoff = _gvrAllContainers.Cells[6].Text;
                        dtSelectedContainers.Rows.Add(ContainerID, Date, VehicleRegNo, ContainerNo, Pickup, Dropoff, OrderNo);
                    }
                }

                if (dtSelectedContainers.Rows.Count > 0)
                {
                    gvSelectedContainers.DataSource = dtSelectedContainers;
                }
                else
                {
                    gvSelectedContainers.DataSource = null;
                }
                gvSelectedContainers.DataBind();
            }
            catch (Exception ex)
            {
                notification("Error", "Error adding containers, due to: " + ex.Message);
            }
        }

        protected void lnkConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                string Action = hfConfirmAction.Value;
                if (Action == "GenInvoice")
                {
                    try
                    {
                        containersList.InnerHtml = string.Empty;
                        double ContainerRate = 0;
                        int ContainersQty = 0;
                        int containerSize = 0;

                        string[] LocationString = gvSelectedContainers.Rows[0].Cells[5].Text.Split('|');
                        string descriptionLocation = "from " + LocationString[0].ToString();
                        double TotalInvoiceBill = 0;
                        tblContainerExpense.InnerHtml = string.Empty;
                        tblCotainerWeighment.InnerHtml = string.Empty;

                        DataTable dtWeighmentBreakUp = new DataTable();
                        dtWeighmentBreakUp.Columns.Add("Amount", typeof(double));
                        dtWeighmentBreakUp.Columns.Add("Count", typeof(int));

                        DataTable dtLolo = new DataTable();
                        dtLolo.Columns.Add("Amount", typeof(double));
                        dtLolo.Columns.Add("Count", typeof(int));

                        string ContainersIDs = string.Empty;

                        for (int i = 0; i < gvSelectedContainers.Rows.Count; i++)
                        {
                            OrderDML dml = new OrderDML();
                            Int64 OrderContainerID = Convert.ToInt64(gvSelectedContainers.DataKeys[i].Values["OrderConsignmentID"]);
                            DataTable dtContainerExpense = dml.GetExpenses(OrderContainerID);
                            if (dtContainerExpense.Rows.Count > 0)
                            {
                                foreach (DataRow _dr in dtContainerExpense.Rows)
                                {
                                    double ExpenseAmount = Convert.ToDouble(_dr["Amount"]);
                                    TotalInvoiceBill += ExpenseAmount;
                                    if (_dr["ExpensesTypeName"].ToString() == "Weighment Charges")
                                    {
                                        if (dtWeighmentBreakUp.Rows.Count > 0)
                                        {
                                            int breakIndex = 0;
                                            bool isExists = false;

                                            for (int drBreakUp = 0; drBreakUp < dtWeighmentBreakUp.Rows.Count; drBreakUp++)
                                            {
                                                if (dtWeighmentBreakUp.Rows[drBreakUp]["Amount"].ToString() == ExpenseAmount.ToString())
                                                {
                                                    isExists = true;
                                                    breakIndex = drBreakUp;
                                                }
                                            }

                                            if (isExists == true)
                                            {
                                                dtWeighmentBreakUp.Rows[breakIndex]["Count"] = (Convert.ToInt32(dtWeighmentBreakUp.Rows[breakIndex]["Count"]) + 1).ToString();
                                            }
                                            else
                                            {
                                                dtWeighmentBreakUp.Rows.Add(ExpenseAmount, 1);
                                            }
                                        }
                                        else
                                        {
                                            dtWeighmentBreakUp.Rows.Add(ExpenseAmount, 1);
                                        }
                                    }
                                    else if (_dr["ExpensesTypeName"].ToString() == "Empty Lift On Charges")
                                    {
                                        if (dtLolo.Rows.Count > 0)
                                        {
                                            int breakIndex = 0;
                                            bool isExists = false;

                                            for (int drBreakUp = 0; drBreakUp < dtLolo.Rows.Count; drBreakUp++)
                                            {
                                                if (dtLolo.Rows[drBreakUp]["Amount"].ToString() == ExpenseAmount.ToString())
                                                {
                                                    isExists = true;
                                                    breakIndex = drBreakUp;
                                                }
                                            }

                                            if (isExists == true)
                                            {
                                                dtLolo.Rows[breakIndex]["Count"] = (Convert.ToInt32(dtLolo.Rows[breakIndex]["Count"]) + 1).ToString();
                                            }
                                            else
                                            {
                                                dtLolo.Rows.Add(ExpenseAmount, 1);
                                            }
                                        }
                                        else
                                        {
                                            dtLolo.Rows.Add(ExpenseAmount, 1);
                                        }
                                    }
                                    else
                                    {
                                        tblContainerExpense.InnerHtml += "<tr>";
                                        tblContainerExpense.InnerHtml += "<td>&nbsp;</td>";
                                        tblContainerExpense.InnerHtml += "<td>" + _dr["ExpensesTypeName"].ToString() + " on " + _dr["ContainerNo"].ToString() + "</td>";
                                        tblContainerExpense.InnerHtml += "<td>" + _dr["Amount"].ToString() + "</td>";
                                        tblContainerExpense.InnerHtml += "<td>" + _dr["Amount"].ToString() + "</td>";
                                        tblContainerExpense.InnerHtml += "</tr>";
                                    }

                                }
                            }

                            //TotalInvoiceBill += Convert.ToDouble(gvSelectedContainers.DataKeys[i].Values["WeighmentCharges"]);

                            containersList.InnerHtml += "<tr>";
                            containersList.InnerHtml += "<td class=\"text-center\">" + Convert.ToDateTime(gvSelectedContainers.Rows[0].Cells[1].Text).ToString("dd-MM-yyyy") + "</td>";
                            containersList.InnerHtml += "<td class=\"text-center\">" + gvSelectedContainers.Rows[i].Cells[3].Text + "</td>";
                            containersList.InnerHtml += "<td class=\"text-center\">" + gvSelectedContainers.Rows[i].Cells[0].Text + "</td>";
                            containersList.InnerHtml += "</tr>";

                            ContainerRate += Convert.ToDouble(gvSelectedContainers.Rows[i].Cells[4].Text);
                            ContainersQty++;
                            containerSize = 20;
                            string[] Location = gvSelectedContainers.Rows[i].Cells[6].Text.Split();
                            descriptionLocation += " to " + Location[0].ToString();

                            TotalInvoiceBill += Convert.ToDouble(gvSelectedContainers.Rows[i].Cells[4].Text);
                            ContainersIDs += OrderContainerID.ToString() + ",";
                        }
                        if (dtLolo.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtLolo.Rows.Count; i++)
                            {
                                tblCotainerWeighment.InnerHtml += "<tr>";
                                tblCotainerWeighment.InnerHtml += "<td>&nbsp;</td>";
                                tblCotainerWeighment.InnerHtml += "<td>" + (i > 0 ? string.Empty : "Empty lift on Charges") + "</td>";
                                tblCotainerWeighment.InnerHtml += "<td>" + dtLolo.Rows[i]["Amount"].ToString() + " X " + dtLolo.Rows[i]["Count"] + "</td>";
                                tblCotainerWeighment.InnerHtml += "<td>" + (Convert.ToDouble(dtLolo.Rows[i]["Amount"].ToString()) * Convert.ToDouble(dtLolo.Rows[i]["Count"])).ToString() + "</td>";
                                tblCotainerWeighment.InnerHtml += "</tr>";
                            }
                        }

                        if (dtWeighmentBreakUp.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtWeighmentBreakUp.Rows.Count; i++)
                            {
                                tblCotainerWeighment.InnerHtml += "<tr>";
                                tblCotainerWeighment.InnerHtml += "<td>&nbsp;</td>";
                                tblCotainerWeighment.InnerHtml += "<td>" + (i > 0 ? string.Empty : "Weighment Charges") + " </td>";
                                tblCotainerWeighment.InnerHtml += "<td>" + dtWeighmentBreakUp.Rows[i]["Amount"].ToString() + " X " + dtWeighmentBreakUp.Rows[i]["Count"] + "</td>";
                                tblCotainerWeighment.InnerHtml += "<td>" + (Convert.ToDouble(dtWeighmentBreakUp.Rows[i]["Amount"].ToString()) * Convert.ToDouble(dtWeighmentBreakUp.Rows[i]["Count"])).ToString() + "</td>";
                                tblCotainerWeighment.InnerHtml += "</tr>";
                            }
                        }
                        string[] BillTo = ddlBilToCustomer.SelectedItem.Text.Split('|');
                        lblBilltoCustomer.Text = BillTo[1].ToString();

                        lblInvoiceDescription.Text = ContainersQty + "X" + containerSize + " for Export " + descriptionLocation;
                        lblInvoiceContainerRate.Text = ContainerRate.ToString();
                        lblInvoicecontainerTotal.Text = ContainerRate.ToString();

                        lblInvoiceGrandTotal.Text = TotalInvoiceBill.ToString();

                        ContainersIDs = ContainersIDs.Substring(0, ContainersIDs.Length - 1);
                        string[] Containers = ContainersIDs.Split(',');
                        string InvoiceNumber = rnd.Next().ToString();
                        for (int i = 0; i < Containers.Length; i++)
                        {
                            string CustomerCompany = lblBilltoCustomer.Text;
                            Int64 ContainerID = Convert.ToInt64(Containers[i]);
                            double Total = TotalInvoiceBill;
                            //string CustomerInvoice = txtCustomerInvoiceNo.Text;
                            InvoiceDML dmlInvoice = new InvoiceDML();
                            //Int64 InvoiceID = dmlInvoice.InsertInvoice(InvoiceNumber, CustomerInvoice, CustomerCompany, ContainerID, Total, LoginID);
                            //if (InvoiceID > 0)
                            //{
                            //    dmlInvoice.InvoiceToContainer(ContainerID);
                            //}
                            
                        }
                        modalInvoice.Show();
                    }
                    catch (Exception ex)
                    {
                        notification("Error", "Error generating invoice, due to: " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error confirming, due to: " + ex.Message);
            }
        }

        #endregion

        #region Gridview's Events

        protected void gvAllContainers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string CurrentContainerNo = e.Row.Cells[1].Text;
                    foreach (GridViewRow _gvrSelectedContainers in gvSelectedContainers.Rows)
                    {
                        if (_gvrSelectedContainers.Cells[0].Text == CurrentContainerNo)
                        {
                            CheckBox cbContainer = e.Row.FindControl("cbContainer") as CheckBox; ;
                            cbContainer.Checked = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error binding row, due to: " + ex.Message);
            }
        }

        #endregion

        #region Misc

        protected void cbContainer_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                gvSelectedContainers.DataSource = null;
                gvSelectedContainers.DataBind();
                DataTable dtSelectedContainers = new DataTable();
                dtSelectedContainers.Columns.Add("OrderConsignmentID");
                dtSelectedContainers.Columns.Add("RecordedDate");
                dtSelectedContainers.Columns.Add("AssignedVehicle");
                dtSelectedContainers.Columns.Add("ContainerNo");
                dtSelectedContainers.Columns.Add("Rate");
                dtSelectedContainers.Columns.Add("EmptyContainerPickLocation");
                dtSelectedContainers.Columns.Add("EmptyContainerDropLocation");
                dtSelectedContainers.Columns.Add("OrderNo");
                dtSelectedContainers.Columns.Add("CustomerCompanyID");
                dtSelectedContainers.Columns.Add("WeighmentCharges");
                foreach (GridViewRow _gvrAllContainers in gvAllContainers.Rows)
                {
                    CheckBox _cbContainer = _gvrAllContainers.FindControl("cbContainer") as CheckBox;
                    if (_cbContainer.Checked == true)
                    {
                        Int64 SelectedContainerID = Convert.ToInt64(gvAllContainers.DataKeys[_gvrAllContainers.RowIndex]["OrderConsignmentID"]);
                        Int64 CustomerCompanyID = Convert.ToInt64(gvAllContainers.DataKeys[_gvrAllContainers.RowIndex]["CustomerCompanyID"]);
                        string SelectedContainerNo = _gvrAllContainers.Cells[1].Text;
                        string SelectedOrderDate = _gvrAllContainers.Cells[2].Text;
                        string SelectedOrderNo = _gvrAllContainers.Cells[3].Text;
                        double Rate = Convert.ToDouble(gvAllContainers.DataKeys[_gvrAllContainers.RowIndex]["Rate"]);
                        double Weighment = Convert.ToDouble(gvAllContainers.DataKeys[_gvrAllContainers.RowIndex]["WeighmentAmount"]);

                        string SelectedVehicle = _gvrAllContainers.Cells[4].Text;
                        string SelectedPickup = _gvrAllContainers.Cells[5].Text;
                        string SelectedDropoff = _gvrAllContainers.Cells[6].Text;


                        dtSelectedContainers.Rows.Add(SelectedContainerID, SelectedOrderDate, SelectedVehicle, SelectedContainerNo, Rate, SelectedPickup, SelectedDropoff, SelectedOrderNo, CustomerCompanyID, Weighment);
                    }
                }

                if (dtSelectedContainers.Rows.Count > 0)
                {
                    gvSelectedContainers.DataSource = dtSelectedContainers;
                }
                else
                {
                    gvSelectedContainers.DataSource = null;
                }
                gvSelectedContainers.DataBind();
                //CheckBox cbContainer = sender as CheckBox;
                //GridViewRow gvr = (GridViewRow)cbContainer.Parent.Parent;
                //Int64 SelectedContainerID = Convert.ToInt64(gvAllContainers.DataKeys[gvr.RowIndex]["OrderConsignmentID"]);
                //Int64 CustomerCompanyID = Convert.ToInt64(gvAllContainers.DataKeys[gvr.RowIndex]["CustomerCompanyID"]);
                //string SelectedContainerNo = gvr.Cells[1].Text;
                //string SelectedOrderDate = gvr.Cells[2].Text;
                //string SelectedOrderNo = gvr.Cells[3].Text;
                //double Rate = Convert.ToDouble(gvAllContainers.DataKeys[gvr.RowIndex]["Rate"]);
                //double Weighment = Convert.ToDouble(gvAllContainers.DataKeys[gvr.RowIndex]["WeighmentCharges"]);

                //string SelectedVehicle = gvr.Cells[4].Text;
                //string SelectedPickup = gvr.Cells[5].Text;
                //string SelectedDropoff = gvr.Cells[6].Text;

                //DataTable dtSelectedContainers = new DataTable();
                //dtSelectedContainers.Columns.Add("OrderConsignmentID");
                //dtSelectedContainers.Columns.Add("RecordedDate");
                //dtSelectedContainers.Columns.Add("AssignedVehicle");
                //dtSelectedContainers.Columns.Add("ContainerNo");
                //dtSelectedContainers.Columns.Add("Rate");
                //dtSelectedContainers.Columns.Add("EmptyContainerPickLocation");
                //dtSelectedContainers.Columns.Add("EmptyContainerDropLocation");
                //dtSelectedContainers.Columns.Add("OrderNo");
                //dtSelectedContainers.Columns.Add("CustomerCompanyID");
                //dtSelectedContainers.Columns.Add("WeighmentCharges");


                //bool SelectedExists = false;
                //if (gvSelectedContainers.Rows.Count > 0)
                //{
                //    foreach (GridViewRow _gvrSelectedContainers in gvSelectedContainers.Rows)
                //    {
                //        string ContainerID = gvSelectedContainers.DataKeys[_gvrSelectedContainers.RowIndex]["OrderConsignmentID"].ToString();
                //        string CustomersCompanyID = gvSelectedContainers.DataKeys[_gvrSelectedContainers.RowIndex]["CustomerCompanyID"].ToString();
                //        string ContainerNo = _gvrSelectedContainers.Cells[0].Text.Replace("&nbsp;", string.Empty);
                //        string Date = _gvrSelectedContainers.Cells[1].Text.Replace("&nbsp;", string.Empty);
                //        string OrderNo = _gvrSelectedContainers.Cells[2].Text.Replace("&nbsp;", string.Empty);
                //        string VehicleRegNo = _gvrSelectedContainers.Cells[3].Text.Replace("&nbsp;", string.Empty);
                //        string Rates = _gvrSelectedContainers.Cells[4].Text.Replace("&nbsp;", string.Empty);
                //        string Pickup = _gvrSelectedContainers.Cells[5].Text.Replace("&nbsp;", string.Empty);
                //        string Dropoff = _gvrSelectedContainers.Cells[6].Text.Replace("&nbsp;", string.Empty);
                //        string Weighmentcharges = gvSelectedContainers.DataKeys[_gvrSelectedContainers.RowIndex].Values["WeighmentCharges"].ToString().Replace("&nbsp;", string.Empty);
                //        dtSelectedContainers.Rows.Add(ContainerID, Date, VehicleRegNo, ContainerNo, Rates, Pickup, Dropoff, OrderNo, CustomersCompanyID, Weighmentcharges);
                //        if (SelectedContainerNo == ContainerNo)
                //        {
                //            SelectedExists = true;
                //        }
                //    }
                //}
                //if (SelectedExists == false)
                //{
                //    dtSelectedContainers.Rows.Add(SelectedContainerID, SelectedOrderDate, SelectedVehicle, SelectedContainerNo, Rate, SelectedPickup, SelectedDropoff, SelectedOrderNo, CustomerCompanyID, Weighment);
                //}
                //gvSelectedContainers.DataSource = dtSelectedContainers;
                //gvSelectedContainers.DataBind();
            }
            catch (Exception ex)
            {
                notification("Error", "Error slecting container, due to: " + ex.Message);
            }
        }

        #endregion

        #endregion

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            try
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "Print()", true);
            }
            catch (Exception ex)
            {
                notification("Error", "Error print invoice, due to: " + ex.Message);
            }
        }

        protected void lnkCreateInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                SearchInvoices.Visible = false;
                ResultInvoices.Visible = false;

                ResultOrders.Visible = true;
                SearchOrders.Visible = true;
            }
            catch (Exception ex)
            {
                notification("Error", "Error enabling new invoice panel, due to: " + ex.Message);
            }
        }

        protected void lnkCloseInvoiceSearch_Click(object sender, EventArgs e)
        {
            try
            {
                SearchInvoices.Visible = true;
                ResultInvoices.Visible = true;

                ResultOrders.Visible = false;
                SearchOrders.Visible = false;
            }
            catch (Exception ex)
            {
                notification("Error", "Error closing new invoice panel, due to: " + ex.Message);
            }
        }

        protected void gvInvoices_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "PrintInvoice")
                {
                    try
                    {
                        int index = Convert.ToInt32(e.CommandArgument);
                        GridViewRow gvr = gvInvoices.Rows[index];
                        string InvoiceNo = gvr.Cells[0].Text;
                        string CustomerName = gvr.Cells[1].Text;
                        InvoiceDML dmlInvoice = new InvoiceDML();
                        DataTable dtInvoice = dmlInvoice.GetInvoices(InvoiceNo);



                        if (dtInvoice.Rows.Count > 0)
                        {
                            string ContainerIDs = string.Empty;
                            foreach (DataRow _drInvoices in dtInvoice.Rows)
                            {
                                ContainerIDs += _drInvoices["OrderconsignmentID"].ToString() + ",";
                            }
                            ContainerIDs = ContainerIDs.Remove(ContainerIDs.Length - 1);

                            DataTable dtContainers = dmlInvoice.GetContainers(ContainerIDs);
                            if (dtContainers.Rows.Count > 0)
                            {
                                string descriptionLocation = string.Empty;
                                containersList.InnerHtml = string.Empty;
                                double ContainerRate = 0;
                                int ContainersQty = 0;
                                int containerSize = 0;
                                for (int invRow = 0; invRow < dtContainers.Rows.Count; invRow++)
                                {
                                    string[] LocationString = dtContainers.Rows[invRow]["EmptyContainerPickLocation"].ToString().Split('|');
                                    descriptionLocation = "from " + LocationString[0].ToString();
                                }

                                double TotalInvoiceBill = 0;
                                tblContainerExpense.InnerHtml = string.Empty;
                                tblCotainerWeighment.InnerHtml = string.Empty;

                                DataTable dtWeighmentBreakUp = new DataTable();
                                dtWeighmentBreakUp.Columns.Add("Amount", typeof(double));
                                dtWeighmentBreakUp.Columns.Add("Count", typeof(int));

                                DataTable dtLolo = new DataTable();
                                dtLolo.Columns.Add("Amount", typeof(double));
                                dtLolo.Columns.Add("Count", typeof(int));

                                string ContainersIDs = string.Empty;

                                for (int i = 0; i < dtContainers.Rows.Count; i++)
                                {
                                    OrderDML dml = new OrderDML();
                                    Int64 OrderContainerID = Convert.ToInt64(dtContainers.Rows[i]["OrderConsignmentID"]);
                                    DataTable dtContainerExpense = dml.GetExpenses(OrderContainerID);
                                    if (dtContainerExpense.Rows.Count > 0)
                                    {
                                        foreach (DataRow _dr in dtContainerExpense.Rows)
                                        {
                                            double ExpenseAmount = Convert.ToDouble(_dr["Amount"]);
                                            TotalInvoiceBill += ExpenseAmount;
                                            if (_dr["ExpensesTypeName"].ToString() == "Weighment Charges")
                                            {
                                                if (dtWeighmentBreakUp.Rows.Count > 0)
                                                {
                                                    int breakIndex = 0;
                                                    bool isExists = false;

                                                    for (int drBreakUp = 0; drBreakUp < dtWeighmentBreakUp.Rows.Count; drBreakUp++)
                                                    {
                                                        if (dtWeighmentBreakUp.Rows[drBreakUp]["Amount"].ToString() == ExpenseAmount.ToString())
                                                        {
                                                            isExists = true;
                                                            breakIndex = drBreakUp;
                                                        }
                                                    }

                                                    if (isExists == true)
                                                    {
                                                        dtWeighmentBreakUp.Rows[breakIndex]["Count"] = (Convert.ToInt32(dtWeighmentBreakUp.Rows[breakIndex]["Count"]) + 1).ToString();
                                                    }
                                                    else
                                                    {
                                                        dtWeighmentBreakUp.Rows.Add(ExpenseAmount, 1);
                                                    }
                                                }
                                                else
                                                {
                                                    dtWeighmentBreakUp.Rows.Add(ExpenseAmount, 1);
                                                }
                                            }
                                            else if (_dr["ExpensesTypeName"].ToString() == "Empty Lift On Charges")
                                            {
                                                if (dtLolo.Rows.Count > 0)
                                                {
                                                    int breakIndex = 0;
                                                    bool isExists = false;

                                                    for (int drBreakUp = 0; drBreakUp < dtLolo.Rows.Count; drBreakUp++)
                                                    {
                                                        if (dtLolo.Rows[drBreakUp]["Amount"].ToString() == ExpenseAmount.ToString())
                                                        {
                                                            isExists = true;
                                                            breakIndex = drBreakUp;
                                                        }
                                                    }

                                                    if (isExists == true)
                                                    {
                                                        dtLolo.Rows[breakIndex]["Count"] = (Convert.ToInt32(dtLolo.Rows[breakIndex]["Count"]) + 1).ToString();
                                                    }
                                                    else
                                                    {
                                                        dtLolo.Rows.Add(ExpenseAmount, 1);
                                                    }
                                                }
                                                else
                                                {
                                                    dtLolo.Rows.Add(ExpenseAmount, 1);
                                                }
                                            }
                                            else
                                            {
                                                tblContainerExpense.InnerHtml += "<tr>";
                                                tblContainerExpense.InnerHtml += "<td>&nbsp;</td>";
                                                tblContainerExpense.InnerHtml += "<td>" + _dr["ExpensesTypeName"].ToString() + " on " + _dr["ContainerNo"].ToString() + "</td>";
                                                tblContainerExpense.InnerHtml += "<td>" + _dr["Amount"].ToString() + "</td>";
                                                tblContainerExpense.InnerHtml += "<td>" + _dr["Amount"].ToString() + "</td>";
                                                tblContainerExpense.InnerHtml += "</tr>";
                                            }

                                        }
                                    }

                                    //TotalInvoiceBill += Convert.ToDouble(gvSelectedContainers.DataKeys[i].Values["WeighmentCharges"]);

                                    containersList.InnerHtml += "<tr>";
                                    containersList.InnerHtml += "<td class=\"text-center\">" + Convert.ToDateTime(dtContainers.Rows[0]["RecordedDate"].ToString()).ToString("dd-MM-yyyy") + "</td>";
                                    containersList.InnerHtml += "<td class=\"text-center\">" + dtContainers.Rows[i]["AssignedVehicle"].ToString() + "</td>";
                                    containersList.InnerHtml += "<td class=\"text-center\">" + dtContainers.Rows[i]["ContainerNo"].ToString() + "</td>";
                                    containersList.InnerHtml += "</tr>";

                                    ContainerRate += Convert.ToDouble(dtContainers.Rows[i]["Rate"].ToString());
                                    ContainersQty++;
                                    containerSize = 20;
                                    string[] Location = dtContainers.Rows[i]["EmptyContainerDropLocation"].ToString().Split();
                                    descriptionLocation += " to " + Location[0].ToString();

                                    TotalInvoiceBill += Convert.ToDouble(dtContainers.Rows[i]["Rate"].ToString());
                                    ContainersIDs += OrderContainerID.ToString() + ",";
                                }
                                if (dtLolo.Rows.Count > 0)
                                {
                                    for (int i = 0; i < dtLolo.Rows.Count; i++)
                                    {
                                        tblCotainerWeighment.InnerHtml += "<tr>";
                                        tblCotainerWeighment.InnerHtml += "<td>&nbsp;</td>";
                                        tblCotainerWeighment.InnerHtml += "<td>" + (i > 0 ? string.Empty : "Empty lift on Charges") + "</td>";
                                        tblCotainerWeighment.InnerHtml += "<td>" + dtLolo.Rows[i]["Amount"].ToString() + " X " + dtLolo.Rows[i]["Count"] + "</td>";
                                        tblCotainerWeighment.InnerHtml += "<td>" + (Convert.ToDouble(dtLolo.Rows[i]["Amount"].ToString()) * Convert.ToDouble(dtLolo.Rows[i]["Count"])).ToString() + "</td>";
                                        tblCotainerWeighment.InnerHtml += "</tr>";
                                    }
                                }

                                if (dtWeighmentBreakUp.Rows.Count > 0)
                                {
                                    for (int i = 0; i < dtWeighmentBreakUp.Rows.Count; i++)
                                    {
                                        tblCotainerWeighment.InnerHtml += "<tr>";
                                        tblCotainerWeighment.InnerHtml += "<td>&nbsp;</td>";
                                        tblCotainerWeighment.InnerHtml += "<td>" + (i > 0 ? string.Empty : "Weighment Charges") + " </td>";
                                        tblCotainerWeighment.InnerHtml += "<td>" + dtWeighmentBreakUp.Rows[i]["Amount"].ToString() + " X " + dtWeighmentBreakUp.Rows[i]["Count"] + "</td>";
                                        tblCotainerWeighment.InnerHtml += "<td>" + (Convert.ToDouble(dtWeighmentBreakUp.Rows[i]["Amount"].ToString()) * Convert.ToDouble(dtWeighmentBreakUp.Rows[i]["Count"])).ToString() + "</td>";
                                        tblCotainerWeighment.InnerHtml += "</tr>";
                                    }
                                }
                                //string[] BillTo = ddlBilToCustomer.SelectedItem.Text.Split('|');
                                lblBilltoCustomer.Text = CustomerName;

                                lblInvoiceDescription.Text = ContainersQty + "X" + containerSize + " for Export " + descriptionLocation;
                                lblInvoiceContainerRate.Text = ContainerRate.ToString();
                                lblInvoicecontainerTotal.Text = ContainerRate.ToString();

                                lblInvoiceGrandTotal.Text = TotalInvoiceBill.ToString();

                            }



                        }


                        //modalInvoice.Show();
                    }
                    catch (Exception ex)
                    {
                        notification("Error", "Error generating invoice, due to: " + ex.Message);
                    }
                    finally
                    {
                        modalInvoice.Show();
                    }
                }
                else if (e.CommandName == "open")
                {
                    modalInvoice.Show();
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error printing inoive, due to: " + ex.Message);
            }
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            modalInvoice.Show();
        }
    }
}