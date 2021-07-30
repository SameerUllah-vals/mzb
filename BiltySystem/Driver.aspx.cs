using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;

namespace BiltySystem
{
    public partial class Driver : System.Web.UI.Page
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

        public void GetAllDrivers()
        {
            try
            {
                DriversDML dml = new DriversDML();
                DataTable dtDrivers = dml.GetDrivers();
                if (dtDrivers.Rows.Count > 0)
                {
                    gvResult.DataSource = dtDrivers;
                }
                else
                {
                    gvResult.DataSource = null;
                }
                gvResult.DataBind();
            }
            catch (Exception ex)
            {
                notification("Error", "Error getting drivers due to: " + ex.Message);
            }
        }

        public void ClearFields()
        {
            try
            {
                hfEditID.Value = string.Empty;
                hfdob.Value = string.Empty;

                txtCode.Text = string.Empty;
                txtName.Text = string.Empty;
                txtFatherName.Text = string.Empty;
                txtDateOfBirth.Text = string.Empty;
                txtBloodGroup.Text = string.Empty;
                txtCellNo.Text = string.Empty;
                txtOtherContact.Text = string.Empty;
                txtHomeNo.Text = string.Empty;
                txtAddress.Text = string.Empty;
                txtNICNo.Text = string.Empty;
                txtIDMark.Text = string.Empty;
                txtNICIssueDate.Text = string.Empty;
                txtNICExpiryDate.Text = string.Empty;
                txtLicenseNo.Text = string.Empty;
                txtIssuingAuthority.Text = string.Empty;
                txtLicenseIssueDate.Text = string.Empty;
                txtLicenseExpiryDate.Text = string.Empty;
                txtIssuingAuthority.Text = string.Empty;
                txtEmerContactName.Text = string.Empty;
                txtEmerContactNo.Text = string.Empty;
                txtEmerContactRelation.Text = string.Empty;
                txtDescription.Text = string.Empty;

                ddlType.ClearSelection();
                ddlGender.ClearSelection();
                ddlLicenseCategory.ClearSelection();
                ddlLicenseType.ClearSelection();
            }
            catch (Exception ex)
            {
                notification("Error", "Error clearing fields, due to: " + ex.Message);
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

        //private void BindDriverImages()
        //{
        //    string constr = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
        //    using (SqlConnection con = new SqlConnection(constr))
        //    {
        //        using (SqlCommand cmd = new SqlCommand())
        //        {
        //            cmd.CommandText = "SELECT Id, Name FROM DriverImage";
        //            cmd.Connection = con;
        //            con.Open();
        //            GridView1.DataSource = cmd.ExecuteReader();
        //            GridView1.DataBind();
        //            con.Close();
        //        }
        //    }
        //}

        #endregion

        #region Page Load

        protected void Page_Load(object sender, EventArgs e)
        {
            notification("", "");
            if (!IsPostBack)
            {
                if (LoginID != 0 && LoginID != null)
                {

                  

                    try
                    {
                        GetAllDrivers();
                    }
                    catch (Exception ex)
                    {
                        notification("Error", "Error loading page due to: " + ex.Message);
                    }
                }
                else
                {
                    Response.Redirect("Login.aspx");
                    //Response.Redirect("Login.aspx");
                }
            }
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

                if (txtCode.Text.Trim() == string.Empty)
                {
                    notification("Error", "Please enter code");
                    txtCode.Focus();
                }
                else if (txtName.Text.Trim() == string.Empty)
                {
                    notification("Error", "Please enter name");
                    txtName.Focus();
                }
                else if (txtNICNo.Text.Trim() == string.Empty)
                {
                    notification("Error", "Please enter nic no");
                    txtNICNo.Focus();
                }
                else
                {
                    string Code = txtCode.Text;
                    string name = txtName.Text;

                    DriversDML dr = new DriversDML();
                    DataTable dt = dr.GetDrivers(Code, name);
                    if (hfEditID.Value.ToString() == string.Empty)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            notification("Error", "Another Driver with same name or code exists in database, try to change name or code");
                        }
                        else
                        {
                            if (fuDriverImage.HasFile)
                            {
                                Session["FileUpload1"] = fuDriverImage;
                            }

                            if (fuDocument.HasFile)
                            {
                                Session["DocumentUpload"] = fuDocument;
                            }
                            ConfirmModal("Are you sure want to save?", "Save");
                        }
                    }
                    else
                    {
                        if (dt.Rows.Count > 1)
                        {
                            notification("Error", "Another Driver with same name or code exists in database, try to change name or code");
                        }
                        else
                        {
                            if (fuDriverImage.HasFile)
                            {
                                Session["FileUpload1"] = fuDriverImage;
                            }

                            if (fuDocument.HasFile)
                            {
                                Session["DocumentUpload"] = fuDocument;
                            }
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

            GetAllDrivers();
            pnlInput.Visible = false;
        }

        protected void lnkCancelSearch_Click(object sender, EventArgs e)
        {
            try
            {
                GetAllDrivers();
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
                Int64 contID = Convert.ToInt64(gvResult.DataKeys[index]["ID"]);
                hfEditID.Value = contID.ToString();


                DriversDML dml = new DriversDML();
                DataTable dtcont = dml.GetDrivers(contID);
                if (dtcont.Rows.Count > 0)
                {
                    txtCode.Text = dtcont.Rows[0]["Code"].ToString();
                    txtName.Text = dtcont.Rows[0]["Name"].ToString();
                    txtFatherName.Text = dtcont.Rows[0]["FatherName"].ToString();



                    hfdob.Value = dtcont.Rows[0]["DateOfBirth"].ToString();

                    hfNICissue.Value = dtcont.Rows[0]["NICIssueDate"].ToString();
                    hfNICexpiry.Value = dtcont.Rows[0]["NICExpiryDate"].ToString();

                    hfLicenseIssue.Value = dtcont.Rows[0]["LicenseIssueDate"].ToString();
                    hfLicenseExpiry.Value = dtcont.Rows[0]["LicenseExpiryDate"].ToString();







                    txtBloodGroup.Text = dtcont.Rows[0]["BloodGroup"].ToString();
                    txtCellNo.Text = dtcont.Rows[0]["CellNo"].ToString();
                    txtOtherContact.Text = dtcont.Rows[0]["OtherContact"].ToString();
                    txtHomeNo.Text = dtcont.Rows[0]["HomeNo"].ToString();
                    txtAddress.Text = dtcont.Rows[0]["Address"].ToString();
                    txtNICNo.Text = dtcont.Rows[0]["NIC"].ToString();
                    txtIDMark.Text = dtcont.Rows[0]["IdentityMark"].ToString();


                    txtLicenseNo.Text = dtcont.Rows[0]["LicenseNo"].ToString();

                    txtIssuingAuthority.Text = dtcont.Rows[0]["LicenseIssuingAuthority"].ToString();
                    txtEmerContactName.Text = dtcont.Rows[0]["EmergencyContactName"].ToString();
                    txtEmerContactNo.Text = dtcont.Rows[0]["EmergencyContactNo"].ToString();
                    txtEmerContactRelation.Text = dtcont.Rows[0]["ContactRelation"].ToString();





                    txtDescription.Text = dtcont.Rows[0]["Description"].ToString();







                    ddlGender.ClearSelection();
                    ddlGender.Items.FindByValue(dtcont.Rows[0]["Gender"].ToString()).Selected = true;

                    ddlLicenseCategory.ClearSelection();
                    ddlLicenseCategory.Items.FindByValue(dtcont.Rows[0]["LicenseCategory"].ToString()).Selected = true;

                    ddlLicenseType.ClearSelection();
                    ddlLicenseType.Items.FindByValue(dtcont.Rows[0]["LicenseStatus"].ToString()).Selected = true;

                    ddlType.ClearSelection();
                    ddlType.Items.FindByValue(dtcont.Rows[0]["Type"].ToString()).Selected = true;


                    lnkDelete.Visible = true;
                    GetAllDrivers();
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
                Int64 DriverID = Convert.ToInt64(gvResult.DataKeys[index]["ID"]);



                DriversDML dml = new DriversDML();
                DataTable dtcont = dml.GetDrivers(DriverID);
                DataTable dtImages = dml.GetDriverImages(DriverID);
                if (dtcont.Rows.Count > 0)
                {
                    lblCodemodal.Text = dtcont.Rows[0]["Code"].ToString();
                    lblNameModal.Text = dtcont.Rows[0]["Name"].ToString();
                    lblFather.Text = dtcont.Rows[0]["FatherName"].ToString();





                    lblNICissue.Text = dtcont.Rows[0]["NICIssueDate"].ToString();
                    lblNICExpiry.Text = dtcont.Rows[0]["NICExpiryDate"].ToString();

                    lblLicenseIssue.Text = dtcont.Rows[0]["LicenseIssueDate"].ToString();
                    lblLicensexpiryDate.Text = dtcont.Rows[0]["LicenseExpiryDate"].ToString();

                    lblDOB.Text = dtcont.Rows[0]["DateOfBirth"].ToString();





                    lblBlood.Text = dtcont.Rows[0]["BloodGroup"].ToString();
                    lblCell.Text = dtcont.Rows[0]["CellNo"].ToString();
                    lblOther.Text = dtcont.Rows[0]["OtherContact"].ToString();
                    lblHome.Text = dtcont.Rows[0]["HomeNo"].ToString();
                    lblAddress.Text = dtcont.Rows[0]["Address"].ToString();
                    lblNIC.Text = dtcont.Rows[0]["NIC"].ToString();
                    lblIdentity.Text = dtcont.Rows[0]["IdentityMark"].ToString();


                    lblLicenseNo.Text = dtcont.Rows[0]["LicenseNo"].ToString();

                    lblIssueAutho.Text = dtcont.Rows[0]["LicenseIssuingAuthority"].ToString();
                    lblEmerConatctName.Text = dtcont.Rows[0]["EmergencyContactName"].ToString();
                    lblEmerContactNo.Text = dtcont.Rows[0]["EmergencyContactNo"].ToString();
                    lblEmerContactRelation.Text = dtcont.Rows[0]["ContactRelation"].ToString();





                    lblDescription.Text = dtcont.Rows[0]["Description"].ToString();








                    lblGender.Text = dtcont.Rows[0]["Gender"].ToString();


                    lblLicenseCat.Text = dtcont.Rows[0]["LicenseCategory"].ToString();


                    lbllicenceStatus.Text = dtcont.Rows[0]["LicenseStatus"].ToString();


                    lblTypeModal.Text = dtcont.Rows[0]["Type"].ToString();


                }

                if (dtImages.Rows.Count > 0)
                {
                    gvImages.DataSource = dtImages;
                }
                else
                {
                    gvImages.DataSource = null;
                }
                gvImages.DataBind();
            }
            else if (e.CommandName == "Active")
            {

                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow gvr = gvResult.Rows[index];
                Int64 CatId = Convert.ToInt64(gvResult.DataKeys[index]["ID"]);
                string Active = gvResult.DataKeys[index]["Active"].ToString() == string.Empty ? "False" : gvResult.DataKeys[index]["Active"].ToString();
                DriversDML dml = new DriversDML();

                hfEditID.Value = CatId.ToString();

                if (Active == "True")
                {
                    dml.DeactivateDriver(CatId, LoginID);
                }
                else
                {
                    dml.ActivateDriver(CatId, LoginID);
                }
                GetAllDrivers();
                ClearFields();

            }
            else if (e.CommandName == "DocDownload")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvResult.Rows[index];
                LinkButton lnkDownload = row.Cells[7].Controls[0] as LinkButton;
                string fName = lnkDownload.Text;
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + fName);
                Response.TransmitFile(Server.MapPath("assets/Document/" + fName));
                Response.End();
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
            DriversDML dm = new DriversDML();
            DataTable dt = dm.GetDrivers(keyword);
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

        protected void lnkConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                string Action = hfConfirmAction.Value;
                if (Action == "Delete")
                {
                    try
                    {
                        Int64 ID = Convert.ToInt64(hfEditID.Value);
                        if (ID > 0)
                        {
                            DriversDML dml = new DriversDML();
                            dml.DeleteDrivers(ID);
                            notification("Success", "Driver deleted successfully");

                            ClearFields();
                            GetAllDrivers();
                            pnlInput.Visible = false;
                        }
                        else
                        {
                            notification("Error", "No Driver selected to delete");
                        }
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
                        string name = txtName.Text;
                        string fname = txtFatherName.Text;
                        string type = ddlType.SelectedValue;

                        string gender = ddlGender.SelectedValue;
                        string blgroup = txtBloodGroup.Text;
                        Int64 cellno = txtCellNo.Text == string.Empty ? 0 : Convert.ToInt64(txtCellNo.Text);
                        Int64 othercontact = txtOtherContact.Text == string.Empty ? 0 : Convert.ToInt64(txtOtherContact.Text);
                        Int64 homeno = txtHomeNo.Text == string.Empty ? 0 : Convert.ToInt64(txtHomeNo.Text);
                        string address = txtAddress.Text;
                        Int64 NIC = txtNICNo.Text == string.Empty ? 0 : Convert.ToInt64(txtNICNo.Text);
                        string idmark = txtIDMark.Text;


                        Int64 licenseno = txtLicenseNo.Text == string.Empty ? 0 : Convert.ToInt64(txtLicenseNo.Text);
                        string licenceCategory = ddlLicenseCategory.SelectedValue;


                        string authority = txtIssuingAuthority.Text;
                        string status = ddlLicenseType.SelectedValue;
                        string emgcontactname = txtEmerContactName.Text;
                        Int64 emgcontactno = txtEmerContactNo.Text == string.Empty ? 0 : Convert.ToInt64(txtEmerContactNo.Text);
                        string contactRelation = txtEmerContactRelation.Text;
                        string descrip = txtDescription.Text;

                        DriversDML dr = new DriversDML();
                        string DOB = txtDateOfBirth.Text;
                        string NICissue = txtNICIssueDate.Text;
                        string NICexpiry = txtNICExpiryDate.Text;
                        string LicIssue = txtLicenseIssueDate.Text;
                        string licExpiry = txtLicenseExpiryDate.Text;


                        FileUpload fuDoc = (FileUpload)Session["DocumentUpload"];
                        string Document = fuDoc != null ? fuDoc.FileName : string.Empty;
                        string extension = fuDoc != null ? Path.GetExtension(fuDoc.PostedFile.FileName) : string.Empty;
                        if (extension.ToLower() != ".doc" || extension.ToLower() != ".docx" || extension.ToLower() != ".xls" || extension.ToLower() != ".xlsx" || extension.ToLower() != ".txt" || extension.ToLower() != ".pdf" || extension.ToLower() != ".jpg" || extension.ToLower() != ".jpeg")
                        {
                            Document = string.Empty;
                        }

                        Int64 DriverID = dr.InsertDrivers(Code, name, fname, type, DOB, gender, blgroup, cellno, othercontact, homeno, address, NIC, idmark, NICissue, NICexpiry, licenseno, licenceCategory, LicIssue, licExpiry, authority, status, emgcontactname, emgcontactno, contactRelation, descrip, LoginID, Document);
                        if (DriverID > 0)
                        {
                            try
                            {
                                FileUpload fuTempImage = (FileUpload)Session["FileUpload1"];
                                if (fuTempImage != null)
                                {
                                    try
                                    {
                                        foreach (HttpPostedFile postedFile in fuTempImage.PostedFiles)
                                        {
                                            string filename = Path.GetFileName(postedFile.FileName);
                                            string contentType = postedFile.ContentType;
                                            using (Stream fs = postedFile.InputStream)
                                            {
                                                using (BinaryReader br = new BinaryReader(fs))
                                                {

                                                    byte[] bytes = br.ReadBytes((Int32)fs.Length);

                                                    DriversDML dml = new DriversDML();
                                                    DataTable dtDriverImage = dml.GetDriverImages(DriverID);
                                                    if (dtDriverImage.Rows.Count > 0)
                                                    {
                                                        dml.UpdateDriverImage(DriverID, filename, contentType, bytes);
                                                    }
                                                    else
                                                    {
                                                        string constr = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
                                                        using (SqlConnection con = new SqlConnection(constr))
                                                        {
                                                            string query = "insert into DriverImage values (@DriverID, @Name, @ContentType, @Data)";
                                                            using (SqlCommand cmd = new SqlCommand(query))
                                                            {
                                                                cmd.Connection = con;
                                                                cmd.Parameters.AddWithValue("@DriverID", DriverID);
                                                                cmd.Parameters.AddWithValue("@Name", filename);
                                                                cmd.Parameters.AddWithValue("@ContentType", contentType);
                                                                cmd.Parameters.AddWithValue("@Data", bytes);
                                                                con.Open();
                                                                cmd.ExecuteNonQuery();
                                                                con.Close();
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        Response.Redirect(Request.Url.AbsoluteUri);
                                    }
                                    catch (Exception ex)
                                    {
                                        notification("Error", "Driver inserted but Error occured uploading images, due to: " + ex.Message);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                notification("Error", "Driver inserted but Error occured uploading images");
                            }

                            try
                            {
                                if (fuDoc != null)
                                {

                                    if (extension.ToLower() == ".doc" || extension.ToLower() == ".docx" || extension.ToLower() == ".xls" || extension.ToLower() == ".xlsx" || extension.ToLower() == ".txt" || extension.ToLower() == ".pdf" || extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg")
                                    {
                                        string fileName = Path.GetFileName(fuDoc.PostedFile.FileName);
                                        fuDoc.PostedFile.SaveAs(Server.MapPath("assets/Document/") + fileName);
                                        Response.Redirect(Request.Url.AbsoluteUri);
                                    }
                                    else
                                    {
                                        notification("Error", "This file cannot be uploaded for driver document.");
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                notification("Error", "Driver inserted but Error occured while uploading document");
                            }
                            pnlInput.Visible = false;
                            notification("Success", "Submited Successfully");
                            ClearFields();
                            GetAllDrivers();
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
                        string Code = txtCode.Text;
                        string name = txtName.Text;
                        string fname = txtFatherName.Text;
                        string type = ddlType.SelectedValue;

                        string gender = ddlGender.SelectedValue;
                        string blgroup = txtBloodGroup.Text;
                        Int64 cellno = txtCellNo.Text == string.Empty ? 0 : Convert.ToInt64(txtCellNo.Text);
                        Int64 othercontact = txtOtherContact.Text == string.Empty ? 0 : Convert.ToInt64(txtOtherContact.Text);
                        Int64 homeno = txtHomeNo.Text == string.Empty ? 0 : Convert.ToInt64(txtHomeNo.Text);
                        string address = txtAddress.Text;
                        Int64 NIC = txtNICNo.Text == string.Empty ? 0 : Convert.ToInt64(txtNICNo.Text);
                        string idmark = txtIDMark.Text;


                        Int64 licenseno = txtLicenseNo.Text == string.Empty ? 0 : Convert.ToInt64(txtLicenseNo.Text);
                        string licenceCategory = ddlLicenseCategory.SelectedValue;


                        string authority = txtIssuingAuthority.Text;
                        string status = ddlLicenseType.SelectedValue;
                        string emgcontactname = txtEmerContactName.Text;
                        Int64 emgcontactno = txtEmerContactNo.Text == string.Empty ? 0 : Convert.ToInt64(txtEmerContactNo.Text);
                        string contactRelation = txtEmerContactRelation.Text;
                        string descrip = txtDescription.Text;


                        DriversDML dr = new DriversDML();



                        string DOB = string.Empty;
                        if (txtDateOfBirth.Text == string.Empty)
                        {
                            DOB = hfdob.Value;
                        }
                        else
                        {
                            DOB = txtDateOfBirth.Text;
                        }

                        string NICissue = string.Empty;
                        if (txtNICIssueDate.Text == string.Empty)
                        {
                            NICissue = hfNICissue.Value;
                        }
                        else
                        {
                            NICissue = txtNICIssueDate.Text;
                        }

                        string NICexpiry = string.Empty;
                        if (txtNICExpiryDate.Text == string.Empty)
                        {
                            NICexpiry = hfNICexpiry.Value;
                        }
                        else
                        {
                            NICexpiry = txtNICExpiryDate.Text;
                        }


                        string LicIssue = string.Empty;
                        if (txtLicenseIssueDate.Text == string.Empty)
                        {
                            LicIssue = hfLicenseIssue.Value;
                        }
                        else
                        {
                            LicIssue = txtLicenseIssueDate.Text;
                        }



                        string licExpiry = string.Empty;
                        if (txtLicenseExpiryDate.Text == string.Empty)
                        {
                            licExpiry = hfLicenseExpiry.Value;
                        }
                        else
                        {
                            licExpiry = txtLicenseExpiryDate.Text;
                        }


                        FileUpload fuTempImage = (FileUpload)Session["FileUpload1"];
                        FileUpload fuDoc = (FileUpload)Session["DocumentUpload"];

                        string Document = fuDoc != null ? fuDoc.FileName : string.Empty;
                        string extension = fuDoc != null ? Path.GetExtension(fuDoc.PostedFile.FileName) : string.Empty;
                        if (extension.ToLower() != ".doc" || extension.ToLower() != ".docx" || extension.ToLower() != ".xls" || extension.ToLower() != ".xlsx" || extension.ToLower() != ".txt" || extension.ToLower() != ".pdf" || extension.ToLower() != ".jpg" || extension.ToLower() != ".jpeg")
                        {
                            Document = string.Empty;
                        }

                        Int64 id = Convert.ToInt64(hfEditID.Value);
                        dr.UpdateDrivers(id, Code, name, fname, type, DOB, gender, blgroup, cellno, othercontact, homeno, address, NIC, idmark, NICissue, NICexpiry, licenseno, licenceCategory, LicIssue, licExpiry, authority, status, emgcontactname, emgcontactno, contactRelation, descrip, LoginID, Document);
                        pnlInput.Visible = false;
                        notification("Success", "Updated Successfully");

                        
                        if (fuTempImage != null)
                        {
                            try
                            {
                                foreach (HttpPostedFile postedFile in fuTempImage.PostedFiles)
                                {
                                    string filename = Path.GetFileName(postedFile.FileName);
                                    string contentType = postedFile.ContentType;
                                    using (Stream fs = postedFile.InputStream)
                                    {
                                        using (BinaryReader br = new BinaryReader(fs))
                                        {
                                            
                                            byte[] bytes = br.ReadBytes((Int32)fs.Length);

                                            DriversDML dml = new DriversDML();
                                            DataTable dtDriverImage = dml.GetDriverImages(id);
                                            if (dtDriverImage.Rows.Count > 0)
                                            {
                                                dml.UpdateDriverImage(id, filename, contentType, bytes);
                                            }
                                            else
                                            {
                                                string constr = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
                                                using (SqlConnection con = new SqlConnection(constr))
                                                {
                                                    string query = "insert into DriverImage values (@DriverID, @Name, @ContentType, @Data)";
                                                    //string query = "insert into tblFiles values (@Name, @ContentType, @Data)";
                                                    using (SqlCommand cmd = new SqlCommand(query))
                                                    {
                                                        cmd.Connection = con;
                                                        cmd.Parameters.AddWithValue("@DriverID", id);
                                                        cmd.Parameters.AddWithValue("@Name", filename);
                                                        cmd.Parameters.AddWithValue("@ContentType", contentType);
                                                        cmd.Parameters.AddWithValue("@Data", bytes);
                                                        con.Open();
                                                        cmd.ExecuteNonQuery();
                                                        con.Close();
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                Response.Redirect(Request.Url.AbsoluteUri);
                            }
                            catch (Exception ex)
                            {
                                notification("Error", "Driver inserted but Error occured uploading images");
                            }
                        }

                        
                        if (fuDoc != null)
                        {
                            
                            if (extension.ToLower() == ".doc" || extension.ToLower() == ".docx" || extension.ToLower() == ".xls" || extension.ToLower() == ".xlsx" || extension.ToLower() == ".txt" || extension.ToLower() == ".pdf" || extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg")
                            {
                                string fileName = Path.GetFileName(fuDoc.PostedFile.FileName);
                                fuDoc.PostedFile.SaveAs(Server.MapPath("assets/Document/") + fileName);
                                Response.Redirect(Request.Url.AbsoluteUri);
                            }
                            else
                            {
                                notification("Error", "This file cannot be uploaded for driver document.");
                            }
                        }

                        ClearFields();
                        GetAllDrivers();
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

        protected void gvResult_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                DataRowView rowView = (DataRowView)e.Row.DataItem;
                bool Active = Convert.ToBoolean(rowView["Active"].ToString() == string.Empty ? "false" : rowView["Active"]);
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

                DataRowView dr = (DataRowView)e.Row.DataItem;
                if (dr["Data"].ToString() != string.Empty)
                {
                    string imageUrl = "data:image/jpg;base64," + Convert.ToBase64String((byte[])dr["Data"]);
                    (e.Row.FindControl("Image1") as System.Web.UI.WebControls.Image).ImageUrl = imageUrl;
                }
            }
        }

        protected void gvImages_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = (DataRowView)e.Row.DataItem;
                string imageUrl = "data:image/jpg;base64," + Convert.ToBase64String((byte[])dr["Data"]);
                (e.Row.FindControl("Image1") as System.Web.UI.WebControls.Image).ImageUrl = imageUrl;
            }
        }

        #endregion
    }
}