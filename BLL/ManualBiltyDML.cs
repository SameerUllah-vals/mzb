using SqlDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ManualBiltyDML
    {
        public DataTable GetPickUpLocation()
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT c.CityName + ' | ' + r.Name + ' | ' + a.AreaName + ' | ' + pdl.Address + ' | ' + pdl.PickDropCode as Location, pdl.*, p.ProvinceName, c.CityName, 	r.Name RegionName,a.AreaName, l.LocationTypeName FROM PickDropLocation pdl INNER JOIN Area a ON a.ID = pdl.AreaID INNER JOIN Region r on r.ID = a.Region INNER JOIN City c on c.CityID = r.CityID INNER JOIN Province p on p.ProvinceID = c.ProvinceID INNER JOIN LocationType l ON l.LocationTypeID = pdl.LocationTypeID WHERE l.LocationTypeName = 'Pick' ORDER BY pdl.PickDropLocationName ASC";
                //_commnadData.CommandText = "SELECT c.CityName + ' | ' + r.Name + ' | ' + a.AreaName + ' | ' + pdl.Address + ' | ' + pdl.PickDropCode as Location, pdl.*, p.ProvinceName, c.CityName, 	r.Name RegionName,a.AreaName, l.LocationTypeName FROM PickDropLocation pdl INNER JOIN Area a ON a.ID = pdl.AreaID INNER JOIN Region r on r.ID = a.Region INNER JOIN City c on c.CityID = r.CityID INNER JOIN Province p on p.ProvinceID = c.ProvinceID INNER JOIN LocationType l ON l.LocationTypeID = pdl.LocationTypeID ORDER BY pdl.PickDropLocationName ASC";

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

        public DataTable GetDropOffLocation()
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT c.CityName + ' | ' + r.Name + ' | ' + a.AreaName + ' | ' + pdl.Address + ' | ' + pdl.PickDropCode as Location, pdl.*, p.ProvinceName, c.CityName, 	r.Name RegionName,a.AreaName, l.LocationTypeName FROM PickDropLocation pdl INNER JOIN Area a ON a.ID = pdl.AreaID INNER JOIN Region r on r.ID = a.Region INNER JOIN City c on c.CityID = r.CityID INNER JOIN Province p on p.ProvinceID = c.ProvinceID INNER JOIN LocationType l ON l.LocationTypeID = pdl.LocationTypeID WHERE l.LocationTypeName = 'Drop' ORDER BY pdl.PickDropLocationName ASC";

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

        public DataTable GetPickDropLocation_OLD()
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT c.CityName + ' | ' + r.Name + ' | ' + a.AreaName + ' | ' + pdl.Address + ' | ' + pdl.PickDropCode as Location, pdl.*, p.ProvinceName, c.CityName, 	r.Name RegionName,a.AreaName, l.LocationTypeName FROM PickDropLocation pdl INNER JOIN Area a ON a.ID = pdl.AreaID INNER JOIN Region r on r.ID = a.Region INNER JOIN City c on c.CityID = r.CityID INNER JOIN Province p on p.ProvinceID = c.ProvinceID INNER JOIN LocationType l ON l.LocationTypeID = pdl.LocationTypeID ORDER BY pdl.PickDropLocationName ASC";

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

        public DataTable GetPickDropLocation()
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT c.CityName + ' | ' + ISNULL(r.Name, '') + ' | ' + ISNULL(a.AreaName, '') + ' | ' + ISNULL(l.Address, '') as Location, l.*, c.CityName, r.Name RegionName, a.AreaName, c.CityID FROM City c LEFT JOIN Region r ON r.CityID = c.CityID LEFT JOIN Area a ON a.Region = r.ID LEFT JOIN PickDropLocation l ON l.AreaID = a.ID WHERE ISNULL(c.IsActive, 0) = 1;";

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

        public DataTable GetCompanyProfile(string CustomerName, string PickUpLocation, string DropOffLocation, string ContainerTipe )
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT c.CompanyName as Company, pLoc.PickDropLocationName AS Pickup, dLoc.PickDropLocationName AS Dropoff, ct.ContainerType, cp.*, cpd.* FROM CustomerProfile cp INNER JOIN CustomerProfileDetail cpd ON cp.ProfileId = cpd.ProfileId INNER JOIN Company c ON c.CompanyID = cp.CustomerId INNER JOIN PickDropLocation pLoc ON pLoc.PickDropID = cpd.PickupLocationID INNER JOIN PickDropLocation dLoc ON dLoc.PickDropID = cpd.DropoffLocationID INNER JOIN ContainerType ct ON ct.ContainerTypeID = cpd.ContainerTypeID WHERE c.CompanyName = '"+CustomerName+"' AND pLoc.PickDropLocationName = '"+PickUpLocation+"' AND dLoc.PickDropLocationName = '"+DropOffLocation+"' AND ct.ContainerType = '"+ContainerTipe+"' ";
                    

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

        public DataTable GetCompanyProfile(string CustomerName, Int64 PickUpLocationID, Int64 DropOffLocationID, string ContainerTipe)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT c.CompanyName as Company, pLoc.CityName AS Pickup, dLoc.CityName AS Dropoff, ct.ContainerType, cp.*, cpd.* FROM CustomerProfile cp INNER JOIN CustomerProfileDetail cpd ON cp.ProfileId = cpd.ProfileId INNER JOIN Company c ON c.CompanyID = cp.CustomerId INNER JOIN City pLoc ON pLoc.CityID = cpd.PickupLocationID INNER JOIN City dLoc ON dLoc.CityID = cpd.DropoffLocationID INNER JOIN ContainerType ct ON ct.ContainerTypeID = cpd.ContainerTypeID WHERE c.CompanyName = '" + CustomerName + "' AND cpd.PickupLocationID = " + PickUpLocationID + " AND cpd.DropoffLocationID = '" + DropOffLocationID + "' AND ct.ContainerType = '" + ContainerTipe + "' ";


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
    }
}
