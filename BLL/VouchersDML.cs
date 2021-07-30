using SqlDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class VouchersDML
    {
        public DataTable GetVouchers(string SortState)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                //_commnadData.CommandText = "SELECT v.*, b.Name AS Broker, (bnk.Name + '|' + bnk.AccountNo) as Bank, v.ChequeNo AS ChequeNo FROM Vouchers v INNER JOIN Brokers b ON b.ID = v.BrokerID LEFT JOIN Banks bnk ON bnk.BankID = v.BankID ORDER BY v.VoucherID " + SortState;
                _commnadData.CommandText = "SELECT v.*, ISNULL(pmp.Name, brk.Name) AS Vendor, (bnk.Name + '|' + bnk.AccountNo) as Bank, v.ChequeNo AS ChequeNo, cu.UserName AS CreatedByName, mu.UserName AS ModifiedByName From Vouchers v LEFT JOIN PatrolPumps pmp ON pmp.PatrolPumpID = v.PatrolPumpID LEFT JOIN Brokers brk ON brk.ID = v.BrokerID LEFT JOIN Banks bnk ON bnk.BankID = v.BankID INNER JOIN UserAccounts cu ON cu.UserID = v.CreatedBy LEFT JOIN UserAccounts mu ON mu.UserID = v.ModifiedBy ORDER BY v.VoucherID " + SortState;

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

        public DataTable GetVoucher(Int64 VoucherID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT v.*, ISNULL(pmp.Name, brk.Name) AS Vendor, (bnk.Name + '|' + bnk.AccountNo) as Bank, v.ChequeNo AS ChequeNo, cu.UserName AS CreatedByName, mu.UserName AS ModifiedByName From Vouchers v LEFT JOIN PatrolPumps pmp ON pmp.PatrolPumpID = v.PatrolPumpID LEFT JOIN Brokers brk ON brk.ID = v.BrokerID LEFT JOIN Banks bnk ON bnk.BankID = v.BankID INNER JOIN UserAccounts cu ON cu.UserID = v.CreatedBy LEFT JOIN UserAccounts mu ON mu.UserID = v.ModifiedBy WHERE v.VoucherID = " + VoucherID;

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

        public DataTable GetVoucher(string Filters)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = @"SELECT v.*, ISNULL(pmp.Name, brk.Name) AS Vendor, (bnk.Name + '|' + bnk.AccountNo) as Bank, v.ChequeNo AS ChequeNo, cu.UserName AS CreatedByName, mu.UserName AS ModifiedByName From Vouchers v 
LEFT JOIN PatrolPumps pmp ON pmp.PatrolPumpID = v.PatrolPumpID 
LEFT JOIN Brokers brk ON brk.ID = v.BrokerID 
LEFT JOIN Banks bnk ON bnk.BankID = v.BankID 
INNER JOIN UserAccounts cu ON cu.UserID = v.CreatedBy 
LEFT JOIN UserAccounts mu ON mu.UserID = v.ModifiedBy " +Filters + " ORDER BY v.VoucherID";
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
        public DataTable GetVoucher()
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                //_commnadData.CommandText = @"select * from Vouchers order by CreatedDate Desc";
                _commnadData.CommandText = @"SELECT v.*, ISNULL(pmp.Name, brk.Name) AS Vendor, (bnk.Name + '|' + bnk.AccountNo) as Bank, v.ChequeNo AS ChequeNo, cu.UserName AS CreatedByName, mu.UserName AS ModifiedByName From Vouchers v 
LEFT JOIN PatrolPumps pmp ON pmp.PatrolPumpID = v.PatrolPumpID 
LEFT JOIN Brokers brk ON brk.ID = v.BrokerID 
LEFT JOIN Banks bnk ON bnk.BankID = v.BankID 
INNER JOIN UserAccounts cu ON cu.UserID = v.CreatedBy 
LEFT JOIN UserAccounts mu ON mu.UserID = v.ModifiedBy order by v.CreatedDate Desc";
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

        public int InsertVouchers(string VoucherNo, Int64 BrokerID, Int64 PatrolPumpID, Int64 BankID, string ChequeNo, double Amount, Int64 OrderID, string VehicleRegNo, Int64 CreatedBy)
        {
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;

                commandData.CommandText = "INSERT INTO Vouchers (VoucherNo, BrokerID, PatrolPumpID, BankID, ChequeNo, Amount, OrderID, VehicleRegNo, CreatedBy, CreatedDate) VALUES ('" + VoucherNo + "', " + BrokerID + ", " + PatrolPumpID + ", " + BankID + ", '" + ChequeNo + "', " + Amount + ", " + OrderID + ", '" + VehicleRegNo + "', " + CreatedBy + ", GETDATE());; SELECT SCOPE_IDENTITY();";

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

        public int UpdateVoucherPaid(Int64 VoucherID, string ReceivedBy, Int64 ModifiedBy)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE Vouchers SET isPayed = 1, ReceivedBy = '" + ReceivedBy + "', ModifiedBy = " + ModifiedBy + ", ModifiedDate = GETDATE() WHERE VoucherID = " + VoucherID;


                //opening connection
                commandData.OpenWithOutTrans();

                //Executing Query
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
