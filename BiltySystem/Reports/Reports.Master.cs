using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiltySystem.Reports
{
    public partial class Reports : System.Web.UI.MasterPage
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
                    LeftSideBar.InnerHtml = string.Empty;

                    try
                    {
                        UsersDML dmlUsers = new UsersDML();
                        DataTable dtUsers = dmlUsers.GetUserForHome(LoginID);
                        if (dtUsers.Rows.Count > 0)
                        {
                            lblLeftuserName.Text = dtUsers.Rows[0]["UserName"].ToString();
                            lblTopRightUserName.Text = dtUsers.Rows[0]["UserName"].ToString();
                            lblUserDesignation.Text = dtUsers.Rows[0]["DesignationName"].ToString();

                            NaviDML dml = new NaviDML();
                            DataTable dtMainMenus = dml.GetMainMenus();

                            if (dtMainMenus.Rows.Count > 0)
                            {
                                for (int i = 0; i < dtMainMenus.Rows.Count; i++)
                                {
                                    Int64 MenuID = Convert.ToInt64(dtMainMenus.Rows[i]["MenuID"]);
                                    string MenuIcon = dtMainMenus.Rows[i]["Icon"].ToString();

                                    DataTable dtSubMenues = dml.GetSubMenus(MenuID);
                                    if (dtSubMenues.Rows.Count > 0)
                                    {

                                        LeftSideBar.InnerHtml += "<li class=\"\">";
                                        LeftSideBar.InnerHtml += "<a href = \"javascript:;\">";

                                        LeftSideBar.InnerHtml += "<i class=\"" + MenuIcon + "\"></i>";
                                        LeftSideBar.InnerHtml += "<span class=\"title\">" + dtMainMenus.Rows[i]["MenuName"].ToString() + "</span>";
                                        LeftSideBar.InnerHtml += "<span class=\"arrow\"></span>";
                                        LeftSideBar.InnerHtml += "</a>";
                                        LeftSideBar.InnerHtml += "<ul class=\"sub-menu\">";

                                        for (int j = 0; j < dtSubMenues.Rows.Count; j++)
                                        {
                                            string SubmenuIcon = dtSubMenues.Rows[j]["icon"].ToString();
                                            if (dtSubMenues.Rows[j]["FormName"].ToString() == "User Account" || dtSubMenues.Rows[j]["FormName"].ToString() == "Navigation Menu")
                                            {
                                                if (dtUsers.Rows[0]["UserName"].ToString() == "SuperAdmin")
                                                {
                                                    LeftSideBar.InnerHtml += "<li>";
                                                    LeftSideBar.InnerHtml += "<a class=\"\" href=\"" + Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath + "/" + dtSubMenues.Rows[j]["url"].ToString() + LoginID + "\">" + dtSubMenues.Rows[j]["FormName"].ToString() + "</a>";
                                                    LeftSideBar.InnerHtml += "</li>";
                                                }
                                            }
                                            else
                                            {
                                                LeftSideBar.InnerHtml += "<li>";
                                                LeftSideBar.InnerHtml += "<a class=\"\" href=\"" + Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath + "/" + dtSubMenues.Rows[j]["url"].ToString() + LoginID + "\">" + dtSubMenues.Rows[j]["FormName"].ToString() + "</a>";
                                                LeftSideBar.InnerHtml += "</li>";
                                            }

                                            //LeftSideBar.InnerHtml += "<li>";
                                            //LeftSideBar.InnerHtml += "<a class=\"\" href=\"" + Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath + "/" + dtSubMenues.Rows[j]["url"].ToString() + LoginID + "\">" + dtSubMenues.Rows[j]["FormName"].ToString() + "</a>";
                                            //LeftSideBar.InnerHtml += "</li>";
                                        }

                                        LeftSideBar.InnerHtml += " </ul>";
                                        LeftSideBar.InnerHtml += "</li>";
                                    }
                                    else
                                    {
                                        LeftSideBar.InnerHtml += "<li class=\"open\"><a href = \"index.html\"><i class=\"" + MenuIcon + "\"></i><span class=\"title\">" + dtMainMenus.Rows[i]["MenuName"].ToString() + "</span></a></li>";
                                    }
                                }
                            }
                        }
                        else
                        {

                        }
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }


                    //sideBar.InnerText = LeftMenus;
                }
                else
                {
                    Response.Redirect("../Login.aspx");
                }
            }
        }
    }
}