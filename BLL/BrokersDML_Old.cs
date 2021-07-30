using SqlDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BrokersDML
    {
        public DataTable GetAllBrokers()
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT * FROM Brokers ORDER BY ID DESC";

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

        public DataTable GetBroker()
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT * FROM Brokers ORDER BY Name ASC";

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
        public DataTable GetBrokerByName(string Name)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT * FROM Brokers WHERE Name = '" + Name + "'";

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
        public DataTable GetActiveBroker()
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT *, Name + '|' + Code AS Namestring FROM Brokers WHERE ISNULL(isActive, 0) = 1 ORDER BY Name ASC";

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

        public DataTable GetBroker(Int64 BrokerID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT * FROM Brokers WHERE ID = @BrokerID ORDER BY ID DESC";

                _commnadData.AddParameter("@BrokerID", BrokerID);

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

        public DataTable GetBroker(string keyword)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT * FROM Brokers WHERE Code LIKE '%" + keyword + "%' OR Name LIKE '%" + keyword + "%' OR Phone LIKE '%" + keyword + "%' OR Phone2 LIKE '%" + keyword + "%' OR HomeNo LIKE '%" + keyword + "%' OR Address LIKE '%" + keyword + "%' OR NIC LIKE '%" + keyword + "%' ";

                _commnadData.AddParameter("@keyword", keyword);

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

        public DataTable GetActiveBrokersByName(string Name)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT * FROM Brokers WHERE Name = @Name AND isActive = 1";

                _commnadData.AddParameter("@Name", Name);

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

        public DataTable GetBroker(string Code, Int64 Phone, Int64 CNIC)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT * FROM Brokers WHERE (Code = @Code OR Phone = @Phone OR NIC = @NIC) AND isActive = 1";

                _commnadData.AddParameter("@Code", Code);
                _commnadData.AddParameter("@Phone", Phone);
                _commnadData.AddParameter("@NIC", CNIC);

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

        public int InsertBroker(string Code, string Name, Int64 Phone, Int64 Phone2, Int64 HomeNo, string Address, Int64 NIC, string Description, Int64 CreatedBy)
        {
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "INSERT INTO Brokers (Code, Name, Phone, Phone2, HomeNo, Address, NIC, Description, CreatedBy, CreatedDate) VALUES (@Code, @Name, @Phone, @Phone2, @HomeNo, @Address, @NIC, @Description, @CreatedBy, GETDATE()); SELECT SCOPE_IDENTITY();";

                commandData.AddParameter("@Code", Code);
                commandData.AddParameter("@Name", Name);
                commandData.AddParameter("@Phone", Phone);
                commandData.AddParameter("@Phone2", Phone2);
                commandData.AddParameter("@HomeNo", HomeNo);
                commandData.AddParameter("@Address", Address);
                commandData.AddParameter("@NIC", NIC);
                commandData.AddParameter("@Description", Description);
                commandData.AddParameter("@CreatedBy", CreatedBy);

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
        public int InsertBroker(string Code, string Name, Int64 CreatedBy)
        {
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "INSERT INTO Brokers (Code, Name,CreatedBy, CreatedDate) VALUES (@Code, @Name, @CreatedBy, GETDATE()); SELECT SCOPE_IDENTITY();";

                commandData.AddParameter("@Code", Code);
                commandData.AddParameter("@Name", Name);
                commandData.AddParameter("@CreatedBy", CreatedBy);

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

        public int UpdateBroker(Int64 BrokerID, string Code, string Name, Int64 Phone, Int64 Phone2, Int64 HomeNo, string Address, Int64 NIC, string Description, Int64 ModifiedBy)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE Brokers SET Code = @Code, Name = @Name, Phone = @Phone, Phone2 = @Phone2, HomeNo = @HomeNo, Address = @Address, NIC = @NIC, Description = @Description, ModifiedBy = @ModifiedBy, ModifiedDate = GETDATE() WHERE ID = @BrokerID";

                commandData.AddParameter("@BrokerID", BrokerID);
                commandData.AddParameter("@Code", Code);
                commandData.AddParameter("@Name", Name);
                commandData.AddParameter("@Phone", Phone);
                commandData.AddParameter("@Phone2", Phone2);
                commandData.AddParameter("@HomeNo", HomeNo);
                commandData.AddParameter("@Address", Address);
                commandData.AddParameter("@NIC", NIC);
                commandData.AddParameter("@Description", Description);
                commandData.AddParameter("@ModifiedBy", ModifiedBy);

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

        public int ActivateBroker(Int64 BrokersID, Int64 ModifiedByID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE Brokers SET [isActive] = 'True' ,  [ModifiedBy] = '" + ModifiedByID + "', [ModifiedDate] = getdate() WHERE [ID] = " + BrokersID;


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

        public int DeactivateBroker(Int64 BrokersID, Int64 ModifiedByID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE Brokers SET [isActive] = 'False' ,  [ModifiedBy] = '" + ModifiedByID + "', [ModifiedDate] = getdate() WHERE [ID] = " + BrokersID;


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

        public int DeleteBroker(Int64 BrokerID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "DELETE FROM Brokers WHERE ID = @BrokerID";

                commandData.AddParameter("@BrokerID", BrokerID);

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
