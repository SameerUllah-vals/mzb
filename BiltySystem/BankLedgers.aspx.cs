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
    public partial class BankLedgers : System.Web.UI.Page
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
                    this.Title = "Banks Ledgers";

                    GetBankLedgers();
                    GetAndPopulateBanks();
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

        public void AddLedgerNotification()
        {
            try
            {
                divAddLedgerNotification.InnerHtml = string.Empty;
            }
            catch (Exception ex)
            {
                divAddLedgerNotification.InnerHtml = "<div class=\"alert alert-danger\">" + ex.Message + "</div>";
            }
        }

        public void AddLedgerNotification(string type, string msg)
        {
            try
            {
                if (type == "Error")
                {
                    divAddLedgerNotification.InnerHtml = "<div class=\"alert alert-danger\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                }
                else if (type == "Success")
                {
                    divAddLedgerNotification.InnerHtml = "<div class=\"alert alert-success\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                }
                else if (type == string.Empty)
                {
                    divAddLedgerNotification.InnerHtml = string.Empty;
                }
            }
            catch (Exception ex)
            {
                divAddLedgerNotification.InnerHtml = "<div class=\"alert alert-danger\">" + ex.Message + "</div>";
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

        public void GetBankLedgers()
        {
            try
            {
                BankAccountsDML dml = new BankAccountsDML();
                DataTable dtBankAccounts = dml.GetBankAccounts();
                if (dtBankAccounts.Rows.Count > 0)
                {
                    gvAllBanksAcounts.DataSource = dtBankAccounts;
                }
                else
                {
                    gvAllBanksAcounts.DataSource = null;
                }
                gvAllBanksAcounts.DataBind();
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting/binding banks accounts, due to: " + ex.Message);
            }
        }

        public void GetAndPopulateBanks()
        {
            try
            {
                BanksDML dml = new BanksDML();
                DataTable dtBanks = dml.GetBank();
                if (dtBanks.Rows.Count > 0)
                {
                    FillDropDown(dtBanks, ddlBanks, "BankID", "BankAccount", "Select Bank");
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting/binding banks accounts, due to: " + ex.Message);
            }
        }

        public void GetLedgerByAccName(string AccountName)
        {
            try
            {
                BankAccountsDML dmlAccounts = new BankAccountsDML();
                DataTable dtAccountCheck = dmlAccounts.GetAccounts(AccountName);
                if (dtAccountCheck.Rows.Count > 0)
                {
                    DataTable dtAccounts = dmlAccounts.GetInAccounts(AccountName, "DESC");
                    if (dtAccounts.Rows.Count > 0)
                    {
                        gvResult.DataSource = dtAccounts;
                        lblSingleLedgerAccountName.Text = AccountName;

                        PnlAllAcounts.Visible = false;
                        PnlMainLedger.Visible = true;
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
            catch (Exception)
            {

                throw;
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

        #endregion

        #region Events

        #region Linkbutton's Events

        protected void lnkAllLedgers_Click(object sender, EventArgs e)
        {
            try
            {
                PnlMainLedger.Visible = false;
                PnlAllAcounts.Visible = true;
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
                pnlAllSearch.Visible = true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        protected void lnkCancelSearchSingleAccount_Click(object sender, EventArgs e)
        {

        }

        protected void lnkSearch_Click(object sender, EventArgs e)
        {

        }

        protected void lnkAddNewAccount_Click(object sender, EventArgs e)
        {
            try
            {
                modalAddLedger.Show();
            }
            catch (Exception ex)
            {
                notification("Error", "Error enabling add account panel, due to: " + ex.Message);
            }
        }

        protected void lnkCloseAddLedgers_Click(object sender, EventArgs e)
        {
            try
            {
                modalAddLedger.Hide();
            }
            catch (Exception ex)
            {
                modalAddLedger.Show();
                AddLedgerNotification("Error", "Error closing add ledger panel, due to: " + ex.Message);
            }
        }

        protected void lnkSaveBankLedger_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlBanks.SelectedIndex == 0)
                {
                    AddLedgerNotification("Error", "Please select any bank");
                    ddlBanks.Focus();
                }
                else if(txtRecordedDate.Text == string.Empty)
                {
                    AddLedgerNotification("Error", "Please enter date of trransaction");
                    txtRecordedDate.Focus();
                }
                else
                {
                    Int64 BankID = Convert.ToInt64(ddlBanks.SelectedItem.Value);
                    BanksDML dmlBank = new BanksDML();
                    DataTable dtBank = dmlBank.GetBank(BankID);
                    if (dtBank.Rows.Count > 0)
                    {
                        string Name = dtBank.Rows[0]["Name"].ToString();
                        string Code = dtBank.Rows[0]["Code"].ToString();
                        string RecordedDate = txtRecordedDate.Text;
                        string BankAccountName = Name + "|" + Code;
                        double OpeningBalance = txtOpeningBalance.Text == string.Empty ? 0 : Convert.ToDouble(txtOpeningBalance.Text);
                        BankAccountsDML dmlBankAccounts = new BankAccountsDML();
                        DataTable dtBankAccounts = dmlBankAccounts.GetAccounts(BankAccountName);
                        if (dtBankAccounts.Rows.Count > 0)
                        {
                            AddLedgerNotification("Error", "Bank account with same name already exists, Try to change bank account");
                        }
                        else
                        {
                            dmlBankAccounts.CreateAccount(BankAccountName);
                            Int64 EntryID = dmlBankAccounts.InsertInAccount(BankAccountName, BankID, "Account created", OpeningBalance, RecordedDate, LoginID);
                            if (EntryID > 0)
                            {
                                ddlBanks.ClearSelection();
                                txtOpeningBalance.Text = string.Empty;
                                AddLedgerNotification("Success", "Bank account created" + (OpeningBalance > 0 ? " with opening balance of Rs. " + OpeningBalance.ToString() + "/-" : string.Empty));
                                GetBankLedgers();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AddLedgerNotification("Error", "Error saving bank account, due to: " + ex.Message);
            }
            finally
            {
                modalAddLedger.Show();
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
                TransactionNotification("Error", "Error closing transaction panel, due to: " + ex.Message);
                modalTransaction.Show();
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

        protected void lnkSaveTransaction_Click(object sender, EventArgs e)
        {
            try
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
                    rbTransactionType.Focus();
                    modalTransaction.Show();
                }
                else if (txtTransactionDocumentNo.Text == string.Empty || Convert.ToDouble(txtTransactionDocumentNo.Text) == 0)
                {
                    TransactionNotification("Error", "Please enter valid document amount to transact");
                    txtTransactionDocumentNo.Focus();
                    modalTransaction.Show();
                }
                else if (txtTransactionRecordedDate.Text == string.Empty)
                {
                    TransactionNotification("Error", "Please enter date of transaction");
                    txtTransactionRecordedDate.Focus();
                    modalTransaction.Show();
                }
                else
                {
                    string TransactionType = rbTransactionType.SelectedItem.Text;
                    double Amount = Convert.ToInt64(txtTransactionAmount.Text);
                    string TransactionMode = rbTransactionMode.SelectedItem.Text;
                    string BankAccountName = lblSingleLedgerAccountName.Text;
                    double DocumentNo = Convert.ToInt64(txtTransactionDocumentNo.Text);
                    string TransactedBy = txtTransactedBy.Text.Trim();
                    string TransactedFor = txtTransactedFor.Text.Trim();
                    DateTime TransactionDate = DateTime.Parse(txtTransactionRecordedDate.Text.Trim());
                    string Description = "Rs. " + Amount + "/- " + TransactionType + "ed via " + TransactionMode + " No. " + DocumentNo;
                    Description += TransactedBy != string.Empty ? " by " + TransactedBy : string.Empty;
                    Description += TransactedFor != string.Empty ? " for " + TransactedFor : string.Empty;

                    string[] BankAccName = BankAccountName.Split('|');
                    string BankName = BankAccName[0].ToString();
                    string BankCode = BankAccName[1].ToString();
                    BanksDML dmlBanks = new BanksDML();
                    DataTable dtBank = dmlBanks.GetBankByName(BankName);
                    Int64 BankID = dtBank.Rows.Count > 0 ? Convert.ToInt64(dtBank.Rows[0]["BankID"]) : 0;
                    double Debit = TransactionType == "Deposit" ? Amount : 0;
                    double Credit = TransactionType == "Withdraw" ? Amount : 0;
                    double Balance = 0;

                    BankAccountsDML dmlBankAccounts = new BankAccountsDML();
                    DataTable dtLedger = dmlBankAccounts.GetInAccounts(BankAccountName, "DESC");
                    if (dtLedger.Rows.Count > 0)
                    {
                        Balance = Convert.ToDouble(dtLedger.Rows[0]["Balance"]);
                    }
                    Balance = Balance + Debit - Credit;

                    Int64 AccountID = dmlBankAccounts.InsertInAccount(BankAccountName, BankID, Description, Debit, Credit, Balance, TransactionDate, TransactedBy, TransactedFor, LoginID);
                    if (AccountID > 0)
                    {
                        GetLedgerByAccName(BankAccountName);
                        TransactionClearFields();
                    }
                    else
                    {
                        TransactionNotification("Error", "Error recording transaction, Tray Again!!!");
                        modalTransaction.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                TransactionNotification("Error", "Error saving transaction, due to: " + ex.Message);
                modalTransaction.Show();
            }
        }

        #endregion

        #region Gridview's Events

        protected void gvAllBankAcounts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Account")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvAllBanksAcounts.Rows[index];
                    
                    LinkButton lnkAccount = gvr.FindControl("lnkAccount") as LinkButton;
                    string AccountName = lnkAccount.Text;
                    BankAccountsDML dmlAccounts = new BankAccountsDML();
                    DataTable dtAccountCheck = dmlAccounts.GetAccounts(AccountName);
                    if (dtAccountCheck.Rows.Count > 0)
                    {
                        DataTable dtAccounts = dmlAccounts.GetInAccountsAlonBankInfo(AccountName, "DESC");
                        if (dtAccounts.Rows.Count > 0)
                        {
                            gvResult.DataSource = dtAccounts;
                            lblSingleLedgerAccountName.Text = AccountName;
                            lblAccountTitle.Text = dtAccounts.Rows[0]["AccountTitle"].ToString();
                            lblAccountNo.Text = dtAccounts.Rows[0]["AccountNo"].ToString();

                            PnlAllAcounts.Visible = false;
                            PnlMainLedger.Visible = true;
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
                    //GetLedgerByAccName(AccountName);
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error commanding row, due to: " + ex.Message);
            }
        }

        protected void gvAllBanksAcounts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
                {
                    DataRowView rowView = (DataRowView)e.Row.DataItem;
                    Int64 BankID = Convert.ToInt64(rowView["BankID"]);
                    LinkButton lnkAccount = e.Row.FindControl("lnkAccount") as LinkButton;
                    string AccountName = lnkAccount.Text;
                    BankAccountsDML dmlBank = new BankAccountsDML();
                    DataTable dtAccount = dmlBank.GetInAccounts(AccountName);

                    Label lblBalance = e.Row.FindControl("lblBalance") as Label;
                    if (dtAccount.Rows.Count > 0)
                    {
                        //lblBalance.Text = dtAccount.Rows[0]["Balance"].ToString();
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
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string[] DateTime = e.Row.Cells[GetColumnIndexByName(e.Row, "Date")].Text.Split(' ');
                    string Date = DateTime[0];
                    e.Row.Cells[GetColumnIndexByName(e.Row, "Date")].Text = Date;

                    double Debit = Convert.ToDouble(e.Row.Cells[GetColumnIndexByName(e.Row, "Debit")].Text.Replace("&nbsp;", "0"));
                    double Credit = Convert.ToDouble(e.Row.Cells[GetColumnIndexByName(e.Row, "Credit")].Text.Replace("&nbsp;", "0"));
                    double Balance = Convert.ToDouble(e.Row.Cells[GetColumnIndexByName(e.Row, "Balance")].Text.Replace("&nbsp;", "0"));

                    e.Row.Cells[GetColumnIndexByName(e.Row, "Debit")].Text = String.Format("{0:n}", Debit);
                    e.Row.Cells[GetColumnIndexByName(e.Row, "Credit")].Text = String.Format("{0:n}", Credit);
                    e.Row.Cells[GetColumnIndexByName(e.Row, "Balance")].Text = String.Format("{0:n}", Balance);
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error binding row, due to: " + ex.Message);
            }
        }

        #endregion

        #endregion



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

        protected void lnkPrintAllAccounts_Click(object sender, EventArgs e)
        {
            try
            {
                

                DataTable dtAllAccounts = AllAccountsGridToDT(gvAllBanksAcounts, "All");
                if (dtAllAccounts.Rows.Count > 0)
                {
                    rvBankLedger.LocalReport.DataSources.Add(new ReportDataSource("BankLedgerDataSet", dtAllAccounts));
                    rvBankLedger.LocalReport.ReportPath = Server.MapPath("~/PrintView/BankLedgerPrintView.rdlc");
                    rvBankLedger.LocalReport.EnableHyperlinks = true;

                    PnlAllAcounts.Attributes.Add("style","display:none");
                    pnlPrintAllAccounts.Attributes.Add("style", "display:block");
                }
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
                PnlAllAcounts.Attributes.Add("style", "display:block");
                pnlPrintAllAccounts.Attributes.Add("style", "display:none");
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
                pnlPrintSingleLedger.Attributes.Add("style","display:none");
                PnlMainLedger.Attributes.Add("style", "display:block");
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
                    rvAllBankLedger.LocalReport.DataSources.Add(new ReportDataSource("AllBankLedgerDataSet", dtSingleAccounts));
                    rvAllBankLedger.LocalReport.ReportPath = Server.MapPath("~/PrintView/AllBankLedgerPrintView.rdlc");
                    rvAllBankLedger.LocalReport.EnableHyperlinks = true;

                    pnlPrintSingleLedger.Attributes.Add("style", "display:block");
                    PnlMainLedger.Attributes.Add("style", "display:none");
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error printing single accounts, due to: " + ex.Message);
            }
        }

        protected void lnkAllSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtFrom.Text == string.Empty && txtTo.Text == string.Empty)
                {
                    notification("Error", "Please provide any date");
                    txtFrom.Focus();
                }
                else if (txtFrom.Text == string.Empty || txtTo.Text == string.Empty)
                {
                    if (txtFrom.Text == string.Empty)
                    {
                        notification("Error", "Please provide from date");
                        txtFrom.Focus();
                    }
                    else if (txtTo.Text == string.Empty)
                    {
                        notification("Error", "Please provide to date");
                        txtTo.Focus();
                    }
                }
                else if (txtFrom.Text != string.Empty && txtTo.Text != string.Empty && txtChequeNumber.Text != string.Empty)
                {
                    DateTime FromDate = Convert.ToDateTime(txtFrom.Text);
                    DateTime ToDate = Convert.ToDateTime(txtTo.Text);
                    string Item = txtChequeNumber.Text.Trim();
                    string AccountName = lblSingleLedgerAccountName.Text;
                    BankAccountsDML dML = new BankAccountsDML();

                    DataTable dtAccount = dML.GetInAccounts(AccountName, FromDate, ToDate, Item);
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

                else if (txtFrom.Text != string.Empty && txtTo.Text != string.Empty )
                {
                    DateTime FromDate = Convert.ToDateTime(txtFrom.Text);
                    DateTime ToDate = Convert.ToDateTime(txtTo.Text);
                    string Item = txtChequeNumber.Text.Trim();
                    string AccountName = lblSingleLedgerAccountName.Text;
                    BankAccountsDML dML = new BankAccountsDML();

                    DataTable dtAccount = dML.GetInAccounts(AccountName, FromDate, ToDate,Item);
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

        protected void lnkCloseSearch_Click(object sender, EventArgs e)
        {
            try
            {
                pnlAllSearch.Visible = false;
                txtFrom.Text = string.Empty;
                txtTo.Text = string.Empty;
                txtChequeNumber.Text = string.Empty;
                
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}