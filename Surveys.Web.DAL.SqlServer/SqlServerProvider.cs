using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Surveys.Web.DAL.SqlServer
{
    public abstract class SqlServerProvider
    {
        public abstract string ConnectionString { get; set; }

        public virtual Task<int> ExecuteNonQueryAsync(string query, SqlParameter[] parameters = null, CommandType commandType = CommandType.Text)
        {
            Task<int> result;
            SqlConnection connection = new SqlConnection(ConnectionString);

            using (SqlCommand command = new SqlCommand(query,connection))
            {
                command.CommandType = commandType;
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                connection.Open();
                result = command.ExecuteNonQueryAsync();
                result.ContinueWith(t =>
                {
                    command.Connection = null;
                    if (connection != null && connection.State != ConnectionState.Closed)
                    {
                        connection.Close();
                        connection.Dispose();
                        connection = null;
                    }
                });
                return result;
            }
        }

        public virtual Task<SqlDataReader> ExecuteReaderAsync(string query, SqlParameter[] parameters = null,
            CommandType commandType = CommandType.Text)
        {
            Task<SqlDataReader> result;
            SqlConnection connection = new SqlConnection(ConnectionString);

            using (SqlCommand command = new SqlCommand(query,connection))
            {
                command.CommandType = commandType;
                if(parameters != null)
                    command.Parameters.AddRange(parameters);

                connection.Open();
                result = command.ExecuteReaderAsync(CommandBehavior.CloseConnection);
            }
            return result;
        }
    }
}