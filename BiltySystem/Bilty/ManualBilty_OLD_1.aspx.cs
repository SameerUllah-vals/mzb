using BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiltySystem.Bilty
{
    public partial class ManualBilty_OLD_1 : System.Web.UI.Page
    {
                
        #region Page Load

        protected void Page_Load(object sender, EventArgs e)
        {
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
                        FillDropDown(dtDocumentType, ddlDocumentType, "DocumentTypeID", "Name", "-Select Type-");
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
                        FillDropDown(dtItem, ddlDamageItem, "ItemID", "ItemName", "- Select-");
                    }
                }
                catch (Exception ex)
                {
                    notification("Error", "Error getting/populating Items, due to: " + ex.Message);
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

        protected void lnkAddContainerType_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlContainerType.SelectedIndex == 0)
                {
                    notification("Error", "Please select container type");
                    ddlContainerType.Focus();
                }
                else if (txtContainerQty.Text == string.Empty)
                {
                    notification("Error", "Please enter container qty");
                    txtContainerQty.Focus();
                }
                else if (txtTotalGrossWeight.Text == string.Empty)
                {
                    notification("Error", "Please enter container total gross weight");
                    txtTotalGrossWeight.Focus();
                }
                else
                {
                    string ContainerType = ddlContainerType.SelectedItem.Text;
                    string Qty = txtContainerQty.Text;
                    string Weight = txtTotalGrossWeight.Text;


                    DataTable dt = new DataTable();
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
                                Int64 OldQty = Convert.ToInt64(gvConsignmentInfo.Rows[i].Cells[1].Text);
                                double OldWeight = Convert.ToDouble(gvConsignmentInfo.Rows[i].Cells[2].Text);

                                Int64 NewQty = Convert.ToInt64(txtContainerQty.Text);
                                double NewWeight = Convert.ToDouble(txtTotalGrossWeight.Text);

                                NewQty += OldQty;
                                NewWeight += OldWeight;

                                dt.Rows.Add(gvConsignmentInfo.Rows[i].Cells[0].Text, NewQty, NewWeight);
                                AddNew = false;
                            }
                            else
                            {
                                dt.Rows.Add(gvConsignmentInfo.Rows[i].Cells[0].Text, gvConsignmentInfo.Rows[i].Cells[1].Text, gvConsignmentInfo.Rows[i].Cells[2].Text);
                            }

                        }
                    }

                    if (AddNew == true)
                    {
                        dt.Rows.Add(ContainerType, Qty, Weight);
                    }

                    gvConsignmentInfo.DataSource = dt;
                    gvConsignmentInfo.DataBind();

                    ddlContainerType.ClearSelection();
                    txtContainerQty.Text = string.Empty;
                    txtTotalGrossWeight.Text = string.Empty;

                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error add container type, due to: " + ex.Message);
            }
        }

        protected void lnkAddVehicleType_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlVehicleType.SelectedIndex == 0)
                {
                    notification("Error", "Please select vehicle type");
                    ddlVehicleType.Focus();
                }
                else if (txtVehicleQuantity.Text == string.Empty)
                {
                    notification("Error", "Please enter vehicle quantity");
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
                notification("Error", "Error adding vehicle type, due to: " + ex.Message);
            }
        }

        protected void lnkAddProduct_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPackageType.Text == string.Empty)
                {
                    notification("Error", "Please enter package type");
                    txtPackageType.Focus();
                }
                else if (txtItem.Text == string.Empty)
                {
                    notification("Error", "Please enter Item");
                    txtItem.Focus();
                }
                else if (txtProductQty.Text == string.Empty)
                {
                    notification("Error", "Please enter product quantity");
                    txtProductQty.Focus();
                }
                else if (txtTotalProductWeight.Text == string.Empty)
                {
                    notification("Error", "Please enter product total weight");
                    txtTotalProductWeight.Focus();
                }
                else
                {
                    string PackageType = txtPackageType.Text;
                    string Item = txtItem.Text;
                    Int64 Quantity = Convert.ToInt64(txtProductQty.Text);
                    double TotalWeight = Convert.ToDouble(txtTotalProductWeight.Text);

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
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error adding product, due to: " + ex.Message);
            }
        }

        protected void lnkAddDispatchDocument_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlDocumentType.SelectedIndex == 0)
                {
                    notification("Error", "Please select Document Type");
                    ddlDocumentType.Focus();
                }
                else if (txtDocumentNo.Text == string.Empty)
                {
                    notification("Error", "Please enter Document No.");
                    txtDocumentNo.Focus();
                }
                else
                {
                    string DocumentType = ddlDocumentType.SelectedItem.Text;
                    string DocumentNo = txtDocumentNo.Text;

                    DataTable dt = new DataTable();
                    dt.Columns.Add("DocumentType");
                    dt.Columns.Add("DocumentNo.");
                    dt.Columns.Add("DocumentName");

                    if (gvDispatchDocument.Rows.Count > 0)
                    {
                        for (int i = 0; i < gvDispatchDocument.Rows.Count; i++)
                        {
                            dt.Rows.Add(gvDispatchDocument.Rows[i].Cells[0].Text, gvDispatchDocument.Rows[i].Cells[1].Text, gvDispatchDocument.Rows[i].Cells[2].Text);
                        }
                    }

                    if (fuDispatchDoc.HasFile)
                    {

                        string sessionName = "DynamicSession" + gvDispatchDocument.Rows.Count;
                        Session[sessionName] = fuDispatchDoc;
                        FileUpload fuDoc = Session[sessionName] as FileUpload;
                        dt.Rows.Add(DocumentType, DocumentNo, fuDoc.FileName);
                        gvDispatchDocument.DataSource = dt;
                        gvDispatchDocument.DataBind();

                        //if (System.Web.HttpContext.Current.Session[sessionName] != null)
                        //{

                        //}
                        //else
                        //{

                        //}
                    }


                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error add Document, due to: " + ex.Message);
            }
        }

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
                    dt.Columns.Add("DocumentNo.");
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
                        string DamageCost = txtDamageCause.Text;
                        string DamageCause = txtDamageCause.Text;

                        DataTable dt = new DataTable();
                        dt.Columns.Add("Item");
                        dt.Columns.Add("Damage Type");
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

        #endregion

        protected void lnkContainerInfo_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("ContainerType");
                dt.Columns.Add("ContainerNo");
                dt.Columns.Add("Weight");
                dt.Columns.Add("EmptyContainerPickUpLocation");
                dt.Columns.Add("EmptyContainerDropoffLocation");
                dt.Columns.Add("VesselName");
                dt.Columns.Add("Remarks");
                for (int i = 0; i < gvConsignmentInfo.Rows.Count; i++)
                {
                    string ContainerType = gvConsignmentInfo.Rows[i].Cells[0].Text;
                    Int64 Quantity = Convert.ToInt64(gvConsignmentInfo.Rows[i].Cells[1].Text);
                    if (Quantity > 1)
                    {
                        for (int j = 0; j < Quantity; j++)
                        {
                            dt.Rows.Add(ContainerType);
                        }
                    }
                }

                gvContainerInfo.DataSource = dt;
                gvContainerInfo.DataBind();
                modalContainerInfo.Show();
            }
            catch (Exception ex)
            {
                notification("Error", "Error opening container info panel, due to: " + ex.Message);
            }
        }

        protected void gvContainerInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList ddlPickLocation = e.Row.Cells[5].FindControl("ddlPickUpLocation") as DropDownList;
                    DropDownList ddlDropOffLocation = e.Row.Cells[5].FindControl("ddlDropoffLocation") as DropDownList;

                    ManualBiltyDML dml = new ManualBiltyDML();

                    DataTable dtPickUpLocation = dml.GetPickUpLocation();
                    FillDropDown(dtPickUpLocation, ddlPickLocation, "PickDropID", "PickUp", "- Select -");

                    DataTable dtDropOffLocation = dml.GetDropOffLocation();
                    FillDropDown(dtDropOffLocation, ddlDropOffLocation, "PickDropID", "DropLocation", "- Select -");
                    
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error Binding row, due to: " + ex.Message);
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
                    notification("Error", "Please add Vehicle types first");
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error adding vehicle info, due to: " + ex.Message);
            }
        }

        protected void lnkSaveVehicleInfo_Click(object sender, EventArgs e)
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

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchCustomers(string prefixText, int count)
        {

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    //cmd.CommandText = "select ContactName from Customers where " + "ContactName like @SearchText + '%'";
                    cmd.CommandText = "SELECT CONCAT(CONCAT(p.Code + ' | ' + p.Name + ' | ' + pt.PackageTypeName, ' | '), p.Weight) AS Product FROM Product p INNER JOIN PackageType pt ON pt.PackageTypeID = p.PackageTypeID WHERE p.Code LIKE '%' + @SearchText + '%' OR p.Name LIKE '%' + @SearchText + '%' OR pt.PackageTypeName LIKE '%' + @SearchText + '%'";
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

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    //cmd.CommandText = "select ContactName from Customers where " + "ContactName like @SearchText + '%'";
                    cmd.CommandText = "SELECT CONCAT(CONCAT(p.Code + ' | ' + p.Name + ' | ' + pt.PackageTypeName, ' | '), p.Weight) AS Product FROM Product p INNER JOIN PackageType pt ON pt.PackageTypeID = p.PackageTypeID WHERE p.Code LIKE '%' + @SearchText + '%' OR p.Name LIKE '%' + @SearchText + '%' OR pt.PackageTypeName LIKE '%' + @SearchText + '%'";
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
    }
}
