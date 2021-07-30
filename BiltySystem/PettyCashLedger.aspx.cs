using BLL;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiltySystem
{
    public partial class PettyCashLedger : System.Web.UI.Page
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
                    this.Title = "Petty Cash Ledger";
                    GetBindPettyCash();
                }
            }
        }

        #endregion

        #region Helper Methods

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

        public void TransactionNotification()
        {
            try
            {
                divTransactionNotification.InnerHtml = string.Empty;
            }
            catch (Exception ex)
            {
                divTransactionNotification.InnerHtml = "<div class=\"alert alert-danger\">" + ex.Message + "</div>";
            }
        }

        public void TransactionNotification(string type, string msg)
        {
            try
            {
                if (type == "Error")
                {
                    divTransactionNotification.InnerHtml = "<div class=\"alert alert-danger\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                }
                else if (type == "Success")
                {
                    divTransactionNotification.InnerHtml = "<div class=\"alert alert-success\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                }
                else if (type == string.Empty)
                {
                    divTransactionNotification.InnerHtml = string.Empty;
                }
            }
            catch (Exception ex)
            {
                divTransactionNotification.InnerHtml = "<div class=\"alert alert-danger\">" + ex.Message + "</div>";
            }
        }

        public void GetBindPettyCash()
        {
            try
            {
                AccountsDML dml = new AccountsDML();
                DataTable dtPettyCashAccountCheck = dml.GetAccounts("PettyCash|PC123");
                if (dtPettyCashAccountCheck.Rows.Count > 0)
                {
                    DataTable dtPettyCash = dml.GetInAccounts("PettyCash|PC123", "DESC");
                    if (dtPettyCash.Rows.Count > 0)
                    {
                        gvResult.DataSource = dtPettyCash;
                    }
                    else
                    {
                        gvResult.DataSource = null;
                    }
                }
                else
                {
                    gvResult.DataSource = null;
                }
                gvResult.DataBind();
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting/binding Petty Cash, due to: " + ex.Message);
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

        //public void GetLedgerByAccName(string AccountName)
        //{
        //    try
        //    {
        //        BankAccountsDML dmlAccounts = new BankAccountsDML();
        //        DataTable dtAccountCheck = dmlAccounts.GetAccounts(AccountName);
        //        if (dtAccountCheck.Rows.Count > 0)
        //        {
        //            DataTable dtAccounts = dmlAccounts.GetInAccounts(AccountName, "DESC");
        //            if (dtAccounts.Rows.Count > 0)
        //            {
        //                gvResult.DataSource = dtAccounts;
        //                lblSingleLedgerAccountName.Text = AccountName;

        //                AllAcounts.Visible = false;
        //                MainLedger.Visible = true;
        //            }
        //            else
        //            {
        //                gvResult.DataSource = null;
        //                gvResult.EmptyDataText = "No account found";
        //            }
        //        }
        //        else
        //        {
        //            gvResult.DataSource = null;
        //            gvResult.EmptyDataText = "No account found";
        //        }
        //        gvResult.DataBind();
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        public void TransactionClearFields()
        {
            try
            {
                rbTransactionType.ClearSelection();
                rbTransactionMode.ClearSelection();
                txtTransactionAmount.Text = string.Empty;
                txtTransactionDocumentNo.Text = string.Empty;
                txtTransactedBy.Text = string.Empty;
                txtTransactedFor.Text = string.Empty;
            }
            catch (Exception ex)
            {
                TransactionNotification("Error", "Cannot clear fields, due to: " + ex.Message);
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

        public void CustomerTransaction(Int64 CustomerCompanyID, double Amount, string DebitCreditType, string EntryDescription)
        {
            CompanyDML dmlComp = new CompanyDML();
            DataTable dtCompany = dmlComp.GetCompany(CustomerCompanyID);
            if (dtCompany.Rows.Count > 0)
            {
                string CustomerAccName = dtCompany.Rows[0]["CompanyName"].ToString() + "|" + dtCompany.Rows[0]["CompanyCode"].ToString();
                double CustomerAccountBalance = GetCustomerAccountBalance(CustomerAccName);

                double Debit = DebitCreditType == "Debit" ? Amount : 0;
                double Credit = DebitCreditType == "Credit" ? Amount : 0;
                double Balance = CustomerAccountBalance + Debit - Credit;

                AccountsDML dmlAcc = new AccountsDML();
                dmlAcc.InsertInAccount(CustomerAccName, CustomerCompanyID, EntryDescription, Debit, Credit, Balance, LoginID);
            }
        }

        public double GetCustomerAccountBalance(string CustomerAccName)
        {
            try
            {
                AccountsDML dmlAcc = new AccountsDML();
                DataTable dtCustAccountCheck = dmlAcc.GetAccounts(CustomerAccName);
                if (dtCustAccountCheck.Rows.Count <= 0)
                    dmlAcc.CreateAccount(CustomerAccName);
                DataTable dtCustAccount = dmlAcc.GetInAccounts(CustomerAccName);
                if (dtCustAccount.Rows.Count > 0)
                {
                    return Convert.ToDouble(dtCustAccount.Rows[dtCustAccount.Rows.Count - 1]["Balance"]);
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        #endregion

        #region Events

        protected void lnkSearchPettyCashAccount_Click(object sender, EventArgs e)
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
                    AccountsDML dml = new AccountsDML();
                    DataTable dtPettyCashAccountCheck = dml.GetAccounts("PettyCash|PC123");
                    if (dtPettyCashAccountCheck.Rows.Count > 0)
                    {
                        DataTable dtAccount = dml.GetInAccounts("PettyCash|PC123", FromDate, ToDate);
                        if (dtAccount.Rows.Count > 0)
                        {
                            gvResult.DataSource = dtAccount;
                        }
                        else
                        {
                            gvResult.DataSource = null;
                        }
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
                notification("Error", "Error searching Petty Cash, due to: " + ex.Message);
            }
        }

        protected void lnkCancelSearchPettyCashAccount_Click(object sender, EventArgs e)
        {
            try
            {
                txtFrom.Text = string.Empty;
                txtTo.Text = string.Empty;

                GetBindPettyCash();
            }
            catch (Exception ex)
            {
                notification("Error", "Error cancelling search, due to: " + ex.Message);
            }
        }

        protected void lnkCloseTransaction_Click(object sender, EventArgs e)
        {

        }

        protected void lnkAddTransaction_Click(object sender, EventArgs e)
        {
            try
            {
                modalTransaction.Show();
            }
            catch (Exception ex)
            {
                notification("Error", "Error enabling transaction mpanel, due to: " + ex.Message);
            }
        }

        protected void lnkSaveTransaction_Click(object sender, EventArgs e)
        {
            if (rbTransactionType.SelectedIndex == -1)
            {
                TransactionNotification("Error", "Please select transaction type");
                rbTransactionType.Focus();
                modalTransaction.Show();
            }
            else if (txtTransactionAmount.Text == string.Empty || Convert.ToDouble(txtTransactionAmount.Text) == 0)
            {
                TransactionNotification("Error", "Please enter valid amount to transact");
                txtTransactionAmount.Focus();
                modalTransaction.Show();
            }
            else if (rbTransactionMode.SelectedIndex == -1)
            {
                TransactionNotification("Error", "Please select transaction mode");
                rbTransactionMode.Focus();
                modalTransaction.Show();
            }
            else if (txtTransactionDocumentNo.Text == string.Empty || Convert.ToDouble(txtTransactionDocumentNo.Text) == 0)
            {
                TransactionNotification("Error", "Please enter valid document amount to transact");
                txtTransactionDocumentNo.Focus();
                modalTransaction.Show();
            }
            else
            {
                string TransactionType = rbTransactionType.SelectedItem.Text;
                double Amount = Convert.ToInt64(txtTransactionAmount.Text);
                string TransactionMode = rbTransactionMode.SelectedItem.Text;
                string PettyCashAccountName = "PettyCash|PC123";
                double DocumentNo = Convert.ToInt64(txtTransactionDocumentNo.Text);
                string TransactedBy = txtTransactedBy.Text.Trim();
                string TransactedFor = txtTransactedFor.Text.Trim();
                DateTime transactionDate = txtTransactionDate.Text != string.Empty ? DateTime.Parse(txtTransactionDate.Text) : DateTime.Now;

                string Description = "Rs. " + Amount + "/- " + TransactionType + "ed via " + TransactionMode + " No. " + DocumentNo;
                Description += TransactedBy != string.Empty ? " by " + TransactedBy : string.Empty;
                Description += TransactedFor != string.Empty ? " for " + TransactedFor : string.Empty;

                string[] PettyCashAccName = PettyCashAccountName.Split('|');
                string PettyCashName = PettyCashAccName[0].ToString();
                string BankCode = PettyCashAccName[1].ToString();
                BanksDML dmlBanks = new BanksDML();

                double Debit = TransactionType == "Deposit" ? Amount : 0;
                double Credit = TransactionType == "Withdraw" ? Amount : 0;
                double Balance = 0;

                AccountsDML dmlAccounts = new AccountsDML();
                GetSetPettyCashAccount(PettyCashAccountName);
                DataTable dtLedger = dmlAccounts.GetInAccounts(PettyCashAccountName);
                if (dtLedger.Rows.Count > 0)
                {
                    Balance = Convert.ToDouble(dtLedger.Rows[dtLedger.Rows.Count - 1]["Balance"]);
                }
                Balance = Balance + Debit - Credit;

                Int64 AccountID = dmlAccounts.InsertInPettyCashAccount(PettyCashAccountName, Description, Debit, Credit, Balance, TransactedBy, TransactedFor, transactionDate, LoginID);
                if (AccountID > 0)
                {
                    GetBindPettyCash();
                    TransactionClearFields();
                }
                else
                {
                    TransactionNotification("Error", "Error recording transaction, Tray Again!!!");
                    modalTransaction.Show();
                }
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
                    rvPettyCash.LocalReport.DataSources.Add(new ReportDataSource("PettyCashDataSet", dtSingleAccounts));
                    rvPettyCash.LocalReport.ReportPath = Server.MapPath("~/PrintView/PettyCashPrintView.rdlc");
                    rvPettyCash.LocalReport.EnableHyperlinks = true;

                    pnlPrintSingleLedger.Attributes.Add("style", "display:block");
                    MainLedger.Attributes.Add("style", "display:none");
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error printing single accounts, due to: " + ex.Message);
            }
        }

        #endregion
    }
}