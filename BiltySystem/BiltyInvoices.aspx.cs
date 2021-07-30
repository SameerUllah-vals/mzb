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
    public partial class BiltyInvoices : System.Web.UI.Page
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
                if (LoginID != 0 && LoginID != null)
                {
                    this.Title = "Invoices";
                    GetInvoices();

                    GetPopulateBanks();
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

        public void ResetPaymentFields()
        {
            try
            {
                DocumentNoPlaceholder.Visible = true;

                rbPaymentMode.ClearSelection();
                txtAmount.Enabled = false;

                bankAccountsPlaceholder.Visible = true;
                cbPettyCash.Checked = false;

                ddlBankAccounts.ClearSelection();
                txtAmount.Text = string.Empty;

                txtDocumentNo.Text = string.Empty;
                DocumentNoPlaceholder.Visible = false;

                hfSelectedInvoice.Value = string.Empty;
                hfSelectedInvoiceCustomer.Value = string.Empty;
            }
            catch (Exception ex)
            {
                PaymentNotification("Error", "Error resetting payment fields");
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

        public void GetSetPettyCashAccount(string AccountName)
        {
            try
            {
                AccountsDML dmlAccounts = new AccountsDML();
                DataTable dtPettyCashAccountCheck = dmlAccounts.GetAccounts(AccountName);
                if (dtPettyCashAccountCheck.Rows.Count <= 0)
                {
                    dmlAccounts.CreatePettyCashAccount(AccountName);
                    dmlAccounts.InsertInvoiceToPettyCash(AccountName, "0", "Account created (Auto)", 0, 0, 0, LoginID);
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting/setting broker account, due to: " + ex.Message);
            }
        }

        public void GetSetBankAccount(string AccountName)
        {
            try
            {
                BankAccountsDML dmlBankAccounts = new BankAccountsDML();
                DataTable dtBankAccountCheck = dmlBankAccounts.GetAccounts(AccountName);
                if (dtBankAccountCheck.Rows.Count <= 0)
                {
                    dmlBankAccounts.CreateAccount(AccountName);
                    dmlBankAccounts.InsertInAccount(AccountName, 0, "Account created", 0, 0, 0, DateTime.Now, "0", "0" ,LoginID);
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting/setting broker account, due to: " + ex.Message);
            }
        }

        public void GetSetCustomerAccount(string AccountName)
        {
            try
            {
                AccountsDML dmlAccounts = new AccountsDML();
                DataTable dtAccountCheck = dmlAccounts.GetAccounts(AccountName);
                if (dtAccountCheck.Rows.Count <= 0)
                {
                    dmlAccounts.CreateAccount(AccountName);
                    dmlAccounts.InsertInAccount(AccountName, 0, "Account Created (Auto)", 0, 0, 0, LoginID);
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting/setting broker account, due to: " + ex.Message);
            }
        }

        public void GetInvoices() 
        {
            try
            {
                OrderDML dml = new OrderDML();
                DataTable dtInvoices = dml.GetInvoices("DESC");
                DataTable dtInvoice = new DataTable();
                dtInvoice.Columns.Add("InvoiceNo");
                dtInvoice.Columns.Add("CustomerCompany");
                dtInvoice.Columns.Add("CustomerInvoice");
                dtInvoice.Columns.Add("OrderID");
                dtInvoice.Columns.Add("Total");
                dtInvoice.Columns.Add("TotalBalance");
                dtInvoice.Columns.Add("CreatedDate");
                dtInvoice.Columns.Add("CreditLimit");
                dtInvoice.Columns.Add("isPaid");
                if (dtInvoices.Rows.Count > 0)
                {
                    foreach (DataRow _drInvoices in dtInvoices.Rows)
                    {
                        DateTime CreatedDate = Convert.ToDateTime(_drInvoices["CreatedDate"]);
                        dtInvoice.Rows.Add(_drInvoices["InvoiceNo"].ToString(), _drInvoices["CustomerCompany"].ToString(), _drInvoices["CustomerInvoice"].ToString(), _drInvoices["OrderID"].ToString(), _drInvoices["Total"].ToString(), (_drInvoices["TotalBalance"] == DBNull.Value ? "0" : _drInvoices["TotalBalance"].ToString()), CreatedDate.ToString("dd/MM/yyyy"), _drInvoices["CreditLimit"], _drInvoices["isPaid"]);
                    }
                }
                gvResult.DataSource = dtInvoice.Rows.Count > 0 ? dtInvoice : null;
                gvResult.DataBind();
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting/binding invoices due to: " + ex.Message);
            }
        }

        public void GetPopulateBanks()
        {
            //Getting all Banks ledgers
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

        #endregion

        #region Events

        #region Linkbutton's Click

        protected void lnkCancelSearch_Click(object sender, EventArgs e)
        {

        }

        protected void lnkSearch_Click(object sender, EventArgs e)
        {

        }

        protected void lnkAddNew_Click(object sender, EventArgs e)
        {

        }

        protected void lnkConfirm_Click(object sender, EventArgs e)
        {

        }

        protected void lnkSavePayment_Click(object sender, EventArgs e)
        {
            try
            {
                if (rbPaymentMode.SelectedIndex == -1)
                {
                    modalPayment.Show();
                    PaymentNotification("Error", "Please select payment mode");
                    rbPaymentMode.Focus();
                }
                else if (cbPettyCash.Checked == false && ddlBankAccounts.SelectedIndex == 0)
                {
                    modalPayment.Show();
                    PaymentNotification("Error", "Please select bank account");
                    ddlBankAccounts.Focus();
                }
                else if (txtAmount.Text == string.Empty || Convert.ToDouble(txtAmount.Text) <= 0)
                {
                    modalPayment.Show();
                    PaymentNotification("Error", "Please enter valid amount");
                    txtAmount.Focus();
                }
                else
                {
                    if (rbPaymentMode.SelectedItem.Text == "Cheque" && txtDocumentNo.Text == string.Empty)
                    {
                        modalPayment.Show();
                        PaymentNotification("Error", "Please enter Document No.");
                        txtDocumentNo.Focus();
                    }
                    else
                    {
                        string InvoiceNo = hfSelectedInvoice.Value;
                        string CustomerName = hfSelectedInvoiceCustomer.Value.Trim();
                        string DocumentNo = txtDocumentNo.Text;
                        string TransferTo = cbPettyCash.Checked ? "PettyCash|PC123" : ddlBankAccounts.SelectedItem.Value;
                        string PaymentMode = rbPaymentMode.SelectedItem.Text;
                        double Amount = Convert.ToDouble(txtAmount.Text);
                        bool PettyCash = cbPettyCash.Checked ? true : false;

                        string Description = CustomerName + " Paid " + Amount + " by " + PaymentMode + ",";
                        Description += PaymentMode == "Cheque" ? " (Chueque# " + DocumentNo + ")" : string.Empty;
                        Description += " transfered to " + TransferTo;

                        OrderDML dmlInvoice = new OrderDML();
                        dmlInvoice.MakePaymentByInvoice(InvoiceNo, Amount, DocumentNo, TransferTo, PaymentMode, LoginID);
                        AccountsDML dmlAcc = new AccountsDML();
                        BankAccountsDML dmlBankAcc = new BankAccountsDML();
                        string AccountName = TransferTo;

                        Int64 PettyCashAccountID = 0;
                        Int64 BankAccountID = 0;

                        if (PettyCash)
                        {
                            //Debitting from Petty Cash
                            GetSetPettyCashAccount(AccountName);
                            DataTable dtPettyCashAccount = dmlAcc.GetInAccounts(AccountName);
                            double PettyCashBalance = (double)dtPettyCashAccount.Rows[dtPettyCashAccount.Rows.Count - 1]["Balance"];
                            double PettyCashDebit = Amount;
                            double PettyCashCredit = 0;

                            PettyCashBalance = PettyCashBalance - PettyCashCredit + PettyCashDebit;
                            PettyCashAccountID = dmlAcc.InsertInvoiceToPettyCash(AccountName, InvoiceNo, Description, PettyCashDebit, PettyCashCredit, PettyCashBalance, LoginID);
                        }
                        else
                        {
                            //Debitting from Bank Account
                            Int64 BankID = 0;
                            string[] BankAccountName = AccountName.Split('|');
                            BanksDML dmlBank = new BanksDML();
                            DataTable dtBank = dmlBank.GetBankByName(BankAccountName[0]);
                            if (dtBank.Rows.Count > 0)
                            {
                                BankID = Convert.ToInt64(dtBank.Rows[0]["BankID"]);
                                GetSetBankAccount(AccountName);
                                DataTable dtBankAccount = dmlBankAcc.GetInAccounts(AccountName, "ASC");
                                double BankAccountBalance = (double)dtBankAccount.Rows[dtBankAccount.Rows.Count - 1]["Balance"];
                                double BankAccountDebit = Amount;
                                double BankAccountCredit = 0;

                                BankAccountBalance = BankAccountBalance - BankAccountCredit + BankAccountDebit;
                                BankAccountID = dmlBankAcc.InsertInAccount(AccountName, BankID, Description, BankAccountDebit, BankAccountCredit, BankAccountBalance, DateTime.Now, "Automatic Payment System", "Transfering amount to bank after receiving Payment from Customer" ,LoginID);
                            }
                        }

                        //Crediting from Customer Account
                        if (PettyCashAccountID > 0 || BankAccountID > 0)
                        {
                            Int64 CompanyID = 0;
                            string CompanyName = hfSelectedInvoiceCustomer.Value.Trim();
                            CompanyDML dmlCompany = new CompanyDML();
                            DataTable dtCompany = dmlCompany.GetCompany(CompanyName);
                            if (dtCompany.Rows.Count > 0)
                            {
                                CompanyID = Convert.ToInt64(dtCompany.Rows[0]["CompanyID"]);
                                string CompanyCode = dtCompany.Rows[0]["CompanyCode"].ToString();
                                string CompanyAccountName = CompanyName + "|" + CompanyCode;
                                GetSetCustomerAccount(CompanyAccountName);
                                DataTable dtBankAccount = dmlAcc.GetInAccounts(CompanyAccountName);
                                double CustomerAccountBalance = (double)dtBankAccount.Rows[dtBankAccount.Rows.Count - 1]["Balance"];
                                double CustomerAccountDebit = 0;
                                double CustomerAccountCredit = Amount;

                                CustomerAccountBalance = CustomerAccountBalance - CustomerAccountCredit + CustomerAccountDebit;
                                Int64 CustomerAccountID = dmlAcc.InsertInAccount(CompanyAccountName, CompanyID, Description, CustomerAccountDebit, CustomerAccountCredit, CustomerAccountBalance, LoginID);
                            }

                        }

                        notification("Success", Description);
                        ResetPaymentFields();
                        GetInvoices();
                    }
                }
            }
            catch (Exception ex)
            {
                PaymentNotification("Error", "Error saving payment, due to: " + ex.Message);
                modalPayment.Show();
            }
        }

        #endregion

        #region Gridview's Events

        protected void gvResult_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Payment")
            {
                try
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvResult.Rows[index];
                    string InvoiceNo = (string)this.gvResult.DataKeys[index]["InvoiceNo"];
                    string CustomerCompany = gvr.Cells[3].Text;

                    hfSelectedInvoice.Value = InvoiceNo.ToString();
                    hfSelectedInvoiceCustomer.Value = gvr.Cells[3].Text;

                    modalPayment.Show();
                }
                catch (Exception ex)
                {
                    notification("Error", "Error commanding row, due to: " + ex.Message);
                }
            }
        }

        protected void gvResult_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    LinkButton lnkReceivePayment = e.Row.FindControl("lnkReceivePayment") as LinkButton;
                    DataRowView rowView = (DataRowView)e.Row.DataItem;
                    bool IsPaid = gvResult.DataKeys[e.Row.RowIndex].Values["isPaid"].ToString() == "True" ? true : false;
                    double TotalBalance = Convert.ToDouble(gvResult.DataKeys[e.Row.RowIndex].Values["TotalBalance"]);
                    //if (IsPaid == false)
                    if (TotalBalance > 0)
                    {
                        //e.Row.BackColor = Color.LightPink;
                        Int64 CreditLimit = gvResult.DataKeys[e.Row.RowIndex].Values["CreditLimit"].ToString() != string.Empty ? Convert.ToInt64(gvResult.DataKeys[e.Row.RowIndex].Values["CreditLimit"]) : 0;
                        if (CreditLimit > 0)
                        {
                            DateTime InvoicedDate = DateTime.ParseExact(e.Row.Cells[5].Text, "dd/MM/yyyy", null);
                            DateTime TodaysDate = DateTime.Now;
                            double DateDifference = (TodaysDate - InvoicedDate).TotalDays;
                            if (DateDifference > CreditLimit)
                            {
                                e.Row.BackColor = Color.LightPink;
                            }
                            lnkReceivePayment.ForeColor = Color.Red;
                        }
                        else
                        {
                            lnkReceivePayment.ForeColor = Color.Red;
                        }
                    }
                    else
                    {
                        lnkReceivePayment.Enabled = false;
                        lnkReceivePayment.ToolTip = "Payment completed";
                    }

                }
                catch (Exception ex)
                {

                    notification("Error", "Error binding bills, due to: " + ex.Message);
                }
            }
        }

        #endregion

        #region Misc

        protected void rbPaymentMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtAmount.Enabled = true;
                //DocumentNoPlaceholder.Visible = rbPaymentMode.SelectedItem.Text == "Cheque" ? true : false;

                if (rbPaymentMode.SelectedItem.Text == "Cheque")
                {
                    DocumentNoPlaceholder.Visible = true;
                }
                else
                {
                    DocumentNoPlaceholder.Visible = false;
                    txtDocumentNo.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                PaymentNotification("Error", "Error selecting payment mode, due to: " + ex.Message);
            }
            finally
            {
                modalPayment.Show();
            }
        }

        protected void cbPettyCash_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //bankAccountsPlaceholder.Visible = cbPettyCash.Checked ? false : true;
                if (cbPettyCash.Checked)
                {
                    bankAccountsPlaceholder.Visible = false;
                    ddlBankAccounts.ClearSelection();
                }
                else
                {
                    bankAccountsPlaceholder.Visible = true;
                }
            }
            catch (Exception ex)
            {
                PaymentNotification("Error", "Error selecting petty cash, due to: " + ex.Message);
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