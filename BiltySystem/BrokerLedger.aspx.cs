using BLL;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;

namespace BiltySystem
{
    public partial class BrokerLedger : System.Web.UI.Page
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
                    this.Title = "Broker Ledger";

                    //Getting/Binding BrokerAccounts
                    try
                    {
                        AccountsDML dml = new AccountsDML();
                        DataTable dtBrokersAccounts = dml.GetBrokerAccounts();
                        if (dtBrokersAccounts.Rows.Count > 0)
                        {
                            gvAllBrokerAcounts.DataSource = dtBrokersAccounts;
                        }
                        else
                        {
                            gvAllBrokerAcounts.DataSource = null;
                        }
                        gvAllBrokerAcounts.DataBind();
                    }
                    catch (Exception ex)
                    {
                        notification("Error", "Error getting/binding brokers accounts, due to: " + ex.Message);

                    }
                    

                    //Getting/Populating all Banks Ledgers
                    try
                    {
                        BanksDML dml = new BanksDML();
                        DataTable dtBankAccounts = dml.GetActiveBanks();
                        if (dtBankAccounts.Rows.Count > 0)
                        {
                            FillDropDown(dtBankAccounts, ddlBankAccounts, "Value", "Text", "Select Bank");
                        }
                    }
                    catch (Exception ex)
                    {
                        notification("Error", "Error getting/populating Bank Accounts");
                    }
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

        #endregion

        #region Custom Methods

        #region Web Methods

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchCompanies(string prefixText, int count)
        {
            List<string> customers = new List<string>();
            OrderDML dmlOrder = new OrderDML();
            DataTable dtCompanies = dmlOrder.GetCompanies(prefixText);
            if (dtCompanies.Rows.Count > 0)
            {
                for (int i = 0; i < dtCompanies.Rows.Count; i++)
                {
                    customers.Add(dtCompanies.Rows[i]["Company"].ToString());
                }
            }
            return customers;
        }

        #endregion

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

        public void PaymentNotification()
        {
            try
            {
                divPaymentNotification.InnerHtml = string.Empty;
            }
            catch (Exception ex)
            {
                divPaymentNotification.InnerHtml = "<div class=\"alert alert-danger\">" + ex.Message + "</div>";
            }
        }

        public void PaymentNotification(string type, string msg)
        {
            try
            {
                if (type == "Error")
                {
                    divPaymentNotification.InnerHtml = "<div class=\"alert alert-danger\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                }
                else if (type == "Success")
                {
                    divPaymentNotification.InnerHtml = "<div class=\"alert alert-success\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                }
                else if (type == string.Empty)
                {
                    divPaymentNotification.InnerHtml = string.Empty;
                }
            }
            catch (Exception ex)
            {
                divPaymentNotification.InnerHtml = "<div class=\"alert alert-danger\">" + ex.Message + "</div>";
            }
        }

        public DataTable AllAccountsGridToDT(GridView _gv, string LedgerType)
        {
            DataTable _dt = new DataTable();
            if (LedgerType == "All")
            {
                _dt.Columns.Add("Account");
                _dt.Columns.Add("Balance");
                foreach (GridViewRow _gvr in _gv.Rows)
                {
                    DataRow _dr = _dt.NewRow();
                    LinkButton lnkAccountName = _gvr.FindControl("lnkAccount") as LinkButton;
                    Label lblAccountBalance = _gvr.FindControl("lblBalance") as Label;
                    _dr[0] = lnkAccountName.Text;
                    _dr[1] = lblAccountBalance.Text;
                    _dt.Rows.Add(_dr);
                }
            }
            else
            {
                for (int i = 0; i < _gv.Columns.Count; i++)
                {
                    _dt.Columns.Add(_gv.Columns[i].HeaderText);
                }
                foreach (GridViewRow row in _gv.Rows)
                {
                    DataRow _dr = _dt.NewRow();
                    for (int j = 0; j < _gv.Columns.Count; j++)
                    {
                        _dr[j] = row.Cells[j].Text;
                    }
                    _dt.Rows.Add(_dr);
                }
            }
            return _dt;
        }

        public DataTable GridToDT(GridView gvExcel)
        {
            DataTable dt = new DataTable();
            for (int i = 0; i < gvExcel.Columns.Count; i++)
            {
                dt.Columns.Add(gvExcel.Columns[i].HeaderText);
            }
            foreach (GridViewRow row in gvExcel.Rows)
            {
                DataRow dr = dt.NewRow();
                for (int j = 0; j < gvExcel.Columns.Count; j++)
                {
                    dr[j] = row.Cells[j].Text;
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        #endregion

        #region Events

        #region Linkbuttons Events

        protected void lnkSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSearch.Text != string.Empty)
                {
                    string[] Keyword = txtSearch.Text.Split('|');
                    string SearchKeyword = Keyword[1].Trim() + "|" + Keyword[0].Trim();
                    AccountsDML dmlAccounts = new AccountsDML();
                    DataTable dtAccountCheck = dmlAccounts.GetAccounts(SearchKeyword);
                    if (dtAccountCheck.Rows.Count > 0)
                    {
                        DataTable dtAccounts = dmlAccounts.GetInAccounts(SearchKeyword);
                        if (dtAccounts.Rows.Count > 0)
                        {
                            gvResult.DataSource = dtAccounts;
                        }
                        else
                        {
                            gvResult.DataSource = null;
                            gvResult.EmptyDataText = "No account found";
                        }
                    }
                    else
                    {
                        gvResult.DataSource = null;
                        gvResult.EmptyDataText = "No account found";
                    }
                }
                else
                {
                    gvResult.DataSource = null;
                    gvResult.EmptyDataText = "No keyword for Search";
                }
                gvResult.DataBind();
            }
            catch (Exception ex)
            {
                notification("Error", "Error seaching, due to: " + ex.Message);
            }
        }

        protected void lnkAllLedgers_Click(object sender, EventArgs e)
        {
            try
            {
                MainLedger.Visible = false;
                AllAcounts.Visible = true;
            }
            catch (Exception ex)
            {
                notification("Error", "Error showing all ledgers, due to: " + ex.Message);

            }
        }

        protected void lnkSearchSingleAccount_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtFrom.Text == string.Empty && txtTo.Text == string.Empty)
                {
                    notification("Error", "Please provide any date");
                }
                else if (txtFrom.Text != string.Empty && txtTo.Text != string.Empty)
                {
                    DateTime FromDate = Convert.ToDateTime(txtFrom.Text);
                    DateTime ToDate = Convert.ToDateTime(txtTo.Text);
                    string AccountName = lblSingleLedgerAccountName.Text;
                    AccountsDML dML = new AccountsDML();

                    DataTable dtAccount = dML.GetInAccounts(AccountName, FromDate, ToDate);
                    if (dtAccount.Rows.Count > 0)
                    {
                        gvResult.DataSource = dtAccount;
                    }
                    else
                    {
                        gvResult.DataSource = null;
                    }
                    gvResult.DataBind();
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error searching ledgers, due to: " + ex.Message);
            }
        }

        protected void lnkCancelSearchSingleAccount_Click(object sender, EventArgs e)
        {

        }

        protected void lnkPayment_Click(object sender, EventArgs e)
        {
            try
            {
                string[] BrokerNameString = lblSingleLedgerAccountName.Text.Split('|');
                string BrokerName = BrokerNameString[0].ToString();

                BrokersDML dml = new BrokersDML();
                DataTable dtBrokers = dml.GetActiveBroker();
                if (dtBrokers.Rows.Count > 0)
                {
                    FillDropDown(dtBrokers, ddlVendors, "Code", "Name", "-Select-");
                }

                ddlVendors.ClearSelection();
                ddlVendors.Items.FindByText(BrokerName).Selected = true;


                ddlVendors.Enabled = false;
                rbVendor.Items.FindByText("Broker").Selected = true;
                rbVendor.Enabled = false;

                modalPayment.Show();
            }
            catch (Exception ex)
            {
                notification("Error", "Error enabling voucher inputs, due to: " + ex.Message);
            }
        }

        protected void lnkPrintAllAccounts_Click(object sender, EventArgs e)
        {
            try
            {

                DataTable dtAllAccounts = AllAccountsGridToDT(gvAllBrokerAcounts, "All");
                if (dtAllAccounts.Rows.Count > 0)
                {
                    rvBrokerLedger.LocalReport.DataSources.Add(new ReportDataSource("BrokerLedgerDataSet", dtAllAccounts));
                    rvBrokerLedger.LocalReport.ReportPath = Server.MapPath("~/PrintView/BrokerLedgerPrintView.rdlc");
                    rvBrokerLedger.LocalReport.EnableHyperlinks = true;
                }
                AllAcounts.Attributes.Add("style", "display:none");
                pnlPrintAllAccounts.Attributes.Add("style", "display:block");

            }
            catch (Exception ex)
            {
                notification("Error", "Error printing all accounts, due to: " + ex.Message);
            }
        }

        protected void lnkCloseAllAccountPrint_Click(object sender, EventArgs e)
        {
            try
            {
                pnlPrintAllAccounts.Attributes.Add("style", "display:none");
                AllAcounts.Attributes.Add("style", "display:block");
            }
            catch (Exception ex)
            {
                notification("Error", "Error closing all accounts printing, due to: " + ex.Message);
            }
        }

        protected void lnkCloseSingleLedger_Click(object sender, EventArgs e)
        {

            try
            {
                pnlPrintSingleLedger.Attributes.Add("style", "display:none");
                MainLedger.Attributes.Add("style", "display:block");
            }
            catch (Exception ex)
            {
                notification("Error", "Error closing all accounts printing, due to: " + ex.Message);
            }
        }

        protected void lnkPrintSingleLedger_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtSingleAccounts = AllAccountsGridToDT(gvResult, "Single");
                if (dtSingleAccounts.Rows.Count > 0)
                {
                    rvAllBrokerLedger.LocalReport.DataSources.Add(new ReportDataSource("AllBrokerLedgerDataSet", dtSingleAccounts));
                    rvAllBrokerLedger.LocalReport.ReportPath = Server.MapPath("~/PrintView/AllBrokerLedgerReport.rdlc");
                    rvAllBrokerLedger.LocalReport.EnableHyperlinks = true;
                    pnlPrintSingleLedger.Attributes.Add("style", "display:block");
                    MainLedger.Attributes.Add("style", "display:none");
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error printing single accounts, due to: " + ex.Message);
            }
        }

        protected void lnkSavePayment_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlVendors.SelectedIndex == -1)
                {
                    PaymentNotification("Error", "Please select vendor");
                    if (ddlVendors.Enabled == true)
                    {
                        ddlVendors.Focus();
                    }
                    else
                    {
                        rbVendor.Focus();
                    }
                    modalPayment.Show();
                }
                else if (txtAmount.Text == string.Empty)
                {
                    PaymentNotification("Error", "Please enter payment amount");
                    txtAmount.Focus();
                    modalPayment.Show();
                }
                else if (rbPaymentMode.SelectedIndex == -1)
                {
                    PaymentNotification("Error", "Please select Payment method");
                    rbPaymentMode.Focus();
                    modalPayment.Show();
                }
                else if (rbPaymentMode.SelectedItem.Text == "Cheque" && ddlBankAccounts.SelectedIndex == 0)
                {
                    PaymentNotification("Error", "Please select Bank");
                    ddlBankAccounts.Focus();
                    modalPayment.Show();
                }
                else
                {
                    Random rnd = new Random();
                    string VoucherNo = rnd.Next().ToString();
                    string VendorType = rbVendor.SelectedItem.Text;
                    string Vendor = ddlVendors.SelectedItem.Text;
                    double Amount = Convert.ToDouble(txtAmount.Text);
                    string ChequeNo = rbPaymentMode.SelectedItem.Text == "Cash" ? string.Empty : txtDocumentNo.Text;
                    string DocumentNo = rbPaymentMode.SelectedItem.Text == "Cash" ? string.Empty : (" by Cheque# " + txtDocumentNo.Text);
                    string VehicleRegNo = txtVehicleRegNo.Text;
                    Int64 OrderID = cbAdvance.Checked ? Convert.ToInt64(ddlOrder.SelectedItem.Value) : 0;
                    Int64 VendorID = 0;
                    Int64 BrokerID = 0;
                    Int64 PatrolPumpID = 0;
                    string VendorAccName = Vendor + "|" + ddlVendors.SelectedItem.Value;
                    Int64 BankID = 0;
                    string BankAccount = string.Empty;
                    if (rbPaymentMode.SelectedItem.Text == "Cheque")
                    {
                        BankAccount = ddlBankAccounts.SelectedItem.Value;
                        string[] BankAccountString = ddlBankAccounts.SelectedItem.Value.Split('|');
                        string BankName = BankAccountString[0];
                        BanksDML dmlBank = new BanksDML();
                        DataTable dtBanks = dmlBank.GetBankByName(BankName);
                        if (dtBanks.Rows.Count > 0)
                        {
                            BankID = Convert.ToInt64(dtBanks.Rows[0]["BankID"]);
                        }
                    }

                    BrokersDML dmlBrokers = new BrokersDML();
                    DataTable dtVendor = VendorType == "Broker" ? dmlBrokers.GetActiveBrokersByName(Vendor) : new DataTable();
                    if (dtVendor.Rows.Count > 0)
                    {
                        if (VendorType == "Broker")
                        {
                            BrokerID = Convert.ToInt64(dtVendor.Rows[0][VendorType == "Broker" ? "ID" : "PumpID"]);
                        }
                        else
                        {
                            PatrolPumpID = Convert.ToInt64(dtVendor.Rows[0][VendorType == "Broker" ? "ID" : "PumpID"]);
                        }
                        //VendorID = Convert.ToInt64(dtVendor.Rows[0][VendorType == "Broker" ? "ID" : "PumpID"]);
                    }

                    OrderDML dmlOrder = new OrderDML();
                    VouchersDML dmlVouchers = new VouchersDML();
                    Int64 VoucherID = dmlVouchers.InsertVouchers(VoucherNo, BrokerID, PatrolPumpID, BankID, ChequeNo, Amount, OrderID, VehicleRegNo, LoginID);
                    if (VoucherID > 0)
                    {
                        AccountsDML dmlBRAccount = new AccountsDML();
                        DataTable dtBRAccount = dmlBRAccount.GetInAccounts(VendorAccName);
                        double Debit = 0;
                        double Credit = Amount;
                        double Balance = Convert.ToDouble(dtBRAccount.Rows[dtBRAccount.Rows.Count - 1]["Balance"]);
                        Balance = Balance - Credit + Debit;

                        string Description = "Rs. " + Amount + "/- has been debitted from " + BankAccount + DocumentNo + " against Voucher # " + VoucherNo;

                        Int64 BrokerTransactionID = dmlBRAccount.InsertInBrokerAccount(VendorAccName, VendorID, Description, Debit, Credit, Balance, LoginID);
                        if (rbPaymentMode.SelectedItem.Text == "Cheque")
                        {
                            string[] BankAccountString = ddlBankAccounts.SelectedItem.Value.Split('|');
                            BankAccount = ddlBankAccounts.SelectedItem.Value;
                            string BankName = BankAccountString[0];

                            double BankDebit = 0;
                            double BankCredit = Amount;
                            double BankBalance = 0;
                            BankAccountsDML dmlBankAccounts = new BankAccountsDML();





                            DataTable dtBankLedger = dmlBankAccounts.GetInAccountsAlonBankInfo(BankAccount, "DESC");
                            if (dtBankLedger.Rows.Count > 0)
                            {
                                BankBalance = Convert.ToDouble(dtBankLedger.Rows[0]["Balance"]);
                            }
                            BankBalance = BankBalance + BankDebit - BankCredit;
                            string BankDescription = "Rs. " + Amount + "/- paid to " + rbVendor.SelectedItem.Text + " " + Vendor + " against Voucher # " + VoucherNo;

                            dmlBankAccounts.InsertInAccount(BankAccount, BankID, BankDescription, BankDebit, BankCredit, BankBalance, DateTime.Now, "", "", LoginID);
                        }



                        DataTable dtAccountCheck = dmlBRAccount.GetAccounts(VendorAccName);
                        if (dtAccountCheck.Rows.Count > 0)
                        {
                            DataTable dtAccounts = dmlBRAccount.GetInAccounts(VendorAccName);
                            if (dtAccounts.Rows.Count > 0)
                            {
                                gvResult.DataSource = dtAccounts;
                                lblSingleLedgerAccountName.Text = VendorAccName;

                                AllAcounts.Visible = false;
                                MainLedger.Visible = true;
                            }
                            else
                            {
                                gvResult.DataSource = null;
                                gvResult.EmptyDataText = "No account found";
                            }
                        }
                        else
                        {
                            gvResult.DataSource = null;
                            gvResult.EmptyDataText = "No account found";
                        }
                        gvResult.DataBind();

                        rbVendor.ClearSelection();
                        ddlVendors.ClearSelection();
                        txtAmount.Text = string.Empty;
                        rbPaymentMode.ClearSelection();
                        txtDocumentNo.Text = string.Empty;
                        ddlBankAccounts.ClearSelection();

                    }
                    //Random rnd = new Random();
                    //string VoucherNo = rnd.Next().ToString();
                    //string VendorType = rbVendor.SelectedItem.Text;
                    //string Vendor = ddlVendors.SelectedItem.Text;
                    //double Amount = Convert.ToDouble(txtAmount.Text);
                    ////string ChequeNo = rbPaymentMode.SelectedItem.Text == "Cash" ? string.Empty : " by Cheque# " + ;
                    //string DocumentNo = rbPaymentMode.SelectedItem.Text == "Cash" ? string.Empty : (" by Cheque# " + txtDocumentNo.Text);
                    //Int64 VendorID = 0;
                    //string VendorAccName = Vendor + "|" + ddlVendors.SelectedItem.Value;

                    //BrokersDML dmlBrokers = new BrokersDML();
                    //DataTable dtVendor = VendorType == "Broker" ? dmlBrokers.GetActiveBrokersByName(Vendor) : new DataTable();
                    //if (dtVendor.Rows.Count > 0)
                    //{
                    //    VendorID = Convert.ToInt64(dtVendor.Rows[0][VendorType == "Broker" ? "ID" : "PumpID"]);
                    //}

                    //OrderDML dmlOrder = new OrderDML();
                    //Int64 VoucherID = dmlOrder.InsertVouchers(VoucherNo, VendorID, Amount, LoginID);
                    //if (VoucherID > 0)
                    //{
                    //    AccountsDML dmlBRAccount = new AccountsDML();
                    //    DataTable dtBRAccount = dmlBRAccount.GetInAccounts(VendorAccName);
                    //    double Debit = 0;
                    //    double Credit = Amount;
                    //    double Balance = Convert.ToDouble(dtBRAccount.Rows[dtBRAccount.Rows.Count - 1]["Balance"]);
                    //    Balance = Balance - Credit + Debit;

                    //    string BankAccount = ddlBankAccounts.SelectedItem.Value;
                    //    string Description = "Rs. " + Amount + "/- has been debitted from " + BankAccount + DocumentNo + " against Voucher # " + VoucherNo;

                    //    Int64 BrokerTransactionID = dmlBRAccount.InsertInBrokerAccount(VendorAccName, VendorID, Description, Debit, Credit, Balance, LoginID);
                    //    if (rbPaymentMode.SelectedItem.Text == "Cheque")
                    //    {
                    //        string[] BankAccountString = ddlBankAccounts.SelectedItem.Value.Split('|');
                    //        BankAccount = ddlBankAccounts.SelectedItem.Value;
                    //        string BankName = BankAccountString[0];

                    //        Int64 BankID = 0;
                    //        double BankDebit = 0;
                    //        double BankCredit = Amount;
                    //        double BankBalance = 0;
                    //        BankAccountsDML dmlBankAccounts = new BankAccountsDML();

                    //        BanksDML dmlBank = new BanksDML();
                    //        DataTable dtBanks = dmlBank.GetBankByName(BankName);
                    //        if (dtBanks.Rows.Count > 0)
                    //        {
                    //            BankID = Convert.ToInt64(dtBanks.Rows[0]["BankID"]);
                    //        }


                    //        DataTable dtBankLedger = dmlBankAccounts.GetInAccountsAlonBankInfo(BankAccount, "DESC");
                    //        if (dtBankLedger.Rows.Count > 0)
                    //        {
                    //            BankBalance = Convert.ToDouble(dtBankLedger.Rows[0]["Balance"]);
                    //        }
                    //        BankBalance = BankBalance + BankDebit - BankCredit;
                    //        string BankDescription = "Rs. " + Amount + "/- paid to " + rbVendor.SelectedItem.Text + " " + Vendor + " against Voucher # " + VoucherNo;

                    //        dmlBankAccounts.InsertInAccount(BankAccount, BankID, BankDescription, BankDebit, BankCredit, BankBalance, "", "", LoginID);
                    //    }



                    //    DataTable dtAccountCheck = dmlBRAccount.GetAccounts(VendorAccName);
                    //    if (dtAccountCheck.Rows.Count > 0)
                    //    {
                    //        DataTable dtAccounts = dmlBRAccount.GetInAccounts(VendorAccName);
                    //        if (dtAccounts.Rows.Count > 0)
                    //        {
                    //            gvResult.DataSource = dtAccounts;
                    //            lblSingleLedgerAccountName.Text = VendorAccName;

                    //            AllAcounts.Visible = false;
                    //            MainLedger.Visible = true;
                    //        }
                    //        else
                    //        {
                    //            gvResult.DataSource = null;
                    //            gvResult.EmptyDataText = "No account found";
                    //        }
                    //    }
                    //    else
                    //    {
                    //        gvResult.DataSource = null;
                    //        gvResult.EmptyDataText = "No account found";
                    //    }
                    //    gvResult.DataBind();

                    //    rbVendor.ClearSelection();
                    //    ddlVendors.ClearSelection();
                    //    txtAmount.Text = string.Empty;
                    //    rbPaymentMode.ClearSelection();
                    //    txtDocumentNo.Text = string.Empty;
                    //    ddlBankAccounts.ClearSelection();

                    //}
                }
            }
            catch (Exception ex)
            {
                PaymentNotification("Error", "Error saving broker payment, due to: " + ex.Message);
                modalPayment.Show();
            }
        }

        #endregion

        #region GridViews Events

        protected void gvAllBrokerAcounts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Account")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvAllBrokerAcounts.Rows[index];
                    //Int64 CompanyID = Convert.ToInt64(gvResult.DataKeys[index]["CompanyID"]);
                    LinkButton lnkAccount = gvr.FindControl("lnkAccount") as LinkButton;
                    string AccountName = lnkAccount.Text;
                    AccountsDML dmlAccounts = new AccountsDML();
                    DataTable dtAccountCheck = dmlAccounts.GetAccounts(AccountName);
                    if (dtAccountCheck.Rows.Count > 0)
                    {
                        DataTable dtAccounts = dmlAccounts.GetInAccounts(AccountName, "DESC");
                        if (dtAccounts.Rows.Count > 0)
                        {
                            gvResult.DataSource = dtAccounts;
                            lblSingleLedgerAccountName.Text = AccountName;

                            AllAcounts.Visible = false;
                            MainLedger.Visible = true;
                        }
                        else
                        {
                            gvResult.DataSource = null;
                            gvResult.EmptyDataText = "No account found";
                        }
                    }
                    else
                    {
                        gvResult.DataSource = null;
                        gvResult.EmptyDataText = "No account found";
                    }
                    gvResult.DataBind();
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error commanding row, due to: " + ex.Message);
            }
        }

        protected void gvAllBrokerAcounts_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
                {
                    DataRowView rowView = (DataRowView)e.Row.DataItem;
                    Int64 BrokerID = Convert.ToInt64(rowView["ID"]);
                    LinkButton lnkAccount = e.Row.FindControl("lnkAccount") as LinkButton;
                    string AccountName = lnkAccount.Text;
                    AccountsDML dmlComp = new AccountsDML();
                    DataTable dtAccount = dmlComp.GetInAccounts(AccountName);

                    Label lblBalance = e.Row.FindControl("lblBalance") as Label;
                    if (dtAccount.Rows.Count > 0)
                    {
                        //lblBalance.Text = dtAccount.Rows[dtAccount.Rows.Count - 1]["Balance"].ToString();
                        double Balance = Convert.ToDouble(dtAccount.Rows[dtAccount.Rows.Count - 1]["Balance"].ToString());
                        lblBalance.Text = String.Format("{0:n}", Balance);

                    }
                    else
                    {
                        lblBalance.Text = "N/A";
                    }
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error binding accounts to grid, due to: " + ex.Message);

            }
        }

        protected void gvResult_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string[] DateTime = e.Row.Cells[0].Text.Split(' ');
                string Date = DateTime[0];
                e.Row.Cells[0].Text = Date;
                double Debit = Convert.ToDouble(e.Row.Cells[2].Text);
                double Credit = Convert.ToDouble(e.Row.Cells[3].Text);

                double Balance = Convert.ToDouble(e.Row.Cells[4].Text);
                e.Row.Cells[2].Text = String.Format("{0:n}", Debit);
                e.Row.Cells[3].Text = String.Format("{0:n}", Credit);
                e.Row.Cells[4].Text = String.Format("{0:n}", Balance);
            }
        }

        #endregion

        #region Dropdowns Events

        protected void ddlOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlOrder.SelectedIndex != 0)
                {
                    string[] OrderAdvanceString = ddlOrder.SelectedItem.Text.Replace(" ", string.Empty).Split('|');
                    string VehicleRegNo = OrderAdvanceString[1].ToString();
                    string Amount = OrderAdvanceString[2].ToString();
                    txtVehicleRegNo.Text = VehicleRegNo;
                    txtAmount.Text = Amount;
                }
                else
                {
                    txtVehicleRegNo.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                PaymentNotification("Error", "Error selecting Order Advance, due to: " + ex.Message);
            }
            finally
            {
                modalPayment.Show();
            }
        }

        #endregion

        #region Misc

        protected void rbVendor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbVendor.SelectedItem.Text == "Broker")
                {
                    BrokersDML dml = new BrokersDML();
                    DataTable dtBrokers = dml.GetActiveBroker();
                    if (dtBrokers.Rows.Count > 0)
                    {
                        FillDropDown(dtBrokers, ddlVendors, "Code", "Name", "-Select-");
                    }
                    ddlVendors.Enabled = true;
                }
                else
                {
                    ddlVendors.Items.Clear();
                    ddlVendors.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error selecting vendor, due to: " + ex.Message);
            }
            finally
            {
                modalPayment.Show();
            }
        }

        protected void rbPaymentMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlBankAccounts.ClearSelection();
                bankAccountsPlaceholder.Visible = false;
                DocumentNoPlaceholder.Visible = false;
                if (rbPaymentMode.SelectedItem.Text == "Cheque")
                {
                    bankAccountsPlaceholder.Visible = true;
                    DocumentNoPlaceholder.Visible = true;
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error selecting payment mode, due to: " + ex.Message);
            }
            finally
            {
                modalPayment.Show();
            }
        }

        protected void cbAdvance_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbAdvance.Checked)
                {
                    ddlOrderplaceholder.Visible = true;
                    VehicleRegNoPlaceholder.Visible = true;

                    txtAmount.Enabled = false;

                    ddlOrder.ClearSelection();
                    txtVehicleRegNo.Text = string.Empty;
                    txtAmount.Text = string.Empty;
                }
                else
                {
                    ddlOrderplaceholder.Visible = false;
                    VehicleRegNoPlaceholder.Visible = false;

                    txtAmount.Enabled = true;

                    ddlOrder.ClearSelection();
                    txtVehicleRegNo.Text = string.Empty;
                    txtAmount.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                PaymentNotification("Error", "Error selecting advance check, due to: " + ex.Message);
            }
            finally
            {
                modalPayment.Show();
            }
        }

        #endregion

        #endregion
    }
}