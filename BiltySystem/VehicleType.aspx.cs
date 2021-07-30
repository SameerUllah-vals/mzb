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
    public partial class VehicleType : System.Web.UI.Page
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
                if (LoginID > 0)
                {
                    GetAllVehicleTypes();
                }
                else
                {

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

        public void ResetInputs()
        {
            try
            {
                hfEditID.Value = string.Empty;
                txtCode.Text = string.Empty;
                txtName.Text = string.Empty;
                txtLowerDeckInnerHeight.Text = "";
                txtLowerDeckInnerLength.Text = "";
                txtLowerDeckInnerWidth.Text = "";
                txtLowerDeckOuterHeight.Text = "";
                txtLowerDeckOuterLength.Text = "";
                txtLowerDeckOuterWidth.Text = "";
                txtLowerPortionInnerHeight.Text = "";
                txtLowerPortionInnerLength.Text = "";
                txtLowerPortionInnerWidth.Text = "";
                txtName.Text = "";
                txtPermisibleHeight.Text = "";
                txtPermisibleLength.Text = "";
                txtUpperDeckInnerHeight.Text = "";
                txtUpperDeckInnerLength.Text = "";
                txtUpperDeckInnerWidth.Text = "";
                txtUpperDeckOuterHeight.Text = "";
                txtUpperDeckOuterLength.Text = "";
                txtUpperDeckOuterWidth.Text = "";
                txtUpperPortionInnerHeight.Text = "";
                txtUpperPortionInnerLength.Text = "";
                txtUpperPortionInnerwidth.Text = "";
                txtUpperPortionInnerwidth.Text = "";



                txtDescription.Text = string.Empty;
            
            }
            catch (Exception ex)
            {
                notification("Error", "Error resetting inputs, due to: " + ex.Message);
            }
        }

        public void GetAllVehicleTypes()
        {
            try
            {
                VehicleTypeDML dmlVehicleType = new VehicleTypeDML();
                DataTable dtVehicleTypes = dmlVehicleType.GetVehicleType();
                if (dtVehicleTypes.Rows.Count > 0)
                {
                    gvResult.DataSource = dtVehicleTypes;
                }
                else
                {
                    gvResult.DataSource = null;
                }
                gvResult.DataBind();
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting/binding vehicle types, due to: " + ex.Message);
            }
        }

        #endregion

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


                    CategoryDML cat = new CategoryDML();
                    DataTable dt = cat.GetCategory(Code, name);
                    if (hfEditID.Value.ToString() == string.Empty)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            notification("Error", "Another Category with same name or code exists in database, try to change name or code");
                        }
                        else
                        {


                            ConfirmModal("Are you sure wan to save?", "Save");

                        }
                    }
                    else
                    {
                        if (dt.Rows.Count > 1)
                        {
                            notification("Error", "Another Category with same name or code exists in database, try to change name or code");
                        }
                        else
                        {

                            ConfirmModal("Are you sure wan to Update?", "Update");


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

            GetAllVehicleTypes();
            pnlInput.Visible = false;
        }

        protected void lnkCancelSearch_Click(object sender, EventArgs e)
        {
            try
            {
                GetAllVehicleTypes();
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
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow gvr = gvResult.Rows[index];
                Int64 VehicleTypeID = Convert.ToInt64(gvResult.DataKeys[index]["VehicleTypeID"]);
                hfEditID.Value = VehicleTypeID.ToString();
                VehicleTypeDML dmlVehicleType = new VehicleTypeDML();
                DataTable dtVehicleType = dmlVehicleType.GetVehicleType(VehicleTypeID);
                if (dtVehicleType.Rows.Count > 0)
                {
                    txtCode.Text = dtVehicleType.Rows[0]["VehicleTypeCode"].ToString();
                    txtName.Text = dtVehicleType.Rows[0]["VehicleTypeName"].ToString();

                    txtDescription.Text = dtVehicleType.Rows[0]["Description"].ToString();
                    txtLowerDeckInnerHeight.Text = dtVehicleType.Rows[0]["LowerDeckInnerHeight"].ToString();
                    txtLowerDeckInnerLength.Text = dtVehicleType.Rows[0]["LowerDeckInnerLength"].ToString();
                    txtLowerDeckInnerWidth.Text = dtVehicleType.Rows[0]["LowerDeckInnerWidth"].ToString();

                    txtLowerDeckOuterHeight.Text = dtVehicleType.Rows[0]["LowerDeckOuterHeight"].ToString();
                    txtLowerDeckOuterLength.Text = dtVehicleType.Rows[0]["LowerDeckOuterLength"].ToString();
                    txtLowerDeckOuterWidth.Text = dtVehicleType.Rows[0]["LowerDeckOuterWidth"].ToString();
                    txtLowerPortionInnerHeight.Text = dtVehicleType.Rows[0]["LowerPortionInnerHeight"].ToString();
                    txtLowerPortionInnerLength.Text = dtVehicleType.Rows[0]["LowerPortionInnerLength"].ToString();
                    txtLowerPortionInnerWidth.Text = dtVehicleType.Rows[0]["LowerPortionInnerWidth"].ToString();


                    txtPermisibleHeight.Text = dtVehicleType.Rows[0]["PermisibleHeight"].ToString();
                    txtPermisibleLength.Text = dtVehicleType.Rows[0]["PermisibleLength"].ToString();
                    txtUpperDeckInnerHeight.Text = dtVehicleType.Rows[0]["UpperDeckInnerHeight"].ToString();
                    txtUpperDeckInnerLength.Text = dtVehicleType.Rows[0]["UpperDeckInnerLength"].ToString();

                    txtUpperDeckInnerWidth.Text = dtVehicleType.Rows[0]["UpperDeckInnerWidth"].ToString();
                    txtUpperDeckOuterHeight.Text = dtVehicleType.Rows[0]["UpperDeckOuterHeight"].ToString();
                    txtUpperDeckOuterLength.Text = dtVehicleType.Rows[0]["UpperDeckOuterLength"].ToString();
                    txtUpperDeckOuterWidth.Text = dtVehicleType.Rows[0]["UpperDeckOuterWidth"].ToString();
                    txtUpperPortionInnerHeight.Text = dtVehicleType.Rows[0]["UpperPortionInnerHeight"].ToString();
                    txtUpperPortionInnerLength.Text = dtVehicleType.Rows[0]["UpperPortionInnerLength"].ToString();
                    txtUpperPortionInnerwidth.Text = dtVehicleType.Rows[0]["UpperPortionInnerwidth"].ToString();

                    ddlUnitType.ClearSelection();
                    ddlUnitType.Items.FindByValue(dtVehicleType.Rows[0]["DimensionUnitType"].ToString()).Selected = true;


                    lnkDelete.Visible = true;
                }
            }
            else if (e.CommandName == "View")
            {
                // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                ResetInputs();
                pnlInput.Visible = false;
                pnlView.Visible = true;


                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow gvr = gvResult.Rows[index];
                Int64 VehicleTypeID = Convert.ToInt64(gvResult.DataKeys[index]["VehicleTypeID"]);

                VehicleTypeDML dmlVehicleType = new VehicleTypeDML();
                DataTable dtVehicleType = dmlVehicleType.GetVehicleType(VehicleTypeID);
                if (dtVehicleType.Rows.Count > 0)
                {
                    lblUnit.Text = dtVehicleType.Rows[0]["DimensionUnitType"].ToString();
                    lblCodemodal.Text = dtVehicleType.Rows[0]["VehicleTypeCode"].ToString();
                    lblNameModal.Text = dtVehicleType.Rows[0]["VehicleTypeName"].ToString();

                    lblDescription.Text = dtVehicleType.Rows[0]["Description"].ToString();
                    lblLowerDeckInnerHeight.Text = dtVehicleType.Rows[0]["LowerDeckInnerHeight"].ToString();
                    lblLowerDeckInnerLength.Text = dtVehicleType.Rows[0]["LowerDeckInnerLength"].ToString();
                    lblLowerDeckInnerWidth.Text = dtVehicleType.Rows[0]["LowerDeckInnerWidth"].ToString();

                    lblLowerDeckOuterHeight.Text = dtVehicleType.Rows[0]["LowerDeckOuterHeight"].ToString();
                    lblLowerDeckOuterLength.Text = dtVehicleType.Rows[0]["LowerDeckOuterLength"].ToString();
                    lblLowerDeckOuterWidth.Text = dtVehicleType.Rows[0]["LowerDeckOuterWidth"].ToString();
                    lblLowerPortionInnerHeight.Text = dtVehicleType.Rows[0]["LowerPortionInnerHeight"].ToString();
                    lblLowerPortionInnerLength.Text = dtVehicleType.Rows[0]["LowerPortionInnerLength"].ToString();
                    lblLowerPortionInnerWidth.Text = dtVehicleType.Rows[0]["LowerPortionInnerWidth"].ToString();


                    lblPermisibleHeight.Text = dtVehicleType.Rows[0]["PermisibleHeight"].ToString();
                    lblPermisibleLength.Text = dtVehicleType.Rows[0]["PermisibleLength"].ToString();
                    lblUpperDeckInnerHeight.Text = dtVehicleType.Rows[0]["UpperDeckInnerHeight"].ToString();
                    lblUpperDeckInnerLength.Text = dtVehicleType.Rows[0]["UpperDeckInnerLength"].ToString();

                    lblUpperDeckInnerWidth.Text = dtVehicleType.Rows[0]["UpperDeckInnerWidth"].ToString();
                    lblUpperDeckOuterHeight.Text = dtVehicleType.Rows[0]["UpperDeckOuterHeight"].ToString();
                    lblUpperDeckOuterLength.Text = dtVehicleType.Rows[0]["UpperDeckOuterLength"].ToString();
                    lblUpperDeckOuterWidth.Text = dtVehicleType.Rows[0]["UpperDeckOuterWidth"].ToString();
                    lblUpperPortionInnerHeight.Text = dtVehicleType.Rows[0]["UpperPortionInnerHeight"].ToString();
                    lblUpperPortionInnerLength.Text = dtVehicleType.Rows[0]["UpperPortionInnerLength"].ToString();
                    lblUpperPortionInnerwidth.Text = dtVehicleType.Rows[0]["UpperPortionInnerwidth"].ToString();
                }
            }
            else if (e.CommandName == "Active")
            {

                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow gvr = gvResult.Rows[index];
                Int64 CatId = Convert.ToInt64(gvResult.DataKeys[index]["VehicleTypeID"]);
                string Active = gvResult.DataKeys[index]["isActive"].ToString() == string.Empty ? "False" : gvResult.DataKeys[index]["isActive"].ToString();
                VehicleTypeDML dml = new VehicleTypeDML();

                hfEditID.Value = CatId.ToString();

                if (Active == "True")
                {
                    dml.DeactivateVehicleType(CatId, LoginID);
                }
                else
                {
                    dml.ActivateVehicleType(CatId, LoginID);
                }
                GetAllVehicleTypes();
                ResetInputs();

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

                    VehicleTypeDML dmlVehicleType = new VehicleTypeDML();
                    DataTable dtFilteredVehicleTypes = dmlVehicleType.GetVehicleType(keyword);
                    if (dtFilteredVehicleTypes.Rows.Count > 0)
                    {
                        gvResult.DataSource = dtFilteredVehicleTypes;
                    }
                    else
                    {
                        gvResult.DataSource = null;
                    }
                    gvResult.DataBind();
                }
                else
                {
                    GetAllVehicleTypes();
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
                        Int64 VehicleTypeID = Convert.ToInt64(hfEditID.Value);
                        VehicleTypeDML dmlVehicleType = new VehicleTypeDML();
                        dmlVehicleType.DeleteVehicleType(VehicleTypeID);

                        ResetInputs();
                        pnlInput.Visible = false;
                        GetAllVehicleTypes();

                        notification("Success", "Vehicle Type deleted successfully");
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
                        string code = txtCode.Text.Trim();
                        string name = txtName.Text.Trim();
                        string unittype = ddlUnitType.SelectedValue;
                        decimal LowerDeckInnerHeight = txtLowerDeckInnerHeight.Text == string.Empty ? 0 : Convert.ToDecimal(txtLowerDeckInnerHeight.Text.Trim());
                        decimal LowerDeckInnerLength = txtLowerDeckInnerLength.Text == string.Empty ? 0 : Convert.ToDecimal(txtLowerDeckInnerLength.Text.Trim());
                        decimal LowerDeckInnerWidth = txtLowerDeckInnerWidth.Text == string.Empty ? 0 : Convert.ToDecimal(txtLowerDeckInnerWidth.Text.Trim());
                        decimal LowerDeckOuterHeight = txtLowerDeckOuterHeight.Text == string.Empty ? 0 : Convert.ToDecimal(txtLowerDeckOuterHeight.Text.Trim());
                        decimal LowerDeckOuterLength = txtLowerDeckOuterLength.Text == string.Empty ? 0 : Convert.ToDecimal(txtLowerDeckOuterLength.Text.Trim());
                        decimal LowerDeckOuterWidth = txtLowerDeckOuterWidth.Text == string.Empty ? 0 : Convert.ToDecimal(txtLowerDeckOuterWidth.Text.Trim());
                        decimal LowerPortionInnerHeight = txtLowerPortionInnerHeight.Text == string.Empty ? 0 : Convert.ToDecimal(txtLowerPortionInnerHeight.Text.Trim());
                        decimal LowerPortionInnerLength = txtLowerPortionInnerLength.Text == string.Empty ? 0 : Convert.ToDecimal(txtLowerPortionInnerLength.Text.Trim());
                        decimal LowerPortionInnerWidth = txtLowerPortionInnerWidth.Text == string.Empty ? 0 : Convert.ToDecimal(txtLowerPortionInnerWidth.Text.Trim());
                        decimal PermisibleHeight = txtPermisibleHeight.Text == string.Empty ? 0 : Convert.ToDecimal(txtPermisibleHeight.Text.Trim());
                        decimal PermisibleLength = txtPermisibleLength.Text == string.Empty ? 0 : Convert.ToDecimal(txtPermisibleLength.Text.Trim());
                        decimal UpperDeckInnerHeight = txtUpperDeckInnerHeight.Text == string.Empty ? 0 : Convert.ToDecimal(txtUpperDeckInnerHeight.Text.Trim());
                        decimal UpperDeckInnerLength = txtUpperDeckInnerLength.Text == string.Empty ? 0 : Convert.ToDecimal(txtUpperDeckInnerLength.Text.Trim());
                        decimal UpperDeckInnerWidth = txtUpperDeckInnerWidth.Text == string.Empty ? 0 : Convert.ToDecimal(txtUpperDeckInnerWidth.Text.Trim());
                        decimal UpperDeckOuterHeight = txtUpperDeckOuterHeight.Text == string.Empty ? 0 : Convert.ToDecimal(txtUpperDeckOuterHeight.Text.Trim());
                        decimal UpperDeckOuterLength = txtUpperDeckOuterLength.Text == string.Empty ? 0 : Convert.ToDecimal(txtUpperDeckOuterLength.Text.Trim());
                        decimal UpperDeckOuterWidth = txtUpperDeckOuterWidth.Text == string.Empty ? 0 : Convert.ToDecimal(txtUpperDeckOuterWidth.Text.Trim());
                        decimal UpperPortionInnerHeight = txtUpperPortionInnerHeight.Text == string.Empty ? 0 : Convert.ToDecimal(txtUpperPortionInnerHeight.Text.Trim());
                        decimal UpperPortionInnerLength = txtUpperPortionInnerLength.Text == string.Empty ? 0 : Convert.ToDecimal(txtUpperPortionInnerLength.Text.Trim());
                        decimal UpperPortionInnerwidth = txtUpperPortionInnerwidth.Text == string.Empty ? 0 : Convert.ToDecimal(txtUpperPortionInnerwidth.Text.Trim());
                        string description = txtDescription.Text;
        

                        VehicleTypeDML dmlVehicleType = new VehicleTypeDML();
                        Int64 VehicleTypeID = dmlVehicleType.InsertVehicleType(code, name, unittype, LowerDeckInnerLength, LowerDeckInnerWidth, LowerDeckInnerHeight, LowerDeckOuterLength, LowerDeckOuterWidth, LowerDeckOuterHeight, UpperDeckInnerLength, UpperDeckInnerWidth, UpperDeckInnerHeight, UpperDeckOuterLength, UpperDeckOuterWidth, UpperDeckOuterHeight, UpperPortionInnerLength, UpperPortionInnerwidth, UpperPortionInnerHeight, LowerPortionInnerWidth, LowerPortionInnerLength, LowerDeckInnerHeight, PermisibleHeight, PermisibleLength, description, LoginID);
                        if (VehicleTypeID > 0)
                        {
                            notification("Success", "Vehicle type added successfully");

                            ResetInputs();
                            GetAllVehicleTypes();
                            txtCode.Focus();
                            pnlInput.Visible = false;
                        }
                        else
                        {
                            notification("Error", "Vehicle type not added, try again");
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
                        string code = txtCode.Text.Trim();
                        string name = txtName.Text.Trim();
                        string unittype = ddlUnitType.SelectedValue;
                        decimal LowerDeckInnerHeight = txtLowerDeckInnerHeight.Text == string.Empty ? 0 : Convert.ToDecimal(txtLowerDeckInnerHeight.Text.Trim());
                        decimal LowerDeckInnerLength = txtLowerDeckInnerLength.Text == string.Empty ? 0 : Convert.ToDecimal(txtLowerDeckInnerLength.Text.Trim());
                        decimal LowerDeckInnerWidth = txtLowerDeckInnerWidth.Text == string.Empty ? 0 : Convert.ToDecimal(txtLowerDeckInnerWidth.Text.Trim());
                        decimal LowerDeckOuterHeight = txtLowerDeckOuterHeight.Text == string.Empty ? 0 : Convert.ToDecimal(txtLowerDeckOuterHeight.Text.Trim());
                        decimal LowerDeckOuterLength = txtLowerDeckOuterLength.Text == string.Empty ? 0 : Convert.ToDecimal(txtLowerDeckOuterLength.Text.Trim());
                        decimal LowerDeckOuterWidth = txtLowerDeckOuterWidth.Text == string.Empty ? 0 : Convert.ToDecimal(txtLowerDeckOuterWidth.Text.Trim());
                        decimal LowerPortionInnerHeight = txtLowerPortionInnerHeight.Text == string.Empty ? 0 : Convert.ToDecimal(txtLowerPortionInnerHeight.Text.Trim());
                        decimal LowerPortionInnerLength = txtLowerPortionInnerLength.Text == string.Empty ? 0 : Convert.ToDecimal(txtLowerPortionInnerLength.Text.Trim());
                        decimal LowerPortionInnerWidth = txtLowerPortionInnerWidth.Text == string.Empty ? 0 : Convert.ToDecimal(txtLowerPortionInnerWidth.Text.Trim());
                        decimal PermisibleHeight = txtPermisibleHeight.Text == string.Empty ? 0 : Convert.ToDecimal(txtPermisibleHeight.Text.Trim());
                        decimal PermisibleLength = txtPermisibleLength.Text == string.Empty ? 0 : Convert.ToDecimal(txtPermisibleLength.Text.Trim());
                        decimal UpperDeckInnerHeight = txtUpperDeckInnerHeight.Text == string.Empty ? 0 : Convert.ToDecimal(txtUpperDeckInnerHeight.Text.Trim());
                        decimal UpperDeckInnerLength = txtUpperDeckInnerLength.Text == string.Empty ? 0 : Convert.ToDecimal(txtUpperDeckInnerLength.Text.Trim());
                        decimal UpperDeckInnerWidth = txtUpperDeckInnerWidth.Text == string.Empty ? 0 : Convert.ToDecimal(txtUpperDeckInnerWidth.Text.Trim());
                        decimal UpperDeckOuterHeight = txtUpperDeckOuterHeight.Text == string.Empty ? 0 : Convert.ToDecimal(txtUpperDeckOuterHeight.Text.Trim());
                        decimal UpperDeckOuterLength = txtUpperDeckOuterLength.Text == string.Empty ? 0 : Convert.ToDecimal(txtUpperDeckOuterLength.Text.Trim());
                        decimal UpperDeckOuterWidth = txtUpperDeckOuterWidth.Text == string.Empty ? 0 : Convert.ToDecimal(txtUpperDeckOuterWidth.Text.Trim());
                        decimal UpperPortionInnerHeight = txtUpperPortionInnerHeight.Text == string.Empty ? 0 : Convert.ToDecimal(txtUpperPortionInnerHeight.Text.Trim());
                        decimal UpperPortionInnerLength = txtUpperPortionInnerLength.Text == string.Empty ? 0 : Convert.ToDecimal(txtUpperPortionInnerLength.Text.Trim());
                        decimal UpperPortionInnerwidth = txtUpperPortionInnerwidth.Text == string.Empty ? 0 : Convert.ToDecimal(txtUpperPortionInnerwidth.Text.Trim());


                        string description = txtDescription.Text;
                

                        VehicleTypeDML dmlVehicleType = new VehicleTypeDML();
                        Int64 VehicleTypeID = Convert.ToInt64(hfEditID.Value);
                        dmlVehicleType.UpdateVehicleType(VehicleTypeID, code, name, unittype, LowerDeckInnerLength, LowerDeckInnerWidth, LowerDeckInnerHeight, LowerDeckOuterLength, LowerDeckOuterWidth, LowerDeckOuterHeight, UpperDeckInnerLength, UpperDeckInnerWidth, UpperDeckInnerHeight, UpperDeckOuterLength, UpperDeckOuterWidth, UpperDeckOuterHeight, UpperPortionInnerLength, UpperPortionInnerwidth, UpperPortionInnerHeight, LowerPortionInnerWidth, LowerPortionInnerLength, LowerDeckInnerHeight, PermisibleHeight, PermisibleLength, description, LoginID);
                        notification("Success", "Vehicle type Updated successfully");
                        ResetInputs();
                        GetAllVehicleTypes();
                        txtCode.Focus();
                        lnkDelete.Visible = false;
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