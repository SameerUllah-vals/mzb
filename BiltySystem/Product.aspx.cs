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
    public partial class Product : System.Web.UI.Page
    {
        private static Random random = new Random();

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
                    GetProduct();

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

        public void GetProduct()
        {
            try
            {
                ProductDML dml = new ProductDML();
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
                txtDescription.Text = string.Empty;
                txtWeight.Text = string.Empty;
                txtName.Text = "";
                txtWidth.Text = "";
                txtHeight.Text = "";
                txtLength.Text = "";
                txtVolume.Text = "";
                ddlDimension.ClearSelection();
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

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        #endregion

        #region Events

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
                
                 if (txtName.Text == string.Empty)
                {
                    notification("Error", "Please Enter Product name");
                    txtName.Focus();
                }
                else if (ddlPackage.SelectedIndex == 0)
                {
                    notification("Error", "Please select Package type");
                    ddlPackage.Focus();
                }

 
                else if (ddlDimension.SelectedIndex == 0)
                {
                    notification("Error", "Please select dimension");
                    ddlDimension.Focus();
                }

                else
                {

                    string Code = RandomString(8);
                    string name = txtName.Text;




                    ProductDML pro = new ProductDML();
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


                ProductDML dml = new ProductDML();
                DataTable dtpro = dml.GetProduct(proID);
                if (dtpro.Rows.Count > 0)
                {
                   
                    txtDescription.Text = dtpro.Rows[0]["Description"].ToString();
                    txtWeight.Text = dtpro.Rows[0]["Weight"].ToString();
                    txtName.Text = dtpro.Rows[0]["Name"].ToString();
                    txtHeight.Text = dtpro.Rows[0]["Height"].ToString();
                    txtWidth.Text = dtpro.Rows[0]["Width"].ToString();
                    txtLength.Text = dtpro.Rows[0]["Length"].ToString();
                    txtVolume.Text = dtpro.Rows[0]["Volume"].ToString();

                    ddlUnit.ClearSelection();
                    if (dtpro.Rows[0]["Unit"].ToString() != string.Empty)
                    {
                        ddlUnit.Items.FindByValue(dtpro.Rows[0]["Unit"].ToString()).Selected = true;
                    }
                    

                    ddlPackage.ClearSelection();
                    if (dtpro.Rows[0]["PackageTypeID"].ToString() != string.Empty)
                    {
                        ddlPackage.Items.FindByValue(dtpro.Rows[0]["PackageTypeID"].ToString()).Selected = true;
                    }
                    //ddlPackage.Items.FindByValue(dtpro.Rows[0]["PackageTypeID"].ToString()).Selected = true;


                    ddlDimension.ClearSelection();

                    if (dtpro.Rows[0]["DimensionUnit"].ToString() != string.Empty)
                    {
                        ddlDimension.Items.FindByValue(dtpro.Rows[0]["DimensionUnit"].ToString()).Selected = true;
                    }
                    //ddlDimension.Items.FindByValue(dtpro.Rows[0]["DimensionUnit"].ToString()).Selected = true;

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



                ProductDML dml = new ProductDML();
                DataTable dtpro = dml.GetProduct(proID);
                if (dtpro.Rows.Count > 0)
                {
                    
                    lblNameModal.Text = dtpro.Rows[0]["Name"].ToString();
                    lblDesModal.Text = dtpro.Rows[0]["Description"].ToString();
                    lblPkg.Text = dtpro.Rows[0]["PackageTypeName"].ToString();
                    lblDimension.Text = dtpro.Rows[0]["DimensionUnit"].ToString();
                    lblWeight.Text = dtpro.Rows[0]["Weight"].ToString();
                    lblHeight.Text = dtpro.Rows[0]["Height"].ToString();
                    lblWidth.Text = dtpro.Rows[0]["Width"].ToString();
                    lblLength.Text = dtpro.Rows[0]["Length"].ToString();
                    lblVolume.Text = dtpro.Rows[0]["Volume"].ToString();
                    lblUnit.Text = dtpro.Rows[0]["Unit"].ToString();




                }
            }
            else if (e.CommandName == "Active")
            {

                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow gvr = gvResult.Rows[index];
                Int64 CatId = Convert.ToInt64(gvResult.DataKeys[index]["ID"]);
                string Active = gvResult.DataKeys[index]["Status"].ToString() == string.Empty ? "False" : gvResult.DataKeys[index]["Status"].ToString();
                ProductDML dml = new ProductDML();

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
            ProductDML dm = new ProductDML();
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
                        ProductDML dml = new ProductDML();
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
                        string Code = RandomString(8);
                        string name = txtName.Text;
                        Int64 package = Convert.ToInt64(ddlPackage.SelectedValue);
                        string unit = ddlUnit.SelectedItem.Text;
                        string Dimension = ddlDimension.SelectedItem.Text;
                        double weight = txtWeight.Text == string.Empty ? 0 : Convert.ToDouble(txtWeight.Text);
                        double Length = txtLength.Text == string.Empty ? 0 : Convert.ToDouble(txtLength.Text);
                        double Width = txtWidth.Text == string.Empty ? 0 : Convert.ToDouble(txtWidth.Text);
                        double Height = txtHeight.Text == string.Empty ? 0 : Convert.ToDouble(txtHeight.Text);
                        double volume = txtVolume.Text == string.Empty ? 0 : Convert.ToDouble(txtVolume.Text);
                        
                        string des = txtDescription.Text;
                        ProductDML pro = new ProductDML();
                        pro.InsertProduct(Code,name,package,Dimension,weight,Width,Height,Length,volume,unit,des,LoginID);
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
                         string Code = RandomString(8);
                        string name = txtName.Text;
                        Int64 package = Convert.ToInt64(ddlPackage.SelectedValue);
                        string unit = ddlUnit.SelectedItem.Text;
                        string Dimension = ddlDimension.SelectedItem.Text;
                        double weight = txtWeight.Text == string.Empty ? 0 : Convert.ToDouble(txtWeight.Text);
                        double Length = txtLength.Text == string.Empty ? 0 : Convert.ToDouble(txtLength.Text);
                        double Width = txtWidth.Text == string.Empty ? 0 : Convert.ToDouble(txtWidth.Text);
                        double Height = txtHeight.Text == string.Empty ? 0 : Convert.ToDouble(txtHeight.Text);
                        double volume = txtVolume.Text == string.Empty ? 0 : Convert.ToDouble(txtVolume.Text);
                        string des = txtDescription.Text;

                        ProductDML pro = new ProductDML();
                        Int64 id = Convert.ToInt64(hfEditID.Value);
                        pro.UpdateProduct(id,name,package,Dimension,weight,Width,Height,Length,volume,unit,des,LoginID);
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

        #endregion
    }
}