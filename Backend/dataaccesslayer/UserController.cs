using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    internal class UserController
    {
        private const string MessageTableName = "User";
        private readonly string _connectionString;
        private readonly string _tableName;
        internal UserController()
        {
            string path = Path.GetFullPath(Path.Combine
            (Directory.GetCurrentDirectory(), "Kanban.db"));
            this._connectionString = $"Data Source={path}; Version=3;";
            this._tableName = MessageTableName;
        }
        public List<UserDTO> Select()
        {
            List<UserDTO> results = new List<UserDTO>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null,connection);
                command.CommandText = $"select * from {_tableName};";
                SQLiteDataReader dataReader = null;
                try
                {
                    connection.Open();
                    dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        results.Add(ConvertReaderToObject(dataReader));
                    }
                }
                finally
                {
                    if (dataReader != null)
                    {
                        dataReader.Close();
                    }
                    command.Dispose();
                    connection.Close();
                }

            }
            return results;
        }
        public List<BoardDTO> SelectAllBoards()
        {
            List<BoardDTO> result = Select().Cast<BoardDTO>().ToList();
            return result;
        }
        public bool Insert(UserDTO user)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                int res = -1;
                SQLiteCommand command = new SQLiteCommand(null,connection);
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO{"Users"} ({ user.Email} ,{ user.Password}) " +$"VALUES (@idVal,@nameVal);";
                    SQLiteParameter emailParam = new SQLiteParameter(@"idVal", user.Email);
                    SQLiteParameter passwordParam = new SQLiteParameter(@"nameVal", user.Password);
                    command.Parameters.Add(emailParam);
                    command.Parameters.Add(passwordParam);
                    command.Prepare();
                    res = command.ExecuteNonQuery();
                }
                catch
                {
                    //log error
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
                return res > 0;
            }
        }

        public bool Delete(UserDTO DTOObj)
        {
            int res = -1;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                var command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"delete from {Users} where email={DTOObj.Email}"};
                try
                {
                    connection.Open();
                    res = command.ExecuteNonQuery();
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
            }
            return res > 0;
        }
        public UserDTO ConvertReaderToObject(SQLiteDataReader reader)
        {
            return new UserDTO(reader.GetString(0),reader.GetString(1));
        }


    }

}
