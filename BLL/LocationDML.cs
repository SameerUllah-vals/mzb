using SqlDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class LocationDML
    {
        public DataTable GetLocationType()
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT * FROM LocationType ORDER BY LocationTypeName ASC";

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

        public DataTable GetLocation()
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                //_commnadData.CommandText = "SELECT l.*, lt.LocationTypeName as 'Type', p.ProvinceName, c.CityName, r.Name AS 'RegionName', a.AreaName FROM PickDropLocation l INNER JOIN Province p ON p.ProvinceID = l.ProvinceID INNER JOIN City c ON c.CityID = l.CityID INNER JOIN Region r ON r.ID = l.RegionID INNER JOIN Area a ON a.ID = l.AreaID INNER JOIN LocationType lt ON lt.LocationTypeID = l.LocationTypeID ORDER BY l.PickDropLocationName ASC";
                _commnadData.CommandText = "SELECT pdl.*, c.CityName,a.AreaName, l.LocationTypeName FROM PickDropLocation pdl INNER JOIN Area a ON a.ID = pdl.AreaID INNER JOIN City c on c.CityID = a.CityID INNER JOIN LocationType l ON l.LocationTypeID = pdl.LocationTypeID  Order by pdl.PickDropID DESC";

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

        public DataTable GetLocation(Int64 ID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT pdl.*, p.ProvinceName, c.CityName, 	r.Name RegionName,a.AreaName, l.LocationTypeName, p.ProvinceID as Province, c.CityID, r.ID as RegionID FROM PickDropLocation pdl INNER JOIN Area a ON a.ID = pdl.AreaID INNER JOIN Region r on r.ID = a.Region INNER JOIN City c on c.CityID = r.CityID INNER JOIN Province p on p.ProvinceID = c.ProvinceID INNER JOIN LocationType l ON l.LocationTypeID = pdl.LocationTypeID WHERE pdl.PickDropID = '" + ID + "' ORDER BY pdl.PickDropLocationName ASC";

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

        public DataTable GetLocation(string Keyword)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                //_commnadData.CommandText = "SELECT l.*, lt.LocationTypeName as 'Type', p.ProvinceName, c.CityName, r.Name AS 'RegionName', a.AreaName FROM PickDropLocation l INNER JOIN Province p ON p.ProvinceID = l.ProvinceID INNER JOIN City c ON c.CityID = l.CityID INNER JOIN Region r ON r.ID = l.RegionID INNER JOIN Area a ON a.ID = l.AreaID INNER JOIN LocationType lt ON lt.LocationTypeID = l.LocationTypeID WHERE l.isActive = 'True' AND (l.PickDropCode = '" + Keyword + "' OR l.PickDropLocationName LIKE '%" + Keyword + "%' OR p.ProvinceName LIKE '%" + Keyword + "%' OR c.CityName LIKE '%" + Keyword + "%' OR r.Name LIKE '%" + Keyword + "%' OR a.AreaName LIKE '%" + Keyword + "%' OR l.Address LIKE '%" + Keyword + "%' OR l.Description LIKE '%" + Keyword + "%' ) ORDER BY l.PickDropLocationName ASC";
                _commnadData.CommandText = "SELECT pdl.*, p.ProvinceName, c.CityName, 	r.Name RegionName,a.AreaName, l.LocationTypeName FROM PickDropLocation pdl INNER JOIN Area a ON a.ID = pdl.AreaID INNER JOIN Region r on r.ID = a.Region INNER JOIN City c on c.CityID = r.CityID INNER JOIN Province p on p.ProvinceID = c.ProvinceID INNER JOIN LocationType l ON l.LocationTypeID = pdl.LocationTypeID WHERE l.isActive = 'True' AND (pdl.PickDropCode = '" + Keyword + "' OR pdl.PickDropLocationName LIKE '%" + Keyword + "%' OR p.ProvinceName LIKE '%" + Keyword + "%' OR c.CityName LIKE '%" + Keyword + "%' OR r.Name LIKE '%" + Keyword + "%' OR a.AreaName LIKE '%" + Keyword + "%' OR pdl.Address LIKE '%" + Keyword + "%' OR pdl.Description LIKE '%" + Keyword + "%' ) ORDER BY pdl.PickDropLocationName ASC";

                //_commnadData.AddParameter("@Keyword", Keyword);

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

        public DataTable GetLocation(string Code, string Name)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                //_commnadData.CommandText = "SELECT l.*, p.ProvinceName, c.CityName, r.Name AS 'RegionName', a.AreaName FROM PickDropLocation l INNER JOIN Province p ON p.ProvinceID = l.ProvinceID INNER JOIN City c ON c.CityID = l.CityID INNER JOIN Region r ON r.ID = l.RegionID INNER JOIN Area a ON a.ID = l.AreaID WHERE  l.isActive = 'True' AND	l.PickDropCode = '" + Code + "' or l.PickDropLocationName = '" + Name + "' ORDER BY l.PickDropLocationName ASC";

                _commnadData.CommandText = "SELECT pdl.*, p.ProvinceName, c.CityName, 	r.Name RegionName,a.AreaName, l.LocationTypeName FROM PickDropLocation pdl INNER JOIN Area a ON a.ID = pdl.AreaID INNER JOIN Region r on r.ID = a.Region INNER JOIN City c on c.CityID = r.CityID INNER JOIN Province p on p.ProvinceID = c.ProvinceID INNER JOIN LocationType l ON l.LocationTypeID = pdl.LocationTypeID WHERE (pdl.PickDropLocationName = '" + Name + "' OR pdl.PickDropCode = '" + Code + "') AND pdl.isActive = 1";

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

        public DataTable GetAllLocationsForBilty()
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT c.CityName + ' | ' + r.Name + ' | ' + a.AreaName + ' | ' + pdl.Address + ' | ' + pdl.PickDropCode as Location, pdl.*, p.ProvinceName, c.CityName, 	r.Name RegionName,a.AreaName, l.LocationTypeName FROM PickDropLocation pdl INNER JOIN Area a ON a.ID = pdl.AreaID INNER JOIN Region r on r.ID = a.Region INNER JOIN City c on c.CityID = r.CityID INNER JOIN Province p on p.ProvinceID = c.ProvinceID INNER JOIN LocationType l ON l.LocationTypeID = pdl.LocationTypeID WHERE pdl.isActive = 'True' ORDER BY pdl.PickDropLocationName ASC";

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

        public DataTable GetPickLocationsForBilty()
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                //_commnadData.CommandText = "SELECT l.*, p.ProvinceName, c.CityName, r.Name AS 'RegionName', a.AreaName FROM PickDropLocation l INNER JOIN Province p ON p.ProvinceID = l.ProvinceID INNER JOIN City c ON c.CityID = l.CityID INNER JOIN Region r ON r.ID = l.RegionID INNER JOIN Area a ON a.ID = l.AreaID WHERE  l.isActive = 'True' AND	l.PickDropCode = '" + Code + "' or l.PickDropLocationName = '" + Name + "' ORDER BY l.PickDropLocationName ASC";

                _commnadData.CommandText = "SELECT c.CityName + ' | ' + r.Name + ' | ' + a.AreaName + ' | ' + pdl.Address + ' | ' + pdl.PickDropCode as Location, pdl.*, p.ProvinceName, c.CityName, 	r.Name RegionName,a.AreaName, l.LocationTypeName FROM PickDropLocation pdl INNER JOIN Area a ON a.ID = pdl.AreaID INNER JOIN Region r on r.ID = a.Region INNER JOIN City c on c.CityID = r.CityID INNER JOIN Province p on p.ProvinceID = c.ProvinceID INNER JOIN LocationType l ON l.LocationTypeID = pdl.LocationTypeID WHERE pdl.isActive = 'True' AND l.LocationTypeName = 'Pick' ORDER BY pdl.PickDropLocationName ASC";

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

        public DataTable GetDropLocationsForBilty()
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                //_commnadData.CommandText = "SELECT l.*, p.ProvinceName, c.CityName, r.Name AS 'RegionName', a.AreaName FROM PickDropLocation l INNER JOIN Province p ON p.ProvinceID = l.ProvinceID INNER JOIN City c ON c.CityID = l.CityID INNER JOIN Region r ON r.ID = l.RegionID INNER JOIN Area a ON a.ID = l.AreaID WHERE  l.isActive = 'True' AND	l.PickDropCode = '" + Code + "' or l.PickDropLocationName = '" + Name + "' ORDER BY l.PickDropLocationName ASC";

                _commnadData.CommandText = "SELECT c.CityName + ' | ' + r.Name + ' | ' + a.AreaName + ' | ' + pdl.Address + ' | ' + pdl.PickDropCode as Location, pdl.*, p.ProvinceName, c.CityName, 	r.Name RegionName,a.AreaName, l.LocationTypeName FROM PickDropLocation pdl INNER JOIN Area a ON a.ID = pdl.AreaID INNER JOIN Region r on r.ID = a.Region INNER JOIN City c on c.CityID = r.CityID INNER JOIN Province p on p.ProvinceID = c.ProvinceID INNER JOIN LocationType l ON l.LocationTypeID = pdl.LocationTypeID WHERE pdl.isActive = 'True' AND l.LocationTypeName = 'Drop' ORDER BY pdl.PickDropLocationName ASC";

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

        public DataTable GetLocationByCode(string Code)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                //_commnadData.CommandText = "SELECT l.*, p.ProvinceName, c.CityName, r.Name AS 'RegionName', a.AreaName FROM PickDropLocation l INNER JOIN Province p ON p.ProvinceID = l.ProvinceID INNER JOIN City c ON c.CityID = l.CityID INNER JOIN Region r ON r.ID = l.RegionID INNER JOIN Area a ON a.ID = l.AreaID WHERE  l.isActive = 'True' AND	l.PickDropCode = '" + Code + "' or l.PickDropLocationName = '" + Name + "' ORDER BY l.PickDropLocationName ASC";

                _commnadData.CommandText = "SELECT pdl.*, p.ProvinceName, c.CityName, 	r.Name RegionName,a.AreaName, l.LocationTypeName FROM PickDropLocation pdl INNER JOIN Area a ON a.ID = pdl.AreaID INNER JOIN Region r on r.ID = a.Region INNER JOIN City c on c.CityID = r.CityID INNER JOIN Province p on p.ProvinceID = c.ProvinceID INNER JOIN LocationType l ON l.LocationTypeID = pdl.LocationTypeID WHERE pdl.PickDropCode = '" + Code + "' AND pdl.isActive = 1";

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

        public int InsertLocation(string Code, string Name, Int64 AreaID, string Address, Int64 TypeID, string Description, Int64 CreatedBy)
        {
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "insert into PickDropLocation (PickDropCode, PickDropLocationName , AreaID, Address, LocationTypeID, Description, CreatedByUserID, DateCreated) Values ('" + Code + "', '" + Name + "' , " + AreaID + ", '" + Address + "', " + TypeID + ", '" + Description + "',  " + CreatedBy + ", getdate()); SELECT SCOPE_IDENTITY();";

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

        public int UpdateLocation(string Code, string Name, Int64 AreaID, string Address, Int64 TypeID, string Description,  Int64 ModifiedBy, Int64 LocationID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE PickDropLocation SET [PickDropCode] = '" + Code + "' , [PickDropLocationName] = '" + Name + "' , AreaID = '" + AreaID + "' , Address = '" + Address + "' , LocationTypeID =" + TypeID + " , [Description] = '" + Description + "'  , ModifiedByUserID = '" + ModifiedBy + "' , DateModified = getdate() where PickDropID = " + LocationID;

                //commandData.AddParameter("@PickDropID", LocationID);
                //commandData.AddParameter("@PickDropCode", Code);
                //commandData.AddParameter("@AreaID", AreaID);
                //commandData.AddParameter("@Address", Addresss);
                //commandData.AddParameter("@LocationTypeID", LocationTypeID);
                //commandData.AddParameter("@OwnerID", OwnerID);
                //commandData.AddParameter("@ModifiedByUserID", ModifiedBy);
                //commandData.AddParameter("@IsActive", Active);
                //commandData.AddParameter("@Description", Description);
                //commandData.AddParameter("@PickDropLocationName", Name);
                //commandData.AddParameter("@IsPort", isPort);

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

        public int ActivateLocation(Int64 ID, Int64 ModifiedByID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE PickDropLocation SET [isActive] = 'True' ,  [ModifiedByUserID] = '" + ModifiedByID + "', [DateModified] = getdate() WHERE [PickDropID] = " + ID;


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

        public int DeactivateLocation(Int64 ID, Int64 ModifiedByID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE PickDropLocation SET [isActive] = 'False' ,  [ModifiedByUserID] = '" + ModifiedByID + "', [DateModified] = getdate() WHERE [PickDropID] = " + ID;


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

        public int DeleteLocation(Int64 ID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "DELETE PickDropLocation WHERE PickDropID = " + ID;

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
