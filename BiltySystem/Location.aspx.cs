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
    public partial class Location : System.Web.UI.Page
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
                    this.Title = "Locations";
                    GetAndBindLocations();
                    GetAndPopulateLocationTypes();
                    GetAndPopulateArea();
                    GetAndPopulateCity();
                    //GetAndPopulateProvince();
                    //GetAndPopulateRegion();
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

        public void GetAndBindLocations()
        {
            try
            {
                LocationDML dml = new LocationDML();
                DataTable dtLocations = dml.GetLocation();
                if (dtLocations.Rows.Count > 0)
                {
                    gvResult.DataSource = dtLocations;
                }
                else
                {
                    gvResult.DataSource = null;
                }
                gvResult.DataBind();
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting/binding Locations, due to: " + ex.Message);
            }
        }

        public void GetAndPopulateArea()
        {
            try
            {
                AreaDML dml = new AreaDML();
                DataTable dtArea = dml.GetArea();
                if (dtArea.Rows.Count > 0)
                {
                    FillDropDown(dtArea, ddlArea, "ID", "AreaName", "-Select Area-");
                }
                else
                {
                    ddlArea.Items.Clear();
                    notification("Error", "No area found, please add area first.");
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting/populating area, due to: " + ex.Message);
            }
        }

        //public void GetAndPopulateProvince()
        //{
        //    try
        //    {
        //        LocationDML dml = new LocationDML();
        //        DataTable dtProvince = dml.GetProvince();
        //        if (dtProvince.Rows.Count > 0)
        //        {
        //            FillDropDown(dtProvince, ddlProvince, "ProvinceID", "ProvinceName", "-Select Province-");
        //        }
        //        else
        //        {
        //            ddlProvince.Items.Clear();
        //            notification("Error", "No province found, please add province first.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notification("Error", "Error getting/populating province, due to: " + ex.Message);
        //    }
        //}

        public void GetAndPopulateLocationTypes()
        {
            try
            {
                LocationDML dml = new LocationDML();
                DataTable dtLocationType = dml.GetLocationType();
                if (dtLocationType.Rows.Count > 0)
                {
                    FillDropDown(dtLocationType, ddlType, "LocationTypeID", "LocationTypeName", "-Select Type-");
                }
                else
                {
                    ddlType.Items.Clear();
                    notification("Error", "No Location type found, please add type first.");
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting/populating location types, due to: " + ex.Message);
            }
        }

        public void GetAndPopulateCity()
        {
            try
            {
                CityDML dml = new CityDML();
                DataTable dtCity = dml.GetCities();
                if (dtCity.Rows.Count > 0)
                {
                    FillDropDown(dtCity, ddlCity, "CityID", "CityName", "-Select city-");
                }
                else
                {
                    ddlCity.Items.Clear();
                    notification("Error", "No City found, please add city first.");
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting/populating city, due to: " + ex.Message);
            }
        }

        //public void GetAndPopulateRegion()
        //{
        //    try
        //    {
        //        RegionDML dml = new RegionDML();
        //        DataTable dtRegion = dml.GetRegion();
        //        if (dtRegion.Rows.Count > 0)
        //        {
        //            FillDropDown(dtRegion, ddlRegion, "ID", "Name", "-Select Region-");
        //        }
        //        else
        //        {
                    
        //            notification("Error", "No region type found, please add region type first.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notification("Error", "Error getting/populating region, due to: " + ex.Message);
        //    }
        //}

        public void ClearFields()
        {
            try
            {
                hfEditID.Value = string.Empty;
                txtCode.Text = string.Empty;
                txtName.Text = string.Empty;
                txtAddress.Text = string.Empty;
                txtDescription.Text = string.Empty;

               // ddlProvince.ClearSelection();
                ddlCity.ClearSelection();
                //ddlRegion.ClearSelection();
                ddlArea.ClearSelection();
                ddlType.ClearSelection();
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
                    _dt.Columns.Add(_gv.HeaderRow.Cells[i].Text.Replace("&nbsp;", string.Empty));
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
                        _dr[j] = row.Cells[j].Text.Replace("&nbsp;", string.Empty);
                    }
                }
                _dt.Rows.Add(_dr);
            }
            return _dt;
        }

        protected void lnkExport_Click(object sender, EventArgs e)
        {
            try
            {
                ExportToExcel(GridToDT(gvResult), "Locations");
            }
            catch (Exception ex)
            {
                notification("Error", "Error exporting excel, due to: " + ex.Message);
            }
        }

        #endregion

        #region Events

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
                    notification("Error", "Please enter location code");
                    txtCode.Focus();
                }
                else if (txtName.Text.Trim() == string.Empty)
                {
                    notification("Error", "Please enter location name");
                    txtName.Focus();
                }
                //else if (ddlProvince.SelectedIndex == 0)
                //{
                //    notification("Error", "Please select location province");
                //    ddlProvince.Focus();
                //}
                //else if (ddlCity.SelectedIndex == 0)
                //{
                //    notification("Error", "Please select  city");
                //    ddlCity.Focus();
                //}
                ////else if (ddlRegion.SelectedIndex == 0)
                //{
                //    notification("Error", "Please select location region");
                //    ddlRegion.Focus();
                //}
                else if (ddlArea.SelectedIndex == 0)
                {
                    notification("Error", "Please select location area");
                    ddlArea.Focus();
                }
                //else if (ddlType.SelectedIndex == 0)
                //{
                //    notification("Error", "Please select location type");
                //    ddlType.Focus();
                //}
                else
                {
                    string Code = txtCode.Text.Trim();
                    string Name = txtName.Text.Trim();
                    //Int64 ProvinceID = Convert.ToInt64(ddlProvince.SelectedValue);
                    Int64 CityID = Convert.ToInt64(ddlCity.SelectedValue);
                    //Int64 RegionID = Convert.ToInt64(ddlRegion.SelectedValue);
                    Int64 AreaID = Convert.ToInt64(ddlArea.SelectedValue);
                    Int64 LocationTypeID = Convert.ToInt64(ddlType.SelectedValue);
                    string Type = ddlType.SelectedItem.Text;
                    string Address = txtAddress.Text;
                    string Description = txtDescription.Text;



                    LocationDML dmlLocation = new LocationDML();
                    DataTable dtLocation = dmlLocation.GetLocation(Code, Name);

                    if (hfEditID.Value == string.Empty)
                    {
                        if (dtLocation.Rows.Count > 0)
                        {
                            notification("Error", "Another location with same name or code exists in database, try to change name or code");
                        }
                        else
                        {
                            ConfirmModal("Are you sure want to save?", "Save");

                        }
                    }
                    else
                    {

                        if (dtLocation.Rows.Count > 1)
                        {
                            notification("Error", "Another location with same name or code exists in database, try to change name or code");
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
                notification("Error", "Error saving location, due to: " + ex.Message);
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

            GetAndBindLocations();
            pnlInput.Visible = false;
        }

        protected void lnkCancelSearch_Click(object sender, EventArgs e)
        {
            try
            {
                GetAndBindLocations();
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
                Int64 LocationID = Convert.ToInt64(gvResult.DataKeys[index]["PickDropID"]);
                hfEditID.Value = LocationID.ToString();

                LocationDML dmlContainer = new LocationDML();
                DataTable dtLocation = dmlContainer.GetLocation(LocationID);
                if (dtLocation.Rows.Count > 0)
                {

                    txtCode.Text = dtLocation.Rows[0]["PickDropCode"].ToString();
                    txtName.Text = dtLocation.Rows[0]["PickDropLocationName"].ToString();

                    //txtOwner.Text = dtContainer.Rows[0]["Owner"].ToString();
                    txtAddress.Text = dtLocation.Rows[0]["Address"].ToString();
                    txtDescription.Text = dtLocation.Rows[0]["Description"].ToString();

                    //ddlProvince.ClearSelection();
                    //ddlProvince.Items.FindByValue(dtLocation.Rows[0]["Province"].ToString()).Selected = true;

                    try
                    {
                        CityDML dmlCity = new CityDML();
                        DataTable dtCity = dmlCity.GetCitieSByProvince(Convert.ToInt64(dtLocation.Rows[0]["Province"].ToString()));
                        if (dtCity.Rows.Count > 0)
                        {
                            FillDropDown(dtCity, ddlCity, "CityID", "CityName", "- Select -");
                        }                        
                    }
                    catch (Exception ex)
                    {
                        notification("Error", "Error getting cities by selected province, due to: " + ex.Message);
                    }
                    
                    ddlCity.ClearSelection();
                    ddlCity.Items.FindByValue(dtLocation.Rows[0]["CityID"].ToString()).Selected = true;

                    //try
                    //{
                    //    RegionDML dmlRegion = new RegionDML();
                    //    DataTable dtRegion = dmlRegion.GetRegionByCity(Convert.ToInt64(ddlCity.SelectedItem.Value));
                    //    if (dtRegion.Rows.Count > 0)
                    //    {
                    //        FillDropDown(dtRegion, ddlRegion, "ID", "Name", "- Select -");
                    //    }
                    //}
                    //catch (Exception ex)
                    //{
                    //    notification("Error", "Error getting regions by selected City, due to: " + ex.Message);
                    //}

                    //ddlRegion.ClearSelection();
                    //ddlRegion.Items.FindByValue(dtLocation.Rows[0]["RegionID"].ToString()).Selected = true;


                    //try
                    //{
                    //    AreaDML dmlArea = new AreaDML();
                    //    DataTable dtArea = dmlArea.GetAreaByRegion(Convert.ToInt64(ddlRegion.SelectedItem.Value));
                    //    if (dtArea.Rows.Count > 0)
                    //    {
                    //        FillDropDown(dtArea, ddlArea, "ID", "AreaName", "- Select -");
                    //    }
                    //}
                    //catch (Exception ex)
                    //{
                    //    notification("Error", "Error getting Areas by selected Region, due to: " + ex.Message);
                    //}
                    ddlArea.ClearSelection();
                    ddlArea.Items.FindByValue(dtLocation.Rows[0]["AreaID"].ToString()).Selected = true;

                    ddlType.ClearSelection();
                    ddlType.Items.FindByValue(dtLocation.Rows[0]["LocationTypeID"].ToString()).Selected = true;

                   



                    ////btnDeleteContainer.Visible = true;
                    lnkDelete.Visible = true;
                  
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
                Int64 LocationID = Convert.ToInt64(gvResult.DataKeys[index]["PickDropID"]);


                LocationDML dmlContainer = new LocationDML();
                DataTable dtLocation = dmlContainer.GetLocation(LocationID);
                if (dtLocation.Rows.Count > 0)
                {

                    lblCodemodal.Text = dtLocation.Rows[0]["PickDropCode"].ToString();
                    lblNameModal.Text = dtLocation.Rows[0]["PickDropLocationName"].ToString();
                    lblAddress.Text = dtLocation.Rows[0]["Address"].ToString();
                    lblDesModal.Text = dtLocation.Rows[0]["Description"].ToString();
                    lblProvinceModal.Text = dtLocation.Rows[0]["ProvinceName"].ToString();
                    lblCityModal.Text = dtLocation.Rows[0]["CityName"].ToString();
                    lblRegionModal.Text = dtLocation.Rows[0]["RegionName"].ToString();
                    lblAreaModal.Text = dtLocation.Rows[0]["AreaName"].ToString();
                    lblType.Text = dtLocation.Rows[0]["LocationTypeID"].ToString();

                }


            }
            else if (e.CommandName == "Active")
            {

                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow gvr = gvResult.Rows[index];
                Int64 CatId = Convert.ToInt64(gvResult.DataKeys[index]["PickDropID"]);
                string Active = gvResult.DataKeys[index]["isActive"].ToString() == string.Empty ? "False" : gvResult.DataKeys[index]["isActive"].ToString();
                LocationDML dml = new LocationDML();

                hfEditID.Value = CatId.ToString();

                if (Active == "True")
                {
                    dml.DeactivateLocation(CatId, LoginID);
                }
                else
                {
                    dml.ActivateLocation(CatId, LoginID);
                }
                GetAndBindLocations();
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
                    GetAndBindLocations();
                }
                else
                {
                    string Keyword = txtSearch.Text;
                    LocationDML dmlLocation = new LocationDML();
                    DataTable dtLocation = dmlLocation.GetLocation(Keyword);
                    if (dtLocation.Rows.Count > 0)
                    {
                        gvResult.DataSource = dtLocation;
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
                        LocationDML dml = new LocationDML();
                        dml.DeleteLocation(LocationID);

                        ClearFields();
                        GetAndBindLocations();

                        notification("Success", "Record deleted successfully");
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
                        //Int64 ProvinceID = Convert.ToInt64(ddlProvince.SelectedValue);
                        //Int64 CityID = Convert.ToInt64(ddlCity.SelectedValue);
                        //Int64 RegionID = Convert.ToInt64(ddlRegion.SelectedValue);
                        Int64 AreaID = Convert.ToInt64(ddlArea.SelectedValue);
                        Int64 LocationTypeID = Convert.ToInt64(ddlType.SelectedValue);
                        string Type = ddlType.SelectedItem.Text;
                        string Address = txtAddress.Text;
                        string Description = txtDescription.Text;


                        LocationDML dmlLocation = new LocationDML();
                        Int64 LocationID = dmlLocation.InsertLocation(Code, Name, AreaID, Address, LocationTypeID, Description, LoginID);
                        if (LocationID > 0)
                        {
                            notification("Success", "Location added successfully.");
                            ClearFields();
                            GetAndBindLocations();
                            pnlInput.Visible = false;
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
                        //Int64 ProvinceID = Convert.ToInt64(ddlProvince.SelectedValue);
                        Int64 CityID = Convert.ToInt64(ddlCity.SelectedValue);
                        //Int64 RegionID = Convert.ToInt64(ddlRegion.SelectedValue);
                        Int64 AreaID = Convert.ToInt64(ddlArea.SelectedValue);
                        Int64 LocationTypeID = Convert.ToInt64(ddlType.SelectedValue);
                        string Type = ddlType.SelectedItem.Text;
                        string Address = txtAddress.Text;
                        string Description = txtDescription.Text;




                        LocationDML dmlLocation = new LocationDML();
                        Int64 LocationID = Convert.ToInt64(hfEditID.Value);
                        dmlLocation.UpdateLocation(Code, Name, AreaID, Address, LocationTypeID, Description, LoginID, LocationID);

                        notification("Success", "Location updated successfully.");
                        ClearFields();
                        GetAndBindLocations();

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

        #endregion

        //protected void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (ddlProvince.SelectedIndex != 0)
        //        {
        //            Int64 ProvinceID = Convert.ToInt64(ddlProvince.SelectedItem.Value);

        //            CityDML dmlCity = new CityDML();
        //            DataTable dtCities = dmlCity.GetCitieSByProvince(ProvinceID);
        //            if (dtCities.Rows.Count > 0)
        //            {
        //                FillDropDown(dtCities, ddlCity, "CityID", "CityName", "- Select -");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notification("Error", "Error selecting Province, due to: " + ex.Message);
        //    }
        //}

        protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlCity.SelectedIndex != 0)
                {
                    Int64 CityID = Convert.ToInt64(ddlCity.SelectedItem.Value);

                    AreaDML dml = new AreaDML();
                    DataTable dtArea = dml.GetAreaByCity(CityID);
                    if (dtArea.Rows.Count > 0)
                    {
                        FillDropDown(dtArea, ddlArea, "ID", "AreaName", "- Select -");
                    }
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error selecting City, due to: " + ex.Message);
            }
        }

        //protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (ddlRegion.SelectedIndex != 0)
        //        {
        //            Int64 RegionID = Convert.ToInt64(ddlRegion.SelectedItem.Value);

        //            AreaDML dmlArea = new AreaDML();
        //            DataTable dtArea = dmlArea.GetAreaByRegion(RegionID);
        //            if (dtArea.Rows.Count > 0)
        //            {
        //                FillDropDown(dtArea, ddlArea, "ID", "AreaName", "- Select -");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notification("Error", "Error selecting Region, due to: " + ex.Message);
        //    }
        //}
    }
}