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
    public class DepartmentRepository : IDepartmentRepository
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
                    sqlCommand.CommandText = "DELETE FROM Department WHERE ID = @id";

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

        public List<Department> Get()
        {
            sqlConnection = new SqlConnection(MyContext.GetConnection());
            List<Department> departments = new List<Department>();

            try
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "SELECT * FROM Department";

                sqlConnection.Open();

                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            Department department = new Department(Convert.ToInt32(sqlDataReader[0]), sqlDataReader[1].ToString(), Convert.ToInt32(sqlDataReader[2]));
                            departments.Add(department);
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
            return departments;
        }

        public Department Get(int id)
        {
            sqlConnection = new SqlConnection(MyContext.GetConnection());

            try
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "SELECT * FROM Department WHERE ID = @id";

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
                            var department = new Department(Convert.ToInt32(sqlDataReader[0]), sqlDataReader[1].ToString(), Convert.ToInt32(sqlDataReader[2]));
                            return department;
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

        public int Insert(Department department)
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
                    sqlCommand.CommandText = "INSERT INTO Department (Nama, DivisionId) VALUES (@name, @divisionid)";

                    SqlParameter parameterName = new SqlParameter();
                    parameterName.ParameterName = "@name";
                    parameterName.SqlDbType = SqlDbType.NVarChar;
                    parameterName.Value = department.Nama;

                    SqlParameter parameterDivisionId = new SqlParameter();
                    parameterDivisionId.ParameterName = "@divisionid";
                    parameterDivisionId.SqlDbType = SqlDbType.Int;
                    parameterDivisionId.Value = department.DivisionId;

                    sqlCommand.Parameters.Add(parameterName);
                    sqlCommand.Parameters.Add(parameterDivisionId);

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

        public int Update(Department department)
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
                    sqlCommand.CommandText = "UPDATE Department SET Nama = @name WHERE ID = @id";

                    SqlParameter parameterName = new SqlParameter();
                    SqlParameter parameterId = new SqlParameter();

                    parameterName.ParameterName = "@name";
                    parameterName.SqlDbType = SqlDbType.NVarChar;
                    parameterName.Value = department.Nama;

                    parameterId.ParameterName = "@id";
                    parameterId.SqlDbType = SqlDbType.Int;
                    parameterId.Value = department.Id;

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
