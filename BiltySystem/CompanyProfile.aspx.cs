using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.Data;

namespace BiltySystem
{
    public partial class CompanyProfile : System.Web.UI.Page
    {
        SqlConnection Xcon = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ConnectionString);

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
            modalNotification("", "");
            if (!IsPostBack)
            {
                if (LoginID > 0)
                {
                    this.Title = "Company Profile";
                    GetCompany();
                    GetCompanyProfile();
                    //GetProductWithCode();
                    GetLocation();
                    GetBroker();

                    try
                    {
                        ContainerTypeDML dml = new ContainerTypeDML();
                        DataTable dtContainerType = dml.GetContainerType();
                        if (dtContainerType.Rows.Count > 0)
                        {
                            FillDropDown(dtContainerType, ddlContainerType, "ContainerTypeID", "ContainerType", "- Select -");
                        }
                    }
                    catch (Exception ex)
                    {
                        notification("Error", "Error getting/populating container types, due to: " + ex.Message);
                    }
                    try
                    {
                        ContainerTypeDML dml = new ContainerTypeDML();
                        DataTable dtVehicleType = dml.GetVehicleType();
                        if (dtVehicleType.Rows.Count > 0)
                        {
                            FillDropDown(dtVehicleType, ddlVehicleType, "VehicleTypeID", "VehicleTypeName", "- Select -");
                        }
                    }
                    catch (Exception ex)
                    {
                        notification("Error", "Error getting/populating container types, due to: " + ex.Message);
                    }
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

        public void modalNotification()
        {
            try
            {
                divModalNotification.InnerHtml = string.Empty;
            }
            catch (Exception ex)
            {
                divModalNotification.InnerHtml = "<div class=\"alert alert-danger\">" + ex.Message + "</div>";
            }
        }

        public void modalNotification(string type, string msg)
        {
            try
            {
                if (type == "Error")
                {
                    divModalNotification.InnerHtml = "<div class=\"alert alert-danger\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                }
                else if (type == "Success")
                {
                    divModalNotification.InnerHtml = "<div class=\"alert alert-success\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                }
                else if (type == string.Empty)
                {
                    divModalNotification.InnerHtml = string.Empty;
                }
            }
            catch (Exception ex)
            {
                divModalNotification.InnerHtml = "<div class=\"alert alert-danger\">" + ex.Message + "</div>";
            }
        }

        #endregion

        public void GetLocation()
        {

            try
            {
                LocationDML dml = new LocationDML();
                ManualBiltyDML dmlBilty = new ManualBiltyDML();

                //DataTable dtLocation = dml.GetAllLocationsForBilty();
                DataTable dtLocation = dmlBilty.GetPickDropLocation();
                if (dtLocation.Rows.Count > 0)
                {
                    FillDropDown(dtLocation, ddlLocationFrom, "CityID", "Location", "-Select-");
                    FillDropDown(dtLocation, ddlLocationTo, "CityID", "Location", "-Select-");
                }


            }
            catch (Exception ex)
            {
                notification("Error", "Error getting/populating locations, due to: " + ex.Message); 
            }
            //string com = "SELECT * FROM PickDropLocation";
            //SqlDataAdapter adpt = new SqlDataAdapter(com, Xcon);
            //DataTable dt = new DataTable();
            //adpt.Fill(dt);
            //ddlLocationFrom.DataSource = dt;
            //ddlLocationFrom.DataBind();
            //ddlLocationFrom.DataTextField = "Address";
            //ddlLocationFrom.DataValueField = "PickDropID";
            //ddlLocationFrom.DataBind();

            //ddlLocationTo.DataSource = dt;
            //ddlLocationTo.DataBind();
            //ddlLocationTo.DataTextField = "Address";
            //ddlLocationTo.DataValueField = "PickDropID";
            //ddlLocationTo.DataBind();
        }

        //public void GetGroupsWithCode()
        //{

        //    string com = "SELECT *, CONCAT(CONCAT(GroupCode, ' - '), GroupName) AS GroupAndCode FROM Groups";
        //    SqlDataAdapter adpt = new SqlDataAdapter(com, Xcon);
        //    DataTable dt = new DataTable();
        //    adpt.Fill(dt);
        //    ddlGroup.DataSource = dt;
        //    ddlGroup.DataBind();
        //    ddlGroup.DataTextField = "GroupAndCode";         
        //    ddlGroup.DataValueField = "GroupID";
        //    ddlGroup.DataBind();
        //}

        //public void GetProductWithCode()
        //{
        //    try
        //    {
        //        ProductDML dml = new ProductDML();
        //        DataTable dtProducts = dml.GetProductDDL();
        //        FillDropDown(dtProducts, ddlProduct, "ID", "Product", "-Select-");
        //    }
        //    catch (Exception ex)
        //    {
        //        notification("Error", "Error getting/populating product due to: " + ex.Message);
        //    }
        //    //string com = "SELECT *, CONCAT(CONCAT(Code, ' - '), Name) AS ProductAndCode FROM Product";
        //    //SqlDataAdapter adpt = new SqlDataAdapter(com, Xcon);
        //    //DataTable dt = new DataTable();
        //    //adpt.Fill(dt);
        //    //ddlProduct.DataSource = dt;
        //    //ddlProduct.DataBind();
        //    //ddlProduct.DataTextField = "ProductAndCode";
        //    //ddlProduct.DataValueField = "ID";
        //    //ddlProduct.DataBind();
        //}

        public void GetCompany()
        {
            try
            {
                CompanyDML profile = new CompanyDML();
                DataTable dtprofile = profile.GetCompany();
                if (dtprofile.Rows.Count > 0)
                {
                    FillDropDown(dtprofile, ddlCompany, "CompanyID", "CompanyName", "-Select-");
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting/binding Companies, due to: " + ex.Message);
            }
        }

        public void GetBroker()
        {
            try
            {
                BrokersDML Broker = new BrokersDML();
                DataTable dtbrokers = Broker.GetAllBrokers();
                if (dtbrokers.Rows.Count > 0)
                {
                    FillDropDown(dtbrokers, ddlBroker, "ID", "Name", "-Select-");
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting/binding Brokers, due to: " + ex.Message);
            }
        }

        public void GetDetail()
        {
            try
            {
                CompanyProfileDML profile = new CompanyProfileDML();
                DataTable dt = profile.GetCompanyProfileDetails();
                if (dt.Rows.Count > 0)
                {
                    gvCustomerDetail.DataSource = dt;
                }
                else
                {
                    gvCustomerDetail.DataSource = null;
                }
                gvResult.DataBind();
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting Details, due to: " + ex.Message);
            }
        }

        public void GetCompanyProfile()
        {
            try
            {
                CompanyProfileDML profile = new CompanyProfileDML();
                DataTable dt = profile.GetCompanyProfile();
                if (dt.Rows.Count > 0)
                {
                    gvResult.DataSource = dt;
                }
                else
                {
                    gvResult.DataSource = null;
                }
                gvResult.DataBind();
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting Customer, due to: " + ex.Message);
            }
        }

        public void ClearItem()
        {
            ddlCompany.ClearSelection();
            ddlCredit.ClearSelection();
            //ddlInvoice.ClearSelection();
            ddlpayment.ClearSelection();
            ddlBroker.ClearSelection();
            //hfEditID.Value = string.Empty;
            lnkDelete.Visible = false;
            hfEditDetail.Value = string.Empty;
            ddlContainerType.ClearSelection();
            ddlLocationFrom.ClearSelection();
            ddlLocationTo.ClearSelection();
            ddlBroker.ClearSelection();
            txtContainerRate.Text = string.Empty;

        }

        public void GetDataSpecificID(Int64 ProfileId)
        {
            try
            {
                CompanyProfileDML profile = new CompanyProfileDML();
                DataTable dtProfile = profile.GetCompanyProfileDetails(ProfileId);
                if (dtProfile.Rows.Count > 0)
                {
                    Int64 CustomerID = Convert.ToInt64(dtProfile.Rows[0]["CustomerID"]);
                    Int64 BrokerID = Convert.ToInt64(dtProfile.Rows[0]["BrokerID"]);

                    if (CustomerID > BrokerID)
                    {
                        gvCustomerDetail.DataSource = dtProfile;
                        gvCustomerDetail.DataBind();
                    }
                    else if (BrokerID > CustomerID)
                    {
                        gvBrokerDetail.DataSource = dtProfile;
                        gvBrokerDetail.DataBind();
                    }
                }
                else
                {
                    gvCustomerDetail.DataSource = null;
                }

            }
            catch (Exception ex)
            {
                notification("Error", "Error with Binding Data due to: " + ex.Message);
            }
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

        #endregion

        #region Events

        #region Linkbutton/Button Events

        protected void lnkSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                //if (ddlCompany.SelectedIndex == 0)
                //{
                //    notification("Error", "Please Select Company");
                //}
                //else if (ddlCredit.SelectedIndex == 0)
                //{
                //    notification("Error", "Please Select Credit");
                //}
                //else if (ddlInvoice.SelectedIndex == 0)
                //{
                //    notification("Error", "Please Select Invoice Fromat");
                //}
                //else 
                if (ddlpayment.SelectedIndex == 0)
                {
                    notification("Error", "Please Select Payment term");
                }
                else
                {
                    if (CustomerPlaceholder.Visible == true && ddlCompany.SelectedIndex == 0)
                    {
                        notification("Error", "Please select Customer");
                        ddlCompany.Focus();
                    }
                    else if (BrokerPlaceholder.Visible == true && ddlBroker.SelectedIndex == 0)
                    {
                        notification("Error", "Please select Broker");
                        ddlBroker.Focus();
                    }
                    else
                    {
                        Int64 Customer = ddlCompany.SelectedIndex == 0 ? 0 : Convert.ToInt64(ddlCompany.SelectedItem.Value);
                        string payment = ddlpayment.SelectedValue;
                        string Credit = ddlCredit.SelectedValue;
                        //string Invoice = ddlInvoice.SelectedValue;
                        Int64 BrokerID = ddlBroker.SelectedIndex == 0 ? 0 : Convert.ToInt64(ddlBroker.SelectedValue);

                        CompanyProfileDML profile = new CompanyProfileDML();
                        Int64 VerifyingId = Customer > 0 ? Customer : BrokerID;
                        DataTable _dt = profile.GetCompanyProfile(VerifyingId,payment);
                        if (_dt.Rows.Count > 0)
                        {
                            notification("Error", "this record already exist");
                        }
                        else
                        {
                            if (hfEditID.Value == string.Empty)
                            {

                                //profile.InsertCompanyProfile(Customer, BrokerID, payment, Credit, Invoice, LoginID);
                                profile.InsertCompanyProfile(Customer, BrokerID, payment, Credit, LoginID);
                                notification("Success", "Profile Added successfully");

                            }
                            else
                            {
                                Int64 ProfileID = Convert.ToInt64(hfEditID.Value);
                                //profile.UpdateCompanyProfile(ProfileID, BrokerID, Customer, payment, Credit, Invoice, LoginID);
                                profile.UpdateCompanyProfile(ProfileID, BrokerID, Customer, payment, Credit, LoginID);
                                notification("Success", " Profile Updated successfully");
                            }


                            GetCompanyProfile();
                            ClearItem();
                            pnlInput.Visible = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Data not submitted due to: " + ex.Message);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                hfEditID.Value = string.Empty;
                ClearItem();

                modalProfileDetail.Hide();
            }
            catch (Exception ex)
            {
                modalNotification();
                modalProfileDetail.Show();
            }
        }

        protected void btnAddDetail_Click(object sender, EventArgs e)
        {
            try
            {
                Int64 CustomerID = ddlCompany.SelectedIndex == 0 ? 0 : Convert.ToInt64(ddlCompany.SelectedItem.Value);
                Int64 BrokerID = ddlBroker.SelectedIndex == 0 ? 0 : Convert.ToInt64(ddlBroker.SelectedItem.Value);
                if (ddlLocationFrom.SelectedIndex == 0 && rbCompanyType.SelectedItem.Text == "Customer")
                {
                    modalNotification("Error", "Please select Start Location");
                    ddlLocationFrom.Focus();
                }
                else if (ddlLocationTo.SelectedIndex == 0 && rbCompanyType.SelectedItem.Text == "Customer")
                {
                    modalNotification("Error", "Please select End Location");
                    ddlLocationFrom.Focus();
                }
                else if (ddlContainerType.SelectedIndex == 0 && rbCompanyType.SelectedItem.Text == "Customer")
                {
                    modalNotification("Error", "Please select Container Type");
                    ddlContainerType.Focus();
                }
                else if (txtContainerRate.Text == string.Empty && rbCompanyType.SelectedItem.Text == "Customer")
                {
                    modalProfileDetail.Show();
                    modalNotification("Error", "Please Enter container Rate");
                }
                else if (ddlVehicleType.SelectedIndex == 0 && rbCompanyType.SelectedItem.Text == "Broker")
                {
                    modalProfileDetail.Show();
                    modalNotification("Error", "Please Enter container Rate");
                }
                else if (txtVehicleRate.Text == string.Empty && rbCompanyType.SelectedItem.Text == "Broker")
                {
                    modalProfileDetail.Show();
                    modalNotification("Error", "Please Enter container Rate");
                }
                else
                {
                    Int64 ProfileID = Convert.ToInt64(hfEditID.Value);
                    Int64 LocationFrom = ddlLocationFrom.SelectedIndex == 0 ? 0 : Convert.ToInt64(ddlLocationFrom.SelectedItem.Value);
                    Int64 LocationTo = ddlLocationTo.SelectedIndex == 0 ? 0 : Convert.ToInt64(ddlLocationTo.SelectedItem.Value);
                    Int64 ContainerTypeID = ddlContainerType.SelectedIndex == 0 ? 0 : Convert.ToInt64(ddlContainerType.SelectedItem.Value);
                    Int64 VehicleTypeID = ddlVehicleType.SelectedIndex == 0 ? 0 : Convert.ToInt64(ddlVehicleType.SelectedItem.Value);
                    double ContainerRate = txtContainerRate.Text == string.Empty ? 0 : Convert.ToDouble(txtContainerRate.Text);
                    double VehicleRate = txtVehicleRate.Text == string.Empty ? 0 : Convert.ToDouble(txtVehicleRate.Text);

                    CompanyProfileDML company = new CompanyProfileDML();
                    if (hfEditDetail.Value.ToString() == string.Empty)
                    {

                        if (CustomerID > BrokerID)
                        {
                            company.InsertProfileDetails(LocationFrom, LocationTo, ContainerRate, ContainerTypeID, ProfileID, LoginID);
                        }
                        else if (BrokerID > CustomerID)
                        {
                            company.InsertProfileDetails(VehicleRate, VehicleTypeID, ProfileID, LoginID);
                        }
                        modalNotification("Success", "Details Added successfully");
                    }
                    else
                    {
                        Int64 ProfileDetailId = Convert.ToInt64(hfEditDetail.Value);
                        // company.UpdateCompanyProfileDetails(ProfileDetailId, LocationFrom, LocationTo, ContainerRate, ContainerTypeID, LoginID);

                        if (CustomerID > BrokerID)
                        {
                            company.UpdateCompanyProfileDetails(ProfileDetailId, LocationFrom, LocationTo, ContainerRate, ContainerTypeID, LoginID);
                        }
                        else if (BrokerID > CustomerID)
                        {
                            company.UpdateCompanyProfileDetails(ProfileDetailId, VehicleRate, VehicleTypeID, ProfileID, LoginID);
                        }
                        modalNotification("Success", "Details Updated successfully");



                    }
                    ddlLocationFrom.ClearSelection();
                    ddlLocationTo.ClearSelection();
                    ddlContainerType.ClearSelection();
                    txtContainerRate.Text = string.Empty;

                    ddlVehicleType.ClearSelection();
                    txtVehicleRate.Text = string.Empty;

                    hfEditDetail.Value = string.Empty;
                    //ClearItem();
                    GetDataSpecificID(ProfileID);
                }
            }
            catch (Exception ex)
            {
                modalNotification("Error", "An Error occured due to: " + ex.Message);
            }
            finally
            {
                modalProfileDetail.Show();
            }
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            ConfirmModal("Are you sure want to delete?", "Delete");
        }

        protected void lnkConfirm_Click(object sender, EventArgs e)
        {
            string Action = hfConfirmAction.Value;
            if (Action == "Delete")
            {
                try
                {
                    Int64 dpid = Convert.ToInt64(hfEditID.Value);
                    if (dpid > 0)
                    {
                        CompanyProfileDML dml = new CompanyProfileDML();
                        dml.DeleteCompanyProfile(dpid);
                        notification("Success", "Profile deleted successfully");

                        ClearItem();
                        GetCompanyProfile();


                    }
                    else
                    {
                        notification("Error", "No Profile selected to delete");
                    }
                }
                catch (Exception ex)
                {

                    notification("Error", ex.Message);
                }
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            try
            {
                hfEditID.Value = string.Empty;
                ClearItem();

                modalProfileDetail.Hide();
            }
            catch (Exception ex)
            {
                modalNotification();
                modalProfileDetail.Show();
            }
        }

        protected void AddNew_Click(object sender, EventArgs e)
        {
            pnlInput.Visible = true;
        }

        protected void lnkCloseInput_Click(object sender, EventArgs e)
        {
            pnlInput.Visible = false;
        }

        #endregion

        #region Gridview Events

        protected void gvResult_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Change")
            {
                pnlInput.Visible = true;
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow gvr = gvResult.Rows[index];
                Int64 ID = Convert.ToInt64(gvResult.DataKeys[index]["ProfileID"]);
                hfEditID.Value = ID.ToString();

                CompanyProfileDML dml = new CompanyProfileDML();
                DataTable dt = dml.GetCompanyProfile(ID);
                if (dt.Rows.Count > 0)
                {



                    if (Convert.ToInt64(dt.Rows[0]["BrokerID"]) > 0)
                    {
                        ddlBroker.ClearSelection();
                        ddlBroker.Items.FindByValue(dt.Rows[0]["BrokerID"].ToString().Replace(" ", string.Empty)).Selected = true;
                        BrokerPlaceholder.Visible = true;
                        CustomerPlaceholder.Visible = false;
                        rbCompanyType.ClearSelection();
                        rbCompanyType.Items.FindByValue("Broker").Selected = true;
                    }
                    else
                    {
                        ddlCompany.ClearSelection();
                        ddlCompany.Items.FindByValue(dt.Rows[0]["CustomerID"].ToString()).Selected = true;
                        BrokerPlaceholder.Visible = false;
                        CustomerPlaceholder.Visible = true;
                        rbCompanyType.ClearSelection();
                        rbCompanyType.Items.FindByValue("Customer").Selected = true;

                    }

                    ddlpayment.ClearSelection();
                    ddlpayment.Items.FindByValue(dt.Rows[0]["PaymentTerm"].ToString()).Selected = true;

                    ddlCredit.ClearSelection();
                    ddlCredit.Items.FindByValue(dt.Rows[0]["CreditTerm"].ToString()).Selected = true;

                    //ddlInvoice.ClearSelection();
                    //ddlInvoice.Items.FindByValue(dt.Rows[0]["InvoiceFormat"].ToString()).Selected = true;

                    lnkDelete.Visible = true;
                }
            }
            else if (e.CommandName == "Details")
            {
                CompanyProfileDML dml = new CompanyProfileDML();
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow gvr = gvResult.Rows[index];
                Int64 ID = Convert.ToInt64(gvResult.DataKeys[index]["ProfileID"]);
                hfEditID.Value = ID.ToString();
                modalProfileDetail.Show();
                GetDataSpecificID(Convert.ToInt64(hfEditID.Value));
                DataTable dtProfile = dml.GetCompanyProfile(ID);
                Int64 CustomerID = 0;
                Int64 BrokerID = 0;
                if (dtProfile.Rows.Count > 0)
                {
                    CustomerID = Convert.ToInt64(dtProfile.Rows[0]["CustomerID"]);
                    BrokerID = Convert.ToInt64(dtProfile.Rows[0]["BrokerID"]);

                }

                DataTable dtProfileDetails = dml.GetCompanyProfileDetails(ID);
                if (CustomerID > BrokerID)
                {
                    LocationFromPlaceholder.Visible = true;
                    LocationToPlaceholder.Visible = true;
                    ContainerTypePlaceholder.Visible = true;
                    ContainerRatePlaceholder.Visible = true;
                    VehicleTypePlaceHolder.Visible = false;
                    VehicleRatePlaceholder.Visible = false;
                    ddlBroker.ClearSelection();
                    ddlCompany.ClearSelection();
                    ddlCompany.Items.FindByValue(dtProfile.Rows[0]["CustomerID"].ToString().Trim()).Selected = true;

                    rbCompanyType.ClearSelection();
                    rbCompanyType.Items.FindByText("Customer").Selected = true;




                    gvCustomerDetail.Visible = true;
                    gvBrokerDetail.Visible = false;

                    gvCustomerDetail.DataSource = dtProfileDetails.Rows.Count > 0 ? dtProfileDetails : null;
                    gvCustomerDetail.DataBind();

                }
                else if (BrokerID > CustomerID)
                {
                    LocationFromPlaceholder.Visible = false;
                    LocationToPlaceholder.Visible = false;
                    ContainerTypePlaceholder.Visible = false;
                    ContainerRatePlaceholder.Visible = false;
                    VehicleTypePlaceHolder.Visible = true;
                    VehicleRatePlaceholder.Visible = true;
                    ddlCompany.ClearSelection();
                    ddlBroker.ClearSelection();
                    ddlBroker.Items.FindByValue(dtProfile.Rows[0]["BrokerID"].ToString().Trim()).Selected = true;

                    rbCompanyType.ClearSelection();
                    rbCompanyType.Items.FindByText("Broker").Selected = true;


                    gvBrokerDetail.Visible = true;
                    gvCustomerDetail.Visible = false;
                    gvBrokerDetail.DataSource = dtProfileDetails.Rows.Count > 0 ? dtProfileDetails : null;
                    gvBrokerDetail.DataBind();
                }


            }
            else if (e.CommandName == "DeleteProfile")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow gvr = gvResult.Rows[index];
                Int64 ID = Convert.ToInt64(gvResult.DataKeys[index]["ProfileID"]);
                CompanyProfileDML profile = new CompanyProfileDML();
                profile.DeleteCompanyProfile(ID);
                notification("Success", "1 Profile has been Deleted");
                GetCompanyProfile();
                ClearItem();
            }
        }

        protected void gvCustomerDetail_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //gvBrokerDetail.Visible = false;
            if (e.CommandName == "ChangeDetail")
            {
                try
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvCustomerDetail.Rows[index];
                    Int64 ID = Convert.ToInt64(gvCustomerDetail.DataKeys[index]["ProfileDetail"]);
                    hfEditDetail.Value = ID.ToString();
                    //SqlDataAdapter sda = new SqlDataAdapter("Select * FROM CustomerProfileDetail where ProfileDetail = '"+Convert.ToInt64(hfEditDetail.Value)+"' ", Xcon);
                    //DataTable dt = new DataTable();
                    //sda.Fill(dt);
                    CompanyProfileDML profile = new CompanyProfileDML();
                    DataTable dt = profile.GetCompanyProfileDetailsWithID(Convert.ToInt64(hfEditDetail.Value));
                    if (dt.Rows.Count > 0)
                    {
                        ddlLocationFrom.ClearSelection();
                        ddlLocationFrom.Items.FindByValue(dt.Rows[0]["PickupLocationID"].ToString()).Selected = true;

                        ddlLocationTo.ClearSelection();
                        ddlLocationTo.Items.FindByValue(dt.Rows[0]["DropoffLocationID"].ToString()).Selected = true;

                        ddlContainerType.ClearSelection();
                        ddlContainerType.Items.FindByValue(dt.Rows[0]["ContainerTypeID"].ToString()).Selected = true;

                        txtContainerRate.Text = dt.Rows[0]["ContainerRate"].ToString();





                    }
                }
                catch (Exception ex)
                {
                    modalNotification("Error", ex.Message);
                }
                finally
                {
                    modalProfileDetail.Show();
                }
            }
            else if (e.CommandName == "DeleteDetail")
            {
                try
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvCustomerDetail.Rows[index];
                    Int64 ID = Convert.ToInt64(gvCustomerDetail.DataKeys[index]["ProfileDetail"]);
                    CompanyProfileDML profile = new CompanyProfileDML();
                    profile.DeleteCompanyProfileDetail(ID);
                    modalNotification("Success", "1 Detail Deleted Successful");
                    ClearItem();

                    GetDataSpecificID(Convert.ToInt64(hfEditID.Value));
                }
                catch (Exception ex)
                {
                    modalNotification("Error", ex.Message);
                }
                finally
                {
                    modalProfileDetail.Show();
                }
            }
        }

        protected void gvBrokerDetail_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            gvCustomerDetail.Visible = false;
            if (e.CommandName == "ChangeDetail")
            {
                try
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvBrokerDetail.Rows[index];
                    Int64 ID = Convert.ToInt64(gvBrokerDetail.DataKeys[index]["ProfileDetail"]);
                    hfEditDetail.Value = ID.ToString();

                    CompanyProfileDML profile = new CompanyProfileDML();
                    DataTable dt = profile.GetCompanyBrokerDetailsWithID(Convert.ToInt64(hfEditDetail.Value));
                    if (dt.Rows.Count > 0)
                    {
                        ddlVehicleType.ClearSelection();
                        ddlVehicleType.Items.FindByValue(dt.Rows[0]["VehicleTypeID"].ToString()).Selected = true;


                        txtVehicleRate.Text = dt.Rows[0]["BrokerRate"].ToString();


                    }
                }
                catch (Exception ex)
                {
                    modalNotification("Error", ex.Message);
                }
                finally
                {
                    modalProfileDetail.Show();
                }
            }
            else if (e.CommandName == "DeleteDetail")
            {
                try
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvBrokerDetail.Rows[index];
                    Int64 ID = Convert.ToInt64(gvBrokerDetail.DataKeys[index]["ProfileDetail"]);
                    CompanyProfileDML profile = new CompanyProfileDML();
                    profile.DeleteCompanyProfileDetail(ID);
                    modalNotification("Success", "1 Detail Deleted Successful");
                    ClearItem();

                    GetDataSpecificID(Convert.ToInt64(hfEditID.Value));
                }
                catch (Exception ex)
                {
                    modalNotification("Error", ex.Message);
                }
                finally
                {
                    modalProfileDetail.Show();
                }
            }
        }

        #endregion

        #region Misc

        protected void cbCompanyType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbCompanyType.SelectedItem.Text == "Customer")
                {
                    CustomerPlaceholder.Visible = true;
                    BrokerPlaceholder.Visible = false;

                    ddlBroker.ClearSelection();
                }
                else if (rbCompanyType.SelectedItem.Text == "Broker")
                {
                    BrokerPlaceholder.Visible = true;
                    CustomerPlaceholder.Visible = false;

                    ddlCompany.ClearSelection();
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error selecting company type, due to: " + ex.Message);
            }
        }

        #endregion

        #endregion
    }
}