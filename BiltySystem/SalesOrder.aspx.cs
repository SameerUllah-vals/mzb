using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using BLL;

namespace BiltySystem
{
    public partial class SalesOrder : System.Web.UI.Page
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
        public void ConfirmModal(string Title, string Action)
        {
            try
            {
                lblModalTitle.Text = Title;
                hfConfirmAction.Value = Action;
                modalConfirm.Show();
            }
            catch (Exception ex)
            {
                notification("Error", "Error confirming, due to: " + ex.Message);
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //ModulesDML dml = new ModulesDML();
                //DataTable dtModules = dml.GetAllModules();
                //if (dtModules.Rows.Count > 0d)
                //{
                //    FillCheckBoxLIst(dtModules, cbModules, "ModuleID", "ModuleName");
                //}
                CompanyDML Companydml = new CompanyDML();
                DataTable dtcompany = Companydml.GetCompany();
                if(dtcompany.Rows.Count >0d)
                {
                    FillDropDown(dtcompany, ddlCustomerName, "CompanyID", "CompanyName", "-Select-");
                }
                DepartmentPersonDML departmentPersonDML = new DepartmentPersonDML();
                DataTable dtDepartmentPerson = departmentPersonDML.GetDepartmentPersonBySO();
                if(dtDepartmentPerson.Rows.Count > 0d)
                {
                    FillDropDown(dtDepartmentPerson, ddlSalesBy, "PersonalID", "Name", "-Select-");
                }
                ModulesDML modulesDML = new ModulesDML();
                DataTable dtModule = modulesDML.GetAllModules();
                if(dtModule.Rows.Count >0d)
                {
                    FillDropDown(dtModule, ddlModule, "ModuleID", "ModuleName", "-Select-");
                }
                GetAllSalesOrder();
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

        private void FillCheckBoxLIst(DataTable dt, CheckBoxList _cbl, string _ddlValue, string _ddlText)
        {
            if (dt.Rows.Count > 0)
            {
                _cbl.DataSource = dt;

                _cbl.DataValueField = _ddlValue;
                _cbl.DataTextField = _ddlText;

                _cbl.DataBind();
            }
        }

        #endregion
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
                if (txtProject.Text == string.Empty)
                {
                    notification("Error", "Please enter Project Name");
                    txtProject.Focus();
                }
                else if (txtProjectCost.Text == string.Empty)
                {
                    notification("Error", "Please enter Project Cost");
                    txtProjectCost.Focus();
                }
                else if (txtSalesPrice.Text == string.Empty)
                {
                    notification("Error", "Please enter Sales Price");
                    txtSalesPrice.Focus();
                }
                else
                {
                    ConfirmModal("Are you sure want to Save?", "Save");

                }
            }
            catch (Exception ex)
            {

                notification("Error", "Error submitting, due to: " + ex.Message);

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

        }

        protected void gvResult_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void lnkConfirm_Click(object sender, EventArgs e)
        {
            string Action = hfConfirmAction.Value;

            if (Action == "Save")
            {
                try
                {
                    Int64 CompanyID = Convert.ToInt64(ddlCustomerName.SelectedValue);
                    string ProjectName = txtProject.Text.Trim();
                    string SalesDate = txtSalesDate.Text.Trim();
                    Int64 ProjectCost = txtProjectCost.Text == string.Empty ? 0 : Convert.ToInt64(txtProjectCost.Text.Trim());
                    Int64 SalesPrice = txtSalesPrice.Text == string.Empty ? 0 : Convert.ToInt64(txtSalesPrice.Text.Trim());
                    Int64 ProjectDiscount = txtProjectDiscount.Text == string.Empty ? 0 : Convert.ToInt64(txtProjectDiscount.Text.Trim());
                    Int64 SalesByID = Convert.ToInt64(ddlSalesBy.SelectedValue);
                    //Int64 ModuleID = Convert.ToInt64(cbModules.SelectedValue);

                    Int64 ReferedBy = 0;
                    SalesOrderDML salesOrderDML = new SalesOrderDML();
                    Int64 SalesOrderID = salesOrderDML.InsertSalesOrder(CompanyID, ProjectName, SalesDate, ProjectCost, SalesPrice, ProjectDiscount, SalesByID, ReferedBy, LoginID);


                    if (SalesOrderID > 0)
                    {
                        if (gvModules.Rows.Count > 0)
                        {
                            foreach (GridViewRow _gvr in gvModules.Rows)
                            {
                                Int64 ModuleID = gvModules.DataKeys[_gvr.RowIndex].Value == string.Empty ? 0 : Convert.ToInt64(gvModules.DataKeys[_gvr.RowIndex].Value);
                                string ModuleName = _gvr.Cells[0].Text;
                                double Price = txtPrice.Text == string.Empty ? 0 : Convert.ToDouble(_gvr.Cells[1].Text);
                                double Discount = txtDiscount.Text == string.Empty ? 0 : Convert.ToDouble(_gvr.Cells[2].Text);
                                //dtModules.Rows.Add(ModuleID, ModuleName, Price, Discount);
                                Int64 SalesModuleID = salesOrderDML.InsertSalesModule(SalesOrderID, ModuleID, Discount, LoginID);

                            }
                        }
                    }
                    else
                    {

                    }
                        


                    ClearFields();
                    gvModules.DataSource = null;
                    gvModules.DataBind();
                    GetAllSalesOrder();
                    notification("Success", "Sales Order added successfully");



                }
                catch (Exception ex)
                {

                    notification("Error", ex.Message);
                }

            }
        }
        public void ClearModulesField()
        {
            ddlModule.ClearSelection(); 
            txtPrice.Text = string.Empty;
            txtDiscount.Text = string.Empty;
        }
        public void ClearFields()
        {
            try
            {
                hfEditID.Value = string.Empty;
                ddlCustomerName.ClearSelection();
                txtProject.Text = string.Empty;
                txtSalesDate.Text = string.Empty;
                txtProjectCost.Text = string.Empty;
                txtProjectDiscount.Text = string.Empty;
                txtSalesPrice.Text = string.Empty;
                ddlSalesBy.ClearSelection();
                //cbModules.Text = string.Empty;



                pnlInput.Visible = false;
            }
            catch (Exception ex)
            {
                notification("Error", "Error clearing fields, due to: " + ex.Message);
            }
        }
        public void GetAllSalesOrder()
        {
            try
            {
                SalesOrderDML salesOrderDML = new SalesOrderDML();
                DataTable dtSalesOrder = salesOrderDML.GetAllSalesOrder();
                

                if (dtSalesOrder.Rows.Count > 0)
                {
                    gvResult.DataSource = dtSalesOrder;
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

        protected void lnkCloseModule_Click(object sender, EventArgs e)
        {
            //pnlAddModule.Visible = false;
            ClearModulesField();
        }

        protected void lnkAddModules_Click(object sender, EventArgs e)
        {
            modalAddModule.Show();
        }

        protected void lnkSaveModule_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlModule.SelectedIndex == 0)
                {
                    notification("Error", "Please enter Module Name");
                    ddlModule.Focus();
                }
                else
                {

                    DataTable dtModules = new DataTable();
                    dtModules.Columns.Add("ModuleID");
                    dtModules.Columns.Add("ModuleName");
                    dtModules.Columns.Add("ModulePrice");
                    dtModules.Columns.Add("ModuleDiscount");

                    Int64 ModuleID = 0;
                    string ModuleName = string.Empty;
                    double Price = 0;
                    double Discount = 0;

                    if (gvModules.Rows.Count > 0)
                    {
                        foreach (GridViewRow _gvr in gvModules.Rows)
                        {
                            //;iew)gvModules.DataKeys[0].Value;
                            ModuleID = gvModules.DataKeys[_gvr.RowIndex].Value == string.Empty ? 0 : Convert.ToInt64(gvModules.DataKeys[_gvr.RowIndex].Value);
                            ModuleName = _gvr.Cells[0].Text;
                            Price = txtPrice.Text == string.Empty ? 0 : Convert.ToDouble(_gvr.Cells[1].Text);
                            Discount = txtDiscount.Text == string.Empty ? 0 : Convert.ToDouble(_gvr.Cells[2].Text);
                            dtModules.Rows.Add(ModuleID, ModuleName, Price, Discount);
                        }
                    }
                    ModuleID = Convert.ToInt64(ddlModule.SelectedItem.Value);
                    ModuleName = ddlModule.SelectedItem.Text;
                    Price = txtPrice.Text == string.Empty ? 0 : Convert.ToDouble(txtPrice.Text);
                    Discount = txtDiscount.Text == string.Empty ? 0 : Convert.ToDouble(txtDiscount.Text);
                    ModuleID = dtModules.Rows.Count <= 0 ? 1 : (gvModules.Rows.Count - 1);
                    dtModules.Rows.Add(ModuleID, ModuleName, Price, Discount);

                    gvModules.DataSource = dtModules.Rows.Count > 0 ? dtModules : null;
                    gvModules.DataBind();
                }
                ClearModulesField();
                ddlModule.Focus();
                //Int64 Price = txtPrice.Text == string.Empty ? 0 : Convert.ToInt64(txtPrice.Text.Trim());
                //Int64 Discount = txtDiscount.Text == string.Empty ? 0 : Convert.ToInt64(txtDiscount.Text.Trim());


            }
            catch (Exception ex)
            {

                throw;
            }
            finally {
                modalAddModule.Show();
            }
        }
    }
}