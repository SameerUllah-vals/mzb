using SqlDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace BLL
{
    public class GroupDML
    {
        public DataTable Getgroups()
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT * FROM Groups Order By [GroupName] asc";

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

        public DataTable GetActiveGroups()
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT * FROM Groups WHERE IsActive = 1 Order By [GroupName] asc";

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

        public DataTable GetGroup(Int64 ID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT * FROM Groups where GroupID = '" + ID + "' Order By [GroupName] asc";

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

        public DataTable GetGroup(string Keyword)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "Select * from Groups where [GroupCode] like '%" + Keyword + "%' or [GroupName] like '%" + Keyword + "%'  or [EmailAdd] like '%" + Keyword + "%' or [WebAdd] like '%" + Keyword + "%' or [Contact] like '%" + Keyword + "%' or [ContactOther] like '%" + Keyword + "%' or  [Address] like '%" + Keyword + "%' or [Description] like '%" + Keyword + "%'   ";

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

        public DataTable GetGroup(string Code, string group)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT * FROM Groups where GroupCode = '" + Code + "' or [GroupName] = '" + group + "' Order By [GroupName] asc";

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

        public DataTable GetCompany(Int64 GroupID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT * FROM Company where GroupID = " + GroupID + " Order By CompanyName asc";

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

        public int InsertGroup(string Code, string group, string Email, string website, string Contact, string OtherContact, string Organization , string Address, string Description,  Int64 CreatedBy, string CompanyAccess)
        {
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "INSERT INTO Groups (GroupCode, GroupName, Contact, ContactOther, EmailAdd, WebAdd, Address, DateCreated, CreatedByUserID, Description, CompanyAccess) values ('" + Code + "',  '" + group + "'  , '" + Contact + "' , '" + OtherContact + "' , '" + Email + "' , '" + website + "' ,'" + Address + "', GETDATE(), " + CreatedBy + ", '" + Description + "', '" + CompanyAccess + "'); SELECT SCOPE_IDENTITY();";

                



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

        public int UpdateGroup(Int64 GroupID, string Code, string group, string Email, string website, string Contact, string OtherContact, string Organization, string Address, string Description, Int64 ModifiedBy, string CompanyAccess)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                //commandData.CommandText = "UPDATE Groups SET [Code] = '" + Code + "', [Group] = '" + group + "', [Email] = '" + Email + "', [Website] = '" + website + "', [Contact] = '" + Contact + "', [OtherContact] = '" + OtherContact + "' , [Organization] = '"+Organization+"'   , [Address] = '" + Address + "' , [Description] = '" + Description + "' , [isActive] = '" + Active + "' ,  [ModifiedBy] = '" + ModifiedBy + "', [ModifiedDate] = getdate() WHERE [ID] = " + GroupID;

                commandData.CommandText = "UPDATE Groups SET GroupCode = '" + Code + "', GroupName = '" + group + "', Contact = '" + Contact + "', ContactOther = '" + OtherContact + "', EmailAdd = '" + Email + "', WebAdd = '" + website + "', Address = '" + Address + "', DateModified = GETDATE(), ModifiedByUserID = " + ModifiedBy + ", Description = '" + Description + "',  CompanyAccess = '" + CompanyAccess + "' WHERE GroupID = " + GroupID;


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


        public int ActivateGroup(Int64 GroupID, Int64 ModifiedByID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE Groups SET [isActive] = 'True' ,  [ModifiedByUserID] = '" + ModifiedByID + "', [DateModified] = getdate() WHERE [GroupID] = " + GroupID;


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

        public int DeactivateGroup(Int64 GroupID, Int64 ModifiedByID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE Groups SET [isActive] = 'False' ,  [ModifiedByUserID] = '" + ModifiedByID + "', [DateModified] = getdate() WHERE [GroupID] = " + GroupID;


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
        public int DeleteGroup(Int64 ID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "Delete from Groups where Groupid =  " + ID;

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
