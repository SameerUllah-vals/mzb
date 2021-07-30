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
    public partial class ExpensesType : System.Web.UI.Page
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

        private void GetExpensesType()
        {
            ExpensesTypeDML dml = new ExpensesTypeDML();

            DataTable dt = dml.GetExpensesType();
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

        private void clearfield()
        {
            hfEditID.Value = string.Empty;
            txtName.Text = "";
            txtCode.Text = "";
            txtDescription.Text = "";

        }

        #region Page Load

        protected void Page_Load(object sender, EventArgs e)
        {
            notification();
            if (!IsPostBack)
            {
                notification();
                if (!IsPostBack)
                {
                    if (LoginID > 0 && LoginID != null)
                    {
                        Title = "ExpensesType";
                        GetExpensesType();
                    }
                    else
                    {
                        Response.Redirect("Login.aspx");
                    }
                }

            }
        }

        #endregion

        #region Helper Methods

        #endregion



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
                    _dt.Columns.Add(_gv.HeaderRow.Cells[i].Text.Replace("&nbsp;", string.Empty));
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
                        _dr[j] = row.Cells[j].Text.Replace("&nbsp;", string.Empty);
                    }
                }
                _dt.Rows.Add(_dr);
            }
            return _dt;
        }


        protected void lnkCloseView_Click(object sender, EventArgs e)
        {
            try
            {
                pnlView.Visible = false;
            }
            catch (Exception ex)
            {

                notification("Error", ex.Message);
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
        protected void lnkSubmit_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtCode.Text == string.Empty)
                {
                    notification("Error", "Please enter Code.");
                    txtCode.Focus();
                }
                else if (txtName.Text == string.Empty)
                {
                    notification("Error", "Please enter Name.");
                    txtName.Focus();
                }
                else
                {

                    string Code = txtCode.Text;
                    string name = txtName.Text;
                    string des = txtDescription.Text;

                    ExpensesTypeDML cat = new ExpensesTypeDML();
                    DataTable dt = cat.GetExpensesType(Code, name);
                    if (hfEditID.Value.ToString() == string.Empty)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            notification("Error", "Another ExpensesType with same name or code exists in database, try to change name or code");
                        }
                        else
                        {
                            ConfirmModal("Are you sure want to Save?", "Save");

                        }
                    }
                    else
                    {
                        if (dt.Rows.Count > 1)
                        {
                            notification("Error", "Another ExpensesType with same name or code exists in database, try to change name or code");
                        }
                        else
                        {
                            ConfirmModal("Are you sure want to Update?", "Update");


                        }
                    }
                }
            }
            catch (Exception ex)
            {

                notification("Error", ex.Message);
            }
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            try
            {
                ConfirmModal("Are you sure want to delete?", "Delete");

            }
            catch (Exception ex)
            {

                notification("error", "Error with Deleting due to:" + ex.Message);
            }

            GetExpensesType();
            pnlInput.Visible = false;
        }

        protected void lnkCancelSearch_Click(object sender, EventArgs e)
        {
            try
            {
                GetExpensesType();
                txtSearch.Text = string.Empty;
            }
            catch (Exception ex)
            {

                notification("Error", ex.Message);
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

        protected void gvResult_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Change")
            {
                pnlInput.Visible = true;
                pnlView.Visible = false;
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow gvr = gvResult.Rows[index];
                Int64 ExpensesTypeID = Convert.ToInt64(gvResult.DataKeys[index]["ExpensesTypeID"]);
                hfEditID.Value = ExpensesTypeID.ToString();


                ExpensesTypeDML dml = new ExpensesTypeDML();
                DataTable dtcat = dml.GetExpensesType(ExpensesTypeID);
                if (dtcat.Rows.Count > 0)
                {
                    txtCode.Text = dtcat.Rows[0]["ExpensesTypeCode"].ToString();
                    txtName.Text = dtcat.Rows[0]["ExpensesTypeName"].ToString();
                    txtDescription.Text = dtcat.Rows[0]["Remarks"].ToString();



                    lnkDelete.Visible = true;
                }
            }
            else if (e.CommandName == "View")
            {

                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);

                //ClearFields();
                pnlInput.Visible = false;
                pnlView.Visible = true;

                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow gvr = gvResult.Rows[index];
                Int64 ID = Convert.ToInt64(gvResult.DataKeys[index]["ExpensesTypeID"]);



                ExpensesTypeDML dml = new ExpensesTypeDML();
                DataTable dtcat = dml.GetExpensesType(ID);
                if (dtcat.Rows.Count > 0)
                {
                    lblCode.Text = dtcat.Rows[0]["ExpensesTypeCode"].ToString();
                    lblName.Text = dtcat.Rows[0]["ExpensesTypeName"].ToString();
                    lblDescription.Text = dtcat.Rows[0]["Remarks"].ToString();




                }
            }
            else if (e.CommandName == "Active")
            {

                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow gvr = gvResult.Rows[index];
                Int64 CatId = Convert.ToInt64(gvResult.DataKeys[index]["ExpensesTypeID"]);
                string Active = gvResult.DataKeys[index]["isActive"].ToString() == string.Empty ? "False" : gvResult.DataKeys[index]["isActive"].ToString();
                ExpensesTypeDML dml = new ExpensesTypeDML();

                hfEditID.Value = CatId.ToString();

                if (Active == "True")
                {
                    dml.DeactivateExpence(CatId, LoginID);
                }
                else
                {
                    dml.ActivateExpence(CatId, LoginID);
                }
                GetExpensesType();
                clearfield();

            }
        }

        protected void gvResult_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView rowView = (DataRowView)e.Row.DataItem;
                    bool Active = Convert.ToBoolean(rowView["isActive"].ToString() == string.Empty ? "false" : rowView["isActive"]);
                    LinkButton lnkActive = e.Row.FindControl("lnkActive") as LinkButton;
                    if (Active == true)
                    {
                        lnkActive.CssClass = "fas fa-toggle-on";
                        lnkActive.ForeColor = Color.DodgerBlue;
                        lnkActive.ToolTip = "Switch to Deactivate";

                    }
                    else
                    {
                        lnkActive.CssClass = "fas fa-toggle-off";
                        lnkActive.ForeColor = Color.Maroon;
                        lnkActive.ToolTip = "Switch to Activate";
                        e.Row.BackColor = Color.LightPink;
                    }
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Binding data to grid, due to: " + ex.Message);
            }
        }

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
            string keyword = txtSearch.Text.Trim();
            if (keyword != string.Empty)
            {
                ExpensesTypeDML dm = new ExpensesTypeDML();
                DataTable dt = dm.GetExpensesType(keyword);
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
            else
            {
                notification("Error", "Please enter keyword to search");
            }
        }

        protected void lnkConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                string Action = hfConfirmAction.Value;
                if (Action == "Delete")
                {
                    try
                    {
                        Int64 id = Convert.ToInt64(hfEditID.Value);
                        ExpensesTypeDML dml = new ExpensesTypeDML();
                        dml.DeleteExpensesType(id);
                        clearfield();
                        lnkDelete.Visible = false;
                        notification("Success", "Deleted Successfully");
                        GetExpensesType();
                        pnlInput.Visible = false;
                    }
                    catch (Exception ex)
                    {

                        notification("Error", ex.Message);
                    }
                   
                }
                else if (Action == "Save")
                {
                    try
                    {
                        string Code = txtCode.Text;
                        string name = txtName.Text;
                        string des = txtDescription.Text;

                        ExpensesTypeDML cat = new ExpensesTypeDML();
                        cat.InsertExpensesType(Code, name, des, LoginID);
                        pnlInput.Visible = false;
                        notification("Success", "Submited Successfully");
                        clearfield();
                        GetExpensesType();
                    }
                    catch (Exception ex)
                    {

                        notification("Error", ex.Message);
                    }

                }
                else if (Action == "Update")
                {
                    try
                    {
                        string Code = txtCode.Text;
                        string name = txtName.Text;
                        string des = txtDescription.Text;

                        ExpensesTypeDML cat = new ExpensesTypeDML();
                        Int64 id = Convert.ToInt64(hfEditID.Value);
                        cat.UpdateExpensesType(id, Code, name, des, LoginID);
                        pnlInput.Visible = false;
                        notification("Success", "Updated Successfully");
                        clearfield();
                        GetExpensesType();
                    }
                    catch (Exception ex)
                    {

                        notification("Error", ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error confirming, due to: " + ex.Message);
            }
        }

        protected void lnkCloseInput_Click1(object sender, EventArgs e)
        {

        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {

        }
    }
}