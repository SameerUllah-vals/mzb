using SqlDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class VehicleDML
    {
        public DataTable GetVehicle()
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT V.*, vt.VehicleTypeName AS 'VehicleType' FROM Vehicle V INNER JOIN VehicleType vt ON vt.VehicleTypeID = V.VehicleTypeID  ";

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
        public DataTable GetVehicleType()
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "select * from VehicleType where IsActive = 1";

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

        public DataTable GetVehicle(Int64 ID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT V.*, vt.VehicleTypeName AS 'VehicleType' FROM Vehicle V INNER JOIN VehicleType vt ON vt.VehicleTypeID = V.VehicleTypeID WHERE V.VehicleID = '" + ID + "' ";

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

        public DataTable GetVehicleWithBroker(string keyword)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = @"select
                                            CONCAT(v.RegNo, ' | ', b.Name) 'VehicleRegNo'
                                            from Vehicle v
                                            LEFT JOIN Brokers b on b.ID = v.BrokerID
                                            where v.VehicleCode like '" + keyword+ "%' or v.RegNo like '" + keyword + "%'";

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
        public DataTable GetVehicleWithBrokerService(string keyword)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = @"select
                                            CONCAT(v.RegNo, ' | ', b.Name) 'VehicleRegNo'
                                            from Vehicle v
                                            LEFT JOIN Brokers b on b.ID = v.BrokerID
                                            where v.RegNo like '" + keyword + "%' ORDER BY v.RegNo ASC";

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

        public DataTable GetVehicleByRegNo(string RegNo)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = @"select * from vehicle where RegNo = '" + RegNo + "'";

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

        public DataTable GetVehicle(string Keyword)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT V.*, vt.VehicleTypeName AS 'VehicleType' FROM Vehicle V INNER JOIN VehicleType vt ON vt.VehicleTypeID = V.VehicleTypeID WHERE V.VehicleCode like '%" + Keyword + "%' or V.RegNo like '%" + Keyword + "%' or V.EngineNo like '%" + Keyword + "%' or V.ChasisNo like '%" + Keyword + "%' or V.ManufacturingYear like '%" + Keyword + "%' or V.VehicleModel like '%" + Keyword + "%' or V.VehicleColor like '%" + Keyword + "%' or V.BodyType like '%" + Keyword + "%' or V.VehicleTypeID like '%" + Keyword + "%' or V.ManufacturerName like '%" + Keyword + "%' or V.Width like '%" + Keyword + "%'  or V.PurchasingDate like '%" + Keyword + "%' or V.PurchasingAmount like '%" + Keyword + "%' or V.PurchaseFromName like '%" + Keyword + "%' or V.PurchaseFromDetail like '%" + Keyword + "%' or V.OwnerName like '%" + Keyword + "%' or V.OwnerContact like '%" + Keyword + "%' or V.OwnerNIC like '%" + Keyword + "%' or V.Description like '%" + Keyword + "%' or V.Length like '%" + Keyword + "%' OR vt.VehicleTypeName like '%" + Keyword + "%'   or V.DimensionUnitType like '%" + Keyword + "%'   or V.Type like '%" + Keyword + "%' ";

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
        
        public DataTable GetVehicleDocuments(Int64 VehicleID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = @"select vd.*,dt.DocumentTypeID,dt.Name from VehicleDocuments vd 
                                            inner join DocumentType dt on vd.DocumentTypeID = dt.DocumentTypeID where vd.VehicleID = " + VehicleID + " and ISNULL(vd.IsDeleted,0) = 0";

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

        public DataTable GetVehicle(string Code, string Regno)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT * FROM Vehicle where VehicleCode = '" + Code + "' or RegNo = '" + Regno + "' order by VehicleCode asc";

                _commnadData.AddParameter("@PickDropCode", Code);
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

        public int InsertVehicle(string VehicleCode, string RegistrationNo, string EngineNo, string ChassisNo, string Make, string Model, string Manufacturer, string Color, string BodyType, Int64 VehicleType, Int64 MaximumLoadingCapacity, Int64 LoadingLimitNHA, DateTime PurchaseDate, Int64 PurchaseAmount, string PurchaseFrom, string PurchaseDetails, string OwnerName, string OwnerContact, string OwnerNIC, string OwnerDescription, string OwnershipStatus, Int64 CreatedBy, double Length, double Width, double height, string DimensionUnitType, string type, string manufacYear, int isownVehicle, string doc,Int64 BrokerID)
        {
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "INSERT INTO Vehicle ([VehicleCode], [RegNo], [EngineNo], [ChasisNo], [VehicleModel],[ManufacturerName],[VehicleColor],[BodyType],[VehicleTypeID],[MaximumLoadingCapacity],[LoadingLimitNHA],[PurchasingDate],[PurchasingAmount],[PurchaseFromName],[PurchaseFromDetail],[OwnerName],[OwnerContact],[OwnerNIC],[Description],[OwnershipStatus] ,[CreatedByUserID],[DateCreated],[Length],[Width], [Height], [DimensionUnitType], [Type],[ManufacturingYear], [isOwnVehicle],[Make], [Document],[BrokerID],[Internal] ) VALUES ('" + VehicleCode + "', '" + RegistrationNo + "', '" + EngineNo + "', '" + ChassisNo + "', '" + Model + "' ,'" + Manufacturer + "','" + Color + "','" + BodyType + "', '" + VehicleType + "','" + MaximumLoadingCapacity + "','" + LoadingLimitNHA + "','" + PurchaseDate + "','" + PurchaseAmount + "','" + PurchaseFrom + "','" + PurchaseDetails + "' , '" + OwnerName + "','" + OwnerContact + "' , '" + OwnerNIC + "','" + OwnerDescription + "','" + OwnershipStatus + "' , '" + CreatedBy + "', getdate() , '" + Length + "' , '" + Width + "' , '" + height + "' , '" + DimensionUnitType + "' , '" + type + "' , '" + manufacYear + "' , '" + isownVehicle + "' , '" + Make + "' , '" + doc + "'," + BrokerID + ",'1' )";



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

        public int InsertVehicle(string code, string RegistrationNo, Int64 BrokerID,Int64 CreatedBy)
        {
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "insert into Vehicle(VehicleCode,RegNo,VehicleTypeID,BrokerID,CreatedByUserID,DateCreated) values ('" + code + "','" + RegistrationNo +"',1," + BrokerID + "," + CreatedBy + ",GETDATE())";



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

        public int UpdateVehicle(Int64 VehicleID, string VehicleCode, string RegistrationNo, string EngineNo, string ChassisNo, string Make, string Model, string Manufacturer, string Color, string BodyType, Int64 VehicleType, Int64 MaximumLoadingCapacity, Int64 LoadingLimitNHA, DateTime PurchaseDate, Int64 PurchaseAmount, string PurchaseFrom, string PurchaseDetails, string OwnerName, string OwnerContact, string OwnerNIC, string OwnerDescription, string OwnershipStatus, Int64 ModifiedBy, double Length, double Width, double height, string DimensionUnitType, string type, string manufacYear, int isownVehicle, string Filename,Int64 brokerID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE Vehicle SET [VehicleCode] = '" + VehicleCode + "', [RegNo] = '" + RegistrationNo + "', [EngineNo] = '" + EngineNo + "', [ChasisNo] = '" + ChassisNo + "', [Make] = '" + Make + "', [VehicleModel] = '" + Model + "',[ManufacturerName] = '" + Manufacturer + "',[VehicleColor] = '" + Color + "',[BodyType] = '" + BodyType + "',[VehicleTypeID] = '" + VehicleType + "',[MaximumLoadingCapacity] = '" + MaximumLoadingCapacity + "', [LoadingLimitNHA] = '" + LoadingLimitNHA + "',[PurchasingDate] = '" + PurchaseDate + "',[PurchasingAmount] = '" + PurchaseAmount + "',[PurchaseFromName] = '" + PurchaseFrom + "',[PurchaseFromDetail] = '" + PurchaseDetails + "',[OwnerName] = '" + OwnerName + "',[OwnerContact] = '" + OwnerContact + "',[OwnerNIC] = '" + OwnerNIC + "',[Description] = '" + OwnerDescription + "',[OwnershipStatus] = '" + OwnershipStatus + "', [ModifiedByUserID] = '" + ModifiedBy + "' , [Height] = '" + height + "' , [Length] = '" + Length + "' , [Width] = '" + Width + "' , [DimensionUnitType] = '" + DimensionUnitType + "' ,[Type] = '" + type + "' ,[ManufacturingYear] = '" + manufacYear + "' ,[isOwnVehicle] = '" + isownVehicle + "', BrokerID = " + brokerID + " ,[Document] = '" + Filename + "' ,[DateModified] = getdate() WHERE [VehicleID] = " + VehicleID;


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

        public int ActivateVehicle(Int64 ID, Int64 ModifiedByID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE Vehicle SET [isActive] = 'True' ,  [ModifiedByUserID] = '" + ModifiedByID + "', [DateModified] = getdate() WHERE [VehicleID] = " + ID;


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

        public int DeactivateVehicle(Int64 ID, Int64 ModifiedByID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE Vehicle SET [isActive] = 'False' ,  [ModifiedByUserID] = '" + ModifiedByID + "', [DateModified] = getdate() WHERE [VehicleID] = " + ID;


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

        public int DeleteVehilceDetails(Int64 ID, Int64 ModifiedByID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "update VehicleDocuments set IsDeleted = 1 , ModifyBy = "+ModifiedByID+ " , ModifyDate = GETDATE() where ID = " + ID;


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
        public int InsertVehicleDocument(Int64 VehilceID,string ImageName,Int64 VehicleType, Int64 CreatedBy)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "insert into VehicleDocuments(VehicleID,ImageName,DocumentTypeID,CreatedBy,CreatedDate) values(" + VehilceID + ",'"+ ImageName +"',"+VehicleType+","+CreatedBy+",GETDATE())";


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

        public DataTable GetVehicleDocuments(string VehicleID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "select * from VehicleDocuments where VehicleID = "+VehicleID;

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

        public int DeleteVehicle(Int64 ID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "Delete from Vehicle where VehicleID =  " + ID;

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
