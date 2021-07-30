using SqlDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class UsersDML
    {
        public DataTable GetUsers()
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT u.*, g.GroupName, c.CompanyName, d.DepartName, ds.DesignationName FROM UserAccounts u INNER JOIN OwnGroups g ON g.GroupID = u.GroupID INNER JOIN OwnCompany c ON c.CompanyID = u.CompanyID INNER JOIN OwnDepartment d ON d.DepartID = u.DepartmentID INNER JOIN Designation ds ON ds.DesignationId = u.DesignationID ORDER BY u.UserName ASC";

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

        public DataTable GetUsers(Int64 UserID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT u.*, g.GroupName, c.CompanyName, d.DepartName, ds.DesignationName FROM UserAccounts u INNER JOIN OwnGroups g ON g.GroupID = u.GroupID INNER JOIN OwnCompany c ON c.CompanyID = u.CompanyID INNER JOIN OwnDepartment d ON d.DepartID = u.DepartmentID INNER JOIN Designation ds ON ds.DesignationId = u.DesignationID  WHERE u.UserID = " + UserID;

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

        public DataTable GetUserForHome(Int64 UserID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT u.*, g.GroupName, c.CompanyName, d.DepartName, ds.DesignationName FROM UserAccounts u LEFT JOIN OwnGroups g ON g.GroupID = u.GroupID LEFT JOIN OwnCompany c ON c.CompanyID = u.CompanyID LEFT JOIN OwnDepartment d ON d.DepartID = u.DepartmentID LEFT JOIN Designation ds ON ds.DesignationId = u.DesignationID WHERE u.UserID = " + UserID;

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

        public DataTable GetUsers(string UserName, Int64 DepartmentID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT * FROM UserAccounts WHERE UserName = '" + UserName + "' AND DepartmentID = " + DepartmentID + " ORDER BY UserName ASC";

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

        public DataTable GetUsers(string Keyword)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT u.*, g.GroupName, c.CompanyName, d.DepartName, ds.DesignationName FROM UserAccounts u INNER JOIN OwnGroups g ON g.GroupID = u.GroupID INNER JOIN OwnCompany c ON c.CompanyID = u.CompanyID INNER JOIN OwnDepartment d ON d.DepartID = u.DepartmentID INNER JOIN Designation ds ON ds.DesignationId = u.DesignationID  WHERE u.UserName LIKE '%" + Keyword + "%' OR g.GroupName LIKE '%" + Keyword + "%' OR c.CompanyName LIKE '%" + Keyword + "%' OR d.DepartName LIKE '%" + Keyword + "%' OR ds.DesignationName LIKE '%" + Keyword + "%' OR r.RoleName LIKE '%" + Keyword + "%' ORDER BY u.UserName ASC";

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

        public DataTable GetUsers(string UserName, string Password)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT * FROM UserAccounts WHERE UserName = '" + UserName + "' AND UserPassword = '" + Password + "'";

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

        public DataTable GetDesignations()
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT * FROM Designation WHERE Active = 1 ORDER BY DesignationName ASC";

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

        public DataTable GetRoles()
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT * FROM Role ORDER BY RoleName ASC";

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

        public int InsertUser(string UserName, string Password, Int64 GroupID, Int64 CompanyID, Int64 DepartmentID, Int64 DesignationID, string Roles, Int64 CreatedByID)
        {
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "INSERT INTO UserAccounts (UserName, UserPassword, GroupID, CompanyID, DepartmentID, DesignationID, Roles, CreatedBy, CreatedDate) VALUES ('" + UserName + "', '" + Password + "', " + GroupID + ", " + CompanyID + ", " + DepartmentID + ", " + DesignationID + ", '" + Roles + "', " + CreatedByID + ", GETDATE()); SELECT SCOPE_IDENTITY()";



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

        public int UpdateUser(Int64 UserID, string UserName, Int64 GroupID, Int64 CompanyID, Int64 DepartmentID, Int64 DesignationID, string Roles, Int64 ModifiedBy)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE UserAccounts SET UserName = '" + UserName + "', GroupID = " + GroupID + ", CompanyID = " + CompanyID + ", DepartmentID = " + DepartmentID + ", DesignationID = " + DesignationID + ", Roles = '" + Roles + "', ModifiedBy = " + ModifiedBy + ", ModifiedDate = GETDATE() WHERE UserID = " + UserID;


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

        public int ActivateUser(Int64 UserID, Int64 ModifiedByID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE UserAccounts SET Active = 'True' ,  [ModifiedBy] = '" + ModifiedByID + "', [ModifiedDate] = getdate() WHERE UserID = " + UserID;


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

        public int DeactivateUser(Int64 UserID, Int64 ModifiedByID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE UserAccounts SET Active = 'False' ,  [ModifiedBy] = '" + ModifiedByID + "', [ModifiedDate] = getdate() WHERE UserID = " + UserID;


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

        public int DeleteUser(Int64 UserID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "DELETE FROM UserAccounts WHERE UserID = " + UserID;

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
