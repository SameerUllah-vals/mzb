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
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiltySystem.Bilty
{
    public partial class Search : System.Web.UI.Page
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

        private string BiltiesSortDirection
        {
            get { return ViewState["SortDirection"] != null ? ViewState["SortDirection"].ToString() : "ASC"; }
            set { ViewState["SortDirection"] = value; }
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
            AdvancesNotification();
            Advances2Notification();
            if (!IsPostBack)
            {
                this.Title = "Search Bilties";
                try
                {
                    GetBilties();

                    GetAndBindPatrolPumps("DESC");

                    //Getting/Populating Vehicle
                    //try
                    //{
                    //    VehicleDML dml = new VehicleDML();
                    //    DataTable dtVehicle = dml.GetVehicle();
                    //    if (dtVehicle.Rows.Count > 0)
                    //    {
                    //        FillDropDown(dtVehicle, ddlVehicleNo, "VehicleID", "RegNo", "- Select-");
                    //    }

                    //}
                    //catch (Exception ex)
                    //{
                    //    notification("Error", "Error getting/populating Vehicle , due to: " + ex.Message);
                    //}

                    //Getting/Populating Clearing agents
                    //try
                    //{
                    //    ClearingAgentDML dml = new ClearingAgentDML();
                    //    DataTable dtAgents = dml.GetClearingAgents();
                    //    if (dtAgents.Rows.Count > 0)
                    //    {
                    //        FillDropDown(dtAgents, ddlClearingAgent, "ID", "Name", "-Select Agent-");
                    //    }
                    //}
                    //catch (Exception ex)
                    //{
                    //    notification("Error", "Error getting/populating clearing agents, due to: " + ex.Message);
                    //}
                    //Getting/Populating Shipping Lines
                    try
                    {
                        OrderDML dml = new OrderDML();
                        DataTable dtShippingLines = dml.GetActiveShippingLine();
                        if (dtShippingLines.Rows.Count > 0)
                        {
                            FillDropDown(dtShippingLines, ddlShippingLine, "ShippingLineID", "Name", "- Select-");
                        }
                    }
                    catch (Exception ex)
                    {
                        notification("Error", "Error getting/populating Damage Type, due to: " + ex.Message);
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

                    //Getting/Binding CustomerCompanies
                    try
                    {
                        CompanyDML dml = new CompanyDML();
                        DataTable dtCompany = dml.GetCompany();
                        if (dtCompany.Rows.Count > 0)
                        {
                            FillDropDown(dtCompany, ddlBuyer, "CompanyID", "CompanyName", "- Select -");
                        }
                    }
                    catch (Exception ex)
                    {
                        notification("Error", "Error getting/binding Companies, due to: " + ex.Message);
                    }

                    //Getting/Binding Own Companies
                    try
                    {
                        OwnCompanyDML dml = new OwnCompanyDML();
                        DataTable dtCompany = dml.GetCompany();
                        if (dtCompany.Rows.Count > 0)
                        {
                            FillDropDown(dtCompany, ddlSuppliers, "CompanyID", "CompanyName", "- Select -");

                            ddlSuppliers.ClearSelection();
                            ddlSuppliers.Items.FindByText("MZB").Selected = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        notification("Error", "Error getting/binding Buyers, due to: " + ex.Message);
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
                        DataTable dtPickUpLocation = dmlBilty.GetPickDropLocation();
                        FillDropDown(dtPickUpLocation, ddlContainerPickup, "CityID", "Location", "- Select -");

                        DataTable dtDropOffLocation = dmlBilty.GetPickDropLocation();
                        FillDropDown(dtDropOffLocation, ddlContainerDropoff, "CityID", "Location", "- Select -");
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

                    //Gettting/Populating Expense Types
                    try
                    {
                        OrderDML dml = new OrderDML();
                        DataTable dtExpenseType = dml.GetExpenseTypes();
                        FillDropDown(dtExpenseType, ddlExpenseType, "ExpensesTypeID", "ExpensesTypeName", "- Select -");
                    }
                    catch (Exception ex)
                    {
                        notification("Error", "Error getting/populating Expense Types, due to: " + ex.Message);
                    }
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

        public void ContExpenseNotification()
        {
            try
            {
                divContExpensesNotification.InnerHtml = string.Empty;
            }
            catch (Exception ex)
            {
                divContExpensesNotification.InnerHtml = "<div class=\"alert alert-danger\">" + ex.Message + "</div>";
            }
        }

        public void ContExpenseNotification(string type, string msg)
        {
            try
            {
                if (type == "Error")
                {
                    divContExpensesNotification.InnerHtml = "<div class=\"alert alert-danger\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                }
                else if (type == "Success")
                {
                    divContExpensesNotification.InnerHtml = "<div class=\"alert alert-success\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                }
                else if (type == string.Empty)
                {
                    divContExpensesNotification.InnerHtml = string.Empty;
                }
            }
            catch (Exception ex)
            {
                divContExpensesNotification.InnerHtml = "<div class=\"alert alert-danger\">" + ex.Message + "</div>";
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

        public void InvoiceNotification()
        {
            try
            {
                divInvoiceNotification.InnerHtml = string.Empty;
            }
            catch (Exception ex)
            {
                divInvoiceNotification.InnerHtml = "<div class=\"alert alert-danger\">" + ex.Message + "</div>";
            }
        }

        public void InvoiceNotification(string type, string msg)
        {
            try
            {
                if (type == "Error")
                {
                    divInvoiceNotification.InnerHtml = "<div class=\"alert alert-danger\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                }
                else if (type == "Success")
                {
                    divInvoiceNotification.InnerHtml = "<div class=\"alert alert-success\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                }
                else if (type == string.Empty)
                {
                    divInvoiceNotification.InnerHtml = string.Empty;
                }
            }
            catch (Exception ex)
            {
                divInvoiceNotification.InnerHtml = "<div class=\"alert alert-danger\">" + ex.Message + "</div>";
            }
        }

        public void AdvancesNotification()
        {
            try
            {
                divAdvancesNotification.InnerHtml = string.Empty;
            }
            catch (Exception ex)
            {
                divAdvancesNotification.InnerHtml = "<div class=\"alert alert-danger\">" + ex.Message + "</div>";
            }
        }

        public void AdvancesNotification(string type, string msg)
        {
            try
            {
                if (type == "Error")
                {
                    divAdvancesNotification.InnerHtml = "<div class=\"alert alert-danger\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                }
                else if (type == "Success")
                {
                    divAdvancesNotification.InnerHtml = "<div class=\"alert alert-success\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                }
                else if (type == string.Empty)
                {
                    divAdvancesNotification.InnerHtml = string.Empty;
                }
            }
            catch (Exception ex)
            {
                divAdvancesNotification.InnerHtml = "<div class=\"alert alert-danger\">" + ex.Message + "</div>";
            }
        }

        public void Advances2Notification()
        {
            try
            {
                divAdvances2Notification.InnerHtml = string.Empty;
            }
            catch (Exception ex)
            {
                divAdvances2Notification.InnerHtml = "<div class=\"alert alert-danger\">" + ex.Message + "</div>";
            }
        }

        public void Advances2Notification(string type, string msg)
        {
            try
            {
                if (type == "Error")
                {
                    divAdvances2Notification.InnerHtml = "<div class=\"alert alert-danger\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                }
                else if (type == "Success")
                {
                    divAdvances2Notification.InnerHtml = "<div class=\"alert alert-success\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                }
                else if (type == string.Empty)
                {
                    divAdvances2Notification.InnerHtml = string.Empty;
                }
            }
            catch (Exception ex)
            {
                divAdvances2Notification.InnerHtml = "<div class=\"alert alert-danger\">" + ex.Message + "</div>";
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
                notification("Error", "Error getting bilty veehicles, due to: " + ex.Message);
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

        public void GetBilties(string sortExpression = "")
        {
            try
            {
                OrderDML dml = new OrderDML();
                DataTable dtBilties = new DataTable();
                if (txtKeyword.Text == string.Empty && txtSearchDateFrom.Text ==string.Empty && txtSearchDateTo.Text == string.Empty)
                {
                    dtBilties = dml.GetBilties();
                }
                else
                {
                    string Keyword = txtKeyword.Text;

                    string Query = string.Empty;

                    
                    if (txtSearchDateFrom.Text != string.Empty || txtSearchDateTo.Text != string.Empty)
                    {
                        Query += " AND ";
                        if (txtSearchDateFrom.Text != string.Empty && txtSearchDateTo.Text == string.Empty)
                        {
                            Query += "Convert(date, o.Date) = CONVERT(date, '" + txtSearchDateFrom.Text + "') AND ";
                        }
                        else if (txtSearchDateFrom.Text == string.Empty && txtSearchDateTo.Text != string.Empty)
                        {
                            Query += "Convert(date, o.Date) = CONVERT(date, '" + txtSearchDateTo.Text + "') AND ";
                        }
                        else if (txtSearchDateFrom.Text != string.Empty && txtSearchDateTo.Text != string.Empty)
                        {
                            Query += "Convert(date, o.Date) BETWEEN CONVERT(date, '" + txtSearchDateFrom.Text + "') AND CONVERT(date, '" + txtSearchDateTo.Text + "') AND ";
                        }
                        Query = Query.Remove(Query.Length - 5);
                    }
                    
                    dtBilties = dml.GetBiltiesNew(Keyword, Query);
                }

                if (dtBilties.Rows.Count > 0)
                {
                    gvBilty.DataSource = dtBilties;
                    if (BiltiesSortDirection != null)
                    {
                        DataView dv = dtBilties.AsDataView();
                        this.BiltiesSortDirection = this.BiltiesSortDirection == "ASC" ? "DESC" : "ASC";
                        if (sortExpression != string.Empty)
                        {
                            dv.Sort = sortExpression + " " + this.BiltiesSortDirection;
                        }
                        
                        gvBilty.DataSource = dv;
                    }
                    else
                    {
                        gvBilty.DataSource = dtBilties;
                    }
                }
                else
                {

                    gvBilty.DataSource = dtBilties;
                }
                lblTotalCount.Text = "Rows count: " + dtBilties.Rows.Count.ToString();
                gvBilty.DataBind();
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting/binding bilties, due to: " + ex.Message);
            }
        }

        #endregion

        public void ConfirmModal(string Title, string Action)
        {
            try
            {
                modalConfirm.Show();
                lblModalTitle.Text = Title;
                hfConfirmAction.Value = Action;
                lblConfirmAction.Text = Action;

            }
            catch (Exception ex)
            {
                notification("Error", "Error confirming, due to: " + ex.Message);
            }
        }

        public void ClearAdvancesFields()
        {
            try
            {
                rbAdvanceTypes.ClearSelection();
                txtAdvanceAmount.Text = string.Empty;
                ddlPatrolPumps.ClearSelection();
                txtAdvanceVehicleFrom.Text = string.Empty;
                txtPatrolRate.Text = string.Empty;
                txtPatrolLitre.Text = string.Empty;

                PatrolPumpAdvancePlaceholder.Visible = false;
                VehicleAdvancePlaceholder.Visible = false;
            }
            catch (Exception ex)
            {
                Advances2Notification("Error", "Error clearing advances, due to: " + ex.Message);
            }
            finally
            {
                modalAdvances2.Show();
            }
        }

        public void ClearDriverVehicleFields()
        {
            try
            {
                ddlVehicleType.ClearSelection();
                txtVehicleRegNo.Text = string.Empty;
               // ddlVehicleNo.ClearSelection();
                txtVehicleContactNo.Text = string.Empty;
                ddlBroker.ClearSelection();
                txtDriverName.Text = string.Empty;
                txtDriverfather.Text = string.Empty; ;
                txtDriverNIC.Text = string.Empty;
                txtDriverLicense.Text = string.Empty;
                txtDriverContactNo.Text = string.Empty;
                txtVehicleRate.Text = string.Empty;
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
                txtContainerRate.Text = string.Empty;
                txtContainerRemarks.Text = string.Empty;
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

                ddlShippingLine.ClearSelection();

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

        //public void AddAccountDebit(Int64 CompanyID, string Description, double Amount, string Account)
        //{
        //    try
        //    {
        //        AccountsDML dmlAccounts = new AccountsDML();
        //        DataTable dtAccount = dmlAccounts.GetInAccounts(Account);
        //        double AccountPreviousBalance = dtAccount.Rows.Count > 0 ? Convert.ToDouble(dtAccount.Rows[0]["Balance"].ToString().Replace("&nbsp;", "0")) : 0;
        //        double NewBalance = Amount + AccountPreviousBalance;
        //        Int64 CompanyAccountID = dmlAccounts.InsertInAccount(Account, CompanyID, Description, Amount, 0, NewBalance, LoginID);
        //        if (CompanyAccountID <= 0)
        //        {
        //            notification("Error", "Account didnt mentained, check Account '" + Account + "' and contact IT Team");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notification("Error", "Error adding debit for General ledger, due to: " + ex.Message);
        //    }
        //}

        //public void AddAccountCredit(Int64 CompanyID, string Description, double Amount, string Account)
        //{
        //    try
        //    {
        //        AccountsDML dmlAccounts = new AccountsDML();
        //        DataTable dtAccount = dmlAccounts.GetInAccounts(Account);
        //        double AccountPreviousBalance = dtAccount.Rows.Count > 0 ? Convert.ToDouble(dtAccount.Rows[0]["Balance"].ToString().Replace("&nbsp;", "0")) : 0;
        //        double NewBalance = AccountPreviousBalance - Amount;
        //        Int64 CompanyAccountID = dmlAccounts.InsertInAccount(Account, CompanyID, Description, 0, Amount, NewBalance, LoginID);
        //        if (CompanyAccountID <= 0)
        //        {
        //            notification("Error", "Account didnt mentained, check Account '" + Account + "' and contact IT Team");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notification("Error", "Error adding Credit for company ledger, due to: " + ex.Message);
        //    }
        //}

        public string GetBrokerCodeByID(Int64 BrokerID)
        {
            try
            {
                BrokersDML dml = new BrokersDML();
                DataTable dtBroker = dml.GetBroker(BrokerID);
                return dtBroker.Rows[0]["Code"].ToString().Trim();
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting Broker Code by ID, due to: " + ex.Message);
                return string.Empty;
            }
        }

        public string GetOrderNoOrderID(Int64 OrderID)
        {
            try
            {
                OrderDML dml = new OrderDML();
                DataTable dtOrder = dml.GetBilty(OrderID);
                return dtOrder.Rows[0]["OrderNo"].ToString().Trim();
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting Broker Code by ID, due to: " + ex.Message);
                return string.Empty;
            }
        }

        public void GetContainerExpense(Int64 SelectedContainerId)
        {
            OrderDML dml = new OrderDML();
            DataTable dtExpense = dml.GetExpenses(SelectedContainerId);
            if (dtExpense.Rows.Count > 0)
            {
                gvContainerExpense.DataSource = dtExpense;
            }
            else
            {
                gvContainerExpense.DataSource = null;
            }
            gvContainerExpense.DataBind();
        }

        public void GetInvoice(string InvoiceNo)
        {
            try
            {
                OrderDML dml = new OrderDML();
                DataTable dtInvoice = dml.GetInvoice(InvoiceNo);
                if (dtInvoice.Rows.Count > 0)
                {
                    Int64 OrderID = Convert.ToInt64(dtInvoice.Rows[0]["OrderID"]);
                    double TotalFreight = 0;
                    DataTable dtSavedExpenses = new DataTable();
                    dtSavedExpenses.Columns.Add("Type");
                    dtSavedExpenses.Columns.Add("Qty");
                    dtSavedExpenses.Columns.Add("Rate");
                    dtSavedExpenses.Columns.Add("Amount");
                    DateTime InvDate = Convert.ToDateTime(dtInvoice.Rows[0]["CreatedDate"]);
                    DateTime OrdDate = Convert.ToDateTime(dtInvoice.Rows[0]["OrderDate"]);
                    lblPrintInvoieno.Text = dtInvoice.Rows[0]["InvoiceNo"].ToString();
                    lblPrintInvoiceDate.Text = InvDate.ToString("dd-MMM-yyy");
                    lblPrintInvoiceCsutomer.Text = dtInvoice.Rows[0]["CustomerCompany"].ToString();
                    lblPrintInvoiceRemarks.Text = string.Empty;

                    tblPrintInvoice.InnerHtml = string.Empty;
                    tblPrintInvoice.InnerHtml += "<tr>";
                        tblPrintInvoice.InnerHtml += "<td>&nbsp;</td>";
                        tblPrintInvoice.InnerHtml += "<td>";
                            tblPrintInvoice.InnerHtml += "<table style=\"width: 100%\">";
                                tblPrintInvoice.InnerHtml += "<tr>";
                                    tblPrintInvoice.InnerHtml += "<td style=\"width: 30%; text-align: left; border: none;\">From</td>";
                                    tblPrintInvoice.InnerHtml += "<td style=\"width: 70%; text-align: left; border: none;\">" + dtInvoice.Rows[0]["EmptyContainerPickLocation"] + "</td>";
                                tblPrintInvoice.InnerHtml += "</tr>";
                                tblPrintInvoice.InnerHtml += "<tr>";
                                    tblPrintInvoice.InnerHtml += "<td style=\"width: 30%; text-align: left; border: none;\">To</td>";
                                    tblPrintInvoice.InnerHtml += "<td style=\"width: 70%; text-align: left; border: none;\">" + dtInvoice.Rows[0]["EmptyContainerDropLocation"] + "</td>";
                                tblPrintInvoice.InnerHtml += "</tr>";
                                tblPrintInvoice.InnerHtml += "<tr>";
                                    tblPrintInvoice.InnerHtml += "<td style=\"width: 30%; text-align: left; border: none;\">Bilty Number</td>";
                                    tblPrintInvoice.InnerHtml += "<td style=\"width: 70%; text-align: left; border: none;\">" + dtInvoice.Rows[0]["OrderNo"] + "</td>";
                                tblPrintInvoice.InnerHtml += "</tr>";
                                tblPrintInvoice.InnerHtml += "<tr>";
                                    tblPrintInvoice.InnerHtml += "<td style=\"width: 30%; text-align: left; border: none;\">Bilty Date</td>";
                                    tblPrintInvoice.InnerHtml += "<td style=\"width: 70%; text-align: left; border: none;\">" + OrdDate.ToString("dd-MMM-yyy") + "</td>";
                                tblPrintInvoice.InnerHtml += "</tr>";
                                tblPrintInvoice.InnerHtml += "<tr>";
                                    tblPrintInvoice.InnerHtml += "<td style=\"width: 30%; text-align: left; border: none;\">Truck Number</td>";
                                    tblPrintInvoice.InnerHtml += "<td style=\"width: 70%; text-align: left; border: none;\">" + dtInvoice.Rows[0]["VehicleRegNo"] + "</td>";
                                tblPrintInvoice.InnerHtml += "</tr>";
                                tblPrintInvoice.InnerHtml += "<tr>";
                                    tblPrintInvoice.InnerHtml += "<td style=\"width: 30%; text-align: left; border: none;\">Containers Qty</td>";
                                    tblPrintInvoice.InnerHtml += "<td style=\"width: 70%; text-align: left; border: none;\">" + dtInvoice.Rows.Count + "</td>";
                                tblPrintInvoice.InnerHtml += "</tr>";
                                tblPrintInvoice.InnerHtml += "<tr>";
                                    tblPrintInvoice.InnerHtml += "<td style=\"width: 30%; text-align: left; border: none;\">Description</td>";
                                    tblPrintInvoice.InnerHtml += "<td style=\"width: 70%; text-align: left; border: none;\">";
                                    foreach (DataRow _drInvoice in dtInvoice.Rows)
                                    {
                                        tblPrintInvoice.InnerHtml += _drInvoice["ContainerNo"] + ", ";
                                    }
                                    tblPrintInvoice.InnerHtml = tblPrintInvoice.InnerHtml.Remove(tblPrintInvoice.InnerHtml.Length - 2);

                                    tblPrintInvoice.InnerHtml += "</td>";
                                tblPrintInvoice.InnerHtml += "</tr>";
                            tblPrintInvoice.InnerHtml += "</table>";
                        tblPrintInvoice.InnerHtml += "</td>";
                        tblPrintInvoice.InnerHtml += "<td>&nbsp;</td>";
                        tblPrintInvoice.InnerHtml += "<td>&nbsp;</td>";
                        tblPrintInvoice.InnerHtml += "<td>&nbsp;</td>";
                    tblPrintInvoice.InnerHtml += "</tr>";

                    foreach (DataRow _drInvoice in dtInvoice.Rows)
                    {
                        TotalFreight = TotalFreight + Convert.ToDouble(_drInvoice["Rate"]);
                        //Int64 ContainerID = Convert.ToInt64(_drInvoice["OrderConsignmentID"]);
                        //DataTable dtContainerExpenses = dml.GetExpenses(ContainerID);
                        //if (dtContainerExpenses.Rows.Count > 0)
                        //{
                        //    foreach (DataRow _drExpenses in dtContainerExpenses.Rows)
                        //    {
                        //        if (dtSavedExpenses.Rows.Count > 0)
                        //        {
                        //            DataRow[] _drSavedExpenses = dtSavedExpenses.Select("Type = '" + _drExpenses["ExpensesTypeName"].ToString() + "'");

                        //            if (_drSavedExpenses.Length > 0)
                        //            {
                        //                int SavedIndex = dtSavedExpenses.Rows.IndexOf(_drSavedExpenses[0]);
                        //                //int SavedIndex = dtSavedExpenses.Rows.IndexOf(_drSavedExpenses[0]);
                        //                //dtSavedExpenses.Rows[SavedIndex]["Qty"] = (Convert.ToInt64(dtSavedExpenses.Rows[SavedIndex]["Qty"]) + 1).ToString();
                        //                //dtSavedExpenses.Rows[SavedIndex]["Amount"] = (Convert.ToDouble(dtSavedExpenses.Rows[SavedIndex]["Amount"]) + Convert.ToDouble(_drExpenses["Amount"])).ToString();
                        //                bool AmountAdded = false;
                        //                for (int i = 0; i < _drSavedExpenses.Length; i++)
                        //                {
                                            
                        //                    if (_drSavedExpenses[i]["Rate"].ToString() == _drExpenses["Amount"].ToString())
                        //                    {
                        //                        AmountAdded = true;
                        //                        SavedIndex = i;
                        //                    }
                        //                }

                        //                if (AmountAdded == true)
                        //                {
                        //                    //int SavedIndex = dtSavedExpenses.Rows.IndexOf(_drSavedExpenses[0]);
                        //                    dtSavedExpenses.Rows[SavedIndex]["Qty"] = (Convert.ToInt64(dtSavedExpenses.Rows[SavedIndex]["Qty"]) + 1).ToString();
                        //                    dtSavedExpenses.Rows[SavedIndex]["Amount"] = (Convert.ToDouble(dtSavedExpenses.Rows[SavedIndex]["Amount"]) + Convert.ToDouble(_drExpenses["Amount"])).ToString();
                        //                }
                        //                else
                        //                {
                        //                    dtSavedExpenses.Rows.Add(_drExpenses["ExpensesTypeName"].ToString(), "1", _drExpenses["Amount"].ToString(), _drExpenses["Amount"].ToString().Replace(" ", string.Empty));
                        //                }
                                        
                        //            }
                        //            else
                        //            {
                        //                dtSavedExpenses.Rows.Add(_drExpenses["ExpensesTypeName"].ToString(), "1", _drExpenses["Amount"].ToString(), _drExpenses["Amount"].ToString().Replace(" ", string.Empty));
                        //            }
                        //        }
                        //        else
                        //        {
                        //            dtSavedExpenses.Rows.Add(_drExpenses["ExpensesTypeName"].ToString(), "1", _drExpenses["Amount"].ToString(), _drExpenses["Amount"].ToString().Replace(" ", string.Empty));
                        //        }                                
                        //    }                            
                        //}
                    }

                    tblPrintInvoice.InnerHtml += "<tr>";
                        tblPrintInvoice.InnerHtml += "<td>&nbsp;</td>";
                        tblPrintInvoice.InnerHtml += "<td>Freight Charges</td>";
                        tblPrintInvoice.InnerHtml += "<td>" + dtInvoice.Rows.Count + "</td>";
                        tblPrintInvoice.InnerHtml += "<td>" + dtInvoice.Rows[0]["Rate"] + "</td>";
                        tblPrintInvoice.InnerHtml += "<td>" + TotalFreight + "</td>";
                    tblPrintInvoice.InnerHtml += "</tr>";

                    DataTable dtExpenses = dml.GetExpenses_Breakup(OrderID);

                    if (dtExpenses.Rows.Count > 0)
                    {
                        foreach (DataRow _drExpenses in dtExpenses.Rows)
                        {
                            tblPrintInvoice.InnerHtml += "<tr>";
                            tblPrintInvoice.InnerHtml += "<td>&nbsp;</td>";
                            tblPrintInvoice.InnerHtml += "<td>" + _drExpenses["Type"].ToString() + "</td>";
                            tblPrintInvoice.InnerHtml += "<td>" + _drExpenses["Qty"].ToString() + "</td>";
                            tblPrintInvoice.InnerHtml += "<td>" + _drExpenses["Rate"].ToString() + "</td>";
                            tblPrintInvoice.InnerHtml += "<td>" + _drExpenses["Total"].ToString() + "</td>";
                            tblPrintInvoice.InnerHtml += "</tr>";
                            //TotalFreight += Convert.ToDouble(_drExpenses["Amount"]);
                        }
                        
                    }

                    lblPrintInvoiceToal.Text = dtInvoice.Rows[0]["Total"].ToString();
                }

                modalInvoicePrint.Show();
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting invoice, due to: " + ex.Message);
            }
        }

        public void ShowInvoice(string InvoiceNo)
        {
            try
            {
                OrderDML dml = new OrderDML();
                DataTable dtInvoice = dml.GetInvoice(InvoiceNo);
                if (dtInvoice.Rows.Count > 0)
                {
                    lblPrintInvoieno.Text = dtInvoice.Rows[0]["InvoiceNo"].ToString();
                    lblPrintInvoiceDate.Text = dtInvoice.Rows[0]["CreatedDate"].ToString();
                    lblPrintInvoiceCsutomer.Text = dtInvoice.Rows[0]["CustomerCompany"].ToString();
                    lblPrintInvoiceRemarks.Text = string.Empty;

                    tblPrintInvoice.InnerHtml = string.Empty;
                    tblPrintInvoice.InnerHtml += "<tr>";
                    tblPrintInvoice.InnerHtml += "<td>&nbsp;</td>";
                    tblPrintInvoice.InnerHtml += "<td>";
                    tblPrintInvoice.InnerHtml += "<table style=\"width: 100%\">";
                    tblPrintInvoice.InnerHtml += "<tr>";
                    tblPrintInvoice.InnerHtml += "<td style=\"width: 30%;\">From</td>";
                    tblPrintInvoice.InnerHtml += "<td style=\"width: 70%;\">" + dtInvoice.Rows[0]["EmptyContainerPickLocation"] + "</td>";
                    tblPrintInvoice.InnerHtml += "</tr>";
                    tblPrintInvoice.InnerHtml += "<tr>";
                    tblPrintInvoice.InnerHtml += "<td style=\"width: 30%;\">To</td>";
                    tblPrintInvoice.InnerHtml += "<td style=\"width: 70%;\">" + dtInvoice.Rows[0]["EmptyContainerDropLocation"] + "</td>";
                    tblPrintInvoice.InnerHtml += "</tr>";
                    tblPrintInvoice.InnerHtml += "<tr>";
                    tblPrintInvoice.InnerHtml += "<td style=\"width: 30%;\">Bilty Number</td>";
                    tblPrintInvoice.InnerHtml += "<td style=\"width: 70%;\">" + dtInvoice.Rows[0]["OrderNo"] + "</td>";
                    tblPrintInvoice.InnerHtml += "</tr>";
                    tblPrintInvoice.InnerHtml += "<tr>";
                    tblPrintInvoice.InnerHtml += "<td style=\"width: 30%;\">Bilty Date</td>";
                    tblPrintInvoice.InnerHtml += "<td style=\"width: 70%;\">" + dtInvoice.Rows[0]["OrderDate"] + "</td>";
                    tblPrintInvoice.InnerHtml += "</tr>";
                    tblPrintInvoice.InnerHtml += "<tr>";
                    tblPrintInvoice.InnerHtml += "<td style=\"width: 30%;\">Truck Number</td>";
                    tblPrintInvoice.InnerHtml += "<td style=\"width: 70%;\">" + dtInvoice.Rows[0]["VehicleRegNo"] + "</td>";
                    tblPrintInvoice.InnerHtml += "</tr>";
                    tblPrintInvoice.InnerHtml += "<tr>";
                    tblPrintInvoice.InnerHtml += "<td style=\"width: 30%;\">Containers Qty</td>";
                    tblPrintInvoice.InnerHtml += "<td style=\"width: 70%;\">" + dtInvoice.Rows.Count + "</td>";
                    tblPrintInvoice.InnerHtml += "</tr>";
                    tblPrintInvoice.InnerHtml += "<tr>";
                    tblPrintInvoice.InnerHtml += "<td style=\"width: 30%;\">Description</td>";
                    tblPrintInvoice.InnerHtml += "<td style=\"width: 70%;\">&nbsp;</td>";
                    tblPrintInvoice.InnerHtml += "</tr>";
                    tblPrintInvoice.InnerHtml += "<table>";
                    tblPrintInvoice.InnerHtml += "</td>";
                    tblPrintInvoice.InnerHtml += "<td>&nbsp;</td>";
                    tblPrintInvoice.InnerHtml += "<td>&nbsp;</td>";
                    tblPrintInvoice.InnerHtml += "<td>&nbsp;</td>";
                    tblPrintInvoice.InnerHtml += "</tr>";

                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        protected void SumAllAdvances(object sender, EventArgs e)
        {
            try
            {
                double Advance = txtAdvancefrei.Text == string.Empty ? 0 : Convert.ToDouble(txtAdvancefrei.Text);
                double Diesel = txtDiesAdvance.Text == string.Empty ? 0 : Convert.ToDouble(txtDiesAdvance.Text);
                double Factory = txtFactAdvance.Text == string.Empty ? 0 : Convert.ToDouble(txtFactAdvance.Text);
                double Vehicle = txtVehicAdvance.Text == string.Empty ? 0 : Convert.ToDouble(txtVehicAdvance.Text);

                double Total = (Advance + Diesel + Factory + Vehicle);
                lblTotAdvance.Text = Total.ToString();
            }
            catch (Exception ex)
            {
                AdvancesNotification("Error", "Error sum of all Advances");
            }
            finally
            {
                modalAdvances.Show();
            }
        }

        public void GetOrderAdvances(Int64 OrderID)
        {
            try
            {
                OrderDML dml = new OrderDML();
                DataTable dtAdvances = dml.GetOrderAdvancesByOrderID(OrderID);
                if (dtAdvances.Rows.Count > 0)
                {
                    gvAdvances2.DataSource = dtAdvances;
                    lblTotalAdvances.Text = dtAdvances.Rows[0]["Total"].ToString();
                    if (dtAdvances.Rows[0]["VehicleStatus"].ToString() == "Complete")
                    {
                        lnkAddAdvance2.Enabled = false;
                        lnkAddAdvance2.CssClass = "btn btn-secondary pull-right";
                        lnkAddAdvance2.ToolTip = "Can not add New Advance to received Bilty/Container";
                    }
                    else
                    {
                        lnkAddAdvance2.Enabled = true;
                        lnkAddAdvance2.CssClass = "btn btn-info pull-right";
                        lnkAddAdvance2.ToolTip = "Click to Add new Advance";
                    }
                }
                else
                {
                    gvAdvances2.DataSource = null;
                }
                //gvAdvances2.DataSource = dtAdvances.Rows.Count > 0 ? dtAdvances : null;
                gvAdvances2.DataBind();

                
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting/binding Order advances, due to: " + ex.Message);
            }
        }

        public void GetAndBindPatrolPumps(string SortState)
        {
            try
            {
                PatrolPumpDML dml = new PatrolPumpDML();
                DataTable dtPatrolPumps = dml.GetActivePatrolPumps(SortState);
                if (dtPatrolPumps.Rows.Count > 0)
                {
                    FillDropDown(dtPatrolPumps, ddlPatrolPumps, "PatrolPumpID", "Name", "-Select-");
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting/binding Patrol Pumps, due to: " + ex.Message);
            }
        }

        public void GetSetBrokerAccount(string BrokerAccountName)
        {
            try
            {
                AccountsDML dmlAccounts = new AccountsDML();
                DataTable dtBrokerAccountCheck = dmlAccounts.GetAccounts(BrokerAccountName);
                if (dtBrokerAccountCheck.Rows.Count <= 0)
                    dmlAccounts.CreateBrokerAccount(BrokerAccountName);
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting/setting broker account, due to: " + ex.Message);
            }
        }

        public void GetSetPatrolPumpAccount(string PatrolPumpAccountName)
        {
            try
            {
                AccountsDML dmlAccounts = new AccountsDML();
                DataTable dtPatrolPumpAccountCheck = dmlAccounts.GetAccounts(PatrolPumpAccountName);
                if (dtPatrolPumpAccountCheck.Rows.Count <= 0)
                    dmlAccounts.CreatePatrolPumpAccount(PatrolPumpAccountName);
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting/setting patrol pump account, due to: " + ex.Message);
            }
        }

        public void GetSetCustomerAccount(string CustomerAccountName)
        {
            try
            {
                AccountsDML dmlAccounts = new AccountsDML();
                DataTable dtCustomerAccountCheck = dmlAccounts.GetAccounts(CustomerAccountName);
                if (dtCustomerAccountCheck.Rows.Count <= 0)
                    dmlAccounts.CreateAccount(CustomerAccountName);
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting/setting broker account, due to: " + ex.Message);
            }
        }

        public void GetSetDisplaySTInvoice(Int64 STInvoiceID)
        {
            try
            {
                SalesTaxInvoiceDML dml = new SalesTaxInvoiceDML();
                DataTable dtSTInvoice = dml.GetSalesTaxInvoice(STInvoiceID);
                if (dtSTInvoice.Rows.Count > 0)
                {
                    lblSTSuppliersNameAddress.Text = dtSTInvoice.Rows[0]["Supplier"].ToString() + "\n" + dtSTInvoice.Rows[0]["SupplierAddress"].ToString();
                    lblSTBuyersNameAddress.Text = dtSTInvoice.Rows[0]["Buyer"].ToString() + "\n" + dtSTInvoice.Rows[0]["BuyerAddress"].ToString();

                    lblSTSupllierPhone.Text = dtSTInvoice.Rows[0]["SupplierPhone"].ToString();
                    lblSTBuyerPhone.Text = dtSTInvoice.Rows[0]["BuyerPhone"].ToString();

                    lblSTSupplierNTN.Text = dtSTInvoice.Rows[0]["SupplierNTN"].ToString();
                    lblSTBuyerNTN.Text = dtSTInvoice.Rows[0]["BuyerNTN"].ToString();

                    lblSTFrom.Text = dtSTInvoice.Rows[0]["EmptyContainerPickLocation"].ToString();
                    lblSTTo.Text = dtSTInvoice.Rows[0]["EmptyContainerDropLocation"].ToString();
                    lblSTContainerRate.Text = dtSTInvoice.Rows[0]["Rate"].ToString();
                    lblSTPercentage.Text = dtSTInvoice.Rows[0]["AppliedTaxPercentage"].ToString();
                    lblSTAmount.Text = dtSTInvoice.Rows[0]["AppliedTaxAmount"].ToString();
                    lblAmountInclusiveST.Text = dtSTInvoice.Rows[0]["ValueInclusiveTax"].ToString();
                    lblSTInvoiceDate.Text = dtSTInvoice.Rows[0]["InvoiceDate"].ToString();
                    lblSTVehicleRegNo.Text = dtSTInvoice.Rows[0]["VehicleRegNo"].ToString();
                    lblSTTotal.Text = dtSTInvoice.Rows[0]["ValueInclusiveTax"].ToString();
                    modalSTInvoice.Show();
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting/setting/displaying Sales Tax invoice, due to: " + ex.Message);
            }
        }

        public double PercentageToNumber(double Percentage, double Amount)
        {
            return (Percentage / 100) * Amount;
        }

        public void STInvoiceNotification()
        {
            try
            {
                divSTInvoiceNotification.InnerHtml = string.Empty;
            }
            catch (Exception ex)
            {
                divSTInvoiceNotification.InnerHtml = "<div class=\"alert alert-danger\">" + ex.Message + "</div>";
            }
        }

        public void STInvoiceNotification(string type, string msg)
        {
            try
            {
                if (type == "Error")
                {
                    divSTInvoiceNotification.InnerHtml = "<div class=\"alert alert-danger\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                }
                else if (type == "Success")
                {
                    divSTInvoiceNotification.InnerHtml = "<div class=\"alert alert-success\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                }
                else if (type == string.Empty)
                {
                    divSTInvoiceNotification.InnerHtml = string.Empty;
                }
            }
            catch (Exception ex)
            {
                divSTInvoiceNotification.InnerHtml = "<div class=\"alert alert-danger\">" + ex.Message + "</div>";
            }
        }

        public void STInvoiceConfirmNotification()
        {
            try
            {
                divSalesTaxInvoiceConfirmNotification.InnerHtml = string.Empty;
            }
            catch (Exception ex)
            {
                divSalesTaxInvoiceConfirmNotification.InnerHtml = "<div class=\"alert alert-danger\">" + ex.Message + "</div>";
            }
        }

        public void STInvoiceConfirmNotification(string type, string msg)
        {
            try
            {
                if (type == "Error")
                {
                    divSalesTaxInvoiceConfirmNotification.InnerHtml = "<div class=\"alert alert-danger\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                }
                else if (type == "Success")
                {
                    divSalesTaxInvoiceConfirmNotification.InnerHtml = "<div class=\"alert alert-success\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                }
                else if (type == string.Empty)
                {
                    divSalesTaxInvoiceConfirmNotification.InnerHtml = string.Empty;
                }
            }
            catch (Exception ex)
            {
                divSalesTaxInvoiceConfirmNotification.InnerHtml = "<div class=\"alert alert-danger\">" + ex.Message + "</div>";
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

        private void FillCheckBox(DataTable dt, CheckBoxList _cbl, string _ddlValue, string _ddlText)
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

        public void AmountCalculation()
        {
            try
            {
                if (txtPatrolRate.Text.Trim() != string.Empty && txtAdvanceAmount.Text.Trim() != string.Empty)
                {
                    double LeterCost = Convert.ToDouble(txtAdvanceAmount.Text.Trim()) / Convert.ToDouble(txtPatrolRate.Text.Trim());
                    txtPatrolLitre.Text = LeterCost.ToString();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
           finally
            {
                modalAdvances2.Show();
            }
        }

        #region Events

        #region Gridview's Events

        #region Row Commands

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
                        Int64 ShippingLineID = dtOrder.Rows[0]["ShippingLineID"].ToString() == string.Empty ? 0 : Convert.ToInt64(dtOrder.Rows[0]["ShippingLineID"].ToString());
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

                        ddlShippingLine.ClearSelection();
                        ddlShippingLine.Items.FindByValue(ShippingLineID.ToString()).Selected = true;
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
                    pnlBiltyContainers.Visible = false;

                }
                else if (e.CommandName == "BiltyContainers")
                {
                    
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvBilty.Rows[index];
                 
                    hfSelectedBillToCustomer.Value = gvBilty.DataKeys[index]["BillToCustomerCompany"].ToString();
                    Int64 OrderID = Convert.ToInt64(gvBilty.DataKeys[index]["OrderID"]);
                    hfSelectedOrder.Value = OrderID.ToString();
                    OrderDML dml = new OrderDML();
                    GetBiltyContainers(OrderID);
                    
                    modalContainers.Show();
                    pnlBiltyContainers.Visible = true;

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
                else if (e.CommandName == "BiltyAdvances")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvBilty.Rows[index];
                    Int64 OrderID = Convert.ToInt64(gvBilty.DataKeys[index]["OrderID"]);
                    hfSelectedOrder.Value = OrderID.ToString();
                    lnkAddAdvance2.Enabled = true;
                    lnkAddAdvance2.ToolTip = "Click to Add new Advance";
                    GetOrderAdvances(OrderID);

                    modalAdvances2.Show();

                    //DataTable dtAdvances = dml.GetOrderAdvancesByOrderID(OrderID);
                    //if (dtAdvances.Rows.Count > 0)
                    //{
                    //    double Total = 0;
                    //    foreach (DataRow _drAdvance in dtAdvances.Rows)
                    //    {
                    //        string AdvanceType = _drAdvance["AdvanceAgainst"].ToString();
                    //        double Amount = Convert.ToDouble(_drAdvance["AdvanceAmount"]);
                    //        switch (AdvanceType)
                    //        {
                    //            case "FreightAdvance":
                    //                txtAdvancefrei.Text = Amount.ToString();
                    //                break;
                    //            case "DieselAdvance":
                    //                txtDiesAdvance.Text = Amount.ToString();
                    //                break;
                    //            case "FactoryAdvance":
                    //                txtFactAdvance.Text = Amount.ToString();
                    //                break;
                    //            case "VehicleAdvance":
                    //                txtVehicAdvance.Text = Amount.ToString();
                    //                break;
                    //            default:
                    //                break;
                    //        }
                    //        Total += Amount;
                    //    }
                    //    lblTotAdvance.Text = Total.ToString();
                    //}
                    //modalAdvances.Show();

                    //DataTable dtOrderVehicles = dml.GetBiltyVehiclesByOrder(OrderID);
                    //if (dtOrderVehicles.Rows.Count > 0)
                    //{
                    //    bool AllCompleted = false;
                    //    foreach (DataRow _drVehicles in dtOrderVehicles.Rows)
                    //    {
                    //        if (_drVehicles["Status"].ToString() == "Complete")
                    //        {
                    //            AllCompleted = true;
                    //        }
                    //    }

                    //    lnkSaveAdvances.Enabled = AllCompleted == true ? false : true;
                    //    lnkSaveAdvances.CssClass = AllCompleted == true ? "btn btn-default pull-right m-b-10 m-r-10" : "btn btn-primary pull-right m-b-10 m-r-10";
                    //    lnkSaveAdvances.ToolTip = AllCompleted == true ? "Cannot save completed bilty advances" : "Click to Save advances";
                    //}
                    //else
                    //{
                    //    lnkSaveAdvances.Enabled = false;
                    //    lnkSaveAdvances.CssClass = "btn btn-default pull-right m-b-10 m-r-10";
                    //    lnkSaveAdvances.ToolTip = "Cannot save 0 vehicle bilty advances";
                    //}
                }
                else if (e.CommandName == "BiltyPrint")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvBilty.Rows[index];
                    Int64 OrderID = Convert.ToInt64(gvBilty.DataKeys[index]["OrderID"]);
                    OrderDML dml = new OrderDML();
                    DataTable dtContainer = dml.GetOrderContainerByOrderID(OrderID);
                    if (dtContainer.Rows.Count > 0)
                    {
                        string ProductString = string.Empty;
                        lblBiltyNo.Text = dtContainer.Rows[0]["OrderNo"].ToString();
                        DateTime date = (DateTime)dtContainer.Rows[0]["RecordedDate"];
                        lblBiltyDate.Text = date.ToShortDateString();
                        lblVehicleRegNo.Text = dtContainer.Rows[0]["VehicleRegNo"].ToString();
                        lblFrom.Text = dtContainer.Rows[0]["EmptyContainerPickLocation"].ToString();
                        lblTo.Text = dtContainer.Rows[0]["EmptyContainerDropLocation"].ToString();
                        lblSenderCompany.Text = dtContainer.Rows[0]["Sender"].ToString();
                        lblReceiverCompany.Text = dtContainer.Rows[0]["Receiver"].ToString();
                        lblBillToCompany.Text = dtContainer.Rows[0]["BillTo"].ToString();
                        lblBroker.Text = dtContainer.Rows[0]["Broker"].ToString();
                        Int64 ContainerWeight = Convert.ToInt64(dtContainer.Rows[0]["ContainerWeight"]);
                        string PaidtoPay = dtContainer.Rows[0]["PaidToPay"].ToString();

                        double TotalFreight = 0;
                        tblDescriptionBody.InnerHtml = string.Empty;
                        if (PaidtoPay == "To-Pay")
                        {
                            tblDescriptionHead.InnerHtml = string.Empty;
                            tblDescriptionHead.InnerHtml += "<tr>";
                                tblDescriptionHead.InnerHtml += "<th>Nos.</th>";
                                tblDescriptionHead.InnerHtml += "<th>Description</th>";
                                tblDescriptionHead.InnerHtml += "<th>Weight</th>";
                                tblDescriptionHead.InnerHtml += "<th>Freight</th>";
                            tblDescriptionHead.InnerHtml += "</tr>";
                        }


                        //DataTable dtExpense = new DataTable();
                        //dtExpense.Columns.Add("ExpenseType");
                        //dtExpense.Columns.Add("Qty");
                        //dtExpense.Columns.Add("Amount");
                        foreach (DataRow _drContainers in dtContainer.Rows)
                        {
                            Int64 ContainerID = Convert.ToInt64(_drContainers["OrderConsignmentID"]);
                            tblDescriptionBody.InnerHtml += "<tr>";
                            tblDescriptionBody.InnerHtml += "<td>" + 1 + " X " + _drContainers["ContainerSize"] + "</td>";
                            tblDescriptionBody.InnerHtml += "<td>" + _drContainers["ContainerNo"] + "</td>";
                            tblDescriptionBody.InnerHtml += "<td>" + _drContainers["ContainerWeight"] + "</td>";
                            tblDescriptionBody.InnerHtml += PaidtoPay == "To-Pay" ? "<td>" + _drContainers["Rate"] + "</td>" : string.Empty;
                            //tblDescriptionBody.InnerHtml += "<td>" + _drContainers["Rate"].ToString() + "</td>";
                            TotalFreight += Convert.ToInt64(_drContainers["Rate"]);
                            tblDescriptionBody.InnerHtml += "</tr>";
                            //if (PaidtoPay == "To-Pay")
                            //{
                            //    DataTable dtExpenses = dml.GetExpenses(ContainerID);
                            //    if (dtExpenses.Rows.Count > 0)
                            //    {
                            //        foreach (DataRow _drExpenses in dtExpenses.Rows)
                            //        {
                            //            string ExpenseTypes = _drExpenses["ExpensesTypeName"].ToString();
                            //            double Amounts = Convert.ToDouble(_drExpenses["Amount"]);
                            //            int EditIndex = 0;
                            //            bool IsExist = false;
                            //            int Qty = 0;
                            //            foreach (DataRow _drExpense in dtExpense.Rows)
                            //            {
                            //                string ExpenseType = _drExpense["ExpenseType"].ToString();
                            //                double Amount = Convert.ToDouble(_drExpense["Amount"]);
                            //                if (Amounts == Amount)
                            //                {
                            //                    EditIndex = dtExpense.Rows.IndexOf(_drExpense);
                            //                    IsExist = true;
                            //                }
                            //            }
                            //            if (IsExist == true)
                            //            {
                            //                dtExpense.Rows[EditIndex]["Qty"] = Convert.ToInt64(dtExpense.Rows[EditIndex]["Qty"]) + 1;
                            //                dtExpense.Rows[EditIndex]["Amount"] = (Convert.ToDouble(dtExpense.Rows[EditIndex]["Amount"]) + Amounts).ToString();
                            //            }
                            //            else
                            //            {
                            //                dtExpense.Rows.Add(ExpenseTypes, 1, Amounts.ToString());
                            //            }

                            //        }
                            //    }
                            //}
                            
                        }

                        if (PaidtoPay == "To-Pay")
                        {
                            DataTable dtExpense = dml.GetExpenses_Breakup(OrderID);
                            if (dtExpense.Rows.Count > 0)
                            {
                                foreach (DataRow _drExpense in dtExpense.Rows)
                                {
                                    double Expense = Convert.ToDouble(_drExpense["Total"]);
                                    //TotalFreight = TotalFreight + Expense;

                                    tblDescriptionBody.InnerHtml += "<tr>";
                                    tblDescriptionBody.InnerHtml += "<td>" + _drExpense["Qty"] + "</td>";
                                    tblDescriptionBody.InnerHtml += "<td>" + _drExpense["Type"] + " @ " + _drExpense["Rate"] + "</td>";
                                    tblDescriptionBody.InnerHtml += "<td>&nbsp;</td>";
                                    tblDescriptionBody.InnerHtml += "<td>" + _drExpense["Total"] + "</td>";
                                    //tblDescriptionBody.InnerHtml += "<td>" + _drContainers["Rate"].ToString() + "</td>";
                                    TotalFreight += Expense;
                                    tblDescriptionBody.InnerHtml += "</tr>";
                                }
                            }
                        }
                        

                        //DataTable dtAdvances = dml.GetOrder(OrderID);
                        //double TotalAdvance = 0;
                        //if (dtAdvances.Rows.Count > 0)
                        //{
                        //    foreach (DataRow _dr in dtAdvances.Rows)
                        //    {
                        //        TotalAdvance += Convert.ToDouble(_dr["AdvanceAmount"]);
                        //    }
                        //}
                        //lblTotalAdvance.Text = TotalAdvance.ToString();

                        DataTable dtProduct = dml.GetBiltyProductsByOrder(OrderID);
                        if (dtProduct.Rows.Count > 0)
                        {
                            foreach (DataRow _drProduct in dtProduct.Rows)
                            {
                                ProductString += _drProduct["Qty"].ToString() + " " + _drProduct["PackageType"].ToString() + " " + _drProduct["Item"] + " &<br>";
                            }
                            ProductString = ProductString.Substring(0, ProductString.Length - 6);

                            if (ProductString.Length > 0)
                            {
                                tblDescriptionBody.InnerHtml += "<tr>";
                                tblDescriptionBody.InnerHtml += "<td class=\"service\">&nbsp;</td>";
                                tblDescriptionBody.InnerHtml += "<td class=\"desc\">" + ProductString + "</td>";
                                tblDescriptionBody.InnerHtml += "<td>&nbsp;</td>";
                                tblDescriptionBody.InnerHtml += "<td>&nbsp;</td>";
                                tblDescriptionBody.InnerHtml += "</tr>";
                            }
                        }

                        //TotalFreight = Convert.ToInt64(dtContainer.Rows[0]["Freight"]);
                        //lblBalance.Text = (TotalFreight - TotalAdvance).ToString();
                        lblBalance.Text = TotalFreight.ToString();


                        lblPaidtoPay.Text = PaidtoPay;
                        if (PaidtoPay == "To-Pay")
                        {
                            trBalance.Visible = true;
                            //trTotalAdvance.Visible = true;
                            trPaid.Visible = false;
                        }
                    }
                    else
                    {
                        notification("Error", "No such record found");
                    }


                    modalBiltyPrint.Show();
                }
                else if (e.CommandName == "Invoice")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvBilty.Rows[index];
                    Int64 OrderID = Convert.ToInt64(gvBilty.DataKeys[index]["OrderID"]);
                    OrderDML dml = new OrderDML();
                    hfSelectedOrder.Value = OrderID.ToString();

                    DataTable dtInvoicedOrder = dml.GetInvoicedorder(OrderID);
                    if (dtInvoicedOrder.Rows.Count > 0)
                    {
                        GetInvoice(dtInvoicedOrder.Rows[0]["InvoiceNo"].ToString());
                    }
                    else
                    {
                        ConfirmModal("Are you want to make bill of this Order?", "GenInvoice");
                    }
                }
                else if (e.CommandName == "STInvoice")
                {

                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvBilty.Rows[index];
                    Int64 OrderID = Convert.ToInt64(gvBilty.DataKeys[index]["OrderID"]);
                    string BillToCustomerCompany = gvBilty.DataKeys[index]["BillToCustomerCompany"].ToString();
                    SalesTaxInvoiceDML dml = new SalesTaxInvoiceDML();
                    hfSelectedOrder.Value = OrderID.ToString();

                    DataTable dtInvoicedOrder = dml.GetSalesTaxInvoiceByOrder(OrderID);
                    if (dtInvoicedOrder.Rows.Count > 0)
                    {
                        //Method to get set and show sales tax invoice
                        Int64 STInvoiceID = Convert.ToInt64(dtInvoicedOrder.Rows[0]["SalesTaxInvoiceID"]);

                        GetSetDisplaySTInvoice(STInvoiceID);
                    }
                    else
                    {
                        OrderDML dmlOrder = new OrderDML();
                        DataTable dtContainer = dmlOrder.GetOrderContainerByOrderID(OrderID);
                        if (dtContainer.Rows.Count > 0)
                        {
                            double ContainerRate = Convert.ToDouble(dtContainer.Rows[0]["Rate"]);
                            double STPerc = 0;

                            DataTable dtTaxType = dml.GetTaxType("Sales Tax");
                            if (dtTaxType.Rows.Count > 0)
                            {
                                STPerc = Convert.ToDouble(dtTaxType.Rows[0]["Percentage"]);
                            }

                            double BuyerID = 0;
                            DataTable dtOrder = dmlOrder.GetBilty(OrderID);
                            if (dtOrder.Rows.Count > 0)
                            {
                                BuyerID = Convert.ToInt64(dtOrder.Rows[0]["CustomerCompanyID"]);

                                ddlBuyer.ClearSelection();
                                ddlBuyer.Items.FindByText(BillToCustomerCompany).Selected = true;
                            }
                                
                            double STAmount = PercentageToNumber(STPerc, ContainerRate);
                            double Total = ContainerRate + STAmount;

                            txtActualAmount.Text = ContainerRate.ToString();
                            txtSalesTaxPercentage.Text = STPerc.ToString();
                            txtSalesTaxAmount.Text = STAmount.ToString();
                            txtValueInclusiveTax.Text = Total.ToString();

                            modalSalesTaxConfirm.Show();
                        }
                        
                        //ConfirmModal("Are you want to make Sales Tax Invoice of this Order?", "GenSTInvoice");
                    }
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error commanding row, due to: " + ex.Message);
            }
            finally
            {
                modalContainers.Show();
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
                    if (dtVehicles.Rows.Count > 0)
                    {
                        pnlBiltyVehicleInputs.Visible = true;

                        ddlVehicleType.ClearSelection();
                        ddlVehicleType.Items.FindByText(dtVehicles.Rows[0]["VehicleType"].ToString()).Selected = true;

                        txtVehicleRegNo.Text = dtVehicles.Rows[0]["VehicleRegNo"].ToString();
                       // ddlVehicleNo.Items.FindByText(dtVehicles.Rows[0]["VehicleRegNo"].ToString()).Selected=true;
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
                        txtVehicleRate.Text = dtVehicles.Rows[0]["Rate"].ToString();
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
                pnlBiltyVehicleInputs.Visible = true;
                pnlBiltyContainers.Visible = false;
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
                        ddlContainerPickup.Items.FindByValue(dtContainer.Rows[0]["PickupLocationID"].ToString()).Selected = true;
                        ddlContainerDropoff.ClearSelection();
                        ddlContainerDropoff.Items.FindByValue(dtContainer.Rows[0]["DropoffLocationID"].ToString()).Selected = true;
                        txtContainerRate.Text = dtContainer.Rows[0]["Rate"].ToString();

                        txtContainerRemarks.Text = dtContainer.Rows[0]["Remarks"].ToString();

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
                        ddlAssignedVehicle.Enabled = false;
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
                else if (e.CommandName == "Expenses")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvContainer.Rows[index];
                    Int64 OrderContainerID = Convert.ToInt64(gvContainer.DataKeys[index]["OrderConsignmentID"]);
                    Int64 OrderID = Convert.ToInt64(hfSelectedOrder.Value);

                    ddlExpenseContainer.Items.Clear();

                    ListItem item = new ListItem();

                    item.Text = gvr.Cells[1].Text;
                    item.Value = gvContainer.DataKeys[index]["OrderConsignmentID"].ToString();

                    ddlExpenseContainer.Items.Insert(0, item);
                    ddlExpenseContainer.SelectedIndex = 0;


                    OrderDML dml = new OrderDML();
                    DataTable dtExpenses = dml.GetExpenses(OrderContainerID);
                    if (dtExpenses.Rows.Count > 0)
                    {
                        gvContainerExpense.DataSource = dtExpenses;
                    }
                    else
                    {
                        gvContainerExpense.DataSource = null;
                        ContainerNotification("Error", "No expenses found for selected container");
                    }

                    gvContainerExpense.DataBind();
                    divContainerDetails.Visible = false;
                    divContainerExpense.Visible = true;

                    pnlBiltyContainerInputs.Visible = false;
                    pnlContainerExpensesInput.Visible = false;

                    lnkCloseContainerExpense.Visible = true;
                    lnkCloseBiltyContainer.Visible = false;
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

        protected void gvContainerExpense_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Remove")
            {
                try
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvContainerExpense.Rows[index];
                    Int64 ExpenseID = Convert.ToInt64(gvContainerExpense.DataKeys[index]["ContainerExpenseID"]);
                    OrderDML dml = new OrderDML();
                    dml.DeleteContainerExpense(ExpenseID);
                    ContExpenseNotification("Success", "1 item Deleted successful");
                    Int64 OrderID = Convert.ToInt64(hfSelectedOrder.Value);
                    Int64 ContainerID = Convert.ToInt64(ddlExpenseContainer.SelectedItem.Value);
                    GetContainerExpense(ContainerID);
                    GetBiltyContainers(OrderID);
                }
                catch (Exception ex)
                {

                    ContExpenseNotification("Error", "Error occured to delete due to: " + ex.Message);
                }

            }
            else if (e.CommandName == "Change")
            {
                try
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvContainerExpense.Rows[index];
                    Int64 ExpenseID = Convert.ToInt64(gvContainerExpense.DataKeys[index]["ContainerExpenseID"]);
                    hfContainerExpense.Value = ExpenseID.ToString();
                    OrderDML dml = new OrderDML();
                    DataTable dt = dml.GetExpenseTypes(ExpenseID);
                    if (dt.Rows.Count > 0)
                    {
                        ddlExpenseContainer.ClearSelection();
                        ddlExpenseContainer.Items.FindByValue(dt.Rows[0]["ContainerID"].ToString()).Selected = true;

                        ddlExpenseType.ClearSelection();
                        ddlExpenseType.Items.FindByValue(dt.Rows[0]["ExpenseTypeID"].ToString()).Selected = true;

                        txtExpenseAmount.Text = dt.Rows[0]["Amount"].ToString();
                    }
                    pnlContainerExpensesInput.Visible = true;
                    modalContainers.Show();

                }
                catch (Exception ex)
                {

                    ContExpenseNotification("Error", "Error occured with editing due to: " + ex.Message);
                }


            }
            else if (e.CommandName == "Paid")
            {
                try
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvContainerExpense.Rows[index];
                    Int64 ContainerExpenseID = Convert.ToInt64(gvContainerExpense.DataKeys[index]["ContainerExpenseID"]);
                    string Paid = gvContainerExpense.DataKeys[index]["PaidByDriver"].ToString() == string.Empty ? "False" : gvContainerExpense.DataKeys[index]["PaidByDriver"].ToString();
                    // CityDML dml = new CityDML();
                    OrderDML odml = new OrderDML();
                    Int64 ContainerID = Convert.ToInt64(ddlExpenseContainer.SelectedItem.Value);
                    //hfEditID.Value = CityID.ToString();

                    if (Paid == "True")
                    {
                        odml.DeactivatePaidByDriver(ContainerExpenseID, LoginID);
                       // dml.DeactivateStudent(ID, LoginID);
                    }
                    else
                    {
                        odml.ActivatePaidByDriver(ContainerExpenseID, LoginID);
                       // dml.ActivateStudent(ID, LoginID);
                    }


                    
                    DataTable dtExpenses = odml.GetExpenses(ContainerID);
                    if (dtExpenses.Rows.Count > 0)
                    {
                        gvContainerExpense.DataSource = dtExpenses;
                    }
                    else
                    {
                        gvContainerExpense.DataSource = null;
                        ContainerNotification("Error", "No expenses found for selected container");
                    }

                    gvContainerExpense.DataBind();
                    modalContainers.Show();

                }
                catch (Exception ex)
                {

                    ContExpenseNotification("Error", "Error occured with editing due to: " + ex.Message);
                }


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

        protected void gvAdvances2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Wipe")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvAdvances2.Rows[index];
                    Int64 OrderAdvanceID = Convert.ToInt64(gvAdvances2.DataKeys[index]["AdvanceID"]);
                    Int64 OrderID = Convert.ToInt64(hfSelectedOrder.Value);
                    OrderDML dml = new OrderDML();
                    dml.DeleteOrderAdvance(OrderAdvanceID);
                    GetBilties();
                    GetOrderAdvances(OrderID);
                    Advances2Notification("Success", "Advance deleted successfully");
                }
            }
            catch (Exception ex)
            {
                Advances2Notification("Error", "Error commanding row, due to: " + ex.Message);
            }
            finally
            {
                modalAdvances2.Show();
            }
        }

        #endregion

        #region Data Bindings

        //protected void gvContainer_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    try
        //    {
        //        if (e.Row.RowType == DataControlRowType.DataRow)
        //        {
        //            DataRowView rowView = (DataRowView)e.Row.DataItem;
        //            bool ReceivingStatus = (rowView["Status"].ToString() == string.Empty ? "False" : rowView["Status"].ToString()) == "False" ? false : true;
        //            DropDownList ddlReceivingStatus = e.Row.FindControl("ddlReceiving") as DropDownList;
        //            if (ReceivingStatus == true)
        //            {
        //                ddlReceivingStatus.Items.FindByText("Received").Selected = true;
        //                ddlReceivingStatus.Enabled = false;
        //            }
        //            else
        //            {
        //                ddlReceivingStatus.Items.FindByText("Pending").Selected = true;
        //                e.Row.BackColor = Color.Pink;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ContainerNotification("Error", "Error binding row, due to: " + ex.Message);
        //    }
        //}

        protected void gvBilty_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView rowView = (DataRowView)e.Row.DataItem;
                    Int64 OrderID = Convert.ToInt64(rowView["OrderID"]);
                    string PaidToPay = rowView["PaidToPay"].ToString();
                    string Orderno = e.Row.Cells[0].Text;
                    OrderDML dml = new OrderDML();
                    DataTable dtContainers = dml.GetBiltyContainersByOrder(OrderID);
                    //int isBilled = Convert.ToInt32(rowView["isBilled"]);
                    bool isPending = true;

                    LinkButton lnkAdvance = e.Row.FindControl("lnkAdvances") as LinkButton;
                    LinkButton lnkUpdateDropdown = e.Row.FindControl("lnkUpdateDropdown") as LinkButton;

                    lnkAdvance.Enabled = PaidToPay == "To-Pay" ? false : true;
                    lnkAdvance.ToolTip = PaidToPay == "To-Pay" ? "Cannot assign advance to To-Pay bilty" : string.Empty;

                    if (dtContainers.Rows.Count > 0)
                    {
                        bool biltyPending = false;
                        bool isBilled = false;
                        LinkButton lnkInvoice = e.Row.FindControl("lnkInvoice") as LinkButton;


                        foreach (DataRow _drContainers in dtContainers.Rows)
                        {
                            if (_drContainers["Status"].ToString() == "False" || _drContainers["Status"].ToString() == "0")
                            {
                                biltyPending = true;
                            }

                            if (_drContainers["Billed"].ToString() != "False")
                            {
                                isBilled = true;
                            }
                        }

                        if (biltyPending == true)
                        {
                            lnkInvoice.Enabled = false;
                            lnkInvoice.ToolTip = "Could not make invoice because Bilty not completed";
                        }
                        else
                        {
                            if (dtContainers.Rows[0]["PaidToPay"].ToString() == "To-Pay")
                            {
                                lnkInvoice.Enabled = false;
                                lnkInvoice.ToolTip = "Could not make To-Pay invoice";
                            }
                            else
                            {
                                if (isBilled == true)
                                {
                                    lnkUpdateDropdown.CssClass = "btn btn-xs btn-default dropdown-toggle disabled";
                                    lnkInvoice.Enabled = false;
                                    lnkInvoice.ToolTip = "Could not make invoice because one of its container has been already billed";
                                }
                                else
                                {
                                    lnkInvoice.Enabled = true;
                                    lnkInvoice.ToolTip = string.Empty;
                                }
                            }

                        }

                        isPending = biltyPending;
                        pnlBiltyContainers.Visible = false;
                    }
                    else
                    {
                        isPending = true;
                    }
                    e.Row.BackColor = isPending == true ? Color.Pink : Color.White;
                    if (isPending == true)
                    {
                        e.Row.BackColor = Color.Pink;
                    }
                    else
                    {
                        e.Row.BackColor = PaidToPay == "To-Pay" ? Color.LightGreen : Color.White;
                    }
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error binding bilties row, due to: " + ex.Message);
            }
        }

        protected void gvContainer_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView rowView = (DataRowView)e.Row.DataItem;
                    bool ReceivingStatus = (rowView["Status"].ToString() == string.Empty ? "False" : rowView["Status"].ToString()) == "False" ? false : true;
                    bool IsBilled = (rowView["IsBilled"].ToString() == string.Empty ? "False" : rowView["IsBilled"].ToString()) == "False" ? false : true;
                    DropDownList ddlReceivingStatus = e.Row.FindControl("ddlReceiving") as DropDownList;
                    LinkButton lnkExpense = e.Row.FindControl("lnkExpenses") as LinkButton;
                    LinkButton lnkPrintInvoice = e.Row.FindControl("lnkInvoice") as LinkButton;
                    LinkButton lnkEditContainer = e.Row.FindControl("lnkEditContainer") as LinkButton;

                    if (IsBilled)
                    {
                        lnkExpense.Enabled = false;
                        lnkExpense.ForeColor = Color.DarkGray;
                        lnkExpense.ToolTip = string.Empty;


                        lnkEditContainer.Enabled = false;
                        lnkEditContainer.ToolTip = string.Empty;
                        lnkEditContainer.CssClass = "btn btn-xs btn-default";
                    }



                    if (ReceivingStatus == true)
                    {
                        ddlReceivingStatus.Items.FindByText("Received").Selected = true;
                        ddlReceivingStatus.Enabled = false;




                        //lnkExpense.Enabled = false;
                        //lnkExpense.ForeColor = Color.DarkGray;
                        //lnkExpense.ToolTip = string.Empty;


                        //lnkEditContainer.Enabled = false;
                        //lnkEditContainer.ToolTip = string.Empty;
                        //lnkEditContainer.CssClass = "btn btn-xs btn-default";
                    }
                    else
                    {
                        ddlReceivingStatus.Items.FindByText("Pending").Selected = true;
                        e.Row.BackColor = Color.Pink;
                    }
                }
            }
            catch (Exception ex)
            {
                ContainerNotification("Error", "Error binding row, due to: " + ex.Message);
            }
        }

        protected void gvBiltyVehicles_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                DataRowView rowView = (DataRowView)e.Row.DataItem;
                string Status = gvBiltyVehicles.DataKeys[e.Row.RowIndex].Values["Status"].ToString();


                LinkButton lnkEdit = e.Row.FindControl("lnkEdit") as LinkButton;
                if (Status == "Complete")
                {
                    lnkEdit.Enabled = false;
                    lnkEdit.ToolTip = "Cannot edit received bilties";
                    e.Row.Cells[7].Visible = false;
                    gvBiltyVehicles.HeaderRow.Cells[7].Visible = false;
                }
                else
                {
                    lnkEdit.Enabled = true;
                    lnkEdit.ToolTip = "Click to edit Bilty Vehicle information";
                    e.Row.Cells[7].Visible = true;
                    gvBiltyVehicles.HeaderRow.Cells[7].Visible = true;
                }
            }
        }

        protected void gvAdvances2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                bool OrderVehicle = false;
                bool OrderContainer = false;
                Int64 OrderID = Convert.ToInt64(hfSelectedOrder.Value);
                LinkButton lnkDelete = e.Row.FindControl("lnkDelete") as LinkButton;
                LinkButton lnkVoucher = e.Row.FindControl("lnkVoucher") as LinkButton;

                string AdvanceAgainst = e.Row.Cells[1].Text;

                OrderDML dmlOrder = new OrderDML();
                DataTable dtOrderVehicles = dmlOrder.GetBiltyVehiclesByOrder(OrderID);
                if (dtOrderVehicles.Rows.Count > 0)
                {
                    foreach (DataRow _dr in dtOrderVehicles.Rows)
                    {
                        if (_dr["Status"].ToString() == "Complete")
                        {
                            OrderVehicle = true;
                        }
                    }
                }

                DataTable dtOrderContainers = dmlOrder.GetBiltyContainersByOrder(OrderID);
                if (dtOrderContainers.Rows.Count > 0)
                {
                    foreach (DataRow _dr in dtOrderContainers.Rows)
                    {
                        if (_dr["Status"].ToString() == "True")
                        {
                            OrderContainer = true;
                        }
                    }
                }



                if (OrderVehicle == true || OrderContainer == true)
                {
                    lnkDelete.Enabled = false;
                    lnkDelete.CssClass = "btn btn-xs btn-secondary";
                    lnkDelete.ToolTip = "Advance of Received Vehicles/Containers can't Delete";
                }

                //if (AdvanceAgainst == "Advance Freight")
                //{
                //    lnkVoucher.Enabled = true;
                //    lnkVoucher.ToolTip = "Click to make Voucher";
                //    lnkVoucher.CssClass = "btn btn-xs btn-primary";
                //}
            }
        }

        protected void gvContainerExpense_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView rowView = (DataRowView)e.Row.DataItem;
                    bool PaidByDriver = Convert.ToBoolean(rowView["PaidByDriver"].ToString() == string.Empty ? "false" : rowView["PaidByDriver"]);
                    LinkButton lnkPaid = e.Row.Cells[3].FindControl("lnkPaid") as LinkButton;

                    lnkPaid.CssClass = PaidByDriver ? "fas fa-toggle-on" : "fas fa-toggle-off";
                    lnkPaid.ForeColor = PaidByDriver ? Color.DodgerBlue : Color.Maroon;


                }

            }
            catch (Exception ex)
            {

                ContExpenseNotification("Error", "Error binding container expenses, due to: " + ex.Message);
            }

        }

        #endregion

        #region Miscellenous

        protected void gvBilty_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvBilty.PageIndex = e.NewPageIndex;
                this.GetBilties();

            }
            catch (Exception ex)
            {
                notification("Error", "Error changing index of grid page, due to: " + ex.Message);
            }
        }

        protected void gvBilty_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                this.GetBilties(e.SortExpression);
            }
            catch (Exception ex)
            {
                notification("Error", "Error sorting bilties grid, due to: " + ex.Message);
            }
        }

        #endregion

        #endregion

        #region LinkButton's Click

        protected void lnkConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                string Action = lblConfirmAction.Text;
                if (Action == "GenInvoice")
                {
                    try
                    {
                        Int64 OrderID = Convert.ToInt64(hfSelectedOrder.Value);
                        OrderDML dml = new OrderDML();
                        DataTable dtInvoicedOrder = dml.GetInvoicedorder(OrderID);
                        if (dtInvoicedOrder.Rows.Count > 0)
                        {
                            string InvoiceNo = dtInvoicedOrder.Rows[0]["InvoiceNo"].ToString();
                            ShowInvoice(InvoiceNo);
                            //DataTable dtInvoice = dml.GetInvoice(InvoiceNo);
                            //if (dtInvoice.Rows.Count > 0)
                            //{
                            //    lblPrintInvoieno.Text = dtInvoice.Rows[0]["InvoiceNo"].ToString();
                            //    lblPrintInvoiceDate.Text = dtInvoice.Rows[0]["CreatedDate"].ToString();
                            //    lblPrintInvoiceCsutomer.Text = dtInvoice.Rows[0]["CustomerCompany"].ToString();
                            //    lblPrintInvoiceRemarks.Text = string.Empty;

                            //    tblPrintInvoice.InnerHtml = string.Empty;
                            //    tblPrintInvoice.InnerHtml += "<tr>";
                            //        tblPrintInvoice.InnerHtml += "<td>&nbsp;</td>";
                            //        tblPrintInvoice.InnerHtml += "<td>";
                            //            tblPrintInvoice.InnerHtml += "<table style=\"width: 100%\">";
                            //                tblPrintInvoice.InnerHtml += "<tr>";
                            //                    tblPrintInvoice.InnerHtml += "<td style=\"width: 30%;\">From</td>";
                            //                    tblPrintInvoice.InnerHtml += "<td style=\"width: 70%;\">" + dtInvoice.Rows[0]["EmptyContainerPickLocation"] + "</td>";
                            //                tblPrintInvoice.InnerHtml += "</tr>";
                            //                tblPrintInvoice.InnerHtml += "<tr>";
                            //                    tblPrintInvoice.InnerHtml += "<td style=\"width: 30%;\">To</td>";
                            //                    tblPrintInvoice.InnerHtml += "<td style=\"width: 70%;\">" + dtInvoice.Rows[0]["EmptyContainerDropLocation"] + "</td>";
                            //                tblPrintInvoice.InnerHtml += "</tr>";
                            //                tblPrintInvoice.InnerHtml += "<tr>";
                            //                    tblPrintInvoice.InnerHtml += "<td style=\"width: 30%;\">Bilty Number</td>";
                            //                    tblPrintInvoice.InnerHtml += "<td style=\"width: 70%;\">" + dtInvoice.Rows[0]["OrderNo"] + "</td>";
                            //                tblPrintInvoice.InnerHtml += "</tr>";
                            //                tblPrintInvoice.InnerHtml += "<tr>";
                            //                    tblPrintInvoice.InnerHtml += "<td style=\"width: 30%;\">Bilty Date</td>";
                            //                    tblPrintInvoice.InnerHtml += "<td style=\"width: 70%;\">" + dtInvoice.Rows[0]["OrderDate"] + "</td>";
                            //                tblPrintInvoice.InnerHtml += "</tr>";
                            //                tblPrintInvoice.InnerHtml += "<tr>";
                            //                    tblPrintInvoice.InnerHtml += "<td style=\"width: 30%;\">Truck Number</td>";
                            //                    tblPrintInvoice.InnerHtml += "<td style=\"width: 70%;\">" + dtInvoice.Rows[0]["VehicleRegNo"] + "</td>";
                            //                tblPrintInvoice.InnerHtml += "</tr>";
                            //                tblPrintInvoice.InnerHtml += "<tr>";
                            //                    tblPrintInvoice.InnerHtml += "<td style=\"width: 30%;\">Containers Qty</td>";
                            //                    tblPrintInvoice.InnerHtml += "<td style=\"width: 70%;\">" + dtInvoice.Rows.Count + "</td>";
                            //                tblPrintInvoice.InnerHtml += "</tr>";
                            //                tblPrintInvoice.InnerHtml += "<tr>";
                            //                    tblPrintInvoice.InnerHtml += "<td style=\"width: 30%;\">Description</td>";
                            //                    tblPrintInvoice.InnerHtml += "<td style=\"width: 70%;\">&nbsp;</td>";
                            //                tblPrintInvoice.InnerHtml += "</tr>";
                            //            tblPrintInvoice.InnerHtml += "<table>";
                            //        tblPrintInvoice.InnerHtml += "</td>";
                            //        tblPrintInvoice.InnerHtml += "<td>&nbsp;</td>";
                            //        tblPrintInvoice.InnerHtml += "<td>&nbsp;</td>";
                            //        tblPrintInvoice.InnerHtml += "<td>&nbsp;</td>";
                            //    tblPrintInvoice.InnerHtml += "</tr>";

                            //}
                        }
                        else
                        {
                            DataTable dtOrder = dml.GetBilty(OrderID);
                            Random rnd = new Random();
                            string InvoiceNumber = rnd.Next().ToString();
                            string CustomerInvoiceNumber = string.Empty;
                            string CustomerCompany = string.Empty;
                            double Total = 0;
                            DataTable dtExpenses = new DataTable();
                            dtExpenses.Columns.Add("Key");
                            dtExpenses.Columns.Add("Value");

                            if (dtOrder.Rows.Count > 0)
                            {
                                CustomerInvoiceNumber = txtCustomerInvoiceNo.Text;
                                CustomerCompany = dtOrder.Rows[0]["BillToCustomerCompany"].ToString();
                            }

                            DataTable dtContainers = dml.GetBiltyContainersByOrder(OrderID);
                            if (dtContainers.Rows.Count > 0)
                            {
                                foreach (DataRow _drContainers in dtContainers.Rows)
                                {
                                    Int64 ContainerID = Convert.ToInt64(_drContainers["OrderconsignmentID"]);

                                    double ContainerFreight = Convert.ToDouble(_drContainers["Rate"]);
                                    Total += ContainerFreight;

                                    DataTable dtContainerExpenses = dml.GetExpenses(ContainerID);
                                    if (dtContainerExpenses.Rows.Count > 0)
                                    {
                                        foreach (DataRow _drExpenses in dtContainerExpenses.Rows)
                                        {
                                            double ContainerExpense = Convert.ToDouble(_drExpenses["Amount"]);
                                            Total += ContainerExpense;
                                        }
                                    }
                                    
                                }
                            }

                            Int64 InvoiceID = dml.InsertOrderInvoices(InvoiceNumber, CustomerInvoiceNumber, CustomerCompany, OrderID, Total, LoginID);
                            if (InvoiceID > 0)
                            {
                                dml.InvoiceToOrder(OrderID);

                                CompanyDML dmlComp = new CompanyDML();
                                string CustomerCode = string.Empty;
                                string CustomerName = string.Empty;
                                Int64 CustomerID = 0;
                                DataTable dtCompany = dmlComp.GetCompany(CustomerCompany);
                                if (dtCompany.Rows.Count > 0)
                                {
                                    CustomerCode = dtCompany.Rows[0]["CompanyCode"].ToString().Trim();
                                    CustomerName = dtCompany.Rows[0]["CompanyName"].ToString().Trim();
                                    CustomerID = Convert.ToInt64(dtCompany.Rows[0]["CompanyID"]);
                                }                                
                                string CustomerAccName = CustomerName + "|" + CustomerCode;
                                AccountsDML dmlAcc = new AccountsDML();
                                DataTable dtCustAccountCheck = dmlAcc.GetAccounts(CustomerAccName);
                                if (dtCustAccountCheck.Rows.Count <= 0)
                                    dmlAcc.CreateAccount(CustomerAccName);
                                double Debit = Total;
                                double Credit = 0;
                                double Balance = 0;
                                DataTable dtCustAccount = dmlAcc.GetInAccounts(CustomerAccName);
                                if (dtCustAccount.Rows.Count > 0)
                                {
                                    Balance = Convert.ToDouble(dtCustAccount.Rows[dtCustAccount.Rows.Count - 1]["Balance"]);
                                }
                               
                                OrderDML dmlOrderCont = new OrderDML();
                                DataTable dtContainer = dmlOrderCont.GetBiltyContainersByOrder(OrderID);
                                if (dtContainer.Rows.Count > 0)
                                {
                                    foreach (DataRow _drContainers in dtContainer.Rows)
                                    {
                                        Int64 ContainerID = Convert.ToInt64(_drContainers["OrderConsignmentID"]);
                                        string Container = dtContainer.Rows.Count > 0 ? (", Container# " + _drContainers["ContainerNo"].ToString()) : string.Empty;
                                        double ContainerRate = Convert.ToDouble(_drContainers["Rate"]);
                                        Balance = Balance + ContainerRate - Credit;
                                        dmlAcc.InsertInAccount(CustomerAccName, CustomerID, "Container booking" + Container + " against Order# " + _drContainers["OrderNo"].ToString() + "", ContainerRate, Credit, Balance, LoginID);

                                        DataTable dtExpense = dmlOrderCont.GetExpenses(ContainerID);
                                        if (dtExpense.Rows.Count > 0)
                                        {
                                            foreach (DataRow _drExpense in dtExpense.Rows)
                                            {
                                                double Expense = Convert.ToDouble(_drExpense["Amount"]);
                                                Balance = Balance + Expense - Credit;
                                                dmlAcc.InsertInAccount(CustomerAccName, CustomerID, "Expense paid for " + _drExpense["ExpensesTypeName"].ToString() + " against Container# " + _drContainers["ContainerNo"].ToString() + " in Bilty# " + _drContainers["OrderNo"].ToString(), Expense, 0, Balance, LoginID);
                                            }
                                        }
                                    }
                                }


                                ShowInvoice(InvoiceNumber);
                            }
                        }
                        modalInvoicePrint.Show();
                        
                    }
                    catch (Exception ex)
                    {
                        notification("Error", "Error generating invoice, due to: " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error confirming, due to: " + ex.Message);
            }
        }

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
                    // Int64 VehicleID = ddlVehicleNo.SelectedIndex == 0 ? 0 : Convert.ToInt64(ddlVehicleNo.SelectedItem.Value);
                    string VehicleRegNo = txtVehicleRegNo.Text.Trim();
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
                    double VehicleRate = txtVehicleRate.Text == string.Empty ? 0 : Convert.ToDouble(txtVehicleRate.Text);

                    OrderDML dml = new OrderDML();
                    if (hfSelectedOrderVehicle.Value == string.Empty)
                    {
                        Int64 OrderVehicleID = dml.InsertOrderVehicleInfo(OrderID, VehicleType, VehicleRegNo, ContactNo, BrokerID, Driver, Driverfather, DriverNIC, License, ContactNo, VehicleReportingDateTime, VehicleInDateTime, VehicleOutDateTime, VehicleRate);
                        if (OrderVehicleID > 0)
                        {
                            //try
                            //{
                            //    string BrokerAccountName = ddlBroker.SelectedItem.Text.Trim() + "|" + GetBrokerCodeByID(Convert.ToInt64(ddlBroker.SelectedItem.Value));
                            //    string BiltyNo = GetOrderNoOrderID(OrderID);
                            //    AccountsDML dmlAccounts = new AccountsDML();
                            //    DataTable dtAccounts = dmlAccounts.GetAccounts(BrokerAccountName);
                            //    if (dtAccounts.Rows.Count > 0)
                            //    {
                            //        AddAccountDebit(Convert.ToInt64(ddlBroker.SelectedItem.Value), "Vehicle# " + VehicleRegNo + " booked against Order# " + BiltyNo, VehicleRate, BrokerAccountName);
                            //    }
                            //    else
                            //    {
                            //        dmlAccounts.CreateAccount(BrokerAccountName);
                            //        AddAccountDebit(Convert.ToInt64(ddlBroker.SelectedItem.Value), "Vehicle# " + VehicleRegNo + " booked against Order# " + BiltyNo, VehicleRate, BrokerAccountName);
                            //    }
                            //}
                            //catch (Exception ex)
                            //{
                            //    VehicleNotification("Error", "Error recording croker account, due to: " + ex.Message);
                            //}

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
                        DataTable dtAssignedVehicles = dml.GetAssignedVehicleInOrder(OrderID, VehicleRegNo);
                        if (dtAssignedVehicles.Rows.Count > 1)
                        {
                            VehicleNotification("Error", "Specified vehicle already assigned in this bilty, try to change Registration No.");
                            txtVehicleRegNo.Focus();
                        }
                        else
                        {
                            Int64 OrderVehicleID = Convert.ToInt64(hfSelectedOrderVehicle.Value);
                            dml.UpdateOrderVehicle(OrderVehicleID, VehicleType, VehicleRegNo, VehicleContactNo, BrokerID, VehicleRate, Driver, Driverfather, DriverNIC, License, ContactNo);

                            dml.UpdateAssignedVehiclesOfContainer(OrderID, VehicleRegNo);
                            

                            VehicleNotification("Success", "Order Vehicle updated successfully");
                            pnlBiltyVehicleInputs.Visible = false;
                            ClearDriverVehicleFields();
                            GetBiltyVehicles(OrderID);
                            GetBilties();
                        }
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
            try
            {
                divContainerExpense.Visible = false;
                divContainerDetails.Visible = true;

                pnlBiltyContainerInputs.Visible = false;
                pnlContainerExpensesInput.Visible = false;

                lnkCloseContainerExpense.Visible = false;
                lnkCloseBiltyContainer.Visible = true;


                GetBilties();
                modalContainers.Hide();
            }
            catch (Exception ex)
            {
                ContainerNotification("Error", "Error closing bilty container, due to: " + ex.Message);
            }
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
                    Int64 PickupLocationID = Convert.ToInt64(ddlContainerPickup.SelectedItem.Value);
                    string CotnainerPickup = ddlContainerPickup.SelectedItem.Text == string.Empty ? string.Empty : ddlContainerPickup.SelectedItem.Text;
                    Int64 DropoffLocationID = Convert.ToInt64(ddlContainerDropoff.SelectedItem.Value);
                    string CotnainerDropoff = ddlContainerDropoff.SelectedItem.Text == string.Empty ? string.Empty : ddlContainerDropoff.SelectedItem.Text;
                    //Int64 ContainerRate = txtContainerRate.Text == string.Empty ? 0 : Convert.ToInt64(txtContainerRate.Text);
                    string Remarks = txtContainerRemarks.Text;
                    string Vehicle = ddlAssignedVehicle.Items.Count > 0 ? (ddlAssignedVehicle.SelectedIndex == 0 ? string.Empty : ddlAssignedVehicle.SelectedItem.Text) : string.Empty ;
                    double ContainerRate = txtContainerRate.Text == string.Empty ? 0 : Convert.ToDouble(txtContainerRate.Text.Trim());
                    OrderDML dml = new OrderDML();
                    if (hfSelectedOrderContainer.Value == string.Empty)
                    {

                        Int64 OrderContainerID = dml.InsertOrderContainerInfo(OrderID, ContainerType, ContainerNo, Weight, PickupLocationID, CotnainerPickup, DropoffLocationID, CotnainerDropoff, Remarks, Vehicle, ContainerRate);
                        if (OrderContainerID > 0)
                        {
                            try
                            {
                                DataTable dtOrder = dml.GetBillToCustomerByBilty(OrderID);
                                if (dtOrder.Rows.Count > 0)
                                {
                                    //Int64 CustomerCompanyID = Convert.ToInt64(dtOrder.Rows[0]["CompanyID"]);
                                    //string CompanyName = dtOrder.Rows[0]["CompanyName"].ToString();
                                    //string CompanyCode = dtOrder.Rows[0]["CompanyCode"].ToString();
                                    //string CompanyAccount = CompanyName + "|" + CompanyCode;
                                    //string BiltyNo = dtOrder.Rows[0]["BiltyNo"].ToString();

                                    //AccountsDML dmlAccounts = new AccountsDML();
                                    //DataTable dtAccounts = dmlAccounts.GetAccounts(CompanyAccount);
                                    //if (dtAccounts.Rows.Count > 0)
                                    //{
                                    //    AddAccountDebit(CustomerCompanyID, "Container# " + ContainerNo + " booked against Order# " + BiltyNo, ContainerRate, CompanyAccount);
                                    //}
                                    //else
                                    //{
                                    //    dmlAccounts.CreateAccount(CompanyAccount);
                                    //    AddAccountDebit(CustomerCompanyID, "Container# " + ContainerNo + " booked against Order# " + BiltyNo, ContainerRate, CompanyAccount);
                                    //}
                                }
                            }
                            catch (Exception ex)
                            {
                                VehicleNotification("Error", "Error adding account, due to: " + ex.Message);
                            }

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
                        dml.UpdateOrderContainerInfo(OrderContainerID, OrderID, ContainerType, ContainerNo, Weight, PickupLocationID, CotnainerPickup, DropoffLocationID, CotnainerDropoff, Remarks, Vehicle, ContainerRate);
                        ContainerNotification("Success", "Conatiner updated successfully");
                        ClearContainerFields();
                        GetBilties();
                        GetBiltyContainers(OrderID);
                        pnlBiltyContainerInputs.Visible = false;

                        modalContainers.Show();
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
                    DataTable dtOrder = dml.GetBilty(OrderID);
                    Int64 BrokerID = 0;
                    string Brokername = string.Empty;
                    string BrokerCode = string.Empty;

                    string VehicleRegNos = string.Empty;

                    string BrokerAccName = string.Empty;
                    if (dtOrder.Rows.Count > 0)
                    {
                        DataTable dtVehicles = dml.GetBiltyVehiclesByOrder(OrderID);
                        if (dtVehicles.Rows.Count > 0)
                        {
                            
                            BrokerID = Convert.ToInt64(dtVehicles.Rows[0]["BrokerID"]);
                            BrokersDML dmlBroker = new BrokersDML();
                            foreach (DataRow _drVehicles in dtVehicles.Rows)
                            {
                                VehicleRegNos += _drVehicles["VehicleRegNo"].ToString() + ", ";
                            }
                            VehicleRegNos = VehicleRegNos.Substring(0, VehicleRegNos.Length - 2);
                            DataTable dtBroker = dmlBroker.GetBroker(BrokerID);
                            if (dtBroker.Rows.Count > 0)
                            {

                                
                                BrokerCode = dtBroker.Rows[0]["Code"].ToString();
                                Brokername = dtBroker.Rows[0]["Name"].ToString();

                                BrokerAccName = Brokername + "|" + BrokerCode;
                            }

                            
                        }
                        double Credit = Convert.ToDouble(dtOrder.Rows[0]["BalanceFreight"].ToString().Replace("-", string.Empty));



                        AccountsDML dmlAcc = new AccountsDML();
                        DataTable dtCustAccountCheck = dmlAcc.GetAccounts(BrokerAccName);
                        if (dtCustAccountCheck.Rows.Count <= 0)
                            dmlAcc.CreateAccount(BrokerAccName);
                        double Debit = 0;
                        //double Credit = 0;
                        double Balance = 0;
                        DataTable dtCustAccount = dmlAcc.GetInAccounts(BrokerAccName);
                        if (dtCustAccount.Rows.Count > 0)
                        {
                            Balance = Convert.ToDouble(dtCustAccount.Rows[dtCustAccount.Rows.Count - 1]["Balance"]);
                        }
                        Balance = Balance + Debit - Credit;
                        //CompanyDML dmlComp = new CompanyDML();
                        //DataTable dtCompany = dmlComp.GetCompany(Broker, CustomerName);
                        //Int64 CustomerID = dtCompany.Rows.Count > 0 ? Convert.ToInt64(dtCompany.Rows[0]["CompanyID"].ToString()) : 0;

                        //OrderDML dmlOrderCont = new OrderDML();
                        //DataTable dtContainer = dmlOrderCont.GetBiltyContainers(ContainerID);
                        //string Container = dtContainer.Rows.Count > 0 ? (", Container# " + dtContainer.Rows[0]["ContainerNo"].ToString()) : string.Empty;
                        dmlAcc.InsertInAccount(BrokerAccName, BrokerID, "Borrowed vehicles '" + VehicleRegNos + "' against Order# " + dtOrder.Rows[0]["OrderNo"].ToString() + "", Debit, Credit, Balance, LoginID);




                        //AccountsDML dmlAcc = new AccountsDML();

                        //dmlAcc.InsertInAccount()
                    }
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
                //else if (hfLoadingDate.Value.Trim() == string.Empty)
                //{
                //     if (txtLoadingDate.Text.Trim() == string.Empty)
                //    {
                //        BiltyNotification("Error", "Please enter Loading Date");
                //        txtLoadingDate.Focus();
                //    }
                //}
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
                    try
                    {
                        LocationDML dmlLocation = new LocationDML();
                        DataTable dtLocation = dmlLocation.GetLocationByCode(PickupLocationString[4].ToString().Trim());
                        if (dtLocation.Rows.Count > 0)
                        {
                            PickupLocationID = Convert.ToInt64(dtLocation.Rows[0]["PickDropID"].ToString());
                        }
                    }
                    catch (Exception ex)
                    {

                        
                    }
                    finally
                    {
                        modalBilty.Show();
                    }
                    string[] DropoffLocationString = ddlSearchDropLocation.Text.Split('|');

                    try
                    {
                        LocationDML dmlLocation = new LocationDML();
                        DataTable dtLocation = dmlLocation.GetLocationByCode(PickupLocationString[4].ToString().Trim());
                        if (dtLocation.Rows.Count > 0)
                        {
                            DropoffLocationID = Convert.ToInt64(dtLocation.Rows[0]["PickDropID"].ToString());
                        }
                    }
                    catch (Exception ex)
                    {

                       
                    }
                    finally
                    {
                        modalBilty.Show();
                    }



                    // string ShippingLineID = ddlShippingLine.Items.Count > 0 ? (ddlShippingLine.SelectedIndex == 0 ? string.Empty : ddlShippingLine.SelectedItem.Text) : string.Empty;
                    Int64 ShippingLineID = ddlShippingLine.SelectedIndex == 0 ? 0 : Convert.ToInt64(ddlShippingLine.SelectedItem.Value);
                    //Int64 ShippingLine = ddlShippingLine.Items.Count > 0 ? (Convert.ToInt64(ddlShippingLine.SelectedIndex == 0 ? string.Empty : ddlShippingLine.SelectedItem.Text); 

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
                    string LoadingDate = string.Empty;
                    if (txtLoadingDate.Text != string.Empty)
                    {
                        LoadingDate = txtLoadingDate.Text.Trim();
                    }
                    else
                    {
                        LoadingDate = hfLoadingDate.Value.Trim();
                    }
                    string BiltyDate = string.Empty;
                    if(txtBiltyDate.Text !=string.Empty)
                    {
                       BiltyDate = txtBiltyDate.Text.Trim();
                    }
                    else
                    {
                        BiltyDate = hfBiltyDate.Value.Trim();
                    }
                    OrderDML dml = new OrderDML();

                    dml.UpdateOrder(OrderID, BiltyNo, BiltyDate, SenderCompanyID, SenderDepartment, ReceiverCompanyID, ReceiverDepartment, CustomerCompanyID,
                        CustomerDepartment, PaymentType, PickupLocationID, DropoffLocationID,LoadingDate, ShippingLineID, AdditionalWeight, ActualWeight, BiltyFreight, Freight,
                        PartyCommission, AdvanceFreight, FactoryAdvance, DieselAdvance, AdvanceAmount, TotalAdvance, BalanceFreight, LoginID);

                    double ExpenseAmount = 0;
                    
                    DataTable dtShippingLine = dml.GetActiveShippingLine(ShippingLineID);
                    if (dtShippingLine.Rows.Count > 0)
                    {
                        ExpenseAmount += Convert.ToDouble(dtShippingLine.Rows[0]["LiftOffLiftOnCharges"]); 
                    }


                    DataTable dtContainer = dml.GetBiltyContainersByOrder(OrderID);
                    for (int i = 0; i < dtContainer.Rows.Count; i++)
                    {
                        Int64 ContainerID = Convert.ToInt64(dtContainer.Rows[i]["OrderConsignmentID"]);

                        DataTable dtContainerExpenses = dml.GetExpenses(ContainerID);
                        if (dtContainerExpenses.Rows.Count > 0)
                        {
                            for (int j = 0; j < dtContainerExpenses.Rows.Count; j++)
                            {
                                Int64 ContainerExpenseID = Convert.ToInt64(dtContainerExpenses.Rows[j]["ContainerExpenseID"]);
                                string ExpenseType = dtContainerExpenses.Rows[j]["ExpensesTypeName"].ToString();
                                //Int64 ContainerExpenseID = Convert.ToInt64(dtContainerExpenses.Rows[i]["ContainerExpenseID"]);
                                if (ExpenseType.Replace(" ", string.Empty) == "LiftOffLiftOn") 
                                {
                                    dml.DeleteContainerExpense(ContainerExpenseID);
                                }
                            }                            

                        }

                        //Saving new expenses if LOLO expense saved to selected shipping line 
                        if (ExpenseAmount > 0)
                        {
                            Int64 ExpenseTypeID = 0;
                            DataTable dtExpTypes = dml.GetExpenseTypes("Lift Off Lift On");
                            if (dtExpTypes.Rows.Count > 0)
                            {
                                ExpenseTypeID = Convert.ToInt64(dtExpTypes.Rows[0]["ExpensesTypeID"]);
                            }
                            dml.InsertContainerExpense(ContainerID, ExpenseTypeID, Convert.ToInt64(ExpenseAmount));
                        }                        
                            
                    }
                   

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
                pnlBiltyContainers.Visible = true;
            }
            catch (Exception ex)
            {
                BiltyNotification("Error", "Error closing bilty popup, due to: " + ex.Message);
                modalBilty.Show();
            }
        }

        protected void lnkCloseInvoices_Click(object sender, EventArgs e)
        {
            try
            {
                hfSelectedOrder.Value = string.Empty;
                modalInvoice.Hide();
            }
            catch (Exception ex)
            {
                InvoiceNotification("Error", "Error closing invoice popup, due to: " + ex.Message);
                modalInvoice.Show();
            }
        }

        protected void lnkAddContainers_Click(object sender, EventArgs e)
        {
            try
            {
                string SelectedContainers = string.Empty;
                foreach (ListItem _cb in cbOrderContainers.Items)
                {
                    if (_cb.Selected == true)
                    {
                        SelectedContainers += _cb.Value + ",";
                    }
                }
                Int64 OrderID = Convert.ToInt64(hfSelectedOrder.Value);
                InvoiceDML dml = new InvoiceDML();
                DataTable dtOrder = dml.GetOrder(OrderID);
                if (dtOrder.Rows.Count > 0)
                {
                    lblOrderNo.Text = dtOrder.Rows[0]["OrderNo"].ToString();
                    lblOrderDate.Text = dtOrder.Rows[0]["RecordedDate"].ToString();
                    lblBilltoCustomer.Text = dtOrder.Rows[0]["CustomerCompany"].ToString();
                    //lblBilltoCustomerAddress.Text = dtOrder.Rows[0]["CAddress"].ToString();

                    lblSenderCompanyName.Text = dtOrder.Rows[0]["Sender"].ToString();
                    lblSenderAddress.Text = dtOrder.Rows[0]["SAddress"].ToString();

                    lblReceiverCompanyName.Text = dtOrder.Rows[0]["Receiver"].ToString();
                    lblReceiverAddress.Text = dtOrder.Rows[0]["RAddress"].ToString();


                }
                //if (dtOrder.Rows.Count > 0)
                //{
                //    if (SelectedContainers != string.Empty)
                //    {

                //    }
                //}
                //else
                //{

                //}
            }
            catch (Exception ex)
            {
                InvoiceNotification("Error", "Error adding containers, due to: " + ex.Message);
            }
            finally
            {
                modalInvoice.Show();
            }
        }

        protected void lnkCancelSaveContainerExpense_Click(object sender, EventArgs e)
        {
            try
            {
                ddlExpenseType.ClearSelection();
                txtExpenseAmount.Text = string.Empty;

                pnlContainerExpensesInput.Visible = false;
            }
            catch (Exception ex)
            {
                ContainerNotification("Error", "Error cancelling add container expense, due to: " + ex.Message);
            }
            finally
            {
                modalContainers.Show();
            }
        }

        //protected void lnkSaveContainerExpense_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (ddlExpenseType.SelectedIndex == 0)
        //        {
        //            ContainerNotification("Error", "Please select Expense type");
        //            ddlExpenseType.Focus();
        //        }
        //        else
        //        {
        //            Int64 ContainerID = Convert.ToInt64(ddlExpenseContainer.SelectedItem.Value);
        //            Int64 ExpenseTypeID = Convert.ToInt64(ddlExpenseType.SelectedItem.Value);
        //            Int64 Amount = Convert.ToInt64(txtExpenseAmount.Text);
        //            OrderDML dml = new OrderDML();
        //            Int64 ExpenseID = dml.InsertContainerExpense(ContainerID, ExpenseTypeID, Amount);
        //            if (ExpenseID > 0)
        //            {
        //                ddlExpenseType.ClearSelection();
        //                txtExpenseAmount.Text = string.Empty;

        //                ContainerNotification("Success", "Expense added successfully");

        //                DataTable dtExpense = dml.GetExpenses(ContainerID);
        //                if (dtExpense.Rows.Count > 0)
        //                {
        //                    gvContainerExpense.DataSource = dtExpense;
        //                }
        //                else
        //                {
        //                    gvContainerExpense.DataSource = null;
        //                }
        //                gvContainerExpense.DataBind();
        //                Int64 OrderID = Convert.ToInt64(hfSelectedOrder.Value);
        //                //DataTable dtContainers = dml.GetBiltyContainers(ContainerID);
        //                //if (dtContainers.Rows.Count > 0)
        //                //{
        //                //    gvContainer.DataSource = dtContainers;
        //                //}
        //                //else
        //                //{
        //                //    gvContainer.DataSource = null;
        //                //}
        //                //gvContainer.DataBind();
        //                GetBiltyContainers(OrderID);
        //            }
        //            else
        //            {
        //                ContainerNotification("Error", "Expense not added, try again or contact Vals IT Team");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ContainerNotification("Error", "Error adding container expense, due to: " + ex.Message);
        //    }
        //    finally
        //    {
        //        modalContainers.Show();
        //    }
        //}
        protected void lnkSaveContainerExpense_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlExpenseType.SelectedIndex == 0)
                {
                    ContainerNotification("Error", "Please select Expense type");
                    ddlExpenseType.Focus();
                }
                else
                {
                    Int64 ContainerID = Convert.ToInt64(ddlExpenseContainer.SelectedItem.Value);
                    Int64 ExpenseTypeID = Convert.ToInt64(ddlExpenseType.SelectedItem.Value);
                    Int64 Amount = Convert.ToInt64(txtExpenseAmount.Text);
                    bool PaidByDriver = ChkPaid.Checked ? true : false;
                    OrderDML dml = new OrderDML();
                    if (hfContainerExpense.Value == string.Empty)
                    {
                        Int64 ExpenseID = dml.InsertContainerExpense(ContainerID, ExpenseTypeID, Amount, PaidByDriver);
                        

                        if (ExpenseID > 0)
                        {
                            ddlExpenseType.ClearSelection();
                            txtExpenseAmount.Text = string.Empty;
                            ContainerNotification("Success", "Expense added successfully");
                        }
                        else
                        {
                            ContainerNotification("Error", "Expense not added, try again or contact Vals IT Team");
                        }
                    }
                    else
                    {
                        Int64 ContainerExpenseID = Convert.ToInt64(hfContainerExpense.Value);
                        dml.UpdateContainerExpense(ContainerExpenseID, ContainerID, ExpenseTypeID, Amount);
                        ContainerNotification("Success", "Expense Updated successfully");
                    }

                    Int64 OrderID = Convert.ToInt64(hfSelectedOrder.Value);
                    GetContainerExpense(ContainerID);
                    GetBiltyContainers(OrderID);

                }
            }
            catch (Exception ex)
            {
                ContainerNotification("Error", "Error adding container expense, due to: " + ex.Message);
            }
            finally
            {
                modalContainers.Show();
            }
        }

        protected void lnkAddExpense_Click(object sender, EventArgs e)
        {
            try
            {
                pnlContainerExpensesInput.Visible = true;
            }
            catch (Exception ex)
            {
                ContainerNotification("Error", "Error enabling expenses input, due to: " + ex.Message);
            }
            finally
            {
                modalContainers.Show();
            }
        }

        protected void lnkCloseContainerExpense_Click(object sender, EventArgs e)
        {
            try
            {
                divContainerExpense.Visible = false;
                divContainerDetails.Visible = true;

                pnlBiltyContainerInputs.Visible = false;
                pnlContainerExpensesInput.Visible = false;

                lnkCloseContainerExpense.Visible = false;
                lnkCloseBiltyContainer.Visible = true;
            }
            catch (Exception ex)
            {
                ContainerNotification("Error", "Error closing container expenses, due to: " + ex.Message);
            }
            finally
            {
                modalContainers.Show();
            }
        }

        protected void lnkCancelSaveContainerReceiving_Click(object sender, EventArgs e)
        {
            try
            {
                int rowIndex = Convert.ToInt32(hfSelectedCotnainerReceiving.Value);
                DropDownList ddlReceivingStatus = gvContainer.Rows[rowIndex].FindControl("ddlReceiving") as DropDownList;
                ddlReceivingStatus.ClearSelection();
                ddlReceivingStatus.Items.FindByText("Pending").Selected = true;

                divContainerDetails.Visible = true;
                pnlContainerReceiving.Visible = false;

                lnkCloseBiltyContainer.Visible = true;
                lnkCloseContainerReceiving.Visible = false;

                hfSelectedCotnainerReceiving.Value = string.Empty;

                txtContainerReceivedBy.Text = string.Empty;
                txtContainerReceivingDate.Text = string.Empty;
                txtContainerReceivingTime.Text = string.Empty;
            }
            catch (Exception ex)
            {
                ContainerNotification("Error", "Error cancel saving container receiving, due to: " + ex.Message);
            }
            finally
            {
                modalContainers.Show();
            }
        }

        public void InsertInBrokerAccount(string _AccountName, Int64 _AccounteeID, string Description, double Debit, double Credit)
        {
            try
            {
                AccountsDML dmlAccounts = new AccountsDML();
                
                double Balance = 0;
                DataTable dtBrokerAccount = dmlAccounts.GetInAccounts(_AccountName);
                if (dtBrokerAccount.Rows.Count > 0)
                {
                    Balance = Convert.ToDouble(dtBrokerAccount.Rows[dtBrokerAccount.Rows.Count - 1]["Balance"]);
                }
                Balance = Balance + Debit - Credit;
                dmlAccounts.InsertInBrokerAccount(_AccountName, _AccounteeID, Description, Debit, Credit, Balance, LoginID);
            }
            catch (Exception ex)
            {
                notification("Error", "Error inserting in broker account, due to: " + ex.Message);
            }
        }

        public void InsertInPatrolPumpAccount(string _AccountName, Int64 _AccounteeID, string Description, double Debit, double Credit)
        {
            try
            {
                AccountsDML dmlAccounts = new AccountsDML();

                string CurrentDate = DateTime.Now.ToShortDateString();
                double Balance = 0;
                DataTable dtPatrolPumpAccount = dmlAccounts.GetInAccounts(_AccountName);
                if (dtPatrolPumpAccount.Rows.Count > 0)
                {
                    //Balance = Convert.ToDouble(dtPatrolPumpAccount.Rows[dtPatrolPumpAccount.Rows.Count - 1]["Balance"]);
                    Balance = Convert.ToDouble(dtPatrolPumpAccount.Rows.Count > 0 ? (dtPatrolPumpAccount.Rows[dtPatrolPumpAccount.Rows.Count > 0 ? dtPatrolPumpAccount.Rows.Count - 1 : 0]["Balance"]) : 0);
                }
                Balance = Balance + Debit - Credit;
                dmlAccounts.InsertInPatrolPumpAccount(_AccountName, _AccounteeID, Description, Debit, Credit, Balance, CurrentDate, "", "",  LoginID);
            }
            catch (Exception ex)
            {
                notification("Error", "Error inserting in patrol pump account, due to: " + ex.Message);
            }
        }

        protected void lnkSaveContainerReceiving_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtContainerReceivedBy.Text == string.Empty)
                {
                    ContainerNotification("Error", "Please enter receiver name");
                    txtContainerReceivedBy.Focus();
                }
                else if (txtContainerReceivingDate.Text == string.Empty)
                {
                    ContainerNotification("Error", "Please enter receiving date");
                    txtContainerReceivingDate.Focus();
                }
                else 
                {
                    int rowIndex = Convert.ToInt32(hfSelectedCotnainerReceiving.Value);
                    Int64 OrderContainerID = Convert.ToInt64(gvContainer.DataKeys[rowIndex]["OrderConsignmentID"]);
                    Int64 OrderID = Convert.ToInt64(hfSelectedOrder.Value);
                    string ReceivedBy = txtContainerReceivedBy.Text;
                    string ReceivedDateTime = txtContainerReceivingDate.Text + (txtContainerReceivingTime.Text == string.Empty ? string.Empty : " " + txtContainerReceivingTime.Text);
                    double TotalContainerFreight = 0;
                    OrderDML dml = new OrderDML();
                    dml.UpdateContainerReceivingStatus(OrderContainerID, 1, ReceivedBy, ReceivedDateTime);

                    DataTable dtContainers = dml.GetBiltyContainersByOrder(OrderID);
                    if (dtContainers.Rows.Count > 0)
                    {
                        bool allReceived = true;
                        foreach (DataRow _drContainers in dtContainers.Rows)
                        {
                            if (_drContainers["ContainerStatus"].ToString() == "False")
                            {
                                allReceived = false;
                            }
                            double ContainerRate = (double)_drContainers["Rate"];
                            TotalContainerFreight += ContainerRate;
                        }

                        if (allReceived == true)
                        {
                            DataTable dtOrderVehicle = dml.GetBiltyVehiclesByOrder(OrderID);
                            if (dtOrderVehicle.Rows.Count > 0)
                            {
                                string PaidToPay = dtOrderVehicle.Rows[0]["PaidToPay"].ToString();
                                string DriverName = dtOrderVehicle.Rows[0]["DriverName"].ToString();
                                Int64 OrderVehicleID = Convert.ToInt64(dtOrderVehicle.Rows[0]["OrderVehicleID"]);
                                double VehicleFreight = dtOrderVehicle.Rows[0]["Rate"].ToString() == string.Empty ? 0 : Convert.ToDouble(dtOrderVehicle.Rows[0]["Rate"]);
                                string VehicleNo = dtOrderVehicle.Rows[0]["VehicleRegNo"].ToString();
                                double TotalAdvances = 0;
                                string Receiver = dtOrderVehicle.Rows[0]["Receiver"].ToString();
                                string OrderNo = dtOrderVehicle.Rows[0]["OrderNo"].ToString();
                                Int64 CustomerCompanyID = Convert.ToInt64(dtOrderVehicle.Rows[0]["CustomerCompanyID"]);
                                double FactoryAdvance = 0;
                                

                                DataTable dtOrderadvances = dml.GetOrderAdvancesByOrderID(OrderID);
                                if (dtOrderadvances.Rows.Count > 0)
                                {
                                    foreach (DataRow _drAdvances in dtOrderadvances.Rows)
                                    {
                                        string AdvanceAgainst = _drAdvances["AdvanceAgainst"].ToString();
                                        double AdvanceAmount = Convert.ToDouble(_drAdvances["AdvanceAmount"]);
                                        //string OrderNo = _drAdvances["OrderNo"].ToString();
                                        if (AdvanceAgainst != "Advance Freight")
                                        {
                                            TotalAdvances += AdvanceAmount;
                                        }

                                        if (AdvanceAgainst == "Factory Advance")
                                        {
                                            FactoryAdvance += AdvanceAmount;
                                        }
                                    }
                                }

                                if (FactoryAdvance > 0)
                                {
                                    CustomerTransaction(CustomerCompanyID, FactoryAdvance, "Credit", "Paid to driver against Vehicle # " + VehicleNo + " as factory advance ");
                                }

                                //Debitting To broker
                                try
                                {
                                    DataTable dtBroker = dml.GetBrokerByOrder(OrderID);
                                    if (dtOrderVehicle.Rows.Count > 0)
                                    {
                                        AccountsDML dmlAccounts = new AccountsDML();

                                        Int64 BrokerID = Convert.ToInt64(dtBroker.Rows[0]["ID"]);
                                        string BrokerCode = dtBroker.Rows[0]["Code"].ToString().Trim();
                                        string BrokerName = dtBroker.Rows[0]["Name"].ToString().Trim();
                                        string BrokerAccountName = BrokerName + "|" + BrokerCode;
                                        //string OrderNo = dtBroker.Rows[0]["OrderNo"].ToString();
                                        string OrderDate = dtBroker.Rows[0]["RecordedDate"].ToString();
                                        string AssignedVehicle = dtOrderVehicle.Rows[0]["VehicleRegNo"].ToString();
                                        string Description = "Vehicle " + AssignedVehicle + " borrow against OrderNo " + OrderNo + " ON " + OrderDate;

                                        GetSetBrokerAccount(BrokerAccountName);

                                        double Credit = 0;
                                        double Debit = VehicleFreight;
                                        InsertInBrokerAccount(BrokerAccountName, BrokerID, Description, Debit, Credit);



                                        try
                                        {

                                            DataTable dtOrderContainer = dml.GetOrderContainerByOrderID(OrderID);
                                            if (dtOrderContainer.Rows.Count > 0)
                                            {
                                                foreach (DataRow _drOrderContainer in dtOrderContainer.Rows)
                                                {
                                                    Int64 ContainerID = Convert.ToInt64(_drOrderContainer["OrderConsignmentID"]);

                                                    DataTable dtDriverPaidContainerExpenses = dml.GetDriverPaidExpenses(ContainerID);
                                                    if (dtDriverPaidContainerExpenses.Rows.Count > 0)
                                                    {
                                                        foreach (DataRow _drDriverPAidExpenses in dtDriverPaidContainerExpenses.Rows)
                                                        {
                                                            string ExpenseTypeName = _drDriverPAidExpenses["ExpensesTypeName"].ToString();
                                                            double Amount = Convert.ToDouble(_drDriverPAidExpenses["Amount"]);

                                                            Debit = Amount;
                                                            Credit = 0;
                                                            Description = "Expense " + ExpenseTypeName + " has been paid by driver";
                                                            InsertInBrokerAccount(BrokerAccountName, BrokerID, Description, Debit, Credit);
                                                        }
                                                    }
                                                }
                                            }

                                            
                                        }
                                        catch (Exception ex)
                                        {
                                            ContExpenseNotification("Error", "Error adding driver paid expense to broker ledger, due to: " + ex.Message);
                                        }

                                        if (PaidToPay == "To-Pay")
                                        {
                                            Credit = TotalContainerFreight;
                                            Debit = 0;
                                            Description = "Driver " + DriverName + " has received Bilty Frieght of To-Pay Bilty# " + OrderNo + " ON " + OrderDate + " from Baba";
                                            InsertInBrokerAccount(BrokerAccountName, BrokerID, Description, Debit, Credit);
                                        }

                                        

                                        if (dtOrderadvances.Rows.Count > 0)
                                        {
                                            foreach (DataRow _drAdvances in dtOrderadvances.Rows)
                                            {
                                                string AdvanceType = _drAdvances["AdvanceAgainst"].ToString();
                                                string AdvanceDescription = "Paid advance for " + AdvanceType + " against Vehicle " + AssignedVehicle + " OrderNo " + OrderNo;
                                                double AdvanceDebit = 0;
                                                double AdvanceCredit = Convert.ToDouble(_drAdvances["AdvanceAmount"]);
                                                if (AdvanceType != "Advance Freight")
                                                {
                                                    InsertInBrokerAccount(BrokerAccountName, BrokerID, AdvanceDescription, AdvanceDebit, AdvanceCredit);
                                                    if (AdvanceType == "Diesel Advance")
                                                    {
                                                        string RefuellingDate = _drAdvances["RecordedDate"].ToString() == string.Empty ? _drAdvances["CreatedDate"].ToString() : _drAdvances["RecordedDate"].ToString();
                                                        Int64 PatrolPumpID = Convert.ToInt64(_drAdvances["PatrolPumpID"]);
                                                        string PatrolPump = _drAdvances["PatrolPump"].ToString();
                                                        string PatrolDescription = "Refuelling Diesel in " + AssignedVehicle + " " + _drAdvances["PatrolLitres"].ToString() + " Litres @ Rs. " + _drAdvances["PatrolRate"].ToString() + "/Ltr on " + RefuellingDate;
                                                        double PatrolDebit = Convert.ToDouble(_drAdvances["AdvanceAmount"]);
                                                        double PatrolCredit = 0;
                                                        GetSetPatrolPumpAccount(PatrolPump);
                                                        InsertInPatrolPumpAccount(PatrolPump, PatrolPumpID, PatrolDescription, PatrolDebit, PatrolCredit);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    ReceivingNotification("Error", "Error updating broker ledger, due to: " + ex.Message);
                                }

                                //dml.CompleteOrderVehicle(OrderVehicleID);

                                gvContainer.Rows[rowIndex].BackColor = Color.White;
                            }
                        }
                    }

                    txtContainerReceivedBy.Text = string.Empty;
                    txtContainerReceivingDate.Text = string.Empty;
                    txtContainerReceivingTime.Text = string.Empty;

                    GetBiltyContainers(OrderID);

                    divContainerDetails.Visible = true;
                    pnlContainerReceiving.Visible = false;

                    lnkCloseBiltyContainer.Visible = true;
                    lnkCloseContainerReceiving.Visible = false;

                    hfSelectedCotnainerReceiving.Value = string.Empty;
                }
            }
            catch (Exception ex)
            {
                ContainerNotification("Error", "Error saving container receiving, due to: " + ex.Message);
            }
            finally
            {
                modalContainers.Show();
            }
        }

        public void CustomerTransaction(Int64 CustomerCompanyID, double Amount, string DebitCreditType, string EntryDescription) 
        {
            CompanyDML dmlComp = new CompanyDML();
            DataTable dtCompany = dmlComp.GetCompany(CustomerCompanyID);
            if (dtCompany.Rows.Count > 0)
            {
                string CustomerAccName = dtCompany.Rows[0]["CompanyName"].ToString() + "|" + dtCompany.Rows[0]["CompanyCode"].ToString();
                double CustomerAccountBalance = GetCustomerAccountBalance(CustomerAccName);

                double Debit = DebitCreditType == "Debit" ? Amount : 0;
                double Credit = DebitCreditType == "Credit" ? Amount : 0;
                double Balance = CustomerAccountBalance + Debit - Credit;

                AccountsDML dmlAcc = new AccountsDML();
                dmlAcc.InsertInAccount(CustomerAccName, CustomerCompanyID, EntryDescription, Debit, Credit, Balance, LoginID);
            }




            
        }

        public double GetCustomerAccountBalance(string CustomerAccName) 
        {
            try
            {
                AccountsDML dmlAcc = new AccountsDML();
                DataTable dtCustAccountCheck = dmlAcc.GetAccounts(CustomerAccName);
                if (dtCustAccountCheck.Rows.Count <= 0)
                    dmlAcc.CreateAccount(CustomerAccName);

                //0;
                DataTable dtCustAccount = dmlAcc.GetInAccounts(CustomerAccName);
                if (dtCustAccount.Rows.Count > 0)
                {
                    return Convert.ToDouble(dtCustAccount.Rows[dtCustAccount.Rows.Count - 1]["Balance"]);
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                return 0;
            }           
        }

        protected void lnkCloseContainerReceiving_Click(object sender, EventArgs e)
        {
            try
            {
                int rowIndex = Convert.ToInt32(hfSelectedCotnainerReceiving.Value);
                DropDownList ddlReceivingStatus = gvContainer.Rows[rowIndex].FindControl("ddlReceiving") as DropDownList;
                ddlReceivingStatus.ClearSelection();
                ddlReceivingStatus.Items.FindByText("Pending").Selected = true;

                divContainerDetails.Visible = true;
                pnlContainerReceiving.Visible = false;

                lnkCloseBiltyContainer.Visible = true;
                lnkCloseContainerReceiving.Visible = false;

                hfSelectedCotnainerReceiving.Value = string.Empty;
            }
            catch (Exception ex)
            {
                ContainerNotification("Error", "Error closing container receiving, due to: " + ex.Message);
            }
            finally
            {
                modalContainers.Show();
            }
        }

        protected void lnkCloseBills_Click(object sender, EventArgs e)
        {

        }

        protected void lnkCloseInvoicePrints_Click(object sender, EventArgs e)
        {

        }

        protected void lnkCancelSaveAdvances_Click(object sender, EventArgs e)
        {

        }

        protected void lnkSaveAdvances_Click(object sender, EventArgs e)
        {
            try
            {
                OrderDML dml = new OrderDML();
                Int64 OrderID = Convert.ToInt64(hfSelectedOrder.Value);
                dml.DeleteOrderAdvancesByOrder(OrderID);

                if (txtAdvancefrei.Text.Trim() != string.Empty)
                {
                    string AdvanceAgainst = "FreightAdvance";
                    double AdvanceAmount = Convert.ToDouble(txtAdvancefrei.Text);

                    dml.InsertOrderAdvances(OrderID, AdvanceAgainst, AdvanceAmount, LoginID);
                }

                if (txtFactAdvance.Text != string.Empty)
                {
                    string AdvanceAgainst = "FactoryAdvance";
                    double AdvanceAmount = Convert.ToDouble(txtFactAdvance.Text);

                    dml.InsertOrderAdvances(OrderID, AdvanceAgainst, AdvanceAmount, LoginID);
                }

                if (txtDiesAdvance.Text != string.Empty)
                {
                    string AdvanceAgainst = "DieselAdvance";
                    double AdvanceAmount = Convert.ToDouble(txtDiesAdvance.Text);

                    dml.InsertOrderAdvances(OrderID, AdvanceAgainst, AdvanceAmount, LoginID);
                }

                if (txtVehicAdvance.Text != string.Empty)
                {
                    string AdvanceAgainst = "VehicleAdvance";
                    double AdvanceAmount = Convert.ToDouble(txtVehicAdvance.Text);

                    dml.InsertOrderAdvances(OrderID, AdvanceAgainst, AdvanceAmount, LoginID);
                }

                modalAdvances.Hide();
                hfSelectedOrder.Value = string.Empty;
                txtAdvancefrei.Text = string.Empty;
                txtDiesAdvance.Text = string.Empty;
                txtFactAdvance.Text = string.Empty;
                txtVehicAdvance.Text = string.Empty;

                GetBilties();

            }
            catch (Exception ex)
            {
                AdvancesNotification("Error", "Error saving advances, due to: " + ex.Message);
                modalAdvances.Show();
            }
        }

        protected void lnkCloseAdvancess_Click(object sender, EventArgs e)
        {
            try
            {
                modalAdvances.Hide();
            }
            catch (Exception ex)
            {
                AdvancesNotification("Error", "Error closing advance notification, due to: " + ex.Message);
                modalAdvances.Show();
            }
        }

        protected void lnkCloseAdvances2_Click(object sender, EventArgs e)
        {
            try
            {
                modalAdvances2.Hide();
                pnlAdvanceInput.Visible = false;
                ClearAdvancesFields();
                hfSelectedOrder.Value = string.Empty;

                modalAdvances2.Hide();
            }
            catch (Exception ex)
            {
                Advances2Notification("Error", "Error closing Advances panel, due to: " + ex.Message);
                modalAdvances2.Show();
            }
        }

        protected void lnkAddAdvance2_Click(object sender, EventArgs e)
        {
            try
            {
                pnlAdvanceInput.Visible = true;
            }
            catch (Exception ex)
            {
                Advances2Notification("Error", "Error enabling add new advances input, due to: " + ex.Message);
            }
            finally
            {
                modalAdvances2.Show();
            }
        }

        protected void lnkCloseSTInvoice_Click(object sender, EventArgs e)
        {

        }

        protected void lnkCloseSalesTaxConfirms_Click(object sender, EventArgs e)
        {

        }

        protected void lnkCancelSaveAdvances2_Click(object sender, EventArgs e)
        {
            try
            {
                ClearAdvancesFields();
                pnlAdvanceInput.Visible = false;
            }
            catch (Exception ex)
            {
                Advances2Notification("Error", "Error cancelling Advance input panel, due to: " + ex.Message);
            }
            finally
            {
                modalAdvances2.Show();
            }
        }
        
        protected void lnkCloseSalesTaxConfirms_Click1(object sender, EventArgs e)
        {

        }

        protected void lnkSaveSalesTax_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlSuppliers.SelectedIndex == 0)
                {
                    modalSalesTaxConfirm.Show();
                    STInvoiceConfirmNotification("Error", "Please select Supplier");
                    ddlSuppliers.Focus();
                }
                else if (ddlBuyer.SelectedIndex == 0)
                {
                    modalSalesTaxConfirm.Show();
                    STInvoiceConfirmNotification("Error", "Please select Buyer");
                    ddlBuyer.Focus();
                }
                else if (txtSTInvoiceDate.Text == string.Empty)
                {
                    modalSalesTaxConfirm.Show();
                    STInvoiceConfirmNotification("Error", "Please select Invoice date");
                    txtSTInvoiceDate.Focus();
                }
                else
                {
                    SalesTaxInvoiceDML dml = new SalesTaxInvoiceDML();
                    Int64 OrderID = Convert.ToInt64(hfSelectedOrder.Value);
                    Int64 SupplierID = Convert.ToInt64(ddlSuppliers.SelectedItem.Value);
                    Int64 BuyerID = Convert.ToInt64(ddlBuyer.SelectedItem.Value);
                    double ActualAmount = Convert.ToDouble(txtActualAmount.Text);
                    double STPerc = Convert.ToDouble(txtSalesTaxPercentage.Text);
                    double STAmount = Convert.ToDouble(txtSalesTaxAmount.Text);
                    double Total = Convert.ToDouble(txtValueInclusiveTax.Text);
                    string InvoiceDate = txtSTInvoiceDate.Text;
                    Int64 STInvoiceID = dml.InsertSalesTaxInvoice(OrderID, SupplierID, BuyerID, ActualAmount, STPerc, STAmount, Total, InvoiceDate.ToString(), LoginID);
                    if (STInvoiceID > 0)
                    {
                        GetSetDisplaySTInvoice(STInvoiceID);
                    }
                }
            }
            catch (Exception ex)
            {
                STInvoiceConfirmNotification("Error", "Error saving Sales Tax invoice, due to: " + ex.Message);
                modalSalesTaxConfirm.Show();
            }
        }

        protected void lnkSaveAdvances2_Click(object sender, EventArgs e)
        {
            try
            {
                OrderDML dml = new OrderDML();
                Int64 OrderID = Convert.ToInt64(hfSelectedOrder.Value);
                double Amount = txtAdvanceAmount.Text == string.Empty ? 0 : Convert.ToDouble(txtAdvanceAmount.Text);
                string AdvanceDate = txtAdvanceDate.Text;
                Int64 PatrolPumpID = 0;
                string AdvancePlace = txtAdvancePlaces.Text.Trim();
                if (rbAdvanceTypes.SelectedIndex == -1)
                {
                    Advances2Notification("Error", "Please select Advance against item");
                    rbAdvanceTypes.Focus();
                }
                else if (Amount <= 0)
                {
                    Advances2Notification("Error", "Please enter valid advance amount");
                    txtAdvanceAmount.Focus();
                }
                else if (rbAdvanceTypes.SelectedItem.Text == "Diesel Advance" && (ddlPatrolPumps.SelectedIndex == 0))
                {
                    Advances2Notification("Error", "Please select Patrol Pump");
                    ddlPatrolPumps.Focus();
                }
                else
                {
                    string AdvanceAgainst = rbAdvanceTypes.SelectedItem.Text;
                    PatrolPumpID = ddlPatrolPumps.SelectedIndex == 0 ? 0 : Convert.ToInt64(ddlPatrolPumps.SelectedItem.Value);
                    double PatrolRate = ddlPatrolPumps.SelectedIndex == 0 ? 0 : (txtPatrolRate.Text == string.Empty ? 0 : Convert.ToDouble(txtPatrolRate.Text));
                    double PatrolLitre = ddlPatrolPumps.SelectedIndex == 0 ? 0 : (txtPatrolLitre.Text == string.Empty ? 0 : Convert.ToDouble(txtPatrolLitre.Text));
                    Int64 OrderAdvanceID = dml.InsertOrderAdvances(OrderID, AdvancePlace, AdvanceAgainst, PatrolPumpID, PatrolRate, PatrolLitre, Amount, AdvanceDate, LoginID);
                    if (OrderAdvanceID > 0)
                    {
                        ClearAdvancesFields();
                        GetOrderAdvances(OrderID);
                        GetBilties();

                        pnlAdvanceInput.Visible = false;
                    }
                    else
                    {
                        Advances2Notification("Error", "Advance not saved... Try Again !!!");
                    }
                }
            }
            catch (Exception ex)
            {
                Advances2Notification("Error", "Error saving advances, due to: " + ex.Message);
            }
            finally
            {
                modalAdvances2.Show();
            }
        }



        protected void lnkSearch_Click(object sender, EventArgs e)
        {
            try
            {
                GetBilties();
            }
            catch (Exception ex)
            {
                notification("Error", "Error esarching, due to: " + ex.Message);
            }
        }

        protected void lnkclear_Click(object sender, EventArgs e)
        {
            try
            {
                OrderDML dml = new OrderDML();

                DataTable dtBilties = new DataTable();
                dtBilties = dml.GetBilties();
                if (dtBilties.Rows.Count > 0)
                {
                    gvBilty.DataSource = dtBilties;
                }
                else
                {
                    gvBilty.DataSource = dtBilties;
                }
                gvBilty.DataBind();

                txtSearchDateFrom.Text = string.Empty;
                txtSearchDateTo.Text = string.Empty;
                txtKeyword.Text = string.Empty;

            }
            catch (Exception ex)
            {
                notification("Error", "Error getting/binding bilties, due to: " + ex.Message);
            }
        }

        protected void lnkClearFrom_Click(object sender, EventArgs e)
        {
            try
            {
                txtSearchDateFrom.Text = string.Empty;
            }
            catch (Exception ex)
            {
                notification("Error", "Error clearing from date, due to: " + ex.Message);
            }
        }

        protected void lnkAmountCalculation_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtAdvanceAmount.Text == string.Empty)
                {
                    txtAdvanceAmount.Focus();
                }
                else if (ddlPatrolPumps.SelectedIndex == 0)
                {
                    ddlPatrolPumps.Focus();
                }
                else
                {
                    AmountCalculation();
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error calculating, due to: " + ex.Message);
            }
            finally 
            {
                modalAdvances2.Show();
            }
        }

        protected void lnkClearTo_Click(object sender, EventArgs e)
        {
            try
            {
                txtSearchDateTo.Text = string.Empty;
            }
            catch (Exception ex)
            {
                notification("Error", "Error clearing to date, due to: " + ex.Message);
            }
        }

        #endregion

        #region Dropdown's Events

        protected void ddlReceiving_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = ((GridViewRow)((DropDownList)sender).NamingContainer);
                DropDownList ddlReceivingStatus = (DropDownList)row.FindControl("ddlReceiving");
                if (ddlReceivingStatus.SelectedItem.Text == "Received")
                {
                    Int64 OrderContainerID = Convert.ToInt64(gvContainer.DataKeys[row.RowIndex].Values["OrderConsignmentID"]);
                    divContainerDetails.Visible = false;
                    pnlContainerReceiving.Visible = true;

                    lnkCloseBiltyContainer.Visible = false;
                    lnkCloseContainerReceiving.Visible = true;
                    hfSelectedCotnainerReceiving.Value = row.RowIndex.ToString();


                    //OrderDML dml = new OrderDML();
                    //DataTable dtContainer = dml.GetExpenses(OrderContainerID);
                    //if (dtContainer.Rows.Count > 0)
                    //{
                    //    bool IsWeighmentAdded = false;

                    //    DataRow[] _drWeighment = dtContainer.Select("ExpensesTypeName = 'Weighment Charges'");
                    //    DataRow[] _drLolo = dtContainer.Select("ExpensesTypeName = 'Lift Off Lift On'");

                    //    if (_drWeighment.Length > 0)
                    //    {
                    //        if (_drLolo.Length > 0)
                    //        {
                    //            IsWeighmentAdded = true;
                    //        }
                    //    }
                    //    else if (_drLolo.Length > 0)
                    //    {
                    //        if (_drWeighment.Length > 0)
                    //        {
                    //            IsWeighmentAdded = true;
                    //        }
                    //    }


                    //    if (IsWeighmentAdded == true)
                    //    {
                    //        divContainerDetails.Visible = false;
                    //        pnlContainerReceiving.Visible = true;

                    //        lnkCloseBiltyContainer.Visible = false;
                    //        lnkCloseContainerReceiving.Visible = true;
                    //        hfSelectedCotnainerReceiving.Value = row.RowIndex.ToString();
                    //    }
                    //    else
                    //    {
                    //        ContainerNotification("Error", "Container cannot be received without Weighment & LOLO Charges");
                    //        DropDownList ddlContainerReceiving = gvContainer.Rows[row.RowIndex].FindControl("ddlReceiving") as DropDownList;
                    //        ddlContainerReceiving.ClearSelection();
                    //        ddlContainerReceiving.Items.FindByText("Pending").Selected = true;
                    //    }
                    //}
                    //else
                    //{
                    //    ContainerNotification("Error", "Container cannot be received without Weighment & LOLO Charges");
                    //    DropDownList ddlContainerReceiving = gvContainer.Rows[row.RowIndex].FindControl("ddlReceiving") as DropDownList;
                    //    ddlContainerReceiving.ClearSelection();
                    //    ddlContainerReceiving.Items.FindByText("Pending").Selected = true;
                    //}


                }
            }
            catch (Exception ex)
            {
                ContainerNotification("Error", "Error changing status, due to: " + ex.Message);
            }
            finally
            {
                modalContainers.Show();
            }
        }

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

        #region Misc

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

        protected void rbAdvanceTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbAdvanceTypes.SelectedItem.Text == "Diesel Advance")
                {
                    PatrolPumpAdvancePlaceholder.Visible = true;
                    PatrolLitrePlaceholder.Visible = true;
                    PatrolRatePlaceholder.Visible = true;
                    VehicleAdvancePlaceholder.Visible = false;
                    divAdvancePlaces.Visible = false;
                    AdvanceAmountPlaceholder.Visible = true;
                    divAdvanceDate.Visible = true;
                }
                if (rbAdvanceTypes.SelectedItem.Text == "Factory Advance")
                {
                    PatrolPumpAdvancePlaceholder.Visible = false;
                    PatrolLitrePlaceholder.Visible = false;
                    PatrolRatePlaceholder.Visible = false;
                    VehicleAdvancePlaceholder.Visible = false;

                    divAdvancePlaces.Visible = false;
                    AdvanceAmountPlaceholder.Visible = true;
                    divAdvanceDate.Visible = true;
                }
                if (rbAdvanceTypes.SelectedItem.Text == "Vehicle Advance")
                {
                    PatrolPumpAdvancePlaceholder.Visible = false;
                    PatrolLitrePlaceholder.Visible = false;
                    PatrolRatePlaceholder.Visible = false;
                    VehicleAdvancePlaceholder.Visible = false;

                    divAdvancePlaces.Visible = false;
                    AdvanceAmountPlaceholder.Visible = false;
                    divAdvanceDate.Visible = false;
                }
                else if (rbAdvanceTypes.SelectedItem.Text == "Advance Freight")
                {
                    PatrolPumpAdvancePlaceholder.Visible = false;
                    PatrolLitrePlaceholder.Visible = false;
                    PatrolRatePlaceholder.Visible = false;
                    VehicleAdvancePlaceholder.Visible = false;

                    divAdvancePlaces.Visible = true;
                    AdvanceAmountPlaceholder.Visible = true;
                    divAdvanceDate.Visible = true;
                }
                ddlPatrolPumps.ClearSelection();
            }
            catch (Exception ex)
            {
                Advances2Notification("Error", "Erorr selecting radio button, due to: " + ex.Message);
                rbAdvanceTypes.ClearSelection();

                PatrolPumpAdvancePlaceholder.Visible = false;
                VehicleAdvancePlaceholder.Visible = false;
            }
            finally
            {
                modalAdvances2.Show();
            }
        }

        #endregion

        #endregion

        #region Working Area

        #endregion

        protected void ddlPatrolPumps_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlPatrolPumps.SelectedIndex > 0)
                {
                    Int64 ID = Convert.ToInt64(ddlPatrolPumps.SelectedValue);
                    OrderDML dml = new OrderDML();
                    DataTable dt = dml.GetPatrolPump(ID);
                    txtPatrolRate.Text = dt.Rows[0]["PerLiterRate"].ToString();


                    AmountCalculation();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                modalAdvances2.Show();
            }

        }

        protected void txtPatrolRate_TextChanged(object sender, EventArgs e)
        {
            AmountCalculation();
        }

        protected void txtAdvanceAmount_TextChanged(object sender, EventArgs e)
        {
            AmountCalculation();
        }

      
        protected void lnkRefreshPatrolPumpDDL_Click(object sender, EventArgs e)
        {
            try
            {
                GetAndBindPatrolPumps("DESC");

            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                modalAdvances2.Show();
            }
        }

        protected void ddlShippingLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                modalBilty.Show();
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                modalBilty.Show();
            }
        }

        protected void ddlSearchSender_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (ddlSearchSender.SelectedIndex == 0 )
                {
                    notification("Error", "Please enter Company code, name or Group to search Company");
                }
                else
                {
                    string[] Company = ddlSearchSender.SelectedItem.Text.Split('|');
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

                
            }
            finally
            {
                modalBilty.Show();
            }
        }

        protected void ddlSearchReceiver_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlSearchReceiver.SelectedIndex == 0)
                {
                    notification("Error", "Please enter Company code, name or Group to search Company");
                }
                else
                {
                    string[] Company = ddlSearchReceiver.SelectedItem.Text.Split('|');
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
            catch (Exception ex)
            {

                
            }
            finally
            {
                modalBilty.Show();
            }
        }

        protected void ddlSearchCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlSearchCustomer.SelectedIndex == 0)
                {
                    notification("Error", "Please enter Company code, name or Group to search Company");
                }
                else
                {
                    string[] Company = ddlSearchCustomer.SelectedItem.Text.Split('|');
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
            finally
            {
                modalBilty.Show();
            }
        }

        protected void ddlContainerDropoff_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetContainerRateFromProfile();
        }

        protected void ddlContainerPickup_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetContainerRateFromProfile();
        }

        protected void ddlContainerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetContainerRateFromProfile();
        }

        //protected void ddlVehicleNo_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        ddlBroker.Enabled = false;
        //        if (ddlVehicleNo.SelectedIndex != 0)
        //        {
        //            ddlBroker.Enabled = true;
        //            Int64 VehicleID = Convert.ToInt64(ddlVehicleNo.SelectedItem.Value);
        //            BrokersDML dmlBrok = new BrokersDML();
        //            DataTable dtBrokersByVehicle = dmlBrok.GetActiveBrokerByVehicle(VehicleID);
        //            if (dtBrokersByVehicle.Rows.Count > 0)
        //            {
        //                FillDropDown(dtBrokersByVehicle, ddlBroker, "ID", "Name", "- Select-");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        modalBiltyVehicles.Show();
        //    }
        //}
        public void GetContainer(object sender, EventArgs e)
        {

            DropDownList ddlContainerDropoff = sender as DropDownList;
            GridViewRow gvr = (GridViewRow)ddlContainerDropoff.Parent.Parent;
            DropDownList ddlContainerPickup = gvr.FindControl("ddlContainerPickup") as DropDownList;
            TextBox txtContainerRate = gvr.FindControl("txtContainerRate") as TextBox;

            if (ddlContainerDropoff.SelectedIndex != 0)
            {
                if (ddlContainerPickup.SelectedIndex != 0)
                {
                    string CustomerName = hfSelectedBillToCustomer.Value;
                    string ContainerTipe = ddlContainerType.SelectedItem.Value;
                    Int64 PickupLocationID = Convert.ToInt64(ddlContainerPickup.SelectedItem.Value);
                    Int64 DropoffID = Convert.ToInt64(ddlContainerDropoff.SelectedItem.Value);
                    ManualBiltyDML dml = new ManualBiltyDML();
                    DataTable _dt = dml.GetCompanyProfile(CustomerName, PickupLocationID, DropoffID, ContainerTipe);
                    if (_dt.Rows.Count > 0)
                    {
                        txtContainerRate.Text = _dt.Rows[0]["ContainerRate"].ToString();
                    }
                    else
                    {
                        txtContainerRate.Text = string.Empty;
                    }
                }
            }
        }


        public void GetContainerRateFromProfile()
        {
            try
            {
                if (hfSelectedBillToCustomer.Value.Trim() == string.Empty)
                {
                    ContainerNotification("Error", "Bill to customer not found, please try again!!!");
                }
                else if (ddlContainerType.SelectedIndex == 0)
                {
                    ContainerNotification("Error", "Please select Container Type");
                    ddlContainerType.Focus();
                }
                else if (ddlContainerPickup.SelectedIndex == 0)
                {
                    ContainerNotification("Error", "Please select Container Pickup");
                    ddlContainerPickup.Focus();
                }
                else if (ddlContainerDropoff.SelectedIndex == 0)
                {
                    ContainerNotification("Error", "Please select Container Dropoff");
                    ddlContainerDropoff.Focus();
                }
                else
                {
                    string BillToCustomer = hfSelectedBillToCustomer.Value;
                    string ContainerType = ddlContainerType.SelectedItem.Text;
                    Int64 PickupLocationID = Convert.ToInt64(ddlContainerPickup.SelectedItem.Value);
                    Int64 DropoffLocationID = Convert.ToInt64(ddlContainerDropoff.SelectedItem.Value);
                    ManualBiltyDML dml = new ManualBiltyDML();
                    DataTable _dt = dml.GetCompanyProfile(BillToCustomer, PickupLocationID, DropoffLocationID, ContainerType);
                    if (_dt.Rows.Count > 0)
                    {
                        txtContainerRate.Text = _dt.Rows[0]["ContainerRate"].ToString();
                    }
                    else
                    {
                        txtContainerRate.Text = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                ContainerNotification("Error", "Error getting container rate from profile, due to: " + ex.Message);
            }
            finally
            {
                modalContainers.Show();
            }

            
        }
    }
} 