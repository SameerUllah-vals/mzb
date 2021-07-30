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
    public partial class Modules : System.Web.UI.Page
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
                if (LoginID != 0 && LoginID != null)
                {
                    this.Title = "Modules";
                    GetAllModule();
                   

                }
                else
                {

                }
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

        protected void lnkSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                //REQUIRE  FIELDS
                if (txtModuleName.Text == string.Empty)
                {
                    notification("Error", "Please enter Module Name");
                    txtModuleName.Focus();
                }
                else if (txtGeneralPrice.Text == string.Empty)
                {
                    notification("Error", "Please enter General Price");
                    txtGeneralPrice.Focus();
                }
                else if (txtMarketPrice.Text == string.Empty)
                {
                    notification("Error", "Please enter Market Price");
                    txtMarketPrice.Focus();
                }

                else
                {
                    string ModuleName = txtModuleName.Text.Trim();
                    Int64 GenernalPrice = txtGeneralPrice.Text == string.Empty ? 0 : Convert.ToInt64(txtGeneralPrice.Text.Trim());
                    Int64 MarketPrice = txtMarketPrice.Text == string.Empty ? 0 : Convert.ToInt64(txtMarketPrice.Text.Trim());
                    Int64 ModuleDiscount = txtModuleDiscount.Text == string.Empty ? 0 : Convert.ToInt64(txtModuleDiscount.Text);

                    //CHECKING SAME CODE OR SAME NAME
                    ModulesDML modulesDML = new ModulesDML();
                    if (hfEditID.Value == string.Empty)
                    {
                        DataTable dtDuplicate = modulesDML.GetModule(ModuleName);
                        if (dtDuplicate.Rows.Count > 0)
                        {
                            notification("Error", "This is already Module Name");
                            txtModuleName.Focus();
                            //string DupModuleName = dtDuplicate.Rows[0]["ModuleName"].ToString();
                            //Int64 DupGeneralPrice = Convert.ToInt64(dtDuplicate.Rows[0]["GeneralPrice"]);
                            //Int64 DupMarketPrice = Convert.ToInt64(dtDuplicate.Rows[0]["MarketPrice"]);
                            //Int64 DupModuleDiscount = Convert.ToInt64(dtDuplicate.Rows[0]["ModuleDiscount"]);


                            //if (ModuleName == DupModuleName)
                            //{
                            //    notification("Error", "This is already Module Name");
                            //    txtModuleName.Focus();
                            //}
                            //else if (GenernalPrice == DupGeneralPrice)
                            //{
                            //    notification("Error", "Another broker already ");
                            //    txtGeneralPrice.Focus();
                            //}
                            //else if (MarketPrice == DupMarketPrice)
                            //{
                            //    notification("Error", "Another broker already ");
                            //    txtMarketPrice.Focus();
                            //}
                            //else if (ModuleDiscount == DupModuleDiscount)
                            //{
                            //    notification("Error", "Another broker already");
                            //    txtModuleDiscount.Focus();
                            //}

                        }
                        else
                        {
                            //OPENING MODEL
                            ConfirmModal("Are you sure want to Save?", "Save");

                        }
                    }
                    else
                    {
                       
                            DataTable dtDuplicate = modulesDML.GetModule(ModuleName);
                            if (dtDuplicate.Rows.Count > 1)
                            {
                                notification("Error", "This is already Module Name");
                                txtModuleName.Focus();
                                //UPDATING 
                                //Int64 ModuleID = Convert.ToInt64(hfEditID.Value);

                                ////CHEKING DUPLICATE 
                                //DataTable dtDuplicate = modulesDML.GetModule(ModuleName);

                                //if (dtDuplicate.Rows.Count > 1)
                                //{
                                //    notification("Error", "This code is already Module Name");
                                //    txtModuleName.Focus();

                                //string DupModuleName = dtDuplicate.Rows[0]["ModuleName"].ToString();
                                //Int64 DupGeneralPrice = Convert.ToInt64(dtDuplicate.Rows[0]["GeneralPrice"]);
                                //Int64 DupMarketPrice = Convert.ToInt64(dtDuplicate.Rows[0]["MarketPrice"]);
                                //Int64 DupModuleDiscount = Convert.ToInt64(dtDuplicate.Rows[0]["ModuleDiscount"]);

                                //if (ModuleName == DupModuleName)
                                //{
                                //    notification("Error", "This code is already Module Name");
                                //    txtModuleName.Focus();
                                //}

                            }
                            else
                            {
                                //OPENING MODAL
                                ConfirmModal("Are you sure want to Update?", "Update");

                            }
                        
                    }

                }
            }

            catch (Exception ex)
            {
                notification("Error", "Error submitting, due to: " + ex.Message);
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
                
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow gvr = gvResult.Rows[index];
                Int64 ModuleID = Convert.ToInt64(gvResult.DataKeys[index]["ModuleID"]);
                hfEditID.Value = ModuleID.ToString();

                //BrokersDML dmlBrokers = new BrokersDML();
                //DataTable dtBrokers = dmlBrokers.GetBroker(BrokerID);
                ModulesDML modulesDML = new ModulesDML();
                DataTable dtmodule = modulesDML.GetModule(ModuleID);
                if (dtmodule.Rows.Count > 0)
                {
                    txtModuleName.Text = dtmodule.Rows[0]["ModuleName"].ToString();
                    txtGeneralPrice.Text = dtmodule.Rows[0]["GeneralPrice"].ToString();
                    txtMarketPrice.Text = dtmodule.Rows[0]["MarketPrice"].ToString();
                    txtModuleDiscount.Text = dtmodule.Rows[0]["ModuleDiscount"].ToString();
                 

                    pnlInput.Visible = true;
                    
                }
                else
                {
                    notification("Error", "No Module found");
                }

            }
            else if (e.CommandName == "Erase")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow gvr = gvResult.Rows[index];
                Int64 ModuleID = Convert.ToInt64(gvResult.DataKeys[index]["ModuleID"]);
                hfEditID.Value = ModuleID.ToString();
                ConfirmModal("Are you sure you want to Delete?", "Delete");
                //ConfirmModal("Are you sure want to Delete?", "Delete");

            }
            else if (e.CommandName == "Active")
            {

                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow gvr = gvResult.Rows[index];
                Int64 ModuleID = Convert.ToInt64(gvResult.DataKeys[index]["ModuleID"]);
                string Active = gvResult.DataKeys[index]["IsActive"].ToString() == string.Empty ? "False" : gvResult.DataKeys[index]["IsActive"].ToString();
                // CityDML dml = new CityDML();
                ModulesDML modulesDML = new ModulesDML();

                //hfEditID.Value = CityID.ToString();

                if (Active == "True")
                {
                    modulesDML.DeactivateModule(ModuleID, LoginID);
                }
                else
                {
                    modulesDML.ActivateModule(ModuleID, LoginID);
                }
                GetAllModule();

            }
        }

        protected void gvResult_DataBinding(object sender, EventArgs e)
        {

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

        protected void lnkConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                string Action = hfConfirmAction.Value;
                if(Action== "Delete")
                {
                    try
                    {
                        Int64 ModuleID = Convert.ToInt64(hfEditID.Value);
                        ModulesDML modulesDML = new ModulesDML();
                        modulesDML.DeleteModule(ModuleID);
                        ClearFields();
                        GetAllModule();
                        notification("Success","Module Deleted Successfully");
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
                        string ModuleName = txtModuleName.Text.Trim();
                        Int64 GenernalPrice = txtGeneralPrice.Text == string.Empty ? 0 : Convert.ToInt64(txtGeneralPrice.Text.Trim());
                        Int64 MarketPrice = txtMarketPrice.Text == string.Empty ? 0 : Convert.ToInt64(txtMarketPrice.Text.Trim());
                        Int64 ModuleDiscount = txtModuleDiscount.Text == string.Empty ? 0 : Convert.ToInt64(txtModuleDiscount.Text.Trim());


                        ModulesDML modulesDML = new ModulesDML();
                        Int64 ModuleID = Convert.ToInt64(modulesDML.InsertModule(ModuleName,GenernalPrice,MarketPrice,ModuleDiscount, LoginID));
                        if (ModuleID > 0)
                        {


                            ClearFields();
                            GetAllModule();
                            notification("Success", "Module added successfully");
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

                        string ModuleName = txtModuleName.Text.Trim();
                        Int64 GenernalPrice = txtGeneralPrice.Text == string.Empty ? 0 : Convert.ToInt64(txtGeneralPrice.Text.Trim());
                        Int64 MarketPrice = txtMarketPrice.Text == string.Empty ? 0 : Convert.ToInt64(txtMarketPrice.Text.Trim());
                        Int64 ModuleDiscount = txtModuleDiscount.Text == string.Empty ? 0 : Convert.ToInt64(txtModuleDiscount.Text.Trim());
                        ModulesDML modulesDML = new ModulesDML();
                        Int64 ModuleID = Convert.ToInt64(hfEditID.Value);

                        //dmlBrokers.Update(BrokerID, Code, Name, Phone, Phone2, HomeNo, Address, NIC, Description, LoginID);
                        modulesDML.UpdateModule(ModuleID, ModuleName,GenernalPrice, MarketPrice, ModuleDiscount, LoginID);
                        ClearFields();
                        GetAllModule();
                        notification("Success", "Module Updated successfully");
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
        public void ClearFields()
        {
            try
            {
                hfEditID.Value = string.Empty;
                txtModuleName.Text = string.Empty;
                txtGeneralPrice.Text = string.Empty;
                txtMarketPrice.Text = string.Empty;
                txtModuleDiscount.Text = string.Empty;
               

                pnlInput.Visible = false;
            }
            catch (Exception ex)
            {
                notification("Error", "Error clearing fields, due to: " + ex.Message);
            }
        }
        public void GetAllModule()
        {
            try
            {
                ModulesDML modulesDML = new ModulesDML();
                DataTable dtmodule = modulesDML.GetAllModules();
                
                if (dtmodule.Rows.Count > 0)
                {
                    gvResult.DataSource = dtmodule;
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

       

        protected void gvResult_RowDataBound(object sender, GridViewRowEventArgs e)
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
                else if(e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView rowView = (DataRowView)e.Row.DataItem;
                    bool Delete = Convert.ToBoolean(rowView["IsDeleted"].ToString() == string.Empty ? "flase" : rowView["IsDeleted"]);
                    LinkButton lnkDelete = e.Row.FindControl("lnkDelete") as LinkButton;
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Binding data to grid, due to: " + ex.Message);
            }
        }
    }
}