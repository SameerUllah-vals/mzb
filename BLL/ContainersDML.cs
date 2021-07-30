using SqlDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ContainersDML
    {
        public DataTable GetContainer()
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT c.*, ct.ContainerType AS ContainerType FROM Container c INNER JOIN ContainerType ct ON ct.ContainerTypeID = c.[Type] ORDER BY ct.ContainerType ASC;";

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

        public DataTable GetContainer(Int64 ContainerID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT c.*, ct.ContainerType AS ContainerType FROM Container c INNER JOIN ContainerType ct ON ct.ContainerTypeID = c.[Type] where c.ID= '"+ContainerID+"' ORDER BY ct.ContainerType ASC;";

                _commnadData.AddParameter("@ContainerID", ContainerID);

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

        public DataTable GetContainer(string keyword)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT c.*, ct.ContainerType AS ContainerType FROM Container c INNER JOIN ContainerType ct ON ct.ContainerTypeID = c.[Type] WHERE c.ContainerNo LIKE '%" + keyword + "%' OR c.Code LIKE '%" + keyword + "%' OR ct.ContainerTypeID LIKE '%" + keyword + "%' OR c.ShippingCompany LIKE '%" + keyword + "%' OR c.Owner LIKE '%" + keyword + "%' OR c.OwnerContact LIKE '%" + keyword + "%' OR c.OwnershipType LIKE '%" + keyword + "%' ORDER BY ct.ContainerType ASC;";

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

        public DataTable GetContainer(string Code, Int64 contact, string containerNo)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT * FROM Container WHERE (Code = @Code OR OwnerContact = @OwnerContact OR ContainerNo = @ContainerNo) AND isActive = 1";

                _commnadData.AddParameter("@Code", Code);
                _commnadData.AddParameter("@OwnerContact", contact);
                _commnadData.AddParameter("@ContainerNo", containerNo);

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

        public DataTable GetContainerType()
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT * FROM ContainerType ORDER BY ContainerType ASC";

                //_commnadData.AddParameter("@keyword", keyword);

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

        public int InsertContainer(string ContainerNo, string Code, Int64 Type, string ShippingCompany, string Owner, Int64 OwnerContact, string Description,  string OwnershipType, Int64 CreatedBy)
        {
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "INSERT INTO Container (Containerno, Code, Type, ShippingCompany, Owner, OwnerContact, Description,  OwnershipType, CreatedBy, CreatedDate) VALUES (@ContainerNo, @Code, @Type, @ShippingCompany, @Owner, @OwnerContact, @Description, @OwnershipType, @CreatedBy, GETDATE()); SELECT SCOPE_IDENTITY();";

                commandData.AddParameter("@ContainerNo", ContainerNo);
                commandData.AddParameter("@Code", Code);
                commandData.AddParameter("@Type", Type);
                commandData.AddParameter("@ShippingCompany", ShippingCompany);
                commandData.AddParameter("@Owner", Owner);
                commandData.AddParameter("@OwnerContact", OwnerContact);
                commandData.AddParameter("@Description", Description);
                commandData.AddParameter("@OwnershipType", OwnershipType);
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

        public int UpdateContainer(Int64 ContainerID, string ContainerNo, string Code, Int64 Type, string ShippingCompany, string Owner, Int64 OwnerContact, string Description, string OwnershipType, Int64 ModifiedBy)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE Container SET ContainerNo = @ContainerNo, Code = @Code, Type = @Type, ShippingCompany = @ShippingCompany, Owner = @Owner, OwnerContact = @OwnerContact, Description = @Description, OwnershipType = @OwnershipType, ModifiedBy = @ModifiedBy, ModifiedDate = GETDATE() WHERE ID = @ContainerID;";

                commandData.AddParameter("@ContainerID", ContainerID);
                commandData.AddParameter("@ContainerNo", ContainerNo);
                commandData.AddParameter("@Code", Code);
                commandData.AddParameter("@Type", Type);
                commandData.AddParameter("@ShippingCompany", ShippingCompany);
                commandData.AddParameter("@Owner", Owner);
                commandData.AddParameter("@OwnerContact", OwnerContact);
                commandData.AddParameter("@Description", Description);
                commandData.AddParameter("@OwnershipType", OwnershipType);
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

     

        public int ActivateContainer(Int64 ContID, Int64 ModifiedByID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE Container SET [isActive] = 'True' ,  [ModifiedBy] = '" + ModifiedByID + "', [ModifiedDate] = getdate() WHERE [ID] = " + ContID;


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
        public int DeactivateContainer(Int64 ContID, Int64 ModifiedByID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE Container SET [isActive] = 'False' ,  [ModifiedBy] = '" + ModifiedByID + "', [ModifiedDate] = getdate() WHERE [ID] = " + ContID;


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
        public int DeleteContainer(Int64 ContainerID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "DELETE FROM Container WHERE ID = @ContainerID";

                commandData.AddParameter("@ContainerID", ContainerID);

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
