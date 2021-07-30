using SqlDataAccess_Accounts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class AccountsDML
    {
        #region Members

        string accountsdbname;
        string biltysystemdbname;

        #endregion

        #region Properties

        public string AccountsDBName
        {
            get
            {

                //accountsdbname = "MZBBiltySystemAccounts";
                accountsdbname = "BiltySystemAccounts";
                //accountsdbname = "BiltySystemAccounts_Clone";
                return accountsdbname;
            }
        }

        public string BiltySystemDBName
        {
            get
            {
                //biltysystemdbname = "MZBBiltySystem";
                biltysystemdbname = "BiltySystem";
                //biltysystemdbname = "BiltySystem_Clone";

                return biltysystemdbname;
            }
        }

        #endregion

        public DataTable GetAccounts(string TableName)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT * FROM " + AccountsDBName + ".INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_NAME = '" + TableName + "'";

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

        public DataTable GetInAccounts(string AccountName, DateTime DateFrom, DateTime DateTo)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT * FROM [" + AccountName + "] WHERE CONVERT(date, DateCreated) BETWEEN '" + DateFrom.ToString("yyyy/MM/dd") + " 00:00:00' AND '" + DateTo.ToString("yyyy/MM/dd") + " 23:59:59'";

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

        public DataTable GetInAccounts(string AccountName, string OrderState)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT * FROM [" + AccountName + "] ORDER BY AccountID " + OrderState;

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

        public DataTable GetInAccounts(string AccountName)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT * FROM [" + AccountName + "]";

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

        public DataTable GetInAccountsAlongInfo(string AccountName, string InfoTable, string CustBroker)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT acc.*, m.*  FROM " + AccountName + " acc INNER JOIN BiltySystem_Clone.dbo." + InfoTable + " m ON m.ID = acc." + CustBroker + "ID";

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

        #region Brokers Accounts

        public DataTable GetBrokerAccounts()
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT acc.*, b.ID FROM " + AccountsDBName + ".INFORMATION_SCHEMA.TABLES acc INNER JOIN " + BiltySystemDBName + ".dbo.Brokers b ON b.Name = LEFT(TABLE_NAME, charindex('|', TABLE_NAME) - 1)  WHERE TABLE_TYPE = 'BASE TABLE' ";

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

        public DataTable GetBrokerAccounts(string AccountName)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT acc.*, b.ID FROM " + AccountsDBName + ".INFORMATION_SCHEMA.TABLES acc INNER JOIN " + BiltySystemDBName + ".dbo.Brokers b ON b.Name = LEFT(TABLE_NAME, charindex('|', TABLE_NAME) - 1)  WHERE TABLE_TYPE = 'BASE TABLE'   AND acc.TABLE_NAME = '" + AccountName + "'";

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

        public int CreateBrokerAccount(string Name)
        {
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = @"
                                            CREATE TABLE[dbo].[" + Name + @"](

                                                [AccountID][bigint] IDENTITY(1, 1) NOT NULL,

                                                [BrokerID] [bigint] NULL,
	                                            [Item] [nvarchar] (250) NULL,
	                                            [Debit] [float] NULL,
	                                            [Credit] [float] NULL,
	                                            [Balance] [float] NULL,
	                                            [CreatedByID] [bigint] NULL,
	                                            [DateCreated] [datetime] NULL,
	                                            [ModifiedById] [bigint] NULL,
	                                            [DateModified] [datetime] NULL,
                                                CONSTRAINT[PK_" + Name + @"] PRIMARY KEY CLUSTERED
                                            (
                                                [AccountID] ASC
                                            )WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]
                                            ) ON[PRIMARY]
                                            
                                            ";

                commandData.AddParameter("@Name", Name);

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

        public int InsertInBrokerAccount(string AccountName, Int64 BrokerID, string Description, double Debit, double Credit, double Balance, Int64 LoginID)
        {
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "INSERT INTO [" + AccountName + "] (BrokerID, Item, Debit, Credit, Balance, CreatedByID, DateCreated) VALUES (" + BrokerID + ", '" + Description + "', " + Debit + ", " + Credit + ", " + Balance + ", " + LoginID + ", GETDATE()); SELECT SCOPE_IDENTITY();";



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

        #region Patrol Pump Accounts

        public DataTable GetPatrolPumpAccounts()
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT acc.*, p.PatrolPumpID FROM " + AccountsDBName + ".INFORMATION_SCHEMA.TABLES acc INNER JOIN " + BiltySystemDBName + ".dbo.PatrolPumps p ON p.Name = LEFT(TABLE_NAME, charindex('|', TABLE_NAME) - 1)  WHERE TABLE_TYPE = 'BASE TABLE' ";

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

        public DataTable GetPatrolPumpAccounts(string AccountName)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT acc.*, p.PatrolPumpID FROM " + AccountsDBName + ".INFORMATION_SCHEMA.TABLES acc INNER JOIN " + BiltySystemDBName + ".dbo.PatrolPumps p ON p.Name = LEFT(TABLE_NAME, charindex('|', TABLE_NAME) - 1)  WHERE TABLE_TYPE = 'BASE TABLE'   AND acc.TABLE_NAME = '" + AccountName + "'";

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

        public int CreatePatrolPumpAccount(string Name)
        {
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = @"
                                            CREATE TABLE[dbo].[" + Name + @"](

                                                [AccountID][bigint] IDENTITY(1, 1) NOT NULL,

                                                [PatrolPumpID] [bigint] NULL,
	                                            [Item] [nvarchar] (250) NULL,
	                                            [Debit] [float] NULL,
	                                            [Credit] [float] NULL,
	                                            [Balance] [float] NULL,
	                                            [TransactedBy] [nvarchar] (50) NULL,
	                                            [TransactedFor] [nvarchar] (MAX) NULL,
                                                [RecordedDate] [datetime] NULL,
	                                            [CreatedByID] [bigint] NULL,
	                                            [DateCreated] [datetime] NULL,
	                                            [ModifiedById] [bigint] NULL,
	                                            [DateModified] [datetime] NULL,
                                                CONSTRAINT[PK_" + Name + @"] PRIMARY KEY CLUSTERED
                                            (
                                                [AccountID] ASC
                                            )WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]
                                            ) ON[PRIMARY]
                                            
                                            ";

                commandData.AddParameter("@Name", Name);

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

        public int InsertInPatrolPumpAccount(string AccountName, Int64 PatrolPumpID, string Description, double Balance, string RecordedDate, Int64 LoginID)
        {
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "INSERT INTO [" + AccountName + "] (PatrolPumpID, Item, Balance, RecordedDate, CreatedByID, DateCreated) VALUES (" + PatrolPumpID + ", '" + Description + "', " + Balance + ", '" + RecordedDate + "', " + LoginID + ", GETDATE()); SELECT SCOPE_IDENTITY();";

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

        public int InsertInPatrolPumpAccount(string AccountName, Int64 PatrolPumpID, string Description, double Debit, double Credit, double Balance, string RecordedDate, string TransactionBy, string TransactedFor, Int64 LoginID)
        {
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "INSERT INTO [" + AccountName + "] (PatrolPumpID, Item, Debit, Credit, Balance, RecordedDate, TransactedBy, TransactedFor, CreatedByID, DateCreated) VALUES (" + PatrolPumpID + ", '" + Description + "', " + Debit + ", " + Credit + ", " + Balance + ", '" + RecordedDate + "', '" + TransactionBy + "', '" + TransactedFor + "', " + LoginID + ", GETDATE()); SELECT SCOPE_IDENTITY();";



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

        public int InsertInPatrolPumpAccount(string AccountName, Int64 PatrolPumpID, string Description, double Debit, double Credit, double Balance, string TransactedBy, string TransactedFor, Int64 LoginID)
        {
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "INSERT INTO [" + AccountName + "] (PatrolPumpID, Item, Debit, Credit, Balance, TransactedBy, TransactedFor, CreatedByID, DateCreated) VALUES (" + PatrolPumpID + ", '" + Description + "', " + Debit + ", " + Credit + ", " + Balance + ", '" + TransactedBy + "', '" + TransactedFor + "', " + LoginID + ", GETDATE()); SELECT SCOPE_IDENTITY();";



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

        #region Customer Accounts

        public DataTable GetAccounts()
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT acc.*, c.CompanyID FROM " + AccountsDBName + ".INFORMATION_SCHEMA.TABLES acc INNER JOIN " + BiltySystemDBName + ".dbo.Company c ON c.CompanyName = LEFT(TABLE_NAME, charindex('|', TABLE_NAME) - 1)  WHERE TABLE_TYPE = 'BASE TABLE' ";

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

        public int CreateAccount(string Name)
        {
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = @"
                                            CREATE TABLE[dbo].[" + Name + @"](

                                                [AccountID][bigint] IDENTITY(1, 1) NOT NULL,

                                                [CompanyID] [bigint] NULL,
	                                            [Item] [nvarchar] (250) NULL,
	                                            [Debit] [float] NULL,
	                                            [Credit] [float] NULL,
	                                            [Balance] [float] NULL,
	                                            [CreatedByID] [bigint] NULL,
	                                            [DateCreated] [datetime] NULL,
	                                            [ModifiedById] [bigint] NULL,
	                                            [DateModified] [datetime] NULL,
                                                CONSTRAINT[PK_" + Name + @"] PRIMARY KEY CLUSTERED
                                            (
                                                [AccountID] ASC
                                            )WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]
                                            ) ON[PRIMARY]
                                            
                                            ";

                commandData.AddParameter("@Name", Name);

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

        public int InsertInAccount(string AccountName, Int64 AccountID, string Description, double Debit, double Credit, double Balance,  Int64 LoginID)
        {
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "INSERT INTO [" + AccountName + "] (CompanyID, Item, Debit, Credit, Balance, CreatedByID, DateCreated) VALUES (" + AccountID + ", '" + Description + "', " + Debit + ", " + Credit + ", " + Balance + ", " + LoginID + ", getdate() ); SELECT SCOPE_IDENTITY();";

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

        #region PettyCash

        public int CreatePettyCashAccount(string Name)
        {
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = @"
                                            CREATE TABLE[dbo].[" + Name + @"](

                                                [AccountID][bigint] IDENTITY(1, 1) NOT NULL,

                                                [BillNo] [nvarchar] (50) NULL,
                                                [InvoiceNo] [nvarchar] (50) NULL,
	                                            [Item] [nvarchar] (250) NULL,
	                                            [Debit] [float] NULL,
	                                            [Credit] [float] NULL,
	                                            [Balance] [float] NULL,
                                                [TransactedBy] [nvarchar] (250) NULL,
                                                [TransactedFor] [nvarchar] (250) NULL,
                                                [TransactionDate] [datetime] NULL,
	                                            [CreatedByID] [bigint] NULL,
	                                            [DateCreated] [datetime] NULL,
	                                            [ModifiedById] [bigint] NULL,
	                                            [DateModified] [datetime] NULL,
                                                CONSTRAINT[PK_" + Name + @"] PRIMARY KEY CLUSTERED
                                            (
                                                [AccountID] ASC
                                            )WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]
                                            ) ON[PRIMARY]
                                            
                                            ";

                commandData.AddParameter("@Name", Name);

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

        public int InsertInPettyCashAccount(string AccountName, string Description, double Debit, double Credit, double Balance, string TransactedBy, string TransactedFor, DateTime TransactionDate, Int64 LoginID)
        {
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "INSERT INTO [" + AccountName + "] (Item, Debit, Credit, Balance, TransactedBy, TransactedFor, TransactionDate, CreatedByID, DateCreated) VALUES ('" + Description + "', " + Debit + ", " + Credit + ", " + Balance + ", '" + TransactedBy + "', '" + TransactedFor + "', '"+TransactionDate+"'  , " + LoginID + ", GETDATE()); SELECT SCOPE_IDENTITY();";



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

        public int InsertBillToPettyCash(string AccountName, string BillNo, string Description, double Debit, double Credit, double Balance,DateTime TransactionDate ,Int64 LoginID)
        {
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "INSERT INTO [" + AccountName + "] (BillNo, Item, Debit, Credit, Balance ,TransactionDate , CreatedByID, DateCreated) VALUES ('" + BillNo + "', '" + Description + "', " + Debit + ", " + Credit + ", " + Balance + ", '"+TransactionDate+"'  ," + LoginID + ", GETDATE()); SELECT SCOPE_IDENTITY();";



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

        public int InsertInvoiceToPettyCash(string AccountName, string InvoiceNo, string Description, double Debit, double Credit, double Balance, Int64 LoginID)
        {
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "INSERT INTO [" + AccountName + "] (InvoiceNo, Item, Debit, Credit, Balance, CreatedByID, DateCreated) VALUES ('" + InvoiceNo + "', '" + Description + "', " + Debit + ", " + Credit + ", " + Balance + ", " + LoginID + ", GETDATE()); SELECT SCOPE_IDENTITY();";



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

        //public DataTable GetCustomerAccounts(Int64 CompanyID)
        //{
        //    //Creating object of DAL class
        //    CommandData _commnadData = new CommandData();

        //    try
        //    {
        //        _commnadData._CommandType = CommandType.Text;
        //        _commnadData.CommandText = "SELECT * FROM BiltySystemAccounts_Clone.INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_NAME = '" + TableName + "'";

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

        //public DataTable GetBrokerAccounts(string TableName)
        //{
        //    //Creating object of DAL class
        //    CommandData _commnadData = new CommandData();

        //    try
        //    {
        //        _commnadData._CommandType = CommandType.Text;
        //        _commnadData.CommandText = "SELECT * FROM BiltySystemAccounts_Clone.INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_NAME = '" + TableName + "'";

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

        //public DataTable GetAccounts()
        //{
        //    //Creating object of DAL class
        //    CommandData _commnadData = new CommandData();

        //    try
        //    {
        //        _commnadData._CommandType = CommandType.Text;
        //        _commnadData.CommandText = "SELECT * FROM BiltySystemAccounts_Clone.INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'; ";

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