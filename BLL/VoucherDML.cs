using SqlDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
   public  class VoucherDML
    {
        public DataTable GetVehicleExpenses()
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = @"select tve.*,v.RegNo,et.ExpensesTypeName,v.RegNo
                                                from TripVehicleExpenses tve
                                                INNER JOIN Vehicle v ON v.VehicleID=tve.VehicleID
                                                INNER JOIN ExpensesType et ON et.ExpensesTypeID=tve.ExpenseTypeID
                                                Where ISNULL(tve.IsDeleted,0)=0";

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
        public DataTable GetVehicleExpenses(Int64 ID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = @"select tve.*,v.RegNo,et.ExpensesTypeName,v.RegNo
                                                from TripVehicleExpenses tve
                                                INNER JOIN Vehicle v ON v.VehicleID=tve.VehicleID
                                                INNER JOIN ExpensesType et ON et.ExpensesTypeID=tve.ExpenseTypeID
                                                Where ISNULL(tve.IsDeleted,0)=0 and  (tve.ID = '" + ID + "')";

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
        public DataTable GetVehicleExpenses(string Keyword)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = @"select tve.*,v.RegNo,et.ExpensesTypeName,v.RegNo
                                                from TripVehicleExpenses tve
                                                INNER JOIN Vehicle v ON v.VehicleID = tve.VehicleID
                                                INNER JOIN ExpensesType et ON et.ExpensesTypeID = tve.ExpenseTypeID
                                                Where ISNULL(tve.IsDeleted,0)= 0
                                                And(tve.VoucherNo like '%" + Keyword + "%' or tve.VehicleID like '%" + Keyword + "%' or tve.ExpenseTypeID like '%" + Keyword + "%' or tve.Vendor like '%" + Keyword + "%' or tve.[Date] like '%" + Keyword + "%' or tve.Amount like '%" + Keyword + "%' or tve.Remarks like '%" + Keyword + "%') Order By ID desc ";

                _commnadData.AddParameter("@Keyword", Keyword);

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

        public int DeleteVehicleExpense(Int64 ID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "update TripVehicleExpenses set IsDeleted=1 where ID =  " + ID;

                //commandData.AddParameter("@CityID", CityID);
                commandData.AddParameter("@ID", ID);

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
        public int InsertVehicleExpense(string VoucherNo, Int64 VehicleID, Int64 ExpenseTypeID, string Vendor, string Date, double Amount, string Remarks, Int64 CreatedBy)
        {
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "INSERT INTO TripVehicleExpenses ([VoucherNo], [VehicleID], [ExpenseTypeID], [Vendor], [Date], [Amount], [Remarks],[IsActive],[CreatedBy] ,[CreatedDate]) VALUES ((SELECT (CASE WHEN COUNT(*) > 0 THEN COUNT(*) ELSE 0 END) FROM TripVehicleExpenses) + 1, '" + VehicleID + "', '" + ExpenseTypeID + "', '" + Vendor + "', '" + Date + "', " + Amount + " , '" + Remarks + "','True','" + CreatedBy + "', getdate())";



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
        public int UpdateVehicleExpense(Int64 ID, string VoucherNo, Int64 VehicleID, Int64 ExpenseTypeID, string Vendor, string Date, double Amount, string Remarks, Int64 ModifiedBy)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE TripVehicleExpenses SET [VoucherNo] = '" + VoucherNo + "', [VehicleID] = '" + VehicleID + "', [ExpenseTypeID] = '" + ExpenseTypeID + "', [Vendor] = '" + Vendor + "', [Date] = '" + Date + "', [Amount] = " + Amount + " , [Remarks] = '" + Remarks + "' ,  [ModifiedBy] = '" + ModifiedBy + "', [ModifiedDate] = getdate() WHERE [ID] = " + ID;


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
        public int ActivateVehicleExpense(Int64 ID, Int64 ModifiedBy)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE TripVehicleExpenses SET [IsActive] = 'True' ,  [ModifiedBy] = '" + ModifiedBy + "', [ModifiedDate] = getdate() WHERE [ID] = " + ID;


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

        public int DeactivateVehicleExpense(Int64 ID, Int64 ModifiedBy)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE TripVehicleExpenses SET [IsActive] = 'False' ,  [ModifiedBy] = '" + ModifiedBy + "', [ModifiedDate] = getdate() WHERE [ID] = " + ID;


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

        public DataTable GetAutoVoucher()
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = @"select COUNT(VoucherNo) as Voucher from TripVehicleExpenses";

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

        public DataTable SearchVoucher(string StartDate, string EndDate, string Condition)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = @"select tve.*,v.RegNo,et.ExpensesTypeName,v.RegNo
                                                from TripVehicleExpenses tve
                                                INNER JOIN Vehicle v ON v.VehicleID = tve.VehicleID
                                                INNER JOIN ExpensesType et ON et.ExpensesTypeID = tve.ExpenseTypeID
                                                Where ISNULL(tve.IsDeleted,0)= 0
                                                And (tve.Date between '" +StartDate + "' and '"+EndDate+"') "+Condition+ "Order By ID desc";
               


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
