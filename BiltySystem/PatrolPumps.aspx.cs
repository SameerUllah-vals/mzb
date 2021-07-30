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
    public partial class PatrolPumps : System.Web.UI.Page
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
                    this.Title = "Patrol Pumps";
                    GetPatrolPumps("DESC");
                }
            }
        }

        #endregion

        #region Custom Methods

        public void GetPatrolPumps(string SortState)
        {
            try
            {
                PatrolPumpDML dml = new PatrolPumpDML();
                DataTable dtProvince = dml.GetPatrolPumps(SortState);
                gvResult.DataSource = dtProvince.Rows.Count > 0 ? dtProvince : null;
                gvResult.DataBind();
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting/populating province, due to: " + ex.Message);
            }
        }

        public void GetPatrolPumps(string Keyword, string SortState)
        {
            try
            {
                PatrolPumpDML dml = new PatrolPumpDML();
                DataTable dtProvince = dml.GetPatrolPumps(Keyword, SortState);
                gvResult.DataSource = dtProvince.Rows.Count > 0 ? dtProvince : null;
                gvResult.DataBind();
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting/populating province, due to: " + ex.Message);
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

        private void ClearFields()
        {
            try
            {
                hfEditID.Value = string.Empty;
                txtCode.Text = "";
                txtName.Text = "";
                txtPerLiterRate.Text = string.Empty;
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

        #endregion

        #region Events

        #region LinkButtons Events

        protected void lnkConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                string Action = hfConfirmAction.Value;
                if (Action == "Save")
                {
                    string Code = txtCode.Text;
                    string Name = txtName.Text;
                    double PerLiterRate =Convert.ToDouble( txtPerLiterRate.Text.Trim());

                    PatrolPumpDML dml = new PatrolPumpDML();
                    Int64 PatrolPumpID = dml.InsertPatrolPump(Code, Name, PerLiterRate, LoginID);
                    if (PatrolPumpID > 0)
                    {
                        
                        pnlInput.Visible = false;
                        notification("Success", "Submited Successfully");
                        ClearFields();
                        GetPatrolPumps("DESC");
                    }
                }
                else if (Action == "Update")
                {
                    string Code = txtCode.Text;
                    string Name = txtName.Text;
                    double PerLiterRate = Convert.ToDouble(txtPerLiterRate.Text.Trim());
                    Int64 PatrolPumpID = Convert.ToInt64(hfEditID.Value);

                    PatrolPumpDML dml = new PatrolPumpDML();
                    dml.UpdatePatrolPump(PatrolPumpID, Code, Name,PerLiterRate, LoginID);
                    pnlInput.Visible = false;
                    notification("Success", "Updated Successfully");
                    ClearFields();
                    GetPatrolPumps("DESC");
                    
                }
                else if (Action == "Delete")
                {
                    Int64 PatrolPumpID = Convert.ToInt64(hfEditID.Value);

                    PatrolPumpDML dml = new PatrolPumpDML();
                    dml.DeletePatrolPump(PatrolPumpID, LoginID);                    

                    GetPatrolPumps("DESC");
                    notification("Success", "Pump has been deleted successfully");
                    ClearFields();
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
                notification("Error", "Error closing input panel, due to: " + ex.Message);
            }
        }

        protected void lnkCloseView_Click(object sender, EventArgs e)
        {
            try
            {
                pnlView.Visible = false;
            }
            catch (Exception ex)
            {
                notification("Error", "Error closing view panel, due to: " + ex.Message);
            }
        }

        protected void lnkSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCode.Text == string.Empty)
                {
                    notification("Error", "Please enter code");
                    txtCode.Focus();
                }
                else if (txtName.Text == string.Empty)
                {
                    notification("Error", "Please enter name");
                    txtName.Focus();
                }
                else
                {
                    if (hfEditID.Value == string.Empty)
                    {
                        ConfirmModal("Are you sure to Save it?", "Save");
                    }
                    else
                    {
                        ConfirmModal("Are you sure to Save it?", "Update");
                    }
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error submitting, due to: " + ex.Message);
            }
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {

        }

        protected void lnkCancelSearch_Click(object sender, EventArgs e)
        {
            try
            {
                txtSearch.Text = string.Empty;
                GetPatrolPumps("DESC");
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
                if (txtSearch.Text == string.Empty)
                {
                    GetPatrolPumps("DESC");
                }
                else 
                {
                    string Keyword = txtSearch.Text;
                    GetPatrolPumps(Keyword, "DESC");
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error searching, due to: " + ex.Message);
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
                notification("Error", "Error enabling input deu to: " + ex.Message);
            }
        }

        #endregion

        #region GridViews Events

        protected void gvResult_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Activate")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvResult.Rows[index];
                    Int64 PatrolPumpID = Convert.ToInt64(gvResult.DataKeys[index]["PatrolPumpID"]);
                    bool Active = Convert.ToBoolean(gvResult.DataKeys[index]["isActive"].ToString() == string.Empty ? "false" : gvResult.DataKeys[index]["isActive"]);
                    LinkButton lnkActive = gvr.FindControl("lnkActive") as LinkButton;
                    PatrolPumpDML dml = new PatrolPumpDML();

                    if (Active == false)
                    {
                        dml.ActivatePatrolPump(PatrolPumpID, LoginID);
                    }
                    else
                    {
                        dml.DeActivatePatrolPump(PatrolPumpID, LoginID);
                    }
                    GetPatrolPumps("Desc");
                }
                else if (e.CommandName == "Change")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvResult.Rows[index];
                    Int64 PatrolPumpID = Convert.ToInt64(gvResult.DataKeys[index]["PatrolPumpID"]);
                    PatrolPumpDML dml = new PatrolPumpDML();
                    DataTable dtPatrolPump = dml.GetPatrolPump(PatrolPumpID, "DESC");
                    if (dtPatrolPump.Rows.Count > 0)
                    {
                        hfEditID.Value = dtPatrolPump.Rows[0]["PatrolPumpID"].ToString();
                        txtCode.Text = dtPatrolPump.Rows[0]["Code"].ToString();
                        txtName.Text = dtPatrolPump.Rows[0]["Name"].ToString();
                        txtPerLiterRate.Text = dtPatrolPump.Rows[0]["PerLiterRate"].ToString();

                        pnlInput.Visible = true;
                    }
                    else
                    {
                        notification("Error", "No such Pump found, Try Again");
                        GetPatrolPumps("DESC");
                    }
                }
                else if (e.CommandName == "View")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvResult.Rows[index];
                    Int64 PatrolPumpID = Convert.ToInt64(gvResult.DataKeys[index]["PatrolPumpID"]);
                    PatrolPumpDML dml = new PatrolPumpDML();
                    DataTable dtPatrolPump = dml.GetPatrolPump(PatrolPumpID, "DESC");
                    if (dtPatrolPump.Rows.Count > 0)
                    {
                        //hfEditID.Value = dtPatrolPump.Rows[0]["PatrolPumpID"].ToString();
                        lblPumpCode.Text = dtPatrolPump.Rows[0]["Code"].ToString();
                        lblPumpName.Text = dtPatrolPump.Rows[0]["Name"].ToString();
                        lblPerLiterRate.Text = dtPatrolPump.Rows[0]["PerLiterRate"].ToString();

                        pnlView.Visible = true;
                    }
                    else
                    {
                        notification("Error", "No such Pump found, Try Again");
                        GetPatrolPumps("DESC");
                    }
                }
                else if (e.CommandName == "Erase")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvResult.Rows[index];
                    Int64 PatrolPumpID = Convert.ToInt64(gvResult.DataKeys[index]["PatrolPumpID"]);

                    hfEditID.Value = PatrolPumpID.ToString();
                    ConfirmModal("Are you sure you want to Delete?", "Delete");
                    //PatrolPumpDML dml = new PatrolPumpDML();
                    //dml.DeletePatrolPump(PatrolPumpID, LoginID);

                    //GetPatrolPumps("DESC");
                    //notification("Success", "Pump has been deleted successfully");
                    //ClearFields();
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error commanding row, due to: " + ex.Message);
            }
        }

        protected void gvResult_RowDataBound(object sender, GridViewRowEventArgs e)
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
                    lnkActive.ToolTip = "Click to De-Activate";
                }
                else
                {
                    lnkActive.CssClass = "fas fa-toggle-off";
                    lnkActive.ForeColor = Color.Maroon;
                    lnkActive.ToolTip = "Click to Activate";
                }
            }
        }

        #endregion

        #region Misc Events

        #endregion

        #endregion
    }
}