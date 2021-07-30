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
    public partial class ShippingLine : System.Web.UI.Page
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
            if (!IsPostBack)
            {
                if (LoginID > 0)
                {
                    this.Title = "Shipping Company";
                    GetShippingLine();

                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }

        #endregion

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

        public void GetShippingLine()
        {
            try
            {
                ShippingLineDML dml = new ShippingLineDML();
                DataTable dt = dml.GetShippingLine();
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
                notification("Error", "Error getting Shipping Line, due to: " + ex.Message);
            }
        }
        public void ClearFields()
        {
            try
            {
                hfEditID.Value = string.Empty;
                txtCode.Text = string.Empty;
                txtName.Text = string.Empty;
                txtAddress.Text = string.Empty;
                txtDescription.Text = string.Empty;
                txtPhone.Text = string.Empty;
                txtSecondryPhone.Text = string.Empty;

                txtContactPerson.Text = string.Empty;
                txtEmail.Text = string.Empty;
                txtLandline.Text = string.Empty;
                txtWebsite.Text = string.Empty;
                txtLoLoCharges.Text = string.Empty;
            }
            catch (Exception ex)
            {
                notification("Error", "Error clearing fields, due to: " + ex.Message);
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
                if (txtCode.Text.Trim() == string.Empty)
                {
                    notification("Error", "Please enter code");
                    txtCode.Focus();
                }
                else if (txtName.Text.Trim() == string.Empty)
                {
                    notification("Error", "Please enter name");
                    txtName.Focus();
                }

                else
                {
                    string Code = txtCode.Text.Trim();
                    string Name = txtName.Text.Trim();


                    ShippingLineDML dml = new ShippingLineDML();
                    DataTable dt = dml.GetShippingLine(Code, Name);

                    if (hfEditID.Value == string.Empty)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            notification("Error", "Another ShippingLine with same name or code exists in database, try to change name or code");
                        }
                        else
                        {
                            ConfirmModal("Are you sure want to save?", "Save");

                        }
                    }
                    else
                    {

                        if (dt.Rows.Count > 1)
                        {
                            notification("Error", "Another Shipping Line with same name or code exists in database, try to change name or code");
                        }
                        else
                        {

                            ConfirmModal("Are you sure want to update?", "Update");

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error saving Shippping Line, due to: " + ex.Message);
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

            GetShippingLine();
            pnlInput.Visible = false;
        }

        protected void lnkCancelSearch_Click(object sender, EventArgs e)
        {
            try
            {
                GetShippingLine();
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
                Int64 ShippingID = Convert.ToInt64(gvResult.DataKeys[index]["ShippingLineID"]);
                hfEditID.Value = ShippingID.ToString();

                ShippingLineDML dml = new ShippingLineDML();
                DataTable dtShipping = dml.GetShippingLine(ShippingID);
                if (dtShipping.Rows.Count > 0)
                {

                    txtCode.Text = dtShipping.Rows[0]["Code"].ToString();
                    txtName.Text = dtShipping.Rows[0]["Name"].ToString();

                    //txtOwner.Text = dtContainer.Rows[0]["Owner"].ToString();
                    txtContactPerson.Text = dtShipping.Rows[0]["ContactPerson"].ToString();
                    txtPhone.Text = dtShipping.Rows[0]["PrimaryContact"].ToString();

                    txtSecondryPhone.Text = dtShipping.Rows[0]["SecondaryContact"].ToString();
                    txtLandline.Text = dtShipping.Rows[0]["LandLine"].ToString();

                    //txtOwner.Text = dtContainer.Rows[0]["Owner"].ToString();
                    txtEmail.Text = dtShipping.Rows[0]["EmailAddress"].ToString(); 
                    txtLoLoCharges.Text = dtShipping.Rows[0]["LiftOffLiftOnCharges"].ToString();
                    txtWebsite.Text = dtShipping.Rows[0]["WebSite"].ToString();
                    txtAddress.Text = dtShipping.Rows[0]["Address"].ToString();
                    txtDescription.Text = dtShipping.Rows[0]["Description"].ToString();




                    ////btnDeleteContainer.Visible = true;
                    lnkDelete.Visible = true;
                
                }
            }
            else if (e.CommandName == "View")
            {
               
                ClearFields();
                pnlInput.Visible = false;
                pnlView.Visible = true;



                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow gvr = gvResult.Rows[index];
                Int64 ShippingID = Convert.ToInt64(gvResult.DataKeys[index]["ShippingLineID"]);
                hfEditID.Value = ShippingID.ToString();

                ShippingLineDML dml = new ShippingLineDML();
                DataTable dtShipping = dml.GetShippingLine(ShippingID);
                if (dtShipping.Rows.Count > 0)
                {

                    lblCodemodal.Text = dtShipping.Rows[0]["Code"].ToString();
                    lblNameModal.Text = dtShipping.Rows[0]["Name"].ToString();

                    //txtOwner.Text = dtContainer.Rows[0]["Owner"].ToString();
                    lblContactPerson.Text = dtShipping.Rows[0]["ContactPerson"].ToString();
                    lblphone.Text = dtShipping.Rows[0]["PrimaryContact"].ToString();

                    lblOther.Text = dtShipping.Rows[0]["SecondaryContact"].ToString();
                    lblLandline.Text = dtShipping.Rows[0]["LandLine"].ToString();

                    //txtOwner.Text = dtContainer.Rows[0]["Owner"].ToString();
                    lblemail.Text = dtShipping.Rows[0]["EmailAddress"].ToString();
                    lblWebsite.Text = dtShipping.Rows[0]["WebSite"].ToString();
                    lblAddress.Text = dtShipping.Rows[0]["Address"].ToString();
                    lblDesModal.Text = dtShipping.Rows[0]["Description"].ToString();

                }


            }
            else if (e.CommandName == "Active")
            {

                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow gvr = gvResult.Rows[index];
                Int64 CatId = Convert.ToInt64(gvResult.DataKeys[index]["ShippingLineID"]);
                string Active = gvResult.DataKeys[index]["isActive"].ToString() == string.Empty ? "False" : gvResult.DataKeys[index]["isActive"].ToString();
                ShippingLineDML dml = new ShippingLineDML();

                hfEditID.Value = CatId.ToString();

                if (Active == "True")
                {
                    dml.DeactivateShipping(CatId, LoginID);
                }
                else
                {
                    dml.ActivateShipping(CatId, LoginID);
                }
                GetShippingLine();
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
                if (txtSearch.Text.Trim() == string.Empty)
                {
                    GetShippingLine();
                }
                else
                {
                    string Keyword = txtSearch.Text;
                    ShippingLineDML dml = new ShippingLineDML();
                    DataTable dt = dml.GetShippingLine(Keyword);
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

            }
            catch (Exception ex)
            {
                notification("Error", "Error searching, due to: " + ex.Message);
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
                        Int64 LocationID = Convert.ToInt64(hfEditID.Value);
                        ShippingLineDML dml = new ShippingLineDML();
                        dml.DeleteShippingLine(LocationID);

                        ClearFields();
                        GetShippingLine();

                        notification("Success", "Record deleted successfully");

                        lnkDelete.Visible = false;
                      
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
                        string Code = txtCode.Text.Trim();
                        string Name = txtName.Text.Trim();
                        string Person = txtContactPerson.Text;
                        Int64 primary = txtPhone.Text == string.Empty ? 0 : Convert.ToInt64(txtPhone.Text);
                        Int64 secondry = txtSecondryPhone.Text == string.Empty ? 0 : Convert.ToInt64(txtSecondryPhone.Text);
                        Int64 Landline = txtLandline.Text == string.Empty ? 0 : Convert.ToInt64(txtLandline.Text);
                        string Email = txtEmail.Text;
                        string website = txtWebsite.Text;
                        string address = txtAddress.Text;
                        string Description = txtDescription.Text;
                        double LoloCharges = txtLoLoCharges.Text == string.Empty ? 0 : Convert.ToDouble(txtLoLoCharges.Text);


                        ShippingLineDML dml = new ShippingLineDML();
                        dml.InsertShippingLine(Code, Name, Person, primary, secondry, Landline, Email, website, address, Description, LoloCharges, LoginID);
                        notification("Success", "ShippingLine added successfully.");
                        ClearFields();
                        GetShippingLine();
                        pnlInput.Visible = false;
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
                        string Person = txtContactPerson.Text;
                        Int64 primary = txtPhone.Text == string.Empty ? 0 : Convert.ToInt64(txtPhone.Text);
                        Int64 secondry = txtSecondryPhone.Text == string.Empty ? 0 : Convert.ToInt64(txtSecondryPhone.Text);
                        Int64 Landline = txtLandline.Text == string.Empty ? 0 : Convert.ToInt64(txtLandline.Text);
                        string Email = txtEmail.Text;
                        string website = txtWebsite.Text;
                        string address = txtAddress.Text;
                        string Description = txtDescription.Text;
                        double LoloCharges = txtLoLoCharges.Text == string.Empty ? 0 : Convert.ToDouble(txtLoLoCharges.Text);


                        ShippingLineDML dml = new ShippingLineDML();
                        Int64 ID = Convert.ToInt64(hfEditID.Value);
                        dml.UpdateShippingLine(ID, Code, Name, Person, primary, secondry, Landline, Email, website, address, Description, LoloCharges, LoginID);

                        notification("Success", "ShippingLine updated successfully.");
                        ClearFields();
                        GetShippingLine();

                        pnlInput.Visible = false;
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
    }
}