using SqlDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
   public class TripDML
    {
        public DataTable GetTrip()
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = @"SELECT 

                                                    t.ID, 
	                                                t.TripStart, 
	                                                t.TripEnd, 
	                                                c.CompanyName, 
													v.RegNo,
	                                                 
	                                                t.Pickup, 
	                                                t.Dropoff, 
	                                                t.Freight,
                                                    t.IsActive, 
	                                                SUM(ISNULL(te.Amount, 0)) AS Expenses,
	                                                (t.Freight - SUM(ISNULL(te.Amount, 0))) AS PnL
                                                FROM Trips t
                                                    LEFT JOIN TripExpenses te ON t.ID = te.TripID
                                                    INNER JOIN Company c ON c.CompanyID = t.CompanyID
													INNER JOIN Vehicle v ON v.VehicleID=t.VehicleRegNo
                                                    where ISNULL(t.IsDeleted,0)=0
                                                GROUP BY

                                                    t.ID, 
	                                                t.TripStart, 
	                                                t.TripEnd, 
	                                                c.CompanyName,
													v.RegNo,
	                                                t.Pickup, 
	                                                t.Dropoff, 
	                                                 
	                                                t.Freight,
                                                    t.IsActive";

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
        public DataTable GetTrip(Int64 ID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT t.ID, t.TripStart, t.TripEnd, c.CompanyName,v.RegNo,t.Pickup,t.Dropoff,t.Freight,t.IsActive, SUM(ISNULL(te.Amount, 0)) AS Expenses,(t.Freight - SUM(ISNULL(te.Amount, 0))) AS PnL  FROM Trips t  LEFT JOIN TripExpenses te ON t.ID = te.TripID INNER JOIN Company c ON c.CompanyID = t.CompanyID INNER JOIN Vehicle v ON v.VehicleID=t.VehicleRegNo where t.ID= '"+ ID +"' GROUP BY t.ID, t.TripStart, t.TripEnd,c.CompanyName,v.RegNo,t.Pickup, t.Dropoff, t.Freight,t.IsActive";

                _commnadData.AddParameter("@PickDropID", ID);

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
        public DataTable GetTrip(string Keyword)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = @"SELECT t.ID, t.TripStart, t.TripEnd, c.CompanyName,v.RegNo,t.Pickup,t.Dropoff,t.Freight,t.IsActive, SUM(ISNULL(te.Amount, 0)) AS Expenses,(t.Freight - SUM(ISNULL(te.Amount, 0))) AS PnL FROM Trips t LEFT JOIN TripExpenses te ON t.ID = te.TripID INNER JOIN Company c ON c.CompanyID = t.CompanyID INNER JOIN Vehicle v ON v.VehicleID=t.VehicleRegNo where ISNULL(t.IsDeleted,0)=0 And(c.CompanyName like '%" + Keyword + "%' or t.TripStart like '%" + Keyword + "%' or t.TripEnd like '%" + Keyword + "%' or t.Pickup like '%" + Keyword + "%' or t.Dropoff like '%" + Keyword + "%' or v.RegNo like '%" + Keyword + "%' or t.Freight like '%" + Keyword + "%')  GROUP BY t.ID, t.TripStart, t.TripEnd,c.CompanyName,v.RegNo,t.Pickup, t.Dropoff, t.Freight,t.IsActive";

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

        public int DeleteTrip(Int64 ID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "update Trips set IsDeleted=1 where ID =  " + ID;

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
        public int InsertTrip(Int64 CompanyID, string TripStart, string TripEnd,string Pickup,string Dropoff,Int64 VehicleReg, Int64 Freight,Int64 CreatedBy)
        {
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "INSERT INTO Trips ([CompanyID], [TripStart], [TripEnd], [Pickup], [Dropoff], [VehicleRegNo], [Freight],[IsActive],[CreatedBy] ,[CreatedDate]) VALUES ('" + CompanyID + "', '" + TripStart + "', '" + TripEnd + "', '" + Pickup + "', '" + Dropoff + "', '" + VehicleReg + "' , '" + Freight + "','True','" + CreatedBy + "', getdate())";



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
        public int UpdateTrip(Int64 ID, Int64 CompanyID, string TripStart, string TripEnd, string Pickup, string Dropoff, Int64 VehicleReg, Int64 Freight, Int64 ModifiedBy)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE Trips SET [CompanyID] = '" + CompanyID + "', [TripStart] = '" + TripStart + "', [TripEnd] = '" + TripEnd + "', [Pickup] = '" + Pickup + "', [Dropoff] = '" + Dropoff + "', [VehicleRegNo] = '" + VehicleReg + "' , [Freight] = '" + Freight + "' ,  [ModifiedBy] = '" + ModifiedBy + "', [ModifiedDate] = getdate() WHERE [ID] = " + ID;


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


        public int ActivateTrip(Int64 ID, Int64 ModifiedBy)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE Trips SET [IsActive] = 'True' ,  [ModifiedBy] = '" + ModifiedBy + "', [ModifiedDate] = getdate() WHERE [ID] = " + ID;


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

        public int DeactivateTrip(Int64 ID, Int64 ModifiedBy)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE Trips SET [IsActive] = 'False' ,  [ModifiedBy] = '" + ModifiedBy + "', [ModifiedDate] = getdate() WHERE [ID] = " + ID;


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

        public int InsertExpenses(Int64 TripID, Int64 ExpenseTypeID, Int64 Amount, string Remarks, Int64 CreatedBy)
        {
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "INSERT INTO TripExpenses ([TripID], [ExpenseTypeID],[Amount],[Remarks],IsActive,  [CreatedBy] ,[CreatedDate]) VALUES ('" + TripID + "',  '" + ExpenseTypeID + "','" + Amount + "','" + Remarks + "','True', '" + CreatedBy + "', getdate());";



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

        public DataTable GetExpenses(Int64 TripID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "select te.*,et.ExpensesTypeName from TripExpenses te INNER JOIN Trips t ON t.ID=te.TripID INNER JOIN ExpensesType et ON et.ExpensesTypeID=te.ExpenseTypeID where ISNULL(te.IsDeleted,0)= 0 and t.ID = " + TripID;

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

        public DataTable GetSearchVehicle()
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = @"SELECT 
	                                            v.RegNo, 
	                                            SUM(t.Freight) TotalFreight, 
	                                            (SELECT SUM(Amount) FROM TripExpenses te WHERE te.TripID IN (SELECT ID FROM Trips t INNER JOIN Vehicle iv ON iv.VehicleID = t.VehicleRegNo WHERE iv.RegNo = v.RegNo)) TripExpenses, 
	                                            (SELECT SUM(t.Freight) - SUM(te.Amount)  FROM TripExpenses te WHERE te.TripID IN (SELECT ID FROM Trips t INNER JOIN Vehicle iv ON iv.VehicleID = t.VehicleRegNo WHERE iv.RegNo = v.RegNo)) TripPNL, 
	                                            (SELECT SUM(Amount) FROM TripVehicleExpenses WHERE t.VehicleRegNo = VehicleID) VehicleExpenses,
	                                            ((SELECT SUM(t.Freight) - SUM(te.Amount)  FROM TripExpenses te WHERE te.TripID IN (SELECT ID FROM Trips t INNER JOIN Vehicle iv ON iv.VehicleID = t.VehicleRegNo WHERE iv.RegNo = v.RegNo))-(SELECT SUM(Amount) FROM TripVehicleExpenses WHERE t.VehicleRegNo = VehicleID)) OverallPNL
	
                                            FROM Trips t 
	                                            INNER JOIN Vehicle v ON v.VehicleID = t.VehicleRegNo 
                                            GROUP BY v.RegNo, t.VehicleRegNo";

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

        public DataTable SearchVehicle(string TripStart, string TripEnd, string Conditions)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = @"SELECT v.RegNo, 
	                                        SUM(t.Freight) TotalFreight, 
	                                        (SELECT SUM(Amount) FROM TripExpenses te WHERE te.TripID IN(SELECT ID FROM Trips t INNER JOIN Vehicle iv ON iv.VehicleID = t.VehicleRegNo WHERE iv.RegNo = v.RegNo)) TripExpenses, 
	                                        (SELECT SUM(t.Freight) - SUM(te.Amount)  FROM TripExpenses te WHERE te.TripID IN(SELECT ID FROM Trips t INNER JOIN Vehicle iv ON iv.VehicleID = t.VehicleRegNo WHERE iv.RegNo = v.RegNo)) TripPNL, 
	                                        (SELECT SUM(Amount) FROM TripVehicleExpenses WHERE t.VehicleRegNo = VehicleID) VehicleExpenses,
	                                       ((SELECT SUM(t.Freight) - SUM(te.Amount)  FROM TripExpenses te WHERE te.TripID IN(SELECT ID FROM Trips t INNER JOIN Vehicle iv ON iv.VehicleID = t.VehicleRegNo WHERE iv.RegNo = v.RegNo))-(SELECT SUM(Amount) FROM TripVehicleExpenses WHERE t.VehicleRegNo = VehicleID)) OverallPNL

                                        FROM Trips t

                                            INNER JOIN Vehicle v ON v.VehicleID = t.VehicleRegNo

                                            where (t.TripStart >= '" + TripStart + "' and t.TripEnd <= '" + TripEnd + "') " + Conditions + " GROUP BY v.RegNo, t.VehicleRegNo";


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
        public DataTable SearchTrip(string TripStart, string TripEnd, string Condition)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = @"SELECT 

                                                    t.ID, 
	                                                t.TripStart, 
	                                                t.TripEnd, 
	                                                c.CompanyName, 
													v.RegNo,
	                                                 
	                                                t.Pickup, 
	                                                t.Dropoff, 
	                                                t.Freight,
                                                    t.IsActive, 
	                                                SUM(ISNULL(te.Amount, 0)) AS Expenses,
	                                                (t.Freight - SUM(ISNULL(te.Amount, 0))) AS PnL
                                                FROM Trips t
                                                    LEFT JOIN TripExpenses te ON t.ID = te.TripID
                                                    INNER JOIN Company c ON c.CompanyID = t.CompanyID
													INNER JOIN Vehicle v ON v.VehicleID=t.VehicleRegNo
                                                    where ISNULL(t.IsDeleted,0)=0 And (t.TripStart >= '" + TripStart + "' and t.TripEnd <= '" + TripEnd + "') " + Condition + " GROUP BY t.ID,t.TripStart, t.TripEnd, c.CompanyName,v.RegNo,t.Dropoff,t.IsActive,t.Pickup,t.Freight";


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
