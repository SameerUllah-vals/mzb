using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace SqlDataAccess_BankAccounts
{
    public enum ExecutionType
    {
        ExecuteNonQuery,
        ExecuteScalar,
        ExecuteReader,
        ExecuteDataSet
    }

    public class CommandData
    {
        SqlCommand cmd = new SqlCommand();
        SqlConnection conn = new SqlConnection();
        SqlTransaction trans;
        SqlDataAdapter da;
        DataSet ds;
        SqlDataReader dr;
        string ConnectionString;

        public CommandData()
        {
            //this.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
            //this.ConnectionString = "Data Source=localhost;Initial Catalog=AGTC;user id=sa;password=Saqib123456;Integrated Security=False; timeout=0;";
            this.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BackAccountsConnection"].ConnectionString;
            conn.ConnectionString = ConnectionString;
            cmd.Connection = conn;
            //cmd.Transaction = trans;
            cmd.CommandTimeout = 10000;
            cmd.CommandType = CommandType.StoredProcedure;
        }

        public CommandData(string connString)
        {
            this.ConnectionString = connString;
            conn.ConnectionString = ConnectionString;
            cmd.Connection = conn;
            //cmd.Transaction = trans;
            cmd.CommandType = CommandType.StoredProcedure;
        }

        public string CommandText
        {
            set
            {
                cmd.CommandText = value;
            }
        }

        public void AddParameter(string ParameterName, object ParameterValue)
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = ParameterName;
            param.Value = ParameterValue;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

        }

        public void AddParameter(string ParameterName, object ParameterValue, ParameterDirection direction)
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = ParameterName;
            param.Value = ParameterValue;
            param.Direction = direction;
            cmd.Parameters.Add(param);

        }

        public object Execute(ExecutionType e)
        {
            object o = -1;

            switch (e)
            {
                case ExecutionType.ExecuteNonQuery:
                    o = ExecuteNonQuery();
                    break;

                case ExecutionType.ExecuteScalar:
                    o = ExecuteScalar();
                    break;

                case ExecutionType.ExecuteDataSet:
                    o = ExecuteDataSet();
                    break;

                case ExecutionType.ExecuteReader:
                    o = ExecuteReader();
                    break;

            }//end of switch

            cmd.Parameters.Clear();

            return o;
        }//end of method

        private int ExecuteNonQuery()
        {
            //			try
            //			{
            //conn.Open();

            int nRows = cmd.ExecuteNonQuery();
            //conn.Close();
            return nRows;
            //			}
            //			catch(Exception exc)
            //			{
            //				Console.WriteLine(exc.Message);
            //				return -1;
            //			}
        }

        private object ExecuteScalar()
        {
            //conn.Open();
            object o = cmd.ExecuteScalar();
            //conn.Close();
            return o;
        }

        private DataSet ExecuteDataSet()
        {
            da = new SqlDataAdapter();
            ds = new DataSet();
            da.SelectCommand = cmd;
            cmd.Connection = conn;
            da.Fill(ds, cmd.CommandText);
            return ds;
        }

        private SqlDataReader ExecuteReader()
        {
            //conn.Open();
            //dr =  SqlDataReader();
            dr = cmd.ExecuteReader();
            //conn.Close();
            return dr;
        }

        public SqlParameterCollection Parameters
        {
            get
            {
                return cmd.Parameters;
            }
        }

        public void Open()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
                trans = BeginTrans();
                cmd.Transaction = trans;
            }
        }

        public void OpenWithOutTrans()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
        }


        public void Close()
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }

        public CommandType _CommandType
        {
            get { return cmd.CommandType; }
            set { cmd.CommandType = value; }
        }

        public SqlTransaction BeginTrans()
        {
            return conn.BeginTransaction();
        }

        public void Commit()
        {
            trans.Commit();
        }

        public void RollBack()
        {
            trans.Rollback();
        }

    }

    public class CommandWrapper
    {
        public static string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BackAccountsConnection"].ConnectionString;

        public static CommandData CommandObj;

        public static void Initialize()
        {
            if (CommandObj == null)
            {
                CommandObj = new CommandData();
            }
        }
    }
}
