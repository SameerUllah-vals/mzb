using BLL;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;

namespace BiltySystem
{
    public partial class CustomerLedger : System.Web.UI.Page
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
                    this.Title = "Customer Ledger";

                    AccountsDML dml = new AccountsDML();
                    DataTable dtAccounts = dml.GetAccounts();
                    if (dtAccounts.Rows.Count > 0)
                    {
                        gvAllAcounts.DataSource = dtAccounts;
                    }
                    else
                    {
                        gvAllAcounts.DataSource = null;
                    }
                    gvAllAcounts.DataBind();
                }
            }
        }

        #endregion

        #region Helper Methods

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

        #endregion

        #region Events

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
                    AccountsDML dML = new AccountsDML();
                    //DataTable dtAccount = dML
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

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtReport = new DataTable();

                dtReport.Columns.Add("Account");
                dtReport.Columns.Add("Balance");

                for (int i = 0; i < gvAllAcounts.Rows.Count; i++)
                {
                    LinkButton lnkAccount = gvAllAcounts.Rows[i].Cells[0].FindControl("lnkAccount") as LinkButton;
                    Label lblBalance = gvAllAcounts.Rows[i].Cells[1].FindControl("lblBalance") as Label;
                    dtReport.Rows.Add(lnkAccount.Text.Trim(), lblBalance.Text.Trim());
                }

                if (dtReport.Rows.Count > 0)
                {
                    rvCustomerLedger.LocalReport.DataSources.Add(new ReportDataSource("CustomerAccountDataSet", dtReport));
                    rvCustomerLedger.LocalReport.ReportPath = Server.MapPath("~/PrintView/CustomerAccountPrintView.rdlc");
                    rvCustomerLedger.LocalReport.EnableHyperlinks = true;
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Cannot Print Due To: " + ex.Message);
            }
        }

        protected void lnkMainLedgerPring_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtReport = new DataTable();

                dtReport.Columns.Add("Date");
                dtReport.Columns.Add("Item");
                dtReport.Columns.Add("Debit");
                dtReport.Columns.Add("Credit");
                dtReport.Columns.Add("Balance");

                for (int i = 0; i < gvResult.Rows.Count; i++)
                {
                    string date = gvResult.Rows[i].Cells[0].Text.Trim();
                    string item = gvResult.Rows[i].Cells[1].Text.Trim();
                    string debit = gvResult.Rows[i].Cells[2].Text.Trim();
                    string credit = gvResult.Rows[i].Cells[3].Text.Trim();
                    string balance = gvResult.Rows[i].Cells[4].Text.Trim();

                    dtReport.Rows.Add(date, item, debit, credit, balance);
                }

                if (dtReport.Rows.Count > 0)
                {
                    rvMainCustomerLedger.LocalReport.DataSources.Add(new ReportDataSource("MainCustomerAccountDataSet", dtReport));
                    rvMainCustomerLedger.LocalReport.ReportPath = Server.MapPath("~/PrintView/MainCustomerAccountPrintView.rdlc");
                    rvMainCustomerLedger.LocalReport.EnableHyperlinks = true;
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Cannot Print Due To: " + ex.Message);
            }
        }

        protected void gvAllAcounts_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
                {
                    DataRowView rowView = (DataRowView)e.Row.DataItem;
                    Int64 CompanyID = Convert.ToInt64(rowView["CompanyID"]);
                    LinkButton lnkAccount = e.Row.FindControl("lnkAccount") as LinkButton;
                    string AccountName = lnkAccount.Text;
                    AccountsDML dmlComp = new AccountsDML();
                    DataTable dtAccount = dmlComp.GetInAccounts(AccountName);

                    Label lblBalance = e.Row.FindControl("lblBalance") as Label;
                    if (dtAccount.Rows.Count > 0)
                    {
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

        protected void gvAllAcounts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Account")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvAllAcounts.Rows[index];
                    //Int64 CompanyID = Convert.ToInt64(gvResult.DataKeys[index]["CompanyID"]);
                    LinkButton lnkAccount = gvr.FindControl("lnkAccount") as LinkButton;
                    string AccountName = lnkAccount.Text;
                    hfAccountName.Value = AccountName;
                    GetAccountDetails(AccountName);
                    AllAcounts.Visible = false;
                    MainLedger.Visible = true;
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error commanding row, due to: " + ex.Message);
            }
        }

        #endregion

        public void GetAccountDetails(string AccountName) 
        {
            try
            {
                AccountsDML dmlAccounts = new AccountsDML();
                DataTable dtAccountCheck = dmlAccounts.GetAccounts(AccountName);
                if (dtAccountCheck.Rows.Count > 0)
                {
                    DataTable dtAccounts = dmlAccounts.GetInAccounts(AccountName, "DESC");
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
                gvResult.DataBind();
            }
            catch (Exception ex)
            {
                notification("Error","Cannot get account details due to : " + ex.Message);
            }
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

        protected void lnkCloseTransaction_Click(object sender, EventArgs e)
        {
            try
            {
                modalTransaction.Hide();
                TransactionClearFields();
            }
            catch (Exception ex)
            {
                TransactionNotification("Error","Cannot close add transaction model due to : " + ex.Message );
            }
        }

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
                string CustomerAccountName = hfAccountName.Value;
                double DocumentNo = Convert.ToInt64(txtTransactionDocumentNo.Text);
                string TransactedBy = txtTransactedBy.Text.Trim();
                string TransactedFor = txtTransactedFor.Text.Trim();
                string Description = "Rs. " + Amount + "/- " + TransactionType + "ed via " + TransactionMode + " No. " + DocumentNo;
                Description += TransactedBy != string.Empty ? " by " + TransactedBy : string.Empty;
                Description += TransactedFor != string.Empty ? " for " + TransactedFor : string.Empty;

                string[] CustomerAccNameString = CustomerAccountName.Split('|');
                string CustomerAccName = CustomerAccNameString[0].ToString();
                string CustomerAccCode = CustomerAccNameString[1].ToString();

                CompanyDML dmlCompany = new CompanyDML();
                DataTable dtCompany = dmlCompany.GetCompany(CustomerAccCode,CustomerAccName);
                Int64 CompanyID = Convert.ToInt64(dtCompany.Rows[0]["CompanyID"]);

                double Debit = TransactionType == "Deposit" ? Amount : 0;
                double Credit = TransactionType == "Withdraw" ? Amount : 0;
                double Balance = 0;

                AccountsDML dmlAccounts = new AccountsDML();
                DataTable dtLedger = dmlAccounts.GetInAccounts(CustomerAccountName);
                if (dtLedger.Rows.Count > 0)
                {
                    Balance = Convert.ToDouble(dtLedger.Rows[dtLedger.Rows.Count - 1]["Balance"]);
                }
                Balance = Balance + Debit - Credit;

                Int64 AccountID = dmlAccounts.InsertInAccount(CustomerAccountName, CompanyID, Description, Debit, Credit, Balance, LoginID);
                if (AccountID > 0)
                {
                    GetAccountDetails(CustomerAccountName);
                    TransactionClearFields();
                    notification("Success","Transaction Created Successfully");
                }
                else
                {
                    TransactionNotification("Error", "Error recording transaction, Tray Again!!!");
                    modalTransaction.Show();
                }
            }
        }
    }
}