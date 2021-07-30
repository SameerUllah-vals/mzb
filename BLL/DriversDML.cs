using SqlDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class DriversDML
    {
        public DataTable GetDrivers()
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT d.*, di.Data FROM Driver d LEFT JOIN DriverImage di ON di.DriverID = d.ID ORDER BY d.Name ASC";

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

        public DataTable GetDrivers(Int64 ID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT * FROM Driver where id = '" + ID + "' Order By Name asc";

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

        public DataTable GetDrivers(string Keyword)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                //_commnadData.CommandText = "Select * from Driver where [Code] like '%" + Keyword + "%' or [Name] like '%" + Keyword + "%'  or [FatherName] like '%" + Keyword + "%' or [Type] like '%" + Keyword + "%' or [DateOfBirth] like '%" + Keyword + "%' or [Gender] like '%" + Keyword + "%' or [BloodGroup] like '%" + Keyword + "%' or [CellNo] like '%" + Keyword + "%' or [OtherContact] like '%" + Keyword + "%' or [HomeNo] like '%" + Keyword + "%' or [Address] like '%" + Keyword + "%' or [NIC] like '%" + Keyword + "%' or [IdentityMark] like '%" + Keyword + "%' or [NICIssueDate] like '%" + Keyword + "%' or [NICExpiryDate] like '%" + Keyword + "%' or [LicenseNo] like '%" + Keyword + "%' or [LicenseCategory] like '%" + Keyword + "%' or [LicenseIssueDate] like '%" + Keyword + "%' or [LicenseExpiryDate] like '%" + Keyword + "%' or [LicenseIssuingAuthority] like '%" + Keyword + "%' or [LicenseStatus] like '%" + Keyword + "%'  or [EmergencyContactName] like '%" + Keyword + "%'  or [EmergencyContactNo] like '%" + Keyword + "%'  or [ContactRelation] like '%" + Keyword + "'  or [Description] like '%" + Keyword + "%'  ";
                _commnadData.CommandText = "Select d.*, di.Data from Driver d LEFT JOIN DriverImage di ON di.DriverID = d.ID where d.[Code] like '%" + Keyword + "%' or d.[Name] like '%" + Keyword + "%'  or d.[FatherName] like '%" + Keyword + "%' or d.[Type] like '%" + Keyword + "%' or d.[DateOfBirth] like '%" + Keyword + "%' or d.[Gender] like '%" + Keyword + "%' or d.[BloodGroup] like '%" + Keyword + "%' or d.[CellNo] like '%" + Keyword + "%' or d.[OtherContact] like '%" + Keyword + "%' or d.[HomeNo] like '%" + Keyword + "%' or d.[Address] like '%" + Keyword + "%' or d.[NIC] like '%" + Keyword + "%' or d.[IdentityMark] like '%" + Keyword + "%' or d.[NICIssueDate] like '%" + Keyword + "%' or d.[NICExpiryDate] like '%" + Keyword + "%' or [LicenseNo] like '%" + Keyword + "%' or d.[LicenseCategory] like '%" + Keyword + "%' or d.[LicenseIssueDate] like '%" + Keyword + "%' or d.[LicenseExpiryDate] like '%" + Keyword + "%' or d.[LicenseIssuingAuthority] like '%" + Keyword + "%' or d.[LicenseStatus] like '%" + Keyword + "%'  or d.[EmergencyContactName] like '%" + Keyword + "%'  or d.[EmergencyContactNo] like '%" + Keyword + "%'  or d.[ContactRelation] like '%" + Keyword + "%'  or d.[Description] like '%" + Keyword + "%'  ";

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

        public DataTable GetDrivers(string Code, string Name)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT * FROM Driver where Code = '" + Code + "' or Name = '" + Name + "' Order By Name asc";

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

        public DataTable GetDrivers(string Code, string Name, Int64 CellNo, Int64 NIC, Int64 License)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT * FROM Driver WHERE Code = '" + Code + "' or Name = '" + Name + "' OR CellNo = " + CellNo + " OR NIC = " + NIC + " OR LicenseNo = " + License + " AND Active = 'True';";

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

        public DataTable GetDriverImages(Int64 DriverID)
        {
            //Creating object of DAL class
            CommandData _commnadData = new CommandData();

            try
            {
                _commnadData._CommandType = CommandType.Text;
                _commnadData.CommandText = "SELECT * FROM DriverImage where DriverID = '" + DriverID + "' Order By Name asc";

                _commnadData.AddParameter("@DriverID", DriverID);

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

        public int UpdateDriverImage(Int64 DriverID, string FileName, string ContentType, byte[] bytes)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE DriverImage SET Name = @Name, ContentType = @ContentType, Data = @Data WHERE DriverID = @DriverID";

                commandData.AddParameter("@DriverID", DriverID);
                commandData.AddParameter("@Name", FileName);
                commandData.AddParameter("@ContentType", ContentType);
                commandData.AddParameter("@Data", bytes);

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

        public int InsertDrivers(string Code, string Name, string FatherName, string Type, string DateOfBirth, string Gender, string BloodGroup, Int64 CellNo, Int64 OtherContact, Int64 HomeNo, string Address, Int64 NIC, string IdentityMark, string NICIssueDate, string NICExpiryDate, Int64 LicenseNo, string LicenseCategory, string LicenseIssueDate, string LicenseExpiryDate, string LicenseIssuingAuthority, string LicenseStatus, string EmergencyContactName, Int64 EmergencyContactNo, string ContactRelation, string Description,  Int64 CreatedBy, string Document)
        {
            CommandData _commandData = new CommandData();

            try
            {

                _commandData._CommandType = CommandType.Text;
                _commandData.CommandText = "INSERT INTO Driver (Code, Name, FatherName, Type, DateOfBirth, Gender, BloodGroup, CellNo, OtherContact, HomeNo, Address, NIC, IdentityMark, NICIssueDate, NICExpiryDate, LicenseNo, LicenseCategory, LicenseIssueDate, LicenseExpiryDate, LicenseIssuingAuthority , LicenseStatus, EmergencyContactName, EmergencyContactNo, ContactRelation, Description, CreatedBy, CreatedDate, Document) VALUES ('" + Code + "', '" + Name + "', '" + FatherName + "', '" + Type + "', '" + DateOfBirth + "', '" + Gender + "', '" + BloodGroup + "', " + CellNo + ", " + OtherContact + ", " + HomeNo + ", '" + Address + "', " + NIC + ", '" + IdentityMark + "', '" + NICIssueDate + "', '" + NICExpiryDate + "', " + LicenseNo + ", '" + LicenseCategory + "', '" + LicenseIssueDate + "', '" + LicenseExpiryDate + "', '" + LicenseIssuingAuthority + "',   '" + LicenseStatus + "', '" + EmergencyContactName + "', " + EmergencyContactNo + ", '" + ContactRelation + "', '" + Description + "',   " + CreatedBy + ", getdate(), '" + Document + "'); SELECT SCOPE_IDENTITY();";

                //_commandData.AddParameter("@Code", Code);
                //_commandData.AddParameter("@Name", Name);
                //_commandData.AddParameter("@Phone", Phone);
                //_commandData.AddParameter("@Phone2", Phone2);
                //_commandData.AddParameter("@HomeNo", HomeNo);
                //_commandData.AddParameter("@Address", Address);
                //_commandData.AddParameter("@NIC", NIC);
                //_commandData.AddParameter("@Active", isActive);
                //_commandData.AddParameter("@Description", Description);
                //_commandData.AddParameter("@CreatedBy", CreatedBy);

                _commandData.OpenWithOutTrans();

                Object valReturned = _commandData.Execute(ExecutionType.ExecuteScalar);

                return Convert.ToInt32(valReturned);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _commandData.Close();
            }
        }

        public int UpdateDrivers(Int64 DriverID, string Code, string Name, string FatherName, string Type, string DateOfBirth, string Gender, string BloodGroup, Int64 CellNo, Int64 OtherContact, Int64 HomeNo, string Address, Int64 NIC, string IdentityMark, string NICIssueDate, string NICExpiryDate, Int64 LicenseNo, string LicenseCategory, string LicenseIssueDate, string LicenseExpiryDate, string LicenseIssuingAuthority, string LicenseStatus, string EmergencyContactName, Int64 EmergencyContactNo, string ContactRelation, string Description,  Int64 ModifiedBy, string Document)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE Driver SET [Code] = '" + Code + "' ,[Name] = '" + Name + "' ,[FatherName] = '" + FatherName + "',[Type] = '" + Type + "',[DateOfBirth] = '" + DateOfBirth + "',[Gender] = '" + Gender + "',[BloodGroup] = '" + BloodGroup + "',[CellNo] = '" + CellNo + "',[OtherContact] = '" + OtherContact + "',[HomeNo] = '" + HomeNo + "',[Address] = '" + Address + "',[NIC] = '" + NIC + "',[IdentityMark] = '" + IdentityMark + "' , [NICIssueDate] = '" + NICIssueDate + "' , [NICExpiryDate] = '" + NICExpiryDate + "' , [LicenseNo] = '" + LicenseNo + "',[LicenseCategory] = '" + LicenseCategory + "',[LicenseIssueDate] = '" + LicenseIssueDate + "',[LicenseExpiryDate] = '" + LicenseExpiryDate + "',[LicenseIssuingAuthority] = '" + LicenseIssuingAuthority + "', [LicenseStatus] = '" + LicenseStatus + "',[EmergencyContactName] = '" + EmergencyContactName + "',[EmergencyContactNo] = '" + EmergencyContactNo + "',[ContactRelation] = '" + ContactRelation + "',[Description] = '" + Description + "' , [ModifiedBy] = '" + ModifiedBy + "' ,  ModifiedDate = getdate(), Document = '" + Document + "' WHERE ID = " + DriverID;

                //commandData.AddParameter("@BrokerID", BrokerID);
                //commandData.AddParameter("@Code", Code);
                //commandData.AddParameter("@Name", Name);
                //commandData.AddParameter("@Phone", Phone);
                //commandData.AddParameter("@Phone2", Phone2);
                //commandData.AddParameter("@HomeNo", HomeNo);
                //commandData.AddParameter("@Address", Address);
                //commandData.AddParameter("@NIC", NIC);
                //commandData.AddParameter("@isActive", isActive);
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

        public int ActivateDriver(Int64 ID, Int64 ModifiedByID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE Driver SET [Active] = 'True' ,  [ModifiedBy] = '" + ModifiedByID + "', [ModifiedDate] = getdate() WHERE [ID] = " + ID;


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

        public int DeactivateDriver(Int64 ID, Int64 ModifiedByID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "UPDATE Driver SET [Active] = 'False' ,  [ModifiedBy] = '" + ModifiedByID + "', [ModifiedDate] = getdate() WHERE [ID] = " + ID;


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

        public int DeleteDrivers(Int64 ID)
        {
            //Creating object of DAL class
            CommandData commandData = new CommandData();

            try
            {
                commandData._CommandType = CommandType.Text;
                commandData.CommandText = "DELETE FROM Driver WHERE ID = " + ID;

                //commandData.AddParameter("@BrokerID", BrokerID);

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
