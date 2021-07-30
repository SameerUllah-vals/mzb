using SqlDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ShippingLineDML
    {
        public DataTable GetShippingLine()
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT * FROM ShippingLine ORDER BY Name asc";

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

        public DataTable GetShippingLine(Int64 ID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT * FROM ShippingLine WHERE ShippingLineID = '" + ID + "' ORDER BY ShippingLineID DESC";

                //_commnadData.AddParameter("@BrokerID", BrokerID);

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

        public DataTable GetShippingLine(string keyword)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT * FROM ShippingLine WHERE Code LIKE '%" + keyword + "%' OR Name LIKE '%" + keyword + "%' OR PrimaryContact LIKE '%" + keyword + "%' OR SecondaryContact LIKE '%" + keyword + "%' OR LandLine LIKE '%" + keyword + "%' OR EmailAddress LIKE '%" + keyword + "%' OR WebSite LIKE '%" + keyword + "%'     OR [Address] LIKE '%" + keyword + "%' OR [Description] LIKE '%" + keyword + "%' OR ContactPerson LIKE '%" + keyword + "%' ";

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

        public DataTable GetShippingLine(string Code, string Name)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT * FROM ShippingLine WHERE (Code = '" + Code + "' OR Name = '" + Name + "') AND isActive = 1";

                //_commnadData.AddParameter("@Code", Code);
                //_commnadData.AddParameter("@Phone", Phone);
                //_commnadData.AddParameter("@NIC", CNIC);

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

        public int InsertShippingLine(string Code, string Name, string ContactPerson, Int64 PrimaryContact, Int64 SecondaryContact, Int64 LandLine, string EmailAddress, string WebSite, string Address, string Description, double LOLOCharges, Int64 CreatedBy)
        {
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "INSERT INTO ShippingLine ([Code], Name, ContactPerson, [PrimaryContact], [SecondaryContact], LandLine,EmailAddress,WebSite,[Address],[Description], LiftOffLiftOnCharges,CreatedByID,DateCreated) VALUES ('" + Code + "', '" + Name + "', '" + ContactPerson + "', '" + PrimaryContact + "', '" + SecondaryContact + "', '" + LandLine + "' , '" + EmailAddress + "' ,'" + WebSite + "' , '" + Address + "','" + Description + "', " + LOLOCharges + " , '" + CreatedBy + "', getdate())";



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

        public int UpdateShippingLine(Int64 ShippingLineID, string Code, string Name, string ContactPerson, Int64 PrimaryContact, Int64 SecondaryContact, Int64 LandLine, string EmailAddress, string WebSite, string Address, string Description, double LOLOCharges, Int64 ModifiedBy)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE ShippingLine SET Code = '" + Code + "', Name = '" + Name + "', ContactPerson = '" + ContactPerson + "', [PrimaryContact] = '" + PrimaryContact + "', [SecondaryContact] = '" + SecondaryContact + "', LandLine = '" + LandLine + "' , EmailAddress = '" + EmailAddress + "' , [Website] = '" + WebSite + "' , [Address] = '" + Address + "', [Description] = '" + Description + "', LiftOffLiftOnCharges = " + LOLOCharges + "  ,  [ModifiedByID] = '" + ModifiedBy + "', [DateModified] = getdate() WHERE ShippingLineID = " + ShippingLineID;


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

        public int ActivateShipping(Int64 ID, Int64 ModifiedByID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE ShippingLine SET [isActive] = 'True' ,  [ModifiedByID] = '" + ModifiedByID + "', [DateModified] = getdate() WHERE [ShippingLineID] = " + ID;


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

        public int DeactivateShipping(Int64 ID, Int64 ModifiedByID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE ShippingLine SET [isActive] = 'False' ,  [ModifiedByID] = '" + ModifiedByID + "', [DateModified] = getdate() WHERE [ShippingLineID] = " + ID;


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
        public int DeleteShippingLine(Int64 BrokerID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "DELETE FROM ShippingLine WHERE ShippingLineID = '" + BrokerID + "' ";

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
