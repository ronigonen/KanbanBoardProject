using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;


namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    internal class TaskController
    {
        private const string MessageTableName = "Forum";
        private readonly string _connectionString;
        private readonly string _tableName;
        internal TaskController()
        {
            string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "Kanban.db"));
            this._connectionString = $"Data Source={path}; Version=3;";
            this._tableName = MessageTableName;
        }
        public List<UserDTO> Select()
        {
            List<UserDTO> results = new List<UserDTO>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
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
        public bool Insert(TaskDTO task)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                int res = -1;
                SQLiteCommand command = new SQLiteCommand(null, connection);
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO{Tasks} ({task.Id} ,{task.CreationTime}, {task.Id} ,{task.DueDate}, {task.Title}, {task.Description}, {task.EmailAssignee}, {0}) " + $"VALUES (@idVal,@nameVal);";
                    SQLiteParameter idParam = new SQLiteParameter("@idVal", task.Id);
                    SQLiteParameter creationTimeParam = new SQLiteParameter("@creationTimeVal", task.CreationTime);
                    SQLiteParameter dueDateParam = new SQLiteParameter("@dueDateVal", task.DueDate);
                    SQLiteParameter titleParam = new SQLiteParameter("@titleVal", task.Title);
                    SQLiteParameter descriptionParam = new SQLiteParameter("@descriptionVal", task.Description);
                    SQLiteParameter emailParam = new SQLiteParameter("@emailVal", task.EmailAssignee);
                    SQLiteParameter columnParam = new SQLiteParameter("@columnVal", 0);
                    command.Parameters.Add(idParam);
                    command.Parameters.Add(creationTimeParam);
                    command.Parameters.Add(dueDateParam);
                    command.Parameters.Add(titleParam);
                    command.Parameters.Add(descriptionParam);
                    command.Parameters.Add(emailParam);
                    command.Parameters.Add(columnParam);
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
                    CommandText = $"delete from {Users} where email={DTOObj.Email}"
                };
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
        public TaskDTO ConvertReaderToObject(SQLiteDataReader reader)
        {
            DateTime creation = DateTime.Parse(reader.GetString(1));
            DateTime dueDate = DateTime.Parse(reader.GetString(1));
            return new TaskDTO(reader.GetInt32(0), creation, dueDate, reader.GetString(3), reader.GetString(4));
        }

    }
}
