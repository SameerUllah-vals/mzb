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
    public partial class Vouchers : System.Web.UI.Page
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

        private string VouchersSortDirection
        {
            get { return ViewState["SortDirection"] != null ? ViewState["SortDirection"].ToString() : "ASC"; }
            set { ViewState["SortDirection"] = value; }
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

        #region Page Load

        protected void Page_Load(object sender, EventArgs e)
        {
            notification();
            PaymentNotification();
            ConfirmNotification();

            if (!IsPostBack)
            {
                if (LoginID > 0)
                {
                    this.Title = "Vouchers";

                    //Get Vouchers
                    GetVouchers("DESC");

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

                    //Getting/Binding Order advances for vouchers
                    //GetBindOrderAdvancesForVouchers();
                }
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

        public void ConfirmNotification()
        {
            try
            {
                divConfirmNotification.InnerHtml = string.Empty;
            }
            catch (Exception ex)
            {
                divConfirmNotification.InnerHtml = "<div class=\"alert alert-danger\">" + ex.Message + "</div>";
            }
        }

        public void ConfirmNotification(string type, string msg)
        {
            try
            {
                if (type == "Error")
                {
                    divConfirmNotification.InnerHtml = "<div class=\"alert alert-danger\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                }
                else if (type == "Success")
                {
                    divConfirmNotification.InnerHtml = "<div class=\"alert alert-success\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                }
                else if (type == string.Empty)
                {
                    divConfirmNotification.InnerHtml = string.Empty;
                }
            }
            catch (Exception ex)
            {
                divConfirmNotification.InnerHtml = "<div class=\"alert alert-danger\">" + ex.Message + "</div>";
            }
        }

        #endregion

        #region Translate Number to Words

        public String changeNumericToWords(double numb)
        {

            String num = numb.ToString(); return changeToWords(num, false);
        }

        public String changeCurrencyToWords(String numb)
        {

            return changeToWords(numb, true);
        }

        public String changeNumericToWords(String numb)
        {

            return changeToWords(numb, false);
        }

        public String changeCurrencyToWords(double numb)
        {

            return changeToWords(numb.ToString(), true);
        }

        private String changeToWords(String numb, bool isCurrency)
        {

            String val = "", wholeNo = numb, points = "", andStr = "", pointStr = "";

            String endStr = (isCurrency) ? ("Only") : ("");
            try

            {

                int decimalPlace = numb.IndexOf("."); if (decimalPlace > 0)
                {

                    wholeNo = numb.Substring(0, decimalPlace);

                    points = numb.Substring(decimalPlace + 1);

                    if (Convert.ToInt32(points) > 0)
                    {

                        andStr = (isCurrency) ? ("and") : ("point");// just to separate whole numbers from points/cents

                        endStr = (isCurrency) ? ("Cents " + endStr) : ("");
                        pointStr = translateCents(points);

                    }

                }

                val = String.Format("{0} {1}{2} {3}", translateWholeNumber(wholeNo).Trim(), andStr, pointStr, endStr);
            }

            catch {; }
            return val;
        }

        private String translateWholeNumber(String number)
        {

            string word = "";

            try

            {

                bool beginsZero = false;//tests for 0XX

                bool isDone = false;//test if already translated

                double dblAmt = (Convert.ToDouble(number));
                //if ((dblAmt > 0) && number.StartsWith("0"))

                if (dblAmt > 0)
                {//test for zero or digit zero in a nuemric

                    beginsZero = number.StartsWith("0");
                    int numDigits = number.Length;

                    int pos = 0;//store digit grouping

                    String place = "";//digit grouping name:hundres,thousand,etc...

                    switch (numDigits)
                    {

                        case 1://ones' range

                            word = ones(number);

                            isDone = true;
                            break;

                        case 2://tens' range

                            word = tens(number);

                            isDone = true;
                            break;

                        case 3://hundreds' range

                            pos = (numDigits % 3) + 1;

                            place = " Hundred ";
                            break;

                        case 4://thousands' range

                        case 5:
                        case 6:
                            pos = (numDigits % 4) + 1;

                            place = " Thousand ";

                            break;
                        case 7://millions' range

                        case 8:
                        case 9:
                            pos = (numDigits % 7) + 1;

                            place = " Million ";

                            break;
                        case 10://Billions's range

                            pos = (numDigits % 10) + 1;

                            place = " Billion ";
                            break;

                        //add extra case options for anything above Billion...

                        default:
                            isDone = true;

                            break;
                    }

                    if (!isDone)
                    {//if transalation is not done, continue...(Recursion comes in now!!)

                        word = translateWholeNumber(number.Substring(0, pos)) + place + translateWholeNumber(number.Substring(pos));

                        //check for trailing zeros

                        if (beginsZero) word = " and " + word.Trim();
                    }

                    //ignore digit grouping names

                    if (word.Trim().Equals(place.Trim())) word = "";
                }

            }

            catch {; }
            return word.Trim();
        }

        private String tens(String digit)
        {

            int digt = Convert.ToInt32(digit);

            String name = null; switch (digt)
            {

                case 10:

                    name = "Ten";
                    break;

                case 11:
                    name = "Eleven";

                    break;
                case 12:

                    name = "Twelve";
                    break;

                case 13:
                    name = "Thirteen";

                    break;
                case 14:

                    name = "Fourteen";
                    break;

                case 15:
                    name = "Fifteen";

                    break;
                case 16:

                    name = "Sixteen";
                    break;

                case 17:
                    name = "Seventeen";

                    break;
                case 18:

                    name = "Eighteen";
                    break;

                case 19:
                    name = "Nineteen";

                    break;
                case 20:

                    name = "Twenty";
                    break;

                case 30:
                    name = "Thirty";

                    break;
                case 40:

                    name = "Fourty";
                    break;

                case 50:
                    name = "Fifty";

                    break;
                case 60:

                    name = "Sixty";
                    break;

                case 70:
                    name = "Seventy";

                    break;
                case 80:

                    name = "Eighty";
                    break;

                case 90:
                    name = "Ninety";

                    break;
                default:

                    if (digt > 0)
                    {

                        name = tens(digit.Substring(0, 1) + "0") + " " + ones(digit.Substring(1));
                    }

                    break;
            }

            return name;
        }

        private String ones(String digit)
        {

            int digt = Convert.ToInt32(digit);
            String name = "";

            switch (digt)
            {

                case 1:
                    name = "One";

                    break;
                case 2:

                    name = "Two";
                    break;

                case 3:
                    name = "Three";

                    break;
                case 4:

                    name = "Four";
                    break;

                case 5:
                    name = "Five";

                    break;
                case 6:

                    name = "Six";
                    break;

                case 7:
                    name = "Seven";

                    break;
                case 8:

                    name = "Eight";
                    break;

                case 9:
                    name = "Nine";

                    break;
            }

            return name;
        }

        private String translateCents(String cents)
        {

            String cts = "", digit = "", engOne = ""; for (int i = 0; i < cents.Length; i++)
            {

                digit = cents[i].ToString();

                if (digit.Equals("0"))
                {

                    engOne = "Zero";
                }

                else

                {

                    engOne = ones(digit);

                }

                cts += " " + engOne;
            }

            return cts;
        }

        #endregion

        public void GetBindOrderAdvancesForVouchers()
        {
            try
            {
                OrderDML dmlOrders = new OrderDML();
                DataTable dtOrder = dmlOrders.GetOrdersForVoucher();
                if (dtOrder.Rows.Count > 0)
                {
                    ddlOrder.Enabled = true;
                    FillDropDown(dtOrder, ddlOrder, "OrderID", "AdvanceString", "-Select Order Advances-");
                }
                else
                {
                    ddlOrder.Items.Clear();
                    ddlOrder.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting/binding Order advances for voucher, due to: " + ex.Message);
            }
        }

        public void GetBindOrderAdvancesForVouchersByBroker(Int64 BrokerID)
        {
            try
            {
                OrderDML dmlOrders = new OrderDML();
                DataTable dtOrder = dmlOrders.GetOrdersForVoucherByBrokerID(BrokerID);
                if (dtOrder.Rows.Count > 0)
                {
                    ddlOrder.Enabled = true;
                    FillDropDown(dtOrder, ddlOrder, "OrderID", "AdvanceString", "-Select Order Advances-");
                }
                else
                {
                    ddlOrder.Items.Clear();
                    ddlOrder.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting/binding Order advances for voucher, due to: " + ex.Message);
            }
        }

        public void GetSetBrokerAccount(string BrokerAccountName)
        {
            try
            {
                AccountsDML dmlAccounts = new AccountsDML();
                DataTable dtBrokerAccountCheck = dmlAccounts.GetAccounts(BrokerAccountName);
                if (dtBrokerAccountCheck.Rows.Count <= 0)
                    dmlAccounts.CreateBrokerAccount(BrokerAccountName);
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
                BankAccountsDML dmlAccounts = new BankAccountsDML();
                DataTable dtBrokerAccountCheck = dmlAccounts.GetAccounts(AccountName);
                if (dtBrokerAccountCheck.Rows.Count <= 0)
                    dmlAccounts.CreateAccount(AccountName);
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting/setting Bank account, due to: " + ex.Message);
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

        private void ClearFields()
        {
            try
            {

            }
            catch (Exception ex)
            {
                notification("Error", "Error clearing all fields, due to: " + ex.Message);
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

        public void GetVouchers(string SortState, string sortExpression = "")
        {
            try
            {
                VouchersDML dml = new VouchersDML();
                DataTable dtVouchers = dml.GetVouchers(SortState);
                //gvResult.DataSource = dtVouchers.Rows.Count > 0 ? dtVouchers : null;


                gvResult.DataSource = dtVouchers;
                if (VouchersSortDirection != null)
                {
                    DataView dv = dtVouchers.AsDataView();
                    this.VouchersSortDirection = this.VouchersSortDirection == "ASC" ? "DESC" : "ASC";
                    if (sortExpression != string.Empty)
                    {
                        dv.Sort = sortExpression + " " + this.VouchersSortDirection;
                    }

                    gvResult.DataSource = dv;
                }
                else
                {
                    gvResult.DataSource = dtVouchers;
                }




                gvResult.DataBind();
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting vouchers, due to: " + ex.Message);
            }
        }
        public void GetVoucher()
        {
            try
            {
                VouchersDML dml = new VouchersDML();
                DataTable dtVoucher = new DataTable();
                if (txtSearchVendor.Text == string.Empty && txtSearchVoucherNo.Text == string.Empty && txtSearchDateFrom.Text == string.Empty && txtSearchDateTo.Text == string.Empty)
                {
                    dtVoucher = dml.GetVoucher();
                }
                else
                {
                    string VoucherNo = txtSearchVoucherNo.Text;
                    string Vendor = txtSearchVendor.Text;

                    string Query = " Where ";

                    if(txtSearchVendor.Text !=string.Empty)
                    {
                        Query += " brk.Name='" + txtSearchVendor.Text + "' AND ";
                    }

                    if (txtSearchVoucherNo.Text !=string.Empty)
                    {
                        Query += " VoucherNo='" + txtSearchVoucherNo.Text + "' AND ";
                    }


                    if (txtSearchDateFrom.Text != string.Empty || txtSearchDateTo.Text != string.Empty)
                    {
                       // Query += " AND ";
                        if (txtSearchDateFrom.Text != string.Empty && txtSearchDateTo.Text == string.Empty)
                        {
                            Query += "Convert(date, v.CreatedDate) = CONVERT(date, '" + txtSearchDateFrom.Text + "') AND ";
                        }
                        else if (txtSearchDateFrom.Text == string.Empty && txtSearchDateTo.Text != string.Empty)
                        {
                            Query += "Convert(date, v.CreatedDate) = CONVERT(date, '" + txtSearchDateTo.Text + "') AND ";
                        }
                        else if (txtSearchDateFrom.Text != string.Empty && txtSearchDateTo.Text != string.Empty)
                        {
                            Query += "Convert(date, v.CreatedDate) BETWEEN CONVERT(date, '" + txtSearchDateFrom.Text + "') AND CONVERT(date, '" + txtSearchDateTo.Text + "') AND ";
                        }
                    }

                        Query = Query.Remove(Query.Length - 5);
                    dtVoucher = dml.GetVoucher(Query);
                }

                if (dtVoucher.Rows.Count > 0)
                {
                    gvResult.DataSource = dtVoucher;
                }
                else
                {
                    gvResult.DataSource = dtVoucher;
                }
                gvResult.DataBind();
            }
        catch (Exception ex)
            {
                notification("Error", "Error getting/binding Voucher, due to: " + ex.Message);
            }
        }

        #endregion

        #region Events

        #region Linkbutton's Events
        protected void lnkCancelSearch_Click(object sender, EventArgs e)
        {

        }        

        protected void lnkAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                modalPayment.Show();
            }
            catch (Exception ex)
            {
                notification("Error", "Error enabling add new voucher panel, due to: " + ex.Message);
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
                    string[] VendorString = Vendor.Split('|');
                    string VendorName = VendorString[0].ToString();

                    //string[] VendorString;
                    string BrokerName = string.Empty;
                    string PatrolPumpName = string.Empty;

                    string VendorAccName = Vendor;
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
                    PatrolPumpDML dmlPatrolPump = new PatrolPumpDML();
                    DataTable dtVendor = VendorType == "Broker" ? dmlBrokers.GetActiveBrokersByName(VendorName) : dmlPatrolPump.GetActivePatrolPumpsByName(VendorName);
                    if (dtVendor.Rows.Count > 0)
                    {
                        if (VendorType == "Broker")
                        {
                            BrokerID = Convert.ToInt64(dtVendor.Rows[0][VendorType == "Broker" ? "ID" : "PatrolPumpID"]);
                            BrokerName = VendorString[0].ToString();
                        }
                        else
                        {
                            PatrolPumpID = Convert.ToInt64(dtVendor.Rows[0][VendorType == "Broker" ? "ID" : "PatrolPumpID"]);
                            PatrolPumpName = VendorString[0].ToString();
                        }
                        //VendorID = Convert.ToInt64(dtVendor.Rows[0][VendorType == "Broker" ? "ID" : "PatrolPumpID"]);
                    }

                    OrderDML dmlOrder = new OrderDML();
                    VouchersDML dmlVouchers = new VouchersDML();
                    Int64 VoucherID = dmlVouchers.InsertVouchers(VoucherNo, BrokerID, PatrolPumpID, BankID, ChequeNo, Amount, OrderID, VehicleRegNo, LoginID);
                    if (VoucherID > 0)
                    {
                        DateTime CurrentDate = DateTime.Now;
                        AccountsDML dmlVendorAccount = new AccountsDML();
                        double Debit = 0;
                        double Credit = Amount;

                        string Description = "Rs. " + Amount + "/- has been credit from " + BankAccount + DocumentNo + " against Voucher # " + VoucherNo;
                        if (cbAdvance.Checked)
                        {
                            DataTable dtOrder = dmlOrder.GetOrderAdvanceByOrderAndType(OrderID, "Advance Freight");
                            string AdvancePlace = dtOrder.Rows.Count > 0 ? ("From " + dtOrder.Rows[0]["AdvancePlace"].ToString() + " ") : string.Empty;
                            Description = "Paid Advance by hand " + AdvancePlace + "for vehicle " + VehicleRegNo + " from " + BankAccount + DocumentNo + " against Voucher # " + VoucherNo;
                        }
                        else
                        {
                            Description = Description;
                        }
                        //Description = cbAdvance.Checked ? ("Paid Advance by hand for vehicle " + VehicleRegNo + " from " + BankAccount + DocumentNo + " against Voucher # " + VoucherNo) : Description;
                        GetSetBrokerAccount(VendorAccName);
                        DataTable dtVendorAccount = dmlVendorAccount.GetInAccounts(VendorAccName);
                        double Balance = Convert.ToDouble(dtVendorAccount.Rows.Count > 0 ? (dtVendorAccount.Rows[dtVendorAccount.Rows.Count > 0 ? dtVendorAccount.Rows.Count - 1 : 0]["Balance"]) : 0);
                        Balance = Balance - Credit + Debit;
                        if (VendorType == "Broker")
                        {
                            //GetSetBrokerAccount(VendorAccName);
                            Int64 BrokerTransactionID = dmlVendorAccount.InsertInBrokerAccount(VendorAccName, VendorID, Description, Debit, Credit, Balance, LoginID);
                        }
                        else
                        {
                            Int64 PatrolPumpTransactionID = dmlVendorAccount.InsertInPatrolPumpAccount(VendorAccName, VendorID, Description, Debit, Credit, Balance, CurrentDate.ToShortDateString(), "", "", LoginID);
                        }
                        
                        if (rbPaymentMode.SelectedItem.Text == "Cheque")
                        {
                            string[] BankAccountString = ddlBankAccounts.SelectedItem.Value.Split('|');
                            BankAccount = ddlBankAccounts.SelectedItem.Value;
                            string BankName = BankAccountString[0];

                            double BankDebit = 0;
                            double BankCredit = Amount;
                            double BankBalance = 0;
                            BankAccountsDML dmlBankAccounts = new BankAccountsDML();


                            GetSetBankAccount(BankAccount);


                            DataTable dtBankLedger = dmlBankAccounts.GetInAccountsAlonBankInfo(BankAccount, "DESC");
                            if (dtBankLedger.Rows.Count > 0)
                            {
                                BankBalance = Convert.ToDouble(dtBankLedger.Rows[0]["Balance"]);
                            }
                            BankBalance = BankBalance + BankDebit - BankCredit;
                            string BankDescription = "Rs. " + Amount + "/- paid to " + rbVendor.SelectedItem.Text + " " + Vendor + " against Voucher # " + VoucherNo;

                            dmlBankAccounts.InsertInAccount(BankAccount, BankID, BankDescription, BankDebit, BankCredit, BankBalance, DateTime.Now, "", "" ,LoginID);
                        }


                        GetVouchers("DESC");

                        //DataTable dtAccountCheck = dmlBRAccount.GetAccounts(VendorAccName);
                        //if (dtAccountCheck.Rows.Count > 0)
                        //{
                        //    DataTable dtAccounts = dmlBRAccount.GetInAccounts(VendorAccName);
                        //    if (dtAccounts.Rows.Count > 0)
                        //    {
                        //        gvResult.DataSource = dtAccounts;
                        //        lblSingleLedgerAccountName.Text = VendorAccName;

                        //        AllAcounts.Visible = false;
                        //        MainLedger.Visible = true;
                        //    }
                        //    else
                        //    {
                        //        gvResult.DataSource = null;
                        //        gvResult.EmptyDataText = "No account found";
                        //    }
                        //}
                        //else
                        //{
                        //    gvResult.DataSource = null;
                        //    gvResult.EmptyDataText = "No account found";
                        //}
                        //gvResult.DataBind();

                        rbVendor.ClearSelection();
                        ddlVendors.ClearSelection();
                        txtAmount.Text = string.Empty;
                        rbPaymentMode.ClearSelection();
                        txtDocumentNo.Text = string.Empty;
                        ddlBankAccounts.ClearSelection();
                        BrokerAdvancePlaceholder.Visible = false;
                        cbAdvance.Checked = false;
                    }
                }
            }
            catch (Exception ex)
            {
                PaymentNotification("Error", "Error saving broker payment, due to: " + ex.Message);
                modalPayment.Show();
            }
        }

        protected void lnkConfirm_Click(object sender, EventArgs e)
        {
            if (txtReceivedBy.Text.Replace(" ", string.Empty).Replace("&nbsp;", string.Empty) == string.Empty)
            {
                ConfirmNotification("Error", "Please enter Received By");
                txtReceivedBy.Focus();
                modalConfirm.Show();
            }
            else
            {
                string Action = hfConfirmAction.Value == string.Empty ? string.Empty : hfConfirmAction.Value;
                if (Action == "VoucherPaid")
                {
                    string ReceivedBy = txtReceivedBy.Text;
                    Int64 VoucherID = Convert.ToInt64(hfVoucherID.Value);
                    VouchersDML dml = new VouchersDML();
                    dml.UpdateVoucherPaid(VoucherID, ReceivedBy, LoginID);
                    GetVouchers("DESC");

                    modalConfirm.Hide();
                    hfConfirmAction.Value = string.Empty;
                    hfVoucherID.Value = string.Empty;
                }
            }
            
        }

        protected void lnkCancelConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                modalConfirm.Hide();
                hfConfirmAction.Value = string.Empty;
                hfVoucherID.Value = string.Empty;
            }
            catch (Exception ex)
            {
                notification("Error", "Error cancelling, due to: " + ex.Message);
                modalConfirm.Show();
            }
        }

        protected void lnkCloseVoucherPrints_Click(object sender, EventArgs e)
        {
            try
            {
                lblVoucherPrintTo.Text = string.Empty;
                lblVoucherPrintVehicleRegNo.Text = string.Empty;
                lblVoucherPrintAmount.Text = string.Empty;
                lblVoucherPrintDate.Text = string.Empty;
                lblVoucherPrintCashNo.Text = string.Empty;
                lblVoucherPrintChequeNo.Text = string.Empty;
                lblVoucherPrintBank.Text = string.Empty;
                lblVoucherPrintAmountInWorlds.Text = string.Empty;
                lblVoucherPrintApprovedBy.Text = string.Empty;
                lblVoucherPrintPaidBy.Text = string.Empty;
                lblVoucherPrintReceivedBy.Text = string.Empty;


                modalVoucherPrint.Hide();
            }
            catch (Exception ex)
            {
                notification("Error", "Error closing voucher print, due to: " + ex.Message);
                modalVoucherPrint.Show();
            }
        }

        protected void lnkClearFrom_Click(object sender, EventArgs e)
        {
            try
            {
                //VouchersDML dml = new VouchersDML();
                //DataTable dtVoucher = new DataTable();
                //dtVoucher = dml.GetVouchers("Desc");
                //if (dtVoucher.Rows.Count > 0)
                //{
                //    gvResult.DataSource = dtVoucher;
                //}
                //else
                //{
                //    gvResult.DataSource = dtVoucher;
                //}
                //gvResult.DataBind();

                txtSearchDateFrom.Text = string.Empty;
                GetVoucher();
            }
            catch (Exception ex)
            {
                notification("Error", "Error clearing Vendor, due to: " + ex.Message);
            }
        }

        protected void lnkClearTo_Click(object sender, EventArgs e)
        {
            try
            {
                //VouchersDML dml = new VouchersDML();
                //DataTable dtVoucher = new DataTable();
                //dtVoucher = dml.GetVouchers("Desc");
                //if (dtVoucher.Rows.Count > 0)
                //{
                //    gvResult.DataSource = dtVoucher;
                //}
                //else
                //{
                //    gvResult.DataSource = dtVoucher;
                //}
                //gvResult.DataBind();

                txtSearchDateTo.Text = string.Empty;
                GetVoucher();
            }
            catch (Exception ex)
            {
                notification("Error", "Error clearing Vendor, due to: " + ex.Message);
            }

        }

        protected void lnkSearchFilter_Click(object sender, EventArgs e)
        {
            try
            {
                GetVoucher();
            }
            catch (Exception ex)
            {
                notification("Error", "Error esarching, due to: " + ex.Message);
            }
        }

        protected void lnkClearVoucherNo_Click(object sender, EventArgs e)
        {
            try
            {
                //VouchersDML dml = new VouchersDML();
                //DataTable dtVoucher = new DataTable();
                //dtVoucher = dml.GetVouchers("Desc");
                //if (dtVoucher.Rows.Count > 0)
                //{
                //    gvResult.DataSource = dtVoucher;
                //}
                //else
                //{
                //    gvResult.DataSource = dtVoucher;
                //}
                //gvResult.DataBind();

                txtSearchVoucherNo.Text = string.Empty;
                GetVoucher();
            }
            catch (Exception ex)
            {
                notification("Error", "Error clearing Vendor, due to: " + ex.Message);
            }

        }

        protected void lnkClearVendor_Click(object sender, EventArgs e)
        {
            try
            {
                //    VouchersDML dml = new VouchersDML();
                //    DataTable dtVoucher = new DataTable();
                //dtVoucher = dml.GetVouchers("Desc");
                //if (dtVoucher.Rows.Count > 0)
                //{
                //    gvResult.DataSource = dtVoucher;
                //}
                //else
                //{
                //    gvResult.DataSource = dtVoucher;
                //}
                //gvResult.DataBind();


                txtSearchVendor.Text = string.Empty;
                GetVoucher();
            }
            catch (Exception ex)
            {
                notification("Error", "Error clearing Vendor, due to: " + ex.Message);
            }

        }

        protected void lnkCancelSearch_Click1(object sender, EventArgs e)
        {
            txtSearchDateFrom.Text = string.Empty;
            txtSearchDateTo.Text = string.Empty;
            txtSearchVoucherNo.Text = string.Empty;
            txtSearchVendor.Text = string.Empty;

            GetVoucher();

        }

        #endregion

        #region Radiobutton's Events

        protected void rbVendor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbVendor.SelectedItem.Text == "Broker")
                {
                    try
                    {
                        BrokersDML dml = new BrokersDML();
                        DataTable dtBrokers = dml.GetActiveBroker();
                        if (dtBrokers.Rows.Count > 0)
                        {
                            FillDropDown(dtBrokers, ddlVendors, "ID", "Name", "-Select-");
                        }
                        ddlVendors.Enabled = true;

                        BrokerAdvancePlaceholder.Visible = true;
                    }
                    catch (Exception ex)
                    {
                        PaymentNotification("Error", "Error getting/populating Brokers, due to: " + ex.Message);
                    }
                }
                else
                {
                    try
                    {
                        PatrolPumpDML dml = new PatrolPumpDML();
                        DataTable dtPatrolPump = dml.GetActivePatrolPumps();
                        if (dtPatrolPump.Rows.Count > 0)
                        {
                            FillDropDown(dtPatrolPump, ddlVendors, "Code", "Name", "-Select-");
                        }
                        ddlVendors.Enabled = true;

                        BrokerAdvancePlaceholder.Visible = false;
                    }
                    catch (Exception ex)
                    {
                        PaymentNotification("Error", "Error getting/populating Patrol pumps, due to: " + ex.Message);
                    }
                    //ddlVendors.Items.Clear();
                    //ddlVendors.Enabled = false;
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
                //VehicleRegNoPlaceholder.Visible = false;
                if (rbPaymentMode.SelectedItem.Text == "Cheque")
                {
                    bankAccountsPlaceholder.Visible = true;
                    DocumentNoPlaceholder.Visible = true;
                    //VehicleRegNoPlaceholder.Visible = true;
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

        #region Checkbutton's Events

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

        #region Gridview's Events

        #region Databinding

        protected void gvResult_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DateTime CreatedDate = Convert.ToDateTime(e.Row.Cells[8].Text);
                    e.Row.Cells[8].Text = CreatedDate.ToShortDateString();


                    ImageButton imgVoucherPayed = e.Row.FindControl("imgVoucherPayed") as ImageButton;
                    DataRowView rowView = (DataRowView)e.Row.DataItem;
                    bool IsPayed = rowView["IsPayed"].ToString() == "True" ? true : false;
                    imgVoucherPayed.Enabled = IsPayed == true ? false : true;

                    imgVoucherPayed.ImageUrl = IsPayed == true ? "~/assets/images/On.png" : "~/assets/images/Off.png";
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error binding Vouchers, due to: " + ex.Message);
            }
        }

        #endregion

        #region Rowcommand

        protected void gvResult_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Payed")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvResult.Rows[index];
                    Int64 VoucherID = Convert.ToInt64(gvResult.DataKeys[index]["VoucherID"]);
                    bool IsPayed = gvResult.DataKeys[index]["isPayed"].ToString() == "True" ? true : false;
                    if (IsPayed == false)
                    {
                        hfVoucherID.Value = VoucherID.ToString();
                        ConfirmModal("Are you sure you want to Set voucher to be paid?", "VoucherPaid");
                    }
                }
                else if (e.CommandName == "PrintVoucher")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvResult.Rows[index];
                    Int64 VoucherID = Convert.ToInt64(gvResult.DataKeys[index]["VoucherID"]);
                    VouchersDML dmlVouchers = new VouchersDML();
                    DataTable dtVouchers = dmlVouchers.GetVoucher(VoucherID);
                    if (dtVouchers.Rows.Count > 0)
                    {
                        lblVoucherPrintNo.Text = dtVouchers.Rows[0]["VoucherNo"].ToString();
                        lblVoucherPrintTo.Text = dtVouchers.Rows[0]["Vendor"].ToString();
                        lblVoucherPrintVehicleRegNo.Text = dtVouchers.Rows[0]["VehicleRegNo"].ToString();
                        lblVoucherPrintAmount.Text = dtVouchers.Rows[0]["Amount"].ToString();
                        lblVoucherPrintDate.Text = dtVouchers.Rows[0]["CreatedDate"].ToString();
                        if (dtVouchers.Rows[0]["ChequeNo"].ToString().Replace(" ", string.Empty).Replace("&nbsp;", string.Empty) == string.Empty)
                        {
                            lblVoucherPrintCashNo.Text = "Yes";
                        }
                        else
                        {
                            lblVoucherPrintBank.Text = dtVouchers.Rows[0]["Bank"].ToString();
                            lblVoucherPrintChequeNo.Text = dtVouchers.Rows[0]["ChequeNo"].ToString();
                        }
                        lblVoucherPrintAmountInWorlds.Text = changeNumericToWords(Convert.ToDouble(dtVouchers.Rows[0]["Amount"].ToString()));

                        lblVoucherPrintApprovedBy.Text = dtVouchers.Rows[0]["CreatedByName"].ToString() == string.Empty ? "&nbsp" : dtVouchers.Rows[0]["CreatedByName"].ToString();
                        lblVoucherPrintPaidBy.Text = dtVouchers.Rows[0]["ModifiedByName"].ToString() == string.Empty ? "&nbsp" : dtVouchers.Rows[0]["ModifiedByName"].ToString();
                        lblVoucherPrintReceivedBy.Text = dtVouchers.Rows[0]["ReceivedBy"].ToString() == string.Empty ? "&nbsp" : dtVouchers.Rows[0]["ReceivedBy"].ToString();

                    }


                    modalVoucherPrint.Show();
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error commanding row, due to: " + ex.Message);
            }
        }

        #endregion

        #endregion

        #region Dropdowns Events

        protected void ddlVendors_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlVendors.SelectedIndex == 0)
                {
                    BrokerAdvancePlaceholder.Visible = false;
                }
                else
                {
                    BrokerAdvancePlaceholder.Visible = true;

                    Int64 BrokerID = Convert.ToInt64(ddlVendors.SelectedItem.Value);
                    //Getting/Binding Order advances for vouchers
                    GetBindOrderAdvancesForVouchersByBroker(BrokerID);
                }
            }
            catch (Exception ex)
            {
                PaymentNotification("Error", "Error selecting vendor, due to: " + ex.Message);
            }
            finally
            {
                modalPayment.Show();
            }
        }

        #endregion

        #endregion

        protected void gvResult_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                this.GetVouchers("DESC", e.SortExpression);
            }
            catch (Exception ex)
            {
                notification("Error", "Error sorting bilties grid, due to: " + ex.Message);
            }
        }

        protected void gvResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvResult.PageIndex = e.NewPageIndex;
                this.GetVouchers("DESC");
            }
            catch (Exception ex)
            {
                notification("Error", "Error changing index of grid page, due to: " + ex.Message);
            }
        }
    }
}