using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ValsProjectManagment
{
    public partial class GroupsJDT : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GroupDML dml = new GroupDML();
            DataTable dtgp = dml.Getgroups();
            if (dtgp.Rows.Count > 0)
            {
                gvCustomers.DataSource = dtgp;
            }
            else
            {
                gvCustomers.DataSource = null;
            }
            gvCustomers.DataBind();
        }
    }
}