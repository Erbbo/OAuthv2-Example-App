using System;
using System.Configuration;
using System.Data.SqlClient;

namespace OAuth_React.Net.DbManager
{
    public class DataOperations<T> where T : class
    {
        private SqlCommand GetCommandFromConnection(string query, SqlConnection connection)
        {
            return new SqlCommand(query, connection);
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        }

        public int SaveChanges(T value)
        {
            int success = -1;
            var userName = value.GetType().GetProperty("UserName");
            var userToken = value.GetType().GetProperty("UserToken");

            var connection = GetConnection();
            var command = GetCommandFromConnection("INSERT INTO [User] (UserName, UserToken) VALUES (@UserName, @UserToken)", connection);
            connection.Open();
            using (connection)
            using (command)
            {
                var name = command.CreateParameter();
                name.ParameterName = "@UserName";
                name.Value = userName.GetValue(value);
                command.Parameters.Add(name);
                var token = command.CreateParameter();
                token.ParameterName = "@UserToken";
                token.Value = userToken.GetValue(value);
                command.Parameters.Add(token);
                success = command.ExecuteNonQuery();
            }

            return success;
        }

        public string FetchToken(string key)
        {
            string refresh = null;
            var connection = GetConnection();
            var command = GetCommandFromConnection("SELECT UserToken FROM [User] WHERE UserName = @UserName", connection);
            connection.Open();

            using (connection)
            using (command)
            {
                var parameter = command.CreateParameter();
                parameter.ParameterName = "@UserName";
                parameter.Value = key;
                command.Parameters.Add(parameter);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    refresh = reader["UserToken"].ToString();
                }

                reader.Close();
                reader.Dispose();
            }

            return refresh;
        }
    }
}