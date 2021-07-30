using SqlDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ContainerTypeDML
    {
        public DataTable GetContainerTypeByName(string Name)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT * FROM ContainerType WHERE ContainerType = '" + Name + "' Order By ContainerType asc";

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
                _commnadData.CommandText = "SELECT * FROM ContainerType Order By ContainerType asc";

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
                _commnadData.CommandText = "SELECT * FROM VehicleType Order By VehicleTypeName asc";

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


        public DataTable GetContainerType(Int64 ID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT * FROM ContainerType where ContainerTypeID = '" + ID + "' Order By ContainerType asc";

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

        public DataTable GetContainerType(string Keyword)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT * FROM ContainerType WHERE Code LIKE '%" + Keyword + "%' OR ContainerType LIKE '%" + Keyword + "%' OR DimensionUnitType LIKE '%" + Keyword + "%' OR LowerDeckInnerLength LIKE '%" + Keyword + "%' OR LowerDeckInnerWidth LIKE '%" + Keyword + "%' OR LowerDeckInnerHeight LIKE '%" + Keyword + "%' OR LowerDeckOuterLength LIKE '%" + Keyword + "%' OR LowerDeckOuterWidth LIKE '%" + Keyword + "%' OR LowerDeckOuterHeight LIKE '%" + Keyword + "%' OR UpperDeckInnerLength LIKE '%" + Keyword + "%' OR UpperDeckInnerWidth LIKE '%" + Keyword + "%'  OR UpperDeckInnerHeight LIKE '%" + Keyword + "%'  OR UpperDeckOuterLength LIKE '%" + Keyword + "%'  OR UpperDeckOuterWidth LIKE '%" + Keyword + "%'  OR UpperDeckOuterHeight LIKE '%" + Keyword + "%' OR UpperPortionInnerLength LIKE '%" + Keyword + "%' OR UpperPortionInnerwidth LIKE '%" + Keyword + "%'   OR UpperPortionInnerHeight LIKE '%" + Keyword + "%'  OR LowerPortionInnerWidth LIKE '%" + Keyword + "%'  OR LowerPortionInnerHeight LIKE '%" + Keyword + "%'  OR LowerPortionInnerLength LIKE '%" + Keyword + "%'  OR CubicCapacity LIKE '%" + Keyword + "%'  OR PayLoadWeight LIKE '%" + Keyword + "%' OR TareWeight LIKE '%" + Keyword + "%' ";

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

        public DataTable GetContainerType(string Code, string Name)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT * FROM ContainerType where Code = '" + Code + "' or ContainerType = '" + Name + "' Order By ContainerType asc";

                _commnadData.AddParameter("@PickDropCode", Code);
                _commnadData.AddParameter("@PickDropLocationName", Name);

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

        public int InsertContainerType(string Code, string Name, string UnitType, decimal lowerDeckInnerLength, decimal LowerDeckInnerWidth, decimal LowerDeckInnerHeight, decimal LowerDeckOuterLength, decimal LowerDeckOuterWidth, decimal LowerDeckOuterHeight, decimal UpperDeckInnerLength, decimal UpperDeckInnerWidth, decimal UpperDeckInnerHeight, decimal UpperDeckOuterLength, decimal UpperDeckOuterWidth, decimal UpperDeckOuterHeight, decimal UpperPortionInnerLength, decimal UpperPortionInnerwidth, decimal UpperPortionInnerHeight, decimal LowerPortionInnerWidth, decimal LowerPortionInnerLength, decimal LowerPortionInnerHeight, decimal TareWeight, decimal PayloadWeight, decimal CubicCapacity, string Description, Int64 CreatedBy)
        {
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "INSERT INTO ContainerType(Code, ContainerType, DimensionUnitType,LowerDeckInnerLength, LowerDeckInnerWidth , LowerDeckInnerHeight ,  LowerDeckOuterLength , LowerDeckOuterWidth , LowerDeckOuterHeight , UpperDeckInnerLength , UpperDeckInnerWidth , UpperDeckInnerHeight ,UpperDeckOuterLength , UpperDeckOuterWidth , UpperDeckOuterHeight , UpperPortionInnerLength , UpperPortionInnerwidth , UpperPortionInnerHeight, LowerPortionInnerWidth , LowerPortionInnerLength ,LowerPortionInnerHeight , TareWeight , PayLoadWeight , CubicCapacity ,  [Description],  CreatedByUserID, DateCreated) VALUES (@Code, @Name, @UnitType,@lowerDeckInnerLength, @LowerDeckInnerWidth ,@LowerDeckInnerHeight, @LowerDeckOuterLength,@LowerDeckOuterWidth , @LowerDeckOuterHeight, @UpperDeckInnerLength , @UpperDeckInnerWidth, @UpperDeckInnerHeight ,@UpperDeckOuterLength , @UpperDeckOuterWidth,@UpperDeckOuterHeight ,@LowerPortionInnerLength, @UpperPortionInnerwidth, @UpperPortionInnerHeight,@LowerPortionInnerWidth,@LowerPortionInnerLength , @LowerPortionInnerHeight , @TareWeight , @PayloadWeight, @CubicCapacity ,  @Description, @CreatedBy, GETDATE()); SELECT SCOPE_IDENTITY();";

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
                commandData.AddParameter("@TareWeight", TareWeight);
                commandData.AddParameter("@PayloadWeight", PayloadWeight);
                commandData.AddParameter("@CubicCapacity", CubicCapacity);
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

        public int UpdateContainerType(Int64 ContainerTypeID, string Code, string Name, string UnitType, decimal lowerDeckInnerLength, decimal LowerDeckInnerWidth, decimal LowerDeckInnerHeight, decimal LowerDeckOuterLength, decimal LowerDeckOuterWidth, decimal LowerDeckOuterHeight, decimal UpperDeckInnerLength, decimal UpperDeckInnerWidth, decimal UpperDeckInnerHeight, decimal UpperDeckOuterLength, decimal UpperDeckOuterWidth, decimal UpperDeckOuterHeight, decimal UpperPortionInnerLength, decimal UpperPortionInnerwidth, decimal UpperPortionInnerHeight, decimal LowerPortionInnerWidth, decimal LowerPortionInnerLength, decimal LowerPortionInnerHeight, decimal TareWeight, decimal PayloadWeight, decimal CubicCapacity, string Description,  Int64 ModifiedBy)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE ContainerType SET Code = @Code, ContainerType = @Name, DimensionUnitType = @UnitType, lowerDeckInnerLength = @lowerDeckInnerLength, LowerDeckInnerWidth = @LowerDeckInnerWidth, LowerDeckInnerHeight = @LowerDeckInnerHeight, LowerDeckOuterWidth = @LowerDeckOuterWidth, LowerDeckOuterHeight = @LowerDeckOuterHeight, LowerDeckOuterLength = @LowerDeckOuterLength ,UpperDeckInnerLength = @UpperDeckInnerLength, UpperDeckInnerWidth = @UpperDeckInnerWidth, UpperDeckInnerHeight = @UpperDeckInnerHeight , UpperDeckOuterLength = @UpperDeckOuterLength, UpperDeckOuterWidth = @UpperDeckOuterWidth , UpperDeckOuterHeight = @UpperDeckOuterHeight, UpperPortionInnerLength = @UpperPortionInnerLength, UpperPortionInnerwidth = @UpperPortionInnerwidth, UpperPortionInnerHeight = @UpperPortionInnerHeight, LowerPortionInnerWidth = @LowerPortionInnerWidth , LowerPortionInnerLength = @LowerPortionInnerLength, LowerPortionInnerHeight = @LowerPortionInnerHeight, TareWeight = @TareWeight,  CubicCapacity = @CubicCapacity , PayLoadWeight = @PayLoadWeight , Description = @Description,  ModifiedByUserID = @ModifiedBy, DateModified = GETDATE() WHERE ContainerTypeID = @ContainerTypeID";

                commandData.AddParameter("@ContainerTypeID", ContainerTypeID);
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
                commandData.AddParameter("@TareWeight", TareWeight);
                commandData.AddParameter("@PayloadWeight", PayloadWeight);
                commandData.AddParameter("@CubicCapacity", CubicCapacity);
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

        public int ActivateContainer(Int64 ID, Int64 ModifiedByID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE ContainerType SET [isActive] = 'True' ,  [ModifiedByUserID] = '" + ModifiedByID + "', [DateModified] = getdate() WHERE [ContainerTypeID] = " + ID;


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

        public int DeactivateContainer(Int64 ID, Int64 ModifiedByID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE ContainerType SET [isActive] = 'False' ,  [ModifiedByUserID] = '" + ModifiedByID + "', [DateModified] = getdate() WHERE [ContainerTypeID] = " + ID;


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
        public int DeleteContainerType(Int64 ID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "Delete from ContainerType where ContainerTypeID =  " + ID;

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

