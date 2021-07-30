using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlDataAccess;
using System.Data.SqlClient;
using System.Data;

namespace BLL
{
    public class ModulesDML
    {
        public DataTable GetModule(string ModuleName)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "select * from Modules where (ModuleName=@ModuleName) and ISNULL(IsDeleted,0)=0";

                _commnadData.AddParameter("@ModuleName", ModuleName);

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
        public DataTable GetAllModules()
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT * FROM Modules where IsDeleted=0 ORDER BY ModuleID DESC";

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
        public DataTable GetModule(Int64 ModuleID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT * FROM Modules WHERE ModuleID = @ModuleID ORDER BY ModuleID DESC";

                _commnadData.AddParameter("@ModuleID", ModuleID);

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
        public int InsertModule(string ModuleName, Int64 GeneralPrice, Int64 MarketPrice, Int64 ModuleDiscount, Int64 CreatedByID)
        {
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "INSERT INTO Modules( ModuleName, GeneralPrice, MarketPrice, ModuleDiscount, CreatedByID, CreatedDate,IsActive,IsDeleted) VALUES(@ModuleName, @GeneralPrice, @MarketPrice, @ModuleDiscount, @CreatedByID, GETDATE(), 1,0); SELECT SCOPE_IDENTITY();";

                commandData.AddParameter("@ModuleName", ModuleName);
                commandData.AddParameter("@GeneralPrice", GeneralPrice);
                commandData.AddParameter("@MarketPrice", MarketPrice);
                commandData.AddParameter("@ModuleDiscount", ModuleDiscount);
                commandData.AddParameter("@CreatedByID", CreatedByID);

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
        public int UpdateModule(Int64 ModuleID, string ModuleName, Int64 GeneralPrice, Int64 MarketPrice, Int64 ModuleDiscount, Int64 ModifiedByID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE Modules SET ModuleName = @ModuleName, GeneralPrice= @GeneralPrice, MarketPrice= @MarketPrice, ModuleDiscount = @ModuleDiscount, ModifiedByID = @ModifiedByID, ModifiedDate = GETDATE() WHERE ModuleID = @ModuleID";

                commandData.AddParameter("@ModuleID", ModuleID);
                commandData.AddParameter("@ModuleName", ModuleName);
                commandData.AddParameter("@GeneralPrice", GeneralPrice);
                commandData.AddParameter("@MarketPrice", MarketPrice);
                commandData.AddParameter("@ModuleDiscount", ModuleDiscount);
                commandData.AddParameter("@ModifiedByID", ModifiedByID);
                

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
        public int DeleteModule(Int64 ModuleID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = " Update Modules set IsDeleted=1 WHERE ModuleID = @ModuleID";

                commandData.AddParameter("@ModuleID", ModuleID);

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
        public int DeactivateModule(Int64 ModuleID, Int64 ModifiedByID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "update Modules set [IsActive]='False',[ModifiedByID]='" + ModifiedByID + "',[ModifiedDate]=GETDATE() where [ModuleID]=" + ModuleID;


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
        public int ActivateModule(Int64 ModuleID, Int64 ModifiedByID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "update Modules set [IsActive]='True',[ModifiedByID]='" + ModifiedByID + "',[ModifiedDate]=GETDATE() where [ModuleID]=" + ModuleID;

               
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
