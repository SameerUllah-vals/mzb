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
    public partial class Container : System.Web.UI.Page
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
                    GetContainerType();
                    GetContainer();
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

        public void GetContainer()
        {
            try
            {
                ContainersDML dmlContainers = new ContainersDML();
                DataTable dtContainers = dmlContainers.GetContainer();
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
                notification("Error", "Error getting/binding containers, due to: " + ex.Message);
            }
        }

        public void GetContainerType()
        {
            try
            {
                ContainersDML dmlContainers = new ContainersDML();
                DataTable dtContainerType = dmlContainers.GetContainerType();
                if (dtContainerType.Rows.Count > 0)
                {
                    FillDropDown(dtContainerType, ddlContainerType, "ContainerTypeID", "ContainerType", "-Select Type-");
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting/populating container types, due to: " + ex.Message);
            }
        }

        public void ResetFields()
        {
            try
            {
                txtContainerNo.Text = string.Empty;
                txtCode.Text = string.Empty;
                txtShippingCompany.Text = string.Empty;
                txtOwner.Text = string.Empty;
                txtOwnerContact.Text = string.Empty;
                txtDescription.Text = string.Empty;

                ddlContainerType.SelectedIndex = 0;
                ddlOwnerShipType.SelectedIndex = 0;


                hfEditID.Value = string.Empty;
            }
            catch (Exception ex)
            {
                notification("Error", "Error resetting fields, due to: " + ex.Message);
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

        protected void lnkSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtContainerNo.Text.Trim() == string.Empty)
                {
                    notification("Error", "Please enter container number.");
                    txtContainerNo.Focus();
                }
                else if (txtCode.Text.Trim() == string.Empty)
                {
                    notification("Error", "Please enter container code");
                    txtCode.Focus();
                }

                else if (ddlContainerType.SelectedIndex == 0)
                {
                    notification("Error", "Please Select Container Type");
                    txtOwnerContact.Focus();
                }

                else if (txtOwnerContact.Text.Trim() == string.Empty)
                {
                    notification("Error", "Please enter owner contact");
                    txtOwnerContact.Focus();
                }


                else
                {
                    string ContainerNo = txtContainerNo.Text.Trim();
                    string Code = txtCode.Text.Trim();
                    Int64 OwnerContact = txtOwnerContact.Text == string.Empty ? 0 : Convert.ToInt64(txtOwnerContact.Text);


                    ContainersDML dmlContainers = new ContainersDML();
                    DataTable dt = dmlContainers.GetContainer(Code, OwnerContact, ContainerNo);


                    if (hfEditID.Value == string.Empty)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            notification("Error", "Another Conrainer with same code or phone or container no exist in database try to change code or phone or container no");
                        }
                        else
                        {
                           ConfirmModal("Are you sure want to save?","Save");
                        }

                    }
                    else
                    {
                        ConfirmModal("Are you sure want to Update?", "Update");

                    }


                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error adding container, due to: " + ex.Message);
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
                notification("Error", "Error deleting container, due to: " + ex.Message);
            }
          
        }

        protected void lnkCancelSearch_Click(object sender, EventArgs e)
        {
            try
            {
                txtSearch.Text = string.Empty;
                GetContainer();
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
                if (txtSearch.Text != string.Empty)
                {
                    string keyword = txtSearch.Text.Trim();
                    ContainersDML dmlContainer = new ContainersDML();
                    DataTable dtContainer = dmlContainer.GetContainer(keyword);
                    if (dtContainer.Rows.Count > 0)
                    {
                        gvResult.DataSource = dtContainer;
                    }
                    else
                    {
                        gvResult.DataSource = null;
                    }
                    gvResult.DataBind();
                }
                else
                {

                    GetContainer();
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error searching container, due to: " + ex.Message);
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

                notification("Error", ex.Message);     }
        }

        protected void gvResult_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

                if (e.CommandName == "Change")
                {
                    pnlInput.Visible = true;
                    pnlView.Visible = false;
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvResult.Rows[index];
                    Int64 ContainerID = Convert.ToInt64(gvResult.DataKeys[index]["ID"]);
                    hfEditID.Value = ContainerID.ToString();

                    ContainersDML dmlContainer = new ContainersDML();
                    DataTable dtContainer = dmlContainer.GetContainer(ContainerID);
                    if (dtContainer.Rows.Count > 0)
                    {
                        txtContainerNo.Text = dtContainer.Rows[0]["ContainerNo"].ToString();
                        txtCode.Text = dtContainer.Rows[0]["Code"].ToString();
                        txtShippingCompany.Text = dtContainer.Rows[0]["ShippingCompany"].ToString();
                        txtOwner.Text = dtContainer.Rows[0]["Owner"].ToString();
                        txtOwnerContact.Text = dtContainer.Rows[0]["OwnerContact"].ToString();
                        txtDescription.Text = dtContainer.Rows[0]["Description"].ToString();

                        ddlContainerType.ClearSelection();
                        ddlContainerType.Items.FindByValue(dtContainer.Rows[0]["Type"].ToString()).Selected = true;
                        ddlOwnerShipType.ClearSelection();
                        ddlOwnerShipType.Items.FindByValue(dtContainer.Rows[0]["OwnershipType"].ToString()).Selected = true;


                        //btnDeleteContainer.Visible = true;
                        lnkDelete.Visible = true;
                    }
                    else
                    {
                        pnlInput.Visible = false;
                        notification("Error", "No such container found");
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
                    Int64 ContainerID = Convert.ToInt64(gvResult.DataKeys[index]["ID"]);


                    ContainersDML dmlContainer = new ContainersDML();
                    DataTable dtContainer = dmlContainer.GetContainer(ContainerID);
                    if (dtContainer.Rows.Count > 0)
                    {

                        txtContainerNoModal.Text = dtContainer.Rows[0]["ContainerNo"].ToString();
                        txtCodeModal.Text = dtContainer.Rows[0]["Code"].ToString();
                        txtShippingModal.Text = dtContainer.Rows[0]["ShippingCompany"].ToString();
                        txtOwnerModal.Text = dtContainer.Rows[0]["Owner"].ToString();
                        txtownerContactModal.Text = dtContainer.Rows[0]["OwnerContact"].ToString();
                        txtDescriptionModal.Text = dtContainer.Rows[0]["Description"].ToString();


                        txtContainerTypeModal.Text = dtContainer.Rows[0]["ContainerType"].ToString();


                        txtOwnerTypeModal.Text = dtContainer.Rows[0]["OwnershipType"].ToString();

                    }

                }
                else if (e.CommandName == "Active")
                {

                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvResult.Rows[index];
                    Int64 AreaID = Convert.ToInt64(gvResult.DataKeys[index]["ID"]);
                    string Active = gvResult.DataKeys[index]["isActive"].ToString() == string.Empty ? "False" : gvResult.DataKeys[index]["isActive"].ToString();
                    ContainersDML dml = new ContainersDML();

                    hfEditID.Value = AreaID.ToString();

                    if (Active == "True")
                    {
                        dml.DeactivateContainer(AreaID, LoginID);
                    }
                    else
                    {
                        dml.ActivateContainer(AreaID, LoginID);
                    }
                    GetContainer();
                    ResetFields();

                }

            }
            catch (Exception ex)
            {
                notification("Error", "Error getting container info, due to: " + ex.Message);
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

        protected void lnkConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                string Action = hfConfirmAction.Value;
                if (Action == "Delete")
                {
                    try
                    {

                        Int64 ContainerID = Convert.ToInt64(hfEditID.Value);
                        ContainersDML dmlContainer = new ContainersDML();
                        dmlContainer.DeleteContainer(ContainerID);

                        ResetFields();
                        GetContainer();
                        pnlInput.Visible = false;
                        notification("Success", "Deleted Successfully");
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
                        string ContainerNo = txtContainerNo.Text.Trim();
                        string Code = txtCode.Text.Trim();
                        Int64 ContainerType = Convert.ToInt64(ddlContainerType.SelectedValue);
                        string ShippingCompany = txtShippingCompany.Text;
                        string Owner = txtOwner.Text.Trim();
                        Int64 OwnerContact = txtOwnerContact.Text == string.Empty ? 0 : Convert.ToInt64(txtOwnerContact.Text);
                        string Ownership = ddlOwnerShipType.SelectedValue;
                        string Description = txtDescription.Text;
                        ContainersDML dmlContainers = new ContainersDML();
                        dmlContainers.InsertContainer(ContainerNo, Code, ContainerType, ShippingCompany, Owner, OwnerContact, Description, Ownership, LoginID);
                        notification("Success", "Submited Successfully");
                        GetContainer();
                        ResetFields();
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
                        string ContainerNo = txtContainerNo.Text.Trim();
                        string Code = txtCode.Text.Trim();
                        Int64 ContainerType = ddlContainerType.SelectedIndex == 0 ? 0 : Convert.ToInt64(ddlContainerType.SelectedValue);
                        string ShippingCompany = txtShippingCompany.Text;
                        string Owner = txtOwner.Text.Trim();
                        Int64 OwnerContact = txtOwnerContact.Text == string.Empty ? 0 : Convert.ToInt64(txtOwnerContact.Text);
                        string Ownership = ddlOwnerShipType.SelectedValue;
                        string Description = txtDescription.Text;

                        ContainersDML dmlContainers = new ContainersDML();
                        Int64 ContainerID = Convert.ToInt64(hfEditID.Value);
                        dmlContainers.UpdateContainer(ContainerID, ContainerNo, Code, ContainerType, ShippingCompany, Owner, OwnerContact, Description, Ownership, LoginID);
                        notification("Success", "Updated Successfully");
                        GetContainer();
                        ResetFields();
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