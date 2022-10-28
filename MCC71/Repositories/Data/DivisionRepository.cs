using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCC71.Context;
using MCC71.Models;
using MCC71.Repositories.Interface;

namespace MCC71.Repositories.Data
{
    public class DivisionRepository : IDivisionRepository
    {
        SqlConnection sqlConnection;
        public int Delete(int id)
        {
            int result = 0;

            using (sqlConnection = new SqlConnection(MyContext.GetConnection()))
            {
                sqlConnection.Open();

                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();

                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;

                try
                {
                    sqlCommand.CommandText = "DELETE FROM Division WHERE ID = @id";

                    SqlParameter parameterName = new SqlParameter();
                    SqlParameter parameterId = new SqlParameter();

                    parameterId.ParameterName = "@id";
                    parameterId.SqlDbType = SqlDbType.Int;
                    parameterId.Value = id;

                    sqlCommand.Parameters.Add(parameterId);

                    result = sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();

                    return result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    try
                    {
                        sqlTransaction.Rollback();
                    }
                    catch (Exception exRollback)
                    {
                        Console.WriteLine(exRollback.Message);
                    }
                }
                return result;
            }
        }

        public List<Division> Get()
        {
            sqlConnection = new SqlConnection(MyContext.GetConnection());
            List<Division> divisions = new List<Division>();

            try
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "SELECT * FROM Division";

                sqlConnection.Open();

                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            Division division = new Division(Convert.ToInt32(sqlDataReader[0]), sqlDataReader[1].ToString());
                            divisions.Add(division);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No Data Found");
                    }
                    sqlDataReader.Close();
                }
                sqlConnection.Close();                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }
            return divisions;
        }

        public Division Get(int id)
        {
            sqlConnection = new SqlConnection(MyContext.GetConnection());

            try
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "SELECT * FROM Division WHERE ID = @id";

                SqlParameter ParameterId = new SqlParameter();
                ParameterId.ParameterName = "@id";
                ParameterId.SqlDbType = SqlDbType.Int;
                ParameterId.Value = id;

                sqlCommand.Parameters.Add(ParameterId);

                sqlConnection.Open();

                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            var division = new Division(Convert.ToInt32(sqlDataReader[0]), sqlDataReader[1].ToString());
                            return division;
                        }
                    }
                    else
                    {
                        Console.WriteLine("No Data Found");
                    }
                    sqlDataReader.Close();
                }
                sqlConnection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException);
            }
            return null;
        }

        public int Insert(Division division)
        {
            int result = 0;
            using (sqlConnection = new SqlConnection(MyContext.GetConnection()))
            {
                sqlConnection.Open();

                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();

                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;

                try
                {
                    sqlCommand.CommandText = "INSERT INTO Division (Name) VALUES (@name)";

                    SqlParameter parameterName = new SqlParameter();
                    parameterName.ParameterName = "@name";
                    parameterName.SqlDbType = SqlDbType.NVarChar;
                    parameterName.Value = division.Name;                    

                    sqlCommand.Parameters.Add(parameterName);

                    result = sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();

                    return result;

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    try
                    {
                        sqlTransaction.Rollback();
                    }
                    catch (Exception exRollback)
                    {
                        Console.WriteLine(exRollback.Message);
                    }
                }
                return result;
            }
        }

        public int Update(Division division)
        {
            int result = 0;

            using (sqlConnection = new SqlConnection(MyContext.GetConnection()))
            {
                sqlConnection.Open();

                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();

                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;

                try
                {
                    sqlCommand.CommandText = "UPDATE Division SET Name = @name WHERE ID = @id";

                    SqlParameter parameterName = new SqlParameter();
                    SqlParameter parameterId = new SqlParameter();

                    parameterName.ParameterName = "@name";
                    parameterName.SqlDbType = SqlDbType.NVarChar;
                    parameterName.Value = division.Name;

                    parameterId.ParameterName = "@id";
                    parameterId.SqlDbType = SqlDbType.Int;
                    parameterId.Value = division.Id;

                    sqlCommand.Parameters.Add(parameterName);
                    sqlCommand.Parameters.Add(parameterId);

                    result = sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();

                    return result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    try
                    {
                        sqlTransaction.Rollback();
                    }
                    catch (Exception exRollback)
                    {
                        Console.WriteLine(exRollback.Message);
                    }
                }
                return result;
            }
        }
    }
}
