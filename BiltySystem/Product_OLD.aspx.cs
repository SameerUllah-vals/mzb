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
    public partial class Product_OLD : System.Web.UI.Page
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



        protected void Page_Load(object sender, EventArgs e)
        {
            notification();
            if (!IsPostBack)
            {
                if (LoginID > 0)
                {
                    GetProduct();
                    GetGener();
                    GetPackage();
                    GetNature();
                    GetCategory();
                    GetItem();

                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }

        public void GetProduct()
        {
            try
            {
                ProductDML_OLD dml = new ProductDML_OLD();
                DataTable dtproduct = dml.GetProduct();
                if (dtproduct.Rows.Count > 0)
                {
                    gvResult.DataSource = dtproduct;
                }
                else
                {
                    gvResult.DataSource = null;
                }
                gvResult.DataBind();
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting Product, due to: " + ex.Message);
            }
        }



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



        public void GetItem()
        {
            try
            {
                ItemDML dml = new ItemDML();
                DataTable dtitem = dml.GetItem(); ;
                if (dtitem.Rows.Count > 0)
                {
                    FillDropDown(dtitem, ddlName, "ItemName", "ItemName", "-Select Item-");
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting/binding Item, due to: " + ex.Message);
            }
        }

        public void GetNature()
        {
            try
            {
                NatureDML dml = new NatureDML();
                DataTable dtnature = dml.Getnature(); ;
                if (dtnature.Rows.Count > 0)
                {
                    FillDropDown(dtnature, ddlnature, "Name", "Name", "-Select Nature-");
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting/binding Nature, due to: " + ex.Message);
            }
        }
        public void GetGener()
        {
            try
            {
                GenerDML dml = new GenerDML();
                DataTable dtgener = dml.Getgener();
                if (dtgener.Rows.Count > 0)
                {
                    FillDropDown(dtgener, ddlGener, "Name", "Name", "-Select Gener-");
                }
                else
                {
                    ddlGener.Items.Clear();
                    notification("Error", "No Gener found, please add gener first.");
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting/populating Gener, due to: " + ex.Message);
            }
        }

        public void GetCategory()
        {
            try
            {
                CategoryDML dml = new CategoryDML();
                DataTable dtcat = dml.GetCategory();
                if (dtcat.Rows.Count > 0)
                {
                    FillDropDown(dtcat, ddlCategory, "Name", "Name", "-Select Category-");
                }
                else
                {
                    ddlCategory.Items.Clear();
                    notification("Error", "No Category found, please add Category first.");
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting/populating Category, due to: " + ex.Message);
            }
        }

        public void GetPackage()
        {
            try
            {
                PackagingTypeDML dml = new PackagingTypeDML();
                DataTable dtpkg = dml.GetPackage();
                if (dtpkg.Rows.Count > 0)
                {
                    FillDropDown(dtpkg, ddlPackage, "PackageTypeID", "PackageTypeName", "-Select Package-");
                }
                else
                {
                    ddlPackage.Items.Clear();
                    notification("Error", "No package type found,");
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting/populating package, due to: " + ex.Message);
            }
        }

        public void ClearFields()
        {
            try
            {
                hfEditID.Value = string.Empty;
                txtCode.Text = string.Empty;
                txtDescription.Text = string.Empty;
                txtWeight.Text = string.Empty;

                ddlCategory.ClearSelection();
                ddlDimension.ClearSelection();
                ddlGener.ClearSelection();
                ddlName.ClearSelection();
                ddlnature.ClearSelection();
                ddlPackage.ClearSelection();
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
                    notification("Error", "Please enter Product code");
                    txtCode.Focus();
                }
                else if (ddlName.SelectedIndex == 0)
                {
                    notification("Error", "Please Select Product name");
                    ddlName.Focus();
                }
                else if (ddlPackage.SelectedIndex == 0)
                {
                    notification("Error", "Please select Package type");
                    ddlPackage.Focus();
                }
                else if (ddlCategory.SelectedIndex == 0)
                {
                    notification("Error", "Please select Product Category");
                    ddlCategory.Focus();
                }
                else if (ddlGener.SelectedIndex == 0)
                {
                    notification("Error", "Please select Product Gener");
                    ddlGener.Focus();
                }

                else if (ddlnature.SelectedIndex == 0)
                {
                    notification("Error", "Please select Nature");
                    ddlnature.Focus();
                }
                else if (ddlDimension.SelectedIndex == 0)
                {
                    notification("Error", "Please select dimension");
                    ddlnature.Focus();
                }

                else
                {

                    string Code = txtCode.Text;
                    string name = ddlName.SelectedValue;




                    ProductDML_OLD pro = new ProductDML_OLD();
                    DataTable dt = pro.GetProduct(Code, name);
                    if (hfEditID.Value.ToString() == string.Empty)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            notification("Error", "Another Product with same name or code exists in database, try to change name or code");
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
                            notification("Error", "Another Product with same name or code exists in database, try to change name or code");
                        }
                        else
                        {
                            ConfirmModal("Are you sure want to update?","Update");


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

            GetProduct();
            pnlInput.Visible = false;
        }

        protected void lnkCancelSearch_Click(object sender, EventArgs e)
        {
            try
            {
                GetProduct();
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
                Int64 proID = Convert.ToInt64(gvResult.DataKeys[index]["ID"]);
                hfEditID.Value = proID.ToString();


                ProductDML_OLD dml = new ProductDML_OLD();
                DataTable dtpro = dml.GetProduct(proID);
                if (dtpro.Rows.Count > 0)
                {
                    txtCode.Text = dtpro.Rows[0]["Code"].ToString();
                    txtDescription.Text = dtpro.Rows[0]["Description"].ToString();
                    txtWeight.Text = dtpro.Rows[0]["Weight"].ToString();


                    ddlName.ClearSelection();
                    ddlName.Items.FindByValue(dtpro.Rows[0]["Name"].ToString()).Selected = true;

                    ddlPackage.ClearSelection();
                    ddlPackage.Items.FindByValue(dtpro.Rows[0]["PackagingType"].ToString()).Selected = true;


                    ddlCategory.ClearSelection();
                    ddlCategory.Items.FindByValue(dtpro.Rows[0]["Category"].ToString()).Selected = true;

                    ddlGener.ClearSelection();
                    ddlGener.Items.FindByValue(dtpro.Rows[0]["Gener"].ToString()).Selected = true;

                    ddlnature.ClearSelection();
                    ddlnature.Items.FindByValue(dtpro.Rows[0]["Nature"].ToString()).Selected = true;

                    ddlDimension.ClearSelection();
                    ddlDimension.Items.FindByValue(dtpro.Rows[0]["DimensionUnit"].ToString()).Selected = true;

                    lnkDelete.Visible = true;
                    GetProduct();
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
                Int64 proID = Convert.ToInt64(gvResult.DataKeys[index]["ID"]);



                ProductDML_OLD dml = new ProductDML_OLD();
                DataTable dtpro = dml.GetProduct(proID);
                if (dtpro.Rows.Count > 0)
                {
                    lblCodemodal.Text = dtpro.Rows[0]["Code"].ToString();
                    lblNameModal.Text = dtpro.Rows[0]["Name"].ToString();
                    lblDesModal.Text = dtpro.Rows[0]["Description"].ToString();
                    lblPkg.Text = dtpro.Rows[0]["PackageType"].ToString();
                    lblCategory.Text = dtpro.Rows[0]["Category"].ToString();
                    lblGener.Text = dtpro.Rows[0]["Gener"].ToString();
                    lblNature.Text = dtpro.Rows[0]["Nature"].ToString();
                    lblDimension.Text = dtpro.Rows[0]["DimensionUnit"].ToString();
                    lblWeight.Text = dtpro.Rows[0]["Weight"].ToString();




                }
            }
            else if (e.CommandName == "Active")
            {

                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow gvr = gvResult.Rows[index];
                Int64 CatId = Convert.ToInt64(gvResult.DataKeys[index]["ID"]);
                string Active = gvResult.DataKeys[index]["Status"].ToString() == string.Empty ? "False" : gvResult.DataKeys[index]["Status"].ToString();
                ProductDML_OLD dml = new ProductDML_OLD();

                hfEditID.Value = CatId.ToString();

                if (Active == "True")
                {
                    dml.DeactivateProduct(CatId, LoginID);
                }
                else
                {
                    dml.ActivateProduct(CatId, LoginID);
                }
                GetProduct();
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
            ProductDML_OLD dm = new ProductDML_OLD();
            DataTable dtpro = dm.GetProduct(keyword);
            if (dtpro.Rows.Count > 0)
            {
                gvResult.DataSource = dtpro;
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
                        ProductDML_OLD dml = new ProductDML_OLD();
                        dml.DeleteProduct(id);
                        ClearFields();
                        lnkDelete.Visible = false;
                        notification("Success", "Deleting Successfully");
                        GetProduct();
                        pnlInput.Visible = false;
                        ClearFields();
                        GetProduct();
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
                        string name = ddlName.SelectedValue;
                        Int64 package = Convert.ToInt64(ddlPackage.SelectedValue);
                        string category = ddlCategory.SelectedValue;
                        string gener = ddlGener.SelectedValue;
                        string nature = ddlnature.SelectedValue; ;
                        string Dimension = ddlDimension.SelectedItem.Text;
                        double weight = txtWeight.Text == string.Empty ? 0 : Convert.ToDouble(txtWeight.Text);
                        string des = txtDescription.Text;


                        ProductDML_OLD pro = new ProductDML_OLD();
                        pro.InsertProduct(Code, name, package, category, gener, nature, Dimension, weight, des, LoginID);
                        pnlInput.Visible = false;
                        notification("Success", "Submited Successfully");
                        GetProduct();
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
                        string name = ddlName.SelectedValue;
                        Int64 package = Convert.ToInt64(ddlPackage.SelectedValue);
                        string category = ddlCategory.SelectedValue;
                        string gener = ddlGener.SelectedValue;
                        string nature = ddlnature.SelectedValue; ;
                        string Dimension = ddlDimension.SelectedItem.Text;
                        double weight = txtWeight.Text == string.Empty ? 0 : Convert.ToDouble(txtWeight.Text);
                        string des = txtDescription.Text;

                        ProductDML_OLD pro = new ProductDML_OLD();
                        Int64 id = Convert.ToInt64(hfEditID.Value);
                        pro.UpdateProduct(id, Code, name, package, category, gener, nature, Dimension, weight, des, LoginID);
                        pnlInput.Visible = false;
                        notification("Success", "Updated Successfully");
                        ClearFields();
                        GetProduct();
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