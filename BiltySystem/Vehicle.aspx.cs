using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiltySystem
{
    public partial class Vehicle : System.Web.UI.Page
    {
        #region Members

        int loginid;
        Random rnd = new Random();
        #endregion#region Properties

        #region Properties

        //public int LoginID
        //{
        //    get
        //    {
        //        if (Request.QueryString["lid"] != string.Empty && Request.QueryString["lid"] != null)
        //        {
        //            loginid = Convert.ToInt32(Request.QueryString["lid"].ToString());
        //        }
        //        return loginid;

        //    }
        //}

        public int LoginID
        {
            get
            {
                if (Session["UserID"] != string.Empty && Session["UserID"] != null)
                {
                    loginid = Convert.ToInt32(Session["UserID"]);
                }
                return loginid;

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
        public void DetailsModalNotification(string type, string msg)
        {
            try
            {
                if (type == "Error")
                {
                    divDetailsNotification.InnerHtml = "<div class=\"alert alert-danger\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                }
                else if (type == "Success")
                {
                    divDetailsNotification.InnerHtml = "<div class=\"alert alert-success\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                }
                else if (type == string.Empty)
                {
                    divDetailsNotification.InnerHtml = string.Empty;
                }
            }
            catch (Exception ex)
            {
                divNotification.InnerHtml = "<div class=\"alert alert-danger\">" + ex.Message + "</div>";
            }
        }

        public void ModalNotification(string type, string msg)
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
        public void GetVehicle()
        {
            VehicleDML dml = new VehicleDML();

            DataTable dtveh = dml.GetVehicle();
            if (dtveh.Rows.Count > 0)
            {
                gvResult.DataSource = dtveh;
            }
            else
            {
                gvResult.DataSource = null;

            }

            gvResult.DataBind();
        }
        private void clearfield()
        {
            hfEditID.Value = string.Empty;
            txtCode.Text = "";
            txtReg.Text = "";
            txtEngin.Text = "";
            txtChasses.Text = "";
            txtmake.Text = "";
            txtmodel.Text = "";
            txtmanu.Text = "";
            txtcolor.Text = "";
            txtbodytype.Text = "";
            txtMLC.Text = "";
            txtloadingNHA.Text = "";
            txtPamount.Text = "";
            txtPdate.Text = "";
            txtPdetails.Text = "";
            txtPfrom.Text = "";
            txtOcontact.Text = "";
            txtOname.Text = "";
            txtOnic.Text = "";
            txtDes.Text = "";
            //ddlOwnership.ClearSelection();
            ddlVehicleType.ClearSelection();

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
        public DataTable GetBrokerByName(string Name)
        {
            BrokersDML dmlBroker = new BrokersDML();
            DataTable dtBroker = dmlBroker.GetBrokerByName(Name);
            if (dtBroker.Rows.Count > 0)
            {
                return dtBroker;
            }
            else
            {
                CodeGenerator cg = new CodeGenerator();
                string code = cg.generateCode(Name);
                int status = dmlBroker.InsertBroker(code, Name, LoginID);
                if (status > 0)
                {
                    dtBroker = dmlBroker.GetBrokerByName(Name);
                    return dtBroker;
                }
                return dtBroker;
            }
        }
        public void GetDetailsData(Int64 VehicleID)
        {
            try
            {
                VehicleDML dml = new VehicleDML();
                DataTable dt = dml.GetVehicleDocuments(VehicleID);
                gvDetails.DataSource = dt;
                gvDetails.DataBind();
            }
            catch (Exception ex)
            {
                DetailsModalNotification("Error", "Cannot Get Data Due To : " + ex.Message);
            }
        }
        #endregion

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {

            notification("", "");
            if (!IsPostBack)
            {
                lnkDelete.Visible = false;
                GetVehicle();


                VehicleTypeDML vt = new VehicleTypeDML();
                DataTable dtvehivle = vt.GetVehicleType();
                BrokersDML dmlBroker = new BrokersDML();
                DataTable dtBrokers = dmlBroker.GetAllBrokers();
                if (dtvehivle.Rows.Count > 0)
                {
                    FillDropDown(dtvehivle, ddlVehicleType, "VehicleTypeID", "VehicleTypeName", "Select Type");
                }
                if (dtBrokers.Rows.Count > 0)
                {
                    FillDropDown(dtBrokers, ddlBroker, "ID", "Name", "Select Broker");
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


        #region Event

        #region Link Button
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
                if (txtReg.Text == string.Empty)
                {
                    notification("Error", "Please enter Registration no");
                    txtReg.Focus();
                }
                else if (ddlVehicleType.SelectedIndex == 0)
                {
                    notification("Error", "Please Select Vehicle type");
                    ddlVehicleType.Focus();
                }
                else if (txtPdate.Text == string.Empty)
                {
                    notification("Error", "Please enter purchase date");
                    txtPdate.Focus();
                }
                else if (rbVehicleOwnerShip.SelectedIndex < 0)
                {
                    notification("Error", "Please Select Ownership");
                    rbVehicleOwnerShip.Focus();
                }
                else if (rbVehicleOwnerShip.SelectedItem.Text == "Hired" && ddlBroker.SelectedIndex == 0)
                {
                    notification("Error", "Please enter Broker");
                    ddlBroker.Focus();
                }

                //else if (txtPdate.Text == string.Empty)
                //{
                //    notification("Error", "Please Enter purchase date");
                //    txtPdate.Focus();
                //}
                else
                {
                    string Regno = txtReg.Text;
                    CodeGenerator cg = new CodeGenerator();
                    string Code = txtCode.Text.Trim() == string.Empty ? cg.generateCode(Regno, ddlVehicleType.SelectedItem.Text) : txtCode.Text.Trim();
                    VehicleDML dm = new VehicleDML();
                    DataTable dt = dm.GetVehicle(Code, Regno);
                    if (hfEditID.Value.ToString() == string.Empty)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            notification("Error", "this Code or Registration no already exist in databse please try to change code or Registartion no");
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
                            notification("Error", "this Code or Registration no already exist in databse please try to change code or Registartion no");
                        }
                        else
                        {
                            ConfirmModal("Are you sure want to update?", "Update");

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

            GetVehicle();
            pnlInput.Visible = false;
        }

        protected void lnkCancelSearch_Click(object sender, EventArgs e)
        {
            try
            {
                GetVehicle();
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
                pnlFileUpload.Visible = false;
                clearfield();
            }
            catch (Exception ex)
            {

                notification("Error", ex.Message);
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
            VehicleDML dm = new VehicleDML();
            DataTable dtdept = dm.GetVehicle(keyword);
            if (dtdept.Rows.Count > 0)
            {
                gvResult.DataSource = dtdept;
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
                        VehicleDML dml = new VehicleDML();
                        dml.DeleteVehicle(id);
                        clearfield();
                        lnkDelete.Visible = false;
                        notification("Success", "Deleted Successfully");
                        pnlInput.Visible = false;
                        GetVehicle();
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
                        string Regno = txtReg.Text;
                        string EngNo = txtEngin.Text;
                        string chasis = txtChasses.Text;
                        string make = txtmake.Text;
                        string model = txtmodel.Text;
                        string manuf = txtmanu.Text;
                        string color = txtcolor.Text;
                        string bType = txtbodytype.Text;
                        Int64 vType = Convert.ToInt64(ddlVehicleType.SelectedValue);
                        Int64 MLC = txtMLC.Text == string.Empty ? 0 : Convert.ToInt64(txtMLC.Text);
                        Int64 LLimit = txtloadingNHA.Text == string.Empty ? 0 : Convert.ToInt64(txtloadingNHA.Text);
                        DateTime pDate = Convert.ToDateTime(txtPdate.Text);
                        Int64 pAmount = txtPamount.Text == string.Empty ? 0 : Convert.ToInt64(txtPamount.Text);
                        string pFrom = txtPfrom.Text;
                        string pDetails = txtPdetails.Text;
                        string OwName = txtOname.Text;
                        string OwContact = txtOcontact.Text;
                        string OwNIC = txtOnic.Text;
                        string owDes = txtDes.Text;
                        double lentgh = txtLength.Text == string.Empty ? 0 : Convert.ToDouble(txtLength.Text);
                        double Width = txtWidth.Text == string.Empty ? 0 : Convert.ToDouble(txtWidth.Text);
                        double Height = txtHeight.Text == string.Empty ? 0 : Convert.ToDouble(txtHeight.Text);
                        string Dimension = txtDimension.Text;
                        string Type = txtType.Text;
                        string manufacYear = txtManufacYear.Text;
                        //int IsOwnVehicle = cbIsOwnVehicle.Checked == true ? 1 : 0;
                        //string owShip = ddlOwnership.SelectedValue;
                        string doc;
                        CodeGenerator cg = new CodeGenerator();
                        string Code = txtCode.Text.Trim() == string.Empty ? cg.generateCode(Regno) : txtCode.Text.Trim();
                        Int64 brokerID = 0;
                        if (ddlBroker.SelectedIndex != 0)
                        {
                            BrokersDML dmlBroker = new BrokersDML();
                            string brokerName = ddlBroker.SelectedItem.Text.Trim();
                            DataTable dtBroker = dmlBroker.GetBrokerByName(brokerName);
                            brokerID = Convert.ToInt64(dtBroker.Rows[0]["ID"]);
                        }
                        int IsOwnVehicle = Convert.ToInt32(rbVehicleOwnerShip.SelectedItem.Text == "Own" ? "0" : "1");
                        string OwnerShipStatus = rbVehicleOwnerShip.SelectedItem.Text;
                        if (OwnerShipStatus == "Own")
                        {
                            DataTable dt = GetBrokerByName("SA Shah");
                            brokerID = Convert.ToInt64(dt.Rows[0]["ID"]);
                        }
                        VehicleDML dm = new VehicleDML();
                        string fileExt = System.IO.Path.GetExtension(FileUpload1.FileName);
                        doc = MapPath("img");
                        doc = doc + "/" + Convert.ToDateTime(DateTime.Now).ToString("dd-MM-yy") + FileUpload1.FileName;
                        FileUpload1.SaveAs(doc);

                        string FileName = System.IO.Path.GetFileName(doc);
                        dm.InsertVehicle(Code, Regno, EngNo, chasis, make, model, manuf, color, bType, vType, MLC, LLimit, pDate, pAmount, pFrom, pDetails, OwName, OwContact, OwNIC, owDes, OwnerShipStatus, LoginID, lentgh, Width, Height, Dimension, Type, manufacYear, IsOwnVehicle, FileName, brokerID);
                        GetVehicle();
                        clearfield();
                        notification("Success", "Submited Successfully");
                        pnlInput.Visible = false;
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
                        string Regno = txtReg.Text;
                        string EngNo = txtEngin.Text;
                        string chasis = txtChasses.Text;
                        string make = txtmake.Text;
                        string model = txtmodel.Text;
                        string manuf = txtmanu.Text;
                        string color = txtcolor.Text;
                        string bType = txtbodytype.Text;
                        Int64 vType = Convert.ToInt64(ddlVehicleType.SelectedValue);
                        Int64 MLC = txtMLC.Text == string.Empty ? 0 : Convert.ToInt64(txtMLC.Text);
                        Int64 LLimit = txtloadingNHA.Text == string.Empty ? 0 : Convert.ToInt64(txtloadingNHA.Text);
                        DateTime pdate;
                        if (txtPdate.Text == string.Empty)
                        {
                            pdate = Convert.ToDateTime(hfPurchaseDate.Value);
                        }
                        else
                        {
                            pdate = Convert.ToDateTime(txtPdate.Text);
                        }
                        Int64 pAmount = txtPamount.Text == string.Empty ? 0 : Convert.ToInt64(txtPamount.Text);
                        string pFrom = txtPfrom.Text;
                        string pDetails = txtPdetails.Text;
                        string OwName = txtOname.Text;
                        string OwContact = txtOcontact.Text;
                        string OwNIC = txtOnic.Text;
                        string owDes = txtDes.Text;
                        double lentgh = txtLength.Text == string.Empty ? 0 : Convert.ToDouble(txtLength.Text);
                        double Width = txtWidth.Text == string.Empty ? 0 : Convert.ToDouble(txtWidth.Text);
                        double Height = txtHeight.Text == string.Empty ? 0 : Convert.ToDouble(txtHeight.Text);
                        string Dimension = txtDimension.Text;
                        string Type = txtType.Text;
                        string manufacYear = txtManufacYear.Text;
                        string doc;
                        Int64 brokerID = 0;
                        if (ddlBroker.SelectedIndex > 0)
                        {
                            string BrokerName = ddlBroker.SelectedItem.Text.Trim();
                            BrokersDML dmlBroker = new BrokersDML();
                            DataTable dtBroker = dmlBroker.GetBrokerByName(BrokerName);
                            brokerID = Convert.ToInt64(dtBroker.Rows[0]["ID"]);
                        }
                        else
                        {
                            BrokersDML dmlBroker = new BrokersDML();
                            DataTable dtBroker = dmlBroker.GetBrokerByName("SA Shah");
                            brokerID = Convert.ToInt64(dtBroker.Rows[0]["ID"]);
                        }
                        //string BrokerName = ddlBroker.SelectedItem.Text.Trim();
                        //Int64 brokerID = 0;
                        //if (BrokerName != string.Empty)
                        //{
                        //    BrokersDML dmlBroker = new BrokersDML();
                        //    DataTable dtBroker = dmlBroker.GetBrokerByName(BrokerName);
                        //    brokerID = Convert.ToInt64(dtBroker.Rows[0]["ID"]);
                        //}
                        int IsOwnVehicle = Convert.ToInt32(rbVehicleOwnerShip.SelectedItem.Text == "Own" ? "0" : "1");
                        string OwnerShipStatus = rbVehicleOwnerShip.SelectedItem.Text;
                        VehicleDML dm = new VehicleDML();
                        string fileExt = System.IO.Path.GetExtension(FileUpload1.FileName);
                        doc = MapPath("img");
                        doc = doc + "/" + Convert.ToDateTime(DateTime.Now).ToString("ddMMyy") + FileUpload1.FileName;
                        FileUpload1.SaveAs(doc);
                        if (FileUpload1.HasFile)

                            hfFileUpload.Value = FileUpload1.FileName;
                        string FileName = hfFileUpload.Value;
                        Int64 id = Convert.ToInt64(hfEditID.Value);
                        dm.UpdateVehicle(id, Code, Regno, EngNo, chasis, make, model, manuf, color, bType, vType, MLC, LLimit, pdate, pAmount, pFrom, pDetails, OwName, OwContact, OwNIC, owDes, OwnerShipStatus, LoginID, lentgh, Width, Height, Dimension, Type, manufacYear, IsOwnVehicle, FileName, brokerID);
                        GetVehicle();
                        clearfield();
                        notification("Success", "Updated Successfully");
                        lnkDelete.Visible = false;
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
        protected void lnkSaveDocument_Click(object sender, EventArgs e)
        {
            try
            {
                int count = 0;
                for (int i = 0; i < gvDocuments.Rows.Count; i++)
                {
                    DropDownList ddlDocumentType = gvDocuments.Rows[i].Cells[3].FindControl("ddlDocumentType") as DropDownList;
                    if (ddlDocumentType.SelectedIndex > 0)
                    {
                        count++;
                    }

                }
                if (count == gvDocuments.Rows.Count)
                {
                    for (int i = 0; i < gvDocuments.Rows.Count; i++)
                    {
                        Int64 VehicleID = Convert.ToInt64(hfEditID.Value);
                        string ImageName = gvDocuments.Rows[i].Cells[1].Text.Trim();
                        DropDownList ddlDocumentType = gvDocuments.Rows[i].Cells[3].FindControl("ddlDocumentType") as DropDownList;
                        string DocumentType = ddlDocumentType.SelectedItem.Text.Trim();
                        DocumentTypeDML dmlDocumentType = new DocumentTypeDML();
                        DataTable dtDocumentType = dmlDocumentType.GetDocumentTypeByName(DocumentType);
                        Int64 DocumentTypeID = Convert.ToInt64(dtDocumentType.Rows[0]["DocumentTypeID"]);
                        VehicleDML dml = new VehicleDML();
                        dml.InsertVehicleDocument(VehicleID, "~/img/VehicleDocument/" + ImageName, DocumentTypeID, LoginID);
                        modalDocuments.Show();
                        ModalNotification("Success", "Documents Save Successfully");
                        DataTable dt = dml.GetVehicleDocuments(VehicleID);
                    }
                }
                else
                {
                    ModalNotification("Error", "Please Select Document Type");
                    modalDocuments.Show();
                }
            }
            catch (Exception ex)
            {
                ModalNotification("Error", "Cannot Add Documents Due To: " + ex.Message);
                modalDocuments.Show();
            }
        }

        protected void lnkAddDocuments_Click(object sender, EventArgs e)
        {
            try
            {
                modalDocuments.Show();
            }
            catch (Exception ex)
            {
                notification("Error", "Cannot Open Documents Modal Due To : " + ex.Message);
            }
        }

        protected void lnlDocumentsUpload_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region GridView

        protected void gvResult_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "Change")
            {
                pnlInput.Visible = true;
                pnlView.Visible = false;
                lnkDelete.Visible = true;
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow gvr = gvResult.Rows[index];
                Int64 veh = Convert.ToInt64(gvResult.DataKeys[index]["VehicleID"]);
                hfEditID.Value = veh.ToString();


                VehicleDML dml = new VehicleDML();
                DataTable dtdept = dml.GetVehicle(veh);
                if (dtdept.Rows.Count > 0)
                {
                    txtCode.Text = dtdept.Rows[0]["VehicleCode"].ToString();
                    txtReg.Text = dtdept.Rows[0]["RegNo"].ToString();
                    txtEngin.Text = dtdept.Rows[0]["EngineNo"].ToString();
                    txtChasses.Text = dtdept.Rows[0]["ChasisNo"].ToString();
                    txtmake.Text = dtdept.Rows[0]["Make"].ToString();
                    txtmodel.Text = dtdept.Rows[0]["VehicleModel"].ToString();
                    txtmanu.Text = dtdept.Rows[0]["ManufacturerName"].ToString();
                    txtcolor.Text = dtdept.Rows[0]["VehicleColor"].ToString();
                    txtbodytype.Text = dtdept.Rows[0]["BodyType"].ToString();
                    txtMLC.Text = dtdept.Rows[0]["MaximumLoadingCapacity"].ToString();
                    txtloadingNHA.Text = dtdept.Rows[0]["LoadingLimitNHA"].ToString();
                    hfPurchaseDate.Value = dtdept.Rows[0]["PurchasingDate"].ToString();
                    txtPamount.Text = dtdept.Rows[0]["PurchasingAmount"].ToString();
                    txtPfrom.Text = dtdept.Rows[0]["PurchaseFromName"].ToString();
                    txtPdetails.Text = dtdept.Rows[0]["PurchaseFromDetail"].ToString();
                    txtOname.Text = dtdept.Rows[0]["OwnerName"].ToString();
                    txtOcontact.Text = dtdept.Rows[0]["OwnerContact"].ToString();
                    txtOnic.Text = dtdept.Rows[0]["OwnerNIC"].ToString();
                    txtDes.Text = dtdept.Rows[0]["Description"].ToString();
                    txtLength.Text = dtdept.Rows[0]["Length"].ToString();
                    txtWidth.Text = dtdept.Rows[0]["Width"].ToString();
                    txtHeight.Text = dtdept.Rows[0]["Height"].ToString();
                    txtDimension.Text = dtdept.Rows[0]["DimensionUnitType"].ToString();
                    txtType.Text = dtdept.Rows[0]["Type"].ToString();
                    txtManufacYear.Text = dtdept.Rows[0]["ManufacturingYear"].ToString();
                    hfFileUpload.Value = dtdept.Rows[0]["Document"].ToString();

                    //ddlOwnership.ClearSelection();
                    //ddlOwnership.Items.FindByValue(dtdept.Rows[0]["OwnershipStatus"].ToString()).Selected = true;

                    ddlVehicleType.ClearSelection();
                    ddlVehicleType.Items.FindByValue(dtdept.Rows[0]["VehicleTypeID"].ToString()).Selected = true;


                    bool isOwnveh = dtdept.Rows[0]["isOwnVehicle"].ToString() == "" ? false : Convert.ToBoolean(dtdept.Rows[0]["isOwnVehicle"]);
                    if (isOwnveh == true)
                        //cbIsOwnVehicle.Checked = true;

                        lnkDelete.Visible = true;
                    pnlFileUpload.Visible = true;
                    txtCode.Focus();

                }


            }
            else if (e.CommandName == "View")
            {
                pnlView.Visible = true;
                pnlInput.Visible = false;
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);

                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow gvr = gvResult.Rows[index];
                Int64 veh = Convert.ToInt64(gvResult.DataKeys[index]["VehicleID"]);
                hfEditID.Value = veh.ToString();


                VehicleDML dml = new VehicleDML();
                DataTable dtdept = dml.GetVehicle(veh);
                if (dtdept.Rows.Count > 0)
                {
                    lblCodemodal.Text = dtdept.Rows[0]["VehicleCode"].ToString();
                    lblregno.Text = dtdept.Rows[0]["RegNo"].ToString();
                    lblEnginNo.Text = dtdept.Rows[0]["EngineNo"].ToString();
                    lblChasisNo.Text = dtdept.Rows[0]["ChasisNo"].ToString();
                    lblMake.Text = dtdept.Rows[0]["Make"].ToString();
                    lblModel.Text = dtdept.Rows[0]["VehicleModel"].ToString();
                    lblManu.Text = dtdept.Rows[0]["ManufacturerName"].ToString();
                    lblcolor.Text = dtdept.Rows[0]["VehicleColor"].ToString();
                    lblBody.Text = dtdept.Rows[0]["BodyType"].ToString();
                    lblMLC.Text = dtdept.Rows[0]["MaximumLoadingCapacity"].ToString();
                    lblLoadLimit.Text = dtdept.Rows[0]["LoadingLimitNHA"].ToString();
                    lblPurchaseDate.Text = dtdept.Rows[0]["PurchasingDate"].ToString();
                    lblPamount.Text = dtdept.Rows[0]["PurchasingAmount"].ToString();
                    lblPfrom.Text = dtdept.Rows[0]["PurchaseFromName"].ToString();
                    lblPdetail.Text = dtdept.Rows[0]["PurchaseFromDetail"].ToString();
                    lblOname.Text = dtdept.Rows[0]["OwnerName"].ToString();
                    lblOcontact.Text = dtdept.Rows[0]["OwnerContact"].ToString();
                    lblONIC.Text = dtdept.Rows[0]["OwnerNIC"].ToString();
                    lblDescription.Text = dtdept.Rows[0]["Description"].ToString();
                    lblLength.Text = dtdept.Rows[0]["Length"].ToString();
                    lblWidth.Text = dtdept.Rows[0]["Width"].ToString();
                    lblHeight.Text = dtdept.Rows[0]["Height"].ToString();
                    lblDimenison.Text = dtdept.Rows[0]["DimensionUnitType"].ToString();
                    lblType.Text = dtdept.Rows[0]["Type"].ToString();
                    lblManufacture.Text = dtdept.Rows[0]["ManufacturingYear"].ToString();
                    lblOstatus.Text = dtdept.Rows[0]["OwnershipStatus"].ToString();
                    lblVehicle.Text = dtdept.Rows[0]["VehicleTypeID"].ToString();


                }
            }
            else if (e.CommandName == "Download")
            {
                try
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvResult.Rows[index];
                    string url = gvResult.Rows[index].Cells[7].Text;
                    System.IO.FileInfo file = new System.IO.FileInfo(url);
                    var filePath = System.Configuration.ConfigurationManager.AppSettings["FilePath"].ToString() + "" + url.ToString();

                    if (System.IO.File.Exists(filePath))
                    {
                        var extension = System.IO.Path.GetExtension(filePath);
                        var FileName = System.IO.Path.GetFileNameWithoutExtension(filePath);

                        Response.Clear();
                        Response.ContentType = "application/" + extension;
                        Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName + "" + extension);
                        Response.TransmitFile(filePath);
                        Response.End();
                    }
                    else
                    {
                        notification("Error", "it has no file");
                    }
                }
                catch (Exception ex)
                {

                    //notification("Error",ex.Message);
                }




            }
            else if (e.CommandName == "Active")
            {

                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow gvr = gvResult.Rows[index];
                Int64 CatId = Convert.ToInt64(gvResult.DataKeys[index]["VehicleID"]);
                string Active = gvResult.DataKeys[index]["isActive"].ToString() == string.Empty ? "False" : gvResult.DataKeys[index]["isActive"].ToString();
                VehicleDML dml = new VehicleDML();


                hfEditID.Value = CatId.ToString();

                if (Active == "True")
                {
                    dml.DeactivateVehicle(CatId, LoginID);
                }
                else
                {
                    dml.ActivateVehicle(CatId, LoginID);
                }
                GetVehicle();
                clearfield();

            }
            else if (e.CommandName == "Details")
            {
                try
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = gvResult.Rows[index];
                    Int64 VehicleID = Convert.ToInt64(gvResult.DataKeys[index]["VehicleID"]);
                    GetDetailsData(VehicleID);
                    hfVehicleID.Value = VehicleID.ToString();

                    modalDetails.Show();
                }
                catch (Exception ex)
                {
                    notification("Error", "Cannot open modal due to : " + ex.Message);
                }

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
        protected void gvDocuments_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "trash")
                {
                }

            }
            catch (Exception ex)
            {
                ModalNotification("Error", "Cannot Remove Due To: " + ex.Message);
            }
            finally
            {
                modalDocuments.Show();
            }
        }

        protected void gvDocuments_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }

        protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "trash")
            {
                try
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    Int64 VehicleDetailsID = Convert.ToInt64(gvDetails.DataKeys[index]["ID"]);
                    VehicleDML dml = new VehicleDML();
                    dml.DeleteVehilceDetails(VehicleDetailsID, LoginID);
                    Int64 VehicleID = Convert.ToInt64(hfVehicleID.Value);
                    GetDetailsData(VehicleID);
                    modalDetails.Show();
                }
                catch (Exception ex)
                {
                    DetailsModalNotification("Error", "Cannot Delete Due To : " + ex.Message);
                }
                finally
                {
                    modalDetails.Show();
                }

            }
        }

        #endregion

        #region RadioButton
        protected void rbVehicleOwnerShip_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbVehicleOwnerShip.SelectedItem.Text == "Hired")
                {
                    ddlBroker.Visible = true;
                    lblBroker.Visible = true;
                }
                else
                {
                    ddlBroker.SelectedIndex = 0;
                    ddlBroker.Visible = false;
                    lblBroker.Visible = false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Panel

        protected void pnlDocumentsClose_Click(object sender, EventArgs e)
        {
            try
            {
                modalDocuments.Hide();
                gvDocuments.DataSource = null;
                gvDocuments.DataBind();
            }
            catch (Exception ex)
            {
                notification("Error", "Cannot Hide Document Modal Due To : " + ex.Message);
            }
        }
        #endregion

        #region FileUpload Button

        protected void btnFileUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (fuDocuments.HasFile)
                {
                    DataTable dtDocuments = new DataTable();
                    int count = fuDocuments.PostedFiles.Count;
                    dtDocuments.Columns.Add("FileName");
                    dtDocuments.Columns.Add("DocumentType");
                    dtDocuments.Columns.Add("File");

                    string folderPath = Server.MapPath("~/img/VehicleDocument/");
                    //Check whether Directory (Folder) exists.
                    if (!Directory.Exists(folderPath))
                    {
                        //If Directory (Folder) does not exists. Create it.
                        Directory.CreateDirectory(folderPath);
                    }
                    foreach (var DocumentName in fuDocuments.PostedFiles)
                    {
                        //DocumentName.SaveAs(System.IO.Path.Combine(folderPath, DocumentName.FileName));
                        DocumentName.SaveAs(folderPath + DocumentName.FileName);
                        dtDocuments.Rows.Add(DocumentName.FileName, string.Empty, folderPath + DocumentName.FileName);
                    }
                    gvDocuments.DataSource = dtDocuments;
                    gvDocuments.DataBind();
                    gvDocuments.Visible = true;
                    lnkSaveDocument.Visible = true;
                    modalDocuments.Show();
                    try
                    {
                        DocumentTypeDML dmlDocumentType = new DocumentTypeDML();
                        DataTable dtDocumentType = dmlDocumentType.GetDocumentType();
                        for (int i = 0; i < gvDocuments.Rows.Count; i++)
                        {
                            GridViewRow gvRow = gvDocuments.Rows[i];
                            DropDownList ddlDocument = gvRow.Cells[2].FindControl("ddlDocumentType") as DropDownList;
                            FillDropDown(dtDocumentType, ddlDocument, "DocumentTypeID", "Name", "-Select-");
                        }

                    }
                    catch (Exception ex)
                    {
                        ModalNotification("Error", "Error getting/populating Document Types, Due To: " + ex.Message);
                        modalDocuments.Show();
                    }
                }
                else
                {
                    ModalNotification("Error", "Atleast One Document is Required To Upload");
                    modalDocuments.Show();
                }
            }
            catch (Exception ex)
            {
                ModalNotification("Error", "Cannot Upload Files Due To : " + ex.Message);
                modalDocuments.Show();
            }
        }
        #endregion
        #endregion












    }
}