using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using BLL;
using System.Drawing;
namespace BiltySystem
{
    public partial class Student : System.Web.UI.Page
    {
        int loginid;
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
        protected void Page_Load(object sender, EventArgs e)
        {
            notification();
            if (!IsPostBack)
            {
                if (LoginID > 0)
                {
                    this.Title = "Student";


                    GetAllStudent();
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }

        protected void lnkSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text.Trim() == string.Empty)
                {
                    notification("Error", "Please enter Student Name");
                    txtName.Focus();
                }
                else if (txtClass.Text.Trim() == string.Empty)
                {
                    notification("Error", "Please enter Class");
                    txtClass.Focus();
                }
                else if (txtGender.Text.Trim() == string.Empty)
                {
                    notification("Error", "Please enter Gender");
                    txtGender.Focus();
                }


                else
                {
                    string name = txtName.Text.Trim();
                    string Class = txtClass.Text.Trim();
                    string Gender = txtGender.Text.Trim();


                    CityDML dmlCity = new CityDML();
                    DataTable dtDuplicate = dmlCity.GetStudent(name, Class, Gender);
                    //if (hfEditID.Value == string.Empty)
                    //{
                    //    //if (dtDuplicate.Rows.Count > 0)
                    //    //{
                    //    //    notification("Error", "Name or class already exists in database");
                    //    //    txtName.Focus();
                    //    //}

                        //}
                    if (hfEditID.Value == string.Empty)
                    {
                        lblMessage.Text = "Are you sure want save?";

                        ConfirmModal("Are you sure want to save?", "Save");

                    }

                    else
                    {
                        try
                        {


                            ConfirmModal("Are you sure want to Update?", "Update");

                        }
                        catch (Exception ex)
                        {
                            notification("Error", "Error occurs while updating record, due to: " + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error saving city, due to: " + ex.Message);
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
        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            try
            {
                Int64 ID = Convert.ToInt64(hfEditID.Value);
                CityDML dmlCity = new CityDML();
                dmlCity.DeleteStudent(ID);

                notification("Success", "Student deleted successfully");
                ClearFields();
                GetAllStudent();
                pnlInput.Visible = false;
            }
            catch (Exception ex)
            {
                notification("Error", "Error deleting city, due to : " + ex.Message);
            }
        }

        protected void lnkCloseView_Click(object sender, EventArgs e)
        {

        }

        protected void lnkCancelSearch_Click(object sender, EventArgs e)
        {
            try
            {

                txtSearch.Text = "";
                GetAllStudent();
            }
            catch (Exception ex)
            {
                notification("Error", "Error canceling search, due to: " + ex.Message);
            }
        }

        protected void lnkSearch_Click(object sender, EventArgs e)
        {


        }

        protected void lnkCancelSearch_Click1(object sender, EventArgs e)
        {

        }

        protected void lnkSearch_Click1(object sender, EventArgs e)
        {
            try
            {
                if (txtSearch.Text.Trim() != string.Empty)
                {
                    string Keyword = txtSearch.Text;
                    CityDML dmlCity = new CityDML();
                    DataTable dtCity = dmlCity.GetStudent(Keyword);
                    if (dtCity.Rows.Count > 0)
                    {
                        gvResult.DataSource = dtCity;
                    }
                    else
                    {
                        notification();
                        gvResult.DataSource = null;
                    }
                    gvResult.DataBind();
                }
                else
                {
                    notification("Error", "Please enter keyword to search");
                    txtSearch.Focus();
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error searching Student, due to: " + ex.Message);
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
                notification("Error", "Error enabling input for add new, due to: " + ex.Message);
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
        public void GetAllStudent()
        {
            try
            {
                CityDML dml = new CityDML();
                DataTable dtCities = dml.GetStudent();
                if (dtCities.Rows.Count > 0)
                {
                    gvResult.DataSource = dtCities;
                }
                else
                {
                    gvResult.DataSource = null;
                }
                gvResult.DataBind();
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting/binding cities, due to: " + ex.Message);
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
                        CityDML dmlCity = new CityDML();
                        dmlCity.DeleteStudent(ID);

                        notification("Success", "Student deleted successfully");
                        ClearFields();
                        GetAllStudent();
                        pnlInput.Visible = false;
                    }
                    catch (Exception ex)
                    {

                        notification("Error", ex.Message);
                    }
                }
                if (Action == "Save")
                {
                    try
                    {
                        // string code = txtCode.Text.Trim();
                        string name = txtName.Text.Trim();
                        string Class = txtClass.Text.Trim();
                        //Int64 province = Convert.ToInt64(ddlProvince.SelectedValue);
                        string Gender = txtGender.Text.Trim();
                        string DOB = txtDOB.Text.Trim();


                        CityDML dmlCity = new CityDML();
                        dmlCity.InsertStudent(name, Class, Gender, DOB, LoginID);

                        notification("Success", "Student inserted successfully");
                        ClearFields();
                        GetAllStudent();
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

                        // string code = txtCode.Text.Trim();
                        string name = txtName.Text.Trim();
                        string Class = txtClass.Text.Trim();
                        //Int64 province = Convert.ToInt64(ddlProvince.SelectedValue);
                        string Gender = txtGender.Text.Trim();
                        string DOB = txtDOB.Text.Trim();

                        Int64 ID = Convert.ToInt64(hfEditID.Value);
                        CityDML dmlCity = new CityDML();
                        dmlCity.UpdateStudent(ID, name, Class, Gender, DOB, LoginID);
                        notification("Success", "Student Updated successfully");
                        ClearFields();
                        GetAllStudent();
                        pnlInput.Visible = false;
                    }
                    catch (Exception ex)
                    {
                        notification("Error", "Error updating area, due to: " + ex.Message);
                        modalConfirm.Hide();
                    }
                }
            }
            catch (Exception ex)
            {
                notification("Error", ex.Message);
            }
        }

        protected void lnkcloseinput_Click(object sender, EventArgs e)
        {
            try
            {
                pnlInput.Visible = false;
            }
            catch (Exception EX)
            {

                notification("Error", EX.Message);
            }
        }

        protected void gvResult_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //try
            //{
            //    if (e.Row.RowType == DataControlRowType.DataRow)
            //    {
            //        DataRowView rowView = (DataRowView)e.Row.DataItem;
            //        bool Active = Convert.ToBoolean(rowView["isActive"].ToString() == string.Empty ? "false" : rowView["isActive"]);
            //        LinkButton lnkActive = e.Row.FindControl("lnkActive") as LinkButton;
            //        if (Active == true)
            //        {
            //            lnkActive.CssClass = "fas fa-toggle-on";
            //            //lnkActive.ForeColor = Color.DodgerBlue;
            //            lnkActive.ToolTip = "Switch to Deactivate";

            //        }
            //        else
            //        {
            //            lnkActive.CssClass = "fas fa-toggle-off";
            //            //lnkActive.ForeColor = Color.Maroon;
            //            lnkActive.ToolTip = "Switch to Activate";
            //            //e.Row.BackColor = Color.LightPink;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification("Error", "Binding data to grid, due to: " + ex.Message);
            //}
        }

        protected void btnmodal_Click(object sender, EventArgs e)
        {

        }

        protected void btncancelmodel_Click(object sender, EventArgs e)
        {

        }
        //public void GetAllGender()
        //{
        //    try
        //    {
        //        CityDML dml = new CityDML();
        //        DataTable dtProvinces = dml.GetProvince();
        //        if (dtProvinces.Rows.Count > 0)
        //        {
        //            FillDropDown(dtProvinces, ddlGender, "GenderID", "ProvinceName", "-Select-");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notification("Error", "Error getting/binding provinces, due to: " + ex.Message);
        //    }
        //}
        //private void FillDropDown(DataTable dt, DropDownList _ddl, string _ddlValue, string _ddlText, string _ddlDefaultText)
        //{
        //    if (dt.Rows.Count > 0)
        //    {
        //        _ddl.DataSource = dt;

        //        _ddl.DataValueField = _ddlValue;
        //        _ddl.DataTextField = _ddlText;

        //        _ddl.DataBind();

        //        ListItem item = new ListItem();

        //        item.Text = _ddlDefaultText;
        //        item.Value = _ddlDefaultText;

        //        _ddl.Items.Insert(0, item);
        //        _ddl.SelectedIndex = 0;
        //    }
        //}
        public void ClearFields()
        {
            try
            {
                hfEditID.Value = string.Empty;
                txtName.Text = string.Empty;
                txtClass.Text = string.Empty;
                //ddlProvince.SelectedIndex = 0;
                txtGender.Text = string.Empty;
                txtDOB.Text = string.Empty;

                //pnlInput.Visible = false;
            }
            catch (Exception ex)
            {
                notification("Error", "Error clearing fields, due to: " + ex.Message);
            }
        }

        protected void lnkCloseView_Click1(object sender, EventArgs e)
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

        protected void gvResult_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Change")
            {
                pnlInput.Visible = true;
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow gvr = gvResult.Rows[index];
                Int64 ID = Convert.ToInt64(gvResult.DataKeys[index]["ID"]);
                hfEditID.Value = ID.ToString();


                CityDML dml = new CityDML();
                DataTable dtcity = dml.GetStudent(ID);
                if (dtcity.Rows.Count > 0)
                {

                    txtName.Text = dtcity.Rows[0]["Name"].ToString();
                    txtClass.Text = dtcity.Rows[0]["Class"].ToString();
                    txtGender.Text = dtcity.Rows[0]["Gender"].ToString();
                    txtDOB.Text = dtcity.Rows[0]["DOB"].ToString();

                   
                    bool chek = Convert.ToBoolean(dtcity.Rows[0]["IsActive"]);


                    lnkDelete.Visible = true;
                    GetAllStudent();
                }
            }
            else if (e.CommandName == "View")
            {
                pnlView.Visible = true;
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow gvr = gvResult.Rows[index];
                Int64 ID = Convert.ToInt64(gvResult.DataKeys[index]["ID"]);
                hfEditID.Value = ID.ToString();


                CityDML dml = new CityDML();

                DataTable dtcity = dml.GetStudent(ID);
                if (dtcity.Rows.Count > 0)
                {

                    txtNameModal.Text = dtcity.Rows[0]["Name"].ToString();
                    txtClassModal.Text = dtcity.Rows[0]["Class"].ToString();
                    txtGenderModal.Text = dtcity.Rows[0]["Gender"].ToString();

                    txtDOBModal.Text = dtcity.Rows[0]["DOB"].ToString();
                }
            }
            else if (e.CommandName == "Active")
            {

                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow gvr = gvResult.Rows[index];
                Int64 ID = Convert.ToInt64(gvResult.DataKeys[index]["ID"]);
                string Active = gvResult.DataKeys[index]["IsActive"].ToString() == string.Empty ? "False" : gvResult.DataKeys[index]["IsActive"].ToString();
                CityDML dml = new CityDML();

                //hfEditID.Value = CityID.ToString();

                if (Active == "True")
                {
                    dml.DeactivateStudent(ID, LoginID);
                }
                else
                {
                    dml.ActivateStudent(ID, LoginID);
                }
                GetAllStudent();

            }
        }

        protected void gvResult_RowDataBound1(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView rowView = (DataRowView)e.Row.DataItem;
                    bool Active = Convert.ToBoolean(rowView["IsActive"].ToString() == string.Empty ? "false" : rowView["IsActive"]);
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
    }
}