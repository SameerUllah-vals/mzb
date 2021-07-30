using SqlDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ProductDML
    {
        public DataTable GetProduct()
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                //_commnadData.CommandText = "SELECT P1.CODE,P1.ProductName, P.*, pr.PackageTypeName AS 'PackageType' FROM ProductDetail P INNER JOIN PackageType pr ON pr.PackageTypeID = P.PackagingType INNER JOIN PRODUCT P1 ON P.productid=p1.productid ORDER BY p.Name ASC;";
                //_commnadData.CommandText = "SELECT p.*, u.UserName AS  CreatedByUser, u.UserName AS  ModifiedByUser FROM Product p INNER JOIN UserAccounts u ON u.UserID = p.CreatedBy ORDER BY p.ProductName ASC ;";
                _commnadData.CommandText = "SELECT p.*, pt.PackageTypeName, cu.UserName AS CreatedByUser, mu.UserName AS ModifiedByUser FROM Product p INNER JOIN PackageType pt ON pt.PackageTypeID = p.PackageTypeID left JOIN UserAccounts cu ON p.CreatedBy = cu.UserID LEFT JOIN UserAccounts mu ON mu.UserID = p.ModifiedBy ORDER BY p.Name ASC; ";

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

        public DataTable GetProductDDL()
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                //_commnadData.CommandText = "SELECT P1.CODE,P1.ProductName, P.*, pr.PackageTypeName AS 'PackageType' FROM ProductDetail P INNER JOIN PackageType pr ON pr.PackageTypeID = P.PackagingType INNER JOIN PRODUCT P1 ON P.productid=p1.productid ORDER BY p.Name ASC;";
                //_commnadData.CommandText = "SELECT p.*, u.UserName AS  CreatedByUser, u.UserName AS  ModifiedByUser FROM Product p INNER JOIN UserAccounts u ON u.UserID = p.CreatedBy ORDER BY p.ProductName ASC ;";
                _commnadData.CommandText = "SELECT CONCAT(CONCAT(p.Code + ' | ' + p.Name + ' | ' + pt.PackageTypeName, ' | '), p.Weight) AS Product, p.ID FROM Product p INNER JOIN PackageType pt ON pt.PackageTypeID = p.PackageTypeID";

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

        public DataTable GetProduct(Int64 ID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                //_commnadData.CommandText = "SELECT P1.CODE,P1.ProductName, P.*, pr.PackageTypeName AS 'PackageType' FROM ProductDetail P INNER JOIN PackageType pr ON pr.PackageTypeID = P.PackagingType INNER JOIN PRODUCT P1 ON P.productid=p1.productid  where p.ID = '" + ID + "' ORDER BY p.Name ASC; ";
                _commnadData.CommandText = "SELECT p.*, pt.PackageTypeName FROM Product p INNER JOIN PackageType pt ON pt.PackageTypeID = p.PackageTypeID WHERE p.ID = " + ID;

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

        public DataTable GetProduct(string Keyword)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                //_commnadData.CommandText = "SELECT P1.CODE,P1.ProductName, P.*, pr.PackageTypeName AS 'PackageType' FROM ProductDetail P INNER JOIN PackageType pr ON pr.PackageTypeID = P.PackagingType INNER JOIN PRODUCT P1 ON P.productid=p.productid WHERE P1.code like '%" + Keyword + "%' or P1.ProductName like '%" + Keyword + "%'  or P.[PackagingType] like '%" + Keyword + "%' or P.[Category] like '%" + Keyword + "%' or P.[Gener] like '%" + Keyword + "%' or P.[Nature] like '%" + Keyword + "%' or P.[DimensionUnit] like '%" + Keyword + "%' or P.[Weight] like '%" + Keyword + "%' or pr.PackageTypeName like '%" + Keyword + "%' ORDER BY P.Name asc ";
                _commnadData.CommandText = "SELECT p.*, pt.PackageTypeName FROM Product p INNER JOIN PackageType pt ON pt.PackageTypeID = p.PackageTypeID WHERE p.Code LIKE '%" + Keyword + "%' OR p.Name LIKE '%" + Keyword + "%' OR pt.PackageTypeName LIKE '%" + Keyword + "%' OR p.DimensionUnit LIKE '%" + Keyword + "%' OR p.Weight LIKE '%" + Keyword + "%' OR p.Description LIKE '%" + Keyword + "%' OR p.Status LIKE '%" + Keyword + "%' OR p.Width LIKE '%" + Keyword + "%' OR p.Height LIKE '%" + Keyword + "%' OR p.Unit LIKE '%" + Keyword + "%' OR p.Volume LIKE '%" + Keyword + "%' OR p.Length LIKE '%" + Keyword + "%' ORDER BY p.Name ASC";

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

        public DataTable GetProduct(string Code, string Name)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                //_commnadData.CommandText = "SELECT P1.CODE,P1.ProductName, P.*, pr.PackageTypeName AS 'PackageType' FROM ProductDetail P INNER JOIN PackageType pr ON pr.PackageTypeID = P.PackagingType INNER JOIN PRODUCT P1 ON P.productid=p.productid where p1.Code = '" + Code + "' and p1.ProductName = '" + Name + "' Order By  p1.ProductName asc";
                _commnadData.CommandText = "SELECT * FROM Product WHERE (Code = '" + Code + "' OR Name = '" + Name + "') AND Status = 1";

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

        public int InsertProduct(string Code, string Name, Int64 PackagingType, string DimensionUnit, double Weight, double Width, double Height, double Length, double Volume, string Unit, string Description, Int64 CreatedBy)
        {
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                //commandData.CommandText = "INSERT INTO Product ([Code], [Name], [PackageTypeID], [Category], [Gener], [Nature] , [DimensionUnit], [Weight] , [Description] , [Status] , [CreatedBy],[CreatedDate]) VALUES ('" + Code + "', '" + Name + "', '" + PackagingType + "', '" + Category + "', '" + Gener + "', '" + Nature + "' , '" + DimensionUnit + "' , '" + Weight + "' , '" + Description + "' , '" + Active + "' ,   '" + CreatedBy + "', getdate())";
                commandData.CommandText = "INSERT INTO Product (Code, Name,  PackageTypeID, DimensionUnit, Unit, Weight, Width, Height, Length, Volume, Description, CreatedBy, CreatedDate) VALUES ('" + Code + "', '" + Name + "',  '" + PackagingType + "', '" + DimensionUnit + "', '"+Unit+"' , '" + Weight + "', '" + Width + "', '" + Height + "', '" + Length + "', '" + Volume + "', '" + Description + "', '" + CreatedBy + "', GETDATE()); SELECT SCOPE_IDENTITY();";



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

        public int UpdateProduct(Int64 ProductID, string Name, Int64 PackagingType, string DimensionUnit, double Weight, double Width, double Height, double Length, double Volume, string Unit, string Description, Int64 ModifiedBy)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                //commandData.CommandText = "UPDATE Product SET [Code] = '" + Code + "', [Name] = '" + Name + "', [PackagingType] = '" + PackagingType + "', [Category] = '" + Category + "', [Gener] = '" + Gener + "', [Nature] = '" + Nature + "' , [DimensionUnit] = '" + DimensionUnit + "' , [Weight] = '" + Weight + "' , [Description] = '" + Description + "' , [Status] = '" + Active + "' ,  [ModifiedBy] = '" + ModifiedBy + "', [ModifiedDate] = getdate() WHERE [ID] = " + ProductID;
                commandData.CommandText = "UPDATE Product SET Name = '" + Name + "', PackageTypeID = '" + PackagingType + "', DimensionUnit = '" + DimensionUnit + "', [Unit] = '"+Unit+"' , Weight = '" + Weight + "', Description = '" + Description + "', Width = '" + Width + "', Height = '" + Height + "', Volume = '" + Volume + "', Length = '" + Length + "', ModifiedBy = '" + ModifiedBy + "', ModifiedDate = GETDATE() WHERE ID = " + ProductID;


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

        public int ActivateProduct(Int64 ProID, Int64 ModifiedByID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE Product SET [Status] = 'True' ,  [ModifiedBy] = '" + ModifiedByID + "', [ModifiedDate] = getdate() WHERE [ID] = " + ProID;


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

        public int DeactivateProduct(Int64 ProID, Int64 ModifiedByID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE Product SET [Status] = 'False' ,  [ModifiedBy] = '" + ModifiedByID + "', [ModifiedDate] = getdate() WHERE [ID] = " + ProID;


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

        public int DeleteProduct(Int64 ID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "Delete from Product where ID =  " + ID;

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
