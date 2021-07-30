using SqlDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ItemDML
    {
        public DataTable GetItem()
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                //_commnadData.CommandText = " SELECT I.* FROM Item I ORDER BY I.itemName ASC";
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

        public DataTable GetItem(Int64 ItemID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT I.*  FROM Item I WHERE I.[ItemID] = '" + ItemID + "' ORDER BY I.itemName ASC";
                _commnadData.AddParameter("@ItemID", ItemID);

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

        public DataTable GetItem(string Code, string Name)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT * FROM Item where ItemCode = '" + Code + "' OR ItemName = '" + Name + "'";

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

        public DataTable GetItem(string Keyword)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT I.* FROM Item I WHERE I.ItemCode like '%" + Keyword + "%' or I.ItemName like '%" + Keyword + "%'  or  I.[Description] like '%" + Keyword + "%' ORDER BY I.itemName ASC;";

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

        public DataTable GetApplications(Int64 LoginID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT * FROM ApplicationAccess aa INNER JOIN Application a ON a.ApplicationID = aa. ApplicationID WHERE aa.LoginID = " + LoginID;

                //Adding Parameters
                // _commnadData.AddParameter("@UserName", userID);

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

        public int InsertItem(string ItemCode, string ItemName, string IsGeneralItem, string Description, Int64 CreatedBy)
        {
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "INSERT INTO Item ([ItemCode], [ItemName], [IsGeneralItem], [Description], [CreatedByUserID],[DateCreated]) VALUES ('" + ItemCode + "', '" + ItemName + "', '" + IsGeneralItem + "',  '" + Description + "' , '" + CreatedBy + "', getdate()); SELECT SCOPE_IDENTITY();";

                //commandData.AddParameter("@ItemCode", code);
                //commandData.AddParameter("@ItemName", Name);
                //commandData.AddParameter("@DateCreated", DateCreated);
                //commandData.AddParameter("@CreatedByUserID", CreatedBy);
                //commandData.AddParameter("@IsActive", Active);
                //commandData.AddParameter("@Description", Description);
                //commandData.AddParameter("@IsGeneralItem", General);
                //commandData.AddParameter("@OwnerID", OwnerID);

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

        public int UpdateItem(Int64 ItemID, string ItemCode, string ItemName, string IsGeneralItem, string Description, Int64 modifiedBy)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE Item SET [ItemCode] = '" + ItemCode + "', [ItemName] = '" + ItemName + "', [IsGeneralItem] = '" + IsGeneralItem + "',  [Description] = '" + Description + "' ,  [ModifiedByUserID] = '" + modifiedBy + "', [DateModified] = getdate() WHERE [ItemID] = " + ItemID;

                //commandData.AddParameter("@ItemID", ItemID);
                //commandData.AddParameter("@ItemCode", code);
                //commandData.AddParameter("@ItemName", Name);
                //commandData.AddParameter("@DateModified", DateModified);
                //commandData.AddParameter("@ModifiedByUserID", CreatedBy);
                //commandData.AddParameter("@IsActive", Active);
                //commandData.AddParameter("@Description", Description);
                //commandData.AddParameter("@IsGeneralItem", General);
                //commandData.AddParameter("@OwnerID", OwnerID);

                //opening connection
                commandData.OpenWithOutTrans();



                //@ItemID		bigint,
                //@ItemCode	varchar(50),
                //@ItemName	varchar(50),
                //@DateModified		datetime,
                //@ModifiedByUserID	bigint,
                //@IsActive		bit,
                //@Description	varchar(500),
                //@IsGeneralItem	bit,
                //@OwnerID		bigint

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

        public int ActivateItem(Int64 ItemID, Int64 ModifiedByID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE Item SET [isActive] = 'True' ,  [ModifiedByUserID] = '" + ModifiedByID + "', [DateModified] = getdate() WHERE [ItemID] = " + ItemID;


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

        public int DeactivateItem(Int64 ItemID, Int64 ModifiedByID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE Item SET [isActive] = 'False' ,  [ModifiedByUserID] = '" + ModifiedByID + "', [DateModified] = getdate() WHERE [ItemID] = " + ItemID;


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
        public int DeleteItemByID(Int64 ItemID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "Delete from Item where ItemID = " + ItemID;

                commandData.AddParameter("@ItemID", ItemID);

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
