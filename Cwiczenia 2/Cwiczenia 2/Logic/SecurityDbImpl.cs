using Cwiczenia2.DTO;
using Cwiczenia2.Logic;
using Cwiczenia2.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Cwiczenia2.Logic
{
    public class SecurityDbImpl : ISecurityDb
    {
        public bool checkIfTokenExists(Guid token)
        {
            using (SqlConnection connection = new SqlConnection(SystemConsts.DB_ADDRESS))
            using (SqlCommand command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = SystemConsts.DB_SELECT_ALL_FROM_REFRESH_TOKENS;
                command.Parameters.AddWithValue("@token", token);
                var reader = command.ExecuteReader();
                if (!reader.Read())
                {
                    return false;
                }

                return true;
            }
        }

        public void createRefreshToken(Guid token)
        {
            using (SqlConnection connection = new SqlConnection(SystemConsts.DB_ADDRESS))
            using (SqlCommand command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = SystemConsts.DB_INSERT_REFRESH_TOKEN;
                command.Parameters.AddWithValue("@token", token);
                //command.Parameters.AddWithValue("@index", StudentIndex);
                command.ExecuteNonQuery();
            }


        }

        public string getSalt(string studentIndex)
        {
            using (SqlConnection connection = new SqlConnection(SystemConsts.DB_ADDRESS))
            using (SqlCommand command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = SystemConsts.DB_GET_SALT;
                command.Parameters.AddWithValue("@index", studentIndex);
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return reader["Salt"].ToString();
                }

                return null;
            }
        }

        public bool loginUser(LoginReq loginReq)
        {
            using (SqlConnection connection = new SqlConnection(SystemConsts.DB_ADDRESS))
            using (SqlCommand command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = SystemConsts.DB_SELECT_ALL_FROM_STUDENT_BY_INDEX_NUMBER_AND_PASSWORD;
                command.Parameters.AddWithValue("@index", loginReq.login);
                command.Parameters.AddWithValue("@password", loginReq.password);
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return true;
                }

                return false;
            }
        }
    }
}
