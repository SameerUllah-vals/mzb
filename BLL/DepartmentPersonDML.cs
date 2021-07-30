using SqlDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class DepartmentPersonDML
    {
        public DataTable GetDepartmentPersonBySO()
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "select * from DepartmentPerson where isdeleted=0 And Active=1  ORDER BY Name ASC";

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
        public DataTable GetDepartmentPerson()
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT D.*, g.[GroupName] AS 'Groups' , c.CompanyName AS 'CompanyName' , dp.DepartName AS 'Department' FROM DepartmentPerson D INNER JOIN Groups g ON g.GroupID = D.[GroupID] INNER JOIN Company c ON c.CompanyID = d.CompanyID INNER JOIN Department dp ON DP.DepartID = D.DepartmentID where d.isdeleted=0 And d.Active=1  ORDER BY D.Name ASC";

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

        public DataTable GetDepartmentPerson(Int64 ID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT D.*, g.[GroupName] AS 'Groups' , c.CompanyName AS 'CompanyName' , dp.DepartName AS 'Department' FROM DepartmentPerson D INNER JOIN Groups g ON g.GroupID = D.[GroupID] INNER JOIN Company c ON c.CompanyID = d.CompanyID INNER JOIN Department dp ON DP.DepartID = D.DepartmentID  WHERE D.PersonalID = '" + ID + "' ORDER BY D.Name ASC";

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

        public DataTable GetDepartmentPerson(string Keyword)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT D.*, g.[GroupName] AS 'Groups' , c.CompanyName AS 'CompanyName' , dp.DepartName AS 'Department' FROM DepartmentPerson D INNER JOIN Groups g ON g.GroupID = D.[GroupID] INNER JOIN Company c ON c.CompanyID = d.CompanyID INNER JOIN Department dp ON DP.DepartID = D.DepartmentID WHERE D.code like '%" + Keyword + "%' or D.[Name] like '%" + Keyword + "%' or D.[Email] like '%" + Keyword + "%' or D.[BusinessEmail] like '%" + Keyword + "%' or C.[CompanyName] like '%" + Keyword + "%'  or DP.DepartName like  '%" + Keyword + "%'  or D.[Designation] like  '%" + Keyword + "%'   or D.[PhoneNo] like  '%" + Keyword + "%'  or D.Cell like  '%" + Keyword + "%'   or D.[AddressOther] like  '%" + Keyword + "%'    or D.[AddressOffice] like '%" + Keyword + "%' or D.[Description] like '%" + Keyword + "%'  or G.GroupName like '%" + Keyword + "%' OR D.OtherContact LIKE '" + Keyword + "' ORDER BY D.Name ASC ";

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

        public DataTable GetDepartmentPerson(string Code, string Name)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT * FROM DepartmentPerson where Code = '" + Code + "' or Name = '" + Name + "' Order By name asc";

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

        public int InsertDepartmentPerson(string Code, string Name, string Email, string BusinessEmail, Int64 Group, Int64 Company, Int64 Department, string Designation, Int64 CellNo, Int64 LandLine, Int64 OtherContact, string AddressOffice, string AddressOther, string Description, int individual, Int64 CreatedBy)
        {
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "INSERT INTO DepartmentPerson ([Code], [Name], [Email], [BusinessEmail], [GroupID], [CompanyID],[DepartmentID],[Designation], [Cell], [PhoneNo] , [OtherContact] , [AddressOffice], [AddressOther] ,[Description], [IsIndividual] ,[CreatedBy],[CreatedDate]) VALUES ('" + Code + "', '" + Name + "', '" + Email + "', '" + BusinessEmail + "', '" + Group + "', '" + Company + "' , '" + Department + "' , '" + Designation + "' ,  '" + CellNo + "' ,  '" + LandLine + "'   ,'" + OtherContact + "' , '" + AddressOffice + "',   '" + AddressOther + "' ,  '" + Description + "' , '" + individual + "' , '" + CreatedBy + "', getdate())";



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

        public int UpdateDepartmentPerson(Int64 PersonID, string Code, string Name, string Email, string BusinessEmail, Int64 Group, Int64 Company, Int64 Department, string Designation, Int64 CellNo, Int64 LandLine, Int64 OtherContact, string AddressOffice, string AddressOther, string Description, int Individual, Int64 ModifiedBy)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE DepartmentPerson SET [Code] = '" + Code + "', [Name] = '" + Name + "', [Email] = '" + Email + "', [BusinessEmail] = '" + BusinessEmail + "', [GroupID] = '" + Group + "', [CompanyID] = '" + Company + "' , [DepartmentID] = '" + Department + "' , [Designation] =  '" + Designation + "' , [Cell] = '" + CellNo + "' , [PhoneNo] = '" + LandLine + "' , [OtherContact] = '" + OtherContact + "' , [AddressOffice] = '" + AddressOffice + "', [AddressOther] = '" + AddressOther + "' , [Description] = '" + Description + "' , [IsIndividual] = '" + Individual + "'  ,  [ModifiedBy] = '" + ModifiedBy + "', [ModifiedDate] = getdate() WHERE [PersonalID] = " + PersonID;


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

        public int ActivatePerson(Int64 PersonalID, Int64 ModifiedByID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE DepartmentPerson SET [Active] = 'True' ,  [ModifiedBy] = '" + ModifiedByID + "', [ModifiedDate] = getdate() WHERE [PersonalID] = " + PersonalID;


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

        public int DeactivatePerson(Int64 PersonalID, Int64 ModifiedByID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE DepartmentPerson SET [Active] = 'False' ,  [ModifiedBy] = '" + ModifiedByID + "', [ModifiedDate] = getdate() WHERE [PersonalID] = " + PersonalID;


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
        public int DeleteDepartmentPerson(Int64 ID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "Delete from DepartmentPerson where PersonalID =  " + ID;

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
