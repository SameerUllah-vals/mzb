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
    public partial class Brokers : System.Web.UI.Page
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
                if (LoginID != 0 && LoginID != null)
                {
                    this.Title = "Brokers";
                    GetAllBrokers();

                }
                else
                {

                }
            }
        }
        #endregion

        #region Helper Methods

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

        public void GetAllBrokers()
        {
            try
            {
                BrokersDML dml = new BrokersDML();
                DataTable dtBrokers = dml.GetAllBrokers();
                if (dtBrokers.Rows.Count > 0)
                {
                    gvResult.DataSource = dtBrokers;
                }
                else
                {
                    gvResult.DataSource = null;
                }
                gvResult.DataBind();
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting brokers, due to: " + ex.Message);
            }
        }

        public void ClearFields()
        {
            try
            {
                hfEditID.Value = string.Empty;
                txtCode.Text = string.Empty;
                txtName.Text = string.Empty;
                txtPhone.Text = string.Empty;
                txtPhone2.Text = string.Empty;
                txtHomeNo.Text = string.Empty;
                txtNIC.Text = string.Empty;
                txtDescription.Text = string.Empty;
                txtAddress.Text = string.Empty;

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

        #endregion

        #region Events

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

        protected void lnkSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                //REQUIRE  FIELDS
                if (txtCode.Text == string.Empty)
                {
                    notification("Error", "Please enter Code");
                    txtCode.Focus();
                }
                else if (txtName.Text == string.Empty)
                {
                    notification("Error", "Please enter Name");
                    txtName.Focus();
                }

                else
                {
                    string Code = txtCode.Text.Trim();
                    string Name = txtName.Text.Trim();
                    Int64 NIC = txtNIC.Text == string.Empty ? 0 : Convert.ToInt64(txtNIC.Text.Trim());
                    Int64 Phone = txtPhone.Text == string.Empty ? 0 : Convert.ToInt64(txtPhone.Text);

                    //CHECKING SAME CODE OR SAME NAME
                    BrokersDML dmlBrokers = new BrokersDML();
                    if (hfEditID.Value == string.Empty)
                    {
                        DataTable dtDuplicate = dmlBrokers.GetBroker(Code,Phone,NIC);
                        if (dtDuplicate.Rows.Count > 0)
                        {
                            string DupCode = dtDuplicate.Rows[0]["Code"].ToString();
                            Int64 DupPhone = Convert.ToInt64(dtDuplicate.Rows[0]["Phone"]);
                            Int64 DupNIC = Convert.ToInt64(dtDuplicate.Rows[0]["NIC"]);
                            string DupName = dtDuplicate.Rows[0]["Name"].ToString();

                            if (Code == DupCode)
                            {
                                notification("Error", "This code is already assigned to another broker");
                                txtCode.Focus();
                            }
                            else if (Name == DupName)
                            {
                                notification("Error", "Another broker already exists with same Name");
                                txtNIC.Focus();
                            }
                            else if (Phone == DupPhone)
                            {
                                notification("Error", "Another broker already exists with same Phone no.");
                                txtPhone.Focus();
                            }
                            else if (NIC == DupNIC)
                            {
                                notification("Error", "Another broker already exists with same CNIC");
                                txtNIC.Focus();
                            }

                        }
                        else
                        {
                            //OPENING MODEL
                            ConfirmModal("Are you sure want to Save?", "Save");

                        }
                    }
                    else
                    {
                        //UPDATING 
                        Int64 BrokerID = Convert.ToInt64(hfEditID.Value);

                        //CHEKING DUPLICATE 
                        DataTable dtDuplicate = dmlBrokers.GetBroker(Code, Phone, NIC);
                        if (dtDuplicate.Rows.Count > 1)
                        {
                            string DupCode = dtDuplicate.Rows[0]["Code"].ToString();
                            Int64 DupPhone = Convert.ToInt64(dtDuplicate.Rows[0]["Phone"]);
                            Int64 DupNIC = Convert.ToInt64(dtDuplicate.Rows[0]["NIC"]);
                            string DupName = dtDuplicate.Rows[0]["Name"].ToString();

                            if (Code == DupCode)
                            {
                                notification("Error", "This code is already assigned to another broker");
                                txtCode.Focus();
                            }
                            else if (Name == DupName)
                            {
                                notification("Error", "Another broker already exists with same Name.");
                                txtPhone.Focus();
                            }
                            else if (Phone == DupPhone)
                            {
                                notification("Error", "Another broker already exists with same Phone no.");
                                txtPhone.Focus();
                            }
                            else if (NIC == DupNIC)
                            {
                                notification("Error", "Another broker already exists with same CNIC");
                                txtNIC.Focus();
                            }
                        }
                        else
                        {
                            //OPENING MODAL
                            ConfirmModal("Are you sure want to Update?", "Update");

                        }
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
            try
            {
                ConfirmModal("Are you sure want to delete?", "Delete");

            }
            catch (Exception ex)
            {

                notification("error", "Error with Deleting due to:" + ex.Message);
            }

            GetAllBrokers();
            pnlInput.Visible = false;
        }

        protected void lnkCancelSearch_Click(object sender, EventArgs e)
        {
            try
            {
                GetAllBrokers();
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
            //GET DATA IN TEXTBOX FOR EDIT
            if (e.CommandName == "Change")
            {
                pnlInput.Visible = true;
                pnlView.Visible = false;
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow gvr = gvResult.Rows[index];
                Int64 BrokerID = Convert.ToInt64(gvResult.DataKeys[index]["ID"]);
                hfEditID.Value = BrokerID.ToString();

                BrokersDML dmlBrokers = new BrokersDML();
                DataTable dtBrokers = dmlBrokers.GetBroker(BrokerID);
                if (dtBrokers.Rows.Count > 0)
                {
                    txtCode.Text = dtBrokers.Rows[0]["Code"].ToString();
                    txtName.Text = dtBrokers.Rows[0]["Name"].ToString();
                    txtPhone.Text = dtBrokers.Rows[0]["Phone"].ToString();
                    txtPhone2.Text = dtBrokers.Rows[0]["Phone2"].ToString();
                    txtHomeNo.Text = dtBrokers.Rows[0]["HomeNo"].ToString();
                    txtNIC.Text = dtBrokers.Rows[0]["NIC"].ToString();
                    txtAddress.Text = dtBrokers.Rows[0]["Address"].ToString();
                    txtDescription.Text = dtBrokers.Rows[0]["Description"].ToString();

                    pnlInput.Visible = true;
                    lnkDelete.Visible = true;
                }
                else
                {
                    notification("Error", "No broker found");
                }

            }
            //GET DATA IN VIEW PANEL'S LABELS
            else if (e.CommandName == "View")
            {

                ClearFields();
                pnlInput.Visible = false;
                pnlView.Visible = true;

                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow gvr = gvResult.Rows[index];
                Int64 BrokerID = Convert.ToInt64(gvResult.DataKeys[index]["ID"]);


                BrokersDML dmlBrokers = new BrokersDML();
                DataTable dtBrokers = dmlBrokers.GetBroker(BrokerID);
                if (dtBrokers.Rows.Count > 0)
                {
                    lblCodemodal.Text = dtBrokers.Rows[0]["Code"].ToString();
                    lblNameModal.Text = dtBrokers.Rows[0]["Name"].ToString();
                    lblPhone.Text = dtBrokers.Rows[0]["Phone"].ToString();
                    lblOther.Text = dtBrokers.Rows[0]["Phone2"].ToString();
                    lblHome.Text = dtBrokers.Rows[0]["HomeNo"].ToString();
                    lblNIC.Text = dtBrokers.Rows[0]["NIC"].ToString();
                    lblAddress.Text = dtBrokers.Rows[0]["Address"].ToString();
                    lblDesModal.Text = dtBrokers.Rows[0]["Description"].ToString();
                }


            }
            else if (e.CommandName == "Active")
            {

                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow gvr = gvResult.Rows[index];
                Int64 CatId = Convert.ToInt64(gvResult.DataKeys[index]["ID"]);
                string Active = gvResult.DataKeys[index]["isActive"].ToString() == string.Empty ? "False" : gvResult.DataKeys[index]["isActive"].ToString();
                BrokersDML dml = new BrokersDML();

                hfEditID.Value = CatId.ToString();

                if (Active == "True")
                {
                    dml.DeactivateBroker(CatId, LoginID);
                }
                else
                {
                    dml.ActivateBroker(CatId, LoginID);

                }
                GetAllBrokers();
                ClearFields();

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
            try
            {
                if (txtSearch.Text.Trim() != string.Empty)
                {
                    string keyword = txtSearch.Text.Trim();
                    BrokersDML dmlBrokers = new BrokersDML();
                    DataTable dtBrokers = dmlBrokers.GetBroker(keyword);
                    if (dtBrokers.Rows.Count > 0)
                    {
                        gvResult.DataSource = dtBrokers;
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
            catch (Exception ex)
            {
                notification("Error", "Error searching by keyword, due to: " + ex.Message);
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
                        Int64 BrokerID = Convert.ToInt64(hfEditID.Value);
                        BrokersDML dmlBrokers = new BrokersDML();
                        dmlBrokers.DeleteBroker(BrokerID);

                        ClearFields();
                        pnlInput.Visible = false;
                        GetAllBrokers();

                        notification("Success", "Broker deleted successfully");
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
                        string Code = txtCode.Text.Trim();
                        string Name = txtName.Text.Trim();
                        Int64 Phone = txtPhone.Text == string.Empty ? 0 : Convert.ToInt64(txtPhone.Text.Trim());
                        Int64 Phone2 = txtPhone2.Text == string.Empty ? 0 : Convert.ToInt64(txtPhone2.Text.Trim());
                        Int64 HomeNo = txtHomeNo.Text == string.Empty ? 0 : Convert.ToInt64(txtHomeNo.Text.Trim());
                        Int64 NIC = txtNIC.Text == string.Empty ? 0 : Convert.ToInt64(txtNIC.Text.Trim());
                        string Address = txtAddress.Text.Trim();
                        string Description = txtDescription.Text.Trim();

                        BrokersDML dmlBrokers = new BrokersDML();
                        Int64 BrokerID = Convert.ToInt64(dmlBrokers.InsertBroker(Code, Name, Phone, Phone2, HomeNo, Address, NIC, Description, LoginID));
                        if (BrokerID > 0)
                        {
                            ClearFields();
                            GetAllBrokers();
                            notification("Success", "Broker added successfully");
                        }
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
                        string Code = txtCode.Text.Trim();
                        string Name = txtName.Text.Trim();
                        Int64 Phone = txtPhone.Text == string.Empty ? 0 : Convert.ToInt64(txtPhone.Text.Trim());
                        Int64 Phone2 = txtPhone2.Text == string.Empty ? 0 : Convert.ToInt64(txtPhone2.Text.Trim());
                        Int64 HomeNo = txtHomeNo.Text == string.Empty ? 0 : Convert.ToInt64(txtHomeNo.Text.Trim());
                        Int64 NIC = txtNIC.Text == string.Empty ? 0 : Convert.ToInt64(txtNIC.Text.Trim());
                        string Address = txtAddress.Text.Trim();
                        string Description = txtDescription.Text.Trim();

                        BrokersDML dmlBrokers = new BrokersDML();
                        Int64 BrokerID = Convert.ToInt64(hfEditID.Value);
                        dmlBrokers.UpdateBroker(BrokerID, Code, Name, Phone, Phone2, HomeNo, Address, NIC, Description, LoginID);

                        ClearFields();
                        GetAllBrokers();
                        notification("Success", "Broker Updated successfully");
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

        #endregion
    }
}