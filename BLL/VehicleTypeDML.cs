using SqlDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class VehicleTypeDML
    {
        public DataTable GetVehicleTypeByName(string Name)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT * FROM VehicleType WHERE VehicleTypeName = '" + Name + "' ORDER BY VehicleTypeName ASC";

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

        public DataTable GetVehicleType()
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT * FROM VehicleType ORDER BY VehicleTypeName ASC";

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

        public DataTable GetVehicleType(Int64 VehicleTypeID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT * FROM VehicleType WHERE VehicleTypeID = @ID";

                _commnadData.AddParameter("@ID", VehicleTypeID);
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

        public DataTable GetVehicleType(string Keyword)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT * FROM VehicleType WHERE VehicleTypeCode LIKE '%" + Keyword + "%' OR VehicleTypeName LIKE '%" + Keyword + "%' OR DimensionUnitType LIKE '%" + Keyword + "%' OR LowerDeckInnerLength LIKE '%" + Keyword + "%' OR LowerDeckInnerWidth LIKE '%" + Keyword + "%' OR LowerDeckInnerHeight LIKE '%" + Keyword + "%' OR LowerDeckOuterLength LIKE '%" + Keyword + "%' OR LowerDeckOuterWidth LIKE '%" + Keyword + "%' OR LowerDeckOuterHeight LIKE '%" + Keyword + "%' OR UpperDeckInnerLength LIKE '%" + Keyword + "%' OR UpperDeckInnerWidth LIKE '%" + Keyword + "%'  OR UpperDeckInnerHeight LIKE '%" + Keyword + "%'  OR UpperDeckOuterLength LIKE '%" + Keyword + "%'  OR UpperDeckOuterWidth LIKE '%" + Keyword + "%'  OR UpperDeckOuterHeight LIKE '%" + Keyword + "%' OR UpperPortionInnerLength LIKE '%" + Keyword + "%' OR UpperPortionInnerwidth LIKE '%" + Keyword + "%'   OR UpperPortionInnerHeight LIKE '%" + Keyword + "%'  OR LowerPortionInnerWidth LIKE '%" + Keyword + "%'  OR LowerPortionInnerHeight LIKE '%" + Keyword + "%'  OR LowerPortionInnerLength LIKE '%" + Keyword + "%'  OR PermisibleHeight LIKE '%" + Keyword + "%'  OR PermisibleLength LIKE '%" + Keyword + "%'";

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

        public DataTable GetVehicleType(string Code, string Name, string UnitType)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT * FROM VehicleType WHERE VehicleTypeCode = '" + Code + "' AND VehicleTypeName = '" + Name + "' AND DimensionUnitType = '" + UnitType + "' ";

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

        public int InsertVehicleType(string Code, string Name, string UnitType, decimal lowerDeckInnerLength, decimal LowerDeckInnerWidth, decimal LowerDeckInnerHeight, decimal LowerDeckOuterLength, decimal LowerDeckOuterWidth, decimal LowerDeckOuterHeight, decimal UpperDeckInnerLength, decimal UpperDeckInnerWidth, decimal UpperDeckInnerHeight, decimal UpperDeckOuterLength, decimal UpperDeckOuterWidth, decimal UpperDeckOuterHeight, decimal UpperPortionInnerLength, decimal UpperPortionInnerwidth, decimal UpperPortionInnerHeight, decimal LowerPortionInnerWidth, decimal LowerPortionInnerLength, decimal LowerPortionInnerHeight, decimal PermisibleHeight, decimal PermisibleLength, string Description, Int64 CreatedBy)
        {
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "INSERT INTO VehicleType (VehicleTypeCode, VehicleTypeName, DimensionUnitType,LowerDeckInnerLength, LowerDeckInnerWidth , LowerDeckInnerHeight ,  LowerDeckOuterLength , LowerDeckOuterWidth , LowerDeckOuterHeight , UpperDeckInnerLength , UpperDeckInnerWidth , UpperDeckInnerHeight ,UpperDeckOuterLength , UpperDeckOuterWidth , UpperDeckOuterHeight , UpperPortionInnerLength , UpperPortionInnerwidth , UpperPortionInnerHeight, LowerPortionInnerWidth , LowerPortionInnerLength ,LowerPortionInnerHeight , PermisibleHeight , PermisibleLength ,  [Description],  CreatedByUserID, DateCreated) VALUES (@Code, @Name, @UnitType,@lowerDeckInnerLength, @LowerDeckInnerWidth ,@LowerDeckInnerHeight, @LowerDeckOuterLength,@LowerDeckOuterWidth , @LowerDeckOuterHeight, @UpperDeckInnerLength , @UpperDeckInnerWidth, @UpperDeckInnerHeight ,@UpperDeckOuterLength , @UpperDeckOuterWidth,@UpperDeckOuterHeight ,@LowerPortionInnerLength, @UpperPortionInnerwidth, @UpperPortionInnerHeight,@LowerPortionInnerWidth,@LowerPortionInnerLength , @LowerPortionInnerHeight , @PermisibleHeight , @PermisibleLength,  @Description, @CreatedBy, GETDATE()); SELECT SCOPE_IDENTITY();";

                commandData.AddParameter("@Code", Code);
                commandData.AddParameter("@Name", Name);
                commandData.AddParameter("@UnitType", UnitType);
                commandData.AddParameter("@lowerDeckInnerLength", lowerDeckInnerLength);
                commandData.AddParameter("@LowerDeckInnerWidth", LowerDeckInnerWidth);
                commandData.AddParameter("@LowerDeckInnerHeight", LowerDeckInnerHeight);
                commandData.AddParameter("@LowerDeckOuterLength", LowerDeckOuterLength);
                commandData.AddParameter("@LowerDeckOuterWidth", LowerDeckOuterWidth);
                commandData.AddParameter("@LowerDeckOuterHeight", LowerDeckOuterHeight);
                commandData.AddParameter("@UpperDeckInnerLength", UpperDeckInnerLength);
                commandData.AddParameter("@UpperDeckInnerWidth", UpperDeckInnerWidth);
                commandData.AddParameter("@UpperDeckInnerHeight", UpperDeckInnerHeight);
                commandData.AddParameter("@UpperDeckOuterLength", UpperDeckOuterLength);
                commandData.AddParameter("@UpperDeckOuterWidth", UpperDeckOuterWidth);
                commandData.AddParameter("@UpperDeckOuterHeight", UpperDeckOuterHeight);
                commandData.AddParameter("@UpperPortionInnerLength", UpperPortionInnerLength);
                commandData.AddParameter("@UpperPortionInnerwidth", UpperPortionInnerwidth);
                commandData.AddParameter("@UpperPortionInnerHeight", UpperPortionInnerHeight);
                commandData.AddParameter("@LowerPortionInnerWidth", LowerPortionInnerWidth);
                commandData.AddParameter("@LowerPortionInnerLength", LowerPortionInnerLength);
                commandData.AddParameter("@LowerPortionInnerHeight", LowerPortionInnerHeight);
                commandData.AddParameter("@PermisibleHeight", PermisibleHeight);
                commandData.AddParameter("@PermisibleLength", PermisibleLength);
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

        public int UpdateVehicleType(Int64 VehicleTypeID, string Code, string Name, string UnitType, decimal lowerDeckInnerLength, decimal LowerDeckInnerWidth, decimal LowerDeckInnerHeight, decimal LowerDeckOuterLength, decimal LowerDeckOuterWidth, decimal LowerDeckOuterHeight, decimal UpperDeckInnerLength, decimal UpperDeckInnerWidth, decimal UpperDeckInnerHeight, decimal UpperDeckOuterLength, decimal UpperDeckOuterWidth, decimal UpperDeckOuterHeight, decimal UpperPortionInnerLength, decimal UpperPortionInnerwidth, decimal UpperPortionInnerHeight, decimal LowerPortionInnerWidth, decimal LowerPortionInnerLength, decimal LowerPortionInnerHeight, decimal PermisibleHeight, decimal PermisibleLength, string Description,  Int64 ModifiedBy)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE VehicleType SET VehicleTypeCode = @Code, VehicleTypeName = @Name, DimensionUnitType = @UnitType, lowerDeckInnerLength = @lowerDeckInnerLength, LowerDeckInnerWidth = @LowerDeckInnerWidth, LowerDeckInnerHeight = @LowerDeckInnerHeight, LowerDeckOuterWidth = @LowerDeckOuterWidth, LowerDeckOuterHeight = @LowerDeckOuterHeight, LowerDeckOuterLength = @LowerDeckOuterLength ,UpperDeckInnerLength = @UpperDeckInnerLength, UpperDeckInnerWidth = @UpperDeckInnerWidth, UpperDeckInnerHeight = @UpperDeckInnerHeight , UpperDeckOuterLength = @UpperDeckOuterLength, UpperDeckOuterWidth = @UpperDeckOuterWidth , UpperDeckOuterHeight = @UpperDeckOuterHeight, UpperPortionInnerLength = @UpperPortionInnerLength, UpperPortionInnerwidth = @UpperPortionInnerwidth, UpperPortionInnerHeight = @UpperPortionInnerHeight, LowerPortionInnerWidth = @LowerPortionInnerWidth , LowerPortionInnerLength = @LowerPortionInnerLength, LowerPortionInnerHeight = @LowerPortionInnerHeight, PermisibleHeight = @PermisibleHeight,  PermisibleLength = @PermisibleLength ,    Description = @Description, ModifiedByUserID = @ModifiedBy, DateModified = GETDATE() WHERE VehicleTypeID = @VehicleTypeID";

                commandData.AddParameter("@VehicleTypeID", VehicleTypeID);
                commandData.AddParameter("@Code", Code);
                commandData.AddParameter("@Name", Name);
                commandData.AddParameter("@UnitType", UnitType);
                commandData.AddParameter("@lowerDeckInnerLength", lowerDeckInnerLength);
                commandData.AddParameter("@LowerDeckInnerWidth", LowerDeckInnerWidth);
                commandData.AddParameter("@LowerDeckInnerHeight", LowerDeckInnerHeight);
                commandData.AddParameter("@LowerDeckOuterLength", LowerDeckOuterLength);
                commandData.AddParameter("@LowerDeckOuterWidth", LowerDeckOuterWidth);
                commandData.AddParameter("@LowerDeckOuterHeight", LowerDeckOuterHeight);
                commandData.AddParameter("@UpperDeckInnerLength", UpperDeckInnerLength);
                commandData.AddParameter("@UpperDeckInnerWidth", UpperDeckInnerWidth);
                commandData.AddParameter("@UpperDeckInnerHeight", UpperDeckInnerHeight);
                commandData.AddParameter("@UpperDeckOuterLength", UpperDeckOuterLength);
                commandData.AddParameter("@UpperDeckOuterWidth", UpperDeckOuterWidth);
                commandData.AddParameter("@UpperDeckOuterHeight", UpperDeckOuterHeight);
                commandData.AddParameter("@UpperPortionInnerLength", UpperPortionInnerLength);
                commandData.AddParameter("@UpperPortionInnerwidth", UpperPortionInnerwidth);
                commandData.AddParameter("@UpperPortionInnerHeight", UpperPortionInnerHeight);
                commandData.AddParameter("@LowerPortionInnerWidth", LowerPortionInnerWidth);
                commandData.AddParameter("@LowerPortionInnerLength", LowerPortionInnerLength);
                commandData.AddParameter("@LowerPortionInnerHeight", LowerPortionInnerHeight);
                commandData.AddParameter("@PermisibleHeight", PermisibleHeight);
                commandData.AddParameter("@PermisibleLength", PermisibleLength);
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


        public int ActivateVehicleType(Int64 ID, Int64 ModifiedByID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE VehicleType SET [isActive] = 'True' ,  [ModifiedByUserID] = '" + ModifiedByID + "', [DateModified] = getdate() WHERE [VehicleTypeID] = " + ID;


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

        public int DeactivateVehicleType(Int64 ID, Int64 ModifiedByID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE VehicleType SET [isActive] = 'False' ,  [ModifiedByUserID] = '" + ModifiedByID + "', [DateModified] = getdate() WHERE [VehicleTypeID] = " + ID;


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
        public int DeleteVehicleType(Int64 VehicleTypeID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "DELETE FROM VehicleType WHERE VehicleTypeID = @VehicleTypeID";

                commandData.AddParameter("@VehicleTypeID", VehicleTypeID);

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
