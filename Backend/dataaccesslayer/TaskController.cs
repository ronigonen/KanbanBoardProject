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
        private const string MessageTableName = "Tasks";
        private readonly string _connectionString;
        private readonly string _tableName;
        internal TaskController()
        {
            string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "Kanban.db"));
            this._connectionString = $"Data Source={path}; Version=3;";
            this._tableName = MessageTableName;
        }
        public List<TaskDTO> Select()
        {
            List<TaskDTO> results = new List<TaskDTO>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                command.CommandText = $"select * from {_tableName}";
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
        public List<TaskDTO> SelectAllTasks()
        {
            List<TaskDTO> result = Select().Cast<TaskDTO>().ToList();
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
                    command.CommandText = $"INSERT INTO {_tableName} (Id, CreationTime, DueDate, Title, Description, EmailAssignee, Column, BoardId) " + $"VALUES (@taskId, @creationTime, @dueDate, @title, @description, @email, @column, @boardId);";
                    SQLiteParameter idParam = new SQLiteParameter("@taskId", task.Id);
                    SQLiteParameter creationTimeParam = new SQLiteParameter("@creationTime", task.CreationTime);
                    SQLiteParameter dueDateParam = new SQLiteParameter("@dueDate", task.DueDate);
                    SQLiteParameter titleParam = new SQLiteParameter("@title", task.Title);
                    SQLiteParameter descriptionParam = new SQLiteParameter("@description", task.Description);
                    SQLiteParameter emailParam = new SQLiteParameter("@email", task.EmailAssignee);
                    SQLiteParameter columnParam = new SQLiteParameter("@column", 0);
                    SQLiteParameter boardIdParam = new SQLiteParameter("@boardId", task.BoardId);
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
                    CommandText = $"delete from {_tableName} where email={DTOObj.Email}"
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

        public bool AdvanceTask(TaskDTO task)
        {
            bool result = false;
            int col = task.ColumnOrdinal + 1;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                string sqlQuery = string.Empty;
                sqlQuery = $"UPDATE {_tableName} SET [Column] = @col WHERE TaskId = @taskId";

                using (SQLiteCommand command = new SQLiteCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@col", col);
                    command.Parameters.AddWithValue("@taskId", task.Id);
                    int rowsAffected = command.ExecuteNonQuery();
                    result = (rowsAffected > 0);
                }
            }
            return result;
        }

        public bool UpdateTaskDueDate(TaskDTO task, DateTime newDueDate)
        {
            bool result = false;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                string sqlQuery = $"UPDATE {_tableName} SET [DueDate] = @newDueDate WHERE TaskId = @taskId";

                using (SQLiteCommand command = new SQLiteCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@newDueDate", newDueDate.ToString());
                    command.Parameters.AddWithValue("@taskId", task.Id);
                    int rowsAffected = command.ExecuteNonQuery();
                    result = (rowsAffected > 0);
                }
            }
            return result;
        }

        public bool UpdateTaskTitle(TaskDTO task, string title)
        {
            bool result = false;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                string sqlQuery = string.Empty;
                sqlQuery = $"UPDATE {_tableName} SET [Title] = @title WHERE TaskId = @taskId";

                using (SQLiteCommand command = new SQLiteCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@title", title);
                    command.Parameters.AddWithValue("@taskId", task.Id);
                    int rowsAffected = command.ExecuteNonQuery();
                    result = (rowsAffected > 0);
                }
            }
            return result;
        }

        public bool UpdateTaskDescription(TaskDTO task, string description)
        {
            bool result = false;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                string sqlQuery = string.Empty;
                sqlQuery = $"UPDATE {_tableName} SET [Description] = @description WHERE TaskId = @taskId";

                using (SQLiteCommand command = new SQLiteCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@description", description);
                    command.Parameters.AddWithValue("@taskId", task.Id);
                    int rowsAffected = command.ExecuteNonQuery();
                    result = (rowsAffected > 0);
                }
            }
            return result;
        }

        public bool UpdateEmailAssignee(TaskDTO task, string email)
        {
            bool result = false;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                string sqlQuery = string.Empty;
                sqlQuery = $"UPDATE {_tableName} SET [EmailAssignee] = @email WHERE TaskId = @taskId";

                using (SQLiteCommand command = new SQLiteCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@taskId", task.Id);
                    int rowsAffected = command.ExecuteNonQuery();
                    result = (rowsAffected > 0);
                }
            }
            return result;
        }

        public TaskDTO ConvertReaderToObject(SQLiteDataReader reader)
        {
            DateTime creation = DateTime.Parse(reader.GetString(1));
            DateTime dueDate = DateTime.Parse(reader.GetString(1));
            return new TaskDTO(reader.GetInt32(0), creation, dueDate, reader.GetString(3), reader.GetString(4));
        }

    }
}
