using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace BiltySystem
{
    public partial class Area : System.Web.UI.Page
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
                    this.Title = "Areas";
                    GetArea();
                    GetCity();
                    GetRegion();
                    GetAndPopulateProvince();
                    //cbActive.Checked = true;

                }

            }
        }

        #endregion

        #region Custom Methods

        public void GetAndPopulateProvince()
        {
            try
            {
                AreaDML dml = new AreaDML();
                DataTable dtProvince = dml.GetProvince();
                if (dtProvince.Rows.Count > 0)
                {
                    FillDropDown(dtProvince, ddlProvince, "ProvinceName", "ProvinceName", "-Select province-");
                }
                else
                {
                    ddlProvince.Items.Clear();
                    notification("Error", "No province found, please add province first.");
                }
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

        public void GetCity()
        {
            try
            {
                CityDML dml = new CityDML();
                DataTable dtCity = dml.GetCities();
                if (dtCity.Rows.Count > 0)
                {
                    FillDropDown(dtCity, ddlCity, "CityID", "CityName", "-Select City-");
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting/binding Cities, due to: " + ex.Message);
            }
        }

        public void GetRegion()
        {
            try
            {
                RegionDML dml = new RegionDML();
                DataTable dtRegion = dml.GetRegion();
                if (dtRegion.Rows.Count > 0)
                {
                    FillDropDown(dtRegion, ddlRegion, "ID", "Name", "-Select Region-");
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting/binding Region, due to: " + ex.Message);
            }
        }

        public void GetArea()
        {
            try
            {
                AreaDML dml = new AreaDML();
                DataTable dtArea = dml.GetArea();
                if (dtArea.Rows.Count > 0)
                {
                    gvResult.DataSource = dtArea;
                }
                else
                {
                    gvResult.DataSource = null;
                }
                gvResult.DataBind();
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting Areas, due to: " + ex.Message);
            }
        }

        public void GetArea(string Keyword)
        {
            try
            {
                AreaDML dm = new AreaDML();
                DataTable dtarea = dm.GetArea(Keyword);
                if (dtarea.Rows.Count > 0)
                {
                    gvResult.DataSource = dtarea;
                }
                else
                {
                    gvResult.DataSource = null;
                }
                gvResult.DataBind();
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting area, due to: " + ex.Message);
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


        private void clearfield()
        {
            hfEditID.Value = string.Empty;
            txtCode.Text = "";
            txtDescription.Text = "";
            txtName.Text = "";
            ddlProvince.ClearSelection();
            ddlCity.ClearSelection();
            ddlRegion.ClearSelection();
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

        #endregion

        #region Events

        protected void btnCloseInput_Click(object sender, EventArgs e)
        {
            pnlInput.Visible = false;
        }

        protected void btnEnableInput_Click(object sender, EventArgs e)
        {
            pnlInput.Visible = true;
        }

        protected void lnkCancelSearch_Click(object sender, EventArgs e)
        {
            try
            {
                GetArea();
                txtSearch.Text = string.Empty;
            }
            catch (Exception ex)
            {
                notification("Error", ex.Message);
            }
        }

        protected void lnkCloseView_Click(object sender, EventArgs e)
        {
            pnlView.Visible = false;

        }

        //protected void btnModal_Click(object sender, EventArgs e)
        //{
        //    if (btnModal.Text == "Delete")
        //    {
        //        try
        //        {

        //            Int64 AreaID = Convert.ToInt64(hfEditID.Value);
        //            if (AreaID > 0)
        //            {
        //                AreaDML dml = new AreaDML();
        //                dml.DeleteArea(AreaID);
        //                notification("Success", "Area deleted successfully");

        //                clearfield();

        //                GetArea();
        //                pnlInput.Visible = false;
        //            }
        //            else
        //            {
        //                notification("Error", "No area selected to delete");
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
                    Int64 AreaID = Convert.ToInt64(gvResult.DataKeys[index]["ID"]);
                    hfEditID.Value = AreaID.ToString();


                    AreaDML dml = new AreaDML();
                    DataTable dtArea = dml.GetArea(AreaID);
                    if (dtArea.Rows.Count > 0)
                    {
                        hfEditID.Value = dtArea.Rows[0]["ID"].ToString();
                        txtCode.Text = dtArea.Rows[0]["AreaCode"].ToString();
                        txtName.Text = dtArea.Rows[0]["AreaName"].ToString();


                        ddlProvince.ClearSelection();
                        ddlProvince.Items.FindByValue(dtArea.Rows[0]["Province"].ToString()).Selected = true;

                        ddlCity.ClearSelection();
                        ddlCity.Items.FindByValue(dtArea.Rows[0]["CityID"].ToString()).Selected = true;

                        ddlRegion.ClearSelection();
                        ddlRegion.Items.FindByValue(dtArea.Rows[0]["Region"].ToString()).Selected = true;

                        txtDescription.Text = dtArea.Rows[0]["Description"].ToString();

                        //cbActive.Checked = false;
                        //if (dtArea.Rows[0]["Status"].ToString() == "True")
                        //    cbActive.Checked = true;


                        lnkDelete.Visible = true;
                    }
                    else
                    {
                        notification("Error", "No such area found for update");
                    }
                }
                else if (e.CommandName == "View")
                {

                    pnlInput.Visible = false;
                    pnlView.Visible = true;


                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvResult.Rows[index];
                    Int64 AreaID = Convert.ToInt64(gvResult.DataKeys[index]["ID"]);



                    AreaDML dml = new AreaDML();
                    DataTable dtArea = dml.GetArea(AreaID);
                    if (dtArea.Rows.Count > 0)
                    {

                        txtCodeModal.Text = dtArea.Rows[0]["AreaCode"].ToString();
                        txtNameModal.Text = dtArea.Rows[0]["AreaName"].ToString();
                        txtProvinceModal.Text = dtArea.Rows[0]["Province"].ToString();



                        txtCityModal.Text = dtArea.Rows[0]["CityName"].ToString();


                        txtRegionModal.Text = dtArea.Rows[0]["RegionName"].ToString();

                        txtDescriptionModal.Text = dtArea.Rows[0]["Description"].ToString();


                    }

                }
                else if (e.CommandName == "Delete")
                {

                    //int index = Convert.ToInt32(e.CommandArgument);
                    //GridViewRow gvr = gvResult.Rows[index];
                    //Int64 AreaID = Convert.ToInt64(gvResult.DataKeys[index]["ID"]);
                    //hfEditID.Value = AreaID.ToString();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);


                }
                else if (e.CommandName == "Active")
                {

                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvResult.Rows[index];
                    Int64 AreaID = Convert.ToInt64(gvResult.DataKeys[index]["ID"]);
                    string Active = gvResult.DataKeys[index]["Status"].ToString()== string.Empty ? "False" : gvResult.DataKeys[index]["Status"].ToString();
                    AreaDML dml = new AreaDML();

                    hfEditID.Value = AreaID.ToString();

                    if (Active == "True")
                    {
                        dml.DeactivateArea(AreaID, LoginID);
                    }
                    else
                    {
                        dml.ActivateArea(AreaID, LoginID);
                    }
                    GetArea();

                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting information for edit, due to: " + ex.Message);
            }

        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {


            AreaDML dm = new AreaDML();
            DataTable dtarea = dm.GetArea(txtSearch.Text.Trim());
            if (dtarea.Rows.Count > 0)
            {
                gvResult.DataSource = dtarea;
            }
            else
            {
                gvResult.DataSource = null;
            }
            gvResult.DataBind();
        }

        protected void btnEnableInput_Click1(object sender, EventArgs e)
        {
            pnlInput.Visible = true;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            pnlInput.Visible = true;
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

        protected void lnkSubmit_Click(object sender, EventArgs e)
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
                else if (ddlCity.SelectedIndex == 0)
                {
                    notification("Error", "Please Select City.");
                    ddlCity.Focus();
                }
                else if (ddlProvince.SelectedIndex == 0)
                {
                    notification("Error", "Please Select Province.");
                    ddlProvince.Focus();
                }

                else if (ddlRegion.SelectedIndex == 0)
                {
                    notification("Error", "Please Select Region.");
                    ddlRegion.Focus();
                }
                else
                {
                    string Code = txtCode.Text;
                    string name = txtName.Text;
                    AreaDML area = new AreaDML();
                    DataTable dt = area.GetArea(Code, name);

                    if (hfEditID.Value.ToString() == string.Empty)
                    {

                        if (dt.Rows.Count > 0)
                        {

                            notification("Error", "Another area with same name or code exist in database please try to change name or code");

                        }

                        else
                        {
                           
                            ConfirmModal("Are you sure want to save?", "Save");
                            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                        }
                    }
                    else
                    {
                        if (dt.Rows.Count > 1)
                        {
                            notification("Error", "Another Area with same name or code exists in database, try to change name or code");
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
                //lblMessage.Text = "Are you sure want to delete this?";
                //btnModal.Text = "Delete";
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);

            }
            catch (Exception ex)
            {
                notification("Error", "Error deleting Area, due to: " + ex.Message);
            }
        }

        protected void lnkSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSearch.Text.Trim() != string.Empty)
                {
                    string keyword = txtSearch.Text.Trim();
                    GetArea(keyword);
                }
                else
                {
                    GetArea();
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
                        Int64 AreaID = Convert.ToInt64(hfEditID.Value);
                        if (AreaID > 0)
                        {
                            AreaDML dml = new AreaDML();
                            dml.DeleteArea(AreaID);
                            notification("Success", "Area deleted successfully");

                            clearfield();

                            GetArea();
                            pnlInput.Visible = false;
                        }
                        else
                        {
                            notification("Error", "No area selected to delete");
                        }
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
                        Int64 city = ddlCity.SelectedIndex == 0 ? 0 : Convert.ToInt64(ddlCity.SelectedValue);
                        Int64 Region = ddlRegion.SelectedIndex == 0 ? 0 : Convert.ToInt64(ddlRegion.SelectedValue);
                        string Province = ddlProvince.SelectedValue;
                        string description = txtDescription.Text;
                        //int active = cbActive.Checked == true ? 1 : 0;

                        AreaDML area = new AreaDML();
                        area.InsertArea(Code, name, city, Province, Region, description, LoginID);
                        pnlInput.Visible = false;
                        notification("Success", "Submited Successfully");
                        clearfield();
                        GetArea();
                    }
                    catch (Exception ex)
                    {
                        notification("Error", "Error saving area, due to: " + ex.Message);
                        modalConfirm.Hide();
                    }

                }
                else if (Action == "Update")
                {
                    try
                    {
                        string Code = txtCode.Text;
                        string name = txtName.Text;
                        Int64 city = ddlCity.SelectedIndex == 0 ? 0 : Convert.ToInt64(ddlCity.SelectedValue);
                        Int64 Region = ddlRegion.SelectedIndex == 0 ? 0 : Convert.ToInt64(ddlRegion.SelectedValue);
                        string Province = ddlProvince.SelectedValue;
                        string description = txtDescription.Text;
                        //int active = cbActive.Checked == true ? 1 : 0;
                        AreaDML area = new AreaDML();

                        Int64 id = Convert.ToInt64(hfEditID.Value);
                        area.UpdateArea(id, Code, name, city, Province, Region, description, LoginID);
                        pnlInput.Visible = false;
                        notification("Success", "Updated Successfully");
                        GetArea();
                        clearfield();
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
                notification("Error", "Error confirming, due to: " + ex.Message);
            }
        }

        protected void gvResult_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView rowView = (DataRowView)e.Row.DataItem;
                    bool Active = Convert.ToBoolean(rowView["Status"].ToString() == string.Empty ? "false" : rowView["Status"]);
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

        #endregion

        protected void lnkConfirm_Click1(object sender, EventArgs e)
        {

        }

       
    }
}