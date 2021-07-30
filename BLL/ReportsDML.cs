using SqlDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ReportsDML
    {
        public DataTable GetBookedVehiclesByBrokers(string SortState)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT b.ID AS BrokerID, o.OrderID, o.OrderNo, b.Name Broker, Convert(date, o.RecordedDate) BookingDate, ov.VehicleRegNo, ov.Rate, oc.EmptyContainerDropLocation, ISNULL((SELECT SUM(AdvanceAmount) FROM OrderAdvances WHERE OrderID = o.OrderID), 0) TotalAdvances, ISNULL((SELECT SUM(Amount) FROM ContainerExpenses WHERE ContainerID IN (SELECT OrderConsignmentID FROM OrderConsignment WHERE OrderID = o.OrderID)), 0) TotalExpenses FROM Brokers b INNER JOIN OrderVehicle ov ON ov.BrokerID = b.ID INNER JOIN [Order] o ON o.OrderID = ov.OrderID INNER JOIN [OrderConsignment] oc ON oc.OrderID = o.OrderID ORDER BY o.RecordedDate " + SortState;

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

        //public DataTable SearchBookedVehiclesByBrokers(string Filters)
        //{
        //    //Creating object of DAL class
        //    CommandData _commnadData = new CommandData();

        //    try
        //    {
        //        _commnadData._CommandType = CommandType.Text;
        //        _commnadData.CommandText = "SELECT b.ID AS BrokerID, o.OrderID, o.OrderNo, b.Name Broker, Convert(date, o.RecordedDate) BookingDate, ov.VehicleRegNo, ov.Rate, oc.EmptyContainerDropLocation, ISNULL((SELECT SUM(AdvanceAmount) FROM OrderAdvances WHERE OrderID = o.OrderID), 0) TotalAdvances, ISNULL((SELECT SUM(Amount) FROM ContainerExpenses WHERE ContainerID IN (SELECT OrderConsignmentID FROM OrderConsignment WHERE OrderID = o.OrderID)), 0) TotalExpenses FROM Brokers b INNER JOIN OrderVehicle ov ON ov.BrokerID = b.ID INNER JOIN [Order] o ON o.OrderID = ov.OrderID INNER JOIN [OrderConsignment] oc ON oc.OrderID = o.OrderID " + Filters;

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

        public DataTable SearchBookedVehiclesByBrokers_OLD(string Filters)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = @"DECLARE @expenescolsName AS NVARCHAR(MAX)
DECLARE @query AS NVARCHAR(MAX)= '';
                SET @expenescolsName = STUFF((SELECT distinct ',' + QUOTENAME(et.ExpensesTypeName)  FROM ContainerExpenses CE INNER JOIN    ExpensesType et ON ce.ExpenseTypeID = et.ExpensesTypeID WHERE ce.ContainerID = COALESCE(ce.ContainerID, ce.ContainerID)    FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,1,'')
SET @query = 'SELECT * FROM(
SELECT* FROM(
SELECT distinct  b.ID AS BrokerID, o.OrderID, b.Name Broker, Convert(date, o.RecordedDate) BookingDate, ov.VehicleRegNo, ov.Rate, oc.EmptyContainerDropLocation, ISNULL((SELECT SUM(AdvanceAmount) FROM OrderAdvances
WHERE OrderID = o.OrderID), 0) TotalAdvances, ISNULL((SELECT SUM(Amount) FROM ContainerExpenses WHERE ContainerID IN(SELECT OrderConsignmentID FROM OrderConsignment WHERE OrderID = o.OrderID)), 0) TotalExpenses
,AD.AdvanceAgainst,AD.AdvanceAmount,ET.ExpensesTypeName,CE.Amount FROM Brokers b
INNER JOIN OrderVehicle ov ON ov.BrokerID = b.ID
INNER JOIN[Order] o ON o.OrderID = ov.OrderID
INNER JOIN[OrderConsignment] oc ON oc.OrderID = o.OrderID
left join[OrderAdvances] AD on o.OrderID = AD.OrderID
LEFT join[ContainerExpenses] CE on oc.OrderConsignmentID = CE.ContainerID
left join[ExpensesType] ET on CE.ExpenseTypeID = ET.ExpensesTypeID " + Filters + @"
) t
PIVOT(
SUM(AdvanceAmount)     FOR AdvanceAgainst IN(      [Advance Freight],[Diesel Advance],[Factory Advance],[Test])
) AS pivot_table
) t
PIVOT(
SUM(Amount)     FOR ExpensesTypeName IN('+@expenescolsName+')
) AS pivot_t'

Execute(@query)
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

        public DataTable SearchBookedVehiclesByBrokers(string Filters, string BrokerID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = @"DECLARE @expenescolsName AS NVARCHAR(MAX)
DECLARE @query AS NVARCHAR(MAX)= '';
                SET @expenescolsName = STUFF((SELECT distinct ',' + QUOTENAME(et.ExpensesTypeName)  FROM ContainerExpenses CE INNER JOIN    ExpensesType et ON ce.ExpenseTypeID = et.ExpensesTypeID WHERE ce.ContainerID = COALESCE(ce.ContainerID, ce.ContainerID)    FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,1,'')
SET @query = 'SELECT * FROM(
SELECT* FROM(
SELECT distinct  b.ID AS BrokerID, o.OrderID, b.Name Broker, Convert(date, o.RecordedDate) BookingDate, ov.VehicleRegNo, ov.Rate, oc.EmptyContainerDropLocation, ISNULL((SELECT SUM(AdvanceAmount) FROM OrderAdvances
WHERE OrderID = o.OrderID), 0) TotalAdvances, ISNULL((SELECT SUM(Amount) FROM ContainerExpenses WHERE ContainerID IN(SELECT OrderConsignmentID FROM OrderConsignment WHERE OrderID = o.OrderID)), 0) TotalExpenses
,AD.AdvanceAgainst,AD.AdvanceAmount,ET.ExpensesTypeName,CE.Amount FROM Brokers b
INNER JOIN OrderVehicle ov ON ov.BrokerID = b.ID
INNER JOIN[Order] o ON o.OrderID = ov.OrderID
INNER JOIN[OrderConsignment] oc ON oc.OrderID = o.OrderID
left join[OrderAdvances] AD on o.OrderID = AD.OrderID
LEFT join[ContainerExpenses] CE on oc.OrderConsignmentID = CE.ContainerID
left join[ExpensesType] ET on CE.ExpenseTypeID = ET.ExpensesTypeID " + Filters + @"
) t
PIVOT(
SUM(AdvanceAmount)     FOR AdvanceAgainst IN(      [Advance Freight],[Diesel Advance],[Factory Advance],[Test])
) AS pivot_table
) t
PIVOT(
SUM(Amount)     FOR ExpensesTypeName IN('+@expenescolsName+')
) AS pivot_t'

Execute(@query)";
//                _commnadData.CommandText = @"DECLARE @expenescolsName AS NVARCHAR(MAX)
//DECLARE @AdvanceColumnName AS NVARCHAR(MAX)
//DECLARE @query AS NVARCHAR(MAX)='';
//SET @expenescolsName = STUFF((Select distinct ',' + QUOTENAME(ET.ExpensesTypeName)  from ContainerExpenses CE INNER JOIN ExpensesType	ET on CE.ExpenseTypeID= ET.ExpensesTypeID WHERE   CE.ContainerID= COALESCE (CE.ContainerID,CE.ContainerID)     FOR XML PATH(''), TYPE      ).value('.', 'NVARCHAR(MAX)')  ,1,1,'')
//SET @AdvanceColumnName = STUFF((SELECT distinct  ',' +  QUOTENAME(OA.AdvanceAgainst) FROM OrderAdvances  OA inner join [Order] on [Order].OrderID=OA.OrderID  INNER JOIN OrderVehicle ov ON ov.OrderID = [Order].OrderID WHERE  [Order].OrderID= COALESCE ( [Order].OrderID,  [Order].OrderID) AND ov.BrokerID= COALESCE ( ISNULL("+BrokerID+@", NULL),  ov.BrokerID)    FOR XML PATH(''), TYPE      ).value('.', 'NVARCHAR(MAX)')  ,1,1,'')
//SET @query='SELECT * FROM(
//SELECT * FROM   (
//SELECT distinct  b.ID AS BrokerID, o.OrderID, b.Name Broker, Convert(date, o.RecordedDate) BookingDate, ov.VehicleRegNo, ov.Rate, oc.EmptyContainerDropLocation, ISNULL((SELECT SUM(AdvanceAmount) FROM OrderAdvances
//WHERE OrderID = o.OrderID), 0) TotalAdvances, ISNULL((SELECT SUM(Amount) FROM ContainerExpenses WHERE ContainerID IN (SELECT OrderConsignmentID FROM OrderConsignment WHERE OrderID = o.OrderID)), 0) TotalExpenses
//,AD.AdvanceAgainst,AD.AdvanceAmount,ET.ExpensesTypeName,CE.Amount FROM Brokers b
//INNER JOIN OrderVehicle ov ON ov.BrokerID = b.ID
//INNER JOIN [Order] o ON o.OrderID = ov.OrderID
//INNER JOIN [OrderConsignment] oc ON oc.OrderID = o.OrderID
//left join [OrderAdvances] AD  on o.OrderID=AD.OrderID
//LEFT join [ContainerExpenses] CE on oc.OrderConsignmentID=CE.ContainerID
//left join [ExpensesType] ET on CE.ExpenseTypeID=ET.ExpensesTypeID
//" + Filters + @"
//) t
//PIVOT(
//SUM(AdvanceAmount)     FOR AdvanceAgainst IN (      ' + @AdvanceColumnName +' )
//) AS pivot_table
//) t
//PIVOT(
//SUM(Amount)     FOR ExpensesTypeName IN ( '+@expenescolsName+')
//) AS pivot_t'
//Execute(@query)
//";

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

        public DataTable GetOrderAdvances(Int64 OrderID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT * FROM OrderAdvances WHERE OrderID = " + OrderID;

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
