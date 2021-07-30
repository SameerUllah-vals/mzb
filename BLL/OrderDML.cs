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

        #region Bilty
        public DataTable GetBilties(string Keyword, string DateFilters)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                // _commnadData.CommandText = @"SELECT o.*, ISNULL(o.IsInvoiced, 0) AS IsInvoiced, sc.CompanyName as 'SenderCompany', rc.CompanyName as 'ReceiverCompany', bc.CompanyName as 'BillToCustomerCompany', (SELECT COUNT(*) FROM OrderVehicle ov WHERE ov.OrderID = o.OrderID) as Vehicles,  (SELECT COUNT(*) FROM OrderConsignment oc WHERE oc.OrderID = o.OrderID) as Containers,  (SELECT COUNT(*) FROM OrderProduct op WHERE op.OrderID = o.OrderID) as Products, (SELECT COUNT(*) FROM OrderConsignmentReceiving ocr WHERE ocr.OrderID = o.OrderID) as Recievings, (SELECT COUNT(*) FROM OrderDocumentReceiving odr WHERE odr.OrderID = o.OrderID) as 'RecievingDocs', (SELECT COUNT(*) FROM OrderDamage od WHERE od.OrderID = o.OrderID) as Damages, (SELECT COUNT(AdvanceAmount) FROM OrderAdvances oa WHERE oa.OrderID = o.OrderID) as Advances FROM [Order] o INNER JOIN Company sc ON sc.CompanyID = o.SenderCompanyID INNER JOIN Company rc ON rc.CompanyID = o.ReceiverCompanyID INNER JOIN Company bc ON bc.CompanyID = o.CustomerCompanyID WHERE (bc.CompanyName LIKE '%" + Keyword + "%' OR rc.CompanyName LIKE '%" + Keyword + "%' OR sc.CompanyName LIKE '%" + Keyword + "%') " + DateFilters + " ORDER BY o.DateCreated DESC"; 
                _commnadData.CommandText = @"SELECT 
                                                o.*, 
                                                ISNULL(o.IsInvoiced, 0) AS IsInvoiced,
                                                sc.CompanyName as 'SenderCompany', 
	                                            rc.CompanyName as 'ReceiverCompany',
                                                bc.CompanyName as 'BillToCustomerCompany',sl.Name as 'ShippingName',
	                                            (SELECT COUNT(*) FROM OrderVehicle ov WHERE ov.OrderID = o.OrderID) as Vehicles, 
	                                            (SELECT COUNT(*) FROM OrderConsignment oc WHERE oc.OrderID = o.OrderID) as Containers, 
	                                            (SELECT COUNT(*) FROM OrderProduct op WHERE op.OrderID = o.OrderID) as Products, 
	                                            (SELECT COUNT(*) FROM OrderConsignmentReceiving ocr WHERE ocr.OrderID = o.OrderID) as Recievings, 
	                                            (SELECT COUNT(*) FROM OrderDocumentReceiving odr WHERE odr.OrderID = o.OrderID) as 'RecievingDocs', 
	                                            (SELECT COUNT(*) FROM OrderDamage od WHERE od.OrderID = o.OrderID) as Damages, 
                                                (SELECT COUNT(AdvanceAmount) FROM OrderAdvances oa WHERE oa.OrderID = o.OrderID) as Advances
                                            FROM[Order] o
                                                INNER JOIN Company sc ON sc.CompanyID = o.SenderCompanyID
                                                
                                                INNER JOIN Company rc ON rc.CompanyID = o.ReceiverCompanyID
                                                INNER JOIN Company bc ON bc.CompanyID = o.CustomerCompanyID

                                                INNER JOIN ShippingLine sl ON sl.ShippingLineID = o.ShippingLineID
                                                WHERE (bc.CompanyName LIKE '%" + Keyword + "%' OR rc.CompanyName LIKE '%" + Keyword + "%' OR sc.CompanyName LIKE '%" + Keyword + "%' OR sl.Name LIKE '%" + Keyword + "%' OR o.OrderNo LIKE '%" + Keyword + "%') " + DateFilters + " ORDER BY o.DateCreated DESC";
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
        public DataTable GetBiltiesNew(string Keyword, string DateFilters)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                // _commnadData.CommandText = @"SELECT o.*, ISNULL(o.IsInvoiced, 0) AS IsInvoiced, sc.CompanyName as 'SenderCompany', rc.CompanyName as 'ReceiverCompany', bc.CompanyName as 'BillToCustomerCompany', (SELECT COUNT(*) FROM OrderVehicle ov WHERE ov.OrderID = o.OrderID) as Vehicles,  (SELECT COUNT(*) FROM OrderConsignment oc WHERE oc.OrderID = o.OrderID) as Containers,  (SELECT COUNT(*) FROM OrderProduct op WHERE op.OrderID = o.OrderID) as Products, (SELECT COUNT(*) FROM OrderConsignmentReceiving ocr WHERE ocr.OrderID = o.OrderID) as Recievings, (SELECT COUNT(*) FROM OrderDocumentReceiving odr WHERE odr.OrderID = o.OrderID) as 'RecievingDocs', (SELECT COUNT(*) FROM OrderDamage od WHERE od.OrderID = o.OrderID) as Damages, (SELECT COUNT(AdvanceAmount) FROM OrderAdvances oa WHERE oa.OrderID = o.OrderID) as Advances FROM [Order] o INNER JOIN Company sc ON sc.CompanyID = o.SenderCompanyID INNER JOIN Company rc ON rc.CompanyID = o.ReceiverCompanyID INNER JOIN Company bc ON bc.CompanyID = o.CustomerCompanyID WHERE (bc.CompanyName LIKE '%" + Keyword + "%' OR rc.CompanyName LIKE '%" + Keyword + "%' OR sc.CompanyName LIKE '%" + Keyword + "%') " + DateFilters + " ORDER BY o.DateCreated DESC"; 
                _commnadData.CommandText = @"SELECT 
                                                o.*, 
                                                ISNULL(o.IsInvoiced, 0) AS IsInvoiced,
                                                sc.CompanyName as 'SenderCompany', 
	                                            rc.CompanyName as 'ReceiverCompany',
                                                bc.CompanyName as 'BillToCustomerCompany',sl.Name as 'ShippingName',
	                                            (SELECT COUNT(*) FROM OrderVehicle ov WHERE ov.OrderID = o.OrderID) as Vehicles, 
	                                            (SELECT COUNT(*) FROM OrderConsignment oc WHERE oc.OrderID = o.OrderID) as Containers, 
	                                            (SELECT COUNT(*) FROM OrderProduct op WHERE op.OrderID = o.OrderID) as Products, 
	                                            (SELECT COUNT(*) FROM OrderConsignmentReceiving ocr WHERE ocr.OrderID = o.OrderID) as Recievings, 
	                                            (SELECT COUNT(*) FROM OrderDocumentReceiving odr WHERE odr.OrderID = o.OrderID) as 'RecievingDocs', 
	                                            (SELECT COUNT(*) FROM OrderDamage od WHERE od.OrderID = o.OrderID) as Damages, 
                                                (SELECT COUNT(AdvanceAmount) FROM OrderAdvances oa WHERE oa.OrderID = o.OrderID) as Advances
                                            FROM[Order] o
                                                INNER JOIN Company sc ON sc.CompanyID = o.SenderCompanyID                                                
                                                INNER JOIN Company rc ON rc.CompanyID = o.ReceiverCompanyID
                                                INNER JOIN Company bc ON bc.CompanyID = o.CustomerCompanyID
                                                INNER JOIN ShippingLine sl ON sl.ShippingLineID = o.ShippingLineID
												left Join OrderVehicle ov ON ov.OrderID = o.OrderID
                                                left JOIN Brokers brokers ON brokers.ID = ov.BrokerID
	                                            left JOIN OrderConsignment oc ON oc.OrderID = o.OrderID
                                                left JOIN OrderProduct op ON op.OrderID = o.OrderID
												left JOIN OrderDamage od ON od.OrderID = o.OrderID
                                                left join OrderAdvances oa ON oa.OrderID = o.OrderID
                                                WHERE (ov.VehicleRegNo LIKE '%" + Keyword + "%' OR ov.DriverCellNo LIKE '%" + Keyword + "%' OR ov.DriverName LIKE '%" + Keyword + "%' OR brokers.Name LIKE '%" + Keyword + "%' OR ov.VehicleContactNo LIKE '%" + Keyword + "%' OR ov.Rate LIKE '%" + Keyword + "%' OR oc.ContainerNo LIKE '%" + Keyword + "%' OR oc.ContainerWeight LIKE '%" + Keyword + "%' OR oc.Rate LIKE '%" + Keyword + "%' OR oa.ReceivedFrom LIKE '%" + Keyword + "%' OR oa.AdvanceAmount LIKE '%" + Keyword + "%' OR oa.AdvancePlace LIKE '%" + Keyword + "%' OR oa.AdvanceID LIKE '%" + Keyword + "%' OR op.PackageType LIKE '%" + Keyword + "%' OR od.DamageDocumentName LIKE '%" + Keyword + "%' OR od.DamageType LIKE '%" + Keyword + "%' OR od.ItemName LIKE '%" + Keyword + "%'  OR  bc.CompanyName LIKE '%" + Keyword + "%' OR rc.CompanyName LIKE '%" + Keyword + "%' OR sc.CompanyName LIKE '%" + Keyword + "%' OR sl.Name LIKE '%" + Keyword + "%' OR o.OrderNo LIKE '%" + Keyword + "%') " + DateFilters + " ORDER BY o.DateCreated DESC";
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
                                                ISNULL(o.IsInvoiced, 0) AS IsInvoiced, 
	                                            sc.CompanyName as 'SenderCompany', 
	                                            rc.CompanyName as 'ReceiverCompany',
                                                bc.CompanyName as 'BillToCustomerCompany',sl.Name as 'ShippingName',
	                                            (SELECT COUNT(*) FROM OrderVehicle ov WHERE ov.OrderID = o.OrderID) as Vehicles, 
	                                            (SELECT COUNT(*) FROM OrderConsignment oc WHERE oc.OrderID = o.OrderID) as Containers, 
	                                            (SELECT COUNT(*) FROM OrderProduct op WHERE op.OrderID = o.OrderID) as Products, 
	                                            (SELECT COUNT(*) FROM OrderConsignmentReceiving ocr WHERE ocr.OrderID = o.OrderID) as Recievings, 
	                                            (SELECT COUNT(*) FROM OrderDocumentReceiving odr WHERE odr.OrderID = o.OrderID) as 'RecievingDocs', 
	                                            (SELECT COUNT(*) FROM OrderDamage od WHERE od.OrderID = o.OrderID) as Damages, 
                                                (SELECT COUNT(AdvanceAmount) FROM OrderAdvances oa WHERE oa.OrderID = o.OrderID) as Advances
                                            FROM [Order] o 
	                                            INNER JOIN Company sc ON sc.CompanyID = o.SenderCompanyID
	                                            INNER JOIN Company rc ON rc.CompanyID = o.ReceiverCompanyID
                                                INNER JOIN Company bc ON bc.CompanyID = o.CustomerCompanyID
												INNER JOIN ShippingLine sl ON sl.ShippingLineID=o.ShippingLineID
                                            ORDER BY o.DateCreated DESC";

                //_commnadData.CommandText = @"SELECT 
                //                                o.*, 
	               //                             sc.CompanyName as 'SenderCompany', 
	               //                             rc.CompanyName as 'ReceiverCompany',
	               //                             (SELECT COUNT(*) FROM OrderVehicle ov WHERE ov.OrderID = o.OrderID) as Vehicles, 
	               //                             (SELECT COUNT(*) FROM OrderConsignment oc WHERE oc.OrderID = o.OrderID) as Containers, 
	               //                             (SELECT COUNT(*) FROM OrderProduct op WHERE op.OrderID = o.OrderID) as Products, 
	               //                             (SELECT COUNT(*) FROM OrderConsignmentReceiving ocr WHERE ocr.OrderID = o.OrderID) as Recievings, 
	               //                             (SELECT COUNT(*) FROM OrderDocumentReceiving odr WHERE odr.OrderID = o.OrderID) as 'RecievingDocs', 
	               //                             (SELECT COUNT(*) FROM OrderDamage od WHERE od.OrderID = o.OrderID) as Damages
                //                            FROM[Order] o 
	               //                             INNER JOIN PickDropLocation pl ON pl.PickDropID = o.PickupLocationID
	               //                             INNER JOIN PickDropLocation dl ON dl.PickDropID = o.DropoffLocationID
	               //                             INNER JOIN Company sc ON sc.CompanyID = o.SenderCompanyID
	               //                             INNER JOIN Company rc ON rc.CompanyID = o.ReceiverCompanyID
                //                            ORDER BY o.RecordedDate DESC";




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

        public DataTable GetBilty(Int64 OrderID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;

                _commnadData.CommandText = @"SELECT 
                                                o.*, 
                                                ISNULL(o.IsInvoiced, 0) AS IsInvoiced, 
	                                            sc.CompanyName as 'SenderCompany', 
	                                            rc.CompanyName as 'ReceiverCompany',
                                                bc.CompanyName as 'BillToCustomerCompany',
	                                            (SELECT COUNT(*) FROM OrderVehicle ov WHERE ov.OrderID = o.OrderID) as Vehicles, 
	                                            (SELECT COUNT(*) FROM OrderConsignment oc WHERE oc.OrderID = o.OrderID) as Containers, 
	                                            (SELECT COUNT(*) FROM OrderProduct op WHERE op.OrderID = o.OrderID) as Products, 
	                                            (SELECT COUNT(*) FROM OrderConsignmentReceiving ocr WHERE ocr.OrderID = o.OrderID) as Recievings, 
	                                            (SELECT COUNT(*) FROM OrderDocumentReceiving odr WHERE odr.OrderID = o.OrderID) as 'RecievingDocs', 
	                                            (SELECT COUNT(*) FROM OrderDamage od WHERE od.OrderID = o.OrderID) as Damages, 
                                                (SELECT COUNT(AdvanceAmount) FROM OrderAdvances oa WHERE oa.OrderID = o.OrderID) as Advances
                                            FROM[Order] o 
	                                            INNER JOIN Company sc ON sc.CompanyID = o.SenderCompanyID
	                                            INNER JOIN Company rc ON rc.CompanyID = o.ReceiverCompanyID
                                                INNER JOIN Company bc ON bc.CompanyID = o.CustomerCompanyID
                                            WHERE o.OrderID = @OrderID
                                            ORDER BY o.DateCreated DESC";
                _commnadData.AddParameter("@OrderID", OrderID);




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

        public DataTable GetBillToCustomerByBilty(Int64 OrderID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;

                _commnadData.CommandText = @"SELECT 
                                                o.*, 
	                                            c.CompanyName as 'CustomerCompany', 
	                                            c.CompanyCode as 'CustomerCompanyCode'
                                            FROM[Order] o 
	                                            INNER JOIN Company c ON c.CompanyID = o.CustomerCompanyID
                                            WHERE o.OrderID = @OrderID";
                _commnadData.AddParameter("@OrderID", OrderID);




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

        public DataTable GetBiltyByContainerNo(string ContainerNo)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;

                _commnadData.CommandText = @"SELECT 
                                                o.*
                                            FROM [OrderConsignment] oc 
	                                            INNER JOIN [Order] o ON o.OrderID = oc.OrderID
                                            WHERE oc.ContainerNo = @ContainerNo";
                _commnadData.AddParameter("@ContainerNo", ContainerNo);




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



        //public int InsertBiltyOrder(Int64 OrderNo, string BiltyDate, string SenderGroup, string SenderCompany, string SenderDepartment, string ReceiverGroup,
        //    string ReceiverCompany, string ReceiverDepartment, string CustomerGroup, string CustomerCompany, string CustomerDepartment, string PaymentType,
        //    string PickCity, string PickRegion, string PickArea, string PickAddress, string DropCity, string DropRegion, string DropArea, string DropAddress,
        //    string ClearingAgent, double AdditionalWeight, double ActualWeight, double BiltyFreight, double Freight, double PartyCommission, double AdvanceFreight,
        //    double FactoryAdvance, double DieselAdvance, double AdvanceAmount, double TotalAdvance, double BalanceFreight, Int64 CreatedByID)
        //{
        //    CommandData commandData = new CommandData();

        //    try
        //    {
        //        commandData._CommandType = CommandType.Text;
        //        commandData.CommandText = "INSERT INTO [Order] (OrderNo, Date, RecordedDate, SenderGroup, SenderCompany, SenderDepartment, ReceiverGroup, ReceiverCompany, ReceiverDepartment, CustomerGroup, CustomerCompany, CustomerDepartment, PaymentType, PickupCity, PickupRegion, PickupArea, PickupAddress, DropoffCity, DropoffRegion, DropoffArea, DropoffAddress, ClearingAgent, AdditionalWeight, ActualWeight, BiltyFreight, Freight, PartyCommission, AdvanceFreight, FactoryAdvance, DieselAdvance, AdvanceAmount, TotalAdvance, BalanceFreight, CreatedByID, DateCreated) VALUES('" + OrderNo + "', '" + BiltyDate + "', GETDATE(), '" + SenderGroup + "', '" + SenderCompany + "', '" + SenderDepartment + "', '" + ReceiverGroup + "', '" + ReceiverCompany + "', '" + ReceiverDepartment + "', '" + CustomerGroup + "', '" + CustomerCompany + "', '" + CustomerDepartment + "', '" + PaymentType + "', '" + PickCity + "', '" + PickRegion + "', '" + PickArea + "', '" + PickAddress + "', '" + DropCity + "', '" + DropRegion + "', '" + DropArea + "', '" + DropAddress + "', '" + ClearingAgent + "', " + AdditionalWeight + ", " + ActualWeight + ", " + BiltyFreight + ", " + Freight + ", " + PartyCommission + ", " + AdvanceFreight + ", " + FactoryAdvance + ", " + DieselAdvance + ", " + AdvanceAmount + ", " + TotalAdvance + ", " + BalanceFreight + ", " + CreatedByID + ", GETDATE()); SELECT SCOPE_IDENTITY();";

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

        public int InsertBiltyOrder_OLD(Int64 OrderNo, string BiltyDate, Int64 SenderCompanyID, string SenderDepartment, Int64 ReceiverCompanyID, string ReceiverDepartment,
            Int64 CustomerCompanyID, string CustomerDepartment, string PaymentType, Int64 PickupLocationID, Int64 DropoffLocationID, string ClearingAgent, double AdditionalWeight,
            double ActualWeight, double BiltyFreight, double Freight, double PartyCommission, double AdvanceFreight, double FactoryAdvance, double DieselAdvance, double AdvanceAmount,
            double TotalAdvance, double BalanceFreight, Int64 CreatedByID)
        {
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "INSERT INTO [Order] (OrderNo, Date, RecordedDate, SenderCompanyID, SenderDepartment, ReceiverCompanyID, ReceiverDepartment, CustomerCompanyID, CustomerDepartment, PaymentType, PickupLocationID, DropoffLocationID, ClearingAgent, AdditionalWeight, ActualWeight, BiltyFreight, Freight, PartyCommission, AdvanceFreight, FactoryAdvance, DieselAdvance, AdvanceAmount, TotalAdvance, BalanceFreight, CreatedByID, DateCreated) VALUES((MAX(CONVERT(bigint, OrderNo)) + 1), '" + BiltyDate + "', GETDATE(), " + SenderCompanyID + ", '" + SenderDepartment + "', " + ReceiverCompanyID + ", '" + ReceiverDepartment + "', " + CustomerCompanyID + ", '" + CustomerDepartment + "', '" + PaymentType + "', " + PickupLocationID + ", '" + DropoffLocationID + "', '" + ClearingAgent + "', " + AdditionalWeight + ", " + ActualWeight + ", " + BiltyFreight + ", " + Freight + ", " + PartyCommission + ", " + AdvanceFreight + ", " + FactoryAdvance + ", " + DieselAdvance + ", " + AdvanceAmount + ", " + TotalAdvance + ", " + BalanceFreight + ", " + CreatedByID + ", GETDATE()); SELECT SCOPE_IDENTITY();";

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

        public int InsertBiltyOrder(Int64 OrderNo, string BiltyDate, string PaidToPay, Int64 SenderCompanyID, string SenderDepartment, Int64 ReceiverCompanyID, string ReceiverDepartment,
            Int64 CustomerCompanyID, string CustomerDepartment, string PaymentType, string ClearingAgent, double AdditionalWeight,
            double ActualWeight, double BiltyFreight, double Freight, double PartyCommission, double BalanceFreight,string LoadingDate, Int64 ShippingLineID, Int64 CreatedByID)
        {
            CommandData commandData = new CommandData();
            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "INSERT INTO [Order] (OrderNo, Date, RecordedDate, PaidToPay, SenderCompanyID, SenderDepartment, ReceiverCompanyID, ReceiverDepartment, CustomerCompanyID, CustomerDepartment, PaymentType, ClearingAgent, AdditionalWeight, ActualWeight, BiltyFreight, Freight, PartyCommission, BalanceFreight,LoadingDate, ShippingLineID, CreatedByID, DateCreated) VALUES((SELECT ISNULL(MAX(CONVERT(bigint, OrderNo)), 0) + 1 FROM [Order]), '" + BiltyDate + "', " + (BiltyDate == string.Empty ? "GETDATE()" : "'" + BiltyDate + "'") + ", '" + PaidToPay + "', " + SenderCompanyID + ", '" + SenderDepartment + "', " + ReceiverCompanyID + ", '" + ReceiverDepartment + "', " + CustomerCompanyID + ", '" + CustomerDepartment + "', '" + PaymentType + "', '" + ClearingAgent + "', " + AdditionalWeight + ", " + ActualWeight + ", " + BiltyFreight + ", " + Freight + ", " + PartyCommission + ", " + BalanceFreight + ",'"+LoadingDate+"', " + ShippingLineID + ", " + CreatedByID + ", GETDATE()); SELECT SCOPE_IDENTITY();";

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

        public int UpdateOrder(Int64 OrderID, Int64 OrderNo, string BiltyDate, Int64 SenderCompanyID, string SenderDepartment, Int64 ReceiverCompanyID, string ReceiverDepartment,
            Int64 CustomerCompanyID, string CustomerDepartment, string PaymentType, Int64 PickupLocationID, Int64 DropoffLocationID,string LoadingDate, Int64 ShippingLineID, double AdditionalWeight,
            double ActualWeight, double BiltyFreight, double Freight, double PartyCommission, double AdvanceFreight, double FactoryAdvance, double DieselAdvance, double AdvanceAmount,
            double TotalAdvance, double BalanceFreight, Int64 ModifiedByID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE [Order] SET OrderNo = '" + OrderNo + "', Date = '" + BiltyDate + "', SenderCompanyID = " + SenderCompanyID + ", SenderDepartment = '" + SenderDepartment + "', ReceiverCompanyID = " + ReceiverCompanyID + ", ReceiverDepartment = '" + ReceiverDepartment + "', CustomerCompanyID = " + CustomerCompanyID + ", CustomerDepartment = '" + CustomerDepartment + "', PaymentType = '" + PaymentType + "', PickupLocationID = " + PickupLocationID + ", DropoffLocationID = " + DropoffLocationID + ",LoadingDate='"+LoadingDate+"', ShippingLineID = " + ShippingLineID + ", AdditionalWeight = " + AdditionalWeight + ", ActualWeight = " + ActualWeight + ", BiltyFreight = " + BiltyFreight + ", Freight = " + BiltyFreight + ", PartyCommission = " + PartyCommission + ", AdvanceFreight = " + AdvanceFreight + ", FactoryAdvance = " + FactoryAdvance + ", DieselAdvance = " + DieselAdvance + ", AdvanceAmount = " + AdvanceAmount + ", TotalAdvance = " + TotalAdvance + ", BalanceFreight = " + BalanceFreight + ", ModifiedByID = " + ModifiedByID + ", DateModified = GETDATE() WHERE OrderID = " + OrderID;



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
        public DataTable GetPatrolPump(Int64 ID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "select * from PatrolPumps WHERE PatrolPumpID = '" + ID + "' ORDER BY Name ASC";

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



        #endregion

        #region Order Vehicle

        public DataTable GetBiltyVehiclesByOrder(Int64 OrderID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = @"SELECT *, o.CustomerCompanyID, (SELECT Name FROM Brokers WHERE ID = ov.BrokerID) AS Broker, (SELECT CompanyName FROM Company WHERE o.ReceiverCompanyID = CompanyID) AS Receiver FROM OrderVehicle ov INNER JOIN [Order] o ON o.OrderID = ov.OrderID WHERE ov.OrderID = " + OrderID;

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
                _commnadData.CommandText = @"SELECT ov.* FROM OrderVehicle ov INNER JOIN [Order] o ON o.OrderID = ov.OrderID WHERE ov.OrderVehicleID = " + OrderVehicleID;

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

        public DataTable GetUncompleteVehicles(string VehicleRegNo)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = @"SELECT ov.*, ct.Size AS ContainerSize, vt.VehicleSize AS VehicleSize FROM OrderVehicle ov INNER JOIN [Order] o ON o.OrderID = ov.OrderID INNER JOIN OrderConsignment oc ON oc.OrderID = ov.OrderID INNER JOIN ContainerType ct ON ct.ContainerTypeID = oc.ContainerType INNER JOIN VehicleType vt ON vt.VehicleTypeName = ov.VehicleType WHERE ov.VehicleRegNo = '" + VehicleRegNo + "' AND ov.Status <> 'Complete'";

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

        public DataTable GetOrderVehiclesFromOrderContainerID(Int64 ContainerID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = @"SELECT oc.*, ov.VehicleRegNo, ov.OrderVehicleID FROM OrderConsignment oc INNER JOIN OrderVehicle ov ON ov.OrderID = oc.OrderID WHERE OrderConsignmentID = " + ContainerID;

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

        public DataTable GetAssignedVehicleInOrder(Int64 OrderID, string VehicleRegno)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = @"SELECT * FROM OrderConsignment WHERE OrderID = " + OrderID + " AND AssignedVehicle = '" + VehicleRegno + "'";

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


        //public DataTable GetAssignedVehicleInOrder(Int64 OrderID, string VehicleRegno)
        //{
        //    //Creating object of DAL class
        //    CommandData _commnadData = new CommandData();

        //    try
        //    {
        //        _commnadData._CommandType = CommandType.Text;
        //        _commnadData.CommandText = @"SELECT * FROM OrderConsignment WHERE OrderID = " + OrderID + " AND AssignedVehicle = '" + VehicleRegno + "'";

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

        public int InsertOrderVehicleInfo(Int64 OrderID, string VehicleType, string VehicleRegNo, Int64 VehicleContactNo, Int64 BrokerID, string DriverName, string FatherName, Int64 DriverNIC, string DriverLicence, Int64 DriverCellNo, string ReportingTime, string InTime, string OutTime, double VehicleRate)
        {
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "INSERT INTO OrderVehicle (OrderID, VehicleType, VehicleRegNo, VehicleContactNo, BrokerID, DriverName, FatherName, DriverNIC, DriverLicence, DriverCellNo, ReportingTime, InTime, OutTime, Rate, Status) VALUES(" + OrderID + ", '" + VehicleType + "', '" + VehicleRegNo + "', " + VehicleContactNo + ", " + BrokerID + ", '" + DriverName + "', '" + FatherName + "', " + DriverNIC + ", '" + DriverLicence + "', " + DriverCellNo + ", '" + ReportingTime + "', '" + InTime + "', '" + OutTime + "', " + VehicleRate + ", ''); SELECT SCOPE_IDENTITY();";

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

        public int UpdateOrderVehicle(Int64 OrderVehicleID, string VehicleType, string VehicleRegNo, Int64 VehicleContactNo, Int64 BrokerID, double Rate, string DriverName, string FatherName, Int64 DriverNIC, string DriverLicence, Int64 DriverCellNo)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE OrderVehicle SET VehicleType = '" + VehicleType + "', VehicleRegNo = '" + VehicleRegNo + "', VehicleContactNo = " + VehicleContactNo + ", BrokerID = " + BrokerID + ", DriverName = '" + DriverName + "', FatherName = '" + FatherName + "', DriverNIC = " + DriverNIC + ", DriverLicence = '" + DriverLicence + "', DriverCellNo = " + DriverCellNo + ", Rate = " + Rate + " WHERE OrderVehicleID = " + OrderVehicleID;


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

        public int CompleteOrderVehicle(Int64 OrderVehicleID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE OrderVehicle SET Status = 'Complete' WHERE OrderVehicleID = " + OrderVehicleID;


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

        #endregion

        #region Order Container

        public DataTable GetBiltyContainersByOrder(Int64 OrderID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = @"SELECT oc.*, ISNULL(IsBilled, 0) as Billed, ISNULL(Status, 0) AS ContainerStatus, ct.ContainerType AS ContainerTypeName, (SELECT COUNT(*) FROM ContainerExpenses WHERE ContainerID = oc.OrderConsignmentID) AS Expenses, (SELECT OrderNo FROM [Order] WHERE OrderID = oc.OrderID) AS OrderNo, (SELECT PaidToPay FROM [Order] WHERE OrderID = oc.OrderID) AS PaidToPay FROM OrderConsignment oc INNER JOIN ContainerType ct ON ct.ContainerTypeID = oc.ContainerType WHERE oc.OrderID = " + OrderID;

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

        public DataTable GetBiltyContainersByOrderNo(string OrderNo)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = @"SELECT * FROM OrderConsignment WHERE OrderID = (SELECT OrderID FROM [Order] WHERE OrderNo = '" + OrderNo + "')";

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

        public DataTable GetOrderContainerByOrderID(Int64 OrderID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                //_commnadData.CommandText = @"SELECT ct.Size AS 'ContainerSize', oc.ContainerNo, ISNULL(oc.ContainerWeight, 0) AS ContainerWeight,  	b.Name AS Broker, sc.CompanyName as Sender, rc.CompanyName AS Receiver, bc.CompanyName as BillTo, ov.VehicleRegNo, oc.EmptyContainerDropLocation, oc.EmptyContainerPickLocation, o.*, oc.OrderConsignmentID, 	ov.OrderVehicleID, p.PackageType, p.Item, ISNULL(p.Qty, 0) AS ProdQty, ISNULL(p.TotalWeight, 0) AS ProductWeight  FROM [Order] o INNER JOIN OrderConsignment oc ON o.OrderID = oc.OrderID INNER JOIN ContainerType ct ON ct.ContainerTypeID = oc.ContainerType INNER JOIN OrderVehicle ov ON ov.OrderID = o.OrderID INNER JOIN Company sc ON sc.CompanyID = o.SenderCompanyID INNER JOIN Company rc ON rc.CompanyID = o.ReceiverCompanyID INNER JOIN Company bc ON bc.CompanyID = o.CustomerCompanyID INNER JOIN Brokers b ON b.ID = ov.BrokerID LEFT JOIN OrderProduct p ON o.OrderID = p.OrderID WHERE o.OrderID = " + OrderID;
                _commnadData.CommandText = @"SELECT ct.Size AS 'ContainerSize', oc.ContainerNo, ISNULL(oc.ContainerWeight, 0) AS ContainerWeight,  	b.Name AS Broker, sc.CompanyName as Sender, rc.CompanyName AS Receiver, bc.CompanyName as BillTo, ov.VehicleRegNo, oc.EmptyContainerDropLocation, oc.EmptyContainerPickLocation, o.*, oc.OrderConsignmentID, 	ov.OrderVehicleID, oc.Rate FROM [Order] o INNER JOIN OrderConsignment oc ON o.OrderID = oc.OrderID INNER JOIN ContainerType ct ON ct.ContainerTypeID = oc.ContainerType INNER JOIN OrderVehicle ov ON ov.OrderID = o.OrderID INNER JOIN Company sc ON sc.CompanyID = o.SenderCompanyID INNER JOIN Company rc ON rc.CompanyID = o.ReceiverCompanyID INNER JOIN Company bc ON bc.CompanyID = o.CustomerCompanyID INNER JOIN Brokers b ON b.ID = ov.BrokerID WHERE o.OrderID = " + OrderID;

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

        public DataTable GetBiltyContainers(Int64 OrderConsignmentID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = @"SELECT oc.*, (SELECT OrderNo FROM [Order] WHERE OrderID = oc.OrderID) as OrderNo FROM OrderConsignment oc WHERE OrderConsignmentID = " + OrderConsignmentID;

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

        public int InsertOrderContainerInfo(Int64 OrderID, Int64 ContainerType, string ContainerNo, double ContainerWeight, Int64 PickupLocationID, string EmptyContainerPickLocation, Int64 DropoffLocationID, string EmptyContainerDropLocation, string Remarks, string AssignedVehicle, double ContainerRate)
        {
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "INSERT INTO OrderConsignment (OrderID, ContainerType, ContainerNo, ContainerWeight, PickupLocationID, EmptyContainerPickLocation, DropoffLocationID, EmptyContainerDropLocation, Remarks, AssignedVehicle, Rate, PaymentStatus, Status) VALUES(" + OrderID + ", " + ContainerType + ", '" + ContainerNo + "', " + ContainerWeight + ", " + PickupLocationID + ", '" + EmptyContainerPickLocation + "', " + DropoffLocationID + ", '" + EmptyContainerDropLocation + "', '" + Remarks + "', '" + AssignedVehicle + "', " + ContainerRate + ", 0, 0); SELECT SCOPE_IDENTITY();";

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

        //public int UpdateOrderContainerInfo(Int64 OrderConsignemntID, Int64 OrderID, Int64 ContainerType, string ContainerNo, double ContainerWeight, Int64 PickuplocationID, string EmptyContainerPickLocation, Int64 DropofflocationID, string EmptyContainerDropLocation, string Remarks, string AssignedVehicle)
        //{
        //    //Creating object of DAL class
        //    CommandData commandData = new CommandData();

        //    try
        //    {
        //        commandData._CommandType = CommandType.Text;
        //        commandData.CommandText = "UPDATE OrderConsignment SET ContainerType = " + ContainerType + ", ContainerNo = '" + ContainerNo + "', ContainerWeight = " + ContainerWeight + ", PickupLocationID = " + PickuplocationID + ", EmptyContainerPickLocation = '" + EmptyContainerPickLocation + "', DropoffLocationid = " + DropofflocationID + ", EmptyContainerDropLocation = '" + EmptyContainerDropLocation + "', Remarks = '" + Remarks + "', AssignedVehicle = '" + AssignedVehicle + "' WHERE OrderConsignmentID = " + OrderConsignemntID;


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



        //public int UpdateContainerReceivingStatus(Int64 OrderConsignemntID, int Status)
        //{
        //    //Creating object of DAL class
        //    CommandData commandData = new CommandData();

        //    try
        //    {
        //        commandData._CommandType = CommandType.Text;
        //        commandData.CommandText = "UPDATE OrderConsignment SET [Status] = " + Status + " WHERE OrderConsignmentID = " + OrderConsignemntID;


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

        public int UpdateOrderContainerInfo(Int64 OrderConsignemntID, Int64 OrderID, Int64 ContainerType, string ContainerNo, double ContainerWeight, Int64 PickuplocationID, string EmptyContainerPickLocation, Int64 DropofflocationID, string EmptyContainerDropLocation, string Remarks, string AssignedVehicle, double ContainerRate)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE OrderConsignment SET ContainerType = " + ContainerType + ", ContainerNo = '" + ContainerNo + "', ContainerWeight = " + ContainerWeight + ", PickupLocationID = " + PickuplocationID + ", EmptyContainerPickLocation = '" + EmptyContainerPickLocation + "', DropoffLocationid = " + DropofflocationID + ", EmptyContainerDropLocation = '" + EmptyContainerDropLocation + "', Remarks = '" + Remarks + "', AssignedVehicle = '" + AssignedVehicle + "', Rate = " + ContainerRate + " WHERE OrderConsignmentID = " + OrderConsignemntID;


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

        public int UpdateAssignedVehiclesOfContainer(Int64 OrderID, string VehicleRegNo)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE OrderConsignment SET AssignedVehicle = '" + VehicleRegNo + "' WHERE OrderID = " + OrderID;


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

        public int UpdateContainerReceivingStatus(Int64 OrderConsignemntID, int Status, string ReceivedBy, string ReceivedDateTime)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                //commandData.CommandText = "UPDATE OrderConsignment SET [Status] = " + Status + ", ReceivedBy = '" + ReceivedBy + "', ReceivedDateTime = '" + ReceivedDateTime + "', WeighmentCharges = " + Weighment + " WHERE OrderConsignmentID = " + OrderConsignemntID;
                commandData.CommandText = "UPDATE OrderConsignment SET [Status] = " + Status + ", ReceivedBy = '" + ReceivedBy + "', ReceivedDateTime = '" + ReceivedDateTime + "' WHERE OrderConsignmentID = " + OrderConsignemntID;


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

        public int CompleteVehicleStatus(string OrderNo)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                //commandData.CommandText = "UPDATE OrderConsignment SET [Status] = " + Status + ", ReceivedBy = '" + ReceivedBy + "', ReceivedDateTime = '" + ReceivedDateTime + "', WeighmentCharges = " + Weighment + " WHERE OrderConsignmentID = " + OrderConsignemntID;
                commandData.CommandText = "UPDATE OrderVehicle SET Status = 'Completed' WHERE OrderID = (SELECT OrderID FROM [Order] WHERE OrderNo = " + OrderNo;


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

        public int DeleteOrderContainer(Int64 OrderContainerID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "DELETE FROM OrderConsignment WHERE OrderConsignmentID = " + OrderContainerID;

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

        #endregion

        #region Order Invoices

        public DataTable GetInvoicedorder(Int64 OrderID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = @"SELECT * FROM OrderInvoices oi INNER JOIN [Order] o ON o.OrderID = oi.OrderID WHERE oi.OrderID = " + OrderID;

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

        public DataTable GetInvoices(string SortState)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                //_commnadData.CommandText = @"SELECT ct.Size AS 'ContainerSize', oc.ContainerNo, ISNULL(oc.ContainerWeight, 0) AS ContainerWeight,  	b.Name AS Broker, sc.CompanyName as Sender, rc.CompanyName AS Receiver, bc.CompanyName as BillTo, ov.VehicleRegNo, oc.EmptyContainerDropLocation, oc.EmptyContainerPickLocation, o.*, oc.OrderConsignmentID, 	ov.OrderVehicleID, p.PackageType, p.Item, ISNULL(p.Qty, 0) AS ProdQty, ISNULL(p.TotalWeight, 0) AS ProductWeight  FROM [Order] o INNER JOIN OrderConsignment oc ON o.OrderID = oc.OrderID INNER JOIN ContainerType ct ON ct.ContainerTypeID = oc.ContainerType INNER JOIN OrderVehicle ov ON ov.OrderID = o.OrderID INNER JOIN Company sc ON sc.CompanyID = o.SenderCompanyID INNER JOIN Company rc ON rc.CompanyID = o.ReceiverCompanyID INNER JOIN Company bc ON bc.CompanyID = o.CustomerCompanyID INNER JOIN Brokers b ON b.ID = ov.BrokerID LEFT JOIN OrderProduct p ON o.OrderID = p.OrderID WHERE o.OrderID = " + OrderID;
                _commnadData.CommandText = @"SELECT *, ISNULL((SELECT CreditTerm FROM CustomerProfile cp INNER JOIN Company c ON c.CompanyID = cp.CustomerId WHERE c.CompanyName = REPLACE(oi.CustomerCompany, ' ', '')), 0) CreditLimit FROM OrderInvoices oi ORDER BY InvoiceID " + SortState;

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

        public DataTable GetInvoice(string InvoiceNo)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                //_commnadData.CommandText = @"SELECT ct.Size AS 'ContainerSize', oc.ContainerNo, ISNULL(oc.ContainerWeight, 0) AS ContainerWeight,  	b.Name AS Broker, sc.CompanyName as Sender, rc.CompanyName AS Receiver, bc.CompanyName as BillTo, ov.VehicleRegNo, oc.EmptyContainerDropLocation, oc.EmptyContainerPickLocation, o.*, oc.OrderConsignmentID, 	ov.OrderVehicleID, p.PackageType, p.Item, ISNULL(p.Qty, 0) AS ProdQty, ISNULL(p.TotalWeight, 0) AS ProductWeight  FROM [Order] o INNER JOIN OrderConsignment oc ON o.OrderID = oc.OrderID INNER JOIN ContainerType ct ON ct.ContainerTypeID = oc.ContainerType INNER JOIN OrderVehicle ov ON ov.OrderID = o.OrderID INNER JOIN Company sc ON sc.CompanyID = o.SenderCompanyID INNER JOIN Company rc ON rc.CompanyID = o.ReceiverCompanyID INNER JOIN Company bc ON bc.CompanyID = o.CustomerCompanyID INNER JOIN Brokers b ON b.ID = ov.BrokerID LEFT JOIN OrderProduct p ON o.OrderID = p.OrderID WHERE o.OrderID = " + OrderID;
                _commnadData.CommandText = @"SELECT oi.*, oc.ContainerNo, oc.OrderConsignmentID, oc.EmptyContainerPickLocation, oc.EmptyContainerDropLocation, oc.Rate, o.OrderNo, ISNULL(o.RecordedDate, o.DateCreated) as OrderDate, ov.VehicleRegNo  FROM OrderInvoices oi INNER JOIN [Order] o ON o.OrderID = oi.OrderID INNER JOIN OrderConsignment oc ON oc.OrderID = o.OrderID INNER JOIN OrderVehicle ov ON ov.OrderID = o.OrderID WHERE oi.InvoiceNo = '" + InvoiceNo + "'";

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

        //public DataTable GetBiltyContainers(Int64 OrderConsignmentID)
        //{
        //    //Creating object of DAL class
        //    CommandData _commnadData = new CommandData();

        //    try
        //    {
        //        _commnadData._CommandType = CommandType.Text;
        //        _commnadData.CommandText = @"SELECT oc.*, (SELECT OrderNo FROM [Order] WHERE OrderID = oc.OrderID) as OrderNo FROM OrderConsignment oc WHERE OrderConsignmentID = " + OrderConsignmentID;

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

        public int InsertOrderInvoices(string InvoiceNo, string CustomerInvoiceNo, string CustomerCompany, Int64 OrderID, double Total, Int64 CreatedBy)
        {
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "INSERT INTO OrderInvoices (InvoiceNo, CustomerInvoice, CustomerCompany, OrderID, Total, CreatedBy, CreatedDate) VALUES ('" + InvoiceNo + "', '" + CustomerInvoiceNo + "', '" + CustomerCompany + "', " + OrderID + ", " + Total + ", " + CreatedBy + ", GETDATE()); SELECT SCOPE_IDENTITY();";

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

        public int InvoiceToOrder(Int64 OrderID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE [Order] SET IsInvoiced = 1 WHERE OrderID = " + OrderID;


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

        public int MakePaymentByInvoice(string InvoiceNo, double Amount, string DocumentNo, string TransferedTo, string PaymentMode, Int64 ModifiedBy)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE OrderInvoices SET isPaid = 1, TotalBalance = ISNULL(TotalBalance, 0) - " + Amount + ", DocumentNo = '" + DocumentNo + "', TransferedTo = '" + TransferedTo + "', PaymentDate = GETDATE(), PaymentMode = '" + PaymentMode + "', ModifiedBy = " + ModifiedBy + ", ModifiedDate = GETDATE() WHERE InvoiceNo = '" + InvoiceNo + "'";

                //commandData.AddParameter("@ContainerTypeID", ContainerTypeID);
                //commandData.AddParameter("@Code", Code);
                //commandData.AddParameter("@Name", Name);
                //commandData.AddParameter("@UnitType", UnitType);
                //commandData.AddParameter("@lowerDeckInnerLength", lowerDeckInnerLength);
                //commandData.AddParameter("@LowerDeckInnerWidth", LowerDeckInnerWidth);
                //commandData.AddParameter("@LowerDeckInnerHeight", LowerDeckInnerHeight);
                //commandData.AddParameter("@LowerDeckOuterLength", LowerDeckOuterLength);
                //commandData.AddParameter("@LowerDeckOuterWidth", LowerDeckOuterWidth);
                //commandData.AddParameter("@LowerDeckOuterHeight", LowerDeckOuterHeight);
                //commandData.AddParameter("@UpperDeckInnerLength", UpperDeckInnerLength);
                //commandData.AddParameter("@UpperDeckInnerWidth", UpperDeckInnerWidth);
                //commandData.AddParameter("@UpperDeckInnerHeight", UpperDeckInnerHeight);
                //commandData.AddParameter("@UpperDeckOuterLength", UpperDeckOuterLength);
                //commandData.AddParameter("@UpperDeckOuterWidth", UpperDeckOuterWidth);
                //commandData.AddParameter("@UpperDeckOuterHeight", UpperDeckOuterHeight);
                //commandData.AddParameter("@UpperPortionInnerLength", UpperPortionInnerLength);
                //commandData.AddParameter("@UpperPortionInnerwidth", UpperPortionInnerwidth);
                //commandData.AddParameter("@UpperPortionInnerHeight", UpperPortionInnerHeight);
                //commandData.AddParameter("@LowerPortionInnerWidth", LowerPortionInnerWidth);
                //commandData.AddParameter("@LowerPortionInnerLength", LowerPortionInnerLength);
                //commandData.AddParameter("@LowerPortionInnerHeight", LowerPortionInnerHeight);
                //commandData.AddParameter("@TareWeight", TareWeight);
                //commandData.AddParameter("@PayloadWeight", PayloadWeight);
                //commandData.AddParameter("@CubicCapacity", CubicCapacity);
                //commandData.AddParameter("@Description", Description);
                //commandData.AddParameter("@ModifiedBy", ModifiedBy);

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

        ////public int UpdateContainerReceivingStatus(Int64 OrderConsignemntID, int Status)
        ////{
        ////    //Creating object of DAL class
        ////    CommandData commandData = new CommandData();

        ////    try
        ////    {
        ////        commandData._CommandType = CommandType.Text;
        ////        commandData.CommandText = "UPDATE OrderConsignment SET [Status] = " + Status + " WHERE OrderConsignmentID = " + OrderConsignemntID;


        ////        //opening connection
        ////        commandData.OpenWithOutTrans();

        ////        //Executing Query
        ////        Object valReturned = commandData.Execute(ExecutionType.ExecuteScalar);

        ////        return Convert.ToInt32(valReturned);
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        throw ex;
        ////    }
        ////    finally
        ////    {
        ////        commandData.Close();
        ////    }
        ////}

        //public int UpdateContainerReceivingStatus(Int64 OrderConsignemntID, int Status, string ReceivedBy, string ReceivedDateTime)
        //{
        //    //Creating object of DAL class
        //    CommandData commandData = new CommandData();

        //    try
        //    {
        //        commandData._CommandType = CommandType.Text;
        //        //commandData.CommandText = "UPDATE OrderConsignment SET [Status] = " + Status + ", ReceivedBy = '" + ReceivedBy + "', ReceivedDateTime = '" + ReceivedDateTime + "', WeighmentCharges = " + Weighment + " WHERE OrderConsignmentID = " + OrderConsignemntID;
        //        commandData.CommandText = "UPDATE OrderConsignment SET [Status] = " + Status + ", ReceivedBy = '" + ReceivedBy + "', ReceivedDateTime = '" + ReceivedDateTime + "' WHERE OrderConsignmentID = " + OrderConsignemntID;


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

        //public int DeleteOrderContainer(Int64 OrderContainerID)
        //{
        //    //Creating object of DAL class
        //    CommandData commandData = new CommandData();

        //    try
        //    {
        //        commandData._CommandType = CommandType.Text;
        //        commandData.CommandText = "DELETE FROM OrderConsignment WHERE OrderConsignmentID = " + OrderContainerID;

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

        #endregion

        #region Products

        public DataTable GetBiltyProductsByOrder(Int64 OrderID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = @"SELECT * FROM OrderProduct WHERE OrderID = " + OrderID;

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

        public DataTable GetBiltyProducts(Int64 OrderProductID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = @"SELECT * FROM OrderProduct WHERE OrderProductID = " + OrderProductID;

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

        public DataTable GetBiltyProductWithCode(Int64 OrderProductID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = @"SELECT op.*, p.Code FROM OrderProduct op INNER JOIN Product p ON p.Name = op.Item WHERE op.OrderProductID = " + OrderProductID;

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

        public int InsertOrderProduct(Int64 OrderID, string PackageType, string Item, Int64 Qty, double Weight)
        {
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "INSERT INTO OrderProduct (OrderID, PackageType, Item, Qty, TotalWeight) VALUES (" + OrderID + ", '" + PackageType + "', '" + Item + "', " + Qty + ", " + Weight + "); SELECT SCOPE_IDENTITY();";

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

        public int UpdateOrderProduct(Int64 OrderProductID, string PackageType, string Product, Int64 Qty, double Weight)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE OrderProduct SET PackageType = '" + PackageType + "', Item = '" + Product + "', Qty = " + Qty + ", TotalWeight = " + Weight + " WHERE OrderProductID = " + OrderProductID;


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

        public int DeleteOrderProduct(Int64 OrderProductID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "DELETE FROM OrderProduct WHERE OrderProductID = " + OrderProductID;

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

        #endregion

        #region Order Recevings

        public DataTable GetBiltyReceivingByOrder(Int64 OrderID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = @"SELECT * FROM OrderConsignmentReceiving WHERE OrderID = " + OrderID;

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

        public DataTable GetBiltyReceiving(Int64 OrderRecevingID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = @"SELECT * FROM OrderConsignmentReceiving WHERE ConsignmentReceiverID = " + OrderRecevingID;

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

        public int InsertOrderReceiving(Int64 OrderID, string ReceivedBy, string DateTime)
        {
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "INSERT INTO OrderConsignmentReceiving (OrderID, ReceivedBy, ReceivedDateTime) VALUES (" + OrderID + ", '" + ReceivedBy + "', '" + DateTime + "'); SELECT SCOPE_IDENTITY();";

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

        public int UpdateOrderReceiving(Int64 OrderReceivingID, string ReceivedBy, string DateTime)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE OrderConsignmentReceiving SET ReceivedBy = '" + ReceivedBy + "', ReceivedDateTime = '" + DateTime + "' WHERE ConsignmentReceiverID = " + OrderReceivingID;


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

        public int DeleteOrderReceiving(Int64 OrderReceivingID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "DELETE FROM OrderConsignmentReceiving WHERE ConsignmentReceiverID = " + OrderReceivingID;

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

        #endregion

        #region Order Recevings Docs

        public DataTable GetBiltyReceivingDocByOrder(Int64 OrderID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = @"SELECT * FROM OrderDocumentReceiving WHERE OrderID = " + OrderID;

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

        public DataTable GetBiltyReceivingDoc(Int64 OrderRecevingDocID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = @"SELECT * FROM OrderDocumentReceiving WHERE OrderReceivedDocumentID = " + OrderRecevingDocID;

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

        public int InsertOrderReceivingDocument(Int64 OrderID, string DocumentType, string DocumentNo, string DocumentName, string DocumentPath)
        {
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "INSERT INTO OrderDocumentReceiving (OrderID, DocumentType, DocumentNo, DocumentName, DocumentPath) VALUES (" + OrderID + ", '" + DocumentType + "', '" + DocumentNo + "', '" + DocumentName + "', '" + DocumentPath + "'); SELECT SCOPE_IDENTITY();";

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

        public int UpdateOrderReceivingDocument(Int64 OrderReceivingDocID, string DocumentType, string DocumentNo, string DocumentName, string DocumentPath)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE OrderDocumentReceiving SET DocumentType = '" + DocumentType + "', DocumentNo = '" + DocumentNo + "', DocumentName = '" + DocumentName + "', DocumentPath = '" + DocumentPath + "' WHERE OrderReceivedDocumentID = " + OrderReceivingDocID;


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

        public int DeleteOrderReceivingDoc(Int64 OrderReceivingID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "DELETE FROM OrderDocumentReceiving WHERE OrderReceivedDocumentID = " + OrderReceivingID;

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

        #endregion

        #region Order Damage

        public DataTable GetBiltyDamageByOrder(Int64 OrderID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = @"SELECT * FROM OrderDamage WHERE OrderID = " + OrderID;

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

        public DataTable GetBiltyDamage(Int64 OrderDamageID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = @"SELECT * FROM OrderDamage WHERE OrderDamageID = " + OrderDamageID;

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

        public int InsertOrderDamage(Int64 OrderID, string ItemName, string DamageType, double DamageCost, string DamageCause, string DocumentName, string DocumentPath)
        {
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "INSERT INTO OrderDamage (OrderID, ItemName, DamageType, DamageCost, DamageCause, DamageDocumentName, DamageDocumentPath) VALUES (" + OrderID + ", '" + ItemName + "', '" + DamageType + "', " + DamageCost + ", '" + DamageCause + "', '" + DocumentName + "', '" + DocumentPath + "'); SELECT SCOPE_IDENTITY();";

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

        public int UpdateOrderDamage(Int64 OrderDamageID, string ItemName, string DamageType, double DamageCost, string DamageCause, string DocumentName, string DocumentPath)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE OrderDamage SET ItemName = '" + ItemName + "', DamageType = '" + DamageType + "', DamageCost = " + DamageCost + ", DamageCause = '" + DamageCause + "', DamageDocumentName = '" + DocumentName + "', DamageDocumentPath = '" + DocumentPath + "' WHERE OrderDamageID = " + OrderDamageID;


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

        public int DeleteOrderDamage(Int64 OrderDamageID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "DELETE FROM OrderDamage WHERE OrderDamageID = " + OrderDamageID;

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

        #endregion

        #region Accounts

        public DataTable GetAccounts(Int64 OrderID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = @"SELECT * FROM OrderDamage WHERE OrderID = " + OrderID;

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

        #endregion

        #region Container Expenses

        public DataTable GetExpenseTypes()
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = @"SELECT * FROM ExpensesType ORDER BY ExpensesTypeName ASC";

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

        public DataTable GetExpenseTypes(Int64 ExpenseID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = @"select * from ContainerExpenses where ContainerExpenseID =" + ExpenseID + " ";

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

        public DataTable GetExpenseTypes(string ExpenseName)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = @"select * from ExpensesType where ExpensesTypeName = '" + ExpenseName + "'";

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

        public DataTable GetExpenses(Int64 ContainerID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = @"SELECT ce.*, oc.ContainerNo, et.ExpensesTypeName FROM ContainerExpenses ce INNER JOIN OrderConsignment oc ON oc.OrderConsignmentID = ce.ContainerID INNER JOIN ExpensesType et ON et.ExpensesTypeID = ce.ExpenseTypeID WHERE ce.ContainerID = " + ContainerID;

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

        public DataTable GetDriverPaidExpenses(Int64 ContainerID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = @"SELECT ce.*, oc.ContainerNo, et.ExpensesTypeName FROM ContainerExpenses ce INNER JOIN OrderConsignment oc ON oc.OrderConsignmentID = ce.ContainerID INNER JOIN ExpensesType et ON et.ExpensesTypeID = ce.ExpenseTypeID WHERE ce.PaidByDriver = 1 AND ce.ContainerID = " + ContainerID;

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

        public DataTable GetExpenses_Breakup(Int64 OrderID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = @"SELECT et.ExpensesTypeName AS Type, ce.Amount AS Rate, COUNT(ce.Amount) AS Qty, SUM(CONVERT(bigint, ce.Amount)) AS Total FROM ContainerExpenses ce INNER JOIN ExpensesType et ON et.ExpensesTypeID = ce.ExpenseTypeID WHERE ContainerID IN (SELECT OrderConsignmentID FROM OrderConsignment WHERE OrderID = " + OrderID + ") GROUP BY ce.Amount, et.ExpensesTypeName";

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

        public DataTable GetExpensesByContainerIDandExpenseType(Int64 ContainerID, Int64 ExpenseTypeID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = @"SELECT ce.* FROM ContainerExpenses ce WHERE ce.ContainerID = " + ContainerID + " AND ExpenseTypeID = " + ExpenseTypeID;

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

        public int InsertContainerExpense(Int64 OrderContainerID, Int64 ExpenseTypeID, Int64 ExpenseAmount)
        {
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "INSERT INTO ContainerExpenses (ContainerID, ExpenseTypeID, Amount) VALUES (" + OrderContainerID + ", " + ExpenseTypeID + ", " + ExpenseAmount + "); SELECT SCOPE_IDENTITY();";

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
        public int InsertContainerExpense(Int64 OrderContainerID, Int64 ExpenseTypeID, Int64 ExpenseAmount, bool PaidByDriver)
        {
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "INSERT INTO ContainerExpenses (ContainerID, ExpenseTypeID, Amount,PaidByDriver) VALUES (" + OrderContainerID + ", " + ExpenseTypeID + ", " + ExpenseAmount + ", '" + PaidByDriver + "'); SELECT SCOPE_IDENTITY();";

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

        public int UpdateContainerExpense(Int64 ContainerExpenseID, Int64 OrderContainerID, Int64 ExpenseTypeID, Int64 ExpenseAmount)
        {
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE ContainerExpenses SET ContainerID = '" + OrderContainerID + "', ExpenseTypeID = '" + ExpenseTypeID + "' , Amount = '" + ExpenseAmount + "' WHERE ContainerExpenseID = " + ContainerExpenseID + " ";

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

        public int DeleteContainerExpense(Int64 ContainerExpenseID)
        {
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "DELETE FROM ContainerExpenses WHERE ContainerExpenseID = '" + ContainerExpenseID + "'  ";

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

        #endregion

        #region Order Advances

        public DataTable GetOrderAdvancesByOrderID(Int64 OrderID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = @"SELECT od.*, (SELECT OrderNo FROM [Order] WHERE OrderID = od.OrderID) as OrderNo, (SELECT SUM(AdvanceAmount) FROM Orderadvances WHERE OrderID = od.OrderID) AS Total, (SELECT Status FROM OrderVehicle WHERE OrderID = od.OrderID) AS VehicleStatus, (SELECT Name + '|' + Code FROM PatrolPumps WHERE PatrolPumpID = od.PatrolPumpId) AS PatrolPump FROM OrderAdvances od WHERE od.OrderID = " + OrderID;

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

        public DataTable GetOrderAdvanceByOrderAndType(Int64 OrderID, string Type)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = @"SELECT * FROM OrderAdvances WHERE OrderID = " + OrderID + " AND AdvanceAgainst = '" + Type + "'";

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

        public int InsertOrderAdvances(Int64 OrderID, string AdvanceAgainst, double AdvanceAmount, Int64 LoginID)
        {
            CommandData commandData = new CommandData();
            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "INSERT INTO OrderAdvances (OrderID, AdvanceAgainst, AdvanceAmount, CreatedBy, CreatedDate) values (" + OrderID + ", '" + AdvanceAgainst + "', " + AdvanceAmount + ", " + LoginID + ", GETDATE()); SELECT SCOPE_IDENTITY();";

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

        public int InsertOrderAdvances(Int64 OrderID, string AdvancePlace, string AdvanceAgainst, Int64 PatrolPumpID, double PatrolRate, double PatrolLitres, double AdvanceAmount, string RecordedDate, Int64 LoginID)
        {
            CommandData commandData = new CommandData();
            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "INSERT INTO OrderAdvances (OrderID, AdvancePlace, AdvanceAgainst, PatrolPumpID, PatrolRate, PatrolLitres, AdvanceAmount, RecordedDate, CreatedBy, CreatedDate) VALUES (" + OrderID + ", '" + AdvancePlace + "', '" + AdvanceAgainst + "', " + PatrolPumpID + ", " + PatrolRate + ", " + PatrolLitres + ", " + AdvanceAmount + ", '" + RecordedDate + "', " + LoginID + ", GETDATE()); SELECT SCOPE_IDENTITY();";

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

        //public int UpdateOrderContainerInfo(Int64 OrderConsignemntID, Int64 OrderID, Int64 ContainerType, string ContainerNo, double ContainerWeight, string EmptyContainerPickLocation, string EmptyContainerDropLocation, string Remarks, string AssignedVehicle)
        //{
        //    //Creating object of DAL class
        //    CommandData commandData = new CommandData();

        //    try
        //    {
        //        commandData._CommandType = CommandType.Text;
        //        commandData.CommandText = "UPDATE OrderConsignment SET ContainerType = " + ContainerType + ", ContainerNo = '" + ContainerNo + "', ContainerWeight = " + ContainerWeight + ", EmptyContainerPickLocation = '" + EmptyContainerPickLocation + "', EmptyContainerDropLocation = '" + EmptyContainerDropLocation + "', Remarks = '" + Remarks + "', AssignedVehicle = '" + AssignedVehicle + "' WHERE OrderConsignmentID = " + OrderConsignemntID;


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

        //public int UpdateContainerReceivingStatus(Int64 OrderConsignemntID, int Status, string ReceivedBy, string ReceivedDateTime)
        //{
        //    //Creating object of DAL class
        //    CommandData commandData = new CommandData();

        //    try
        //    {
        //        commandData._CommandType = CommandType.Text;
        //        //commandData.CommandText = "UPDATE OrderConsignment SET [Status] = " + Status + ", ReceivedBy = '" + ReceivedBy + "', ReceivedDateTime = '" + ReceivedDateTime + "', WeighmentCharges = " + Weighment + " WHERE OrderConsignmentID = " + OrderConsignemntID;
        //        commandData.CommandText = "UPDATE OrderConsignment SET [Status] = " + Status + ", ReceivedBy = '" + ReceivedBy + "', ReceivedDateTime = '" + ReceivedDateTime + "' WHERE OrderConsignmentID = " + OrderConsignemntID;


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

        public int DeleteOrderAdvance(Int64 OrderAdvanceID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "DELETE FROM OrderAdvances WHERE AdvanceID = " + OrderAdvanceID;

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

        public int DeleteOrderAdvancesByOrder(Int64 OrderID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "DELETE FROM OrderAdvances WHERE OrderID = " + OrderID;

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

        #endregion

        #region Order Broker

        public DataTable GetBrokerByOrder(Int64 OrderID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = @"SELECT ov.*, b.*, (SELECT OrderNo FROM [Order] WHERE OrderID = ov.OrderID) as OrderNo, (SELECT RecordedDate FROM [Order] WHERE OrderID = ov.OrderID) as RecordedDate FROM OrderVehicle ov INNER JOIN Brokers b ON b.ID = ov.BrokerID WHERE ov.OrderID = " + OrderID;

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

        #endregion

        #region Shipping Line

        public DataTable GetActiveShippingLine()
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = @"SELECT *, ISNULL(LiftOffLiftOnCharges, 0) AS ExpenseCharges FROM ShippingLine WHERE ISNULL(IsActive, 0) = 1";

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

        public DataTable GetActiveShippingLine(Int64 ShippingLineID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = @"SELECT *, ISNULL(LiftOffLiftOnCharges, 0) AS ExpenseCharges FROM ShippingLine WHERE ISNULL(IsActive, 0) = 1 AND ShippingLineID = " + ShippingLineID;

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

        public DataTable GetActiveShippingLine(string Name)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = @"SELECT *, ISNULL(LiftOffLiftOnCharges, 0) AS ExpenseCharges FROM ShippingLine WHERE ISNULL(IsActive, 0) = 1 AND Name = '" + Name + "'";

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

        #endregion

        #region Vouchers

        public DataTable GetOrdersForVoucher()
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = @"SELECT DISTINCT OrderNo, (o.OrderNo + ' | ' + ov.VehicleRegNo + ' | ' + CAST(oa.AdvanceAmount as nvarchar(10))) AS AdvanceString, o.* FROM [Order] o INNER JOIN OrderAdvances oa ON oa.OrderID = o.OrderID INNER JOIN OrderVehicle ov ON ov.OrderID = o.OrderID LEFT JOIN OrderConsignment oc ON oc.OrderID = o.OrderID WHERE o.OrderID NOT IN (SELECT OrderID FROM Vouchers WHERE ISNULL(OrderID, 0) <> 0) AND oa.AdvanceAgainst = 'Advance Freight' AND ov.Status <> 'Complete' AND ISNULL(oc.Status, 0) <> 1 ORDER BY OrderID DESC";

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

        public DataTable GetOrdersForVoucherByBrokerID(Int64 BrokerID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = @"SELECT DISTINCT OrderNo, (o.OrderNo + ' | ' + ov.VehicleRegNo + ' | ' + CAST(oa.AdvanceAmount as nvarchar(10))) AS AdvanceString, o.* FROM [Order] o INNER JOIN OrderAdvances oa ON oa.OrderID = o.OrderID INNER JOIN OrderVehicle ov ON ov.OrderID = o.OrderID LEFT JOIN OrderConsignment oc ON oc.OrderID = o.OrderID WHERE o.OrderID NOT IN (SELECT OrderID FROM Vouchers WHERE ISNULL(OrderID, 0) <> 0) AND oa.AdvanceAgainst = 'Advance Freight' AND ov.Status <> 'Complete' AND ISNULL(oc.Status, 0) <> 1 AND ov.BrokerID = " + BrokerID + " ORDER BY OrderID DESC";

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

        #endregion

        public int DeactivatePaidByDriver(Int64 ContainerID, Int64 ModifiedByID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "update ContainerExpenses set [PaidByDriver]='False',[ModifiedByID]='" + ModifiedByID + "',[ModifiedDate]=GETDATE() where [ContainerExpenseID]=" + ContainerID;


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
        public int ActivatePaidByDriver(Int64 ContainerID, Int64 ModifiedByID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "update ContainerExpenses set [PaidByDriver]='True',[ModifiedByID]='" + ModifiedByID + "',[ModifiedDate]=GETDATE() where [ContainerExpenseID]=" + ContainerID;


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
