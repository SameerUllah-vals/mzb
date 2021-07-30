using SqlDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class AreaDML
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
        //public DataTable GetArea()
        //{
        //    //Creating object of DAL class
        //    CommandData _commnadData = new CommandData();

        //    try
        //    {
        //        _commnadData._CommandType = CommandType.StoredProcedure;
        //        _commnadData.CommandText = "Area_GetAllArea";

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

        //public DataTable GetArea(Int64 AreaID)
        //{
        //    //Creating object of DAL class
        //    CommandData _commnadData = new CommandData();

        //    try
        //    {
        //        _commnadData._CommandType = CommandType.StoredProcedure;
        //        _commnadData.CommandText = "Area_GetAreaByAreaID";

        //        _commnadData.AddParameter("@AreaID", AreaID);

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

        //public DataTable GetArea(string Code, string Name, string City)
        //{
        //    //Creating object of DAL class
        //    CommandData _commnadData = new CommandData();

        //    try
        //    {
        //        _commnadData._CommandType = CommandType.StoredProcedure;
        //        _commnadData.CommandText = "Area_GetDuplicateArea";

        //        _commnadData.AddParameter("@AreaCode", Code);
        //        _commnadData.AddParameter("@AreaName", Name);
        //        _commnadData.AddParameter("@CityName", City);

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

        //public int InsertArea(string Code, string Name, Int64 CityID, string Description, int isActive, Int64 CreatedBy)
        //{
        //    CommandData commandData = new CommandData();

        //    try
        //    {
        //        commandData._CommandType = CommandType.StoredProcedure;
        //        commandData.CommandText = "Area_InsertArea";

        //        commandData.AddParameter("@AreaCode", Code);
        //        commandData.AddParameter("@AreaName", Name);
        //        commandData.AddParameter("@CityID", CityID);
        //        commandData.AddParameter("@Description", Description);
        //        commandData.AddParameter("@IsActive", isActive);
        //        commandData.AddParameter("@CreatedByUserID", CreatedBy);

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

        //public int UpdateArea(Int64 AreaID, string Code, string Name, Int64 CityID, string Description, int isActive, Int64 ModifiedBy)
        //{
        //    //Creating object of DAL class
        //    CommandData commandData = new CommandData();

        //    try
        //    {
        //        commandData._CommandType = CommandType.StoredProcedure;
        //        commandData.CommandText = "Area_UpdateArea";

        //        commandData.AddParameter("@AreaID", AreaID);
        //        commandData.AddParameter("@AreaCode", Code);
        //        commandData.AddParameter("@AreaName", Name);
        //        commandData.AddParameter("@CityID", CityID);
        //        commandData.AddParameter("@ModifiedByUserID", ModifiedBy);
        //        commandData.AddParameter("@IsActive", isActive);
        //        commandData.AddParameter("@Description", Description);

        //        //opening connection
        //        commandData.OpenWithOutTrans();

        //        //Executing Query
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

        //public int DeleteArea(Int64 AreaID)
        //{
        //    //Creating object of DAL class
        //    CommandData commandData = new CommandData();

        //    try
        //    {
        //        commandData._CommandType = CommandType.StoredProcedure;
        //        commandData.CommandText = "Area_DeleteAreaByAreaID";

        //        commandData.AddParameter("@AreaID", AreaID);

        //        //opening connection
        //        commandData.OpenWithOutTrans();

        //        //Executing Query
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

        public DataTable GetArea()
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT A.*, c.CityName AS 'CityName' , r.Name AS 'RegionName' FROM Area A INNER JOIN City c ON A.CityID = c.CityID INNER JOIN Region r ON a.Region = r.ID ORDER BY a.AreaName ASC";

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



        public DataTable GetArea(Int64 ID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT A.*, c.CityName AS 'CityName', r.Name AS 'RegionName' FROM Area A INNER JOIN City c ON c.CityID = A.CityID INNER JOIN Region r ON r.ID = A.Region WHERE A.ID = '" + ID + "' ORDER BY A.AreaName ASC";

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

        public DataTable GetArea(string Keyword)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT A.*, c.CityName AS 'CityName' , r.Name AS 'RegionName' FROM Area A INNER JOIN City c ON c.CityID = A.CityID INNER JOIN Region r ON r.ID = a.Region WHERE  A.Areacode like '%" + Keyword + "%' or A.AreaName like '%" + Keyword + "%'  or c.CityName like '%" + Keyword + "%' or A.[Province] like '%" + Keyword + "%' or r.Name like '%" + Keyword + "%' or A.[Description] like '" + Keyword + "' order by A.AreaName asc   ";

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

        public DataTable GetArea(string Code, string Name)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT * FROM Area where AreaCode = '" + Code + "' or AreaName = '" + Name + "' Order By AreaName asc";

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

        public DataTable GetAreaByRegion(Int64 RegionID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT * FROM Area WHERE Region = " + RegionID;

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
        public DataTable GetAreaByCity(Int64 CityID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT * FROM Area WHERE CityID = " + CityID;

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

        public int InsertArea(string Code, string Name, Int64 City, string Province, Int64 Region, string Description, Int64 CreatedBy)
        {
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "INSERT INTO Area ([AreaCode], [AreaName], [CityID], [Province], [Region], [Description] , [CreatedByUserID],[DateCreated]) VALUES ('" + Code + "', '" + Name + "', '" + City + "', '" + Province + "', '" + Region + "', '" + Description + "' , '" + CreatedBy + "', getdate())";



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

        public int ActivateArea(Int64 AreaID, Int64 ModifiedByID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE Area SET [Status] = 'True' ,  [ModifiedByUserID] = '" + ModifiedByID + "', [DateModified] = getdate() WHERE [ID] = " + AreaID;


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

        public int DeactivateArea(Int64 AreaID, Int64 ModifiedByID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE Area SET [Status] = 'False' ,  [ModifiedByUserID] = '" + ModifiedByID + "', [DateModified] = getdate() WHERE [ID] = " + AreaID;


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
