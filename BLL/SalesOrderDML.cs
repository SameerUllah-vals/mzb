using SqlDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
 public class SalesOrderDML
    {
        public Int64 InsertSalesOrder(Int64 CompanyID, string ProjectName, string SalesDate, Int64 ProjectCost, Int64 SalesPrice, Int64 ProjectDiscount, Int64 SalesBy, Int64 ReferedBy, Int64 CreatedBy)
        {
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "insert into SalesOrder (CustomerID,Project,SalesDate,ProjectCost,SalesPrice,ProjectDiscount,SalesByID, ReferedByID,CreatedBy,CreatedDate) values ('" + CompanyID + "', '" + ProjectName + "', '" + SalesDate + "', '" + ProjectCost + "', '" + SalesPrice + "', '" + ProjectDiscount + "', '" + SalesBy + "', '" + ReferedBy + "', '" + CreatedBy + "', getdate()); SELECT SCOPE_IDENTITY();";

                commandData.OpenWithOutTrans();

                Object valReturned = commandData.Execute(ExecutionType.ExecuteScalar);

                return Convert.ToInt32(valReturned);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                commandData.Close();
            }
        }
        public int InsertSalesModule(Int64 SalesOrderID, Int64 ModuleID, double ModuleDiscount, Int64 CreatedByID)
        {
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "insert into SalesModules (SalesOrderID,ModuleID,ModuleDiscount,CreatedByID,CreatedDate) values ('" + SalesOrderID + "', '" + ModuleID + "', '" + ModuleDiscount + "', '" + CreatedByID + "', getdate())";

                commandData.OpenWithOutTrans();

                Object valReturned = commandData.Execute(ExecutionType.ExecuteScalar);

                return Convert.ToInt32(valReturned);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                commandData.Close();
            }
        }
        public DataTable GetAllSalesOrder()
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "select S.*,C.CompanyName from SalesOrder S Inner join Company C ON s.CustomerID = C.CompanyID where ISNULL(s.IsDeleted , 0)=0 order by s.SalesOrderID DESC";
               // _commnadData.CommandText = "SELECT * FROM SalesOrder ORDER BY SalesOrderID DESC";


                //opening connection
                _commnadData.OpenWithOutTrans();

                //Executing Query
                DataSet _ds = _commnadData.Execute(ExecutionType.ExecuteDataSet) as DataSet;

                return _ds.Tables[0];
            }
            catch (Exception ex)
            {
                //Console.WriteLine("No record found");
                throw ex;
            }
            finally
            {
                //Console.WriteLine("No ");
                _commnadData.Close();

            }
        }
    }
}
