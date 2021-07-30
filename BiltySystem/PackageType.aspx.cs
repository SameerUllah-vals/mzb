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
    public partial class PackageType : System.Web.UI.Page
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
                    GetPackage();
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

        public void ClearFields()
        {
            try
            {
                hfEditID.Value = string.Empty;
                txtCode.Text = string.Empty;
                txtName.Text = string.Empty;
                txtlength.Text = string.Empty;

                txtwidth.Text = string.Empty;
                txtWeight.Text = string.Empty;
                txtheight.Text = string.Empty;
                ddlDimension.ClearSelection();

                txtDescription.Text = string.Empty;
             



            }
            catch (Exception ex)
            {
                notification("Error", "Error clearing fields, due to: " + ex.Message);
            }
        }

        public void GetPackage()
        {
            try
            {
                PackagingTypeDML dml = new PackagingTypeDML();
                DataTable dtpackage = dml.GetPackage();
                if (dtpackage.Rows.Count > 0)
                {
                    gvResult.DataSource = dtpackage;
                }
                else
                {
                    gvResult.DataSource = null;
                }
                gvResult.DataBind();
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting brokers, due to: " + ex.Message);
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


                    string Code = txtCode.Text;
                    string name = txtName.Text;
                    PackagingTypeDML pkg = new PackagingTypeDML();
                    DataTable dt = pkg.GetPackage(Code, name);

                    if (hfEditID.Value.ToString() == string.Empty)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            notification("Error", "Another Package Type with same name or code exists in database, try to change name or code");
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
                            notification("Error", "Another package type with same name or code exists in database, try to change name or code");
                        }
                        else
                        {
                            ConfirmModal("Are you sure want to Update?","Update");

                        }
                    }



                    GetPackage();
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

            GetPackage();
            pnlInput.Visible = false;
        }

        protected void lnkCancelSearch_Click(object sender, EventArgs e)
        {
            try
            {
                GetPackage();
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
                Int64 pkgID = Convert.ToInt64(gvResult.DataKeys[index]["PackageTypeID"]);
                hfEditID.Value = pkgID.ToString();


                PackagingTypeDML dml = new PackagingTypeDML();
                DataTable dtpkg = dml.GetPackage(pkgID);
                if (dtpkg.Rows.Count > 0)
                {
                    txtCode.Text = dtpkg.Rows[0]["PackageTypeCode"].ToString();
                    txtName.Text = dtpkg.Rows[0]["PackageTypeName"].ToString();
                    txtlength.Text = dtpkg.Rows[0]["Length"].ToString();
                    txtwidth.Text = dtpkg.Rows[0]["Width"].ToString();
                    txtheight.Text = dtpkg.Rows[0]["Height"].ToString();
                    txtWeight.Text = dtpkg.Rows[0]["Weight"].ToString();
                    txtDescription.Text = dtpkg.Rows[0]["Description"].ToString();

                    ddlDimension.ClearSelection();
                    ddlDimension.Items.FindByText(dtpkg.Rows[0]["DimensionUnit"].ToString()).Selected = true;

    

                    bool Master = Convert.ToBoolean(dtpkg.Rows[0]["IsMaster"]);
                    if (Master == true)
                    {
                        cbMasterCarton.Checked = true;
                    }
                    else
                    {
                        cbMasterCarton.Checked = false;
                    }

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
                Int64 pkgID = Convert.ToInt64(gvResult.DataKeys[index]["PackageTypeID"]);



                PackagingTypeDML dml = new PackagingTypeDML();
                DataTable dtpkg = dml.GetPackage(pkgID);

                lblCodemodal.Text = dtpkg.Rows[0]["PackageTypeCode"].ToString();
                lblNameModal.Text = dtpkg.Rows[0]["PackageTypeName"].ToString();
                lblLength.Text = dtpkg.Rows[0]["Length"].ToString();
                lblWidth.Text = dtpkg.Rows[0]["Width"].ToString();
                lblHeight.Text = dtpkg.Rows[0]["Height"].ToString();
                lblWeight.Text = dtpkg.Rows[0]["Weight"].ToString();
                lblDescription.Text = dtpkg.Rows[0]["Description"].ToString();
                lblDimension.Text = dtpkg.Rows[0]["DimensionUnit"].ToString();

                bool Master = Convert.ToBoolean(dtpkg.Rows[0]["IsMaster"]);
                if (Master == true)
                {
                    lblMasterCarton.Text = "yes";
                }
                else
                {
                    lblMasterCarton.Text = "No";
                }






            }
            else if (e.CommandName == "Active")
            {

                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow gvr = gvResult.Rows[index];
                Int64 CatId = Convert.ToInt64(gvResult.DataKeys[index]["PackageTypeID"]);
                string Active = gvResult.DataKeys[index]["isActive"].ToString() == string.Empty ? "False" : gvResult.DataKeys[index]["isActive"].ToString();
                PackagingTypeDML dml = new PackagingTypeDML();

                hfEditID.Value = CatId.ToString();

                if (Active == "True")
                {
                    dml.DeactivatePackage(CatId, LoginID);
                }
                else
                {
                    dml.ActivatePackage(CatId, LoginID);
                }
                GetPackage();
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
            string keyword = txtSearch.Text.Trim();
            PackagingTypeDML dm = new PackagingTypeDML();
            DataTable dtpkg = dm.GetPackage(keyword);
            if (dtpkg.Rows.Count > 0)
            {
                gvResult.DataSource = dtpkg;
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
                        Int64 id = Convert.ToInt64(hfEditID.Value);
                        PackagingTypeDML dml = new PackagingTypeDML();
                        dml.DeletePackage(id);
                        lnkDelete.Visible = false;
                        notification("Success", "Deleted Successfully");
                        GetPackage();
                        ClearFields();

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
                        double length = txtlength.Text == string.Empty ? 0 : Convert.ToDouble(txtlength.Text);
                        double width = txtwidth.Text == string.Empty ? 0 : Convert.ToDouble(txtwidth.Text);
                        double heigth = txtheight.Text == string.Empty ? 0 : Convert.ToDouble(txtheight.Text);
                        string Dimension = ddlDimension.SelectedItem.Text;
                        double Weight = txtWeight.Text == string.Empty ? 0 : Convert.ToDouble(txtWeight.Text);
     
                        int Master = cbMasterCarton.Checked == true ? 1 : 0;
                        string des = txtDescription.Text;


                        PackagingTypeDML pkg = new PackagingTypeDML();
                        pkg.InsertPackage(Code, name, length, width, heigth, Dimension, Weight, des, LoginID, Master);
                        pnlInput.Visible = false;
                        notification("Success", "Submited Successfully");
                        ClearFields();
                        GetPackage();

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
                        double length = txtlength.Text == string.Empty ? 0 : Convert.ToDouble(txtlength.Text);
                        double width = txtwidth.Text == string.Empty ? 0 : Convert.ToDouble(txtwidth.Text);
                        double heigth = txtheight.Text == string.Empty ? 0 : Convert.ToDouble(txtheight.Text);
                        string Dimension = ddlDimension.SelectedItem.Text;
                        double Weight = txtWeight.Text == string.Empty ? 0 : Convert.ToDouble(txtWeight.Text);
                    
                        int Master = cbMasterCarton.Checked == true ? 1 : 0;
                        string des = txtDescription.Text;



                        PackagingTypeDML pkg = new PackagingTypeDML();
                        Int64 id = Convert.ToInt64(hfEditID.Value);
                        pkg.UpdatePackage(id, Code, name, length, width, heigth, Dimension, Weight, Master, des, LoginID);
                        pnlInput.Visible = false;
                        notification("Success", "Updated Successfully");
                        ClearFields();
                        GetPackage();
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