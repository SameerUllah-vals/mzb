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
    public partial class Region : System.Web.UI.Page
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
                    this.Title = "Region";
                    GetAndBindRegion();

                    GetAndPopulateProvince();
                    GetAndPopulateCity();
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

        public void GetAndBindRegion()
        {
            try
            {
                RegionDML dml = new RegionDML();
                DataTable dtRegions = dml.GetRegion();
                if (dtRegions.Rows.Count > 0)
                {
                    gvResult.DataSource = dtRegions;
                }
                else
                {
                    gvResult.DataSource = null;
                }
                gvResult.DataBind();
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting regions, due to: " + ex.Message);
            }
        }

        public void GetAndPopulateProvince()
        {
            try
            {
                CityDML dml = new CityDML();
                DataTable dtProvince = dml.GetProvince();
                if (dtProvince.Rows.Count > 0)
                {
                    FillDropDown(dtProvince, ddlProvince, "ProvinceID", "ProvinceName", "-Select Province-");
                }
                else
                {
                    ddlProvince.Items.Clear();
                    notification("Error", "No province found, please add province first.");
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting/populating provinces, due to: " + ex.Message);
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
                    FillDropDown(dtCity, ddlCity, "CityID", "CityName", "-Select City-");
                }
                else
                {
                    ddlCity.Items.Clear();
                    notification("Error", "No city found, please add city first.");
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting/populating cities, due to: " + ex.Message);
            }
        }

        public void ClearFields()
        {
            try
            {
                hfEditID.Value = string.Empty;
                txtCode.Text = string.Empty;
                txtName.Text = string.Empty;

                ddlProvince.ClearSelection();
                ddlCity.ClearSelection();
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
                    notification("Error", "Please enter region code");
                    txtCode.Focus();
                }
                else if (txtName.Text == string.Empty)
                {
                    notification("Error", "Please enter region name");
                    txtName.Focus();
                }
                else if (ddlProvince.SelectedIndex == 0)
                {
                    notification("Error", "Please select region province");
                    ddlProvince.Focus();
                }
                else if (ddlCity.SelectedIndex == 0)
                {
                    notification("Error", "Please select region city");
                    ddlCity.Focus();
                }
                else
                {
                    string Code = txtCode.Text.Trim();
                    string Name = txtName.Text.Trim();
                    string Description = txtDescription.Text.Trim();
                    Int64 ProvinceID = Convert.ToInt64(ddlProvince.SelectedValue);
                    Int64 CityID = Convert.ToInt64(ddlCity.SelectedValue);

                    RegionDML dml = new RegionDML();
                    if (hfEditID.Value == string.Empty)
                    {

                        DataTable dtRegion = dml.GetRegion(Code, Name);
                        if (dtRegion.Rows.Count > 0)
                        {
                            notification("Error", "Region with same code or name already exists in database.");
                        }
                        else
                        {
                            ConfirmModal("Are you sure want to save?", "Save");

                        }
                    }
                    else
                    {

                        DataTable dtRegion = dml.GetRegion(Code, Name);
                        if (dtRegion.Rows.Count > 1)
                        {
                            notification("Error", "Region with same code or name already exists in database.");
                        }
                        else
                        {
                            ConfirmModal("Are you sure want to Update?","Update");

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error saving region, due to: " + ex.Message);
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

            GetAndBindRegion();
            pnlInput.Visible = false;
        }

        protected void lnkCancelSearch_Click(object sender, EventArgs e)
        {
            try
            {
                GetAndBindRegion();
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
                Int64 RegionID = Convert.ToInt64(gvResult.DataKeys[index]["ID"]);
                hfEditID.Value = RegionID.ToString();

                RegionDML dml = new RegionDML();
                DataTable dtRegion = dml.GetRegion(RegionID);
                if (dtRegion.Rows.Count > 0)
                {

                    txtCode.Text = dtRegion.Rows[0]["Code"].ToString();
                    txtName.Text = dtRegion.Rows[0]["Name"].ToString();
                    txtDescription.Text = dtRegion.Rows[0]["Description"].ToString();

                    ddlProvince.ClearSelection();
                    ddlProvince.Items.FindByValue(dtRegion.Rows[0]["ProvinceID"].ToString()).Selected = true;

                    ddlCity.ClearSelection();
                    ddlCity.Items.FindByValue(dtRegion.Rows[0]["CityID"].ToString()).Selected = true;

               

                    lnkDelete.Visible = true;

                }
            }
            else if (e.CommandName == "View")
            {
                pnlView.Visible = true;
                pnlInput.Visible = false;
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow gvr = gvResult.Rows[index];
                Int64 RegionID = Convert.ToInt64(gvResult.DataKeys[index]["ID"]);
                hfEditID.Value = RegionID.ToString();

                RegionDML dml = new RegionDML();
                DataTable dtRegion = dml.GetRegion(RegionID);
                if (dtRegion.Rows.Count > 0)
                {

                    lblCode.Text = dtRegion.Rows[0]["Code"].ToString();
                    lblName.Text = dtRegion.Rows[0]["Name"].ToString();
                    lblDescription.Text = dtRegion.Rows[0]["Description"].ToString();

                    lblProvince.Text = dtRegion.Rows[0]["ProvinceName"].ToString();

                    lblCity.Text = dtRegion.Rows[0]["City"].ToString();

                }

            }
            else if (e.CommandName == "Active")
            {

                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow gvr = gvResult.Rows[index];
                Int64 CatId = Convert.ToInt64(gvResult.DataKeys[index]["ID"]);
                string Active = gvResult.DataKeys[index]["Active"].ToString() == string.Empty ? "False" : gvResult.DataKeys[index]["Active"].ToString();
                RegionDML dml = new RegionDML();

                hfEditID.Value = CatId.ToString();

                if (Active == "True")
                {
                    dml.DeactivateRegion(CatId, LoginID);
                }
                else
                {
                    dml.ActivateRegion(CatId, LoginID);
                }
                GetAndBindRegion();
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
                    bool Active = Convert.ToBoolean(rowView["Active"].ToString() == string.Empty ? "false" : rowView["Active"]);
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
                    notification("Error", "Please enter keyword to search any region");
                    GetAndBindRegion();
                }
                else
                {
                    string Keyword = txtSearch.Text.Trim();
                    RegionDML dml = new RegionDML();
                    DataTable dtRegion = dml.GetRegion(Keyword);
                    if (dtRegion.Rows.Count > 0)
                    {
                        gvResult.DataSource = dtRegion;
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
                notification("Error", "Error searching region, due to: " + ex.Message);
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
                        Int64 RegionID = Convert.ToInt64(hfEditID.Value);
                        RegionDML dml = new RegionDML();
                        dml.DeleteRegion(RegionID);

                        ClearFields();
                        GetAndBindRegion();
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
                        string Code = txtCode.Text.Trim();
                        string Name = txtName.Text.Trim();
                        string Description = txtDescription.Text.Trim();
                        Int64 ProvinceID = Convert.ToInt64(ddlProvince.SelectedValue);
                        Int64 CityID = Convert.ToInt64(ddlCity.SelectedValue);

                        RegionDML dml = new RegionDML();
                        Int64 RegionID = Convert.ToInt64(dml.InsertRegion(Code, Name, ProvinceID, CityID, Description, LoginID));
                        if (RegionID > 0)
                        {
                            notification("Success", "Region inserted successfully");
                            ClearFields();
                            GetAndBindRegion();
                            pnlInput.Visible = false;
                        }
                        else
                        {
                            notification("Error", "Region not inserted successfully, Trgy again...");
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
                        string Description = txtDescription.Text.Trim();
                        Int64 ProvinceID = Convert.ToInt64(ddlProvince.SelectedValue);
                        Int64 CityID = Convert.ToInt64(ddlCity.SelectedValue);

                        RegionDML dml = new RegionDML();
                        Int64 RegionID = Convert.ToInt64(hfEditID.Value);
                        dml.UpdateRegion(RegionID, Code, Name, ProvinceID, CityID, Description, LoginID);

                        notification("Success", "Region updated successfully");
                        ClearFields();
                        GetAndBindRegion();
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