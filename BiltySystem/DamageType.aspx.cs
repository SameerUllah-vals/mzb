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
    public partial class DamageType : System.Web.UI.Page
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

        private void GetDamageType()
        {
            DamageTypeDML dml = new DamageTypeDML();

            DataTable dt = dml.GetDamageType();
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

                if (!IsPostBack)
                {
                    if (LoginID > 0 && LoginID != null)
                    {
                        Title = "Damage Type";
                        GetDamageType();
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
                    string des = txtDescription.Text;


                    DamageTypeDML cat = new DamageTypeDML();
                    DataTable dt = cat.GetDamageType(Code, name);
                    if (hfEditID.Value.ToString() == string.Empty)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            notification("Error", "Another DamageType with same name or code exists in database, try to change name or code");
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
                            notification("Error", "Another DamageType with same name or code exists in database, try to change name or code");
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
                notification("Error", "Error deleting , due to: " + ex.Message);
            }
        }

        protected void lnkCancelSearch_Click(object sender, EventArgs e)
        {
            try
            {
                GetDamageType();
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
                    pnlView.Visible = false;
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvResult.Rows[index];
                    Int64 DamageID = Convert.ToInt64(gvResult.DataKeys[index]["DamageTypeID"]);
                    hfEditID.Value = DamageID.ToString();


                    DamageTypeDML dml = new DamageTypeDML();
                    DataTable dtcat = dml.GetDamageType(DamageID);
                    if (dtcat.Rows.Count > 0)
                    {
                        txtCode.Text = dtcat.Rows[0]["Code"].ToString();
                        txtName.Text = dtcat.Rows[0]["Name"].ToString();
                        txtDescription.Text = dtcat.Rows[0]["Description"].ToString();



                        lnkDelete.Visible = true;
                    }
                }
                else if (e.CommandName == "View")
                {
                    pnlInput.Visible = false;
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);

                    //ClearFields();
                    pnlInput.Visible = false;
                    pnlView.Visible = true;

                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvResult.Rows[index];
                    Int64 ID = Convert.ToInt64(gvResult.DataKeys[index]["DamageTypeID"]);



                    DamageTypeDML dml = new DamageTypeDML();
                    DataTable dtcat = dml.GetDamageType(ID);
                    if (dtcat.Rows.Count > 0)
                    {
                        lblCode.Text = dtcat.Rows[0]["Code"].ToString();
                        lblName.Text = dtcat.Rows[0]["Name"].ToString();
                        lblDescription.Text = dtcat.Rows[0]["Description"].ToString();




                    }
                }



              else if (e.CommandName == "Active")
                {

                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvResult.Rows[index];
                    Int64 ID = Convert.ToInt64(gvResult.DataKeys[index]["DamageTypeID"]);
                    string Active = gvResult.DataKeys[index]["isActive"].ToString() == string.Empty ? "False" : gvResult.DataKeys[index]["isActive"].ToString();
                    DamageTypeDML dml = new DamageTypeDML();

                    hfEditID.Value = ID.ToString();

                    if (Active == "True")
                    {
                        dml.DeactivateDamage(ID, LoginID);
                    }
                    else
                    {
                        dml.ActivateDamage(ID, LoginID);
                    }
                    GetDamageType();
                    clearfield();

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
            if (keyword != string.Empty)
            {
                DamageTypeDML dm = new DamageTypeDML();
                DataTable dtdamage = dm.GetDamageType(keyword);
                if (dtdamage.Rows.Count > 0)
                {
                    gvResult.DataSource = dtdamage;
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
                        DamageTypeDML dml = new DamageTypeDML();
                        dml.DeleteDamageType(id);
                        clearfield();
                        lnkDelete.Visible = false;
                        notification("Success", "Deleted Successfully");
                        GetDamageType();
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

                        DamageTypeDML cat = new DamageTypeDML();
                        cat.InsertDamageType(Code, name, des, LoginID);
                        pnlInput.Visible = false;
                        notification("Success", "Submited Successfully");
                        clearfield();
                        GetDamageType();
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

                        DamageTypeDML cat = new DamageTypeDML();
                        Int64 id = Convert.ToInt64(hfEditID.Value);
                        cat.UpdateDamageType(id, Code, name, des, LoginID);
                        pnlInput.Visible = false;
                        notification("Success", "Updated Successfully");
                        clearfield();
                        GetDamageType();
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