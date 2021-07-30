using SqlDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CompanyProfileDML
    {

        public DataTable GetCompanyProfileDetails()
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "Select * from CompanyProfileDetail";

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

        public DataTable GetCompanyProfileByCustomer(string CustomerName)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT cpd.*, c.CompanyName, ct.ContainerType FROM CustomerProfileDetail cpd INNER JOIN CustomerProfile cp ON cp.ProfileId = cpd.ProfileId INNER JOIN Company c ON c.CompanyID = cp.CustomerId INNER JOIN ContainerType ct ON ct.ContainerTypeID = cpd.ContainerTypeID WHERE c.CompanyName = '" + CustomerName + "'";

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

        public DataTable GetCompanyProfileDetails(Int64 ProfileID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                //_commnadData.CommandText = "SELECT lf.CityName as 'LocationFrom', lt.CityName as 'LocationTo', cpd.*, ct.ContainerType FROM CustomerProfileDetail cpd INNER JOIN City lf ON lf.CityID = cpd.PickupLocationID INNER JOIN City lt ON lt.CityID = cpd.DropoffLocationID INNER JOIN ContainerType ct ON ct.ContainerTypeID = cpd.ContainerTypeID WHERE cpd.ProfileId = '" + ProfileID + "' ";
                _commnadData.CommandText = "SELECT lf.CityName as 'LocationFrom', lt.CityName as 'LocationTo', cpd.*, ct.ContainerType, vt.VehicleTypeName, p.* FROM CustomerProfileDetail cpd LEFT JOIN City lf ON lf.CityID = cpd.PickupLocationID LEFT JOIN City lt ON lt.CityID = cpd.DropoffLocationID LEFT JOIN ContainerType ct ON ct.ContainerTypeID = cpd.ContainerTypeID LEFT JOIN VehicleType vt ON vt.VehicleTypeID = cpd.VehicleTypeID INNER JOIN CustomerProfile p ON p.ProfileId = cpd.ProfileId WHERE cpd.ProfileId = '" + ProfileID + "' ";

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

        public DataTable GetCompanyProfileDetailsWithID(Int64 ProfileDetailID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT lf.CityName as 'LocationFrom', lt.CityName as 'LocationTo', cpd.*, ct.ContainerType FROM CustomerProfileDetail cpd INNER JOIN City lf ON lf.CityID = cpd.PickupLocationID INNER JOIN City lt ON lt.CityID = cpd.DropoffLocationID INNER JOIN ContainerType ct ON ct.ContainerTypeID = cpd.ContainerTypeID WHERE cpd.ProfileDetail = '" + ProfileDetailID + "' ";

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
        public DataTable GetCompanyBrokerDetailsWithID(Int64 ProfileDetailID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT cpd.*, vt.VehicleTypeName FROM CustomerProfileDetail cpd INNER JOIN VehicleType vt ON vt.VehicleTypeID = cpd.VehicleTypeID WHERE cpd.ProfileDetail = '" + ProfileDetailID + "' ";

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

        public DataTable GetCompanyProfile()
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT cp.*, CompanyName AS 'Companies',b.Name 'BrokerName' FROM CustomerProfile cp LEFT JOIN Company c ON c.CompanyID = cp.CustomerID LEFT JOIN Brokers b ON cp.BrokerID = b.ID";

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

        public DataTable GetCompanyProfile(Int64 ProfileID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT * FROM CustomerProfile WHERE ProfileId = '" + ProfileID + "' ";

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

        public DataTable GetCompanyProfile(Int64 ID,string PaymentTerm)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT * FROM CustomerProfile WHERE (BrokerID = "+ID+" OR CustomerId = "+ID+") AND PaymentTerm = '"+PaymentTerm+"' ";

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

        public int InsertProfileDetails(Int64 LocationFrom, Int64 LocationTo, double Rate, Int64 ContainerTypeID, Int64 ProfileID, Int64 LoginID)
        {
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "Insert Into CustomerProfileDetail (PickupLocationID, DropoffLocationID, ContainerRate, ContainerTypeID, ProfileId, CreatedBy, CreatedDate) Values ('" + LocationFrom + "', '" + LocationTo + "', '" + Rate + "', " + ContainerTypeID + ", '" + ProfileID + "', '" + LoginID + "', getdate())";



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
        public int InsertProfileDetails(double BrokerRate, Int64 VehicleTypeID, Int64 ProfileID, Int64 LoginID)
        {
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "Insert Into CustomerProfileDetail (BrokerRate, VehicleTypeID, ProfileId, CreatedBy, CreatedDate) Values ( '" + BrokerRate + "', " + VehicleTypeID + ", '" + ProfileID + "', '" + LoginID + "', getdate())";



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

        public int InsertCompanyProfile(Int64 CustomerID, Int64 BrokerID, string payment, string Credit, Int64 LoginID)
        {
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                //commandData.CommandText = "Insert Into CustomerProfile (CustomerID,BrokerID,PaymentTerm,CreditTerm,InvoiceFormat,CreatedDate,CreatedBy) Values ('"+CustomerID+"' , '"+BrokerID+"' ,'" + payment + "','" + Credit + "', '" + Invoice + "' , getdate() , " + LoginID + ") ";
                commandData.CommandText = "Insert Into CustomerProfile (CustomerID,BrokerID,PaymentTerm,CreditTerm,CreatedDate,CreatedBy) Values ('" + CustomerID + "' , '" + BrokerID + "' ,'" + payment + "','" + Credit + "', getdate() , " + LoginID + ") ";



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

        public int UpdateCompanyProfile(Int64 ProfileID, Int64 BrokerID, Int64 CustomerID, string payment, string Credit, Int64 ModifiedBy)
        {
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE CustomerProfile SET [CustomerID] = " + CustomerID + " ,[BrokerID] = '" + BrokerID + "' ,[PaymentTerm] = '" + payment + "', [CreditTerm] = '" + Credit + "', [ModifiedBy] = '" + ModifiedBy + "', [ModifiedDate] = getdate() WHERE ProfileID = '" + ProfileID + "'    ";



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

        public int UpdateCompanyProfileDetails(Int64 ProfileDetailId, Int64 LocationFrom, Int64 LocationTo, double ContainerRate, Int64 ContainerTypeID, Int64 LoginID)
        {
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE CustomerProfileDetail SET [PickupLocationID] = '" + LocationFrom + "', [DropoffLocationID] = '" + LocationTo + "', [ContainerRate] = '" + ContainerRate + "', [ContainerTypeID] = " + ContainerTypeID + ", [ModifiedBy] = '" + LoginID + "', [ModifiedDate] = getdate() WHERE ProfileDetail = '" + ProfileDetailId + "'";

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
        public int UpdateCompanyProfileDetails(Int64 ProfileDetailId, double BrokerRate, Int64 VehicleTypeID, Int64 ProfileID, Int64 LoginID)
        {
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE CustomerProfileDetail SET [BrokerRate] = '" + BrokerRate + "', [VehicleTypeID] = '" + VehicleTypeID + "',  [ModifiedBy] = '" + LoginID + "', [ModifiedDate] = getdate() WHERE ProfileDetail = '" + ProfileDetailId + "'";

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


        public int DeleteCompanyProfile(Int64 ProfileID)
        {
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = " Delete from CustomerProfile WHERE ProfileID = '" + ProfileID + "' ";



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

        public int DeleteCompanyProfileDetail(Int64 ID)
        {
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = " Delete from CustomerProfileDetail WHERE ProfileDetail = '" + ID + "' ";



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


    }
}
