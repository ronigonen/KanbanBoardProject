using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    internal class BoardController
    {
        private const string MessageTableName = "Forum";
        private readonly string _connectionString;
        private readonly string _tableName;
        internal BoardController()
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

        public bool Delete(TaskDTO DTOObj)
        {
            int res = -1;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                var command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"delete from {Tasks} where BoardId={DTOObj.BoardId}"
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

        public List<TaskDTO> SelectAllTasksInColumn(int col)
        {
            List<TaskDTO> result = new List<TaskDTO>();

            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();

                string sqlQuery = $"SELECT * FROM Tasks WHERE ColumnName = {col}";

                using (SQLiteCommand command = new SQLiteCommand(sqlQuery, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            DateTime creationTime = DateTime.Parse(reader.GetString(1));
                            DateTime dueDate = DateTime.Parse(reader.GetString(2));
                            string title = reader.GetString(3);
                            string description = reader.GetString(4);
                            TaskDTO task = new TaskDTO(id, creationTime, dueDate, title, description);
                            result.Add(task);
                        }
                    }
                }
            }

            return result;
        }

        public List<string> SelectEmailsFromInProgress(int boardId)
        {
            List<string> result = new List<string>();

            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();

                string sqlQuery = $"SELECT {EmailAssignee} FROM {Tasks} WHERE Column = {1} AND BoardId = {boardId}";

                using (SQLiteCommand command = new SQLiteCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@boardId", boardId);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string email = reader.GetString(0);
                            result.Add(email);
                        }
                    }
                }
            }

            return result;
        }

        public BoardDTO ConvertReaderToObject(SQLiteDataReader reader)
        {
            DateTime creation = DateTime.Parse(reader.GetString(1));
            DateTime dueDate = DateTime.Parse(reader.GetString(1));
            List<TaskDTO> backlog = SelectAllTasksInColumn(0);
            List<TaskDTO> inProgress = SelectAllTasksInColumn(0);
            List<TaskDTO> done = SelectAllTasksInColumn(0);
            return new BoardDTO(reader.GetString(1), backlog, inProgress, done, taskId, reader.GetInt32(4), reader.GetInt32(5), reader.GetInt32(6), inprogressuser, boardID, owneremail);
        }

    }
}
