using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiltySystem
{
    public partial class BillUpdate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

       

        protected void lnkUpdate_Click(object sender, EventArgs e)
        {
            InvoiceDML dml = new InvoiceDML();
            DataTable dtBilledCustomer = dml.GetBilledCustomer();
            //string code = dt.Rows[0]["Code"].ToString();

            for (int i = 0; i < dtBilledCustomer.Rows.Count; i++)
            {
                string Customer = dtBilledCustomer.Rows[i]["CustomerCompany"].ToString();
                DataTable dtBill = dml.GetTotalBills(Customer);

                for (int j = 0; j < dtBill.Rows.Count; j++)
                {
                    string BillNo = (j + 1).ToString();
                    //Int64 BillID = Convert.ToInt64(dtBill.Rows[j]["BillID"]);
                    double Total = Convert.ToDouble(dtBill.Rows[j]["Total"]);
                    int Seconds = Convert.ToInt32(dtBill.Rows[j]["Seconds"]);
                    DataTable dtTotalBills = dml.GetBills(Customer, Total, Seconds);
                    foreach (DataRow _drBreakup in dtTotalBills.Rows)
                    {
                        Int64 BillID = Convert.ToInt64(_drBreakup["BillID"]);

                        InvoiceDML dmlInvoice = new InvoiceDML();
                        dmlInvoice.UpdateBillNo(BillID, BillNo);
                    }
                }
            }

        }
    }
}