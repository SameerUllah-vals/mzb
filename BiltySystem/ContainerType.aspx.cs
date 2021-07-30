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
    public partial class ContainerType : System.Web.UI.Page
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

            ddlUnitType.AutoPostBack = true;
            notification("", "");
            if (!IsPostBack)
            {
                if (LoginID > 0)
                {
                    GetContainerType();



                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
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

        #region Cutom Methods

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

        public void GetContainerType()
        {
            try
            {
                ContainerTypeDML dmlContainers = new ContainerTypeDML();
                DataTable dtContainers = dmlContainers.GetContainerType();
                if (dtContainers.Rows.Count > 0)
                {
                    gvResult.DataSource = dtContainers;
                }
                else
                {
                    gvResult.DataSource = null;
                }
                gvResult.DataBind();
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting/binding container Type, due to: " + ex.Message);
            }
        }

        public void ResetFields()
        {
            try
            {

                txtCode.Text = "";
                txtCubicCapacity.Text = "";
                txtDescription.Text = "";
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
                txtPayload.Text = "";
                txtTareWeight.Text = "";
                txtUpperDeckInnerHeight.Text = "";
                txtUpperDeckInnerLength.Text = "";
                txtUpperDeckInnerWidth.Text = "";
                txtUpperDeckOuterHeight.Text = "";
                txtUpperDeckOuterLength.Text = "";
                txtUpperDeckOuterWidth.Text = "";
                txtUpperPortionInnerHeight.Text = "";
                txtUpperPortionInnerLength.Text = "";
                txtUpperPortionInnerwidth.Text = "";

                hfEditID.Value = string.Empty;
            }
            catch (Exception ex)
            {
                notification("Error", "Error resetting fields, due to: " + ex.Message);
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


                    string code = txtCode.Text.Trim();
                    string name = txtName.Text.Trim();
                    string unittype = ddlUnitType.SelectedValue;



                    ContainerTypeDML dml = new ContainerTypeDML();
                    DataTable dt = dml.GetContainerType(code, name);


                    if (hfEditID.Value.ToString() == string.Empty)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            notification("Error", "Another Container Type with same name or code exists in database, try to change name or code");
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
                            notification("Error", "Another Container Type with same name or code exists in database, try to change name or code");
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
                ConfirmModal("Are you sure want to Delete?", "Delete");

            }
            catch (Exception ex)
            {
                notification("Error", "Error delete Container Type, due to: " + ex.Message);
            }
        }

        protected void lnkCancelSearch_Click(object sender, EventArgs e)
        {
            try
            {
                GetContainerType();
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
            try
            {

                if (e.CommandName == "Change")
                {
                    pnlInput.Visible = true;
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvResult.Rows[index];
                    Int64 contID = Convert.ToInt64(gvResult.DataKeys[index]["ContainerTypeID"]);
                    hfEditID.Value = contID.ToString();


                    ContainerTypeDML dml = new ContainerTypeDML();
                    DataTable dtcontainerType = dml.GetContainerType(contID);
                    if (dtcontainerType.Rows.Count > 0)
                    {
                        txtCode.Text = dtcontainerType.Rows[0]["Code"].ToString();
                        txtName.Text = dtcontainerType.Rows[0]["ContainerType"].ToString();

                        txtDescription.Text = dtcontainerType.Rows[0]["Description"].ToString();
                        txtLowerDeckInnerHeight.Text = dtcontainerType.Rows[0]["LowerDeckInnerHeight"].ToString();
                        txtLowerDeckInnerLength.Text = dtcontainerType.Rows[0]["LowerDeckInnerLength"].ToString();
                        txtLowerDeckInnerWidth.Text = dtcontainerType.Rows[0]["LowerDeckInnerWidth"].ToString();

                        txtLowerDeckOuterHeight.Text = dtcontainerType.Rows[0]["LowerDeckOuterHeight"].ToString();
                        txtLowerDeckOuterLength.Text = dtcontainerType.Rows[0]["LowerDeckOuterLength"].ToString();
                        txtLowerDeckOuterWidth.Text = dtcontainerType.Rows[0]["LowerDeckOuterWidth"].ToString();
                        txtLowerPortionInnerHeight.Text = dtcontainerType.Rows[0]["LowerPortionInnerHeight"].ToString();
                        txtLowerPortionInnerLength.Text = dtcontainerType.Rows[0]["LowerPortionInnerLength"].ToString();
                        txtLowerPortionInnerWidth.Text = dtcontainerType.Rows[0]["LowerPortionInnerWidth"].ToString();


                        txtTareWeight.Text = dtcontainerType.Rows[0]["TareWeight"].ToString();
                        txtPayload.Text = dtcontainerType.Rows[0]["PayloadWeight"].ToString();
                        txtCubicCapacity.Text = dtcontainerType.Rows[0]["CubicCapacity"].ToString();

                        txtUpperDeckInnerHeight.Text = dtcontainerType.Rows[0]["UpperDeckInnerHeight"].ToString();
                        txtUpperDeckInnerLength.Text = dtcontainerType.Rows[0]["UpperDeckInnerLength"].ToString();

                        txtUpperDeckInnerWidth.Text = dtcontainerType.Rows[0]["UpperDeckInnerWidth"].ToString();
                        txtUpperDeckOuterHeight.Text = dtcontainerType.Rows[0]["UpperDeckOuterHeight"].ToString();
                        txtUpperDeckOuterLength.Text = dtcontainerType.Rows[0]["UpperDeckOuterLength"].ToString();
                        txtUpperDeckOuterWidth.Text = dtcontainerType.Rows[0]["UpperDeckOuterWidth"].ToString();
                        txtUpperPortionInnerHeight.Text = dtcontainerType.Rows[0]["UpperPortionInnerHeight"].ToString();
                        txtUpperPortionInnerLength.Text = dtcontainerType.Rows[0]["UpperPortionInnerLength"].ToString();
                        txtUpperPortionInnerwidth.Text = dtcontainerType.Rows[0]["UpperPortionInnerwidth"].ToString();

                        ddlUnitType.ClearSelection();
                        ddlUnitType.Items.FindByValue(dtcontainerType.Rows[0]["DimensionUnitType"].ToString()).Selected = true;

                        lnkDelete.Visible = true;
                        GetContainerType();
                    }
                }
                else if (e.CommandName == "View")
                {
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                    ResetFields();
                    pnlInput.Visible = false;
                    pnlView.Visible = true;


                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvResult.Rows[index];
                    Int64 contID = Convert.ToInt64(gvResult.DataKeys[index]["ContainerTypeID"]);



                    ContainerTypeDML dml = new ContainerTypeDML();
                    DataTable dtcontainerType = dml.GetContainerType(contID);

                    lblCodemodal.Text = dtcontainerType.Rows[0]["Code"].ToString();
                    lblNameModal.Text = dtcontainerType.Rows[0]["ContainerType"].ToString();

                    lblUnit.Text = dtcontainerType.Rows[0]["DimensionUnitType"].ToString();
                    lblCodemodal.Text = dtcontainerType.Rows[0]["Code"].ToString();
                    lblNameModal.Text = dtcontainerType.Rows[0]["ContainerType"].ToString();

                    lblDescription.Text = dtcontainerType.Rows[0]["Description"].ToString();
                    lblLowerDeckInnerHeight.Text = dtcontainerType.Rows[0]["LowerDeckInnerHeight"].ToString();
                    lblLowerDeckInnerLength.Text = dtcontainerType.Rows[0]["LowerDeckInnerLength"].ToString();
                    lblLowerDeckInnerWidth.Text = dtcontainerType.Rows[0]["LowerDeckInnerWidth"].ToString();

                    lblLowerDeckOuterHeight.Text = dtcontainerType.Rows[0]["LowerDeckOuterHeight"].ToString();
                    lblLowerDeckOuterLength.Text = dtcontainerType.Rows[0]["LowerDeckOuterLength"].ToString();
                    lblLowerDeckOuterWidth.Text = dtcontainerType.Rows[0]["LowerDeckOuterWidth"].ToString();
                    lblLowerPortionInnerHeight.Text = dtcontainerType.Rows[0]["LowerPortionInnerHeight"].ToString();
                    lblLowerPortionInnerLength.Text = dtcontainerType.Rows[0]["LowerPortionInnerLength"].ToString();
                    lblLowerPortionInnerWidth.Text = dtcontainerType.Rows[0]["LowerPortionInnerWidth"].ToString();


                    lblPayload.Text = dtcontainerType.Rows[0]["PayloadWeight"].ToString();
                    lblTareWeight.Text = dtcontainerType.Rows[0]["TareWeight"].ToString();
                    lblCubicCapacity.Text = dtcontainerType.Rows[0]["CubicCapacity"].ToString();
                    lblUpperDeckInnerHeight.Text = dtcontainerType.Rows[0]["UpperDeckInnerHeight"].ToString();
                    lblUpperDeckInnerLength.Text = dtcontainerType.Rows[0]["UpperDeckInnerLength"].ToString();

                    lblUpperDeckInnerWidth.Text = dtcontainerType.Rows[0]["UpperDeckInnerWidth"].ToString();
                    lblUpperDeckOuterHeight.Text = dtcontainerType.Rows[0]["UpperDeckOuterHeight"].ToString();
                    lblUpperDeckOuterLength.Text = dtcontainerType.Rows[0]["UpperDeckOuterLength"].ToString();
                    lblUpperDeckOuterWidth.Text = dtcontainerType.Rows[0]["UpperDeckOuterWidth"].ToString();
                    lblUpperPortionInnerHeight.Text = dtcontainerType.Rows[0]["UpperPortionInnerHeight"].ToString();
                    lblUpperPortionInnerLength.Text = dtcontainerType.Rows[0]["UpperPortionInnerLength"].ToString();
                    lblUpperPortionInnerwidth.Text = dtcontainerType.Rows[0]["UpperPortionInnerwidth"].ToString();











                }



                else if (e.CommandName == "Active")
                {

                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvResult.Rows[index];
                    Int64 ID = Convert.ToInt64(gvResult.DataKeys[index]["ContainerTypeID"]);
                    string Active = gvResult.DataKeys[index]["isActive"].ToString() == string.Empty ? "False" : gvResult.DataKeys[index]["isActive"].ToString();
                    ContainerTypeDML dml = new ContainerTypeDML();

                    hfEditID.Value = ID.ToString();

                    if (Active == "True")
                    {
                        dml.DeactivateContainer(ID, LoginID);
                    }
                    else
                    {
                        dml.ActivateContainer(ID, LoginID);
                    }
                    GetContainerType();
                    ResetFields();

                }
            }
            catch (Exception ex)
            {
                notification("Error", ex.Message);
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
            ContainerTypeDML dm = new ContainerTypeDML();
            DataTable dtnat = dm.GetContainerType(keyword);
            if (dtnat.Rows.Count > 0)
            {
                gvResult.DataSource = dtnat;
            }
            else
            {
                gvResult.DataSource = null;
            }
            gvResult.DataBind();
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
                        Int64 ID = Convert.ToInt64(hfEditID.Value);
                        ContainerTypeDML dml = new ContainerTypeDML();
                        dml.DeleteContainerType(ID);
                        ResetFields();
                        pnlInput.Visible = false;
                        GetContainerType();

                        notification("Success", "Container Type deleted successfully");
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
                        decimal TareWeight = txtTareWeight.Text == string.Empty ? 0 : Convert.ToDecimal(txtTareWeight.Text.Trim());
                        decimal Payload = txtPayload.Text == string.Empty ? 0 : Convert.ToDecimal(txtPayload.Text.Trim());
                        decimal CubicCapacity = txtCubicCapacity.Text == string.Empty ? 0 : Convert.ToDecimal(txtCubicCapacity.Text.Trim());
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


                        ContainerTypeDML dml = new ContainerTypeDML();
                        dml.InsertContainerType(code, name, unittype, LowerDeckInnerLength, LowerDeckInnerWidth, LowerDeckInnerHeight, LowerDeckOuterLength, LowerDeckOuterWidth, LowerDeckOuterHeight, UpperDeckInnerLength, UpperDeckInnerWidth, UpperDeckInnerHeight, UpperDeckOuterLength, UpperDeckOuterWidth, UpperDeckOuterHeight, UpperPortionInnerLength, UpperPortionInnerwidth, UpperPortionInnerHeight, LowerPortionInnerWidth, LowerPortionInnerLength, LowerDeckInnerHeight, TareWeight, Payload, CubicCapacity, description, LoginID);
                        pnlInput.Visible = false;
                        notification("Success", "Submited Successfully");
                        GetContainerType();
                        ResetFields();
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
                        decimal TareWeight = txtTareWeight.Text == string.Empty ? 0 : Convert.ToDecimal(txtTareWeight.Text.Trim());
                        decimal Payload = txtPayload.Text == string.Empty ? 0 : Convert.ToDecimal(txtPayload.Text.Trim());
                        decimal CubicCapacity = txtCubicCapacity.Text == string.Empty ? 0 : Convert.ToDecimal(txtCubicCapacity.Text.Trim());
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


                        ContainerTypeDML dml = new ContainerTypeDML();
                        Int64 id = Convert.ToInt64(hfEditID.Value);
                        dml.UpdateContainerType(id, code, name, unittype, LowerDeckInnerLength, LowerDeckInnerWidth, LowerDeckInnerHeight, LowerDeckOuterLength, LowerDeckOuterWidth, LowerDeckOuterHeight, UpperDeckInnerLength, UpperDeckInnerWidth, UpperDeckInnerHeight, UpperDeckOuterLength, UpperDeckOuterWidth, UpperDeckOuterHeight, UpperPortionInnerLength, UpperPortionInnerwidth, UpperPortionInnerHeight, LowerPortionInnerWidth, LowerPortionInnerLength, LowerDeckInnerHeight, TareWeight, Payload, CubicCapacity, description, LoginID);
                        pnlInput.Visible = false;
                        notification("Success", "Updated Successfully");
                        GetContainerType();
                        ResetFields();

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
    }
}