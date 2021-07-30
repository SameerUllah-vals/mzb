using BLL;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiltySystem
{
    public partial class Invoice_OLD : System.Web.UI.Page
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
            notification("", "");
            Session["InvoiceContent"] = string.Empty;
            if (!IsPostBack)
            {
                if (LoginID > 0)
                {
                    this.Title = "Invoice";
                    GetCompany();

                }

            }
        }

        #endregion

        #region Helper Methdos

        private void FillDropDown(DataTable dt, DropDownList _ddl, string _ddlValue, string _ddlText, string _ddlDefaultText)
        {
            if (dt.Rows.Count > 0)
            {
                _ddl.DataSource = dt;

                _ddl.DataValueField = _ddlValue;
                _ddl.DataTextField = _ddlText;

                _ddl.DataBind();

                System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem();

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

        public void GetCompany()
        {
            try
            {
                InvoiceDML Invoice = new InvoiceDML();
                DataTable dtInvoice = Invoice.GetCompany();
                if (dtInvoice.Rows.Count > 0)
                {
                    FillDropDown(dtInvoice, ddlSender, "CompanyID", "CompanyName", "-Select-");
                    FillDropDown(dtInvoice, ddlReciver, "CompanyID", "CompanyName", "-Select-");
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting/binding Companies, due to: " + ex.Message);
            }
        }

        public void check(string Content)
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(Content);
                    //Export HTML String as PDF.
                    StringReader sr = new StringReader(sb.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    htmlparser.Parse(sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=LocalTripGaatePass_" + DateTime.Now.ToString() + ".pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                    pnlInput.Visible = true;
                }
            }
        }

        #endregion

        #region Web Methods

        [WebMethod]
        public static void GenerateInvoicePDF(string Content)
        {
            //Dummy data for Invoice (Bill).

            Invoice i = new Invoice();
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(Content);
                    //Export HTML String as PDF.
                    StringReader sr = new StringReader(sb.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
                    pdfDoc.Open();
                    htmlparser.Parse(sr);
                    pdfDoc.Close();
                    HttpContext.Current.Response.ContentType = "application/pdf";
                    HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=LocalTripGaatePass_" + DateTime.Now.ToString() + ".pdf");
                    HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    HttpContext.Current.Response.Write(pdfDoc);
                    HttpContext.Current.Response.End();
                    //i.pnlInput.Visible = true;
                }
            }
        }

        #endregion

        #region Events

        protected void lnkSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlSender.SelectedIndex != 0 || ddlReciver.SelectedIndex != 0 || txtDateFrom.Text != string.Empty || txtDateTo.Text != string.Empty || txtBiltyNo.Text != string.Empty)
                {

                    string Sender = ddlSender.SelectedIndex == 0 ? string.Empty : "'" + ddlSender.SelectedItem.Text + "'";
                    Int64 SenderCompanyID = ddlSender.SelectedIndex == 0 ? 0 : Convert.ToInt64(ddlSender.SelectedItem.Value);
                    string Reciver = ddlReciver.SelectedIndex == 0 ? string.Empty : "'" + ddlReciver.SelectedItem.Text + "'";
                    Int64 ReceiverCompanyID = ddlReciver.SelectedIndex == 0 ? 0 : Convert.ToInt64(ddlReciver.SelectedItem.Value);
                    string Biltyno = txtBiltyNo.Text == string.Empty ? string.Empty : "'%" + txtBiltyNo.Text + "%'";

                    string ExtendedQuery = string.Empty;
                    ExtendedQuery = " WHERE ";
                    ExtendedQuery += Sender == string.Empty ? string.Empty : " o.SenderCompanyID = " + SenderCompanyID + " AND ";
                    ExtendedQuery += Reciver == string.Empty ? string.Empty : " o.ReceiverCompanyID = " + ReceiverCompanyID + " AND ";
                    ExtendedQuery += Biltyno == string.Empty ? string.Empty : " o.OrderNo LIKE " + Biltyno + " AND ";
                    if (txtDateFrom.Text != string.Empty && txtDateTo.Text != string.Empty)
                    {
                        string DateFrom = txtDateFrom.Text == string.Empty ? string.Empty : "'" + txtDateFrom.Text + " 00:00:00'";
                        string DateTo = txtDateTo.Text == string.Empty ? string.Empty : "'" + txtDateTo.Text + " 23:59:59'";

                        ExtendedQuery += " o.RecordedDate Between " + DateFrom + " AND " + DateTo + " AND ";
                    }

                    ExtendedQuery = ExtendedQuery.Substring(0, ExtendedQuery.Length - 4);

                    InvoiceDML invoice = new InvoiceDML();
                    DataTable dtinvoice = invoice.GetOrder(ExtendedQuery);

                    if (dtinvoice.Rows.Count > 0)
                    {
                        gvResult.DataSource = dtinvoice;
                        gvResult.DataBind();
                    }
                }
                else
                {
                    notification("Error", "Please Select first what you want to search?");
                }
            }
            catch (Exception ex)
            {

                notification("Error", "Error with fetching data due to: " + ex.Message);
            }
        }

        protected void gvResult_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "View")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow gvr = gvResult.Rows[index];
                Int64 ID = Convert.ToInt64(gvResult.DataKeys[index]["OrderID"]);


                InvoiceDML dml = new InvoiceDML();
                DataTable dt = dml.GetOrder(ID);
                DataTable dtCosigment = dml.GetOrderConsigment(ID);

                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["PaymentType"].ToString().ToLower() == "container wise")
                    {
                        modalConfirm.Show();
                        lblSenderCompany.Text = dt.Rows[0]["Sender"].ToString();
                        //lblSenderGroup.Text = dt.Rows[0]["SenderGroup"].ToString();
                        //lblSenderDepartment.Text = dt.Rows[0]["SenderDepartment"].ToString();
                        lblReciverCompany.Text = dt.Rows[0]["Receiver"].ToString();
                        //lblReciverGroup.Text = dt.Rows[0]["ReceiverGroup"].ToString();
                        //lblReciverDepartment.Text = dt.Rows[0]["ReceiverDepartment"].ToString();
                        lblCustomerCompany.Text = dt.Rows[0]["Customer"].ToString();
                        //lblCustomerGroup.Text = dt.Rows[0]["CustomerGroup"].ToString();
                        //lblCustomerDepartment.Text = dt.Rows[0]["CustomerDepartment"].ToString();
                        lblRecordedDate.Text = dt.Rows[0]["RecordedDate"].ToString();
                        lblOrderNo.Text = dt.Rows[0]["OrderNo"].ToString();
                        lblDetail.Text = dtCosigment.Rows[0]["ContainerTypeName"].ToString() + " From " + dtCosigment.Rows[0]["EmptyContainerPickLocation"].ToString() + " To " + dtCosigment.Rows[0]["EmptyContainerDropLocation"].ToString();
                        lblNoOfPackage.Text = dtCosigment.Rows[0]["ContainerSize"].ToString() + " ft ";
                        lblQuantity.Text = dtCosigment.Rows.Count.ToString();
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "name", "<script> return alert(); </script>",true);
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyFun1", "GetContent();", true);
                        // Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "GetContent()", true);


                    }
                }
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            modalConfirm.Hide();
        }

        protected void GenerateInvoicePDFss(object sender, EventArgs e)
        {
            //Dummy data for Invoice (Bill).


            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(Session["InvoiceContent"]);
                    //sb.Append("<table border=\"1\" style=\"border: 1px solid black;\">");
                    //sb.Append("<tr>");
                    //sb.Append("<td colspan = \"4\" align = \"center\">");
                    //sb.Append("<h3> <strong>Awan Goods Transport Company</strong> </h3>");
                    //sb.Append("</td>");
                    //sb.Append("</tr>");
                    //sb.Append("</table> ");
                    //Export HTML String as PDF.
                    StringReader sr = new StringReader(sb.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    htmlparser.Parse(sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=LocalTripGaatePass_" + DateTime.Now.ToString() + ".pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();



                }
            }
        }

        protected void btnPrints_Click(object sender, EventArgs e)
        {
            try
            {
                GenerateInvoicePDFss(sender, e);

            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion
    }
}