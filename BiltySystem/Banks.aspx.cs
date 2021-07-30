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
    public partial class Banks : System.Web.UI.Page
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
            notification("", "");
            if (!IsPostBack)
            {
                if (LoginID > 0)
                {
                    this.Title = "Banks";
                    GetBanks();
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

        public void GetBanks()
        {
            try
            {
                BanksDML dml = new BanksDML();
                DataTable dtBank = dml.GetBank();
                if (dtBank.Rows.Count > 0)
                {
                    gvResult.DataSource = dtBank;
                }
                else
                {
                    gvResult.DataSource = null;
                }
                gvResult.DataBind();
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting Banks, due to: " + ex.Message);
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
                hfEditID.Value = string.Empty;
                txtCode.Text = "";
                ddlBank.ClearSelection();
                txtAccountNo.Text = string.Empty;
                txtAccountTitle.Text = string.Empty;
            }
            catch (Exception ex)
            {
                notification("Error", "Error clearing fields, due to: " + ex.Message);
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

        protected void lnkSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCode.Text == string.Empty)
                {
                    notification("Error", "Plese enter bank code");
                    txtCode.Focus();
                }
                else if (ddlBank.SelectedIndex == 0)
                {
                    notification("Error", "Plese select bank");
                    ddlBank.Focus();
                }
                else if (txtAccountNo.Text == string.Empty)
                {
                    notification("Error", "Plese enter bank account number");
                    txtAccountNo.Focus();
                }
                else if (txtAccountTitle.Text == string.Empty)
                {
                    notification("Error", "Plese enter bank account title");
                    txtAccountTitle.Focus();
                }
                else
                {
                    string Code = txtCode.Text.Trim();
                    string Bank = ddlBank.SelectedItem.Text;
                    string AccountNo = txtAccountNo.Text.Trim();
                    string AccountTitle = txtAccountTitle.Text.Trim();

                    if (hfEditID.Value == string.Empty)
                    {
                        ConfirmModal("Are you sure want to save Bank with provided info?", "Save");
                    }
                    else
                    {
                        ConfirmModal("Are you sure want to save Bank with provided info?", "Update");
                    }
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error submitting data, due to: " + ex.Message);
            }
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {

        }

        protected void lnkAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                pnlInput.Visible = true;
            }
            catch (Exception ex)
            {
                notification("Error", "Error enabling Input Panel due to: " + ex.Message);
            }
        }

        protected void lnkConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                string Action = hfConfirmAction.Value;
                BanksDML dml = new BanksDML();
                if (Action == "Save")
                {
                    string Code = txtCode.Text.Trim();
                    string Bank = ddlBank.SelectedItem.Text;
                    string AccountNo = txtAccountNo.Text.Trim();
                    string AccountTitle = txtAccountTitle.Text.Trim();

                    Int64 BankID = dml.InsertBank(Code, Bank, AccountNo, AccountTitle, LoginID);
                    if (BankID > 0)
                    {
                        ClearFields();
                        pnlInput.Visible = false;
                        GetBanks();
                    }
                }
                else if (Action == "Update")
                {
                    Int64 BankID = Convert.ToInt64(hfEditID.Value);
                    string Code = txtCode.Text.Trim();
                    string Bank = ddlBank.SelectedItem.Text;
                    string AccountNo = txtAccountNo.Text.Trim();
                    string AccountTitle = txtAccountTitle.Text.Trim();
                    dml.UpdateBank(BankID, Code, Bank, AccountNo, AccountTitle, LoginID);
                    ClearFields();
                    pnlInput.Visible = false;
                    GetBanks();
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error confirming, due to: " + ex.Message);
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
                notification("Error", "Error enabling Input Panel due to: " + ex.Message);
            }
        }

        protected void lnkCancelSearch_Click(object sender, EventArgs e)
        {
            try
            {
                GetBanks();
                txtSearch.Text = string.Empty;
            }
            catch (Exception ex)
            {

                notification("Error", ex.Message);
            }
        }

        protected void lnkSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();
            BanksDML dml = new BanksDML();

            DataTable dt = dml.GetBank(keyword);
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

        protected void gvResult_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    LinkButton lnkActive = e.Row.FindControl("lnkActive") as LinkButton;
                    DataRowView rowView = (DataRowView)e.Row.DataItem;
                    bool IsPaid = gvResult.DataKeys[e.Row.RowIndex].Values["isActive"].ToString() == "True" ? true : false;

                    lnkActive.CssClass = IsPaid == true ? "fas fa-toggle-on" : "fas fa-toggle-off";
                    lnkActive.ToolTip = IsPaid == true ? "Click to Deactivate" : "Click to Activate";
                    lnkActive.ForeColor = IsPaid == true ? Color.DodgerBlue : Color.Maroon;

                    e.Row.BackColor = IsPaid == true ? Color.White : Color.LightPink;
                }
                catch (Exception ex)
                {

                    notification("Error", "Error binding bills, due to: " + ex.Message);
                }
            }
        }

        protected void gvResult_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Active")
            {

                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow gvr = gvResult.Rows[index];
                Int64 BankID = Convert.ToInt64(gvResult.DataKeys[index]["BankID"]);
                bool Active = gvResult.DataKeys[index]["isActive"].ToString() == "True" ? true : false;
                BanksDML dml = new BanksDML();

                hfEditID.Value = BankID.ToString();

                if (Active == true)
                {
                    dml.DeactivateBank(BankID, LoginID);
                }
                else
                {
                    dml.ActivateBank(BankID, LoginID);
                }
                GetBanks();
                ClearFields();

            }
            else if(e.CommandName == "Change")
            {

                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow gvr = gvResult.Rows[index];
                Int64 BankID = Convert.ToInt64(gvResult.DataKeys[index]["BankID"]);
                BanksDML dml = new BanksDML();
                DataTable dtBanks = dml.GetBank(BankID);


                if (dtBanks.Rows.Count > 0)
                {
                    hfEditID.Value = BankID.ToString();
                    txtCode.Text = dtBanks.Rows[0]["Code"].ToString();
                    ddlBank.ClearSelection();
                    ddlBank.Items.FindByText(dtBanks.Rows[0]["Name"].ToString()).Selected = true;
                    txtAccountNo.Text = dtBanks.Rows[0]["AccountNo"].ToString();
                    txtAccountTitle.Text = dtBanks.Rows[0]["AccountTitle"].ToString();

                    pnlInput.Visible = true;
                }
            }
        }

        #endregion
    }
}