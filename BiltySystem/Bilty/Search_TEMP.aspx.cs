using BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiltySystem.Bilty
{
    public partial class Search_TEMP : System.Web.UI.Page
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
            notification();
            BiltyNotification();
            VehicleNotification();
            ContainerNotification();
            ProductNotification();
            ReceivingNotification();
            ReceivingDocNotification();
            if (!IsPostBack)
            {
                this.Title = "Search Orders";
                try
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

                    //Getting/Binding Vehicle Types
                    try
                    {
                        VehicleTypeDML dml = new VehicleTypeDML();
                        DataTable dtVehicleType = dml.GetVehicleType();
                        if (dtVehicleType.Rows.Count > 0)
                        {
                            FillDropDown(dtVehicleType, ddlVehicleType, "VehicleTypeID", "VehicleTypeName", "- Select -");
                        }
                    }
                    catch (Exception ex)
                    {
                        notification("Error", "Error getting/binding Vehicle Types, due to: " + ex.Message);
                    }

                    //Getting/Binding Brokers
                    try
                    {
                        BrokersDML dml = new BrokersDML();
                        DataTable dtBrokers = dml.GetBroker();
                        if (dtBrokers.Rows.Count > 0)
                        {
                            FillDropDown(dtBrokers, ddlBroker, "ID", "Name", "- Select -");
                        }
                    }
                    catch (Exception ex)
                    {
                        notification("Error", "Error getting/binding Brokers, due to: " + ex.Message);
                    }

                    //Getting/Binding Container Types
                    try
                    {
                        ContainersDML dml = new ContainersDML();
                        DataTable dtContainers = dml.GetContainerType();
                        if (dtContainers.Rows.Count > 0)
                        {
                            FillDropDown(dtContainers, ddlContainerType, "ContainerTypeID", "ContainerType", "- Select -");
                        }
                    }
                    catch (Exception ex)
                    {
                        notification("Error", "Error getting/binding Brokers, due to: " + ex.Message);
                    }

                    //Gettting/Populating Locations
                    try
                    {
                        ManualBiltyDML dmlBilty = new ManualBiltyDML();
                        DataTable dtPickUpLocation = dmlBilty.GetPickUpLocation();
                        FillDropDown(dtPickUpLocation, ddlContainerPickup, "PickDropID", "Location", "- Select -");

                        DataTable dtDropOffLocation = dmlBilty.GetDropOffLocation();
                        FillDropDown(dtDropOffLocation, ddlContainerDropoff, "PickDropID", "Location", "- Select -");
                    }
                    catch (Exception ex)
                    {
                        notification("Error", "Error getting/populating locations, due to: " + ex.Message);
                    }

                    //Gettting/Populating Package Types for Product
                    try
                    {
                        PackagingTypeDML dmlPackageType = new PackagingTypeDML();
                        DataTable dtPackageType = dmlPackageType.GetPackage();
                        FillDropDown(dtPackageType, ddlPackageType, "PackageTypeID", "PackageTypeName", "- Select -");
                    }
                    catch (Exception ex)
                    {
                        notification("Error", "Error getting/populating Package Types, due to: " + ex.Message);
                    }

                    //Gettting/Populating Products
                    try
                    {
                        ProductDML dmlProduct = new ProductDML();
                        DataTable dtProduct = dmlProduct.GetProductDDL();
                        FillDropDown(dtProduct, ddlProductItem, "ID", "Product", "- Select -");
                    }
                    catch (Exception ex)
                    {
                        notification("Error", "Error getting/populating Package Types, due to: " + ex.Message);
                    }

                    //Gettting/Populating Document Type
                    try
                    {
                        DocumentTypeDML dml = new DocumentTypeDML();
                        DataTable dtDocType = dml.GetDocumentType();
                        FillDropDown(dtDocType, ddlDocumentType, "DocumentTypeID", "Name", "- Select -");
                    }
                    catch (Exception ex)
                    {
                        notification("Error", "Error getting/populating Package Types, due to: " + ex.Message);
                    }

                    //Gettting/Populating Damage Type
                    try
                    {
                        DamageTypeDML dml = new DamageTypeDML();
                        DataTable dtDamage = dml.GetDamageType();
                        FillDropDown(dtDamage, ddlDamageType, "DamageTypeID", "Name", "- Select -");
                    }
                    catch (Exception ex)
                    {
                        notification("Error", "Error getting/populating Damage Types, due to: " + ex.Message);
                    }

                    //Gettting/Populating Damage Items
                    try
                    {
                        ItemDML dml = new ItemDML();
                        DataTable dtDamageItems = dml.GetItem();
                        FillDropDown(dtDamageItems, ddlDamageItem, "ID", "Product", "- Select -");
                    }
                    catch (Exception ex)
                    {
                        notification("Error", "Error getting/populating Damage items, due to: " + ex.Message);
                    }

                    //Gettting/Populating Damage Items
                    try
                    {
                        CompanyDML dml = new CompanyDML();
                        DataTable dtCompanies = dml.GetCompaniesForBilty();
                        FillDropDown(dtCompanies, ddlSearchSender, "CompanyID", "Company", "- Select -");
                        FillDropDown(dtCompanies, ddlSearchReceiver, "CompanyID", "Company", "- Select -");
                        FillDropDown(dtCompanies, ddlSearchCustomer, "CompanyID", "Company", "- Select -");
                    }
                    catch (Exception ex)
                    {
                        notification("Error", "Error getting/populating Damage items, due to: " + ex.Message);
                    }

                    //Gettting/Populating Pick/Drop Locations
                    try
                    {
                        LocationDML dml = new LocationDML();
                        DataTable dtPickLocations = dml.GetPickLocationsForBilty();
                        DataTable dtDropLocations = dml.GetDropLocationsForBilty();
                        FillDropDown(dtPickLocations, ddlSearchPickLocation, "PickDropID", "Location", "- Select -");
                        FillDropDown(dtDropLocations, ddlSearchDropLocation, "PickDropID", "Location", "- Select -");
                    }
                    catch (Exception ex)
                    {
                        notification("Error", "Error getting/populating Pick/Drop Locations, due to: " + ex.Message);
                    }

                    GetBilties();
                }
                catch (Exception ex)
                {
                    notification("Error", "Error getting bilties, due to: " + ex.Message);
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

        public void BiltyNotification()
        {
            try
            {
                divBiltyNotification.InnerHtml = string.Empty;
            }
            catch (Exception ex)
            {
                divBiltyNotification.InnerHtml = "<div class=\"alert alert-danger\">" + ex.Message + "</div>";
            }
        }

        public void BiltyNotification(string type, string msg)
        {
            try
            {
                if (type == "Error")
                {
                    divBiltyNotification.InnerHtml = "<div class=\"alert alert-danger\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                }
                else if (type == "Success")
                {
                    divBiltyNotification.InnerHtml = "<div class=\"alert alert-success\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                }
                else if (type == string.Empty)
                {
                    divBiltyNotification.InnerHtml = string.Empty;
                }
            }
            catch (Exception ex)
            {
                divBiltyNotification.InnerHtml = "<div class=\"alert alert-danger\">" + ex.Message + "</div>";
            }
        }

        public void VehicleNotification()
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

        public void VehicleNotification(string type, string msg)
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

        public void ContainerNotification()
        {
            try
            {
                divContainerNotifications.InnerHtml = string.Empty;
            }
            catch (Exception ex)
            {
                divContainerNotifications.InnerHtml = "<div class=\"alert alert-danger\">" + ex.Message + "</div>";
            }
        }

        public void ContainerNotification(string type, string msg)
        {
            try
            {
                if (type == "Error")
                {
                    divContainerNotifications.InnerHtml = "<div class=\"alert alert-danger\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                }
                else if (type == "Success")
                {
                    divContainerNotifications.InnerHtml = "<div class=\"alert alert-success\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                }
                else if (type == string.Empty)
                {
                    divContainerNotifications.InnerHtml = string.Empty;
                }
            }
            catch (Exception ex)
            {
                divContainerNotifications.InnerHtml = "<div class=\"alert alert-danger\">" + ex.Message + "</div>";
            }
        }

        public void ProductNotification()
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

        public void ProductNotification(string type, string msg)
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

        public void ReceivingNotification()
        {
            try
            {
                divRecievingNotification.InnerHtml = string.Empty;
            }
            catch (Exception ex)
            {
                divRecievingNotification.InnerHtml = "<div class=\"alert alert-danger\">" + ex.Message + "</div>";
            }
        }

        public void ReceivingNotification(string type, string msg)
        {
            try
            {
                if (type == "Error")
                {
                    divRecievingNotification.InnerHtml = "<div class=\"alert alert-danger\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                }
                else if (type == "Success")
                {
                    divRecievingNotification.InnerHtml = "<div class=\"alert alert-success\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                }
                else if (type == string.Empty)
                {
                    divRecievingNotification.InnerHtml = string.Empty;
                }
            }
            catch (Exception ex)
            {
                divRecievingNotification.InnerHtml = "<div class=\"alert alert-danger\">" + ex.Message + "</div>";
            }
        }

        public void ReceivingDocNotification()
        {
            try
            {
                hfReceivingDocNotification.InnerHtml = string.Empty;
            }
            catch (Exception ex)
            {
                hfReceivingDocNotification.InnerHtml = "<div class=\"alert alert-danger\">" + ex.Message + "</div>";
            }
        }

        public void ReceivingDocNotification(string type, string msg)
        {
            try
            {
                if (type == "Error")
                {
                    hfReceivingDocNotification.InnerHtml = "<div class=\"alert alert-danger\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                }
                else if (type == "Success")
                {
                    hfReceivingDocNotification.InnerHtml = "<div class=\"alert alert-success\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                }
                else if (type == string.Empty)
                {
                    hfReceivingDocNotification.InnerHtml = string.Empty;
                }
            }
            catch (Exception ex)
            {
                hfReceivingDocNotification.InnerHtml = "<div class=\"alert alert-danger\">" + ex.Message + "</div>";
            }
        }

        public void DamageNotification()
        {
            try
            {
                divDamageNotification.InnerHtml = string.Empty;
            }
            catch (Exception ex)
            {
                divDamageNotification.InnerHtml = "<div class=\"alert alert-danger\">" + ex.Message + "</div>";
            }
        }

        public void DamageNotification(string type, string msg)
        {
            try
            {
                if (type == "Error")
                {
                    divDamageNotification.InnerHtml = "<div class=\"alert alert-danger\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                }
                else if (type == "Success")
                {
                    divDamageNotification.InnerHtml = "<div class=\"alert alert-success\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                }
                else if (type == string.Empty)
                {
                    divDamageNotification.InnerHtml = string.Empty;
                }
            }
            catch (Exception ex)
            {
                divDamageNotification.InnerHtml = "<div class=\"alert alert-danger\">" + ex.Message + "</div>";
            }
        }

        #endregion

        #region GetMethods

        public void GetBiltyVehicles(Int64 OrderID)
        {
            try
            {
                OrderDML dml = new OrderDML();
                DataTable dtVehicle = dml.GetBiltyVehiclesByOrder(OrderID);
                if (dtVehicle.Rows.Count > 0)
                {
                    gvBiltyVehicles.DataSource = dtVehicle;
                }
                else
                {
                    gvBiltyVehicles.DataSource = null;
                }
                gvBiltyVehicles.DataBind();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void GetBiltyContainers(Int64 OrderID)
        {
            try
            {
                OrderDML dml = new OrderDML();
                DataTable dtContainer = dml.GetBiltyContainersByOrder(OrderID);
                if (dtContainer.Rows.Count > 0)
                {
                    gvContainer.DataSource = dtContainer;
                }
                else
                {
                    gvContainer.DataSource = null;
                }
                gvContainer.DataBind();
            }
            catch (Exception ex)
            {
                ContainerNotification("Error", "Error Getting/Binding Containers, due to: " + ex.Message);
            }
        }

        public void GetBiltyProducts(Int64 OrderID)
        {
            try
            {
                OrderDML dml = new OrderDML();
                DataTable dtProducts = dml.GetBiltyProductsByOrder(OrderID);
                if (dtProducts.Rows.Count > 0)
                {
                    gvProduct.DataSource = dtProducts;
                }
                else
                {
                    gvProduct.DataSource = null;
                }
                gvProduct.DataBind();
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting products detail, due to: " + ex.Message);
            }
        }

        public void GetBiltyReceiving(Int64 OrderID)
        {
            try
            {
                OrderDML dml = new OrderDML();
                DataTable dtReceiving = dml.GetBiltyReceivingByOrder(OrderID);
                if (dtReceiving.Rows.Count > 0)
                {
                    gvRecievings.DataSource = dtReceiving;
                }
                else
                {
                    gvRecievings.DataSource = null;
                }
                gvRecievings.DataBind();
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting Bilty receiving, due to: " + ex.Message);
            }
        }

        public void GetBiltyReceivingDocs(Int64 OrderID)
        {
            try
            {
                OrderDML dml = new OrderDML();
                DataTable dtReceivingDoc = dml.GetBiltyReceivingDocByOrder(OrderID);
                if (dtReceivingDoc.Rows.Count > 0)
                {
                    gvRecievingDoc.DataSource = dtReceivingDoc;
                }
                else
                {
                    gvRecievingDoc.DataSource = null;
                }
                gvRecievingDoc.DataBind();
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting Bilty receiving, due to: " + ex.Message);
            }
        }

        public void GetBiltyDamages(Int64 OrderID)
        {
            try
            {
                OrderDML dml = new OrderDML();
                DataTable dtDamages = dml.GetBiltyDamageByOrder(OrderID);
                if (dtDamages.Rows.Count > 0)
                {
                    gvDamage.DataSource = dtDamages;
                }
                else
                {
                    gvDamage.DataSource = null;
                }
                gvDamage.DataBind();
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting Order Damages, due to: " + ex.Message);
            }
        }

        public void GetBilties()
        {
            try
            {
                OrderDML dml = new OrderDML();
                DataTable dtBilties = new DataTable();
                if (txtKeyword.Text == string.Empty)
                {
                    dtBilties = dml.GetBilties();
                }
                else
                {
                    string Keyword = txtKeyword.Text;
                    dtBilties = dml.GetBilties(Keyword);
                }

                if (dtBilties.Rows.Count > 0)
                {
                    gvBilty.DataSource = dtBilties;
                }
                else
                {
                    gvBilty.DataSource = dtBilties;
                }
                gvBilty.DataBind();
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting/binding bilties, due to: " + ex.Message);
            }
        }

        #endregion

        public void ClearDriverVehicleFields()
        {
            try
            {
                ddlVehicleType.ClearSelection();
                txtVehicleRegNo.Text = string.Empty;
                txtVehicleContactNo.Text = string.Empty;
                ddlBroker.ClearSelection();
                txtDriverName.Text = string.Empty;
                txtDriverfather.Text = string.Empty; ;
                txtDriverNIC.Text = string.Empty;
                txtDriverLicense.Text = string.Empty;
                txtDriverContactNo.Text = string.Empty;
            }
            catch (Exception ex)
            {
                notification("Error", "Error clearing Order Vehicle fields, due to: " + ex.Message);
            }
        }

        public void ClearContainerFields()
        {
            try
            {
                ddlContainerType.ClearSelection();
                txtContainerNo.Text = string.Empty;
                txtWeight.Text = string.Empty;
                ddlContainerPickup.ClearSelection();
                ddlContainerDropoff.ClearSelection();
                txtVesselName.Text = string.Empty;
                txtRemarks.Text = string.Empty;
                ddlAssignedVehicle.ClearSelection();
            }
            catch (Exception ex)
            {
                ContainerNotification("Error", "Error clearing fields, due to: " + ex.Message);
            }
            finally
            {
                modalContainers.Show();
            }
        }

        public void ClearProductFields()
        {
            try
            {
                ddlPackageType.ClearSelection();
                ddlProductItem.ClearSelection();
                txtProductQantity.Text = string.Empty;
                txtProductWeight.Text = string.Empty;

                hfSelectedProductID.Value = string.Empty;
            }
            catch (Exception ex)
            {
                ProductNotification("Error", "Error clearing product fields, due to: " + ex.Message);
            }
        }

        public void ClearReceivingFields()
        {
            try
            {
                txtOrderReceivedBy.Text = string.Empty;
                txtOrderReceivingDate.Text = string.Empty;
                txtOrderReceivingTime.Text = string.Empty;
                hfSelectedReceiving.Value = string.Empty;
            }
            catch (Exception ex)
            {
                ReceivingNotification("Error", "Error clearing receiving fields, due to: " + ex.Message);
            }
        }

        public void ClearReceivingDocFields()
        {
            try
            {
                hfSelectedRecievingDocID.Value = string.Empty;
                ddlDocumentType.ClearSelection();
                txtDocumentNo.Text = string.Empty;
                hfReceivingDocumentName.Value = string.Empty;
            }
            catch (Exception ex)
            {
                ReceivingDocNotification("Error", "Error clearing receiving doc fields, due to: " + ex.Message);
            }
        }

        public void ClearDamageFields()
        {
            try
            {
                hfSelectedDamageID.Value = string.Empty;
                ddlDamageItem.ClearSelection();
                ddlDamageType.ClearSelection();
                txtDamageCost.Text = string.Empty;
                txtDamageCause.Text = string.Empty;
                hfDamageDocument.Value = string.Empty;
            }
            catch (Exception ex)
            {
                DamageNotification("Error", "Error clearing damage fields, due to: " + ex.Message);
            }
            finally
            {
                modalDamages.Show();
            }
        }

        public void ClearBiltyFields()
        {
            try
            {
                txtBiltyNo.Text = string.Empty;
                txtBiltyDate.Text = string.Empty;

                ddlSearchSender.ClearSelection();
                txtSenderCompanyCode.Text = string.Empty;
                txtSenderGroup.Text = string.Empty;
                txtSenderCompany.Text = string.Empty;
                txtSenderDepartment.Text = string.Empty;

                
                ddlSearchReceiver.ClearSelection();
                txtReceiverCompanyCode.Text = string.Empty;
                txtReceiverGroup.Text = string.Empty;
                txtReceiverCompany.Text = string.Empty;
                txtReceiverDepartment.Text = string.Empty;

                ddlSearchCustomer.ClearSelection();
                txtCustomerCode.Text = string.Empty;
                txtCustomerGroup.Text = string.Empty;
                txtCustomerCompany.Text = string.Empty;
                txtCustomerDepartment.Text = string.Empty;

                ddlShippingType.ClearSelection();
                txtLoadingDate.Text = string.Empty;

                ddlSearchPickLocation.ClearSelection();
                txtPickCity.Text = string.Empty;
                txtPickRegion.Text = string.Empty;
                txtPickArea.Text = string.Empty;
                txtPickAddress.Text = string.Empty;
                ddlSearchDropLocation.ClearSelection();
                txtDropCity.Text = string.Empty;
                txtDropRegion.Text = string.Empty;
                txtDropArea.Text = string.Empty;
                txtDropAddress.Text = string.Empty;

                ddlClearingAgent.ClearSelection();

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
            }
            catch (Exception ex)
            {
                notification("Error", "Error clearing fields, due to: " + ex.Message);
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
                BiltyNotification("Error", "Error calculating Party commission, due to: " + ex.Message);
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
                BiltyNotification("Error", "Error calculating Total Advance, due to: " + ex.Message);
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
                BiltyNotification("Error", "Error calculating balance freight, due to: " + ex.Message);
                return 0;
            }
        }

        public void SelectCompany(DropDownList _ddl, Int64 CompanyID, string Department, TextBox txtCompanyCode, TextBox txtCompany, TextBox txtCompanyGroup, TextBox txtCompanyDepartment)
        {
            try
            {
                if (_ddl.Items.Count > 0)
                {
                    _ddl.ClearSelection();
                    _ddl.Items.FindByValue(CompanyID.ToString()).Selected = true;

                    string[] Company = _ddl.SelectedItem.Text.Split('|');
                    if (Company.Length > 0)
                    {
                        //Code, Group, Company, Department
                        txtCompanyCode.Text = Company[0].ToString().Trim();
                        txtCompany.Text = Company[1].ToString().Trim();
                        txtCompanyGroup.Text = Company[2].ToString().Trim();
                        txtCompanyDepartment.Text = Company[3].ToString().Trim();
                    }
                }
            }
            catch (Exception ex)
            {
                BiltyNotification("Error", "Error selecting locations, due to: " + ex.Message); ;
            }
        }

        public void SelectLocations(DropDownList _ddl, Int64 LocationID, TextBox txtCity, TextBox txtRegion, TextBox txtArea, TextBox txtAddress)
        {
            try
            {
                if (_ddl.Items.Count > 0)
                {
                    _ddl.ClearSelection();
                    _ddl.Items.FindByValue(LocationID.ToString()).Selected = true;

                    string[] Locations = _ddl.SelectedItem.Text.Split('|');
                    if (Locations.Length > 0)
                    {
                        //Code, Group, Company, Department
                        txtCity.Text = Locations[0].ToString().Trim();
                        txtRegion.Text = Locations[1].ToString().Trim();
                        txtArea.Text = Locations[2].ToString().Trim();
                        txtAddress.Text = Locations[3].ToString().Trim();
                    }
                }
            }
            catch (Exception ex)
            {
                BiltyNotification("Error", "Error selecting Locations, due to: " + ex.Message);
            }
        }

        #endregion

        #region Web Methods

        [ScriptMethod()]
        [WebMethod]
        public static List<string> SearchProducts(string prefixText, int count)
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

        #region Events

        #region Gridview's Events

        protected void gvBilty_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Change")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvBilty.Rows[index];
                    Int64 OrderID = Convert.ToInt64(gvBilty.DataKeys[index]["OrderID"]);
                    hfSelectedOrder.Value = OrderID.ToString();
                    OrderDML dml = new OrderDML();
                    DataTable dtOrder = dml.GetBilty(OrderID);
                    if (dtOrder.Rows.Count > 0)
                    {
                        string BiltyNo = dtOrder.Rows[0]["OrderNo"].ToString().Replace("&nbsp;", string.Empty);
                        string BiltyDate = dtOrder.Rows[0]["Date"].ToString().Replace("&nbsp;", string.Empty);
                        Int64 SenderCompanyID = dtOrder.Rows[0]["SenderCompanyID"].ToString() == string.Empty ? 0 : Convert.ToInt64(dtOrder.Rows[0]["SenderCompanyID"].ToString());
                        Int64 ReceivercompanyID = dtOrder.Rows[0]["ReceiverCompanyID"].ToString() == string.Empty ? 0 : Convert.ToInt64(dtOrder.Rows[0]["ReceiverCompanyID"].ToString());
                        Int64 CustomerCompanyID = dtOrder.Rows[0]["CustomerCompanyID"].ToString() == string.Empty ? 0 : Convert.ToInt64(dtOrder.Rows[0]["CustomerCompanyID"].ToString());
                        string PaymentType = dtOrder.Rows[0]["PaymentType"].ToString();
                        string ShipmentType = dtOrder.Rows[0]["ShipmentType"].ToString().Replace("&nbsp;", ToString());
                        string SenderDepartment = dtOrder.Rows[0]["SenderDepartment"].ToString().Replace("&nbsp;", ToString());
                        string ReceiverDepartment = dtOrder.Rows[0]["ReceiverDepartment"].ToString().Replace("&nbsp;", ToString());
                        string BillToCustomerDepartment = dtOrder.Rows[0]["CustomerDepartment"].ToString().Replace("&nbsp;", ToString());
                        string LoadingDate = dtOrder.Rows[0]["LoadingDate"].ToString().Replace("&nbsp;", ToString());
                        Int64 PickupLocationID = dtOrder.Rows[0]["PickupLocationID"].ToString() == string.Empty ? 0 : Convert.ToInt64(dtOrder.Rows[0]["PickupLocationID"].ToString());
                        Int64 DropoffLocationID = dtOrder.Rows[0]["DropoffLocationID"].ToString() == string.Empty ? 0 : Convert.ToInt64(dtOrder.Rows[0]["DropoffLocationID"].ToString());
                        double BiltyFreight = dtOrder.Rows[0]["BiltyFreight"].ToString() == string.Empty ? 0 : Convert.ToInt64(dtOrder.Rows[0]["BiltyFreight"].ToString());
                        double Freight = dtOrder.Rows[0]["Freight"].ToString() == string.Empty ? 0 : Convert.ToInt64(dtOrder.Rows[0]["Freight"].ToString());
                        double PartyCommission = dtOrder.Rows[0]["PartyCommission"].ToString() == string.Empty ? 0 : Convert.ToInt64(dtOrder.Rows[0]["PartyCommission"].ToString());
                        double AdvanceFreight = dtOrder.Rows[0]["AdvanceFreight"].ToString() == string.Empty ? 0 : Convert.ToInt64(dtOrder.Rows[0]["AdvanceFreight"].ToString());
                        double FactoryAdvance = dtOrder.Rows[0]["FactoryAdvance"].ToString() == string.Empty ? 0 : Convert.ToInt64(dtOrder.Rows[0]["FactoryAdvance"].ToString());
                        double DieselAdvance = dtOrder.Rows[0]["DieselAdvance"].ToString() == string.Empty ? 0 : Convert.ToInt64(dtOrder.Rows[0]["DieselAdvance"].ToString());
                        double AdvanceAmount = dtOrder.Rows[0]["AdvanceAmount"].ToString() == string.Empty ? 0 : Convert.ToInt64(dtOrder.Rows[0]["AdvanceAmount"].ToString());
                        double TotalAdvance = dtOrder.Rows[0]["TotalAdvance"].ToString() == string.Empty ? 0 : Convert.ToInt64(dtOrder.Rows[0]["TotalAdvance"].ToString());
                        double BalanceFreight = dtOrder.Rows[0]["BalanceFreight"].ToString() == string.Empty ? 0 : Convert.ToInt64(dtOrder.Rows[0]["BalanceFreight"].ToString());
                        double ActualWeight = dtOrder.Rows[0]["ActualWeight"].ToString() == string.Empty ? 0 : Convert.ToInt64(dtOrder.Rows[0]["ActualWeight"].ToString());
                        double AdditionalWeight = dtOrder.Rows[0]["AdditionalWeight"].ToString() == string.Empty ? 0 : Convert.ToInt64(dtOrder.Rows[0]["AdditionalWeight"].ToString());

                        txtBiltyNo.Text = BiltyNo;
                        hfBiltyDate.Value = BiltyDate;
                        SelectCompany(ddlSearchSender, SenderCompanyID, SenderDepartment, txtSenderCompanyCode, txtSenderCompany, txtSenderGroup, txtSenderDepartment);
                        SelectCompany(ddlSearchReceiver, ReceivercompanyID, ReceiverDepartment, txtReceiverCompanyCode, txtReceiverCompany, txtReceiverGroup, txtReceiverDepartment);
                        SelectCompany(ddlSearchCustomer, CustomerCompanyID, BillToCustomerDepartment, txtCustomerCode, txtCustomerCompany, txtCustomerGroup, txtCustomerDepartment);

                        
                        ddlBillingType.ClearSelection();
                        if (PaymentType != string.Empty)
                        {
                            ddlBillingType.Items.FindByText(PaymentType).Selected = true;
                        }

                        ddlShippingType.ClearSelection();
                        if (ShipmentType != string.Empty)
                        {
                            ddlShippingType.Items.FindByText(ShipmentType).Selected = true;
                        }
                        

                        hfLoadingDate.Value = LoadingDate;

                        SelectLocations(ddlSearchPickLocation, PickupLocationID, txtPickCity, txtPickRegion, txtPickArea, txtPickAddress);
                        SelectLocations(ddlSearchDropLocation, DropoffLocationID, txtDropCity, txtDropRegion, txtDropArea, txtDropAddress);

                        txtBiltyFreight.Text = BiltyFreight.ToString();
                        txtFreight.Text = Freight.ToString();
                        txtPartyCommission.Text = PartyCommission.ToString();

                        txtAdvanceFreight.Text = AdvanceFreight.ToString();
                        txtFactoryAdvance.Text = FactoryAdvance.ToString();
                        txtDieselAdvance.Text = DieselAdvance.ToString();
                        txtVehicleAdvanceAmount.Text = AdvanceAmount.ToString();
                        txtTotalAdvance.Text = TotalAdvance.ToString();
                        txtActualWeight.Text = ActualWeight.ToString();
                        txtAdditionalWeight.Text = AdditionalWeight.ToString();
                        txtBalanceFreight.Text = BalanceFreight.ToString();
                    }
                    else
                    {

                    }

                    modalBilty.Show();
                }
                else if (e.CommandName == "BiltyVehicles")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvBilty.Rows[index];
                    Int64 OrderID = Convert.ToInt64(gvBilty.DataKeys[index]["OrderID"]);
                    hfSelectedOrder.Value = OrderID.ToString();
                    OrderDML dml = new OrderDML();
                    GetBiltyVehicles(OrderID);

                    modalBiltyVehicles.Show();
                }
                else if (e.CommandName == "BiltyContainers")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvBilty.Rows[index];
                    Int64 OrderID = Convert.ToInt64(gvBilty.DataKeys[index]["OrderID"]);
                    hfSelectedOrder.Value = OrderID.ToString();
                    OrderDML dml = new OrderDML();
                    GetBiltyContainers(OrderID);

                    modalContainers.Show();

                    try
                    {
                        DataTable dtVehicles = dml.GetBiltyVehiclesByOrder(OrderID);
                        if (dtVehicles.Rows.Count > 0)
                        {
                            FillDropDown(dtVehicles, ddlAssignedVehicle, "OrderVehicleID", "VehicleRegNo", "-Select-");
                        }
                    }
                    catch (Exception ex)
                    {
                        notification("Error", "Error getting Vehicles used in selected order, due to: " + ex.Message);
                    }
                }
                else if (e.CommandName == "BiltyProducts")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvBilty.Rows[index];
                    Int64 OrderID = Convert.ToInt64(gvBilty.DataKeys[index]["OrderID"]);
                    hfSelectedOrder.Value = OrderID.ToString();
                    OrderDML dml = new OrderDML();
                    GetBiltyProducts(OrderID);

                    modalProducts.Show();
                }
                else if (e.CommandName == "BiltyRecievings")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvBilty.Rows[index];
                    Int64 OrderID = Convert.ToInt64(gvBilty.DataKeys[index]["OrderID"]);
                    hfSelectedOrder.Value = OrderID.ToString();
                    OrderDML dml = new OrderDML();
                    GetBiltyReceiving(OrderID);

                    modalRecievings.Show();
                }
                else if (e.CommandName == "BiltyRecievingDocs")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvBilty.Rows[index];
                    Int64 OrderID = Convert.ToInt64(gvBilty.DataKeys[index]["OrderID"]);
                    hfSelectedOrder.Value = OrderID.ToString();
                    OrderDML dml = new OrderDML();
                    GetBiltyReceivingDocs(OrderID);

                    modalRecievingDocs.Show();
                }
                else if (e.CommandName == "BiltDamages")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvBilty.Rows[index];
                    Int64 OrderID = Convert.ToInt64(gvBilty.DataKeys[index]["OrderID"]);
                    hfSelectedOrder.Value = OrderID.ToString();
                    OrderDML dml = new OrderDML();
                    GetBiltyDamages(OrderID);

                    modalDamages.Show();
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error commanding row, due to: " + ex.Message);
            }
        }

        protected void gvBiltyVehicles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Change")
                {

                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvBiltyVehicles.Rows[index];
                    Int64 OrderVehicleID = Convert.ToInt64(gvBiltyVehicles.DataKeys[index]["OrderVehicleID"]);
                    hfSelectedOrderVehicle.Value = OrderVehicleID.ToString();
                    OrderDML dml = new OrderDML();
                    DataTable dtVehicles = dml.GetBiltyVehicles(OrderVehicleID);
                    //pnlBiltyVehicleInputs.Visible = true;
                    if (dtVehicles.Rows.Count > 0)
                    {


                        pnlBiltyVehicleInputs.Visible = true;


                        ddlVehicleType.ClearSelection();
                        ddlVehicleType.Items.FindByText(dtVehicles.Rows[0]["VehicleType"].ToString()).Selected = true;



                        // 
                        txtVehicleRegNo.Text = dtVehicles.Rows[0]["VehicleRegNo"].ToString();
                        txtVehicleContactNo.Text = dtVehicles.Rows[0]["VehicleContactNo"].ToString();
                        if (dtVehicles.Rows[0]["BrokerID"].ToString() == "0" || dtVehicles.Rows[0]["BrokerID"].ToString() == "&nbsp;" || dtVehicles.Rows[0]["BrokerID"].ToString() == string.Empty)
                        {
                            ddlBroker.ClearSelection();
                        }
                        else
                        {
                            ddlBroker.ClearSelection();
                            ddlBroker.Items.FindByValue(dtVehicles.Rows[0]["BrokerID"].ToString()).Selected = true;
                        }
                        txtDriverName.Text = dtVehicles.Rows[0]["DriverName"].ToString();
                        txtDriverfather.Text = dtVehicles.Rows[0]["FatherName"].ToString();
                        txtDriverNIC.Text = dtVehicles.Rows[0]["DriverNIC"].ToString();
                        txtDriverLicense.Text = dtVehicles.Rows[0]["DriverLicence"].ToString();
                        txtDriverContactNo.Text = dtVehicles.Rows[0]["DriverCellNo"].ToString();
                    }
                    else
                    {
                        VehicleNotification("Error", "No such record found");
                    }
                }
                else if (e.CommandName == "Wipe")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvBiltyVehicles.Rows[index];
                    Int64 OrderVehicleID = Convert.ToInt64(gvBiltyVehicles.DataKeys[index]["OrderVehicleID"]);
                    OrderDML dml = new OrderDML();
                    dml.DeleteOrderVehicle(OrderVehicleID);
                    Int64 OrderID = Convert.ToInt64(hfSelectedOrder.Value);
                    GetBiltyVehicles(OrderID);
                    GetBilties();
                }
            }
            catch (Exception ex)
            {
                VehicleNotification("Error", "Error commanding row, due to: " + ex.Message);
            }
            finally
            {
                modalBiltyVehicles.Show();
            }
        }

        protected void gvContainer_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Change")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvContainer.Rows[index];
                    Int64 OrderContainerID = Convert.ToInt64(gvContainer.DataKeys[index]["OrderConsignmentID"]);
                    Int64 OrderID = Convert.ToInt64(hfSelectedOrder.Value);
                    OrderDML dml = new OrderDML();
                    DataTable dtContainer = dml.GetBiltyContainers(OrderContainerID);
                    hfSelectedOrderContainer.Value = OrderContainerID.ToString();

                    if (dtContainer.Rows.Count > 0)
                    {
                        ddlContainerType.ClearSelection();
                        ddlContainerType.Items.FindByValue(dtContainer.Rows[0]["ContainerType"].ToString()).Selected = true;

                        txtContainerNo.Text = dtContainer.Rows[0]["ContainerNo"].ToString();
                        txtWeight.Text = dtContainer.Rows[0]["ContainerWeight"].ToString();
                        ddlContainerPickup.ClearSelection();
                        ddlContainerPickup.Items.FindByText(dtContainer.Rows[0]["EmptyContainerPickLocation"].ToString()).Selected = true;
                        ddlContainerDropoff.ClearSelection();
                        ddlContainerDropoff.Items.FindByText(dtContainer.Rows[0]["EmptyContainerDropLocation"].ToString()).Selected = true;
                        txtVesselName.Text = dtContainer.Rows[0]["VesselName"].ToString();

                        DataTable dtOrderVehicle = dml.GetBiltyVehiclesByOrder(OrderID);
                        if (dtOrderVehicle.Rows.Count > 0)
                        {
                            FillDropDown(dtOrderVehicle, ddlAssignedVehicle, "OrderVehicleID", "VehicleRegNo", "-Select-");
                            ddlAssignedVehicle.ClearSelection();
                            if (dtContainer.Rows[0]["AssignedVehicle"].ToString() != string.Empty)
                            {
                                ddlAssignedVehicle.Items.FindByText(dtContainer.Rows[0]["AssignedVehicle"].ToString()).Selected = true;
                            }
                        }

                        pnlBiltyContainerInputs.Visible = true;

                    }
                    else
                    {
                        ContainerNotification("Error", "No such record found");
                    }
                }
                else if (e.CommandName == "Wipe")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvContainer.Rows[index];
                    Int64 OrderContainerID = Convert.ToInt64(gvContainer.DataKeys[index]["OrderConsignmentID"]);
                    Int64 OrderID = Convert.ToInt64(hfSelectedOrder.Value);

                    OrderDML dml = new OrderDML();
                    dml.DeleteOrderContainer(OrderContainerID);

                    GetBiltyContainers(OrderID);
                    GetBilties();
                }

            }
            catch (Exception ex)
            {
                ContainerNotification("Error", "Error commanding row, due to: " + ex.Message);
            }
            finally
            {
                modalContainers.Show();
            }
        }

        protected void gvProduct_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Change")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvProduct.Rows[index];
                    Int64 OrderProductID = Convert.ToInt64(gvProduct.DataKeys[index]["OrderProductID"]);
                    Int64 OrderID = Convert.ToInt64(hfSelectedOrder.Value);
                    OrderDML dml = new OrderDML();
                    DataTable dtProduct = dml.GetBiltyProductWithCode(OrderProductID);
                    hfSelectedProductID.Value = OrderProductID.ToString();

                    if (dtProduct.Rows.Count > 0)
                    {
                        ddlPackageType.ClearSelection();
                        ddlPackageType.Items.FindByText(dtProduct.Rows[0]["PackageType"].ToString()).Selected = true;

                        ddlProductItem.ClearSelection();

                        string ProductString = string.Empty;
                        string ProdCode = dtProduct.Rows[0]["Code"].ToString();
                        string Product = dtProduct.Rows[0]["Item"].ToString();
                        string PackageType = dtProduct.Rows[0]["PackageType"].ToString().Trim();
                        string Weight = dtProduct.Rows[0]["TotalWeight"].ToString().Replace("&nbsp;", string.Empty).Trim();

                        ProductString = ProdCode == string.Empty ? string.Empty : ProdCode + " | ";
                        ProductString += Product == string.Empty ? string.Empty : Product + " | ";
                        ProductString += PackageType == string.Empty ? string.Empty : PackageType + " | ";
                        ProductString += Weight == string.Empty ? string.Empty : Weight + " | ";
                        ProductString = ProductString.Substring(0, ProductString.Length - 3);
                        ddlProductItem.Items.FindByText(ProductString).Selected = true;

                        txtProductQantity.Text = dtProduct.Rows[0]["Qty"].ToString();
                        txtProductWeight.Text = dtProduct.Rows[0]["TotalWeight"].ToString();

                        pnlBiltyProductInputs.Visible = true;
                        modalProducts.Show();
                    }
                    else
                    {
                        ProductNotification("Error", "No such record found");
                    }
                }
                else if (e.CommandName == "Wipe")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvProduct.Rows[index];
                    Int64 OrderProductID = Convert.ToInt64(gvProduct.DataKeys[index]["OrderProductID"]);
                    Int64 OrderID = Convert.ToInt64(hfSelectedOrder.Value);
                    OrderDML dml = new OrderDML();
                    dml.DeleteOrderProduct(OrderProductID);
                    ProductNotification("Success", "Product Deleted from bilty");
                    GetBiltyProducts(OrderID);
                    GetBilties();
                }
            }
            catch (Exception ex)
            {
                ProductNotification("Error", "Error commanding row, due to: " + ex.Message);
            }
            finally
            {
                modalProducts.Show();
            }
        }

        protected void gvRecievings_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Change")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvRecievings.Rows[index];
                    Int64 OrderReceivingID = Convert.ToInt64(gvRecievings.DataKeys[index]["ConsignmentReceiverID"]);
                    Int64 OrderID = Convert.ToInt64(hfSelectedOrder.Value);
                    OrderDML dml = new OrderDML();
                    DataTable dtReceiving = dml.GetBiltyReceiving(OrderReceivingID);
                    hfSelectedReceiving.Value = OrderReceivingID.ToString();

                    if (dtReceiving.Rows.Count > 0)
                    {
                        txtOrderReceivedBy.Text = dtReceiving.Rows[0]["ReceivedBy"].ToString();
                        string[] DateTime = dtReceiving.Rows[0]["ReceivedDateTime"].ToString().Split(' ');
                        txtOrderReceivingDate.Text = DateTime[0].ToString() == string.Empty ? string.Empty : DateTime[0].ToString();
                        txtOrderReceivingTime.Text = DateTime[1].ToString() == string.Empty ? string.Empty : DateTime[1].ToString();

                        pnlRecievingInputs.Visible = true;
                    }
                    else
                    {
                        ReceivingNotification("Error", "No such record found");
                    }
                }
                else if (e.CommandName == "Wipe")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvRecievings.Rows[index];
                    Int64 OrderReceivingID = Convert.ToInt64(gvRecievings.DataKeys[index]["ConsignmentReceiverID"]);
                    Int64 OrderID = Convert.ToInt64(hfSelectedOrder.Value);
                    OrderDML dml = new OrderDML();
                    dml.DeleteOrderReceiving(OrderReceivingID);

                    GetBiltyReceiving(OrderID);
                    GetBilties();
                }
            }
            catch (Exception ex)
            {
                ReceivingNotification("Error", "Error commanding row, due to: " + ex.Message);
            }
            finally
            {
                modalRecievings.Show();
            }
        }

        protected void gvRecievingDoc_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Change")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvRecievingDoc.Rows[index];
                    Int64 OrderReceivingDocID = Convert.ToInt64(gvRecievingDoc.DataKeys[index]["OrderReceivedDocumentID"]);
                    Int64 OrderID = Convert.ToInt64(hfSelectedOrder.Value);
                    OrderDML dml = new OrderDML();
                    DataTable dtReceivingDoc = dml.GetBiltyReceivingDoc(OrderReceivingDocID);
                    hfSelectedRecievingDocID.Value = OrderReceivingDocID.ToString();

                    if (dtReceivingDoc.Rows.Count > 0)
                    {
                        ddlDocumentType.ClearSelection();
                        ddlDocumentType.Items.FindByText(dtReceivingDoc.Rows[0]["DocumentType"].ToString()).Selected = true;
                        txtDocumentNo.Text = dtReceivingDoc.Rows[0]["DocumentNo"].ToString();
                        hfReceivingDocumentName.Value = dtReceivingDoc.Rows[0]["DocumentName"].ToString();
                        pnlRecievingDocInputs.Visible = true;
                    }
                    else
                    {
                        ReceivingNotification("Error", "No such record found");
                    }
                }
                else if (e.CommandName == "Wipe")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvRecievingDoc.Rows[index];
                    Int64 OrderReceivingDocID = Convert.ToInt64(gvRecievingDoc.DataKeys[index]["OrderReceivedDocumentID"]);
                    Int64 OrderID = Convert.ToInt64(hfSelectedOrder.Value);
                    OrderDML dml = new OrderDML();
                    dml.DeleteOrderReceivingDoc(OrderReceivingDocID);

                    ReceivingDocNotification("Success", "Receiving document has been deleted successfully");
                    GetBiltyReceivingDocs(OrderID);
                    GetBilties();
                }
            }
            catch (Exception ex)
            {
                ReceivingDocNotification("Error", "Error commanding row, due to: " + ex.Message);
            }
            finally
            {
                modalRecievingDocs.Show();
            }
        }

        protected void gvDamage_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Change")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvDamage.Rows[index];
                    Int64 OrderDamageID = Convert.ToInt64(gvDamage.DataKeys[index]["OrderDamageID"]);
                    Int64 OrderID = Convert.ToInt64(hfSelectedOrder.Value);
                    OrderDML dml = new OrderDML();
                    //dml.DeleteOrderDamage(OrderDamageID);

                    DataTable dtDamages = dml.GetBiltyDamage(OrderDamageID);
                    hfSelectedDamageID.Value = OrderDamageID.ToString();

                    if (dtDamages.Rows.Count > 0)
                    {
                        ddlDamageItem.ClearSelection();
                        ddlDamageItem.Items.FindByText(dtDamages.Rows[0]["ItemName"].ToString()).Selected = true;

                        ddlDamageType.ClearSelection();
                        ddlDamageType.Items.FindByText(dtDamages.Rows[0]["DamageType"].ToString()).Selected = true;

                        txtDamageCost.Text = dtDamages.Rows[0]["DamageCost"].ToString();
                        txtDamageCause.Text = dtDamages.Rows[0]["DamageCause"].ToString();
                        hfDamageDocument.Value = dtDamages.Rows[0]["DamageDocumentName"].ToString().Replace("&nbsp;", string.Empty);
                        hfSelectedDamageID.Value = dtDamages.Rows[0]["OrderDamageID"].ToString();
                        pnlDamageInputs.Visible = true;
                    }
                    else
                    {
                        ReceivingNotification("Error", "No such record found");
                    }
                }
                else if (e.CommandName == "Wipe")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvDamage.Rows[index];
                    Int64 OrderDamageID = Convert.ToInt64(gvDamage.DataKeys[index]["OrderDamageID"]);
                    Int64 OrderID = Convert.ToInt64(hfSelectedOrder.Value);
                    OrderDML dml = new OrderDML();
                    dml.DeleteOrderDamage(OrderDamageID);

                    DamageNotification("Success", "Order damage has been deleted successfully");
                    GetBiltyDamages(OrderID);
                    GetBilties();
                }
            }
            catch (Exception ex)
            {
                DamageNotification("Error", "Error commanding row, due to: " + ex.Message);
            }
            finally
            {
                modalDamages.Show();
            }
        }

        #endregion

        #region LinkButton's Click

        protected void lnkAddNewBiltyVehicle_Click(object sender, EventArgs e)
        {
            try
            {
                pnlBiltyVehicleInputs.Visible = true;
            }
            catch (Exception ex)
            {
                VehicleNotification("Error", "Error enabling add new vehicle inputs, due to: " + ex.Message);
            }
            finally
            {
                modalBiltyVehicles.Show();
            }
        }

        protected void lnkCancelAddingNewBilty_Click(object sender, EventArgs e)
        {
            try
            {
                pnlBiltyVehicleInputs.Visible = false;
            }
            catch (Exception ex)
            {
                VehicleNotification("Error", "Error cancel adding vehicle, due to: " + ex.Message);
            }
            finally
            {
                modalBiltyVehicles.Show();
            }
        }

        protected void lnkSaveBiltyVehicles_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlVehicleType.SelectedIndex == 0)
                {
                    VehicleNotification("Error", "Please select Vehicle Type");
                    ddlVehicleType.Focus();
                }
                else if (txtVehicleRegNo.Text.Trim() == string.Empty)
                {

                    VehicleNotification("Error", "Please select Vehicle Registration No.");
                    txtVehicleRegNo.Focus();
                }
                else
                {
                    Int64 OrderID = Convert.ToInt64(hfSelectedOrder.Value);
                    string VehicleType = ddlVehicleType.SelectedItem.Text;
                    string VehicleRegNo = txtVehicleRegNo.Text;
                    Int64 VehicleContactNo = txtVehicleContactNo.Text == string.Empty ? 0 : Convert.ToInt64(txtVehicleContactNo.Text);
                    Int64 BrokerID = ddlBroker.Items.Count > 0 ? (ddlBroker.SelectedIndex == 0 ? 0 : Convert.ToInt64(ddlBroker.SelectedItem.Value)) : 0;
                    string Driver = txtDriverName.Text;
                    string Driverfather = txtDriverfather.Text;
                    Int64 DriverNIC = txtDriverNIC.Text.Trim() == string.Empty ? 0 : Convert.ToInt64(txtDriverNIC.Text);
                    string License = txtDriverLicense.Text.Trim();
                    Int64 ContactNo = txtDriverContactNo.Text == string.Empty ? 0 : Convert.ToInt64(txtDriverContactNo.Text);
                    string VehicleReportingDateTime = string.Empty;
                    string VehicleInDateTime = string.Empty;
                    string VehicleOutDateTime = string.Empty;

                    OrderDML dml = new OrderDML();
                    if (hfSelectedOrderVehicle.Value == string.Empty)
                    {
                        Int64 OrderVehicleID = dml.InsertOrderVehicleInfo(OrderID, VehicleType, VehicleRegNo, ContactNo, BrokerID, Driver, Driverfather, DriverNIC, License, ContactNo, VehicleReportingDateTime, VehicleInDateTime, VehicleOutDateTime);
                        if (OrderVehicleID > 0)
                        {
                            VehicleNotification("Success", "Vehicle added to Order");
                            pnlBiltyVehicleInputs.Visible = false;
                            ClearDriverVehicleFields();
                            GetBiltyVehicles(OrderID);
                            GetBilties();
                        }
                        else
                        {
                            VehicleNotification("Error", "Vehicle not inserted in Order, Try Again");
                        }
                    }
                    else
                    {
                        Int64 OrderVehicleID = Convert.ToInt64(hfSelectedOrderVehicle.Value);
                        dml.UpdateOrderVehicle(OrderVehicleID, VehicleType, VehicleRegNo, VehicleContactNo, BrokerID, Driver, Driverfather, DriverNIC, License, ContactNo);

                        VehicleNotification("Success", "Order Vehicle updated successfully");
                        pnlBiltyVehicleInputs.Visible = false;
                        ClearDriverVehicleFields();
                        GetBiltyVehicles(OrderID);
                        GetBilties();
                    }
                    
                }
            }
            catch (Exception ex)
            {
                VehicleNotification("Error", "Error saving Bilty Vehicle, due to: " + ex.Message);
            }
            finally
            {
                modalBiltyVehicles.Show();
            }
        }

        protected void lnkCloseBiltyVehicle_Click(object sender, EventArgs e)
        {
            try
            {
                ClearDriverVehicleFields();
                pnlBiltyVehicleInputs.Visible = false;
                modalBiltyVehicles.Hide();
                hfSelectedOrder.Value = string.Empty;
                hfSelectedOrderVehicle.Value = string.Empty;
            }
            catch (Exception ex)
            {
                VehicleNotification("Error", "Error closing Bilty Vehicles popup, due to: " + ex.Message);
                modalBiltyVehicles.Show();
            }
        }

        protected void lnkCloseBiltyContainer_Click(object sender, EventArgs e)
        {

        }

        protected void lnkAddNewContainer_Click(object sender, EventArgs e)
        {
            try
            {
                pnlBiltyContainerInputs.Visible = true;
            }
            catch (Exception ex)
            {
                ContainerNotification("Error", "Error enabling Containers input, due to: " + ex.Message);
            }
            finally
            {
                modalContainers.Show();
            }
        }

        protected void lnkAddNewProduct_Click(object sender, EventArgs e)
        {
            try
            {
                pnlBiltyProductInputs.Visible = true;
                modalProducts.Show();
            }
            catch (Exception ex)
            {
                ProductNotification("Error", "Error enabling Product inputs, due to: " + ex.Message);
            }
        }        

        protected void lnkCloseBiltyRecieving_Click(object sender, EventArgs e)
        {

        }

        protected void lnkAddNewRecieving_Click(object sender, EventArgs e)
        {
            try
            {
                pnlRecievingInputs.Visible = true;
            }
            catch (Exception ex)
            {
                ReceivingNotification("Error", "ErrorEnabling Receiving, due to: " + ex.Message);
            }
            finally
            {
                modalRecievings.Show();
            }
        }

        protected void lnkCloseBiltyRecievingDoc_Click(object sender, EventArgs e)
        {

        }

        protected void lnkAddNewRecievingDoc_Click(object sender, EventArgs e)
        {
            try
            {
                pnlRecievingDocInputs.Visible = true;
            }
            catch (Exception ex)
            {
                ReceivingDocNotification("Error", "Error enabling receiving doc input, due to: " + ex.Message);
            }
            finally
            {
                modalRecievingDocs.Show();
            }
        }

        protected void lnkCloseBiltyDamage_Click(object sender, EventArgs e)
        {

        }

        protected void lnkAddNewDamage_Click(object sender, EventArgs e)
        {
            try
            {
                pnlDamageInputs.Visible = true;
            }
            catch (Exception ex)
            {
                notification("Error", "Error enabling damages input, due to: " + ex.Message);
            }
            finally
            {
                modalDamages.Show();
            }
        }

        protected void lnkCancelSaveBiltyContainer_Click(object sender, EventArgs e)
        {
            try
            {
                pnlBiltyContainerInputs.Visible = false;
            }
            catch (Exception ex)
            {
                ContainerNotification("Error", "Error cancel adding Containers, due to: " + ex.Message);
            }
            finally
            {
                modalContainers.Show();
            }
        }

        protected void lnkSaveBiltyContainer_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlContainerType.SelectedIndex == 0)
                {
                    ContainerNotification("Error", "Please select container type");
                    ddlContainerType.Focus();
                }
                else if (txtContainerNo.Text == string.Empty)
                {
                    ContainerNotification("Error", "Please enter container no.");
                    txtContainerNo.Focus();
                }
                else
                {
                    Int64 OrderID = Convert.ToInt64(hfSelectedOrder.Value);
                    Int64 ContainerType = Convert.ToInt64(ddlContainerType.SelectedItem.Value);
                    string ContainerNo = txtContainerNo.Text;
                    double Weight = txtWeight.Text == string.Empty ? 0 : Convert.ToDouble(txtWeight.Text.Trim());
                    string CotnainerPickup = ddlContainerPickup.SelectedItem.Text == string.Empty ? string.Empty : ddlContainerPickup.SelectedItem.Text;
                    string CotnainerDropoff = ddlContainerDropoff.SelectedItem.Text == string.Empty ? string.Empty : ddlContainerDropoff.SelectedItem.Text;
                    string VesselName = txtVesselName.Text.Trim();
                    string Remarks = txtRemarks.Text;
                    string Vehicle = ddlAssignedVehicle.Items.Count > 0 ? (ddlAssignedVehicle.SelectedIndex == 0 ? string.Empty : ddlAssignedVehicle.SelectedItem.Text) : string.Empty;
                    OrderDML dml = new OrderDML();
                    if (hfSelectedOrderContainer.Value == string.Empty)
                    {

                        Int64 OrderContainerID = dml.InsertOrderContainerInfo(OrderID, ContainerType, ContainerNo, Weight, CotnainerPickup, CotnainerDropoff, VesselName, Remarks, Vehicle);
                        if (OrderContainerID > 0)
                        {
                            ContainerNotification("Success", "Container added successfully");
                            ClearContainerFields();
                            GetBilties();
                            GetBiltyContainers(OrderID);
                            pnlBiltyContainerInputs.Visible = false;
                        }
                        else
                        {
                            ContainerNotification("Error", "Error updating ");
                        }
                    }
                    else
                    {
                        Int64 OrderContainerID = Convert.ToInt64(hfSelectedOrderContainer.Value);
                        dml.UpdateOrderContainerInfo(OrderContainerID, OrderID, ContainerType, ContainerNo, Weight, CotnainerPickup, CotnainerDropoff, VesselName, Remarks, Vehicle);
                        ContainerNotification("Success", "Conatiner updated successfully");
                        ClearContainerFields();
                        GetBilties();
                        GetBiltyContainers(OrderID);
                        pnlBiltyContainerInputs.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ContainerNotification("Error", "Error saving container, due to: " + ex.Message);
            }
            finally
            {
                modalContainers.Show();
            }
        }

        protected void lnkAddProduct_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlProductItem.SelectedIndex == 0)
                {
                    ProductNotification("Error", "Please select Product");
                    ddlProductItem.Focus();
                }
                else if (txtProductQantity.Text.Trim() == string.Empty)
                {
                    ProductNotification("Error", "Please enter product Quantity");
                    txtProductQantity.Focus();
                }
                else
                {
                    Int64 OrderID = Convert.ToInt64(hfSelectedOrder.Value);
                    string[] ProductString = ddlProductItem.SelectedItem.Text.Split('|');
                    string Product = ProductString[1].Trim();
                    string PackageType = ddlPackageType.SelectedItem.Text;
                    Int64 Quantity = Convert.ToInt64(txtProductQantity.Text);
                    double Weight = txtProductWeight.Text == string.Empty ? 0 : Convert.ToDouble(txtProductWeight.Text);
                    OrderDML dml = new OrderDML();
                    if (hfSelectedProductID.Value == string.Empty)
                    {
                        Int64 OrderProductID = dml.InsertOrderProduct(OrderID, PackageType, Product, Quantity, Weight);
                        if (OrderProductID > 0)
                        {
                            ProductNotification("Success", "Order added successully");
                            ClearProductFields();
                            GetBiltyProducts(OrderID);
                            GetBilties();
                            pnlBiltyProductInputs.Visible = false;
                        }
                    }
                    else
                    {
                        Int64 OrderProductID = Convert.ToInt64(hfSelectedProductID.Value);
                        dml.UpdateOrderProduct(OrderProductID, PackageType, Product, Quantity, Weight);
                        ProductNotification("Success", "Product updated, successfully");
                        ClearProductFields();
                        GetBiltyProducts(OrderID);
                        GetBilties();
                        pnlBiltyProductInputs.Visible = false;
                    }                    
                }
            }
            catch (Exception ex)
            {
                ProductNotification("Error", "Error saving product, due to: " + ex.Message);
            }
            finally
            {
                modalProducts.Show();
            }
        }

        protected void lnkCancelAddingProduct_Click(object sender, EventArgs e)
        {
            try
            {
                ClearProductFields();
                pnlBiltyProductInputs.Visible = false;
            }
            catch (Exception ex)
            {
                ProductNotification("Error", "Error closing Product input panel, due to: " + ex.Message);
            }
            finally
            {
                modalProducts.Show();
            }
        }

        protected void lnkCacnelAddingReceiving_Click(object sender, EventArgs e)
        {
            try
            {
                ClearReceivingFields();
                pnlRecievingInputs.Visible = false;
            }
            catch (Exception ex)
            {
                ReceivingNotification("Error", "Error cancel adding receiving, due to: " + ex.Message);
            }
            finally
            {
                modalRecievings.Show();
            }
        }

        protected void lnkAddReceiving_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtOrderReceivedBy.Text == string.Empty)
                {
                    ReceivingNotification("Error", "Please enter Receiver");
                    txtOrderReceivedBy.Focus();
                }
                else if (txtOrderReceivingDate.Text == string.Empty)
                {
                    ReceivingNotification("Error", "Please enter Receiveiving date");
                    txtOrderReceivingDate.Focus();
                }
                else
                {
                    Int64 OrderID = Convert.ToInt64(hfSelectedOrder.Value);
                    string Receiver = txtOrderReceivedBy.Text.Trim();
                    string Date = txtOrderReceivingDate.Text;
                    string Time = txtOrderReceivingTime.Text;
                    string ReceivingDateTime = Date + " " + Time;
                    OrderDML dml = new OrderDML();

                    if (hfSelectedReceiving.Value == string.Empty)
                    {
                        Int64 OrderReceivingID = dml.InsertOrderReceiving(OrderID, Receiver, ReceivingDateTime);
                        if (OrderReceivingID > 0)
                        {
                            ReceivingNotification("Success", "Receiving inserted successfully");
                            GetBiltyReceiving(OrderID);
                            GetBilties();
                            ClearReceivingFields();
                            pnlRecievingInputs.Visible = false;
                        }
                        else
                        {
                            ReceivingNotification("Error", "Receiving nor inserted, Try Again!!!");
                        }
                    }
                    else
                    {
                        Int64 OrderReceivingID = Convert.ToInt64(hfSelectedReceiving.Value);
                        dml.UpdateOrderReceiving(OrderReceivingID, Receiver, ReceivingDateTime);
                        ReceivingNotification("Success", "Receiving updated successfully");
                        GetBiltyReceiving(OrderID);
                        GetBilties();
                        ClearReceivingFields();
                        pnlRecievingInputs.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ReceivingNotification("Error", "Error submitting data, due to: " + ex.Message);
            }
            finally
            {
                modalRecievings.Show();
            }
        }

        protected void lnkAddReceivingDoc_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlDocumentType.SelectedIndex == 0)
                {
                    ReceivingDocNotification("Error", "Please select document type");
                    ddlDocumentType.Focus();
                }
                else
                {
                    Int64 OrderID = Convert.ToInt64(hfSelectedOrder.Value);
                    string DocumentType = ddlDocumentType.SelectedItem.Text;
                    string DocumentNo = txtDocumentNo.Text;
                    string DocumentName = fuReceivingDocument.HasFile == true ? fuReceivingDocument.FileName : string.Empty;
                    string DocumentPath = Server.MapPath("../assets/Document/Receiving/");

                    //Check whether Directory (Folder) exists.
                    if (!Directory.Exists(DocumentPath))
                    {
                        //If Directory (Folder) does not exists. Create it.
                        Directory.CreateDirectory(DocumentPath);
                    }

                    OrderDML dml = new OrderDML();
                    if (hfSelectedRecievingDocID.Value == string.Empty)
                    {
                        Int64 OrderReceivingDocID = dml.InsertOrderReceivingDocument(OrderID, DocumentType, DocumentNo, DocumentName, DocumentPath);
                        if (OrderReceivingDocID > 0)
                        {
                            if (fuReceivingDocument.HasFile == true)
                            {

                                fuReceivingDocument.SaveAs(DocumentPath + DocumentName);
                            }
                            else
                            {
                                DocumentName = hfReceivingDocumentName.Value;
                            }                      

                            ReceivingDocNotification("Success", "Receiving Doc has been inserted & uploaded");
                            GetBiltyReceivingDocs(OrderID);
                            GetBilties();
                            ClearReceivingDocFields();
                            pnlRecievingDocInputs.Visible = false;
                        }
                        else
                        {
                            ReceivingDocNotification("Error", "Error saving Receiving Doc, Try Again !!!");
                        }
                    }
                    else 
                    {
                        if (fuReceivingDocument.HasFile == true)
                        {

                            fuReceivingDocument.SaveAs(DocumentPath + DocumentName);
                        }
                        else
                        {
                            DocumentName = hfReceivingDocumentName.Value;
                        }
                        Int64 OrderReceivingDocID = Convert.ToInt64(hfSelectedRecievingDocID.Value);
                        dml.UpdateOrderReceivingDocument(OrderReceivingDocID, DocumentType, DocumentNo, DocumentName, DocumentPath);

                        ReceivingDocNotification("Success", "Receiving Doc has been updated & uploaded");
                        ClearReceivingDocFields();
                        GetBiltyReceivingDocs(OrderID);
                        GetBilties();
                        pnlRecievingDocInputs.Visible = false;
                    }                    
                }
            }
            catch (Exception ex)
            {
                ReceivingDocNotification("Error", "Error submitting data, due to: " + ex.Message);
            }
            finally
            {
                modalRecievingDocs.Show();
            }
        }

        protected void lnkCancelAddingReceivingDoc_Click(object sender, EventArgs e)
        {
            try
            {
                pnlRecievingDocInputs.Visible = false;
                ClearReceivingDocFields();
            }
            catch (Exception ex)
            {
                ReceivingDocNotification("Error", "Error cancel adding receiving doc, due to: " + ex.Message);
            }
            finally
            {
                modalRecievingDocs.Show();
            }

        }

        protected void lnkCancelSaveBiltyDamages_Click(object sender, EventArgs e)
        {
            try
            {
                pnlDamageInputs.Visible = false;
                ClearDamageFields();
            }
            catch (Exception ex)
            {
                DamageNotification("Error", "Error cancelling save damage, due to: " + ex.Message);
            }
            finally
            {
                modalDamages.Show();
            }
        }

        protected void lnkSaveBiltyDamages_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlDamageItem.SelectedIndex == 0)
                {
                    DamageNotification("Error", "Please select damaged item");
                    ddlDamageItem.Focus();
                }
                else if (ddlDamageType.SelectedIndex == 0)
                {
                    DamageNotification("Error", "Please select damaged type");
                    ddlDamageType.Focus();
                }
                else if (txtDamageCost.Text == string.Empty)
                {
                    DamageNotification("Error", "Please enter Damage Cost");
                    txtDamageCost.Focus();
                }
                else if (txtDamageCause.Text == string.Empty)
                {
                    DamageNotification("Error", "Please enter Damage Cause");
                    txtDamageCause.Focus();
                }
                else
                {
                    string Item = ddlDamageItem.SelectedItem.Text;
                    string DamageType = ddlDamageType.SelectedItem.Text;
                    double DamageCost = txtDamageCost.Text.Trim() == string.Empty ? 0 : Convert.ToDouble(txtDamageCost.Text.Trim());
                    string DamageCause = txtDamageCause.Text.Trim();
                    string DocumentName = fuDamageDocument.HasFile == true ? fuDamageDocument.FileName : string.Empty;
                    string folderPath = Server.MapPath("../assets/Document/Damage/");
                    Int64 OrderID = Convert.ToInt64(hfSelectedOrder.Value);

                    OrderDML dml = new OrderDML();
                    if (hfSelectedDamageID.Value == string.Empty)
                    {
                        Int64 OrderDamageDocID = dml.InsertOrderDamage(OrderID, Item, DamageType, DamageCost, DamageCause, DocumentName, folderPath);
                        if (OrderDamageDocID > 0)
                        {
                            //Check whether Directory (Folder) exists.
                            if (!Directory.Exists(folderPath))
                            {
                                //If Directory (Folder) does not exists. Create it.
                                Directory.CreateDirectory(folderPath);
                                fuDamageDocument.SaveAs(folderPath + Path.GetFileName(fuDamageDocument.FileName));
                            }                           

                            DamageNotification("Success", "Damage added successfully");
                            pnlDamageInputs.Visible = false;
                            ClearDamageFields();

                            GetBiltyDamages(OrderID);
                            GetBilties();
                        }
                    }
                    else
                    {
                        Int64 OrderDamageID = Convert.ToInt64(hfSelectedDamageID.Value);
                        DocumentName = fuDamageDocument.HasFile == true ? fuDamageDocument.FileName : hfDamageDocument.Value;
                        dml.UpdateOrderDamage(OrderDamageID, Item, DamageType, DamageCost, DamageCause, DocumentName, folderPath);
                        if (!Directory.Exists(folderPath))
                        {
                            //If Directory (Folder) does not exists. Create it.
                            Directory.CreateDirectory(folderPath);
                            fuDamageDocument.SaveAs(folderPath + Path.GetFileName(fuDamageDocument.FileName));
                        }
                        DamageNotification("Success", "Damage updated successfully");
                        ClearDamageFields();

                        GetBiltyDamages(OrderID);
                        GetBilties();
                    }
                    
                }                
            }
            catch (Exception ex)
            {
                DamageNotification("Error", "Error saving Bilty Damage, due to: " + ex.Message);
            }
        }

        protected void lnkSaveBilty_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSenderCompany.Text.Trim() == string.Empty)
                {
                    BiltyNotification("Error", "Please enter Sender Company");
                    txtSenderCompany.Focus();
                }
                else if (txtReceiverCompany.Text.Trim() == string.Empty)
                {
                    BiltyNotification("Error", "Please enter Receiver Company");
                    txtReceiverCompany.Focus();
                }
                else if (txtCustomerCompany.Text.Trim() == string.Empty)
                {
                    BiltyNotification("Error", "Please enter Customer Company");
                    txtCustomerCompany.Focus();
                }
                else if (ddlBillingType.SelectedIndex == 0)
                {
                    BiltyNotification("Error", "Please select Payment Type");
                    txtCustomerCompany.Focus();
                }
                else if (txtLoadingDate.Text.Trim() == string.Empty)
                {
                    BiltyNotification("Error", "Please enter Loading Date");
                    txtLoadingDate.Focus();
                }
                else if (txtBiltyFreight.Text.Trim() == string.Empty)
                {
                    BiltyNotification("Error", "Please enter bilty freight");
                    txtBiltyFreight.Focus();
                }
                else
                {
                    Int64 OrderID = Convert.ToInt64(hfSelectedOrder.Value);
                    Int64 BiltyNo = txtBiltyNo.Text == string.Empty ? rnd.Next(0, 999999) : Convert.ToInt64(txtBiltyNo.Text);

                    Int64 SenderCompanyID = 0;
                    Int64 ReceiverCompanyID = 0;
                    Int64 CustomerCompanyID = 0;

                    CompanyDML dmlCompany = new CompanyDML();
                    string[] SenderCompanyString = ddlSearchSender.SelectedItem.Text.Split('|');
                    DataTable dtSenderCompany = dmlCompany.GetCompanyByCode(SenderCompanyString[0].ToString().Trim());
                    SenderCompanyID = Convert.ToInt64(dtSenderCompany.Rows[0]["CompanyID"].ToString());

                    string[] ReceiverCompanyString = ddlSearchReceiver.SelectedItem.Text.Split('|');
                    DataTable dtReceiverCompany = dmlCompany.GetCompanyByCode(ReceiverCompanyString[0].ToString().Trim());
                    ReceiverCompanyID = Convert.ToInt64(dtReceiverCompany.Rows[0]["CompanyID"].ToString());

                    string[] CustomerCompanyString = ddlSearchCustomer.SelectedItem.Text.Split('|');
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
                    string[] PickupLocationString = ddlSearchPickLocation.Text.Split('|');
                    LocationDML dmlLocation = new LocationDML();
                    DataTable dtLocation = dmlLocation.GetLocationByCode(PickupLocationString[4].ToString().Trim());
                    if (dtLocation.Rows.Count > 0)
                    {
                        PickupLocationID = Convert.ToInt64(dtLocation.Rows[0]["PickDropID"].ToString());
                    }

                    string[] DropoffLocationString = ddlSearchDropLocation.Text.Split('|');
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

                    dml.UpdateOrder(OrderID, BiltyNo, BiltyDate, SenderCompanyID, SenderDepartment, ReceiverCompanyID, ReceiverDepartment, CustomerCompanyID,
                        CustomerDepartment, PaymentType, PickupLocationID, DropoffLocationID, ClearingAgent, AdditionalWeight, ActualWeight, BiltyFreight, Freight,
                        PartyCommission, AdvanceFreight, FactoryAdvance, DieselAdvance, AdvanceAmount, TotalAdvance, BalanceFreight, LoginID);

                    ClearBiltyFields();
                    GetBilties();
                    modalBilty.Hide();
                    
                }
            }
            catch (Exception ex)
            {
                BiltyNotification("Error", "Error saving bilty, due to: " + ex.Message);
                modalBilty.Show();
            }
        }

        protected void lnkCancelSaveBilty_Click(object sender, EventArgs e)
        {

        }

        protected void lnkCloseBiltys_Click(object sender, EventArgs e)
        {
            try
            {
                hfSelectedOrder.Value = string.Empty;
                modalBilty.Hide();
            }
            catch (Exception ex)
            {
                BiltyNotification("Error", "Error closing bilty popup, due to: " + ex.Message);
                modalBilty.Show();
            }
        }

        #endregion

        #region Dropdown's Events

        protected void ddlProductItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlProductItem.SelectedIndex == 0)
                {
                    ProductNotification("Error", "Please select Product.");
                    ddlProductItem.Focus();
                }
                else
                {
                    string[] ProductString = ddlProductItem.SelectedItem.Text.Split('|');
                    string Code = ProductString[0].ToString().Trim();
                    string Product = ProductString[1].ToString().Trim();
                    string PackageType = ProductString[2].ToString().Trim();
                    string Weight = ProductString[3].ToString().Trim();

                    ddlPackageType.ClearSelection();
                    ddlPackageType.Items.FindByText(PackageType).Selected = true;
                    txtProductWeight.Text = Weight.ToString();
                }
            }
            catch (Exception ex)
            {
                ProductNotification("Error", "Error selecting item, due to: " + ex.Message);
            }
            finally
            {
                modalProducts.Show();
            }
        }

        protected void ddlSearchPickLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlSearchPickLocation.SelectedIndex == 0)
                {
                    notification("Error", "Please select Pickup locations");
                    ddlSearchPickLocation.Focus();

                    txtPickCity.Text = string.Empty;
                    txtPickRegion.Text = string.Empty;
                    txtPickArea.Text = string.Empty;
                    txtPickAddress.Text = string.Empty;
                }
                else
                {
                    string[] Locations = ddlSearchPickLocation.SelectedItem.Text.Split('|');
                    if (Locations.Length > 0)
                    {
                        //Code, Group, Company, Department
                        txtPickCity.Text = Locations[0].ToString().Trim();
                        txtPickRegion.Text = Locations[1].ToString().Trim();
                        txtPickArea.Text = Locations[2].ToString().Trim();
                        txtPickAddress.Text = Locations[3].ToString().Trim();
                    }
                    else
                    {
                        txtPickCity.Text = string.Empty;
                        txtPickRegion.Text = string.Empty;
                        txtPickArea.Text = string.Empty;
                        txtPickAddress.Text = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error searching Pick Locations, due to: " + ex.Message);
            }
            finally
            {
                modalBilty.Show();
            }
        }

        protected void ddlSearchDropLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlSearchDropLocation.SelectedIndex == 0)
                {
                    notification("Error", "Please Drop location");
                    ddlSearchDropLocation.Focus();

                    txtDropCity.Text = string.Empty;
                    txtDropRegion.Text = string.Empty;
                    txtDropArea.Text = string.Empty;
                    txtDropAddress.Text = string.Empty;
                }
                else
                {
                    string[] Locations = ddlSearchDropLocation.SelectedItem.Text.Split('|');
                    if (Locations.Length > 0)
                    {
                        //Code, Group, Company, Department
                        txtDropCity.Text = Locations[0].ToString().Trim();
                        txtDropRegion.Text = Locations[1].ToString().Trim();
                        txtDropArea.Text = Locations[2].ToString().Trim();
                        txtDropAddress.Text = Locations[3].ToString().Trim();
                    }
                    else
                    {
                        txtDropCity.Text = string.Empty;
                        txtDropRegion.Text = string.Empty;
                        txtDropArea.Text = string.Empty;
                        txtDropAddress.Text = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error selecting Drop Locations, due to: " + ex.Message);
            }
            finally
            {
                modalBilty.Show();
            }
        }


        #endregion

        #region TextBox Text Changes

        protected void txtFreight_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtPartyCommission.Text = CalculatePartyCommission().ToString();
            }
            catch (Exception ex)
            {
                BiltyNotification("Error", "Error calculating party commission, due to: " + ex.Message);
            }
            finally
            {
                modalBilty.Show();
            }

            try
            {
                txtBalanceFreight.Text = CalculateBalanceFreight().ToString();
            }
            catch (Exception ex)
            {
                BiltyNotification("Error", "Error calculating party commission, due to: " + ex.Message);
            }
            finally
            {
                modalBilty.Show();
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
                BiltyNotification("Error", "Error calculating party commission, due to: " + ex.Message);
            }
            finally
            {
                modalBilty.Show();
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
                BiltyNotification("Error", "Error calculating total advance, due to: " + ex.Message);
            }
            finally
            {
                modalBilty.Show();
            }


            try
            {
                txtBalanceFreight.Text = CalculateBalanceFreight().ToString();
            }
            catch (Exception ex)
            {
                BiltyNotification("Error", "Error calculating party commission, due to: " + ex.Message);
            }
            finally
            {
                modalBilty.Show();
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
                BiltyNotification("Error", "Error calculating party commission, due to: " + ex.Message);
            }
            finally
            {
                modalBilty.Show();
            }
        }

        #endregion

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

        #endregion
    }
} 