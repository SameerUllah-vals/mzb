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
    public partial class City : System.Web.UI.Page
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
                    this.Title = "Cities";
                    GetAllProvince();

                    GetAllCities();
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

        public void GetAllProvince()
        {
            try
            {
                CityDML dml = new CityDML();
                DataTable dtProvinces = dml.GetProvince();
                if (dtProvinces.Rows.Count > 0)
                {
                    FillDropDown(dtProvinces, ddlProvince, "ProvinceID", "ProvinceName", "-Select-");
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting/binding provinces, due to: " + ex.Message);
            }
        }

        public void GetAllCities()
        {
            try
            {
                CityDML dml = new CityDML();
                DataTable dtCities = dml.GetCities();
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

        public void ClearFields()
        {
            try
            {
                hfEditID.Value = string.Empty;
                txtCode.Text = string.Empty;
                txtName.Text = string.Empty;
                ddlProvince.SelectedIndex = 0;
                txtDescription.Text = string.Empty;

                //pnlInput.Visible = false;
            }
            catch (Exception ex)
            {
                notification("Error", "Error clearing fields, due to: " + ex.Message);
            }
        }

        #endregion

            #region Events

        

            protected void gvResult_RowCommand(object sender, GridViewCommandEventArgs e)
            {
                if (e.CommandName == "Change")
                {
                    pnlInput.Visible = true;
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvResult.Rows[index];
                    Int64 ID = Convert.ToInt64(gvResult.DataKeys[index]["CityID"]);
                    hfEditID.Value = ID.ToString();


                    CityDML dml = new CityDML();
                    DataTable dtcity = dml.GetCities(ID);
                    if (dtcity.Rows.Count > 0)
                    {

                        txtCode.Text = dtcity.Rows[0]["CityCode"].ToString();
                        txtName.Text = dtcity.Rows[0]["CityName"].ToString();
                        txtDescription.Text = dtcity.Rows[0]["Description"].ToString();







                        ddlProvince.ClearSelection();
                        ddlProvince.Items.FindByValue(dtcity.Rows[0]["ProvinceID"].ToString()).Selected = true;

                        bool chek = Convert.ToBoolean(dtcity.Rows[0]["IsActive"]);


                    lnkDelete.Visible = true;
                        GetAllCities();
                    }
                }
                else if (e.CommandName == "View")
                {
                    pnlView.Visible = true;
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvResult.Rows[index];
                    Int64 ID = Convert.ToInt64(gvResult.DataKeys[index]["CityID"]);
                    hfEditID.Value = ID.ToString();


                    CityDML dml = new CityDML();

                    DataTable dtcity = dml.GetCities(ID);
                    if (dtcity.Rows.Count > 0)
                    {

                        txtCodeModal.Text = dtcity.Rows[0]["CityCode"].ToString();
                        txtNameModal.Text = dtcity.Rows[0]["CityName"].ToString();
                        txtDescriptionModal.Text = dtcity.Rows[0]["Description"].ToString();

                        txtProvinceModal.Text = dtcity.Rows[0]["ProvinceName"].ToString();
                    }
                }
                else if (e.CommandName == "Active")
                {

                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvResult.Rows[index];
                    Int64 CityID = Convert.ToInt64(gvResult.DataKeys[index]["CityID"]);
                    string Active = gvResult.DataKeys[index]["isActive"].ToString() == string.Empty ? "False" : gvResult.DataKeys[index]["isActive"].ToString();
                    CityDML dml = new CityDML();

                    //hfEditID.Value = CityID.ToString();

                    if (Active == "True")
                    {
                        dml.DeactivateCity(CityID, LoginID);
                    }
                    else
                    {
                        dml.ActivateCity(CityID, LoginID);
                    }
                    GetAllCities();
                    
                }
        }

        //protected void btnModal_Click(object sender, EventArgs e)
        //{
        //    if (btnModal.Text == "Delete")
        //    {
        //        try
        //        {

        //            Int64 CityID = Convert.ToInt64(hfEditID.Value);
        //            if (CityID > 0)
        //            {
        //                CityDML dml = new CityDML();
        //                dml.DeleteCity(CityID);
        //                notification("Success", "City deleted successfully");

        //                ClearFields();

        //                GetAllCities();
        //                pnlInput.Visible = false;
        //            }
        //            else
        //            {
        //                notification("Error", "No city selected to delete");
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            notification("Error", ex.Message);
        //        }
        //    }
        //    else if (btnModal.Text == "Save")
        //    {
        //        string Code = txtCode.Text;
        //        string name = txtName.Text;
        //        Int64 city = ddlCity.SelectedIndex == 0 ? 0 : Convert.ToInt64(ddlCity.SelectedValue);
        //        Int64 Region = ddlRegion.SelectedIndex == 0 ? 0 : Convert.ToInt64(ddlRegion.SelectedValue);
        //        string Province = ddlProvince.SelectedValue;
        //        string description = txtDescription.Text;
        //        //int active = cbActive.Checked == true ? 1 : 0;

        //        AreaDML area = new AreaDML();
        //        area.InsertArea(Code, name, city, Province, Region, description, LoginID);
        //        pnlInput.Visible = false;
        //        notification("Success", "Submited Successfully");
        //        clearfield();
        //        GetArea();
        //    }
        //    else if (btnModal.Text == "Update")
        //    {
        //        string Code = txtCode.Text;
        //        string name = txtName.Text;
        //        Int64 city = ddlCity.SelectedIndex == 0 ? 0 : Convert.ToInt64(ddlCity.SelectedValue);
        //        Int64 Region = ddlRegion.SelectedIndex == 0 ? 0 : Convert.ToInt64(ddlRegion.SelectedValue);
        //        string Province = ddlProvince.SelectedValue;
        //        string description = txtDescription.Text;
        //        //int active = cbActive.Checked == true ? 1 : 0;
        //        AreaDML area = new AreaDML();

        //        Int64 id = Convert.ToInt64(hfEditID.Value);
        //        area.UpdateArea(id, Code, name, city, Province, Region, description, LoginID);
        //        pnlInput.Visible = false;
        //        notification("Success", "Updated Successfully");
        //        GetArea();
        //        clearfield();
        //    }
        //}
        protected void btnCloseInput_Click(object sender, EventArgs e)
            {
                try
                {
                    ClearFields();
                    //btnCloseInput.Visible = false;

                }
                catch (Exception ex)
                {
                    notification("Error", "Error closing input panel, due to: " + ex.Message);
                }
            }

            protected void btnSearch_Click(object sender, EventArgs e)
            {
                try
                {
                    if (txtSearch.Text.Trim() != string.Empty)
                    {
                        string Keyword = txtSearch.Text;
                        CityDML dmlCity = new CityDML();
                        DataTable dtCity = dmlCity.GetCities(Keyword);
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
                    notification("Error", "Error searching City, due to: " + ex.Message);
                }
            }

            protected void btnCloseSearch_Click(object sender, EventArgs e)
            {
                try
                {
                    txtSearch.Text = string.Empty;

                    GetAllCities();
                }
                catch (Exception ex)
                {
                    notification("Error", "Error searching, due to: " + ex.Message);
                }
            }

            protected void btnDeleteCity_Click(object sender, EventArgs e)
            {
                try
                {
                    Int64 CityID = Convert.ToInt64(hfEditID.Value);
                    CityDML dmlCity = new CityDML();
                    dmlCity.DeleteCity(CityID);

                    notification("Success", "City deleted successfully");
                    ClearFields();
                    GetAllCities();
                    pnlInput.Visible = false;
                }
                catch (Exception ex)
                {
                    notification("Error", "Error deleting city, due to : " + ex.Message);
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



            protected void btnDeleteCity_Click1(object sender, EventArgs e)
            {

            }

            protected void btnEnableInput_Click(object sender, EventArgs e)
            {
                try
                {
                    pnlInput.Visible = true;
                    lnkDelete.Visible = false;
                }
                catch (Exception ex)
                {

                    notification("error", "Error enabling input due to:" + ex.Message);
                }
            }

            protected void lnkCancelSearch_Click(object sender, EventArgs e)
            {
                try
                {

                    txtSearch.Text = "";
                    GetAllCities();
                }
                catch (Exception ex)
                {
                    notification("Error", "Error canceling search, due to: " + ex.Message);
                }
            }

            protected void btnEnableInput_Click1(object sender, EventArgs e)
            {

            }

            protected void btnCloseInput_Click1(object sender, EventArgs e)
            {

            }

            protected void btncloseinput_Click2(object sender, EventArgs e)
            {
                pnlInput.Visible = false;
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
        protected void lnkSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCode.Text.Trim() == string.Empty)
                {
                    notification("Error", "Please enter city code");
                    txtCode.Focus();
                }
                else if (txtName.Text.Trim() == string.Empty)
                {
                    notification("Error", "Please enter city name");
                    txtName.Focus();
                }

                else
                {
                    string code = txtCode.Text.Trim();
                    string name = txtName.Text.Trim();


                    CityDML dmlCity = new CityDML();
                    DataTable dtDuplicate = dmlCity.GetCities(code, name);
                    if (hfEditID.Value == string.Empty)
                    {
                        if (dtDuplicate.Rows.Count > 0)
                        {
                            notification("Error", "City code or name already exists in database");
                            txtCode.Focus();
                        }
                        else
                        {
                            lblMessage.Text = "Are you sure want save?";

                            ConfirmModal("Are you sure want to save?", "Save");



                        }
                    }
                    else
                    {
                        if (dtDuplicate.Rows.Count > 1)
                        {
                            notification("Error", "City code or name already exists in database");
                            txtCode.Focus();
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
            }
            catch (Exception ex)
            {
                notification("Error", "Error saving city, due to: " + ex.Message);
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
                notification("Error", "Error deleting Area, due to: " + ex.Message);
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
                        Int64 CityID = Convert.ToInt64(hfEditID.Value);
                        CityDML dmlCity = new CityDML();
                        dmlCity.DeleteCity(CityID);

                        notification("Success", "City deleted successfully");
                        ClearFields();
                        GetAllCities();
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
                        string code = txtCode.Text.Trim();
                        string name = txtName.Text.Trim();
                        Int64 province = Convert.ToInt64(ddlProvince.SelectedValue);
                        string description = txtDescription.Text.Trim();


                        CityDML dmlCity = new CityDML();
                        dmlCity.InsertCity(code, name, province, description, LoginID);

                        notification("Success", "City inserted successfully");
                        ClearFields();
                        GetAllCities();
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
                        string code = txtCode.Text.Trim();
                        string name = txtName.Text.Trim();
                        Int64 province = Convert.ToInt64(ddlProvince.SelectedValue);
                        string description = txtDescription.Text.Trim();

                        Int64 CityID = Convert.ToInt64(hfEditID.Value);
                        CityDML dmlCity = new CityDML();
                        dmlCity.UpdateCity(CityID, code, name, description, province, LoginID);
                        notification("Success", "City Updated successfully");
                        ClearFields();
                        GetAllCities();
                        pnlInput.Visible = false;
                    }
                    catch (Exception ex)
                    {
                        notification("Error", "Error updating area, due to: " + ex.Message);
                        modalConfirm.Hide();
                    }
                }
            }
            catch(Exception ex)
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
                    string Keyword = txtSearch.Text;
                    CityDML dmlCity = new CityDML();
                    DataTable dtCity = dmlCity.GetCities(Keyword);
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
                notification("Error", "Error searching City, due to: " + ex.Message);
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
            catch (Exception EX)
            {

                notification("Error", EX.Message);
            }
        }


    }
}
