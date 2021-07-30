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
    public partial class MonthlyVehicleReport : System.Web.UI.Page
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

        #region HelpMethods
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

        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            notification();

            if (!IsPostBack)
            {
                if (LoginID > 0)
                {
                    this.Title = "Monthly Vehicle Report";

                    GetSearchVehicle();
                    GetVehicle();
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
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

        public void GetVehicle()
        {
            try
            {
                VehicleDML dml = new VehicleDML();
                DataTable dt = dml.GetVehicle();
                if (dt.Rows.Count > 0)
                {
                    FillDropDown(dt, ddlVehicleReg, "VehicleID", "RegNo", "-Select Vehicle-");
                }
                else
                {

                    notification("Error", "");
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting/populating Vehicle, due to: " + ex.Message);
            }
        }
        public void GetSearchVehicle()
        {
            try
            {
                TripDML dml = new TripDML();
                DataTable dt = dml.GetSearchVehicle();
                if (dt.Rows.Count > 0)
                {
                    gvResult.DataSource = dt;
                }
                else
                {
                    gvResult.DataSource = null;
                }
                gvResult.DataBind();
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting Search Vehicle, due to: " + ex.Message);
            }
        }

        public void ClearFields()
        {
            try
            {
                hfEditID.Value = string.Empty;
                txtFromDate.Text = string.Empty;
                txtToDate.Text = string.Empty;
                pnlInput.Visible = false;
            }
            catch (Exception ex)
            {
                notification("Error", "Error clearing fields, due to: " + ex.Message);
            }
        }

        public void ExportToExcel(DataTable _dt, string FileName)
        {
            try
            {
                string attachment = "attachment; filename=" + FileName + "_" + DateTime.Now + ".xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.ms-excel";
                string tab = "";

                foreach (DataColumn dc in _dt.Columns)
                {
                    Response.Write(tab + dc.ColumnName);
                    tab = "\t";
                }

                Response.Write("\n");

                int i;
                foreach (DataRow dr in _dt.Rows)
                {
                    tab = "";
                    for (i = 0; i < _dt.Columns.Count; i++)
                    {
                        Response.Write(tab + dr[i].ToString());
                        tab = "\t";


                    }
                    Response.Write("\n");
                }

                Response.End();
            }
            catch (Exception ex)
            {
                notification("Error", "Error exporting excel, due to: " + ex.Message);
            }
        }

        public DataTable GridToDT(GridView _gv)
        {
            DataTable _dt = new DataTable();
            for (int i = 0; i < _gv.HeaderRow.Cells.Count; i++)
            {
                if (_gv.HeaderRow.Cells[i].Text == "&nbsp;" || _gv.HeaderRow.Cells[i].Text == string.Empty)
                {

                }
                else
                {
                    _dt.Columns.Add(_gv.HeaderRow.Cells[i].Text);
                }
            }

            foreach (GridViewRow row in _gv.Rows)
            {
                DataRow _dr = _dt.NewRow();
                for (int j = 0; j < _gv.HeaderRow.Cells.Count; j++)
                {
                    if (_gv.HeaderRow.Cells[j].Text == "&nbsp;" || _gv.HeaderRow.Cells[j].Text == string.Empty)
                    {

                    }
                    else
                    {
                        _dr[j] = row.Cells[j].Text;
                    }
                }
                _dt.Rows.Add(_dr);
            }
            return _dt;
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

        public string GetAuto()
        {
            VoucherDML dml = new VoucherDML();
            DataTable dt = dml.GetAutoVoucher();
            string lastVoucher = dt.Rows[0]["Voucher"].ToString();
            return lastVoucher;

        }



        #endregion

        #region Event

        #region LinkButton
        protected void lnkCloseInput_Click(object sender, EventArgs e)
        {
            try
            {
                pnlInput.Visible = false;
            }
            catch (Exception ex)
            {

                notification("Error", ex.Message);
            }
        }

        protected void lnkSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtFromDate.Text == string.Empty)
                {
                    notification("Error", "Please enter From Date");
                    txtFromDate.Focus();
                }
                else if (txtToDate.Text == string.Empty)
                {
                    notification("Error", "Please enter ToDate");
                    txtToDate.Focus();
                }

                string TripStart = txtFromDate.Text.Trim();
                string TripEnd = txtToDate.Text.Trim();
                string RegNo = ddlVehicleReg.SelectedItem.Text;
                TripDML dml = new TripDML();
                string QueryConditions = string.Empty;

                if (ddlVehicleReg.SelectedIndex != 0 )
                {
                    QueryConditions += " AND v.RegNo = '" + RegNo + "'";
                }
                DataTable dt = dml.SearchVehicle(TripStart, TripEnd, QueryConditions);
                if (dt.Rows.Count > 0)
                {
                    gvResult.DataSource = dt;
                }
                else
                {
                    gvResult.DataSource = null;
                }
                gvResult.DataBind();
                //GetSearchVehicle();
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        protected void lnkAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                pnlInput.Visible = true;
            }
            catch (Exception ex)
            {

                notification("Error", ex.Message);
            }
        }

        #endregion

        #endregion
    }
}