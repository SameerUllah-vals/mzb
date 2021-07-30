using SqlDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class OwnDepartmentDML
    {
        

        public DataTable GetDepartment()
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT D.*, c.CompanyName, g.GroupName AS 'Groups' FROM OwnDepartment D INNER JOIN OwnCompany c ON c.CompanyID = D.CompanyID INNER JOIN OwnGroups g ON g.GroupID = D.GroupID ORDER BY D.DepartName ASC";

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

        public DataTable GetDepartment(Int64 ID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT D.*, c.CompanyName, g.GroupName AS 'Groups' FROM OwnDepartment D INNER JOIN OwnCompany c ON c.CompanyID = D.CompanyID INNER JOIN OwnGroups g ON g.GroupID = D.GroupID WHERE D.DepartID = '"+ID+"' ORDER BY D.DepartName ASC";

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

        public DataTable GetDepartment(string Keyword)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = " SELECT D.*, c.CompanyName, g.[GroupName] AS 'Groups' FROM OwnDepartment D INNER JOIN OwnCompany c ON c.CompanyID = D.CompanyID INNER JOIN OwnGroups g ON g.GroupID = D.GroupID WHERE  D.DepartCode like '%" + Keyword + "%' or D.DepartName like '%" + Keyword + "%' or D.EmailAdd like '%" + Keyword + "%' or c.CompanyName like '%" + Keyword + "%' or g.[GroupName] like '%" + Keyword + "%' or D.WebAdd like '%" + Keyword + "%' or D.contact like '%" + Keyword + "%' or D.ContactOther like '%" + Keyword + "%' or D.[Address] like '%" + Keyword + "%' or D.[Description] like '%" + Keyword + "%'  ORDER BY D.DepartName ASC ";

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

        public DataTable GetDepartment(string Code, string Name)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT * FROM OwnDepartment where DepartCode = '" + Code + "' and DepartName = '" + Name + "' Order By DepartName asc";

                _commnadData.AddParameter("@PickDropCode", Code);
                _commnadData.AddParameter("@PickDropLocationName", Name);

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

        public DataTable GetDepartmentByGroupAndCompany(Int64 CompanyID, Int64 GroupID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT * FROM OwnDepartment WHERE GROUPID = '" + GroupID + "' AND COMPANYID = '" + CompanyID + "' ORDER BY DepartName ASC";
                
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

        public int InsertDepartment(string Code, string Name, string Email, Int64 GroupID, Int64 CompanyID, string Website, Int64 ContactNo, Int64 OtherContact , string Address, string Description, Int64 CreatedBy)
        {
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "INSERT INTO OwnDepartment ([DepartCode], DepartName, EmailAdd, [GroupID], [CompanyID], WebAdd,Contact,ContactOther,[Address],[Description],CreatedByUserID,DateCreated) VALUES ('" + Code + "', '" + Name + "', '" + Email + "', '" + GroupID + "', '" + CompanyID + "', '" + Website + "' , '" + ContactNo + "' ,'" + OtherContact + "' , '" + Address + "','" + Description + "' , '" + CreatedBy + "', getdate())";



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

        public int UpdateDepartment(Int64 SectionID, string Code, string Name, string Email, Int64 GroupID, Int64 CompanyID, string Website, Int64 ContactNo, Int64 OtherContact, string Address, string Description, Int64 ModifiedBy)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE OwnDepartment SET DepartCode = '" + Code + "', DepartName = '" + Name + "', EmailAdd = '" + Email + "', [GroupID] = '" + GroupID + "', [CompanyID] = '" + CompanyID + "', WebAdd = '" + Website + "' , Contact = '" + ContactNo + "' , [Address] = '" + Address + "', [Description] = '" + Description + "' , ContactOther = '" + OtherContact + "' ,  [ModifiedByUser] = '" + ModifiedBy + "', [DateModified] = getdate() WHERE DepartID = " + SectionID;


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

        public int ActivateDepart(Int64 DepartID, Int64 ModifiedByID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE OwnDepartment SET [isActive] = 'True' ,  [ModifiedByUser] = '" + ModifiedByID + "', [DateModified] = getdate() WHERE [DepartID] = " + DepartID;


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

        public int DeactivateDepart(Int64 DepartID, Int64 ModifiedByID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE OwnDepartment SET [isActive] = 'False' ,  [ModifiedByUser] = '" + ModifiedByID + "', [DateModified] = getdate() WHERE [DepartID] = " + DepartID;


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
        public int DeleteDepartment(Int64 ID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "Delete from OwnDepartment where DepartID =  " + ID;

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
