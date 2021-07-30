using SqlDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class OrderDML
    {
        public DataTable GetBilties(string Keyword)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                //_commnadData.CommandText = @"SELECT 
                //                                o.*, 
	               //                             (SELECT COUNT(*) FROM OrderVehicle ov WHERE ov.OrderID = o.OrderID) as Vehicles, 
	               //                             (SELECT COUNT(*) FROM OrderConsignment oc WHERE oc.OrderID = o.OrderID) as Containers, 
	               //                             (SELECT COUNT(*) FROM OrderProduct op WHERE op.OrderID = o.OrderID) as Products, 
	               //                             (SELECT COUNT(*) FROM OrderConsignmentReceiver ocr WHERE ocr.OrderID = o.OrderID) as Recievings, 
	               //                             (SELECT COUNT(*) FROM OrderDocumentReceiving odr WHERE odr.OrderID = o.OrderID) as 'RecievingDocs', 
	               //                             (SELECT COUNT(*) FROM OrderDamage od WHERE od.OrderID = o.OrderID) as Damages
                //                            FROM[Order] o ORDER BY o.RecordedDate DESC";

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

        public DataTable GetBilties()
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = @"SELECT 
                                                o.*, 
	                                            (SELECT COUNT(*) FROM OrderVehicle ov WHERE ov.OrderID = o.OrderID) as Vehicles, 
	                                            (SELECT COUNT(*) FROM OrderConsignment oc WHERE oc.OrderID = o.OrderID) as Containers, 
	                                            (SELECT COUNT(*) FROM OrderProduct op WHERE op.OrderID = o.OrderID) as Products, 
	                                            (SELECT COUNT(*) FROM OrderConsignmentReceiver ocr WHERE ocr.OrderID = o.OrderID) as Recievings, 
	                                            (SELECT COUNT(*) FROM OrderDocumentReceiving odr WHERE odr.OrderID = o.OrderID) as 'RecievingDocs', 
	                                            (SELECT COUNT(*) FROM OrderDamage od WHERE od.OrderID = o.OrderID) as Damages
                                            FROM[Order] o ORDER BY o.RecordedDate DESC";

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

        public DataTable GetBiltyVehiclesByOrder(Int64 OrderID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = @"SELECT * FROM OrderVehicle WHERE OrderID = " + OrderID;

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

        public DataTable GetBiltyVehicles(Int64 OrderVehicleID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = @"SELECT * FROM OrderVehicle WHERE OrderVehicleID = " + OrderVehicleID;

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

        public DataTable GetCompanies(string Keyword)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT CONCAT(c.CompanyCode + ' | ' + c.CompanyName + ' | ' + g.GroupName + ' | ', d.DepartName) AS Company FROM Company c LEFT JOIN Groups g ON g.GroupID = c.GroupID LEFT JOIN Department d ON d.COMPANYID = c.CompanyID WHERE c.CompanyName LIKE  '%" + Keyword + "%' OR d.DepartName LIKE '%" + Keyword + "%' OR g.GroupName LIKE '%" + Keyword + "%' OR c.CompanyCode LIKE '%" + Keyword + "%' OR g.GroupCode LIKE '%" + Keyword + "%' OR d.DepartCode LIKE '%" + Keyword + "%'";

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


        public int InsertBiltyOrder(Int64 OrderNo, string BiltyDate, Int64 SenderCompanyID, string SenderDepartment, Int64 ReceiverCompanyID, string ReceiverDepartment, 
            Int64 CustomerCompanyID, string CustomerDepartment, string PaymentType, Int64 PickupLocationID, Int64 DropoffLocationID, string ClearingAgent, double AdditionalWeight, 
            double ActualWeight, double BiltyFreight, double Freight, double PartyCommission, double AdvanceFreight, double FactoryAdvance, double DieselAdvance, double AdvanceAmount, 
            double TotalAdvance, double BalanceFreight, Int64 CreatedByID)
        {
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "INSERT INTO [Order] (OrderNo, Date, RecordedDate, SenderCompanyID, SenderDepartment, ReceiverCompanyID, ReceiverDepartment, CustomerCompanyID, CustomerDepartment, PaymentType, PickupLocationID, DropoffLocationID, ClearingAgent, AdditionalWeight, ActualWeight, BiltyFreight, Freight, PartyCommission, AdvanceFreight, FactoryAdvance, DieselAdvance, AdvanceAmount, TotalAdvance, BalanceFreight, CreatedByID, DateCreated) VALUES('" + OrderNo + "', '" + BiltyDate + "', GETDATE(), " + SenderCompanyID + ", '" + SenderDepartment + "', " + ReceiverCompanyID + ", '" + ReceiverDepartment + "', " + CustomerCompanyID + ", '" + CustomerDepartment + "', '" + PaymentType + "', " + PickupLocationID + ", '" + DropoffLocationID + "', '" + ClearingAgent + "', " + AdditionalWeight + ", " + ActualWeight + ", " + BiltyFreight + ", " + Freight + ", " + PartyCommission + ", " + AdvanceFreight + ", " + FactoryAdvance + ", " + DieselAdvance + ", " + AdvanceAmount + ", " + TotalAdvance + ", " + BalanceFreight + ", " + CreatedByID + ", GETDATE()); SELECT SCOPE_IDENTITY();";

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

        public int InsertOrderContainerInfo(Int64 OrderID, Int64 ContainerTypeID, string ContainerNo, double ContainerWeight, string EmptyContainerPickLocation, string EmptyContainerDropLocation, string VesselName, string Remarks, string AssignedVehicle)
        {
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "INSERT INTO OrderConsignment (OrderID, ContainerType, ContainerNo, ContainerWeight, EmptyContainerPickLocation, EmptyContainerDropLocation, VesselName, Remarks, AssignedVehicle) VALUES(" + OrderID + ", " + ContainerTypeID + ", '" + ContainerNo + "', " + ContainerWeight + ", '" + EmptyContainerPickLocation + "', '" + EmptyContainerDropLocation + "', '" + VesselName + "', '" + Remarks + "', '" + AssignedVehicle + "');";

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

        public int InsertOrderVehicleInfo(Int64 OrderID, string VehicleType, string VehicleRegNo, Int64 VehicleContactNo, Int64 BrokerID, string DriverName, string FatherName, Int64 DriverNIC, string DriverLicence, Int64 DriverCellNo, string ReportingTime, string InTime, string OutTime)
        {
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "INSERT INTO OrderVehicle (OrderID, VehicleType, VehicleRegNo, VehicleContactNo, BrokerID, DriverName, FatherName, DriverNIC, DriverLicence, DriverCellNo, ReportingTime, InTime, OutTime) VALUES(" + OrderID + ", '" + VehicleType + "', '" + VehicleRegNo + "', " + VehicleContactNo + ", " + BrokerID + ", '" + DriverName + "', '" + FatherName + "', " + DriverNIC + ", '" + DriverLicence + "', " + DriverCellNo + ", '" + ReportingTime + "', '" + InTime + "', '" + OutTime + "'); SELECT SCOPE_IDENTITY();";

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

        public int InsertOrderProduct(Int64 OrderID, string PackageType, string Item, Int64 Qty, double Weight)
        {
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "INSERT INTO OrderProduct (OrderID, PackageType, Item, Qty, TotalWeight) VALUES (" + OrderID + ", '" + PackageType + "', '" + Item + "', " + Qty + ", " + Weight + ");";

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

        public int InsertOrderReceiving(Int64 OrderID, string ReceivedBy, string DateTime)
        {
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "INSERT INTO OrderConsignmentReceiver (OrderID, ReceivedBy, ReceivedDateTime) VALUES (" + OrderID + ", '" + ReceivedBy + "', '" + DateTime + "');";

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

        public int InsertOrderReceivingDocument(Int64 OrderID, string DocumentType, string DocumentNo, string DocumentName, string DocumentPath)
        {
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "INSERT INTO OrderDocumentReceiving (OrderID, DocumentType, DocumentNo, DocumentName, DocumentPath) VALUES (" + OrderID + ", '" + DocumentType + "', '" + DocumentNo + "', '" + DocumentName + "', '" + DocumentPath + "');";

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

        public int InsertOrderDamage(Int64 OrderID, string ItemName, string DamageType, Int64 DamageCost, string DamageCause, string DocumentName, string DocumentPath)
        {
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "INSERT INTO OrderDamage (OrderID, ItemName, DamageType, DamageCost, DamageCause, DamageDocumentName, DamageDocumentPath) VALUES (" + OrderID + ", '" + ItemName + "', '" + DamageType + "', " + DamageCost + ", '" + DamageCause + "', '" + DocumentName + "', '" + DocumentPath + "');";

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

        public int UpdateOrderVehicle(Int64 OrderVehicleID, string VehicleType, string VehicleRegNo, Int64 VehicleContactNo, Int64 BrokerID, string DriverName, string FatherName, Int64 DriverNIC, string DriverLicence, Int64 DriverCellNo)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE OrderVehicle SET VehicleType = '" + VehicleType + "', VehicleRegNo = '" + VehicleRegNo + "', VehicleContactNo = " + VehicleContactNo + ", BrokerID = " + BrokerID + ", DriverName = '" + DriverName + "', FatherName = '" + FatherName + "', DriverNIC = " + DriverNIC + ", DriverLicence = '" + DriverLicence + "', DriverCellNo = " + DriverCellNo + " WHERE OrderVehicleID = " + OrderVehicleID;


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

        public int DeleteOrderVehicle(Int64 OrderVehicleID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "DELETE FROM OrderVehicle WHERE OrderVehicleID = " + OrderVehicleID;

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
