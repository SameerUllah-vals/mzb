using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiltySystem.Error_Pages
{
    public partial class _404 : System.Web.UI.Page
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (LoginID > 0 && LoginID != null)
                {
                    //LeftSideBar.InnerHtml = string.Empty;

                    try
                    {
                        UsersDML dmlUsers = new UsersDML();
                        DataTable dtUsers = dmlUsers.GetUsers(LoginID);
                        if (dtUsers.Rows.Count > 0)
                        {
                            //lblLeftuserName.Text = dtUsers.Rows[0]["UserName"].ToString();
                            lblTopRightUserName.Text = dtUsers.Rows[0]["UserName"].ToString();
                        }
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }
                    lblBackHomeText.Text = "Back to Home";

                    //sideBar.InnerText = LeftMenus;
                }
                else
                {
                    lblBackHomeText.Text = "Back to Login";
                }
            }
        }

        protected void lnkBackHome_Click(object sender, EventArgs e)
        {
            try
            {
                if (LoginID > 0 && LoginID != null)
                {
                    Response.Redirect("../Dashboard.aspx?lid=" + LoginID);
                }
                else
                {
                    Response.Redirect("../Login.aspx");
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}