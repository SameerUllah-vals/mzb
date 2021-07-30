using SqlDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CityDML
    {
        public DataTable GetProvince()
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "select * from province order by ProvinceName ASC";

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

        public DataTable GetCities()
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT C.*, p.provinceName AS 'ProvinceName' FROM City C INNER JOIN Province p ON p.ProvinceID = C.ProvinceID  ORDER BY C.CityName ASC;";

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
        public DataTable GetStudent()
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "select * from Student where isDeleted=0 ";

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

        public DataTable GetCities(Int64 ID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT C.*, p.provinceName AS 'ProvinceName' FROM City C INNER JOIN Province p ON p.ProvinceID = C.ProvinceID where C.CityID= '" + ID + "' ORDER BY C.CityName ASC;";

                //_commnadData.AddParameter("@PickDropID", ID);

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

        public DataTable GetStudent(Int64 ID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "   select * from Student s where s.ID='" + ID + "' order by s.Name asc;";

                //_commnadData.AddParameter("@PickDropID", ID);

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

        public DataTable GetCities(string Keyword)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT C.*, p.provinceName AS 'ProvinceName' FROM City C INNER JOIN Province p ON p.ProvinceID = C.ProvinceID where C.CityCode like '%" + Keyword + "%' or C.CityName like '%" + Keyword + "%' or p.ProvinceName like '" + Keyword + "'    ORDER BY C.CityName ASC; ";

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
        public DataTable GetStudent(string Keyword)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "select * from Student s where s.Name like'%" + Keyword + "%' or s.Class like'%" + Keyword + "%' or s.Gender like'%" + Keyword + "%' or s.DOB like'%" + Keyword + "%' order by s.Name ASC; ";

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

        public DataTable GetCities(string Code, string Name)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT * FROM City where CityCode = '" + Code + "' or CityName = '" + Name + "' Order By CityName asc";

                //_commnadData.AddParameter("@PickDropCode", Code);
                //_commnadData.AddParameter("@PickDropLocationName", Name);

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
        public DataTable GetStudent(string Name, string Class, string Gender)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT * FROM Student where Name = '" + Name + "' or Class = '" + Class + "' or Gender='" + Gender + "' Order By Name asc";

                //_commnadData.AddParameter("@PickDropCode", Code);
                //_commnadData.AddParameter("@PickDropLocationName", Name);

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

        public DataTable GetCitieSByProvince(Int64 ProvinceID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT * FROM City WHERE ProvinceID = " + ProvinceID;

                //_commnadData.AddParameter("@PickDropCode", Code);
                //_commnadData.AddParameter("@PickDropLocationName", Name);

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

        public int InsertCity(string Code, string Name, Int64 ProvinceID, string Description, Int64 CreatedBy)
        {
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "insert into city (CityCode,CityName,ProvinceID,Description,CreatedByUserID,DateCreated) values ('" + Code + "', '" + Name + "' ,'" + ProvinceID + "','" + Description + "',  '" + CreatedBy + "' , getdate() )";

                //commandData.AddParameter("@CityCode", Code);
                //commandData.AddParameter("@CityName", Name);
                //commandData.AddParameter("@ProvinceID", ProvinceID);
                //commandData.AddParameter("@Description", Description);
                //commandData.AddParameter("@isActive", Active);
                //commandData.AddParameter("@CreatedByUserID", CreatedBy);

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
        public int InsertStudent(string Name, string Class, string Gender, string DOB, Int64 CreatedBy)
        {
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "insert into Student (Name,Class,Gender,DOB,CreatedBy,CreatedDate,IsDeleted,IsActive) values ('" + Name + "', '" + Class + "' ,'" + Gender + "','" + DOB + "',  '" + CreatedBy + "' , getdate(),0,1 )";

                //commandData.AddParameter("@CityCode", Code);
                //commandData.AddParameter("@CityName", Name);
                //commandData.AddParameter("@ProvinceID", ProvinceID);
                //commandData.AddParameter("@Description", Description);
                //commandData.AddParameter("@isActive", Active);
                //commandData.AddParameter("@CreatedByUserID", CreatedBy);

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

        public int UpdateCity(Int64 CityID, string CityCode, string CityName, string Description, Int64 ProvinceID, Int64 ModifiedByUserID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE city SET [CityCode] = '" + CityCode + "' ,[CityName] = '" + CityName + "' , [ProvinceID] = '" + ProvinceID + "' , [Description] = '" + Description + "' ,  [ModifiedByUserID] = '" + ModifiedByUserID + "' , [DateModified] = getdate() where CityID =  " + CityID;

                //commandData.AddParameter("@CityID", CityID);
                //commandData.AddParameter("@CityCode", CityCode);
                //commandData.AddParameter("@CityName", CityName);
                //commandData.AddParameter("@Description", Description);
                //commandData.AddParameter("@ModifiedByUserID", ModifiedByUserID);
                //commandData.AddParameter("@IsActive", Active);

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

        public int UpdateStudent(Int64 ID, string Name, string Class, string Gender, string DOB, Int64 ModifiedBy)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE Student SET [Name] = '" + Name + "' ,[Class] = '" + Class + "' , [Gender] = '" + Gender + "' , [DOB] = '" + DOB + "' ,  [ModifiedBy] = '" + ModifiedBy + "' , [ModifiedDate] = getdate() where ID =  " + ID;

                //commandData.AddParameter("@CityID", CityID);
                //commandData.AddParameter("@CityCode", CityCode);
                //commandData.AddParameter("@CityName", CityName);
                //commandData.AddParameter("@Description", Description);
                //commandData.AddParameter("@ModifiedByUserID", ModifiedByUserID);
                //commandData.AddParameter("@IsActive", Active);

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

        public int ActivateCity(Int64 CityID, Int64 ModifiedByID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE City SET [isActive] = 'True' ,  [ModifiedByUserID] = '" + ModifiedByID + "', [DateModified] = getdate() WHERE [CityID] = " + CityID;


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
        public int ActivateStudent(Int64 ID, Int64 ModifiedBy)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "update Student set [isActive]='True',[ModifiedBy]='" + ModifiedBy + "',[ModifiedDate]=GETDATE() where [ID]=" + ID;


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

        public int DeactivateCity(Int64 CityID, Int64 ModifiedByID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE City SET [isActive] = 'False' ,  [ModifiedByUserID] = '" + ModifiedByID + "', [DateModified] = getdate() WHERE [CityID] = " + CityID;


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

        public int DeactivateStudent(Int64 ID, Int64 ModifiedBy)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "update Student set [isActive]='False',[ModifiedBy]='" + ModifiedBy + "',[ModifiedDate]=GETDATE() where [ID]=" + ID;


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

        public int DeleteCity(Int64 CityID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "Delete From City where CityID = " + CityID;

                //commandData.AddParameter("@CityID", CityID);
                //commandData.AddParameter("@CityID", CityID);

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
        public int DeleteStudent(Int64 ID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "Update Student set isDeleted = 'true' where ID  = " + ID;



                //commandData.AddParameter("@CityID", CityID);
                //commandData.AddParameter("@CityID", CityID);

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
        //public DataTable SGetGender()
        //{
        //    //Creating object of DAL class
        //    CommandData _commnadData = new CommandData();

        //    try
        //    {
        //        _commnadData._CommandType = CommandType.Text;
        //        _commnadData.CommandText = "select * from Student order by Gender ASC";

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
    }
}