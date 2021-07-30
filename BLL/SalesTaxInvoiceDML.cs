using SqlDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class SalesTaxInvoiceDML
    {
        public DataTable GetTaxType(string TypeName)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT * FROM TaxTypes WHERE Name = '" + TypeName + "'"; ;

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

        public DataTable GetSalesTaxInvoice(Int64 STInvoiceID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT sti.*, o.*, ocn.EmptyContainerPickLocation, ocn.EmptyContainerDropLocation, ov.VehicleRegNo, c.CompanyName AS Buyer, c.NTN AS BuyerNTN, c.Contact AS BuyerPhone, c.Address AS BuyerAddress, ocn.Rate, oc.CompanyName AS Supplier, oc.NTN AS SupplierNTN, oc.Address AS SupplierAddress, oc.Contact AS SupplierPhone FROM SalesTaxInvoice sti INNER JOIN[Order] o ON o.OrderID = sti.OrderID INNER JOIN Company c ON c.CompanyID = sti.BuyerID INNER JOIN OwnCompany oc ON oc.CompanyID = sti.SupplierID INNER JOIN OrderConsignment ocn ON ocn.OrderID = o.OrderID INNER JOIN OrderVehicle ov ON ov.OrderID = o.OrderID WHERE sti.SalesTaxInvoiceID = " + STInvoiceID;

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

        public DataTable GetSalesTaxInvoiceByOrder(Int64 OrderID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT * FROM SalesTaxInvoice WHERE OrderID = " + OrderID;

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

        public int InsertSalesTaxInvoice(Int64 OrderID, Int64 SupplierID, Int64 BuyerID, double ActualAmount, double AppliedTaxPercentage, double AppliedTaxAmount, double ValueInclusiveTax, string InvoiceDate, Int64 CreatedBy)
        {
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "INSERT INTO SalesTaxInvoice (OrderID, SupplierID, BuyerID, ActualAmount, AppliedTaxPercentage, AppliedTaxAmount, ValueInclusiveTax, InvoiceDate, CreatedBy, CreatedDate) VALUES (" + OrderID + ", " + SupplierID + ", " + BuyerID + ", " + ActualAmount + ", " + AppliedTaxPercentage + ", " + AppliedTaxAmount + ", " + ValueInclusiveTax + ", '" + InvoiceDate + "', " + CreatedBy + ", GETDATE()); SELECT SCOPE_IDENTITY();";



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
    }
}
