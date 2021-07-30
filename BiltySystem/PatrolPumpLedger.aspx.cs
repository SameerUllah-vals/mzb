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
    public partial class PatrolPumpLedger : System.Web.UI.Page
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
            TransactionNotification();
            if (!IsPostBack)
            {
                if (LoginID > 0)
                {
                    this.Title = "Patrol Pumps Ledgers";

                    GetBindPatrolPumpsAccounts();
                    GetAndPopulatePatrolPumps();
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
            PatrolPumpDML dmlOrder = new PatrolPumpDML();
            DataTable dtPatrolPumps = dmlOrder.GetPatrolPumps(prefixText, "ASC");
            if (dtPatrolPumps.Rows.Count > 0)
            {
                for (int i = 0; i < dtPatrolPumps.Rows.Count; i++)
                {
                    customers.Add(dtPatrolPumps.Rows[i]["PatrolPump"].ToString());
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

        public void VehicleFuelNotification()
        {
            try
            {
                divVehicleFuelNotification.InnerHtml = string.Empty;
            }
            catch (Exception ex)
            {
                divVehicleFuelNotification.InnerHtml = "<div class=\"alert alert-danger\">" + ex.Message + "</div>";
            }
        }

        public void VehicleFuelNotification(string type, string msg)
        {
            try
            {
                if (type == "Error")
                {
                    divVehicleFuelNotification.InnerHtml = "<div class=\"alert alert-danger\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                }
                else if (type == "Success")
                {
                    divVehicleFuelNotification.InnerHtml = "<div class=\"alert alert-success\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                }
                else if (type == string.Empty)
                {
                    divVehicleFuelNotification.InnerHtml = string.Empty;
                }
            }
            catch (Exception ex)
            {
                divVehicleFuelNotification.InnerHtml = "<div class=\"alert alert-danger\">" + ex.Message + "</div>";
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

        public void GetBindPatrolPumpsAccounts() 
        {
            try
            {
                AccountsDML dml = new AccountsDML();
                DataTable dtPatrolPumpAccounts = dml.GetPatrolPumpAccounts();
                if (dtPatrolPumpAccounts.Rows.Count > 0)
                {
                    gvAllPatrolPumpsAcounts.DataSource = dtPatrolPumpAccounts;
                }
                else
                {
                    gvAllPatrolPumpsAcounts.DataSource = null;
                }
                gvAllPatrolPumpsAcounts.DataBind();
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting/binding Patrol Pump accounts, due to: " + ex.Message);
            }
        }

        public void GetBindAccountsLedger(string AccountName)
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
            catch (Exception ex)
            {
                notification("Error", "Error getting single account ledger, due to: " + ex.Message);
            }
        }

        public void GetSetPatrolPumpAccount(string PatrolPumpAccountName)
        {
            try
            {
                AccountsDML dmlAccounts = new AccountsDML();
                DataTable dtPatrolPumpAccountCheck = dmlAccounts.GetAccounts(PatrolPumpAccountName);
                if (dtPatrolPumpAccountCheck.Rows.Count <= 0)
                    dmlAccounts.CreatePatrolPumpAccount(PatrolPumpAccountName);
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting/setting patrol pump account, due to: " + ex.Message);
            }
        }

        public void GetAndPopulatePatrolPumps()
        {
            try
            {
                PatrolPumpDML dml = new PatrolPumpDML();
                DataTable dtPatrolPumps = dml.GetActivePatrolPumps();
                if (dtPatrolPumps.Rows.Count > 0)
                {
                    FillDropDown(dtPatrolPumps, ddlPatrolPump, "PatrolPumpID", "PatrolPumpAccount", "-Select-");
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting/binding Patrol Pump accounts, due to: " + ex.Message);
            }
        }

        #endregion

        #region Events

        #region Linkbutton's Events

        protected void lnkAllLedgers_Click(object sender, EventArgs e)
        {
            try
            {
                MainLedger.Visible = false;
                AllAcounts.Visible = true;
                hfSelectedAccount.Value = string.Empty;
            }
            catch (Exception ex)
            {
                notification("Error", "Error showing all ledgers, due to: " + ex.Message);

            }
        }

        protected void lnkCancelSearchSingleAccount_Click(object sender, EventArgs e)
        {
            try
            {
                txtFrom.Text = string.Empty;
                txtTo.Text = string.Empty;
                string AccountName = hfSelectedAccount.Value;
                GetBindAccountsLedger(AccountName);
            }
            catch (Exception ex)
            {
                notification("Error", "Error cancelling search single account, due to: " + ex.Message);
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

        protected void lnkPayment_Click(object sender, EventArgs e)
        {

        }

        protected void lnkCancelSearch_Click(object sender, EventArgs e)
        {
            try
            {
                txtSearch.Text = string.Empty;
                GetBindPatrolPumpsAccounts();
            }
            catch (Exception ex)
            {
                notification("Error", "Error cancelling search, due to: " + ex.Message);
            }
        }

        protected void lnkSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSearch.Text != string.Empty)
                {
                    string[] Keyword = txtSearch.Text.Split('|');
                    string SearchKeyword = Keyword[1].Trim() + "|" + Keyword[0].Trim();
                    AccountsDML dmlAccounts = new AccountsDML();
                    DataTable dtAccountCheck = dmlAccounts.GetPatrolPumpAccounts(Keyword[1].Trim() + "|" + Keyword[0].Trim());
                    if (dtAccountCheck.Rows.Count > 0)
                    {
                        gvAllPatrolPumpsAcounts.DataSource = dtAccountCheck;
                    }
                    else
                    {
                        gvAllPatrolPumpsAcounts.DataSource = null;
                        gvAllPatrolPumpsAcounts.EmptyDataText = "No account found";
                    }
                }
                else
                {
                    gvAllPatrolPumpsAcounts.DataSource = null;
                    gvAllPatrolPumpsAcounts.EmptyDataText = "No keyword for Search";
                }
                gvAllPatrolPumpsAcounts.DataBind();
            }
            catch (Exception ex)
            {
                notification("Error", "Error seaching, due to: " + ex.Message);
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
                else
                {
                    string TransactionType = rbTransactionType.SelectedItem.Text;
                    double Amount = Convert.ToInt64(txtTransactionAmount.Text);
                    string TransactionMode = rbTransactionMode.SelectedItem.Text;
                    string PatrolPumpAccountName = lblSingleLedgerAccountName.Text;
                    double DocumentNo = Convert.ToInt64(txtTransactionDocumentNo.Text);
                    string TransactedBy = txtTransactedBy.Text.Trim();
                    string TransactedFor = txtTransactedFor.Text.Trim();
                    string Description = "Rs. " + Amount + "/- " + TransactionType + "ed via " + TransactionMode + " No. " + DocumentNo;
                    Description += TransactedBy != string.Empty ? " by " + TransactedBy : string.Empty;
                    Description += TransactedFor != string.Empty ? " for " + TransactedFor : string.Empty;

                    string[] PatrolPumpAccName = PatrolPumpAccountName.Split('|');
                    string PatrolPumpName = PatrolPumpAccName[0].ToString();
                    string PatrolPumpCode = PatrolPumpAccName[1].ToString();
                    //BanksDML dmlBanks = new BanksDML();
                    //DataTable dtBank = dmlBanks.GetBankByName(BankName);

                    PatrolPumpDML dmlPatrolPump = new PatrolPumpDML();
                    DataTable dtPatrolPump = dmlPatrolPump.GetActivePatrolPumpsByName(PatrolPumpName);
                    Int64 PatrolPumpID = dtPatrolPump.Rows.Count > 0 ? Convert.ToInt64(dtPatrolPump.Rows[0]["PatrolPumpID"]) : 0;
                    double Debit = TransactionType == "Deposit" ? Amount : 0;
                    double Credit = TransactionType == "Withdraw" ? Amount : 0;
                    double Balance = 0;

                    AccountsDML dmlAccounts = new AccountsDML();
                    DataTable dtLedger = dmlAccounts.GetInAccounts(PatrolPumpAccountName, "DESC");
                    if (dtLedger.Rows.Count > 0)
                    {
                        Balance = Convert.ToDouble(dtLedger.Rows[0]["Balance"]);
                    }
                    Balance = Balance + Debit - Credit;

                    Int64 AccountID = dmlAccounts.InsertInPatrolPumpAccount(PatrolPumpAccountName, PatrolPumpID, Description, Debit, Credit, Balance, TransactedBy, TransactedFor, LoginID);
                    if (AccountID > 0)
                    {
                        GetBindAccountsLedger(PatrolPumpAccountName);
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

        protected void lnkSavePatrolPumpLedger_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlPatrolPump.SelectedIndex == 0)
                {
                    AddLedgerNotification("Error", "Please select any Patrol Pump");
                    ddlPatrolPump.Focus();
                }
                else if (txtOpeningDate.Text == string.Empty)
                {
                    AddLedgerNotification("Error", "Please select opening date");
                    txtOpeningDate.Focus();
                }
                else
                {
                    Int64 PatrolPumpID = Convert.ToInt64(ddlPatrolPump.SelectedItem.Value);
                    PatrolPumpDML dmlPatrolPump = new PatrolPumpDML();
                    DataTable dtBank = dmlPatrolPump.GetPatrolPump(PatrolPumpID, "ASC");
                    if (dtBank.Rows.Count > 0)
                    {
                        //string OpeningDate = txtOpeningDate.Text;
                        string Name = dtBank.Rows[0]["Name"].ToString();
                        string Code = dtBank.Rows[0]["Code"].ToString();
                        string PatrolPumpAccountName = Name + "|" + Code;
                        double OpeningBalance = txtOpeningBalance.Text == string.Empty ? 0 : Convert.ToDouble(txtOpeningBalance.Text);
                        string OpeningDate = txtOpeningDate.Text;
                        AccountsDML dmlAccounts = new AccountsDML();
                        DataTable dtBankAccounts = dmlAccounts.GetAccounts(PatrolPumpAccountName);
                        if (dtBankAccounts.Rows.Count > 0)
                        {
                            AddLedgerNotification("Error", "Patrol Pump account with same name already exists, Try to change Patrol Pump account");
                        }
                        else
                        {
                            dmlAccounts.CreatePatrolPumpAccount(PatrolPumpAccountName);
                            Int64 EntryID = dmlAccounts.InsertInPatrolPumpAccount(PatrolPumpAccountName, PatrolPumpID, "Account created", OpeningBalance, OpeningDate, LoginID);
                            if (EntryID > 0)
                            {
                                ddlPatrolPump.ClearSelection();
                                txtOpeningBalance.Text = string.Empty;
                                AddLedgerNotification("Success", "Patrolpump account created" + (OpeningBalance > 0 ? " with opening balance of Rs. " + OpeningBalance.ToString() + "/-" : string.Empty));
                                GetBindPatrolPumpsAccounts();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AddLedgerNotification("Error", "Error saving Patrolpump account, due to: " + ex.Message);
            }
            finally
            {
                modalAddLedger.Show();
            }
        }

        protected void lnkCloseVehicleFuels_Click(object sender, EventArgs e)
        {

        }

        protected void lnkSaveVehicleFuel_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtRefuellingDate.Text == string.Empty)
                {
                    VehicleFuelNotification("Please enter refuelling date", "");
                    modalVehicleFuel.Show();
                    txtRefuellingDate.Focus();
                }
                else if (txtVehicleRegNo.Text == string.Empty)
                {
                    VehicleFuelNotification("Please enter Vehicle Reg. No. ", "");
                    modalVehicleFuel.Show();
                    txtVehicleRegNo.Focus();
                }
                else if (txtAmount.Text == string.Empty)
                {
                    VehicleFuelNotification("Please enter Vehicle Reg. No. ", "");
                    modalVehicleFuel.Show();
                    txtAmount.Focus();
                }
                else
                {
                    string PatrolPumpAccountName = lblSingleLedgerAccountName.Text;
                    string[] PatrolPumpAccountNameString = lblSingleLedgerAccountName.Text.Split('|');
                    string PatrolPumpName = PatrolPumpAccountNameString[0].ToString();
                    string PatrolPumpCode = PatrolPumpAccountNameString[1].ToString();
                    Int64 PatrolPumpID = 0;
                    string RefuellingDate = txtRefuellingDate.Text;
                    string VehicleRegNo = txtVehicleRegNo.Text;
                    string TransactionBy = txtVehicleFuelBy.Text;
                    string TransactionFor = txtVehicleFuelFor.Text;
                    double Rate = txtRatePerLitre.Text.Trim() == string.Empty ? 0 : Convert.ToDouble(txtRatePerLitre.Text);
                    double Amount = Convert.ToDouble(txtAmount.Text);
                    double Litres = txtLitres.Text.Trim() == string.Empty ? 0 : Convert.ToDouble(txtLitres.Text);
                    //"Refuelling 19 Litres in JZ-9093 @ Rs. 190/Ltr on 25-Nov-2019";
                    string Description = "Refuelling ";
                    Description += txtLitres.Text == string.Empty ? string.Empty : (Litres + " Litres ");
                    Description += "in " + VehicleRegNo + " ";
                    Description += "@ Rs. " + Rate + "/Ltr ";
                    Description += "on " + RefuellingDate;
                    Description += TransactionBy == string.Empty ? string.Empty : " by " + TransactionBy;
                    Description += TransactionFor == string.Empty ? string.Empty : " for " + TransactionFor;

                    GetSetPatrolPumpAccount(PatrolPumpAccountName);
                    double Balance = 0;

                    AccountsDML dml = new AccountsDML();
                    DataTable dtLedger = dml.GetInAccounts(PatrolPumpAccountName, "DESC");
                    if (dtLedger.Rows.Count > 0)
                    {
                        Balance = Convert.ToDouble(dtLedger.Rows[0]["Balance"]);
                    }
                    Balance = Balance + Amount - 0;
                    PatrolPumpDML dmlPP = new PatrolPumpDML();
                    DataTable dtPP = dmlPP.GetActivePatrolPumpsByName(PatrolPumpName);
                    if (dtPP.Rows.Count > 0)
                    {
                        PatrolPumpID = Convert.ToInt64(dtPP.Rows[0]["PatrolPumpID"]);
                        Int64 TransactionID = dml.InsertInPatrolPumpAccount(PatrolPumpAccountName, PatrolPumpID, Description, Amount, 0, Balance, RefuellingDate, TransactionBy, TransactionFor, LoginID);
                        if (TransactionID > 0)
                        {
                            VehicleFuelNotification("Success", "Vehicle refuelling recorded successfully.");
                            txtRefuellingDate.Text = string.Empty;
                            txtVehicleRegNo.Text = string.Empty;
                            txtRatePerLitre.Text = string.Empty;
                            txtAmount.Text = string.Empty;
                            txtLitres.Text = string.Empty;
                            GetBindAccountsLedger(PatrolPumpAccountName);
                            modalVehicleFuel.Show();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                VehicleFuelNotification("Error", "Error saving vehicle fuel, due to: " + ex.Message);
                modalVehicleFuel.Show();
            }
        }

        protected void lnkAddVehicleFuel_Click(object sender, EventArgs e)
        {
            try
            {
                modalVehicleFuel.Show();
            }
            catch (Exception ex)
            {
                notification("Error", "Error adding vehicle fuel, due to: " + ex.Message);
            }
        }

        #endregion

        #region Radiobutton's Events
        protected void rbVendor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        #endregion

        #region Gridview's Events

        protected void gvResult_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView rowView = (DataRowView)e.Row.DataItem;
                    string CreatedDate = rowView["DateCreated"].ToString();
                    string RecordedDate = rowView["RecordedDate"].ToString();
                    string TempDate = RecordedDate.Replace("&nbsp;", string.Empty) == string.Empty ? CreatedDate : RecordedDate;
                    string[] DateTime = TempDate.Split(' ');
                    string Date = DateTime[0];
                    Label lblDate = e.Row.FindControl("lblDate") as Label;
                    lblDate.Text = Date;

                    string sDebit = e.Row.Cells[3].Text.Replace("&nbsp;", string.Empty);
                    string sCredit = e.Row.Cells[4].Text.Replace("&nbsp;", string.Empty);
                    double Debit = 0;
                    double Credit = 0;
                    if (sDebit != string.Empty)
                    {
                        Debit = Convert.ToDouble(sDebit);
                    }
                    if (sCredit != string.Empty)
                    {
                        Credit = Convert.ToDouble(sCredit);
                    }



                    double Balance = Convert.ToDouble(e.Row.Cells[5].Text);
                    e.Row.Cells[3].Text = String.Format("{0:n}", Debit);
                    e.Row.Cells[4].Text = String.Format("{0:n}", Credit);
                    e.Row.Cells[5].Text = String.Format("{0:n}", Balance);
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error binding Patrol pump ledger, due to: " + ex.Message);
            }
        }

        protected void gvAllPatrolPumpsAcounts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
                {
                    DataRowView rowView = (DataRowView)e.Row.DataItem;
                    Int64 PatrolPumpID = Convert.ToInt64(rowView["PatrolPumpID"]);
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

        protected void gvAllPatrolPumpsAcounts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Account")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvAllPatrolPumpsAcounts.Rows[index];

                    LinkButton lnkAccount = gvr.FindControl("lnkAccount") as LinkButton;
                    
                    string AccountName = lnkAccount.Text;
                    hfSelectedAccount.Value = AccountName;
                    GetBindAccountsLedger(AccountName);
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error commanding row, due to: " + ex.Message);
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
               

                DataTable dtAllAccounts = AllAccountsGridToDT(gvAllPatrolPumpsAcounts, "All");
                if (dtAllAccounts.Rows.Count > 0)
                {

                    rvPetrolPump.LocalReport.DataSources.Add(new ReportDataSource("PetrolPumpLedgerDataSet", dtAllAccounts));
                    rvPetrolPump.LocalReport.ReportPath = Server.MapPath("~/PrintView/PetrolPumpLedgerPrintVIew.rdlc");
                    rvPetrolPump.LocalReport.EnableHyperlinks = true;

                    AllAcounts.Attributes.Add("style","display:none");
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
                AllAcounts.Attributes.Add("style", "display:block");
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
                MainLedger.Attributes.Add("style", "display:block");
                pnlPrintSingleLedger.Attributes.Add("style", "display:none");
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
                DataTable dtSingleAccounts = new DataTable();
                dtSingleAccounts.Columns.Add("Date");
                dtSingleAccounts.Columns.Add("Item");
                dtSingleAccounts.Columns.Add("Debit");
                dtSingleAccounts.Columns.Add("Credit");
                dtSingleAccounts.Columns.Add("Balance");
                dtSingleAccounts.Columns.Add("AccountID");
                for (int i = 0; i < gvResult.Rows.Count; i++)
                {
                    Int64 AccountID = Convert.ToInt64(gvResult.DataKeys[i]["AccountID"]);
                    Label Date = gvResult.Rows[i].Cells[1].FindControl("lblDate") as Label;
                    string Item = gvResult.Rows[i].Cells[2].Text.Trim();
                    string Debit = gvResult.Rows[i].Cells[3].Text.Trim();
                    string Credit = gvResult.Rows[i].Cells[4].Text.Trim();
                    string Balance = gvResult.Rows[i].Cells[5].Text.Trim();
                    dtSingleAccounts.Rows.Add(Date.Text.Trim(), Item, Debit, Credit, Balance, AccountID);
                }
                if (dtSingleAccounts.Rows.Count > 0)
                {

                    rvAllPetrolPumpLedger.LocalReport.DataSources.Add(new ReportDataSource("AllPetrolPumpLedgerDataSet", dtSingleAccounts));
                    rvAllPetrolPumpLedger.LocalReport.ReportPath = Server.MapPath("~/PrintView/AllPetrolPumpLedgerPrintView.rdlc");
                    rvAllPetrolPumpLedger.LocalReport.EnableHyperlinks = true;
                }
                MainLedger.Attributes.Add("style", "display:none");
                pnlPrintSingleLedger.Attributes.Add("style", "display:block");
            }
            catch (Exception ex)
            {
                notification("Error", "Error printing single accounts, due to: " + ex.Message);
            }
        }
    }
}