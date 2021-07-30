using BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiltySystem.Bilty
{
    public partial class ManualBilty_OLD : System.Web.UI.Page
    {
        
        Random rnd = new Random();

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
            ClearAllNotifications();
            try
            {
                txtPartyCommission.Text = CalculatePartyCommission().ToString();
            }
            catch (Exception ex)
            {
                BiltyFreightNotification("Error", "Error calculating party commission, due to: " + ex.Message);
            }
            if (!IsPostBack)
            {
                //Getting/Populating Clearing agents
                try
                {
                    ClearingAgentDML dml = new ClearingAgentDML();
                    DataTable dtAgents = dml.GetClearingAgents();
                    if (dtAgents.Rows.Count > 0)
                    {
                        FillDropDown(dtAgents, ddlClearingAgent, "ID", "Name", "-Select Agent-");
                    }
                }
                catch (Exception ex)
                {
                    notification("Error", "Error getting/populating clearing agents, due to: " + ex.Message);
                }

                //Getting/Populating Container Types
                try
                {
                    ContainersDML dml = new ContainersDML();
                    DataTable dtContainerTypes = dml.GetContainerType();
                    if (dtContainerTypes.Rows.Count > 0)
                    {
                        FillDropDown(dtContainerTypes, ddlContainerType, "ContainerTypeID", "ContainerType", "-Select Type-");
                    }
                }
                catch (Exception ex)
                {
                    notification("Error", "Error getting/populating container types, due to: " + ex.Message);
                }

                //Getting/Populating Vehicle Types
                try
                {
                    VehicleTypeDML dml = new VehicleTypeDML();
                    DataTable dtVehicleTypes = dml.GetVehicleType();
                    if (dtVehicleTypes.Rows.Count > 0)
                    {
                        FillDropDown(dtVehicleTypes, ddlVehicleType, "VehicleTypeID", "VehicleTypeName", "-Select Type-");
                    }
                }
                catch (Exception ex)
                {
                    notification("Error", "Error getting/populating vehicle types, due to: " + ex.Message);
                }

                //Getting/Populating Document Types
                try
                {
                    DocumentTypeDML dml = new DocumentTypeDML();
                    DataTable dtDocumentType = dml.GetDocumentType();
                    if (dtDocumentType.Rows.Count > 0)
                    {
                        //FillDropDown(dtDocumentType, ddlDocumentType, "DocumentTypeID", "Name", "-Select Type-");
                        FillDropDown(dtDocumentType, ddlReceivingDocumentType, "DocumentTypeID", "Name", "-Select Type-");
                    }
                }
                catch (Exception ex)
                {
                    notification("Error", "Error getting/populating Document types, due to: " + ex.Message);
                }

                //Getting/Populating Damaged Items
                try
                {
                    ItemDML dml = new ItemDML();
                    DataTable dtItem = dml.GetItem();
                    if (dtItem.Rows.Count > 0)
                    {
                        FillDropDown(dtItem, ddlDamageItem, "ID", "Product", "- Select-");
                    }
                }
                catch (Exception ex)
                {
                    notification("Error", "Error getting/populating Damage Items, due to: " + ex.Message);
                }

                //Getting/Populating Damage Types
                try
                {
                    DamageTypeDML dml = new DamageTypeDML();
                    DataTable dtDamagetype = dml.GetDamageType();
                    if (dtDamagetype.Rows.Count > 0)
                    {
                        FillDropDown(dtDamagetype, ddlDamageType, "DamageTypeID", "Name", "- Select-");
                    }
                }
                catch (Exception ex)
                {
                    notification("Error", "Error getting/populating Damage Type, due to: " + ex.Message);
                }
            }
        }


        #endregion

        #region Custom Methods

        #region Notifications

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

        public void VehicleInfoModalNotification()
        {
            try
            {
                divVehicleInfoModalNotification.InnerHtml = string.Empty;
            }
            catch (Exception ex)
            {
                divVehicleInfoModalNotification.InnerHtml = "<div class=\"alert alert-danger\">" + ex.Message + "</div>";
            }
        }

        public void VehicleInfoModalNotification(string type, string msg)
        {
            try
            {
                if (type == "Error")
                {
                    divVehicleInfoModalNotification.InnerHtml = "<div class=\"alert alert-danger\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                }
                else if (type == "Success")
                {
                    divVehicleInfoModalNotification.InnerHtml = "<div class=\"alert alert-success\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                }
                else if (type == string.Empty)
                {
                    divVehicleInfoModalNotification.InnerHtml = string.Empty;
                }
            }
            catch (Exception ex)
            {
                divVehicleInfoModalNotification.InnerHtml = "<div class=\"alert alert-danger\">" + ex.Message + "</div>";
            }
        }

        public void CustomerInfoNotification()
        {
            try
            {
                divCustomerInfoNotification.InnerHtml = string.Empty;
            }
            catch (Exception ex)
            {
                divCustomerInfoNotification.InnerHtml = "<div class=\"alert alert-danger\">" + ex.Message + "</div>";
            }
        }

        public void CustomerInfoNotification(string type, string msg)
        {
            try
            {
                if (type == "Error")
                {
                    divCustomerInfoNotification.InnerHtml = "<div class=\"alert alert-danger\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                }
                else if (type == "Success")
                {
                    divCustomerInfoNotification.InnerHtml = "<div class=\"alert alert-success\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                }
                else if (type == string.Empty)
                {
                    divCustomerInfoNotification.InnerHtml = string.Empty;
                }
            }
            catch (Exception ex)
            {
                divCustomerInfoNotification.InnerHtml = "<div class=\"alert alert-danger\">" + ex.Message + "</div>";
            }
        }

        public void ShippingInfoNotification()
        {
            try
            {
                divShippingInfoNotification.InnerHtml = string.Empty;
            }
            catch (Exception ex)
            {
                divShippingInfoNotification.InnerHtml = "<div class=\"alert alert-danger\">" + ex.Message + "</div>";
            }
        }

        public void ShippingInfoNotification(string type, string msg)
        {
            try
            {
                if (type == "Error")
                {
                    divShippingInfoNotification.InnerHtml = "<div class=\"alert alert-danger\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                }
                else if (type == "Success")
                {
                    divShippingInfoNotification.InnerHtml = "<div class=\"alert alert-success\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                }
                else if (type == string.Empty)
                {
                    divShippingInfoNotification.InnerHtml = string.Empty;
                }
            }
            catch (Exception ex)
            {
                divShippingInfoNotification.InnerHtml = "<div class=\"alert alert-danger\">" + ex.Message + "</div>";
            }
        }

        public void ReceivingInfoNotification()
        {
            try
            {
                divReceivingInfoNotification.InnerHtml = string.Empty;
            }
            catch (Exception ex)
            {
                divReceivingInfoNotification.InnerHtml = "<div class=\"alert alert-danger\">" + ex.Message + "</div>";
            }
        }

        public void ReceivingInfoNotification(string type, string msg)
        {
            try
            {
                if (type == "Error")
                {
                    divReceivingInfoNotification.InnerHtml = "<div class=\"alert alert-danger\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                }
                else if (type == "Success")
                {
                    divReceivingInfoNotification.InnerHtml = "<div class=\"alert alert-success\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                }
                else if (type == string.Empty)
                {
                    divReceivingInfoNotification.InnerHtml = string.Empty;
                }
            }
            catch (Exception ex)
            {
                divReceivingInfoNotification.InnerHtml = "<div class=\"alert alert-danger\">" + ex.Message + "</div>";
            }
        }

        public void ReceivingDocInfoNotification()
        {
            try
            {
                divReceivingDocInfoNotification.InnerHtml = string.Empty;
            }
            catch (Exception ex)
            {
                divReceivingDocInfoNotification.InnerHtml = "<div class=\"alert alert-danger\">" + ex.Message + "</div>";
            }
        }

        public void ReceivingDocInfoNotification(string type, string msg)
        {
            try
            {
                if (type == "Error")
                {
                    divReceivingDocInfoNotification.InnerHtml = "<div class=\"alert alert-danger\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                }
                else if (type == "Success")
                {
                    divReceivingDocInfoNotification.InnerHtml = "<div class=\"alert alert-success\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                }
                else if (type == string.Empty)
                {
                    divReceivingDocInfoNotification.InnerHtml = string.Empty;
                }
            }
            catch (Exception ex)
            {
                divReceivingDocInfoNotification.InnerHtml = "<div class=\"alert alert-danger\">" + ex.Message + "</div>";
            }
        }

        public void DamageInfoNotification()
        {
            try
            {
                divDamageInfoNotification.InnerHtml = string.Empty;
            }
            catch (Exception ex)
            {
                divDamageInfoNotification.InnerHtml = "<div class=\"alert alert-danger\">" + ex.Message + "</div>";
            }
        }

        public void DamageInfoNotification(string type, string msg)
        {
            try
            {
                if (type == "Error")
                {
                    divDamageInfoNotification.InnerHtml = "<div class=\"alert alert-danger\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                }
                else if (type == "Success")
                {
                    divDamageInfoNotification.InnerHtml = "<div class=\"alert alert-success\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                }
                else if (type == string.Empty)
                {
                    divDamageInfoNotification.InnerHtml = string.Empty;
                }
            }
            catch (Exception ex)
            {
                divDamageInfoNotification.InnerHtml = "<div class=\"alert alert-danger\">" + ex.Message + "</div>";
            }
        }

        public void ConsignmentInfoNotification()
        {
            try
            {
                divConsignmentInfoNotification.InnerHtml = string.Empty;
            }
            catch (Exception ex)
            {
                divConsignmentInfoNotification.InnerHtml = "<div class=\"alert alert-danger\">" + ex.Message + "</div>";
            }
        }

        public void ConsignmentInfoNotification(string type, string msg)
        {
            try
            {
                if (type == "Error")
                {
                    divConsignmentInfoNotification.InnerHtml = "<div class=\"alert alert-danger\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                }
                else if (type == "Success")
                {
                    divConsignmentInfoNotification.InnerHtml = "<div class=\"alert alert-success\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                }
                else if (type == string.Empty)
                {
                    divConsignmentInfoNotification.InnerHtml = string.Empty;
                }
            }
            catch (Exception ex)
            {
                divConsignmentInfoNotification.InnerHtml = "<div class=\"alert alert-danger\">" + ex.Message + "</div>";
            }
        }

        public void ConsignmentInfoModalNotification()
        {
            try
            {
                divConsignmentInfoModalNotification.InnerHtml = string.Empty;
            }
            catch (Exception ex)
            {
                divConsignmentInfoModalNotification.InnerHtml = "<div class=\"alert alert-danger\">" + ex.Message + "</div>";
            }
        }

        public void ConsignmentInfoModalNotification(string type, string msg)
        {
            try
            {
                if (type == "Error")
                {
                    divConsignmentInfoModalNotification.InnerHtml = "<div class=\"alert alert-danger\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                }
                else if (type == "Success")
                {
                    divConsignmentInfoModalNotification.InnerHtml = "<div class=\"alert alert-success\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                }
                else if (type == string.Empty)
                {
                    divConsignmentInfoModalNotification.InnerHtml = string.Empty;
                }
            }
            catch (Exception ex)
            {
                divConsignmentInfoModalNotification.InnerHtml = "<div class=\"alert alert-danger\">" + ex.Message + "</div>";
            }
        }

        public void VehicleInfoNotification()
        {
            try
            {
                divVehicleInfoNotification.InnerHtml = string.Empty;
            }
            catch (Exception ex)
            {
                divVehicleInfoNotification.InnerHtml = "<div class=\"alert alert-danger\">" + ex.Message + "</div>";
            }
        }

        public void VehicleInfoNotification(string type, string msg)
        {
            try
            {
                if (type == "Error")
                {
                    divVehicleInfoNotification.InnerHtml = "<div class=\"alert alert-danger\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                }
                else if (type == "Success")
                {
                    divVehicleInfoNotification.InnerHtml = "<div class=\"alert alert-success\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                }
                else if (type == string.Empty)
                {
                    divVehicleInfoNotification.InnerHtml = string.Empty;
                }
            }
            catch (Exception ex)
            {
                divVehicleInfoNotification.InnerHtml = "<div class=\"alert alert-danger\">" + ex.Message + "</div>";
            }
        }

        public void ProductInfoNotification()
        {
            try
            {
                divProductNotification.InnerHtml = string.Empty;
            }
            catch (Exception ex)
            {
                divProductNotification.InnerHtml = "<div class=\"alert alert-danger\">" + ex.Message + "</div>";
            }
        }

        public void ProductInfoNotification(string type, string msg)
        {
            try
            {
                if (type == "Error")
                {
                    divProductNotification.InnerHtml = "<div class=\"alert alert-danger\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                }
                else if (type == "Success")
                {
                    divProductNotification.InnerHtml = "<div class=\"alert alert-success\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                }
                else if (type == string.Empty)
                {
                    divProductNotification.InnerHtml = string.Empty;
                }
            }
            catch (Exception ex)
            {
                divProductNotification.InnerHtml = "<div class=\"alert alert-danger\">" + ex.Message + "</div>";
            }
        }

        public void BiltyFreightNotification()
        {
            try
            {
                divBiltyFreightNotification.InnerHtml = string.Empty;
            }
            catch (Exception ex)
            {
                divBiltyFreightNotification.InnerHtml = "<div class=\"alert alert-danger\">" + ex.Message + "</div>";
            }
        }

        public void BiltyFreightNotification(string type, string msg)
        {
            try
            {
                if (type == "Error")
                {
                    divBiltyFreightNotification.InnerHtml = "<div class=\"alert alert-danger\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                    divFreightInfo.Style.Add("display", "block");
                    toggleIconFreightInfo.Attributes.Add("class", "box_toggle fa fa-chevron-down");
                }
                else if (type == "Success")
                {
                    divBiltyFreightNotification.InnerHtml = "<div class=\"alert alert-success\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                }
                else if (type == string.Empty)
                {
                    divBiltyFreightNotification.InnerHtml = string.Empty;
                }
            }
            catch (Exception ex)
            {
                divBiltyFreightNotification.InnerHtml = "<div class=\"alert alert-danger\">" + ex.Message + "</div>";
            }
        }

        public void AdvanceInfoNotification()
        {
            try
            {
                divAdvanceInfoNotification.InnerHtml = string.Empty;
            }
            catch (Exception ex)
            {
                divAdvanceInfoNotification.InnerHtml = "<div class=\"alert alert-danger\">" + ex.Message + "</div>";
            }
        }

        public void AdvanceInfoNotification(string type, string msg)
        {
            try
            {
                if (type == "Error")
                {
                    divAdvanceInfoNotification.InnerHtml = "<div class=\"alert alert-danger\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                }
                else if (type == "Success")
                {
                    divAdvanceInfoNotification.InnerHtml = "<div class=\"alert alert-success\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                }
                else if (type == string.Empty)
                {
                    divAdvanceInfoNotification.InnerHtml = string.Empty;
                }
            }
            catch (Exception ex)
            {
                divAdvanceInfoNotification.InnerHtml = "<div class=\"alert alert-danger\">" + ex.Message + "</div>";
            }
        }

        #endregion

        public void ClearAllNotifications()
        {
            try
            {
                notification();
                CustomerInfoNotification();
                ShippingInfoNotification();
                ReceivingInfoNotification();
                ReceivingDocInfoNotification();
                VehicleInfoNotification();
                ConsignmentInfoNotification();
                ConsignmentInfoModalNotification();
                DamageInfoNotification();
                ProductInfoNotification();
                BiltyFreightNotification();
                AdvanceInfoNotification();
                VehicleInfoModalNotification();
            }
            catch (Exception ex)
            {
                notification("Error", "Error clearing all notification, due to: " + ex.Message);
            }
        }

        public double CalculatePartyCommission()
        {
            try
            {
                double BiltyFreight = txtBiltyFreight.Text.Trim() == string.Empty ? 0 : Convert.ToDouble(txtBiltyFreight.Text.Trim());
                double Freight = txtFreight.Text.Trim() == string.Empty ? 0 : Convert.ToDouble(txtFreight.Text.Trim());

                double PartyCommission = BiltyFreight - Freight;
                return PartyCommission;
            }
            catch (Exception ex)
            {
                BiltyFreightNotification("Error", "Error calculating Party commission, due to: " + ex.Message);
                return 0;
            }
        }

        public double CalculateTotalAdvance()
        {
            try
            {
                double AdvanceFreight = txtAdvanceFreight.Text.Trim() == string.Empty ? 0 : Convert.ToDouble(txtAdvanceFreight.Text.Trim());
                double FactoryAdvance = txtFactoryAdvance.Text.Trim() == string.Empty ? 0 : Convert.ToDouble(txtFactoryAdvance.Text.Trim());
                double DieselAdvance = txtDieselAdvance.Text.Trim() == string.Empty ? 0 : Convert.ToDouble(txtDieselAdvance.Text.Trim());
                double VehicleAdvance = txtVehicleAdvanceAmount.Text.Trim() == string.Empty ? 0 : Convert.ToDouble(txtVehicleAdvanceAmount.Text.Trim());

                double TotalAdvance = AdvanceFreight + FactoryAdvance + DieselAdvance + VehicleAdvance;
                return TotalAdvance;
            }
            catch (Exception ex)
            {
                BiltyFreightNotification("Error", "Error calculating Total Advance, due to: " + ex.Message);
                return 0;
            }
        }

        public double CalculateBalanceFreight()
        {
            try
            {
                double Freight = txtFreight.Text == string.Empty ? 0 : Convert.ToDouble(txtFreight.Text);
                double TotalAdvance = txtTotalAdvance.Text == string.Empty ? 0 : Convert.ToDouble(txtTotalAdvance.Text);
                double BalanceFreight = TotalAdvance - Freight;
                return BalanceFreight;
            }
            catch (Exception ex)
            {
                AdvanceInfoNotification("Error", "Error calculating balance freight, due to: " + ex.Message);
                return 0;
            }
        }

        public void ClearFields()
        {
            try
            {
                txtBiltyNo.Text = string.Empty;
                txtBiltyDate.Text = string.Empty;

                txtSearchSender.Text = string.Empty;
                txtSenderCompanyCode.Text = string.Empty;
                txtSenderGroup.Text = string.Empty;
                txtSenderCompany.Text = string.Empty;
                txtSenderDepartment.Text = string.Empty;

                txtSearchReceiver.Text = string.Empty;
                txtReceiverCompanyCode.Text = string.Empty;
                txtReceiverGroup.Text = string.Empty;
                txtReceiverCompany.Text = string.Empty;
                txtReceiverDepartment.Text = string.Empty;

                txtSearchCustomer.Text = string.Empty;
                txtCustomerCode.Text = string.Empty;
                txtCustomerGroup.Text = string.Empty;
                txtCustomerCompany.Text = string.Empty;
                txtCustomerDepartment.Text = string.Empty;

                ddlShippingType.ClearSelection();
                txtLoadingDate.Text = string.Empty;

                txtSearchPickLocation.Text = string.Empty;
                txtPickCity.Text = string.Empty;
                txtPickRegion.Text = string.Empty;
                txtPickArea.Text = string.Empty;
                txtPickAddress.Text = string.Empty;
                txtSearchDropLocation.Text = string.Empty;
                txtDropCity.Text = string.Empty;
                txtDropRegion.Text = string.Empty;
                txtDropArea.Text = string.Empty;
                txtDropAddress.Text = string.Empty;

                ddlVehicleType.ClearSelection();
                txtVehicleQuantity.Text = string.Empty;

                ddlClearingAgent.ClearSelection();
                ddlContainerType.ClearSelection();
                txtContainerQty.Text = string.Empty;
                txtTotalGrossWeight.Text = string.Empty;

                txtSearchProduct.Text = string.Empty;
                txtPackageType.Text = string.Empty;
                txtItem.Text = string.Empty;
                txtProductQty.Text = string.Empty;
                txtTotalProductWeight.Text = string.Empty;

                txtReceivedBy.Text = string.Empty;
                txtReceivingDate.Text = string.Empty;
                txtReceivingTime.Text = string.Empty;

                ddlReceivingDocumentType.ClearSelection();
                txtReceivingDocumentNo.Text = string.Empty;

                ddlDamageItem.ClearSelection();
                ddlDamageType.ClearSelection();
                txtDamageCost.Text = string.Empty;
                txtDamageCause.Text = string.Empty;

                txtBiltyFreight.Text = string.Empty;
                txtFreight.Text = string.Empty;
                txtPartyCommission.Text = string.Empty;

                txtAdvanceFreight.Text = string.Empty;
                txtFactoryAdvance.Text = string.Empty;
                txtDieselAdvance.Text = string.Empty;
                txtVehicleAdvanceAmount.Text = string.Empty;
                txtTotalAdvance.Text = "0";

                txtActualWeight.Text = string.Empty;
                txtAdditionalWeight.Text = string.Empty;
                txtBalanceFreight.Text = "0";

                txtVehicleBiltyAdvance.Text = string.Empty;
                txtVehilceFreightAdvance.Text = string.Empty;
                txtVehicleAdvance.Text = string.Empty;
                txtBalanceCommission.Text = string.Empty;

                DataTable dt = new DataTable();
                gvProducts.DataSource = dt;
                gvProducts.DataBind();

                gvVehicleInfo.DataSource = dt;
                gvVehicleInfo.DataBind();

                gvVehicleType.DataSource = dt;
                gvVehicleType.DataBind();

                gvContainerInfo.DataSource = dt;
                gvContainerInfo.DataBind();

                gvConsignmentInfo.DataSource = dt;
                gvConsignmentInfo.DataBind();

                gvReceiving.DataSource = dt;
                gvReceiving.DataBind();

                gvVehicleTimings.DataSource = dt;
                gvVehicleTimings.DataBind();

                gvReceivingDocument.DataSource = dt;
                gvReceivingDocument.DataBind();

                gvDamage.DataSource = dt;
                gvDamage.DataBind();

                Session.Clear();
                Session.Abandon();
            }
            catch (Exception ex)
            {
                notification("Error", "Error clearing fields, due to: " + ex.Message);
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

        #region Script & Web Methods

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchCustomers(string prefixText, int count)
        {

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT CONCAT(CONCAT(p.Code + ' | ' + p.Name + ' | ' + pt.PackageTypeName, ' | '), p.Weight) AS Product FROM Product p INNER JOIN PackageType pt ON pt.PackageTypeID = p.PackageTypeID WHERE p.Code LIKE '%' + @SearchText + '%' OR p.Name LIKE '%' + @SearchText + '%' OR pt.PackageTypeName LIKE '%' + @SearchText + '%'";
                    //cmd.CommandText = "SELECT c.CompanyCode + ' | ' + c.CompanyName + ' | ' + g.GroupName + ' | ' + d.DepartName AS Company FROM Company c LEFT JOIN Groups g ON g.GroupID = c.GroupID LEFT JOIN Department d ON d.COMPANYID = c.CompanyID WHERE c.CompanyName LIKE  '%' + @SearchText + '%' OR d.DepartName LIKE '%' + @SearchText + '%' OR g.GroupName LIKE '%' + @SearchText + '%' OR c.CompanyCode LIKE '%' + @SearchText + '%' OR g.GroupCode LIKE '%' + @SearchText + '%' OR d.DepartCode LIKE '%' + @SearchText + '%'";
                    cmd.Parameters.AddWithValue("@SearchText", prefixText);
                    cmd.Connection = conn;
                    conn.Open();
                    List<string> customers = new List<string>();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            customers.Add(sdr["Product"].ToString());
                        }
                    }
                    conn.Close();
                    return customers;
                }
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchCompanies(string prefixText, int count)
        {
            List<string> customers = new List<string>();
            OrderDML dmlOrder = new OrderDML();
            DataTable dtCompanies = dmlOrder.GetCompanies(prefixText);
            if (dtCompanies.Rows.Count > 0)
            {
                for (int i = 0; i < dtCompanies.Rows.Count; i++)
                {
                    customers.Add(dtCompanies.Rows[i]["Company"].ToString());
                }
            }
            return customers;
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchLocations(string prefixText, int count)
        {

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    //cmd.CommandText = "select ContactName from Customers where " + "ContactName like @SearchText + '%'";
                    //cmd.CommandText = "SELECT c.CompanyCode + ' | ' + c.CompanyName + ' | ' + g.GroupName + ' | ' + d.DepartName AS Company FROM Company c LEFT JOIN Groups g ON g.GroupID = c.GroupID LEFT JOIN Department d ON d.COMPANYID = c.CompanyID WHERE c.CompanyName LIKE  '%' + @SearchText + '%' OR d.DepartName LIKE '%' + @SearchText + '%' OR g.GroupName LIKE '%' + @SearchText + '%' OR c.CompanyCode LIKE '%' + @SearchText + '%' OR g.GroupCode LIKE '%' + @SearchText + '%' OR d.DepartCode LIKE '%' + @SearchText + '%'";
                    cmd.CommandText = "SELECT c.CityName + ' | ' + r.Name + ' | ' + a.AreaName + ' | ' + pdl.Address + ' | ' + pdl.PickDropCode as Location, pdl.*, p.ProvinceName, c.CityName, 	r.Name RegionName,a.AreaName, l.LocationTypeName FROM PickDropLocation pdl INNER JOIN Area a ON a.ID = pdl.AreaID INNER JOIN Region r on r.ID = a.Region INNER JOIN City c on c.CityID = r.CityID INNER JOIN Province p on p.ProvinceID = c.ProvinceID INNER JOIN LocationType l ON l.LocationTypeID = pdl.LocationTypeID WHERE pdl.isActive = 'True' AND (pdl.PickDropCode LIKE '%' + @SearchText + '%' OR pdl.PickDropLocationName LIKE '%' + @SearchText + '%' OR p.ProvinceName LIKE '%' + @SearchText + '%' OR c.CityName LIKE '%' + @SearchText + '%' OR r.Name LIKE '%' + @SearchText + '%' OR a.AreaName LIKE '%' + @SearchText + '%' OR pdl.Address LIKE '%' + @SearchText + '%' OR pdl.Description LIKE '%' + @SearchText + '%' ) ORDER BY pdl.PickDropLocationName ASC";
                    cmd.Parameters.AddWithValue("@SearchText", prefixText);
                    cmd.Connection = conn;
                    conn.Open();
                    List<string> customers = new List<string>();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            customers.Add(sdr["Location"].ToString());
                        }
                    }
                    conn.Close();
                    return customers;
                }
            }
        }

        #endregion

        #region Events

        #region LinkButton Click

        protected void lnkAddContainerType_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlContainerType.SelectedIndex == 0)
                {
                    ConsignmentInfoNotification("Error", "Please select container type");
                    ddlContainerType.Focus();
                }
                else if (txtContainerQty.Text == string.Empty)
                {
                    ConsignmentInfoNotification("Error", "Please enter container qty");
                    txtContainerQty.Focus();
                }
                else
                {
                    Int64 ContainerTypeID = Convert.ToInt64(ddlContainerType.SelectedItem.Value);
                    string ContainerType = ddlContainerType.SelectedItem.Text;
                    string Qty = txtContainerQty.Text;
                    string Weight = txtTotalGrossWeight.Text;

                    DataTable dt = new DataTable();
                    dt.Columns.Add("ContainerTypeID");
                    dt.Columns.Add("ContainerType");
                    dt.Columns.Add("ContainerQty");
                    dt.Columns.Add("TotalWeight");

                    bool AddNew = true;
                    if (gvConsignmentInfo.Rows.Count > 0)
                    {
                        for (int i = 0; i < gvConsignmentInfo.Rows.Count; i++)
                        {
                            if (gvConsignmentInfo.Rows[i].Cells[0].Text == ddlContainerType.SelectedItem.Text)
                            {
                                //Int64 ContainerTypeID = Convert.ToInt64(gvConsignmentInfo.DataKeys[i].Values[0]);
                                Int64 OldQty = Convert.ToInt64(gvConsignmentInfo.Rows[i].Cells[1].Text);
                                double OldWeight = Convert.ToDouble(gvConsignmentInfo.Rows[i].Cells[2].Text);

                                Int64 NewQty = Convert.ToInt64(txtContainerQty.Text);
                                double NewWeight = Convert.ToDouble(txtTotalGrossWeight.Text);

                                NewQty += OldQty;
                                NewWeight += OldWeight;

                                dt.Rows.Add(gvConsignmentInfo.DataKeys[i].Values[0].ToString(), gvConsignmentInfo.Rows[i].Cells[0].Text, NewQty, NewWeight);
                                AddNew = false;
                            }
                            else
                            {
                                dt.Rows.Add(gvConsignmentInfo.DataKeys[i].Values[0].ToString(), gvConsignmentInfo.Rows[i].Cells[0].Text, gvConsignmentInfo.Rows[i].Cells[1].Text, gvConsignmentInfo.Rows[i].Cells[2].Text);
                            }

                        }
                    }

                    if (AddNew == true)
                    {
                        dt.Rows.Add(ContainerTypeID, ContainerType, Qty, Weight);
                    }

                    gvConsignmentInfo.DataSource = dt;
                    gvConsignmentInfo.DataBind();

                    ddlContainerType.ClearSelection();
                    txtContainerQty.Text = string.Empty;
                    txtTotalGrossWeight.Text = string.Empty;

                    ConsignmentInfoNotification("Success", "Consignment added successfully");
                }
            }
            catch (Exception ex)
            {
                ConsignmentInfoNotification("Error", "Error add container type, due to: " + ex.Message);
            }
        }

        protected void lnkAddVehicleType_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlVehicleType.SelectedIndex == 0)
                {
                    VehicleInfoNotification("Error", "Please select vehicle type");
                    ddlVehicleType.Focus();
                }
                else if (txtVehicleQuantity.Text == string.Empty)
                {
                    VehicleInfoNotification("Error", "Please enter vehicle quantity");
                    txtVehicleQuantity.Focus();
                }
                else
                {
                    string VehicleType = ddlVehicleType.SelectedItem.Text;
                    Int64 VehicleQuantity = Convert.ToInt64(txtVehicleQuantity.Text);

                    DataTable dt = new DataTable();
                    dt.Columns.Add("VehicleType");
                    dt.Columns.Add("VehicleQty");

                    bool AddNew = true;
                    if (gvVehicleType.Rows.Count > 0)
                    {
                        for (int i = 0; i < gvVehicleType.Rows.Count; i++)
                        {
                            if (gvVehicleType.Rows[i].Cells[0].Text == ddlVehicleType.SelectedItem.Text)
                            {
                                Int64 OldQty = Convert.ToInt64(gvVehicleType.Rows[i].Cells[1].Text);
                                Int64 NewQty = Convert.ToInt64(txtVehicleQuantity.Text);

                                NewQty += OldQty;

                                dt.Rows.Add(gvVehicleType.Rows[i].Cells[0].Text, NewQty);
                                AddNew = false;
                            }
                            else
                            {
                                dt.Rows.Add(gvVehicleType.Rows[i].Cells[0].Text, gvVehicleType.Rows[i].Cells[1].Text);
                            }
                        }
                    }

                    if (AddNew == true)
                    {
                        dt.Rows.Add(VehicleType, VehicleQuantity);
                    }

                    gvVehicleType.DataSource = dt;
                    gvVehicleType.DataBind();

                    ddlVehicleType.ClearSelection();
                    txtVehicleQuantity.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                VehicleInfoNotification("Error", "Error adding vehicle type, due to: " + ex.Message);
            }
        }

        protected void lnkAddProduct_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPackageType.Text == string.Empty)
                {
                    ProductInfoNotification("Error", "Please enter package type");
                    txtPackageType.Focus();
                }
                else if (txtItem.Text == string.Empty)
                {
                    ProductInfoNotification("Error", "Please enter Item");
                    txtItem.Focus();
                }
                else if (txtProductQty.Text.Trim() == string.Empty || txtProductQty.Text.Trim() == "0")
                {
                    ProductInfoNotification("Error", "Please enter product quantity");
                    txtProductQty.Focus();
                }
                else
                {
                    string PackageType = txtPackageType.Text;
                    string Item = txtItem.Text;
                    Int64 Quantity = Convert.ToInt64(txtProductQty.Text);
                    double TotalWeight = txtTotalProductWeight.Text.Trim() == string.Empty ? 0 : Convert.ToDouble(txtTotalProductWeight.Text);

                    DataTable dt = new DataTable();
                    dt.Columns.Add("PackageType");
                    dt.Columns.Add("Item");
                    dt.Columns.Add("Qty");
                    dt.Columns.Add("TotalWeight");

                    if (gvProducts.Rows.Count > 0)
                    {
                        for (int i = 0; i < gvProducts.Rows.Count; i++)
                        {
                            dt.Rows.Add(gvProducts.Rows[i].Cells[0].Text, gvProducts.Rows[i].Cells[1].Text, gvProducts.Rows[i].Cells[2].Text, gvProducts.Rows[i].Cells[3].Text);
                        }
                    }

                    dt.Rows.Add(PackageType, Item, Quantity, TotalWeight);

                    txtPackageType.Text = string.Empty;
                    txtItem.Text = string.Empty;
                    txtProductQty.Text = string.Empty;
                    txtTotalProductWeight.Text = string.Empty;
                    txtSearchProduct.Text = string.Empty;

                    gvProducts.DataSource = dt;
                    gvProducts.DataBind();

                    ProductInfoNotification("Success", "Product Added");
                }
            }
            catch (Exception ex)
            {
                ProductInfoNotification("Error", "Error adding product, due to: " + ex.Message);
            }
        }

        //protected void lnkAddDispatchDocument_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (ddlDocumentType.SelectedIndex == 0)
        //        {
        //            notification("Error", "Please select Document Type");
        //            ddlDocumentType.Focus();
        //        }
        //        else if (txtDocumentNo.Text == string.Empty)
        //        {
        //            notification("Error", "Please enter Document No.");
        //            txtDocumentNo.Focus();
        //        }
        //        else
        //        {
        //            string DocumentType = ddlDocumentType.SelectedItem.Text;
        //            string DocumentNo = txtDocumentNo.Text;

        //            DataTable dt = new DataTable();
        //            dt.Columns.Add("DocumentType");
        //            dt.Columns.Add("DocumentNo.");
        //            dt.Columns.Add("DocumentName");

        //            if (gvDispatchDocument.Rows.Count > 0)
        //            {
        //                for (int i = 0; i < gvDispatchDocument.Rows.Count; i++)
        //                {
        //                    dt.Rows.Add(gvDispatchDocument.Rows[i].Cells[0].Text, gvDispatchDocument.Rows[i].Cells[1].Text, gvDispatchDocument.Rows[i].Cells[2].Text);
        //                }
        //            }

        //            if (fuDispatchDoc.HasFile)
        //            {

        //                string sessionName = "DynamicSession" + gvDispatchDocument.Rows.Count;
        //                Session[sessionName] = fuDispatchDoc;
        //                FileUpload fuDoc = Session[sessionName] as FileUpload;
        //                dt.Rows.Add(DocumentType, DocumentNo, fuDoc.FileName);
        //                gvDispatchDocument.DataSource = dt;
        //                gvDispatchDocument.DataBind();

        //                //if (System.Web.HttpContext.Current.Session[sessionName] != null)
        //                //{

        //                //}
        //                //else
        //                //{

        //                //}
        //            }


        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notification("Error", "Error add Document, due to: " + ex.Message);
        //    }
        //}

        protected void lnkAddReceiving_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtReceivedBy.Text == string.Empty)
                {
                    notification("Error", "Please enter Receivied by name");
                    txtReceivedBy.Focus();
                }
                else if (txtReceivingDate.Text == string.Empty)
                {
                    notification("Error", "Please select receiving date");
                    txtReceivingDate.Focus();
                }
                else if (txtReceivingTime.Text == string.Empty)
                {
                    notification("Error", "Please select time");
                    txtReceivingTime.Focus();
                }
                else
                {
                    string ReceivedBy = txtReceivedBy.Text;
                    string ReceivingDate = txtReceivingDate.Text;
                    string ReceivingTime = txtReceivingTime.Text;

                    DateTime FullDateTime = Convert.ToDateTime(ReceivingDate + " " + ReceivingTime);
                    DataTable dt = new DataTable();
                    dt.Columns.Add("ReceivedBy");
                    dt.Columns.Add("ReceivedDateTime");

                    if (gvReceiving.Rows.Count > 0)
                    {
                        for (int i = 0; i < gvReceiving.Rows.Count; i++)
                        {
                            dt.Rows.Add(gvReceiving.Rows[i].Cells[0].ToString(), gvReceiving.Rows[i].Cells[1].ToString() + " " + gvReceiving.Rows[i].Cells[2].ToString());
                        }
                    }

                    dt.Rows.Add(ReceivedBy, FullDateTime);

                    gvReceiving.DataSource = dt;
                    gvReceiving.DataBind();

                    txtReceivedBy.Text = string.Empty;
                    txtReceivingDate.Text = string.Empty;
                    txtReceivingTime.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error adding receiving, due to: " + ex.Message);
            }
        }

        protected void lnkAddReceivingDocument_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlReceivingDocumentType.SelectedIndex == 0)
                {
                    notification("Error", "Please select document type");
                    ddlReceivingDocumentType.Focus();
                }
                else if (txtReceivingDocumentNo.Text == string.Empty)
                {
                    notification("Error", "Please select receiving date");
                    txtReceivingDocumentNo.Focus();
                }
                else
                {
                    string DocumentType = ddlReceivingDocumentType.SelectedItem.Text;
                    string DocumentNo = txtReceivingDocumentNo.Text;

                    DataTable dt = new DataTable();
                    dt.Columns.Add("DocumentType");
                    dt.Columns.Add("DocumentNo");
                    dt.Columns.Add("DocumentName");
                    if (gvReceivingDocument.Rows.Count > 0)
                    {
                        for (int i = 0; i < gvReceivingDocument.Rows.Count; i++)
                        {
                            dt.Rows.Add(gvReceivingDocument.Rows[i].Cells[0].Text, gvReceivingDocument.Rows[i].Cells[1].Text, gvReceivingDocument.Rows[i].Cells[2].Text);
                        }
                    }

                    if (fuReceivingDoc.HasFile)
                    {

                        string sessionName = "ReceivingDocument" + gvReceivingDocument.Rows.Count;
                        Session[sessionName] = fuReceivingDoc;
                        FileUpload fuDoc = Session[sessionName] as FileUpload;
                        dt.Rows.Add(DocumentType, DocumentNo, fuDoc.FileName);
                        gvReceivingDocument.DataSource = dt;
                        gvReceivingDocument.DataBind();

                        ddlReceivingDocumentType.ClearSelection();
                        txtReceivingDocumentNo.Text = string.Empty;
                    }

                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error receiving document, due to: " + ex.Message);
            }
        }

        protected void lnkAddDamage_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlDamageItem.SelectedIndex == 0)
                {
                    notification("Error", "Please select Damaged Item");
                    ddlDamageItem.Focus();
                }
                else if (ddlDamageType.SelectedIndex == 0)
                {
                    notification("Error", "Please select damage type");
                    ddlDamageType.Focus();
                }
                else
                {
                    if (txtDamageCost.Text == string.Empty)
                    {
                        notification("Error", "Please enter damage cost, if no cost applied then enter 0");
                        txtDamageCost.Focus();
                    }
                    else
                    {
                        string DamageItem = ddlDamageItem.SelectedItem.Text;
                        string DamageType = ddlDamageType.SelectedItem.Text;
                        string DamageCost = txtDamageCost.Text.Trim();
                        string DamageCause = txtDamageCause.Text.Trim();

                        DataTable dt = new DataTable();
                        dt.Columns.Add("Item");
                        dt.Columns.Add("DamageType");
                        dt.Columns.Add("Cost");
                        dt.Columns.Add("Cause");
                        dt.Columns.Add("Document");

                        if (gvDamage.Rows.Count > 0)
                        {
                            for (int i = 0; i < gvDamage.Rows.Count; i++)
                            {
                                dt.Rows.Add(gvDamage.Rows[i].Cells[0].Text, gvDamage.Rows[i].Cells[1].Text, gvDamage.Rows[i].Cells[2].Text, gvDamage.Rows[i].Cells[3].Text, gvDamage.Rows[i].Cells[4].Text);
                            }
                        }

                        if (fuDamageDoc.HasFile)
                        {
                            string SessionName = "DamageDoc" + gvDamage.Rows.Count;
                            Session[SessionName] = fuDamageDoc;
                            FileUpload fuDoc = Session[SessionName] as FileUpload;
                            dt.Rows.Add(DamageItem, DamageType, DamageCost, DamageCause, fuDoc.FileName);

                            gvDamage.DataSource = dt;
                            gvDamage.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error adding damage, due to: " + ex.Message);
            }
        }

        protected void lnkDamageCostSharing_Click(object sender, EventArgs e)
        {

        }

        protected void lnkContainerInfo_Click(object sender, EventArgs e)
        {
            try
            {
                //if (gvVehicleInfo.Rows.Count > 0)
                //{
                //    bool isAnyEmpty = false;
                //    foreach (GridViewRow gvr in gvVehicleInfo.Rows)
                //    {
                //        TextBox txtVehicleRegNo = gvr.Cells[2].FindControl("txtVehicleNo") as TextBox;
                //        if (txtVehicleRegNo.Text == string.Empty)
                //        {
                //            isAnyEmpty = true;
                //        }
                //    }

                //    if (isAnyEmpty == true)
                //    {
                //        modalVehicleInfo.Show();
                //        VehicleInfoModalNotification("Error", "Please provide all vehicle registration numbers first");
                //    }
                //    else
                //    {
                //        DataTable dt = new DataTable();
                //        dt.Columns.Add("ContainerType");
                //        dt.Columns.Add("ContainerNo");
                //        dt.Columns.Add("Weight");
                //        dt.Columns.Add("EmptyContainerPickUpLocation");
                //        dt.Columns.Add("EmptyContainerDropoffLocation");
                //        dt.Columns.Add("VesselName");
                //        dt.Columns.Add("Remarks");
                //        for (int i = 0; i < gvConsignmentInfo.Rows.Count; i++)
                //        {
                //            string ContainerType = gvConsignmentInfo.Rows[i].Cells[0].Text;
                //            Int64 Quantity = Convert.ToInt64(gvConsignmentInfo.Rows[i].Cells[1].Text);
                //            if (Quantity > 1)
                //            {
                //                for (int j = 0; j < Quantity; j++)
                //                {
                //                    dt.Rows.Add(ContainerType);
                //                }
                //            }
                //        }

                //        gvContainerInfo.DataSource = dt;
                //        gvContainerInfo.DataBind();
                //        modalContainerInfo.Show();
                //    }
                //}
                //else
                //{
                //    ConsignmentInfoNotification("Error", "No Vehicle info found, please add vehicle info first");
                //}
                DataTable dt = new DataTable();
                dt.Columns.Add("ContainerTypeID");
                dt.Columns.Add("ContainerType");
                dt.Columns.Add("ContainerNo");
                dt.Columns.Add("Weight");
                dt.Columns.Add("EmptyContainerPickUpLocation");
                dt.Columns.Add("EmptyContainerDropoffLocation");
                dt.Columns.Add("VesselName");
                dt.Columns.Add("Remarks");
                for (int i = 0; i < gvConsignmentInfo.Rows.Count; i++)
                {
                    Int64 ContainerTypeID = Convert.ToInt64(gvConsignmentInfo.DataKeys[i].Values[0]);
                    string ContainerType = gvConsignmentInfo.Rows[i].Cells[0].Text;
                    Int64 Quantity = Convert.ToInt64(gvConsignmentInfo.Rows[i].Cells[1].Text);
                    if (Quantity > 0)
                    {
                        for (int j = 0; j < Quantity; j++)
                        {
                            dt.Rows.Add(ContainerTypeID, ContainerType);
                        }
                    }
                }

                gvContainerInfo.DataSource = dt;
                gvContainerInfo.DataBind();
                modalContainerInfo.Show();
            }
            catch (Exception ex)
            {
                ConsignmentInfoNotification("Error", "Error opening container info panel, due to: " + ex.Message);
            }
        }

        protected void lnkCancelContainerInfo_Click(object sender, EventArgs e)
        {
            try
            {
                modalContainerInfo.Hide();
            }
            catch (Exception ex)
            {
                notification("Error", "Error cancelling container info, due to: " + ex.Message);
            }
        }

        protected void lnkSaveContainerInfo_Click(object sender, EventArgs e)
        {
            try
            {
                modalContainerInfo.Hide();
            }
            catch (Exception ex)
            {
                notification("Error", "Error saving container info, due to: " + ex.Message);
            }
        }

        protected void lnkAddVehicleInfo_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvVehicleType.Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("VehicleType");
                    for (int i = 0; i < gvVehicleType.Rows.Count; i++)
                    {
                        string VehicleType = gvVehicleType.Rows[i].Cells[0].Text;
                        Int64 Qty = Convert.ToInt64(gvVehicleType.Rows[i].Cells[1].Text);

                        for (int j = 0; j < Qty; j++)
                        {
                            dt.Rows.Add(VehicleType);
                        }
                    }

                    gvVehicleInfo.DataSource = dt;
                    gvVehicleInfo.DataBind();
                    modalVehicleInfo.Show();
                }
                else
                {
                    VehicleInfoNotification("Error", "Please add Vehicle types first");
                }
            }
            catch (Exception ex)
            {
                VehicleInfoNotification("Error", "Error adding vehicle info, due to: " + ex.Message);
            }
        }

        protected void lnkSaveVehicleInfo_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvVehicleInfo.Rows.Count > 0)
                {
                    int RequiredIndex = 0;
                    for (int i = 0; i < gvVehicleInfo.Rows.Count; i++)
                    {
                        TextBox txtVehicleRegNo = gvVehicleInfo.Rows[i].Cells[2].FindControl("txtVehicleNo") as TextBox;
                        if (txtVehicleRegNo.Text.Trim() == string.Empty)
                        {
                            RequiredIndex = i + 1;
                        }
                    }

                    if (RequiredIndex > 0)
                    {
                        VehicleInfoModalNotification("Error", "Please enter provide Registration no of vehicle on index " + RequiredIndex);
                        gvVehicleInfo.Rows[RequiredIndex - 1].BackColor = Color.Pink;
                        TextBox txtVehicleRegNo = gvVehicleInfo.Rows[RequiredIndex - 1].Cells[2].FindControl("txtVehicleNo") as TextBox;
                        txtVehicleRegNo.Focus();

                        modalVehicleInfo.Show();
                    }
                    else
                    {
                        foreach (GridViewRow gvr in gvVehicleInfo.Rows)
                        {
                            gvr.BackColor = Color.White;

                        }
                        modalVehicleInfo.Hide();
                    }
                }
                else
                {
                    VehicleInfoNotification("Error", "No vehicle info found, please add some vehicle info");
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error closing vehicle info popup, due to: " + ex.Message);
            }
        }

        protected void lnkCancelSaveVehicleInfo_Click(object sender, EventArgs e)
        {
            try
            {
                modalVehicleInfo.Hide();
            }
            catch (Exception ex)
            {
                notification("Error", "Error closing vehicle info popup, due to: " + ex.Message);
            }
        }

        protected void lnkViewVehicleInfo_Click(object sender, EventArgs e)
        {
            try
            {
                modalVehicleInfo.Show();
            }
            catch (Exception ex)
            {
                notification("Error", "Error viewing Vehicle info, due to: " + ex.Message);
            }
        }

        protected void lnkAddVehicleTiming_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvVehicleInfo.Rows.Count > 0)
                {
                    DataTable dtVehicles = new DataTable();
                    dtVehicles.Columns.Add("VehicleType");
                    dtVehicles.Columns.Add("VehicleRegNo");
                    bool isAnyEmpty = false;
                    for (int i = 0; i < gvVehicleInfo.Rows.Count; i++)
                    {
                        Label lblVehicleType = gvVehicleInfo.Rows[i].Cells[1].FindControl("lblVehicleType") as Label;
                        TextBox txtVehicleRegNo = gvVehicleInfo.Rows[i].Cells[2].FindControl("txtVehicleNo") as TextBox;

                        if (txtVehicleRegNo.Text.Trim() == string.Empty)
                        {
                            isAnyEmpty = true;
                        }
                        else
                        {
                            dtVehicles.Rows.Add(lblVehicleType.Text, txtVehicleRegNo.Text);
                        }
                    }
                    if (isAnyEmpty == true)
                    {
                        VehicleInfoModalNotification("Error", "Please provide all vehicle Registration no first");
                        modalVehicleInfo.Show();
                    }
                    else
                    {
                        gvVehicleTimings.DataSource = dtVehicles;
                        gvVehicleTimings.DataBind();

                        modalVehicleTiming.Show();
                    }
                }
                else
                {
                    ReceivingInfoNotification("Error", "No Vehice Info found, Please add Vehicle Info first");
                }
            }
            catch (Exception ex)
            {
                ReceivingInfoNotification("Error", "Error enabling vehicle timing, due to: " + ex.Message);
            }
        }

        protected void lnkCancelSaveVehicleTiming_Click(object sender, EventArgs e)
        {

        }

        protected void lnkSaveVehicleTiming_Click(object sender, EventArgs e)
        {

        }

        protected void lnkSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSenderCompany.Text.Trim() == string.Empty)
                {
                    CustomerInfoNotification("Error", "Please enter Sender Company");
                    txtSenderCompany.Focus();
                }
                else if (txtReceiverCompany.Text.Trim() == string.Empty)
                {
                    CustomerInfoNotification("Error", "Please enter Receiver Company");
                    txtReceiverCompany.Focus();
                }
                else if (txtCustomerCompany.Text.Trim() == string.Empty)
                {
                    CustomerInfoNotification("Error", "Please enter Customer Company");
                    txtCustomerCompany.Focus();
                }
                else if (ddlBillingType.SelectedIndex == 0)
                {
                    CustomerInfoNotification("Error", "Please select Payment Type");
                    txtCustomerCompany.Focus();
                }
                else if (txtLoadingDate.Text.Trim() == string.Empty)
                {
                    ShippingInfoNotification("Error", "Please enter Loading Date");
                    txtLoadingDate.Focus();
                }
                //else if (gvVehicleInfo.Rows.Count <= 0)
                //{
                //    VehicleInfoNotification("Error", "Please enter Vehicle Information");
                //}
                else if (txtBiltyFreight.Text.Trim() == string.Empty)
                {
                    BiltyFreightNotification("Error", "Please enter bilty freight");
                    txtBiltyFreight.Focus();
                }
                else
                {
                    Int64 BiltyNo = txtBiltyNo.Text == string.Empty ? rnd.Next(0, 999999) : Convert.ToInt64(txtBiltyNo.Text);

                    Int64 SenderCompanyID = 0;
                    Int64 ReceiverCompanyID = 0;
                    Int64 CustomerCompanyID = 0;

                    CompanyDML dmlCompany = new CompanyDML();
                    string[] SenderCompanyString = txtSearchSender.Text.Split('|');
                    DataTable dtSenderCompany = dmlCompany.GetCompanyByCode(SenderCompanyString[0].ToString().Trim());
                    SenderCompanyID = Convert.ToInt64(dtSenderCompany.Rows[0]["CompanyID"].ToString());

                    string[] ReceiverCompanyString = txtSearchReceiver.Text.Split('|');
                    DataTable dtReceiverCompany = dmlCompany.GetCompanyByCode(ReceiverCompanyString[0].ToString().Trim());
                    ReceiverCompanyID = Convert.ToInt64(dtReceiverCompany.Rows[0]["CompanyID"].ToString());

                    string[] CustomerCompanyString = txtSearchCustomer.Text.Split('|');
                    DataTable dtCustomerCompany = dmlCompany.GetCompanyByCode(CustomerCompanyString[0].ToString().Trim());
                    CustomerCompanyID = Convert.ToInt64(dtCustomerCompany.Rows[0]["CompanyID"].ToString());

                    string BiltyDate = txtBiltyDate.Text;
                    string SenderGroup = txtSenderGroup.Text;
                    string SenderCompany = txtSenderCompany.Text;
                    string SenderDepartment = txtSenderDepartment.Text;

                    string ReceiverGroup = txtReceiverGroup.Text;
                    string ReceiverCompany = txtReceiverCompany.Text;
                    string ReceiverDepartment = txtReceiverDepartment.Text;

                    string CustomerGroup = txtCustomerGroup.Text;
                    string CustomerCompany = txtCustomerCompany.Text;
                    string CustomerDepartment = txtCustomerDepartment.Text;

                    string PaymentType = ddlBillingType.SelectedItem.Text;

                    Int64 PickupLocationID = 0;
                    Int64 DropoffLocationID = 0;
                    string[] PickupLocationString = txtSearchPickLocation.Text.Split('|');
                    LocationDML dmlLocation = new LocationDML();
                    DataTable dtLocation = dmlLocation.GetLocationByCode(PickupLocationString[4].ToString().Trim());
                    if (dtLocation.Rows.Count > 0)
                    {
                        PickupLocationID = Convert.ToInt64(dtLocation.Rows[0]["PickDropID"].ToString());
                    }

                    string[] DropoffLocationString = txtSearchDropLocation.Text.Split('|');
                    dtLocation = dmlLocation.GetLocationByCode(DropoffLocationString[4].ToString().Trim());
                    if (dtLocation.Rows.Count > 0)
                    {
                        DropoffLocationID = Convert.ToInt64(dtLocation.Rows[0]["PickDropID"].ToString());
                    }

                    string ClearingAgent = ddlClearingAgent.Items.Count > 0 ? (ddlClearingAgent.SelectedIndex == 0 ? string.Empty : ddlClearingAgent.SelectedItem.Text) : string.Empty;

                    double AdditionalWeight = txtAdditionalWeight.Text == string.Empty ? 0 : Convert.ToDouble(txtAdditionalWeight.Text);
                    double ActualWeight = txtActualWeight.Text == string.Empty ? 0 : Convert.ToDouble(txtActualWeight.Text);

                    double BiltyFreight = txtBiltyFreight.Text == string.Empty ? 0 : Convert.ToDouble(txtBiltyFreight.Text);
                    double Freight = txtFreight.Text == string.Empty ? 0 : Convert.ToDouble(txtFreight.Text);
                    double PartyCommission = txtPartyCommission.Text == string.Empty ? 0 : Convert.ToDouble(txtPartyCommission.Text);

                    double AdvanceFreight = txtAdvanceFreight.Text == string.Empty ? 0 : Convert.ToDouble(txtAdvanceFreight.Text);
                    double FactoryAdvance = txtFactoryAdvance.Text == string.Empty ? 0 : Convert.ToDouble(txtFactoryAdvance.Text);
                    double DieselAdvance = txtDieselAdvance.Text == string.Empty ? 0 : Convert.ToDouble(txtDieselAdvance.Text);
                    double AdvanceAmount = txtVehicleAdvanceAmount.Text == string.Empty ? 0 : Convert.ToDouble(txtVehicleAdvanceAmount.Text);
                    double TotalAdvance = txtTotalAdvance.Text == string.Empty ? 0 : Convert.ToDouble(txtTotalAdvance.Text);
                    double BalanceFreight = txtBalanceFreight.Text == string.Empty ? 0 : Convert.ToDouble(txtBalanceFreight.Text);


                    OrderDML dml = new OrderDML();
                    Int64 OrderID = dml.InsertBiltyOrder(BiltyNo, BiltyDate, SenderCompanyID, SenderDepartment, ReceiverCompanyID, ReceiverDepartment, CustomerCompanyID, 
                        CustomerDepartment, PaymentType, PickupLocationID, DropoffLocationID, ClearingAgent, AdditionalWeight, ActualWeight, BiltyFreight, Freight, 
                        PartyCommission, AdvanceFreight, FactoryAdvance, DieselAdvance, AdvanceAmount, TotalAdvance, BalanceFreight, LoginID);

                    if (OrderID > 0)
                    {
                        //Inserting Container Info
                        try
                        {
                            if (gvContainerInfo.Rows.Count > 0)
                            {

                                for (int i = 0; i < gvContainerInfo.Rows.Count; i++)
                                {
                                    //Label lblContainerType = gvContainerInfo.Rows[i].FindControl("lblContainerType") as Label;
                                    TextBox txtContainerNo = gvContainerInfo.Rows[i].FindControl("txtContainerNo") as TextBox;
                                    TextBox txtWeight = gvContainerInfo.Rows[i].FindControl("txtWeight") as TextBox;
                                    TextBox txtVesselName = gvContainerInfo.Rows[i].FindControl("txtVesselName") as TextBox;
                                    TextBox txtRemarks = gvContainerInfo.Rows[i].FindControl("txtRemarks") as TextBox;
                                    DropDownList ddlPickLocation = gvContainerInfo.Rows[i].FindControl("ddlPickUpLocation") as DropDownList;
                                    DropDownList ddlDropOffLocation = gvContainerInfo.Rows[i].FindControl("ddlPickUpLocation") as DropDownList;
                                    DropDownList ddlVehicle = gvContainerInfo.Rows[i].FindControl("ddlVehicle") as DropDownList;


                                    //string ContainerType = lblContainerType.Text.Trim();
                                    Int64 ContainerTypeID = Convert.ToInt32(gvContainerInfo.DataKeys[i].Values[0]);
                                    string ContainerNo = txtContainerNo.Text.Trim();
                                    double Weight = txtWeight.Text == string.Empty ? 0 : Convert.ToDouble(txtWeight.Text.Trim());
                                    string ContainerPickup = ddlPickLocation.SelectedIndex == 0 ? string.Empty : ddlPickLocation.SelectedItem.Text;
                                    string ContainerDropoff = ddlDropOffLocation.SelectedIndex == 0 ? string.Empty : ddlDropOffLocation.SelectedItem.Text;
                                    string VesselName = txtVesselName.Text.Trim();
                                    string Remarks = txtRemarks.Text;
                                    string Vehicle = ddlVehicle.SelectedIndex == 0 ? string.Empty : ddlVehicle.SelectedItem.Text;

                                    //OrderDML dml = new OrderDML();
                                    Int64 OrderContainerID = dml.InsertOrderContainerInfo(OrderID, ContainerTypeID, ContainerNo, Weight, ContainerPickup, ContainerDropoff, VesselName, Remarks, Vehicle);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            ConsignmentInfoNotification("Error", "Error saving Container info, due to: " + ex.Message);
                        }

                        //Adding Vehicle information
                        try
                        {
                            if (gvVehicleInfo.Rows.Count > 0)
                            {
                                for (int i = 0; i < gvVehicleInfo.Rows.Count; i++)
                                {
                                    Label lblVehicleType = gvVehicleInfo.Rows[i].Cells[1].FindControl("lblVehicleType") as Label;
                                    TextBox txtVehicleRegNo = gvVehicleInfo.Rows[i].Cells[2].FindControl("txtVehicleNo") as TextBox;
                                    TextBox txtVehicleContactNo = gvVehicleInfo.Rows[i].Cells[3].FindControl("txtVehicleContactNo") as TextBox;
                                    DropDownList ddlBroker = gvVehicleInfo.Rows[i].Cells[4].FindControl("ddlBroker") as DropDownList;
                                    TextBox txtDriverName = gvVehicleInfo.Rows[i].Cells[5].FindControl("txtDriverName") as TextBox;
                                    TextBox txtFatherName = gvVehicleInfo.Rows[i].Cells[6].FindControl("txtFatherName") as TextBox;
                                    TextBox txtDriverNIC = gvVehicleInfo.Rows[i].Cells[7].FindControl("txtDriverNIC") as TextBox;
                                    TextBox txtDriverLicence = gvVehicleInfo.Rows[i].Cells[8].FindControl("txtDriverLicence") as TextBox;
                                    TextBox txtDriverCellNo = gvVehicleInfo.Rows[i].Cells[9].FindControl("txtDriveCellNo") as TextBox;

                                    string VehicleType = lblVehicleType.Text;
                                    string VehicleRegNo = txtVehicleRegNo.Text.Trim();
                                    Int64 VehicleContactNo = txtVehicleContactNo.Text.Trim() == string.Empty ? 0 : Convert.ToInt64(txtVehicleContactNo.Text.Trim());
                                    Int64 BrokerID = ddlBroker.SelectedIndex == 0 ? 0 : Convert.ToInt64(ddlBroker.SelectedItem.Value);
                                    string Driver = txtDriverName.Text.Trim();
                                    string FatherName = txtFatherName.Text.Trim();
                                    Int64 DriverNIC = txtDriverNIC.Text.Trim() == string.Empty ? 0 : Convert.ToInt64(txtDriverNIC.Text.Trim());
                                    string DriverLicense = txtDriverLicence.Text.Trim();

                                    Int64 DriverCellNo = txtDriverCellNo.Text.Trim() == string.Empty ? 0 : Convert.ToInt64(txtDriverCellNo.Text.Trim());

                                    string VehicleReportingDateTime = string.Empty;
                                    string VehicleInDateTime = string.Empty;
                                    string VehicleOutDateTime = string.Empty;

                                    if (gvVehicleTimings.Rows.Count > 0)
                                    {
                                        foreach (GridViewRow gvr in gvVehicleTimings.Rows)
                                        {
                                            if (gvr.Cells[1].Text == VehicleRegNo)
                                            {
                                                TextBox txtVRDate = gvr.Cells[2].FindControl("txtVRDate") as TextBox;
                                                TextBox txtVRTime = gvr.Cells[2].FindControl("txtVRTime") as TextBox;

                                                TextBox txtVIDate = gvr.Cells[3].FindControl("txtVIDate") as TextBox;
                                                TextBox txtVITime = gvr.Cells[3].FindControl("txtVITime") as TextBox;

                                                TextBox txtVODate = gvr.Cells[4].FindControl("txtVODate") as TextBox;
                                                TextBox txtVOTime = gvr.Cells[4].FindControl("txtVOTime") as TextBox;

                                                VehicleReportingDateTime = txtVRDate.Text + " " + txtVRTime.Text;
                                                VehicleInDateTime = txtVIDate.Text + " " + txtVITime.Text;
                                                VehicleOutDateTime = txtVODate.Text + " " + txtVOTime.Text;
                                            }
                                        }
                                    }

                                    dml.InsertOrderVehicleInfo(OrderID, VehicleType, VehicleRegNo, VehicleContactNo, BrokerID, Driver, FatherName, DriverNIC, DriverLicense, DriverCellNo, VehicleReportingDateTime, VehicleInDateTime, VehicleOutDateTime);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            VehicleInfoNotification("Error", "Error adding Vehicle Info, due to: " + ex.Message);
                        }

                        //Adding Product
                        try
                        {
                            if (gvProducts.Rows.Count > 0)
                            {
                                for (int i = 0; i < gvProducts.Rows.Count; i++)
                                {
                                    string Product = gvProducts.Rows[i].Cells[0].Text;
                                    string PackageType = gvProducts.Rows[i].Cells[1].Text;
                                    Int64 Quantity = gvProducts.Rows[i].Cells[2].Text.Trim().Replace("&nbsp;", string.Empty) == string.Empty ? 0 : Convert.ToInt64(gvProducts.Rows[i].Cells[2].Text);
                                    double Weight = gvProducts.Rows[i].Cells[3].Text.Trim().Replace("&nbsp;", string.Empty) == string.Empty ? 0 : Convert.ToDouble(gvProducts.Rows[i].Cells[3].Text.Trim().Replace("&nbsp;", string.Empty));

                                    dml.InsertOrderProduct(OrderID, PackageType, Product, Quantity, Weight);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            ProductInfoNotification("Error", "Error adding product information, due to: " + ex.Message);
                        }

                        //Add Receiving Information
                        try
                        {
                            if (gvReceiving.Rows.Count > 0)
                            {
                                for (int i = 0; i < gvReceiving.Rows.Count; i++)
                                {
                                    string ReceivedBy = gvReceiving.Rows[i].Cells[0].Text;
                                    string ReceivedDateTime = gvReceiving.Rows[i].Cells[1].Text;

                                    dml.InsertOrderReceiving(OrderID, ReceivedBy, ReceivedDateTime);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            ReceivingInfoNotification("Error", "Error adding receiving information, due to: " + ex.Message);
                        }

                        //Add Receiving Documents
                        try
                        {
                            if (gvReceivingDocument.Rows.Count > 0)
                            {
                                for (int i = 0; i < gvReceivingDocument.Rows.Count; i++)
                                {
                                    string sessionName = "ReceivingDocument" + (i);
                                    FileUpload fuDoc = (FileUpload)Session[sessionName];
                                    if (fuDoc != null)
                                    {
                                        string Type = gvReceivingDocument.Rows[i].Cells[0].Text.Replace("&nbsp;", string.Empty);
                                        string No = gvReceivingDocument.Rows[i].Cells[1].Text.Replace("&nbsp;", string.Empty);
                                        string Name = gvReceivingDocument.Rows[i].Cells[2].Text.Replace("&nbsp;", string.Empty);

                                        string folderPath = Server.MapPath("../assets/Document/Receiving/");

                                        //Check whether Directory (Folder) exists.
                                        if (!Directory.Exists(folderPath))
                                        {
                                            //If Directory (Folder) does not exists. Create it.
                                            Directory.CreateDirectory(folderPath);
                                        }

                                        Int64 OrderReceivingDocID = dml.InsertOrderReceivingDocument(OrderID, Type, No, Name, folderPath);
                                        if (OrderReceivingDocID > 0)
                                        {
                                            fuDoc.SaveAs(folderPath + Path.GetFileName(fuDoc.FileName));
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            ReceivingDocInfoNotification("Error", "Error add Receiving document information, due to: " + ex.Message);
                        }

                        //Adding Damage Detail
                        try
                        {
                            if (gvDamage.Rows.Count > 0)
                            {
                                for (int i = 0; i < gvDamage.Rows.Count; i++)
                                {
                                    string sessionName = "ReceivingDocument" + (i);
                                    FileUpload fuDoc = (FileUpload)Session[sessionName];

                                    if (fuDoc != null)
                                    {
                                        string Item = gvDamage.Rows[i].Cells[0].Text.Trim().Replace("&nbsp;", string.Empty);
                                        string DamageType = gvDamage.Rows[i].Cells[1].Text.Trim().Replace("&nbsp;", string.Empty);
                                        double DamageCost = gvDamage.Rows[i].Cells[2].Text.Trim().Replace("&nbsp;", string.Empty) == string.Empty ? 0 : Convert.ToDouble(gvDamage.Rows[i].Cells[2].Text);
                                        string DamageCause = gvDamage.Rows[i].Cells[3].Text.Trim().Replace("&nbsp;", string.Empty);
                                        string Document = gvDamage.Rows[i].Cells[4].Text.Replace("&nbsp;", string.Empty);

                                        string folderPath = Server.MapPath("../assets/Document/Damage/");

                                        //Check whether Directory (Folder) exists.
                                        if (!Directory.Exists(folderPath))
                                        {
                                            //If Directory (Folder) does not exists. Create it.
                                            Directory.CreateDirectory(folderPath);
                                        }

                                        Int64 OrderDamageDocID = dml.InsertOrderDamage(OrderID, Item, DamageType, DamageCost, DamageCause, Document, folderPath);
                                        if (OrderDamageDocID > 0)
                                        {
                                            fuDoc.SaveAs(folderPath + Path.GetFileName(fuDoc.FileName));
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            DamageInfoNotification("Error", "Error adding damage info, due to: " + ex.Message);
                        }


                        ClearFields();
                    }
                    else
                    {
                        notification("Error", "Bilty not recorded, please try again");
                    }
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error booking bilty, due to: " + ex.Message);
            }
        }

        #endregion

        #region TextBox TextChanged

        protected void txtSearchProduct_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSearchProduct.Text == string.Empty)
                {
                    notification("Error", "Please enter product code, name or package type to search product");
                }
                else
                {
                    string[] Product = txtSearchProduct.Text.Split('|');
                    if (Product.Length > 0)
                    {
                        txtPackageType.Text = Product[2].ToString().Trim();
                        txtItem.Text = Product[1].ToString().Trim();
                        txtProductQty.Text = txtProductQty.Text == string.Empty ? "0" : txtProductQty.Text;
                        txtTotalProductWeight.Text = txtProductQty.Text == "0" ? Product[3].ToString() : (Convert.ToDouble(txtProductQty.Text) * Convert.ToDouble(Product[3].ToString() == string.Empty ? "0" : Product[3].ToString())).ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error selecting product, due to: " + ex.Message);
            }
        }

        protected void txtProductQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string[] Product = txtSearchProduct.Text.Split('|');
                double Weight = 0;
                double Qty = Convert.ToDouble(txtProductQty.Text);
                if (Product.Length > 0)
                {
                    Weight = Convert.ToDouble(Product[3] == string.Empty ? "0" : Product[3]);
                }


                txtTotalProductWeight.Text = (Qty * Weight).ToString();
            }
            catch (Exception ex)
            {
                notification("Error", "Error");
            }
        }

        protected void txtSearchSender_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSearchSender.Text == string.Empty)
                {
                    notification("Error", "Please enter Company code, name or Group to search Company");
                }
                else
                {
                    string[] Company = txtSearchSender.Text.Split('|');
                    if (Company.Length > 0)
                    {
                        //Code, Group, Company, Department
                        txtSenderCompanyCode.Text = Company[0].ToString().Trim();
                        txtSenderCompany.Text = Company[1].ToString().Trim();
                        txtSenderGroup.Text = Company[2].ToString().Trim();
                        txtSenderDepartment.Text = Company[3].ToString().Trim();

                        //txtPackageType.Text = Company[2].ToString().Trim();
                        //txtItem.Text = Company[1].ToString().Trim();
                        //txtProductQty.Text = txtProductQty.Text == string.Empty ? "0" : txtProductQty.Text;
                        //txtTotalProductWeight.Text = txtProductQty.Text == "0" ? Company[3].ToString() : (Convert.ToDouble(txtProductQty.Text) * Convert.ToDouble(Company[3].ToString() == string.Empty ? "0" : Company[3].ToString())).ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error selecting product, due to: " + ex.Message);
            }
        }

        protected void txtSearchReceiver_TextChanged(object sender, EventArgs e)
        {
            if (txtSearchReceiver.Text == string.Empty)
            {
                notification("Error", "Please enter Company code, name or Group to search Company");
            }
            else
            {
                string[] Company = txtSearchReceiver.Text.Split('|');
                if (Company.Length > 0)
                {
                    //Code, Group, Company, Department
                    txtReceiverCompanyCode.Text = Company[0].ToString().Trim();
                    txtReceiverCompany.Text = Company[1].ToString().Trim();
                    txtReceiverGroup.Text = Company[2].ToString().Trim();
                    txtReceiverDepartment.Text = Company[3].ToString().Trim();
                }
            }
        }

        protected void txtSearchCustomer_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSearchCustomer.Text == string.Empty)
                {
                    notification("Error", "Please enter Company code, name or Group to search Company");
                }
                else
                {
                    string[] Company = txtSearchCustomer.Text.Split('|');
                    if (Company.Length > 0)
                    {
                        //Code, Group, Company, Department
                        txtCustomerCode.Text = Company[0].ToString().Trim();
                        txtCustomerCompany.Text = Company[1].ToString().Trim();
                        txtCustomerGroup.Text = Company[2].ToString().Trim();
                        txtCustomerDepartment.Text = Company[3].ToString().Trim();
                    }
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error searching Customer, due to: " + ex.Message);
            }
        }

        protected void txtSearchPickLocation_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSearchPickLocation.Text == string.Empty)
                {
                    notification("Error", "Please enter Company code, name or Group to search Company");
                }
                else
                {
                    string[] Locations = txtSearchPickLocation.Text.Split('|');
                    if (Locations.Length > 0)
                    {
                        //Code, Group, Company, Department
                        txtPickCity.Text = Locations[0].ToString().Trim();
                        txtPickRegion.Text = Locations[1].ToString().Trim();
                        txtPickArea.Text = Locations[2].ToString().Trim();
                        txtPickAddress.Text = Locations[3].ToString().Trim();
                    }
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error searching Pick Locations, due to: " + ex.Message);
            }
        }

        protected void txtSearchDropLocation_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSearchDropLocation.Text == string.Empty)
                {
                    notification("Error", "Please enter Company code, name or Group to search Company");
                }
                else
                {
                    string[] Locations = txtSearchDropLocation.Text.Split('|');
                    if (Locations.Length > 0)
                    {
                        //Code, Group, Company, Department
                        txtDropCity.Text = Locations[0].ToString().Trim();
                        txtDropRegion.Text = Locations[1].ToString().Trim();
                        txtDropArea.Text = Locations[2].ToString().Trim();
                        txtDropAddress.Text = Locations[3].ToString().Trim();
                    }
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error searching Drop Locations, due to: " + ex.Message);
            }
        }

        protected void txtFreight_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtPartyCommission.Text = CalculatePartyCommission().ToString();
            }
            catch (Exception ex)
            {
                BiltyFreightNotification("Error", "Error calculating party commission, due to: " + ex.Message);
            }

            try
            {
                txtBalanceFreight.Text = CalculateBalanceFreight().ToString();
            }
            catch (Exception ex)
            {
                BiltyFreightNotification("Error", "Error calculating party commission, due to: " + ex.Message);
            }
        }

        protected void txtBiltyFreight_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtPartyCommission.Text = CalculatePartyCommission().ToString();
            }
            catch (Exception ex)
            {
                BiltyFreightNotification("Error", "Error calculating party commission, due to: " + ex.Message);
            }
        }

        protected void txtAdvanceFreight_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtTotalAdvance.Text = CalculateTotalAdvance().ToString();
            }
            catch (Exception ex)
            {
                AdvanceInfoNotification("Error", "Error calculating total advance, due to: " + ex.Message);
            }


            try
            {
                txtBalanceFreight.Text = CalculateBalanceFreight().ToString();
            }
            catch (Exception ex)
            {
                BiltyFreightNotification("Error", "Error calculating party commission, due to: " + ex.Message);
            }
        }

        protected void txtTotalAdvance_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtBalanceFreight.Text = CalculateBalanceFreight().ToString();
            }
            catch (Exception ex)
            {
                BiltyFreightNotification("Error", "Error calculating party commission, due to: " + ex.Message);
            }
        }

        #endregion
                
        #region Gridview's Events

        protected void gvContainerInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList ddlPickLocation = e.Row.FindControl("ddlPickUpLocation") as DropDownList;
                    DropDownList ddlDropOffLocation = e.Row.FindControl("ddlDropoffLocation") as DropDownList;

                    ManualBiltyDML dml = new ManualBiltyDML();

                    DataTable dtPickUpLocation = dml.GetPickUpLocation();
                    FillDropDown(dtPickUpLocation, ddlPickLocation, "PickDropID", "PickUp", "- Select -");

                    DataTable dtDropOffLocation = dml.GetDropOffLocation();
                    FillDropDown(dtDropOffLocation, ddlDropOffLocation, "PickDropID", "DropLocation", "- Select -");

                    if (gvVehicleInfo.Rows.Count > 0)
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add("RegNo");
                        for (int i = 0; i < gvVehicleInfo.Rows.Count; i++)
                        {
                            TextBox txtRegNo = gvVehicleInfo.Rows[i].Cells[2].FindControl("txtVehicleNo") as TextBox;
                            dt.Rows.Add(txtRegNo.Text);
                        }
                        DropDownList ddlVehicle = e.Row.FindControl("ddlVehicle") as DropDownList;
                        FillDropDown(dt, ddlVehicle, "RegNo", "RegNo", "-Select-");
                    }
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error Binding row, due to: " + ex.Message);
            }
        }

        protected void gvConsignmentInfo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Wipe")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    DataTable dt = new DataTable();
                    dt.Columns.Add("ContainerType");
                    dt.Columns.Add("ContainerQty");
                    dt.Columns.Add("TotalWeight");

                    for (int i = 0; i < gvConsignmentInfo.Rows.Count; i++)
                    {
                        if (i != index)
                        {
                            dt.Rows.Add(gvConsignmentInfo.Rows[i].Cells[0].Text, gvConsignmentInfo.Rows[i].Cells[1].Text, gvConsignmentInfo.Rows[i].Cells[2].Text);
                        }
                    }
                    gvConsignmentInfo.DataSource = dt;
                    gvConsignmentInfo.DataBind();
                }
            }
            catch (Exception ex)
            {
                ConsignmentInfoNotification("Error", "Error Commanding Row, due to: " + ex.Message);
            }
        }

        protected void gvVehicleType_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Wipe")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    DataTable dt = new DataTable();
                    dt.Columns.Add("VehicleType");
                    dt.Columns.Add("VehicleQty");

                    for (int i = 0; i < gvVehicleType.Rows.Count; i++)
                    {
                        if (i != index)
                        {
                            dt.Rows.Add(gvVehicleType.Rows[i].Cells[0].Text, gvVehicleType.Rows[i].Cells[1].Text);
                        }
                    }
                    gvVehicleType.DataSource = dt;
                    gvVehicleType.DataBind();
                }
            }
            catch (Exception ex)
            {
                VehicleInfoNotification("Error", "Error Commanding Row, due to: " + ex.Message);
            }
        }

        protected void gvProducts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Wipe")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Item");
                    dt.Columns.Add("PackageType");
                    dt.Columns.Add("Qty");
                    dt.Columns.Add("TotalWeight");

                    for (int i = 0; i < gvProducts.Rows.Count; i++)
                    {
                        if (i != index)
                        {
                            dt.Rows.Add(gvProducts.Rows[i].Cells[0].Text, gvProducts.Rows[i].Cells[1].Text, gvProducts.Rows[i].Cells[2].Text, gvProducts.Rows[i].Cells[3].Text);
                        }
                    }
                    gvProducts.DataSource = dt;
                    gvProducts.DataBind();
                }
            }
            catch (Exception ex)
            {
                ProductInfoNotification("Error", "Error Commanding Row, due to: " + ex.Message);
            }
        }

        protected void gvVehicleInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList ddlBroker = e.Row.Cells[4].FindControl("ddlBroker") as DropDownList;
                    BrokersDML dmlBrokers = new BrokersDML();
                    DataTable dtBrokers = dmlBrokers.GetBroker();
                    if (dtBrokers.Rows.Count > 0)
                    {
                        FillDropDown(dtBrokers, ddlBroker, "ID", "Name", "-Select-");
                    }
                }
            }
            catch (Exception ex)
            {
                VehicleInfoNotification("Error", "Error binding vehicle information to grid, due to: " + ex.Message);
            }
        }

        protected void gvVehicleTimings_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        #endregion

        #region Miscellaneous

        protected void cbAdvVehicle_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbAdvVehicle.Checked == true)
                {
                    divAdvanceVehicle.Visible = true;
                }
                else
                {
                    divAdvanceVehicle.Visible = false;
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error toggling, due to: " + ex.Message);
            }
        }

        protected void ddlVehicle_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList ddl = (DropDownList)sender;
                GridViewRow row = (GridViewRow)ddl.Parent.Parent;
                DropDownList ddlVehicle = row.FindControl("ddlVehicle") as DropDownList;
                foreach (GridViewRow gvr in gvContainerInfo.Rows)
                {
                    if (gvr != row)
                    {
                        DropDownList ddlVehicles = gvr.FindControl("ddlVehicle") as DropDownList;
                        if (ddlVehicles.SelectedItem.Text == ddlVehicle.SelectedItem.Text)
                        {
                            ddlVehicle.ClearSelection();
                            ConsignmentInfoModalNotification("Error", "Vehicle cannot be assigned twice, please try to assign another vehicle");
                        }
                    }

                }
                //int idx = row.RowIndex;
            }
            catch (Exception ex)
            {
                ConsignmentInfoModalNotification("Error", "Error selecting vehicle, due to: " + ex.Message);
            }
            finally
            {
                modalContainerInfo.Show();
            }
        }

        #endregion

        #endregion
    }
}