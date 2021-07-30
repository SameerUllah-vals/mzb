using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiltySystem
{
    public partial class Login : System.Web.UI.Page
    {
        #region Page Load

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtUserName.Focus();
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

        #region Events

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUserName.Text.Trim() == string.Empty)
                {
                    notification("Error", "Please enter username");
                    txtUserName.Focus();
                }
                else if (txtPassword.Text.Trim() == string.Empty)
                {
                    notification("Error", "Please enter password");
                    txtPassword.Focus();
                }
                else
                {
                    string UserName = txtUserName.Text.Trim();
                    string Password = txtPassword.Text.Trim();
                    UsersDML dml = new UsersDML();
                    DataTable dtUser = dml.GetUsers(UserName, Password);
                    if (dtUser.Rows.Count > 0)
                    {
                        if (dtUser.Rows[0]["Active"].ToString() == "True")
                        {
                            Response.Redirect("Dashboard.aspx?lid=" + dtUser.Rows[0]["UserID"].ToString());
                        }
                        else
                        {
                            notification("Error", "No such active user found");
                            txtUserName.Focus();
                        }                        
                    }
                    else
                    {
                        notification("Error", "UserName or Password incorrect");
                        txtPassword.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                notification("Error", "Error logging in, due to: " + ex.Message);
            }
        }

        #endregion
    }
}