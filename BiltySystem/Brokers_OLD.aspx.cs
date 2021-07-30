using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.Drawing;

namespace BiltySystem
{
    public partial class Broker_OLD : System.Web.UI.Page
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

                this.Title = "Brokers";
                GetAllBrokers();

                pnlInput.Visible = false;
                cbActive.Checked = true;

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
                cbActive.Checked = false;
                txtDescription.Text = string.Empty;
                txtAddress.Text = string.Empty;

                pnlInput.Visible = false;
            }
            catch (Exception ex)
            {
                notification("Error", "Error clearing fields, due to: " + ex.Message);
            }
        }

        #endregion

        #region Events

        protected void btnDeleteBroker_Click(object sender, EventArgs e)
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
                notification("Error", "Error delete broker, due to: " + ex.Message);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
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
                else if (txtPhone.Text == string.Empty)
                {
                    notification("Error", "Please enter Primary Contact No.");
                    txtPhone.Focus();
                }
                else if (txtNIC.Text == string.Empty)
                {
                    notification("Error", "Please enter National Identity Card no.");
                    txtNIC.Focus();
                }
                else if (txtHomeNo.Text == string.Empty)
                {
                    notification("Error", "Please enter Home no.");
                    txtHomeNo.Focus();
                }
                else if (txtPhone2.Text == string.Empty)
                {
                    notification("Error", "Please enter Secondary Contact No.");
                    txtPhone2.Focus();
                }



                else
                {
                    string Code = txtCode.Text.Trim();
                    string Name = txtName.Text.Trim();
                    Int64 Phone = Convert.ToInt64(txtPhone.Text.Trim());
                    Int64 Phone2 = txtPhone.Text == string.Empty ? 0 : Convert.ToInt64(txtPhone.Text.Trim());
                    Int64 HomeNo = txtHomeNo.Text == string.Empty ? 0 : Convert.ToInt64(txtHomeNo.Text.Trim());
                    Int64 NIC = Convert.ToInt64(txtNIC.Text.Trim());
                    int active = cbActive.Checked ? 1 : 0;
                    string Address = txtAddress.Text.Trim();
                    string Description = txtDescription.Text.Trim();

                    BrokersDML dmlBrokers = new BrokersDML();


                    if (hfEditID.Value == string.Empty)
                    {
                        DataTable dtDuplicate = dmlBrokers.GetBroker(Code, Phone, NIC);
                        if (dtDuplicate.Rows.Count > 0)
                        {
                            string DupCode = dtDuplicate.Rows[0]["Code"].ToString();
                            Int64 DupPhone = Convert.ToInt64(dtDuplicate.Rows[0]["Phone"]);
                            Int64 DupNIC = Convert.ToInt64(dtDuplicate.Rows[0]["NIC"]);

                            if (Code == DupCode)
                            {
                                notification("Error", "This code is already assigned to another broker");
                                txtCode.Focus();
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
                            Int64 BrokerID = Convert.ToInt64(dmlBrokers.InsertBroker(Code, Name, Phone, Phone, HomeNo, Address, NIC, Description, active, LoginID));
                            if (BrokerID > 0)
                            {
                                ClearFields();
                                GetAllBrokers();
                                notification("Success", "Broker added successfully");
                            }
                        }
                    }
                    else
                    {
                        Int64 BrokerID = Convert.ToInt64(hfEditID.Value);
                        DataTable dtDuplicate = dmlBrokers.GetBroker(Code, Phone, NIC);
                        if (dtDuplicate.Rows.Count > 1)
                        {
                            string DupCode = dtDuplicate.Rows[0]["Code"].ToString();
                            Int64 DupPhone = Convert.ToInt64(dtDuplicate.Rows[0]["Phone"]);
                            Int64 DupNIC = Convert.ToInt64(dtDuplicate.Rows[0]["NIC"]);

                            if (Code == DupCode)
                            {
                                notification("Error", "This code is already assigned to another broker");
                                txtCode.Focus();
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
                            dmlBrokers.UpdateBroker(BrokerID, Code, Name, Phone, Phone, HomeNo, Address, NIC, Description, active, LoginID);

                            ClearFields();
                            GetAllBrokers();
                            notification("Success", "Broker Updated successfully");//}
                        }
                    }

                }

            }

            catch (Exception ex)
            {
                notification("Error", "Error submitting, due to: " + ex.Message);
            }
        }

        protected void btnEnableInput_Click(object sender, EventArgs e)
        {
            try
            {
                pnlView.Visible = false;
                pnlInput.Visible = true;
                btnDeleteBroker.Visible = false;
            }
            catch (Exception ex)
            {
                notification("Error", "Error enabling input panel, due to: " + ex.Message);
            }
        }

        protected void btnEnableSearch_Click(object sender, EventArgs e)
        {
            try
            {
                //pnlSearch.Visible = true;
            }
            catch (Exception ex)
            {
                notification("Error", "Error enabling search panel, due to: " + ex.Message);
            }
        }

        protected void btnCloseInput_Click(object sender, EventArgs e)
        {
            try
            {
                pnlInput.Visible = false;
            }
            catch (Exception ex)
            {
                notification("Error", "Error closing panel, due to: " + ex.Message);
            }
        }

        protected void gvResult_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Change")
                {
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
                        if (dtBrokers.Rows[0]["isActive"].ToString() == "True")
                        {
                            cbActive.Checked = true;
                        }
                        else
                        {
                            cbActive.Checked = false;
                        }

                        pnlInput.Visible = true;
                        btnDeleteBroker.Visible = true;
                    }
                    else
                    {
                        notification("Error", "No broker found");
                    }

                }
                else if (e.CommandName == "View")
                {
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
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
            }
            catch (Exception ex)
            {
                notification("Error", "Error collecting data for edit, due to: " + ex.Message);


            }
        }

        protected void btnCloseSearch_Click(object sender, EventArgs e)
        {
            try
            {
                //pnlSearch.Visible = false;
                txtSearch.Text = string.Empty;
            }
            catch (Exception ex)
            {
                notification("Error", "Error closing search panel, due to: " + ex.Message);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
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

        protected void lnkCancelSearch_Click(object sender, EventArgs e)
        {
            try
            {
                txtSearch.Text = string.Empty;
                GetAllBrokers();
            }
            catch (Exception ex)
            {
                notification("Error", "Error canceling search, due to: " + ex.Message);
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

        #endregion
    }
}