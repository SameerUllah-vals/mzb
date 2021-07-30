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
    public partial class Invoice : System.Web.UI.Page
    {
        //#region Members

        //int loginid;

        //#endregion

        //#region Properties

        //public int LoginID
        //{
        //    get
        //    {
        //        if (Request.QueryString["lid"] != string.Empty && Request.QueryString["lid"] != null)
        //        {
        //            loginid = Convert.ToInt32(Request.QueryString["lid"].ToString());
        //        }
        //        return loginid;

        //    }
        //}

        //#endregion

        //#region Helper Methods

        //private void CheckBoxList(DataTable dt, CheckBoxList _cbl, string _cblValue, string _cblText, string _cblDefaultText)
        //{
        //    if (dt.Rows.Count > 0)
        //    {
        //        _cbl.DataSource = dt;

        //        _cbl.DataValueField = _cblValue;
        //        _cbl.DataTextField = _cblText;

        //        _cbl.DataBind();
        //    }
        //}

        //#endregion

        //#region Page Load

        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    notification("", "");
        //    if (!IsPostBack)
        //    {
        //        try
        //        {
        //            InvoiceDML dmlInvoice = new InvoiceDML();
        //            DataTable dtInvoice = dmlInvoice.GetCompletedContainers();
        //            if (dtInvoice.Rows.Count > 0)
        //            {
        //                gvAllContainers.DataSource = dtInvoice;
        //            }
        //            else
        //            {
        //                gvAllContainers.DataSource = null;
        //            }
        //            gvAllContainers.DataBind();
        //        }
        //        catch (Exception ex)
        //        {
        //            notification("Error", "Error getting completed containers, due to: " + ex.Message);
        //        }
        //    }
        //}

        //#endregion

        //#region Custom Methods

        //public void notification()
        //{
        //    try
        //    {
        //        divNotification.InnerHtml = string.Empty;
        //    }
        //    catch (Exception ex)
        //    {
        //        divNotification.InnerHtml = "<div class=\"alert alert-danger\">" + ex.Message + "</div>";
        //    }
        //}

        //public void notification(string type, string msg)
        //{
        //    try
        //    {
        //        if (type == "Error")
        //        {
        //            divNotification.InnerHtml = "<div class=\"alert alert-danger\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
        //        }
        //        else if (type == "Success")
        //        {
        //            divNotification.InnerHtml = "<div class=\"alert alert-success\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
        //        }
        //        else if (type == string.Empty)
        //        {
        //            divNotification.InnerHtml = string.Empty;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        divNotification.InnerHtml = "<div class=\"alert alert-danger\">" + ex.Message + "</div>";
        //    }
        //}

        //#endregion

        //#region Events

        //protected void lnkSearch_Click(object sender, EventArgs e)
        //{
        //    try
        //    {

        //    }
        //    catch (Exception ex)
        //    {
        //        notification("Error", "Error searching/binding completed containers, due to: " + ex.Message);
        //    }
        //}

        //#endregion
    }
}