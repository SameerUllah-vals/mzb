using SqlDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CompanyAccountsDML
    {
        //public DataTable GetAccount(Int64 CompanyID, string SortState)
        //{
        //    //Creating object of DAL class
        //    CommandData _commnadData = new CommandData();

        //    try
        //    {
        //        _commnadData._CommandType = CommandType.Text;
        //        _commnadData.CommandText = "SELECT * FROM CompanyAccounts WHERE CompanyID = " + CompanyID + " ORDER BY AccountID " + SortState + ";";

        //        //opening connection
        //        _commnadData.OpenWithOutTrans();

        //        //Executing Query
        //        DataSet _ds = _commnadData.Execute(ExecutionType.ExecuteDataSet) as DataSet;

        //        return _ds.Tables[0];
        //    }
        //    catch (Exception ex)
        //    {
        //        //Console.WriteLine("No record found");
        //        throw ex;
        //    }
        //    finally
        //    {
        //        //Console.WriteLine("No ");
        //        _commnadData.Close();

        //    }
        //}

        //public int InsertCompanyAccount(Int64 CompanyID, string Description, double Debit, double Credit, double Balance, Int64 LoginID)
        //{
        //    CommandData commandData = new CommandData();

        //    try
        //    {
        //        commandData._CommandType = CommandType.Text;
        //        commandData.CommandText = "INSERT INTO CompanyAccounts (CompanyID, Item, Debit, Credit, Balance, CreatedByID, DateCreated) VALUES (" + CompanyID + ", '" + Description + "', " + Debit + ", " + Credit + ", " + Balance + ", " + LoginID + ", GETDATE()); SELECT SCOPE_IDENTITY();";



        //        commandData.OpenWithOutTrans();

        //        Object valReturned = commandData.Execute(ExecutionType.ExecuteScalar);

        //        return Convert.ToInt32(valReturned);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        commandData.Close();
        //    }
        //}

        public int UpdateArea(Int64 AreaID, string Code, string Name, Int64 City, string Province, Int64 Region, string Description, Int64 ModifiedBy)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE Area SET [AreaCode] = '" + Code + "', [AreaName] = '" + Name + "', [CityID] = '" + City + "', [Province] = '" + Province + "', [Region] = '" + Region + "', [Description] = '" + Description + "',  [ModifiedByUserID] = '" + ModifiedBy + "', [DateModified] = getdate() WHERE [ID] = " + AreaID;


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

        public int DeleteArea(Int64 ID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "Delete from Area where id =  " + ID;

                //commandData.AddParameter("@CityID", CityID);
                commandData.AddParameter("@PickDropID", ID);

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
