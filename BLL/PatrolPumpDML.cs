using SqlDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class PatrolPumpDML
    {
        public DataTable GetActivePatrolPumps(string SortState)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = @"SELECT * FROM PatrolPumps WHERE ISNULL(IsActive, 0) = 1 AND ISNULL(IsDelete, 0) = 0 ORDER BY PatrolPumpID " + SortState;

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

        public DataTable GetPatrolPump(Int64 PatrolPumpID, string SortState)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = @"SELECT * FROM PatrolPumps WHERE ISNULL(IsDelete, 0) = 0 AND PatrolPumpID = " + PatrolPumpID + " ORDER BY PatrolPumpID " + SortState;

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

        public DataTable GetPatrolPumps(string SortState)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT p.*, cu.UserName AS CreatedByName, mu.UserName AS ModifiedByName FROM PatrolPumps p INNER JOIN UserAccounts cu ON cu.UserID = p.CreatedBy LEFT JOIN UserAccounts mu ON mu.UserID = p.ModifiedBy WHERE ISNULL(p.IsDelete, 0) = 0 ORDER BY p.PatrolPumpID " + SortState;

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

        public DataTable GetPatrolPumps(string Keyword, string SortState)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT p.*, (p.Code + '|' + p.Name) AS PatrolPump, cu.UserName AS CreatedByName, mu.UserName AS ModifiedByName FROM PatrolPumps p INNER JOIN UserAccounts cu ON cu.UserID = p.CreatedBy LEFT JOIN UserAccounts mu ON mu.UserID = p.ModifiedBy WHERE ISNULL(p.IsDelete, 0) = 0 AND p.Code LIKE '%" + Keyword + "%' OR p.Name LIKE '%" + Keyword + "%' OR cu.UserName LIKE '%" + Keyword + "%' OR mu.UserName LIKE '%" + Keyword + "%' ORDER BY p.PatrolPumpID " + SortState;

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

        public DataTable GetActivePatrolPumps()
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT *, Name + '|' + Code AS PatrolPumpAccount FROM PatrolPumps WHERE ISNULL(isActive, 0) = 1 AND ISNULL(isDelete, 0) = 0 ORDER BY Name ASC";

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

        public DataTable GetActivePatrolPumpsByName(string Name)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT * FROM PatrolPumps WHERE Name = @Name AND ISNULL(isActive, 0) = 1 AND ISNULL(isDelete, 0) = 0";

                _commnadData.AddParameter("@Name", Name);

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

        public int InsertPatrolPump(string Code, string Name,double PerLiterRate, Int64 CreatedBy)
        {
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "INSERT INTO PatrolPumps (Code, Name,PerLiterRate, CreatedBy, CreatedDate) VALUES ('" + Code +  "', '" + Name + "',"+PerLiterRate+", " + CreatedBy + ", GETDATE()); SELECT SCOPE_IDENTITY();";



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

        public int UpdatePatrolPump(Int64 PatrolPumpID, string Code, string Name,double PerLiterRate, Int64 ModifiedBy)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE PatrolPumps SET Code = '" + Code + "', Name = '" + Name + "',PerLiterRate="+ PerLiterRate + ", ModifiedBy = " + ModifiedBy + ", ModifiedDate = GETDATE() WHERE PatrolPumpID = " + PatrolPumpID;


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

        public int DeletePatrolPump(Int64 PatrolPumpID, Int64 ModifiedBy)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE PatrolPumps SET isDelete = 1, ModifiedBy = " + ModifiedBy + ", ModifiedDate = GETDATE() WHERE PatrolPumpID = " + PatrolPumpID;


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

        public int ActivatePatrolPump(Int64 PatrolPumpID, Int64 ModifiedByID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE PatrolPumps SET isActive = 1, ModifiedBy = " + ModifiedByID + ", ModifiedDate = GETDATE() WHERE PatrolPumpID = " + PatrolPumpID;

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

        public int DeActivatePatrolPump(Int64 PatrolPumpID, Int64 ModifiedByID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE PatrolPumps SET isActive = 0, ModifiedBy = " + ModifiedByID + ", ModifiedDate = GETDATE() WHERE PatrolPumpID = " + PatrolPumpID;

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
