using BLL;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using ListItem = System.Web.UI.WebControls.ListItem;

namespace BiltySystem
{
    public partial class SearchInvoice : System.Web.UI.Page
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

        private string BiltiesSortDirection
        {
            get { return ViewState["SortDirection"] != null ? ViewState["SortDirection"].ToString() : "ASC"; }
            set { ViewState["SortDirection"] = value; }
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
            PaymentNotification("", "");
            if (!IsPostBack)
            {
                this.Title = "Create Bill";

                //Getting Bills
                GetBills();

                //Getting/Populating Banks
                GetPopulateBanks();

                //Getting/Populating Bill to Customers/Companies
                try
                {
                    CompanyDML dml = new CompanyDML();
                    DataTable dtCompanies = dml.GetCompaniesForBilty();
                    FillDropDown(dtCompanies, ddlBilToCustomer, "CompanyID", "Company", "- Select -");
                }
                catch (Exception ex)
                {
                    notification("Error", "Error getting/populating Company, due to: " + ex.Message);
                }
            }
        }

        #endregion

        #region Custom Methods
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

        public void GetBills(string sortExpression = "")
        {
            try
            {
                InvoiceDML dml = new InvoiceDML();
                //DataTable dtBills = dml.GetBill();


                string Query = " WHERE ";
                DataTable dtInvoice = new DataTable();
                //DataTable dtBill = new DataTable();
                if (txtInvoiceNo.Text == string.Empty && ddlBilToCustomer.SelectedIndex <= 0 && txtStartDate.Text == string.Empty && txtEndDate.Text == string.Empty)
                {
                    dtInvoice = dml.GetBill();

                }
                else
                {
                    if (ddlBilToCustomer.SelectedIndex != 0)
                    {
                        string[] CompanyString = ddlBilToCustomer.SelectedItem.Text.Split('|');
                        string Company = CompanyString[1].Trim();
                        Query += "bi.CustomerCompany = '" + Company + "' AND ";
                    }

                    if (txtStartDate.Text != string.Empty)
                    {
                        if (txtEndDate.Text == string.Empty)
                        {
                            Query += "Convert(date, bi.CreatedDate) = CONVERT(date, '" + txtStartDate.Text + "') AND ";
                        }
                        else
                        {
                            Query += "(SELECT TOP 1 CreatedDate FROM Bill where BillNo = bi.BillNo) BETWEEN '" + txtStartDate.Text + "' AND '" + txtEndDate.Text + "' AND ";
                        }
                    }

                    if (txtEndDate.Text != string.Empty && txtStartDate.Text == string.Empty)
                    {
                        Query += "Convert(date, bi.CreatedDate) = CONVERT(date, '" + txtEndDate.Text + "') AND ";
                    }

                    if (txtInvoiceNo.Text != string.Empty)
                    {
                        Query += "bi.BillNo LIKE '%" + txtInvoiceNo.Text + "%' AND ";
                    }
                    Query = Query.Substring(0, Query.Length - 5);

                    dtInvoice = dml.SearchBill(Query);
                }





                DataTable dtBill = new DataTable();
                dtBill.Columns.Add("BillNo"); 
                dtBill.Columns.Add("ActualBillNo");
                dtBill.Columns.Add("CustCodeID");
                dtBill.Columns.Add("CustomerCompany");
                dtBill.Columns.Add("Total");
                dtBill.Columns.Add("TotalBalance");
                dtBill.Columns.Add("InvoiceDate");
                dtBill.Columns.Add("BiltyDate");
                dtBill.Columns.Add("CreditLimit");
                dtBill.Columns.Add("TotalContainers");
                dtBill.Columns.Add("ShippingLine");
                dtBill.Columns.Add("isPaid");
                if (dtInvoice.Rows.Count > 0)
                {
                    foreach (DataRow _drBills in dtInvoice.Rows)
                    {
                        DateTime CreatedDate = Convert.ToDateTime(_drBills["CreatedDate"]);
                        dtBill.Rows.Add(
                            _drBills["BillNo"].ToString(),
                            _drBills["ActualBillNo"].ToString(),
                            _drBills["CustCodeID"].ToString(),
                            _drBills["CustomerCompany"].ToString(),
                            _drBills["Total"].ToString(),
                            (_drBills["TotalBalance"] == DBNull.Value ? "0" : _drBills["TotalBalance"].ToString()),
                            CreatedDate.ToString("dd/MM/yyyy"),
                            _drBills["BiltyDate"].ToString(),
                            _drBills["CreditLimit"],
                            _drBills["TotalContainers"].ToString(),
                            _drBills["ShippingLine"].ToString(),
                            _drBills["isPaid"]
                        );
                    }
                }

                //gvInvoice.DataSource = dtBill.Rows.Count > 0 ? dtBill : null;
                if (dtBill.Rows.Count > 0)
                {
                    if (BiltiesSortDirection != null)
                    {
                        DataView dv = dtBill.AsDataView();
                        this.BiltiesSortDirection = this.BiltiesSortDirection == "ASC" ? "DESC" : "ASC";
                        if (sortExpression != string.Empty)
                        {
                            dv.Sort = sortExpression + " " + this.BiltiesSortDirection;
                        }

                        gvInvoice.DataSource = dv;
                        DataTable dtReport = dv.ToTable();
                        Session.Add("ReportDataTable", dtReport);
                    }
                    else
                    {
                        gvInvoice.DataSource = dtBill;
                    }
                }
                else
                {
                    gvInvoice.DataSource = null;
                }

                gvInvoice.DataBind();
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting/binding Invoices, due to: " + ex.Message);
            }
        }

        public void GetInvoices(string Query)
        {
            try
            {
                InvoiceDML dml = new InvoiceDML();
                DataTable dtInvoice = dml.GetBill();
                gvInvoice.DataSource = dtInvoice.Rows.Count > 0 ? dtInvoice : null;
                gvInvoice.DataBind();
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting/binding Invoices, due to: " + ex.Message);
            }
        }

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

        public void BindGrid(GridView _gv, DataTable _dt)
        {
            try
            {
                _gv.DataSource = _dt.Rows.Count > 0 ? _dt : null;
                _gv.DataBind();
            }
            catch (Exception ex)
            {
                notification("Error", "Error binding grid, due to: " + ex.Message);
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

                hfSelectedBill.Value = string.Empty;
                hfSelectedBillCustomer.Value = string.Empty;
            }
            catch (Exception ex)
            {
                PaymentNotification("Error", "Error resetting payment fields");
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
                    dmlAccounts.InsertBillToPettyCash(AccountName, "0", "Account created (Auto)", 0, 0, 0,DateTime.Now, LoginID);
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
                    dmlBankAccounts.InsertInAccount(AccountName, 0, "Account created", 0, 0, 0, DateTime.Now, "0", "0" , LoginID);
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

        public void TransactToCustLedger(string CustomerAccName, Int64 CustomerCompanyID, double TransactAmount, string TransactMode, string TransactDescription)
        {

            try
            {
                //dmlInvoice.BillToContainer(ContainerID);

                AccountsDML dmlAcc = new AccountsDML();
                DataTable dtCustAccountCheck = dmlAcc.GetAccounts(CustomerAccName);
                if (dtCustAccountCheck.Rows.Count <= 0)
                    dmlAcc.CreateAccount(CustomerAccName);

                double Debit = TransactMode == "Dr" ? TransactAmount : 0;
                double Credit = TransactMode == "Dr" ? 0 : TransactAmount;

                double Balance = 0;
                DataTable dtCustAccount = dmlAcc.GetInAccounts(CustomerAccName);
                if (dtCustAccount.Rows.Count > 0)
                {
                    Balance = Convert.ToDouble(dtCustAccount.Rows[dtCustAccount.Rows.Count - 1]["Balance"]);
                }
                Balance = Balance + Debit - Credit;

                string[] CustomerNameString = CustomerAccName.Split('|');

                string CustomerName = CustomerNameString[0].ToString().Trim();
                string CustomerCode = CustomerNameString[1].ToString().Trim();
                //Int64 CustomerID = dtCompany.Rows.Count > 0 ? Convert.ToInt64(dtCompany.Rows[0]["CompanyID"].ToString()) : 0;




                dmlAcc.InsertInAccount(CustomerAccName, CustomerCompanyID, TransactDescription, Debit, Credit, Balance, LoginID);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [WebMethod]
        [Obsolete]
        public static string SendEmail(string Subject, string body , string emailTo)
        {
            string message = "mail sent successfully";
            try
            {
                string mzb1 = "data:image/jpeg;base64,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+tB20/A9QvmufWh60HbT8D1C+a59aHXiT7N3L1TqfNdo3n6LGqEbK+tB20/A9QvmufWh60HbT8D1C+a59aHXiT7N3L1TqfNdo3n6LGqEbK+tB20/A9QvmufWh60HbT8D1C+a59aHXiT7N3L1TqfNdo3n6LGqEbK+tB20/A9QvmufWh60HbT8D1C+a59aHXiT7N3L1TqfNdo3n6LGqEbK+tB20/A9QvmufWh60HbT8D1C+a59aHXiT7N3L1TqfNdo3n6LGqEbK+tB20/A9QvmufWh60HbT8D1C+a59aHXiT7N3L1TqfNdo3n6LGqEbK+tB20/A9QvmufWh60HbT8D1C+a59aHXiT7N3L1TqfNdo3n6LGqEbK+tB20/A9QvmufWh60HbT8D1C+a59aHXiT7N3L1TqfNdo3n6LGqEbK+tB20/A9QvmufWh60HbT8D1C+a59aHXiT7N3L1TqfNdo3n6LGqEbK+tB20/A9QvmufWh60HbT8D1C+a59aHXiT7N3L1TqfNdo3n6LGqEbK+tB20/A9QvmufWh60HbT8D1C+a59aHXiT7N3L1TqfNdo3n6LGqEbK+tB20/A9QvmufWh60HbT8D1C+a59aHXiT7N3L1TqfNdo3n6LGqEbK+tB20/A9QvmufWh60HbT8D1C+a59aHXiT7N3L1TqfNdo3n6LGqEbK+tB20/A9QvmufWh60HbT8D1C+a59aHXiT7N3L1TqfNdo3n6LGqEbK+tB20/A9QvmufWh60HbT8D1C+a59aHXiT7N3L1TqfNdo3n6LGqEbK+tB20/A9QvmufWh60HbT8D1C+a59aHXiT7N3L1TqfNdo3n6LGqEbK+tB20/A9QvmufWh60HbT8D1C+a59aHXiT7N3L1TqfNdo3n6LGqEbK+tB20/A9QvmufWh60HbT8D1C+a59aHXiT7N3L1TqfNdo3n6LGqEbK+tB20/A9QvmufWh60HbT8D1C+a59aHXiT7N3L1TqfNdo3n6LGqEbK+tB20/A9QvmufWh60HbT8D1C+a59aHXiT7N3L1TqfNdo3n6LGqEbK+tB20/A9QvmufWh60HbT8D1C+a59aHXiT7N3L1TqfNdo3n6LGqEbK+tB20/A9QvmufWh60HbT8D1C+a59aHXiT7N3L1TqfNdo3n6LGqEbK+tB20/A9QvmufWh60HbT8D1C+a59aHXiT7N3L1TqfNdo3n6LGqEbK+tB20/A9QvmufWh60HbT8D1C+a59aHXiT7N3L1TqfNdo3n6LGqEbK+tB20/A9QvmufWh60HbT8D1C+a59aHXiT7N3L1TqfNdo3n6LGqEbK+tB20/A9QvmufWh60HbT8D1C+a59aHXiT7N3L1TqfNdo3n6LGqEbK+tB20/A9QvmufWh60HbT8D1C+a59aHXiT7N3L1TqfNdo3n6LGqEbK+tB20/A9QvmufWh60HbT8D1C+a59aHXiT7N3L1TqfNdo3n6LGqEbK+tB20/A9QvmufWh60HbT8D1C+a59aHXiT7N3L1TqfNdo3n6LGqEbK+tB20/A9QvmufWh60HbT8D1C+a59aHXiT7N3L1TqfNdo3n6LGqEbK+tB20/A9QvmufWh60HbT8D1C+a59aHXiT7N3L1TqfNdo3n6LGqEbK+tB20/A9QvmufWh60HbT8D1C+a59aHXiT7N3L1TqfNdo3n6LGqEbK+tB20/A9QvmufWh60HbT8D1C+a59aHXiT7N3L1TqfNdo3n6LGqEbK+tB20/A9QvmufWh60HbT8D1C+a59aHXiT7N3L1TqfNdo3n6LGqEbK+tB20/A9QvmufWh60HbT8D1C+a59aHXiT7N3L1TqfNdo3n6LGqEbK+tB20/A9QvmufWh60HbT8D1C+a59aHXiT7N3L1TqfNdo3n6LGqEbK+tB20/A9QvmufWh60HbT8D1C+a59aHXiT7N3L1TqfNdo3n6LGqEbK+tB20/A9QvmufWh60HbT8D1C+a59aHXiT7N3L1TqfNdo3n6LGqEbK+tB20/A9QvmufWh60HbT8D1C+a59aHXiT7N3L1TqfNdo3n6LGqEbK+tB20/A9QvmufWh60HbT8D1C+a59aHXiT7N3L1TqfNdo3n6LGqEbK+tB20/A9QvmufWh60HbT8D1C+a59aHXiT7N3L1TqfNdo3n6LGqEbK+tB20/A9QvmufWh60HbT8D1C+a59aHXiT7N3L1TqfNdo3n6LGqEbK+tB20/A9QvmufWh60HbT8D1C+a59aHXiT7N3L1TqfNdo3n6LGqEbK+tB20/A9QvmufWh60HbT8D1C+a59aHXiT7N3L1TqfNdo3n6LGqEbK+tB20/A9QvmufWh60HbT8D1C+a59aHXiT7N3L1TqfNdo3n6LGqEbK+tB20/A9QvmufWh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 ";
                string mzb = "data:image/jpeg;base64,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";
                //body = body.Replace("http://valstechnologies.com/images/mzb.jpeg",mzb);
                ///body = body.Replace("http://valstechnologies.com/images/mzb1.jpeg",mzb1);
                string Host = "mail.vals.com.pk";               
                string Username = "mzb@valstechnologies.com";             
                string Password = "Valspakistan";
                StringReader sr = new StringReader(body.ToString());
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient(Host);
                    mail.From = new MailAddress(Username);
                    mail.To.Add(emailTo);
                    mail.Subject = Subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;
                    SmtpServer.Port = 2525;
                    SmtpServer.Credentials = new System.Net.NetworkCredential(Username, Password);
                    SmtpServer.EnableSsl = false;
                    BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                    iTextSharp.text.Font font = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.NORMAL);
                    Paragraph p1 = new Paragraph(new Chunk("Sample text", font));
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
                    pdfDoc.Open();
                    htmlparser.Parse(sr);
                    pdfDoc.Close();
                    byte[] bytes = memoryStream.ToArray();
                    memoryStream.Close();
                    mail.Attachments.Add(new Attachment(new MemoryStream(bytes), "Bill.pdf"));
                    SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                    SmtpServer.Send(mail);
                }
            }
            catch (Exception ex)
            {
                message = "email not sent due to" + ex.Message;      
            }
            return message;
       
        }

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

        #endregion

        #region Events

        #region Grid's Events

        protected void gvInvoice_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                UsersDML dmlUser = new UsersDML();
                DataTable dtUser = dmlUser.GetUsers(LoginID);
                string Username = dtUser.Rows[0]["UserName"].ToString();
                if (Username.Trim().ToLower() != "superadmin")
                {
                    gvInvoice.Columns[11].Visible = false;
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    LinkButton lnkReceivePayment = e.Row.FindControl("lnkReceivePayment") as LinkButton;
                    DataRowView rowView = (DataRowView)e.Row.DataItem;
                    string[] InvoiceDateTime = e.Row.Cells[GetColumnIndexByName(e.Row, "InvoiceDate")].Text.Split(' ');
                    e.Row.Cells[GetColumnIndexByName(e.Row, "InvoiceDate")].Text = InvoiceDateTime[0].ToString();
                    bool IsPaid = gvInvoice.DataKeys[e.Row.RowIndex].Values["isPaid"].ToString() == "True" ? true : false;
                    double TotalBalance = Convert.ToDouble(gvInvoice.DataKeys[e.Row.RowIndex].Values["TotalBalance"]);
                    //if (IsPaid == false)
                    if (TotalBalance > 0)
                    {
                        //e.Row.BackColor = Color.LightPink;
                        Int64 CreditLimit = gvInvoice.DataKeys[e.Row.RowIndex].Values["CreditLimit"].ToString() != string.Empty ? Convert.ToInt64(gvInvoice.DataKeys[e.Row.RowIndex].Values["CreditLimit"]) : 0;
                        if (CreditLimit > 0)
                        {
                            DateTime InvoicedDate = DateTime.ParseExact(e.Row.Cells[1].Text, "dd/MM/yyyy", null);
                            DateTime TodaysDate = DateTime.Now;
                            double DateDifference = (TodaysDate - InvoicedDate).TotalDays;
                            if (DateDifference > CreditLimit)
                            {
                                e.Row.BackColor = Color.LightPink;
                            }
                            else
                            {
                                lnkReceivePayment.ForeColor = Color.Red;
                            }
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

        protected void gvInvoice_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "PrintBill")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow gvr = gvInvoice.Rows[index];
                DataTable dtReport = new DataTable();

                dtReport.Columns.Add("BillID");
                dtReport.Columns.Add("CreatedDate");
                dtReport.Columns.Add("ShippingLine");
                dtReport.Columns.Add("BillTo");
                dtReport.Columns.Add("");
                dtReport.Columns.Add("OrderNo");
                dtReport.Columns.Add("BiltyDate");
                dtReport.Columns.Add("ContainerNo");
                dtReport.Columns.Add("VehicleRegNo");
                dtReport.Columns.Add("EmptyContainerPickLocation");
                dtReport.Columns.Add("EmptyContainerDropLocation");
                dtReport.Columns.Add("Expenses");
                dtReport.Columns.Add("Lolo");
                dtReport.Columns.Add("Weighment");

                //string InvoiceNo = gvr.Cells[1].Text;
                string InvoiceNo = gvInvoice.DataKeys[index]["ActualBillNo"].ToString();
                //string InvoiceNo = gvInvoice.DataKeys[index]["ActualBillNo"].ToString();
                string CustomerName = gvr.Cells[2].Text;
                InvoiceDML dmlInvoice = new InvoiceDML();
                DataTable dtInvoice = dmlInvoice.GetBill(InvoiceNo, CustomerName);
                if (dtInvoice.Rows.Count > 0)
                {
                    lblBillNo.Text = dtInvoice.Rows[0]["NewBillNo"].ToString();
                    string ContainerIDs = string.Empty;
                    foreach (DataRow _drInvoices in dtInvoice.Rows)
                    {
                        ContainerIDs += _drInvoices["OrderconsignmentID"].ToString() + ",";
                    }
                    ContainerIDs = ContainerIDs.Remove(ContainerIDs.Length - 1);
                    DateTime dateBill = Convert.ToDateTime(dtInvoice.Rows[0]["CreatedDate"].ToString());
                    lblShippingLine.Text = dtInvoice.Rows[0]["ShippingLine"].ToString();
                    lblBillDate.Text = dateBill.ToString("ddd, dd MMM yyy");
                    lblCustomerBillNo.Text = dtInvoice.Rows[0]["CustomerBillNo"].ToString();

                    DataTable dtContainers = dmlInvoice.GetContainers(ContainerIDs);
                    if (dtContainers.Rows.Count > 0)
                    {
                        lblPartyName.Text = dtContainers.Rows[0]["BillTo"].ToString();
                        double GrandTotal = 0;
                        string tblInvoiceHTML = string.Empty;
                        foreach (DataRow _drContainers in dtContainers.Rows)
                        {
                            DateTime BiltyDate = Convert.ToDateTime(_drContainers["Date"]);
                            double Freight = Convert.ToDouble(_drContainers["Rate"]);
                            double Lolo = Convert.ToDouble(_drContainers["LOLO"]);
                            double Weighment = Convert.ToDouble(_drContainers["Weighment"]);
                            //double Expenses = Convert.ToDouble(_drContainers["Expenses"]);

                            double Total = Freight + Lolo + Weighment;

                            //double Total = Freight + Expenses;
                            double Expenses = Convert.ToInt64(_drContainers["Expenses"]);
                            double Rate = Convert.ToInt64(_drContainers["Rate"]);

                            double TotalAmount = Expenses + Rate;
                            GrandTotal += TotalAmount;
                            tblInvoiceHTML += "<tr>";
                            tblInvoiceHTML += "<td class=\"text-center\">" + _drContainers["OrderNo"].ToString() + "</td>";
                            tblInvoiceHTML += "<td class=\"text-center\">" + BiltyDate.ToString("dd-MMM-yyy") + "</td>";
                            tblInvoiceHTML += "<td class=\"text-center\">" + _drContainers["ContainerNo"].ToString() + "</td>";
                            tblInvoiceHTML += "<td class=\"text-center\">" + _drContainers["VehicleRegNo"].ToString() + "</td>";
                            tblInvoiceHTML += "<td class=\"text-center\">" + _drContainers["EmptyContainerPickLocation"].ToString() + "</td>";
                            tblInvoiceHTML += "<td class=\"text-center\">" + _drContainers["EmptyContainerDropLocation"].ToString() + "</td>";
                            tblInvoiceHTML += "<td class=\"text-center\">" + String.Format("{0:n0}", _drContainers["Rate"]) + "</td>";
                            tblInvoiceHTML += "<td class=\"text-center\">" + String.Format("{0:n0}", Lolo) + "</td>";
                            tblInvoiceHTML += "<td class=\"text-center\">" + String.Format("{0:n0}", Weighment) + "</td>";
                            //string ExpensesBreakUpHTML = "" +
                            //    "<table style=\"width: 100%; font-size: 10px; \" border=\"1\">" +
                            //        "<thead><tr><th>LoLo</th><th>Weigh</th></tr></thead>" +
                            //        "<tbody>" +
                            //            "<tr>" +
                            //                "<td>" + String.Format("{0:n0}", Lolo) + "</td>" +
                            //                "<td>" + String.Format("{0:n0}", Weighment) + "</td>" +
                            //            "</tr>" +
                            //        "</tbody>" +
                            //    "</table>";
                            //tblInvoiceHTML += "<td class=\"text-center\">" + ExpensesBreakUpHTML + "</td>";

                            //tblInvoiceHTML += "<td class=\"text-center\">" + String.Format("{0:n0}", _drContainers["Expenses"]) + "</td>";
                            
                            tblInvoiceHTML += "<td class=\"text-center\">" + String.Format("{0:n}", TotalAmount) + "</td>";
                            //tblInvoiceHTML += "<td class=\"text-center\"><strong>" + Total.ToString() + "</strong></td>";
                            tblInvoiceHTML += "</tr>";
                        }
                        lblClearingAgent.Text = dtContainers.Rows[0]["ReceivedBy"].ToString();
                        Tbody1.InnerHtml = string.Empty;
                        Tbody1.InnerHtml += tblInvoiceHTML;

                        //lblInvoiceGrandTotal.Text = GrandTotal.ToString();
                        lblInvoiceGrandTotal.Text = "Rs. " + String.Format("{0:n}", GrandTotal) + "/-";
                        //lblInvoiceGrandTotal.Text = GrandTotal.ToString("C", CultureInfo.CreateSpecificCulture("en-PK"));

                        lblAmountinWords.Text = changeNumericToWords(GrandTotal);
                    }

                    lblContainersQty.Text = dtContainers.Rows.Count.ToString();
                }
                modalInvoice.Show();
            }
            else if (e.CommandName == "EditBill")
            {

                try
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    // GridViewRow gvr = gvAllContainers.Rows[index];
                    string CustomerCompany = gvInvoice.DataKeys[index]["CustomerCompany"].ToString();
                    string CustomerCompanyName = gvInvoice.Rows[index].Cells[2].Text.Trim();
                    Label lblOldAmount = gvInvoice.Rows[index].Cells[6].FindControl("lblTotal") as Label;
                    //string OldAmount = gvInvoice.Rows[index].Cells[6].Text.Trim();
                    string BillNo = gvInvoice.DataKeys[index]["BillNo"].ToString();
                    CompanyDML dmlCompany = new CompanyDML();
                    DataTable dtCompany = dmlCompany.GetCompanyByNameBill(CustomerCompany.Trim());
                    Int64 CustomerCompanyID = Convert.ToInt64(dtCompany.Rows[0]["CompanyID"]);
                    hfBillNo.Value = BillNo.Trim();
                    hfCustomerCompanyName.Value = CustomerCompanyName.ToString().Trim();
                    hfOldAmount.Value = lblOldAmount.Text.Trim();
                    InvoiceDML dml = new InvoiceDML();
                    DataTable dtContainers = dml.GetEditContainers(CustomerCompanyID);
                    if (dtContainers.Rows.Count > 0)
                    {
                        gvAllContainers.DataSource = dtContainers;
                    }
                    else
                    {
                        gvAllContainers.DataSource = null;
                    }
                    gvAllContainers.DataBind();

                    //if (gvSelectedContainers.Rows.Count > 0)
                    //{
                    //    if (gvSelectedContainers.DataKeys[0]["CustomerCompanyID"].ToString() != ddlBilToCustomer.SelectedItem.Value)
                    //    {
                    //        gvSelectedContainers.DataSource = null;
                    //        gvSelectedContainers.DataBind();
                    //    }
                    //}

                    modalEditBill.Show();

                }
                catch (Exception ex)
                {

                    throw;
                }
                finally
                {
                    modalEditBill.Show();
                }
            }
            else if(e.CommandName == "DeleteBill")
            {
                try
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    string[] BillNo = gvInvoice.DataKeys[index]["BillNo"].ToString().Split('-');
                    string OnlyBillNo = BillNo[1];
                    string[] CustomerCodeID = gvInvoice.DataKeys[index]["CustCodeID"].ToString().Split('-');
                    string CustomerCode = CustomerCodeID[0].ToString();
                    Int64 CustomerCompanyID = Convert.ToInt64(CustomerCodeID[1]);
                    string CustomerName = gvInvoice.DataKeys[index]["CustomerCompany"].ToString();
                    string CustomerCompanyName = gvInvoice.Rows[index].Cells[2].Text.Trim();

                    string CustomerAccName = CustomerName + "|" + CustomerCode;
                    CompanyDML dmlComp = new CompanyDML();
                    DataTable dtCompany = dmlComp.GetCompany(CustomerCode, CustomerName);
                    Int64 CustomerID = dtCompany.Rows.Count > 0 ? Convert.ToInt64(dtCompany.Rows[0]["CompanyID"].ToString()) : 0;



                    OrderDML dmlOrderCont = new OrderDML();
                    InvoiceDML dml = new InvoiceDML();
                    DataTable dt = dml.GetBillNoByCustomerCompany(OnlyBillNo, CustomerName);
                    double TotalAmount = 0;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Int64 ContainerID = Convert.ToInt64(dt.Rows[i]["OrderConsignmentID"]);
                        double ContainerRate = Convert.ToDouble(dt.Rows[i]["Rate"]);
                        double Expenses = Convert.ToDouble(dt.Rows[i]["LiftOffLiftOnCharges"]);
                        //DataTable dtContainer = dmlOrderCont.GetBiltyContainers(ContainerID);
                       // string Container = dt.Rows.Count > 0 ? (", Container# " + dt.Rows[i]["ContainerNo"].ToString()) : string.Empty;
                        dml.UnBillToContainer(ContainerID);
                    }
                    TotalAmount = Convert.ToDouble(dt.Rows[0]["Total"]);
                    string TransactDescription = "(DELETED)  against Order# " + dt.Rows[0]["OrderNo"].ToString() + "";
                    TransactToCustLedger(CustomerAccName, CustomerCompanyID, TotalAmount, "Credit", TransactDescription);
                    dml.DeleteBill(OnlyBillNo);
                    notification("Success", "Bill Deleted Successfully");
                    GetBills();
                }
                catch (Exception ex)
                {

                    notification("Error", "Error Commanding row, due to: " + ex.Message);
                }
            }
            else if (e.CommandName == "Payment")
            {
                try
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvInvoice.Rows[index];
                    string BillID = gvInvoice.DataKeys[index]["BillNo"].ToString();
                    string CustomerCompany = gvr.Cells[2].Text;
                    hfSelectedBill.Value = BillID.ToString();
                    hfSelectedBillCustomer.Value = gvr.Cells[2].Text;

                    modalPayment.Show();
                }
                catch (Exception ex)
                {
                    notification("Error", "Error commanding row, due to: " + ex.Message);
                }
            }
        }

        protected void gvInvoice_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                this.GetBills(e.SortExpression);
            }
            catch (Exception ex)
            {
                notification("Error", "Error sorting bilties grid, due to: " + ex.Message);
            }
        }

        protected void gvInvoice_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvInvoice.PageIndex = e.NewPageIndex;
                this.GetBills();
            }
            catch (Exception ex)
            {
                notification("Error", "Error changing index of grid page, due to: " + ex.Message);
            }
        }

        #endregion

        #region Linkbutton
        protected void lnkSearchInvoices_Click(object sender, EventArgs e)
        {
            try
            {
                pnlSearchInvoicces.Visible = true;
            }
            catch (Exception ex)
            {
                notification("Error", "Error enabling search invoice, due to: " + ex.Message);
            }
        }

        protected void lnkGenerateInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("CreateInvoice.aspx?lid=" + LoginID);
            }
            catch (Exception ex)
            {
                notification("Error", "Error redirecting to create new invoice, due to: " + ex.Message);
            }
        }

        protected void lnkSearch_Click(object sender, EventArgs e)

        {
            GetBills();
            //try
            //{
            //    string Query = " WHERE ";
            //    InvoiceDML dml = new InvoiceDML();
            //    DataTable dtInvoice = new DataTable();
            //    DataTable dtBill = new DataTable();
            //    if (txtInvoiceNo.Text == string.Empty && ddlBilToCustomer.SelectedIndex == 0 && txtStartDate.Text == string.Empty && txtEndDate.Text == string.Empty)
            //    {
            //        dtInvoice = dml.GetBill();

            //    }
            //    else
            //    {
            //        if (ddlBilToCustomer.SelectedIndex != 0)
            //        {
            //            string[] CompanyString = ddlBilToCustomer.SelectedItem.Text.Split('|');
            //            string Company = CompanyString[1];
            //            Query += "bi.CustomerCompany LIKE '%" + Company + "%' AND ";
            //        }

            //        if (txtStartDate.Text != string.Empty)
            //        {
            //            if (txtEndDate.Text == string.Empty)
            //            {
            //                Query += "Convert(date, bi.CreatedDate) = CONVERT(date, '" + txtStartDate.Text + "') AND ";
            //            }
            //            else
            //            {
            //                Query += "Convert(date, bi.CreatedDate) BETWEEN '" + txtStartDate.Text + "' AND '" + txtEndDate.Text + "' AND ";
            //            }
            //        }

            //        if (txtEndDate.Text != string.Empty && txtStartDate.Text == string.Empty)
            //        {
            //            Query += "Convert(date, bi.CreatedDate) = CONVERT(date, '" + txtEndDate.Text + "') AND ";
            //        }

            //        if (txtInvoiceNo.Text != string.Empty)
            //        {
            //            Query += "bi.BillNo LIKE '%" + txtInvoiceNo.Text + "%' AND ";
            //        }
            //        Query = Query.Substring(0, Query.Length - 5);

            //        dtInvoice = dml.SearchBill(Query);

            //        dtBill.Columns.Add("BillNo");
            //        dtBill.Columns.Add("ActualBillNo");
            //        dtBill.Columns.Add("CustomerCompany");
            //        dtBill.Columns.Add("Total");
            //        dtBill.Columns.Add("TotalBalance");
            //        dtBill.Columns.Add("InvoiceDate");
            //        dtBill.Columns.Add("BiltyDate");
            //        dtBill.Columns.Add("CreditLimit");
            //        dtBill.Columns.Add("TotalContainers");
            //        dtBill.Columns.Add("ShippingLine");
            //        dtBill.Columns.Add("isPaid");
            //        if (dtInvoice.Rows.Count > 0)
            //        {
            //            foreach (DataRow _drBills in dtInvoice.Rows)
            //            {
            //                DateTime CreatedDate = Convert.ToDateTime(_drBills["CreatedDate"]);
            //                dtBill.Rows.Add(
            //                    _drBills["BillNo"].ToString(),
            //                    _drBills["ActualBillNo"].ToString(),
            //                    _drBills["CustomerCompany"].ToString(),
            //                    _drBills["Total"].ToString(),
            //                    (_drBills["TotalBalance"] == DBNull.Value ? "0" : _drBills["TotalBalance"].ToString()),
            //                    CreatedDate.ToString("dd/MM/yyyy"),
            //                    _drBills["BiltyDate"].ToString(),
            //                    _drBills["CreditLimit"],
            //                    _drBills["TotalContainers"].ToString(),
            //                    _drBills["ShippingLine"].ToString(),
            //                    _drBills["isPaid"]
            //                );
            //            }
            //        }



            //    }
            //    if (dtBill.Rows.Count > 0)
            //    {
            //        gvInvoice.DataSource = dtBill;
            //    }
            //    else
            //    {
            //        gvInvoice.DataSource = null;
            //    }

            //    gvInvoice.DataBind();

            //    BindGrid(gvInvoice, dtInvoice);
            //}
            //catch (Exception ex)
            //{
            //    notification("Error", "Error searching, due to: " + ex.Message);
            //}
        }

        protected void lnkClearFilter_Click(object sender, EventArgs e)
        {
            try
            {
                InvoiceDML dml = new InvoiceDML();
                DataTable dtInvoices = dml.GetBill();
                DataTable dtBill = new DataTable();
                dtBill.Columns.Add("CustCodeID");
                dtBill.Columns.Add("ActualBillNo");
                dtBill.Columns.Add("BillNo");
                dtBill.Columns.Add("CustomerCompany");
                dtBill.Columns.Add("Total");
                dtBill.Columns.Add("TotalBalance");
                dtBill.Columns.Add("InvoiceDate");
                dtBill.Columns.Add("BiltyDate");
                dtBill.Columns.Add("CreditLimit");
                dtBill.Columns.Add("TotalContainers");
                dtBill.Columns.Add("ShippingLine");
                dtBill.Columns.Add("isPaid");
                if (dtInvoices.Rows.Count > 0)
                {
                    foreach (DataRow _drBills in dtInvoices.Rows)
                    {
                        DateTime CreatedDate = Convert.ToDateTime(_drBills["CreatedDate"]);
                        dtBill.Rows.Add(
                            _drBills["CustCodeID"].ToString(),
                            _drBills["ActualBillNo"].ToString(),
                            _drBills["BillNo"].ToString(),
                            _drBills["CustomerCompany"].ToString(),
                            _drBills["Total"].ToString(),
                            (_drBills["TotalBalance"] == DBNull.Value ? "0" : _drBills["TotalBalance"].ToString()),
                            CreatedDate.ToString("dd/MM/yyyy"),
                            _drBills["BiltyDate"].ToString(),
                            _drBills["CreditLimit"],
                            _drBills["TotalContainers"].ToString(),
                            _drBills["ShippingLine"].ToString(),
                            _drBills["isPaid"]
                        );
                    }

                    if (dtBill.Rows.Count > 0)
                    {
                        gvInvoice.DataSource = dtBill;
                    }
                    else
                    {
                        gvInvoice.DataSource = null;
                    }


                    gvInvoice.DataBind();

                    txtInvoiceNo.Text = string.Empty;
                    ddlBilToCustomer.ClearSelection();
                    txtStartDate.Text = string.Empty;
                    txtEndDate.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error clearing filter, due to: " + ex.Message);
            }
        }

        public void GetReport()
        {
            try
            {
                //DataTable dtReport = new DataTable();
                //dtReport.Columns.Add("InvoiceDate");
                //dtReport.Columns.Add("BillNo");
                //dtReport.Columns.Add("CustomerCompany");
                //dtReport.Columns.Add("Total");
                //dtReport.Columns.Add("TotalBalance");
                //dtReport.Columns.Add("BiltyDate");
                //dtReport.Columns.Add("TotalContainers");
                //dtReport.Columns.Add("ShippingLine");

                //if (gvInvoice.Rows.Count > 0)
                //{
                //    foreach (GridViewRow _gvrBills in gvInvoice.Rows)
                //    {
                //        Label lblTotal = _gvrBills.FindControl("lblTotal") as Label;
                //        Label lblTotalBalance = _gvrBills.FindControl("lblTotalBalance") as Label;
                //        double Total = Convert.ToDouble(lblTotal.Text.Replace("Rs. ", string.Empty).Replace(",", string.Empty).Replace(".00/-", string.Empty));
                //        double TotalBalance = Convert.ToDouble(lblTotalBalance.Text.Replace("Rs. ", string.Empty).Replace(",", string.Empty).Replace(".00/-", string.Empty));
                //        dtReport.Rows.Add(
                //            _gvrBills.Cells[0].Text,
                //            _gvrBills.Cells[1].Text,
                //            _gvrBills.Cells[2].Text,
                //            Total.ToString(),
                //            TotalBalance.ToString(),
                //            _gvrBills.Cells[3].Text,
                //            _gvrBills.Cells[4].Text,
                //            _gvrBills.Cells[5].Text
                //        );
                //    }

                DataTable dtReport = Session["ReportDataTable"] as DataTable;
                if (dtReport.Rows.Count > 0)
                {
                    rvInvoices.LocalReport.DataSources.Add(new ReportDataSource("InvoicesDataSet", dtReport));
                    rvInvoices.LocalReport.ReportPath = Server.MapPath("~/InvoicesReport.rdlc");
                    rvInvoices.LocalReport.EnableHyperlinks = true;
                }
                //}
            }
            catch (Exception ex)
            {
                notification("Error", "Error Occured Due To : " + ex.Message);
            }
        }
        protected void lnkCloseSearch_Click(object sender, EventArgs e)
        {
            try
            {
                txtInvoiceNo.Text = string.Empty;
                ddlBilToCustomer.ClearSelection();
                txtStartDate.Text = string.Empty;
                txtEndDate.Text = string.Empty;
                pnlSearchInvoicces.Visible = false;
            }
            catch (Exception ex)
            {
                notification("Error", "Error closing invoice search, due to: " + ex.Message);
            }
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
                        string BillNo = hfSelectedBill.Value;
                        string CustomerName = hfSelectedBillCustomer.Value.Trim();
                        string DocumentNo = txtDocumentNo.Text;
                        string TransferTo = cbPettyCash.Checked ? "PettyCash|PC123" : ddlBankAccounts.SelectedItem.Value;
                        string PaymentMode = rbPaymentMode.SelectedItem.Text;
                        double Amount = Convert.ToDouble(txtAmount.Text);
                        DateTime TransactionDate = txtTransactionDate.Text != string.Empty ? DateTime.Parse(txtTransactionDate.Text) : DateTime.Now;
                        bool PettyCash = cbPettyCash.Checked ? true : false;
                        string PaymentDescription = txtDescription.Text;

                        string Description = CustomerName + " Paid " + Amount + " by " + PaymentMode + ",";
                        Description += PaymentMode == "Cheque" ? " (Chueque# " + DocumentNo + ")" : string.Empty;
                        Description += " transfered to " + TransferTo;
                        Description += PaymentDescription == string.Empty ? string.Empty : ", Description: '" + PaymentDescription + "'";

                        InvoiceDML dmlBill = new InvoiceDML();
                        dmlBill.MakePaymentByBill(BillNo, Amount, DocumentNo, TransferTo, PaymentMode, LoginID);
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
                            PettyCashAccountID = dmlAcc.InsertBillToPettyCash(AccountName, BillNo, Description, PettyCashDebit, PettyCashCredit, PettyCashBalance, TransactionDate, LoginID);
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
                                BankAccountID = dmlBankAcc.InsertInAccount(AccountName, BankID, Description, BankAccountDebit, BankAccountCredit, BankAccountBalance, TransactionDate, "Automatic Payment System", "Transfering amount to bank after receiving Payment from Customer", LoginID);
                            }
                        }

                        //Crediting from Customer Account
                        if (PettyCashAccountID > 0 || BankAccountID > 0)
                        {
                            Int64 CompanyID = 0;
                            string CompanyName = hfSelectedBillCustomer.Value.Trim();
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
                        GetBills();
                    }
                }
            }
            catch (Exception ex)
            {
                PaymentNotification("Error", "Error saving payment, due to: " + ex.Message);
                modalPayment.Show();
            }
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            try
            {
                pnlBills.Attributes.Add("style", "display:none");
                pnlSSRSReport.Attributes.Add("style", "display:block");
                GetReport();
            }
            catch (Exception ex)
            {
                notification("Error", "Connot Bind Report Data Due To: " + ex.Message);
            }

        }

        protected void Unnamed_Click(object sender, EventArgs e)
        {

            pnlBills.Attributes.Add("style", "display:block");
            pnlSSRSReport.Attributes.Add("style", "display:none");
        }

        protected void lnkPnlReportWithDetails_Click(object sender, EventArgs e)
        {

        }

        protected void lnkEditBill_Click(object sender, EventArgs e)
        {
            try
            {
                int count = 0;
                for (int i = 0; i < gvAllContainers.Rows.Count; i++)
                {
                    CheckBox cbSelect = gvAllContainers.Rows[i].Cells[0].FindControl("cbSelect") as CheckBox;
                    if (cbSelect.Checked)
                    {
                        count++;
                    }
                }
                if (count > 0)
                {
                    DataTable dtSelectedContainers = new DataTable();
                    dtSelectedContainers.Columns.Add("OrderConsignmentID");
                    dtSelectedContainers.Columns.Add("ContainerNo");
                    dtSelectedContainers.Columns.Add("RecordedDate");
                    dtSelectedContainers.Columns.Add("ShippingLine");
                    dtSelectedContainers.Columns.Add("AssignedVehicle");
                    dtSelectedContainers.Columns.Add("EmptyContainerPickLocation");
                    dtSelectedContainers.Columns.Add("EmptyContainerDropLocation");
                    dtSelectedContainers.Columns.Add("BillNo");
                    dtSelectedContainers.Columns.Add("CustomerCompanyName");
                    dtSelectedContainers.Columns.Add("Rate");
                    string[] BillNo = hfBillNo.Value.Split('-');
                    string CustomerCompanyName = hfCustomerCompanyName.Value.Trim();
                    double rate = 0;
                    for (int i = 0; i < gvAllContainers.Rows.Count; i++)
                    {
                        CheckBox cbSelect = gvAllContainers.Rows[i].Cells[0].FindControl("cbSelect") as CheckBox;
                        if (cbSelect.Checked)
                        {
                            rate += Convert.ToDouble(gvAllContainers.DataKeys[i]["Rate"]);
                        }
                    }
                    string OldAmountClearRs = hfOldAmount.Value.Replace("Rs.", string.Empty);
                    string PureOldAmount = OldAmountClearRs.Replace("/-", string.Empty);
                    double TotalAmount = rate + Convert.ToDouble(PureOldAmount.Trim());
                    InvoiceDML dml = new InvoiceDML();

                    CompanyDML dmlComp = new CompanyDML();
                    for (int i = 0; i < gvAllContainers.Rows.Count; i++)
                    {
                        CheckBox cbSelect = gvAllContainers.Rows[i].Cells[0].FindControl("cbSelect") as CheckBox;
                        if (cbSelect.Checked)
                        {
                            Int64 ContainerID = Convert.ToInt64(gvAllContainers.DataKeys[i]["OrderConsignmentID"]);
                            Int64 CustomerCompanyID = Convert.ToInt64(gvAllContainers.DataKeys[i]["CustomerCompanyID"]);
                            //double Rate = Convert.ToDouble(gvAllContainers.DataKeys[i]["Rate"]);
                            string ContainerNo = gvAllContainers.Rows[i].Cells[1].Text;
                            string Date = gvAllContainers.Rows[i].Cells[2].Text;
                            string ShippingLine = gvAllContainers.Rows[i].Cells[3].Text;
                            string VehicleRegNo = gvAllContainers.Rows[i].Cells[4].Text;
                            string Pickup = gvAllContainers.Rows[i].Cells[5].Text;
                            string Dropoff = gvAllContainers.Rows[i].Cells[6].Text;
                            double Rate = Convert.ToDouble(gvAllContainers.DataKeys[i]["Rate"]);
                            dtSelectedContainers.Rows.Add(ContainerID, ContainerNo, Date, ShippingLine, VehicleRegNo, Pickup, Dropoff, BillNo[1], CustomerCompanyName, TotalAmount);
                            Int64 BillID = dml.InsertBillFromEditBill(BillNo[1], string.Empty, CustomerCompanyName, ContainerID, TotalAmount, LoginID);
                            if (BillID > 0)
                            {
                                dml.UpdateOrderConsignmentIsBilled(ContainerID);



                                DataTable dtCustomer = dmlComp.GetCompany(CustomerCompanyID);

                                if (dtCustomer.Rows.Count > 0)
                                {
                                    string CustomerName = dtCustomer.Rows[0]["CompanyName"].ToString();
                                    string CustomerCode = dtCustomer.Rows[0]["CompanyCode"].ToString();

                                    string[] CustomerNameString = ddlBilToCustomer.SelectedItem.Text.Split('|');
                                    string CustomerAccName = CustomerName + "|" + CustomerCode;
                                    AccountsDML dmlAcc = new AccountsDML();
                                    DataTable dtCustAccountCheck = dmlAcc.GetAccounts(CustomerAccName);
                                    if (dtCustAccountCheck.Rows.Count <= 0)
                                        dmlAcc.CreateAccount(CustomerAccName);
                                    double Debit = Rate;
                                    double Credit = 0;
                                    double Balance = 0;
                                    DataTable dtCustAccount = dmlAcc.GetInAccounts(CustomerAccName);
                                    if (dtCustAccount.Rows.Count > 0)
                                    {
                                        Balance = Convert.ToDouble(dtCustAccount.Rows[dtCustAccount.Rows.Count - 1]["Balance"]);
                                    }
                                    Balance = Balance + Debit - Credit;

                                    DataTable dtCompany = dmlComp.GetCompany(CustomerCode, CustomerName);
                                    Int64 CustomerID = dtCompany.Rows.Count > 0 ? Convert.ToInt64(dtCompany.Rows[0]["CompanyID"].ToString()) : 0;

                                    OrderDML dmlOrderCont = new OrderDML();
                                    DataTable dtContainer = dmlOrderCont.GetBiltyContainers(ContainerID);
                                    string Container = dtContainer.Rows.Count > 0 ? (", Container# " + dtContainer.Rows[0]["ContainerNo"].ToString()) : string.Empty;
                                    dmlAcc.InsertInAccount(CustomerAccName, CustomerID, "Container Added after bill created" + Container + " against Order# " + dtContainer.Rows[0]["OrderNo"].ToString() + "", Debit, Credit, Balance, LoginID);

                                }



                            }
                            // GetBills();
                        }
                    }
                    dml.UpdateBillFromEdit(BillNo[1], TotalAmount, LoginID);
                    GetBills();
                }
                else
                {
                    notification("Error", "Atleast one container is requird to generate bill");
                }

            }
            catch (Exception ex)
            {
                notification("Error", "Cannot edit bill due to : " + ex.Message);
            }
        }

        protected void lnkCloseEditBill_Click(object sender, EventArgs e)
        {
            try
            {
                modalEditBill.Hide();
            }
            catch (Exception ex)
            {

                throw;
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