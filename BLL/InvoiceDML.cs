using SqlDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class InvoiceDML
    {
        public DataTable GetCompany()
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT * FROM Company";


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

        public DataTable GetOrder(Int64 ID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT *, sc.CompanyName AS Sender, sc.Address as SAddress, rc.CompanyName AS Receiver, rc.Address AS RAddress, cc.CompanyName AS CustomerCompany, cc.Address AS CAddress FROM [Order] o INNER JOIN Company sc ON o.SenderCompanyID = sc.CompanyID INNER JOIN Company rc ON o.ReceiverCompanyID = rc.CompanyID INNER JOIN Company cc ON o.SenderCompanyID = cc.CompanyID   WHERE o.OrderID = " + ID;


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

        public DataTable GetLastBillNo()
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT MAX(BillNo) AS BillNo FROm Bill";


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

        public DataTable GetPackages(Int64 ID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT * FROM [OrderProduct] where OrderID = '" + ID + "' ";


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

        public DataTable GetOrder(string FilterExpression)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT *, sc.CompanyName AS Sender, rc.CompanyName AS Receiver FROM [Order] o INNER JOIN Company sc ON o.SenderCompanyID = sc.CompanyID INNER JOIN Company rc ON o.ReceiverCompanyID = rc.CompanyID " + FilterExpression;


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

        public DataTable GetOrderConsigment(Int64 Id)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT O.*, ct.ContainerType AS ContainerTypeName,ct.Size AS ContainerSize FROM OrderConsignment O INNER JOIN ContainerType ct ON ct.ContainerTypeID = O.ContainerType WHERE OrderID = '" + Id + "'";


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

        public DataTable GetCompletedContainers()
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT oc.*, o.OrderNo, o.RecordedDate, o.CustomerCompanyID, sp.Name AS Shippingline FROM OrderConsignment oc INNER JOIN [Order] o ON o.OrderID = oc.OrderID LEFT JOIN ShippingLine sp ON sp.ShippingLineID = o.ShippingLineID WHERE ISNULL(oc.Status, 0) = 1 AND ISNULL(PaymentStatus, 0) = 0 AND ISNULL(IsBilled, 0) = 0";


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

        public DataTable GetCompletedContainers(string QueryExpression)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;

                ////With expenses
                //_commnadData.CommandText = "SELECT oc.*, o.OrderNo, o.RecordedDate, o.CustomerCompanyID, ISNULL(ce.Amount, 0) AS WeighmentAmount FROM OrderConsignment oc INNER JOIN [Order] o ON o.OrderID = oc.OrderID LEFT JOIN ContainerExpenses ce ON ce.ContainerID = oc.OrderConsignmentID WHERE ISNULL(oc.Status, 0) = 1 AND ISNULL(PaymentStatus, 0) = 0 AND ISNULL(IsInvoiced, 0) = 0 " + QueryExpression;

                //Without expenses
                _commnadData.CommandText = "SELECT oc.*,o.Date ,o.OrderNo, o.RecordedDate, o.CustomerCompanyID, sp.Name AS Shippingline, o.PaidToPay FROM OrderConsignment oc INNER JOIN [Order] o ON o.OrderID = oc.OrderID LEFT JOIN ShippingLine sp ON sp.ShippingLineID = o.ShippingLineID WHERE ISNULL(oc.Status, 0) = 1 AND ISNULL(PaymentStatus, 0) = 0 AND ISNULL(IsBilled, 0) = 0 AND o.PaidToPay = 'Paid' " + QueryExpression;


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
        public DataTable GetEditContainers(Int64 CustomerCompany)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;

                ////With expenses
                //_commnadData.CommandText = "SELECT oc.*, o.OrderNo, o.RecordedDate, o.CustomerCompanyID, ISNULL(ce.Amount, 0) AS WeighmentAmount FROM OrderConsignment oc INNER JOIN [Order] o ON o.OrderID = oc.OrderID LEFT JOIN ContainerExpenses ce ON ce.ContainerID = oc.OrderConsignmentID WHERE ISNULL(oc.Status, 0) = 1 AND ISNULL(PaymentStatus, 0) = 0 AND ISNULL(IsInvoiced, 0) = 0 " + QueryExpression;

                //Without expenses
                _commnadData.CommandText = @"SELECT oc.*,
                                             o.OrderNo, o.RecordedDate, o.CustomerCompanyID, sp.Name AS Shippingline, o.PaidToPay FROM OrderConsignment oc INNER JOIN[Order] o ON o.OrderID = oc.OrderID LEFT JOIN ShippingLine sp ON sp.ShippingLineID = o.ShippingLineID
                                            WHERE ISNULL(oc.Status, 0) = 1 AND ISNULL(PaymentStatus, 0) = 0 AND ISNULL(IsBilled, 0) = 0 AND o.PaidToPay = 'Paid' AND o.CustomerCompanyID = " + CustomerCompany;


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

        public DataTable SearchBill(string Query)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                //_commnadData.CommandText = "SELECT DISTINCT BillNo, CustomerCompany, Total, CONVERT(date, CreatedDate) as CreatedDate FROM Bill i " + Query;
                //_commnadData.CommandText = @"SELECT

                //                                bi.BillNo, 
                //                             bi.CustomerCompany, 
                //                             bi.Total, 
                //                             bi.TotalBalance, 
                //                             CONVERT(date, bi.CreatedDate) AS CreatedDate,
                //                                ISNULL((SELECT CreditTerm FROM CustomerProfile cp INNER JOIN Company c ON c.CompanyID = cp.CustomerId WHERE c.CompanyName = REPLACE(bi.CustomerCompany, ' ', '')), 0) CreditLimit, 
                //                             ISNULL((SELECT TOP 1 isPayed FROM Bill WHERE BillNo = bi.BillNo), 0) AS isPaid,
                //                                COUNT(OrderConsignmentID) as TotalContainers, 
                //                             (SELECT TOP 1 sl.Name FROM OrderConsignment oc INNER JOIN Bill b on b.OrderConsignmentID = oc.OrderConsignmentID INNER JOIN[Order] o ON o.OrderID = oc.OrderID INNER JOIN ShippingLine sl ON sl.ShippingLineID = o.ShippingLineID WHERE b.BillNo = bi.BillNo Group By sl.Name) AS ShippingLine, 
                //                             (SELECT TOP 1 [Date] FROM [Order] o INNER JOIN OrderConsignment oc ON oc.OrderID = o.OrderID) AS BiltyDate
                //                                FROM
                //                                    Bill bi " + Query + @"
                //                                Group BY
                //                                    bi.BillNo, 
                //                                 bi.CustomerCompany, 
                //                                 bi.Total, 
                //                                 bi.TotalBalance, 
                //                                 Convert(date, bi.CreatedDate)
                //                                ORDER by MAX(bi.CreatedDate) DESC";
                //_commnadData.CommandText = @"
                //                            SELECT


                //                             CONCAT((SELECT CompanyCode FROM Company c WHERE REPLACE(CompanyName, ' ', '') = REPLACE(bi.CustomerCompany, ' ', '')), bi.BillNo) As BillNo, 
                //                             bi.BillNo as ActualBillNo, 
                //                             bi.CustomerCompany, 
                //                             bi.Total, 
                //                             bi.TotalBalance, 
                //                             CONVERT(date, bi.CreatedDate) AS CreatedDate,
                //                                ISNULL((SEGeLECT CreditTerm FROM CustomerProfile cp INNER JOIN Company c ON c.CompanyID = cp.CustomerId WHERE c.CompanyName = REPLACE(bi.CustomerCompany, ' ', '')), 0) CreditLimit, 
                //                             ISNULL((SELECT TOP 1 isPayed FROM Bill WHERE BillNo = bi.BillNo), 0) AS isPaid,
                //                                COUNT(OrderConsignmentID) as TotalContainers, 
                //                             (SELECT TOP 1 sl.Name FROM OrderConsignment oc INNER JOIN Bill b on b.OrderConsignmentID = oc.OrderConsignmentID INNER JOIN[Order] o ON o.OrderID = oc.OrderID INNER JOIN ShippingLine sl ON sl.ShippingLineID = o.ShippingLineID WHERE b.BillNo = bi.BillNo Group By sl.Name) AS ShippingLine, 
                //                             (SELECT TOP 1 [Date] FROM [Order] o INNER JOIN OrderConsignment oc ON oc.OrderID = o.OrderID) AS BiltyDate
                //                            FROM
                //                                Bill bi " + Query + @"
                //                            Group BY
                //                                bi.BillNo, 
                //                             bi.CustomerCompany, 
                //                             bi.Total, 
                //                             bi.TotalBalance, 
                //                             Convert(date, bi.CreatedDate)
                //                            ORDER by MAX(bi.CreatedDate) DESC";

                _commnadData.CommandText = @" SELECT
	                                            CONCAT((SELECT CompanyCode FROM Company c WHERE REPLACE(CompanyName, ' ', '') = REPLACE(bi.CustomerCompany, ' ', '')),'-', bi.BillNo) As BillNo, 
                                                (SELECT CONCAT(CompanyCode,'-', CompanyID) FROM Company c WHERE REPLACE(CompanyName, ' ', '') = REPLACE(bi.CustomerCompany, ' ', '')) As CustCodeID, 
	                                            bi.BillNo as ActualBillNo, 
	                                            bi.CustomerCompany, 
	                                            bi.Total, 
	                                            bi.TotalBalance, 
	                                            (SELECT TOP 1 CreatedDate FROM Bill where BillNo = bi.BillNo ORDER BY BillID ASC) 'CreatedDate', 
	                                            (SELECT TOP 1 ModifiedDate FROM Bill where BillNo = bi.BillNo ORDER BY BillID DESC) 'ModifiedDate', 
	                                            --CONVERT(date, bi.CreatedDate) AS CreatedDate,
                                                ISNULL((SELECT CreditTerm FROM CustomerProfile cp INNER JOIN Company c ON c.CompanyID = cp.CustomerId WHERE c.CompanyName = REPLACE(bi.CustomerCompany, ' ', '')), 0) CreditLimit, 
	                                            ISNULL((SELECT TOP 1 isPayed FROM Bill WHERE BillNo = bi.BillNo), 0) AS isPaid,
                                                COUNT(OrderConsignmentID) as TotalContainers, 
	                                            (SELECT TOP 1 sl.Name FROM OrderConsignment oc INNER JOIN Bill b on b.OrderConsignmentID = oc.OrderConsignmentID INNER JOIN[Order] o ON o.OrderID = oc.OrderID INNER JOIN ShippingLine sl ON sl.ShippingLineID = o.ShippingLineID 
                                                INNER JOIN Company c ON c.CompanyID = o.CustomerCompanyID
												WHERE b.BillNo = bi.BillNo AND b.CustomerCompany = bi.CustomerCompany Group By sl.Name) AS ShippingLine, 
	                                            (SELECT TOP 1 [Date] FROM [Order] o INNER JOIN OrderConsignment oc ON oc.OrderID = o.OrderID) AS BiltyDate
                                            FROM
                                                Bill bi " + Query + @"
                                               Where ISNULL(bi.IsDeleted,0) = 0
                                                Group BY
                                                bi.BillNo, 		
	                                            bi.CustomerCompany, 
	                                            bi.Total, 
	                                            bi.TotalBalance
	                                            --Convert(date, bi.CreatedDate)
                                            ORDER by MAX(bi.CreatedDate) DESC";

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

        public DataTable GetBill()
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();
            try
            {
                _commnadData._CommandType = CommandType.Text;
                //_commnadData.CommandText = "SELECT bi.BillNo, bi.CustomerCompany, bi.Total, bi.TotalBalance, CONVERT(date, bi.CreatedDate) AS CreatedDate, ISNULL((SELECT CreditTerm FROM CustomerProfile cp INNER JOIN Company c ON c.CompanyID = cp.CustomerId WHERE c.CompanyName = REPLACE(bi.CustomerCompany, ' ', '')), 0) CreditLimit, ISNULL((SELECT TOP 1 isPayed FROM Bill WHERE BillNo = bi.BillNo), 0) AS isPaid FROM Bill bi Group BY bi.BillNo, bi.CustomerCompany, bi.Total, bi.TotalBalance, Convert(date, bi.CreatedDate) ORDER by MAX(bi.CreatedDate) DESC";
                //_commnadData.CommandText = @"
                //                            SELECT


                //                             CONCAT((SELECT CompanyCode FROM Company c WHERE REPLACE(CompanyName, ' ', '') = REPLACE(bi.CustomerCompany, ' ', '')),'-', bi.BillNo) As BillNo, 
                //                             bi.BillNo as ActualBillNo, 
                //                             bi.CustomerCompany, 
                //                             bi.Total, 
                //                             bi.TotalBalance, 
                //                             CONVERT(date, bi.CreatedDate) AS CreatedDate,
                //                                ISNULL((SELECT CreditTerm FROM CustomerProfile cp INNER JOIN Company c ON c.CompanyID = cp.CustomerId WHERE c.CompanyName = REPLACE(bi.CustomerCompany, ' ', '')), 0) CreditLimit, 
                //                             ISNULL((SELECT TOP 1 isPayed FROM Bill WHERE BillNo = bi.BillNo), 0) AS isPaid,
                //                                COUNT(OrderConsignmentID) as TotalContainers, 
                //                             (SELECT TOP 1 sl.Name FROM OrderConsignment oc INNER JOIN Bill b on b.OrderConsignmentID = oc.OrderConsignmentID INNER JOIN[Order] o ON o.OrderID = oc.OrderID INNER JOIN ShippingLine sl ON sl.ShippingLineID = o.ShippingLineID WHERE b.BillNo = bi.BillNo Group By sl.Name) AS ShippingLine, 
                //                             (SELECT TOP 1 [Date] FROM [Order] o INNER JOIN OrderConsignment oc ON oc.OrderID = o.OrderID) AS BiltyDate
                //                            FROM
                //                                Bill bi
                //                            Group BY
                //                                bi.BillNo, 
                //                             bi.CustomerCompany, 
                //                             bi.Total, 
                //                             bi.TotalBalance, 
                //                             Convert(date, bi.CreatedDate)
                //                            ORDER by MAX(bi.CreatedDate) DESC";

                _commnadData.CommandText = @" SELECT
	                                            CONCAT((SELECT CompanyCode FROM Company c WHERE REPLACE(CompanyName, ' ', '') = REPLACE(bi.CustomerCompany, ' ', '')),'-', bi.BillNo) As BillNo, 
                                                (SELECT CONCAT(CompanyCode,'-', CompanyID) FROM Company c WHERE REPLACE(CompanyName, ' ', '') = REPLACE(bi.CustomerCompany, ' ', '')) As CustCodeID, 
	                                            bi.BillNo as ActualBillNo, 
	                                            bi.CustomerCompany, 
	                                            bi.Total, 
	                                            bi.TotalBalance, 
	                                            (SELECT TOP 1 CreatedDate FROM Bill where BillNo = bi.BillNo ORDER BY BillID ASC) 'CreatedDate', 
	                                            (SELECT TOP 1 ModifiedDate FROM Bill where BillNo = bi.BillNo ORDER BY BillID DESC) 'ModifiedDate', 
	                                            --CONVERT(date, bi.CreatedDate) AS CreatedDate,
                                                ISNULL((SELECT CreditTerm FROM CustomerProfile cp INNER JOIN Company c ON c.CompanyID = cp.CustomerId WHERE c.CompanyName = REPLACE(bi.CustomerCompany, ' ', '')), 0) CreditLimit, 
	                                            ISNULL((SELECT TOP 1 isPayed FROM Bill WHERE BillNo = bi.BillNo), 0) AS isPaid,
                                                COUNT(OrderConsignmentID) as TotalContainers, 
	                                            (SELECT TOP 1 sl.Name FROM OrderConsignment oc INNER JOIN Bill b on b.OrderConsignmentID = oc.OrderConsignmentID INNER JOIN[Order] o ON o.OrderID = oc.OrderID INNER JOIN ShippingLine sl ON sl.ShippingLineID = o.ShippingLineID 
                                                INNER JOIN Company c ON c.CompanyID = o.CustomerCompanyID
												WHERE b.BillNo = bi.BillNo AND b.CustomerCompany = bi.CustomerCompany Group By sl.Name) AS ShippingLine,
	                                            (SELECT TOP 1 [Date] FROM [Order] o INNER JOIN OrderConsignment oc ON oc.OrderID = o.OrderID) AS BiltyDate
                                            FROM
                                                Bill bi
                                               Where ISNULL(bi.IsDeleted,0) = 0
                                            Group BY
                                                bi.BillNo, 		
	                                            bi.CustomerCompany, 
	                                            bi.Total, 
	                                            bi.TotalBalance
                                            ORDER by MAX(bi.CreatedDate) DESC";


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
        //public DataTable GetBillByIsBilled()
        //{
        //    //Creating object of DAL class
        //    CommandData _commnadData = new CommandData();
        //    try
        //    {
        //        _commnadData._CommandType = CommandType.Text;
                

        //        _commnadData.CommandText = " select * from OrderConsignment where ISNULL(IsBilled,0)=0 Order By ContainerNo";


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

        public DataTable GetBill(string InvoiceNumber, string CustomerCompany)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                //_commnadData.CommandText = "SELECT * FROM Bill WHERE BillNo = '" + InvoiceNumber + "'";
                _commnadData.CommandText = @"SELECT *, CONCAT((SELECT CompanyCode FROM Company c WHERE REPLACE(CompanyName, ' ', '') = REPLACE(b.CustomerCompany, ' ', '')),'-', b.BillNo) As NewBillNo,
                                                (SELECT sl.Name FROM[Order] o
                                                INNER JOIN OrderConsignment oc ON oc.OrderID = o.OrderID
                                                LEFT JOIN ShippingLine sl ON sl.ShippingLineID = o.ShippingLineID WHERE OrderConsignmentID = b.OrderConsignmentID) as ShippingLine
                                                FROM Bill b WHERE BillNo = '"+InvoiceNumber+ "' AND REPLACE(b.CustomerCompany, ' ', '') = REPLACE('" + CustomerCompany + "', ' ', '')";


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

        public DataTable GetContainers(string ContainerIDs)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                //_commnadData.CommandText = "SELECT oc.*, o.RecordedDate, scomp.CompanyName AS Sender, rcomp.CompanyName AS Receiver, bcomp.CompanyName AS BillTo FROM OrderConsignment oc INNER JOIN [Order] o ON o.OrderID = oc.OrderID INNER JOIN Company scomp ON scomp.CompanyID = o.SenderCompanyID INNER JOIN Company rcomp ON rcomp.CompanyID = o.ReceiverCompanyID INNER JOIN Company bcomp ON bcomp.CompanyID = o.CustomerCompanyID WHERE OrderConsignmentID IN (" + ContainerIDs + ")";
                //_commnadData.CommandText = "SELECT oc.*, o.RecordedDate, o.OrderNo, bcomp.CompanyName AS BillTo, ISNULL((SELECT Amount FROM ContainerExpenses ice INNER JOIN ExpensesType iet ON iet.ExpensesTypeID = ice.ExpenseTypeID WHERE ice.ContainerID = oc.OrderConsignmentID AND iet.ExpensesTypeName = 'Lift Off Lift On'), 0) AS LOLO, ISNULL((SELECT Amount FROM ContainerExpenses ice INNER JOIN ExpensesType iet ON iet.ExpensesTypeID = ice.ExpenseTypeID WHERE ice.ContainerID = oc.OrderConsignmentID AND iet.ExpensesTypeName = 'Weighment Charges'), 0) AS Weighment FROM OrderConsignment oc INNER JOIN [Order] o ON o.OrderID = oc.OrderID INNER JOIN Company bcomp ON bcomp.CompanyID = o.CustomerCompanyID WHERE OrderConsignmentID IN (" + ContainerIDs + ")";
                // _commnadData.CommandText = "SELECT oc.*, o.RecordedDate, bcomp.CompanyName AS BillTo, ISNULL((SELECT SUM(CONVERT(bigint, Amount)) FROM ContainerExpenses ice INNER JOIN ExpensesType iet ON iet.ExpensesTypeID = ice.ExpenseTypeID WHERE ice.ContainerID = oc.OrderConsignmentID AND iet.ExpensesTypeName = 'Lift Off Lift On'), 0) AS LOLO, ISNULL((SELECT Amount FROM ContainerExpenses ice INNER JOIN ExpensesType iet ON iet.ExpensesTypeID = ice.ExpenseTypeID WHERE ice.ContainerID = oc.OrderConsignmentID AND iet.ExpensesTypeName = 'Weighment Charges'), 0) AS Weighment, ISNULL((SELECT SUM(CONVERT(bigint, Amount)) FROM ContainerExpenses ice INNER JOIN ExpensesType iet ON iet.ExpensesTypeID = ice.ExpenseTypeID WHERE ice.ContainerID = oc.OrderConsignmentID), 0) AS Expenses, ov.VehicleRegNo, o.OrderNo FROM OrderConsignment oc INNER JOIN[Order] o ON o.OrderID = oc.OrderID INNER JOIN Company bcomp ON bcomp.CompanyID = o.CustomerCompanyID INNER JOIN OrderVehicle ov ON o.OrderID = ov.OrderID WHERE OrderConsignmentID IN(" + ContainerIDs + ")";
                _commnadData.CommandText = @"SELECT oc.*,o.Date, o.RecordedDate, bcomp.CompanyName AS BillTo, ISNULL((SELECT SUM(CONVERT(bigint, Amount)) FROM ContainerExpenses ice 
                                                INNER JOIN ExpensesType iet ON iet.ExpensesTypeID = ice.ExpenseTypeID WHERE ice.ContainerID = oc.OrderConsignmentID AND iet.ExpensesTypeName = 'Lift Off Lift On'), 0) AS LOLO,
                                                ISNULL((SELECT SUM(Amount) FROM ContainerExpenses ice INNER JOIN ExpensesType iet ON iet.ExpensesTypeID = ice.ExpenseTypeID WHERE ice.ContainerID = oc.OrderConsignmentID AND iet.ExpensesTypeName = 'Weighment Charges'), 0) AS Weighment,
                                                 ISNULL((SELECT SUM(CONVERT(bigint, Amount)) FROM ContainerExpenses ice INNER JOIN ExpensesType iet ON iet.ExpensesTypeID = ice.ExpenseTypeID WHERE ice.ContainerID = oc.OrderConsignmentID), 0) AS Expenses, ov.VehicleRegNo, o.OrderNo
                                                 FROM OrderConsignment oc
                                                 INNER JOIN[Order] o ON o.OrderID = oc.OrderID
                                                 INNER JOIN Company bcomp ON bcomp.CompanyID = o.CustomerCompanyID
                                                 INNER JOIN OrderVehicle ov ON o.OrderID = ov.OrderID WHERE OrderConsignmentID IN(" + ContainerIDs + ")";


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
        public DataTable GetOrderConsignmentID(Int64 OrderConsignmentID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                //_commnadData.CommandText = "SELECT oc.*, o.RecordedDate, scomp.CompanyName AS Sender, rcomp.CompanyName AS Receiver, bcomp.CompanyName AS BillTo FROM OrderConsignment oc INNER JOIN [Order] o ON o.OrderID = oc.OrderID INNER JOIN Company scomp ON scomp.CompanyID = o.SenderCompanyID INNER JOIN Company rcomp ON rcomp.CompanyID = o.ReceiverCompanyID INNER JOIN Company bcomp ON bcomp.CompanyID = o.CustomerCompanyID WHERE OrderConsignmentID IN (" + ContainerIDs + ")";
                //_commnadData.CommandText = "SELECT oc.*, o.RecordedDate, o.OrderNo, bcomp.CompanyName AS BillTo, ISNULL((SELECT Amount FROM ContainerExpenses ice INNER JOIN ExpensesType iet ON iet.ExpensesTypeID = ice.ExpenseTypeID WHERE ice.ContainerID = oc.OrderConsignmentID AND iet.ExpensesTypeName = 'Lift Off Lift On'), 0) AS LOLO, ISNULL((SELECT Amount FROM ContainerExpenses ice INNER JOIN ExpensesType iet ON iet.ExpensesTypeID = ice.ExpenseTypeID WHERE ice.ContainerID = oc.OrderConsignmentID AND iet.ExpensesTypeName = 'Weighment Charges'), 0) AS Weighment FROM OrderConsignment oc INNER JOIN [Order] o ON o.OrderID = oc.OrderID INNER JOIN Company bcomp ON bcomp.CompanyID = o.CustomerCompanyID WHERE OrderConsignmentID IN (" + ContainerIDs + ")";
                _commnadData.CommandText = "SELECT * FROM OrderConsignment Where OrderConsignmentID IN ("+OrderConsignmentID+")";


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

        public DataTable GetBilledCustomer()
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT DISTINCT b.CustomerCompany, (SELECT CompanyCode FROM Company WHERE REPLACE(CompanyName, ' ', '') = REPLACE(b.CustomerCompany, ' ', '')) AS Code FROM Bill b";


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

        public DataTable GetTotalBills(string CustomerCompany)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = @"
                                            SELECT Total, DATEPART(Second, CreatedDate) Seconds
                                            FROM Bill 
                                            WHERE 
                                                CustomerCompany='" + CustomerCompany + @"'
                                            GROUP BY 
                                                Total, DATEPART(Second, CreatedDate)
                                            ";


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

        public DataTable GetBills(string CustomerCompany)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "Select * from Bill where CustomerCompany='"+CustomerCompany+"' ORDER BY BillID DESC";


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
        public DataTable GetBills(string CustomerCompany, double Total, Int64 Second)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "Select * from Bill where CustomerCompany='"+CustomerCompany+ "' AND Total = " + Total + " AND DATEPART(Second, CreatedDate) = " + Second + " ORDER BY BillID DESC";


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

        public DataTable GetBillNo(string CustomerCompany)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = @"select  

                                                MAX(CAST(ISNULL(BillNo, 0) AS BIGINT)) as BillNo, 
	                                            c.CompanyCode
                                            from Bill b
                                            INNER JOIN Company c ON REPLACE(b.CustomerCompany, ' ', '') = REPLACE(c.CompanyName, ' ', '')
                                            where REPLACE(b.CustomerCompany, ' ', '') = REPLACE('"+ CustomerCompany +@"', ' ', '')
                                            GROUP BY CompanyCode";


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
        public DataTable GetBillNoByCustomerCompany(string BillNo,string CustomerCompany)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = @"
                                            select b.*,oc.Rate, oc.ContainerNo,sl.LiftOffLiftOnCharges ,o.OrderNo from bill b
                                                INNER JOIN OrderConsignment oc ON oc.OrderConsignmentID=b.OrderConsignmentID
                                                INNER JOIN [Order] o ON o.OrderID = oc.OrderID
                                                INNER JOIN ShippingLine sl ON sl.ShippingLineID=o.ShippingLineID
                                            where BillNo = '" + BillNo+"' and CustomerCompany = '"+CustomerCompany+"'";


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


        public int CompleteVehicleStatus(string OrderNo)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE OrderVehicle SET Status = 'Complete' WHERE OrderID = (SELECT OrderID FROM [Order] WHERE OrderNo = '" + OrderNo + "')";


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

        public int InsertBill(string BillNo, string CustomerBillNo, string CustomerCompany, Int64 ContainerID, double Total, Int64 CreatedBy)
        {
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                //commandData.CommandText = "INSERT INTO Bill (BillNo, CustomerBillNo, CustomerCompany, OrderConsignmentID, Total, TotalBalance, CreatedBy, CreatedDate) VALUES ((SELECT (select TOP 1 (c.CompanyCode + ISNULL(BillNo, 0)), c.CompanyCode from Bill b INNER JOIN Company c ON REPLACE(b.CustomerCompany, ' ', '') = REPLACE(c.CompanyName, ' ', '') where REPLACE(b.CustomerCompany, ' ', '') = REPLACE('" + CustomerCompany + "', ' ', '') order by b.CreatedDate desc)  + 1), '" + CustomerBillNo + "', '" + CustomerCompany + "', " + ContainerID + ", " + Total + ", " + Total + ", " + CreatedBy + ", GETDATE()); SELECT SCOPE_IDENTITY();";
                commandData.CommandText = "INSERT INTO Bill (BillNo, CustomerBillNo, CustomerCompany, OrderConsignmentID, Total, TotalBalance, CreatedBy, CreatedDate) VALUES ('" + BillNo + "', '" + CustomerBillNo + "', '" + CustomerCompany + "', " + ContainerID + ", " + Total + ", " + Total + ", " + CreatedBy + ", GETDATE()); SELECT SCOPE_IDENTITY();";



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

        public int InsertBillFromEditBill(string BillNo, string CustomerBillNo, string CustomerCompany, Int64 ContainerID, double Total, Int64 CreatedBy)
        {
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                //commandData.CommandText = "INSERT INTO Bill (BillNo, CustomerBillNo, CustomerCompany, OrderConsignmentID, Total, TotalBalance, CreatedBy, CreatedDate) VALUES ((SELECT (select TOP 1 (c.CompanyCode + ISNULL(BillNo, 0)), c.CompanyCode from Bill b INNER JOIN Company c ON REPLACE(b.CustomerCompany, ' ', '') = REPLACE(c.CompanyName, ' ', '') where REPLACE(b.CustomerCompany, ' ', '') = REPLACE('" + CustomerCompany + "', ' ', '') order by b.CreatedDate desc)  + 1), '" + CustomerBillNo + "', '" + CustomerCompany + "', " + ContainerID + ", " + Total + ", " + Total + ", " + CreatedBy + ", GETDATE()); SELECT SCOPE_IDENTITY();";
                commandData.CommandText = "INSERT INTO Bill (BillNo, CustomerBillNo, CustomerCompany, OrderConsignmentID, Total, TotalBalance, ModifiedBy, ModifiedDate) VALUES ('" + BillNo + "', '" + CustomerBillNo + "', '" + CustomerCompany + "', " + ContainerID + ", " + Total + ", " + Total + ", " + CreatedBy + ", GETDATE()); SELECT SCOPE_IDENTITY();";



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

        public int UpdateBillFromEdit(string BillNo, double Total, Int64 CreatedBy)
        {
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                //commandData.CommandText = "INSERT INTO Bill (BillNo, CustomerBillNo, CustomerCompany, OrderConsignmentID, Total, TotalBalance, CreatedBy, CreatedDate) VALUES ((SELECT (select TOP 1 (c.CompanyCode + ISNULL(BillNo, 0)), c.CompanyCode from Bill b INNER JOIN Company c ON REPLACE(b.CustomerCompany, ' ', '') = REPLACE(c.CompanyName, ' ', '') where REPLACE(b.CustomerCompany, ' ', '') = REPLACE('" + CustomerCompany + "', ' ', '') order by b.CreatedDate desc)  + 1), '" + CustomerBillNo + "', '" + CustomerCompany + "', " + ContainerID + ", " + Total + ", " + Total + ", " + CreatedBy + ", GETDATE()); SELECT SCOPE_IDENTITY();";
                commandData.CommandText = "UPDATE BILL SET Total = "+Total+", TotalBalance = "+Total+",ModifiedBy = "+CreatedBy+",ModifiedDate = GETDATE() WHERE BillNo = " + BillNo;



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

        public int UpdateBillNo(Int64 BillID, string BillNo)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE Bill SET BillNo = '" + BillNo + "' WHERE BillID = " + BillID;


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

        public int UpdateOrderConsignmentIsBilled(Int64 OrderConsignmentID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE OrderConsignment SET IsBilled = 1 WHERE OrderConsignmentID = " + OrderConsignmentID;


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

        public int BillToContainer(Int64 ContainerID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE OrderConsignment SET [IsBilled] = 1 WHERE [OrderConsignmentID] = " + ContainerID;


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

        public int UnBillToContainer(Int64 ContainerID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE OrderConsignment SET [IsBilled] = 0 WHERE [OrderConsignmentID] = " + ContainerID;


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

        public int MakePaymentByBill(string BillNo, double Amount, string DocumentNo, string TransferedTo, string PaymentMode, Int64 ModifiedBy)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE Bill SET isPayed = 1, TotalBalance = TotalBalance - " + Amount + ", DocumentNo = '" + DocumentNo + "', TransferedTo = '" + TransferedTo + "', PaymentDate = GETDATE(), PaymentMode = '" + PaymentMode + "', ModifiedBy = " + ModifiedBy + ", ModifiedDate = GETDATE() WHERE BillNo = '" + BillNo + "'";

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
        public int DeleteBill(string BillNo)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE Bill Set IsDeleted = 1 WHERE BillNo = '" + BillNo + "' ";

                commandData.AddParameter("@BillNo", BillNo);

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
