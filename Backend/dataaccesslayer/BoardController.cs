using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.ServiceLayer;
using IntroSE.Kanban.Backend.BuisnessLayer;



namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    internal class BoardController
    {
        private const string MessageTableName = "Boards";
        private readonly string _connectionString;
        private readonly string _tableName;
        internal BoardController()
        {
            string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "Kanban.db"));
            this._connectionString = $"Data Source={path}; Version=3;";
            this._tableName = MessageTableName;
        }
        public List<BoardDTO> Select()
        {
            List<BoardDTO> results = new List<BoardDTO>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                command.CommandText = $"SELECT * from {_tableName}";
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
        public List<BoardDTO> SelectAllBoards(int boardId)
        {
            List<BoardDTO> result = Select().Cast<BoardDTO>().ToList();
            return result;
        }
        public List<BoardDTO> SelectBoardsByEmail(string email)
        {
            List<BoardDTO> results = new List<BoardDTO>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                command.CommandText = $"SELECT * FROM {_tableName} WHERE OwnerEmail = @Email";
                command.Parameters.AddWithValue("@Email", email);

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



        public bool Insert(BoardDTO board)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                int res = -1;
                SQLiteCommand command = new SQLiteCommand(null, connection);
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO {_tableName} (BoardId, Name, TaskId, BackLogMax, InProgressMax, DoneMax, OwnerEmail) VALUES (@idVal, @nameVal, @taskIdVal, @backLogMaxVal, @inProgressMaxVal, @doneMaxVal, @ownerEmailVal)";
                    SQLiteParameter idParam = new SQLiteParameter("@idVal", board.BoardId);
                    SQLiteParameter nameParam = new SQLiteParameter("@nameVal", board.Name);
                    SQLiteParameter taskIdParam = new SQLiteParameter("@taskIdVal", board.TaskId);
                    SQLiteParameter backLogMaxParam = new SQLiteParameter("@backLogMaxVal", board.BackLogMax);
                    SQLiteParameter inProgressMaxParam = new SQLiteParameter("@inProgressMaxVal", board.InProgressMax);
                    SQLiteParameter doneMaxParam = new SQLiteParameter("@doneMaxVal", board.DoneMax);
                    SQLiteParameter ownerEmailParam = new SQLiteParameter("@ownerEmailVal", board.OwnerEmail);
                    command.Parameters.Add(idParam);
                    command.Parameters.Add(nameParam);
                    command.Parameters.Add(taskIdParam);
                    command.Parameters.Add(backLogMaxParam);
                    command.Parameters.Add(inProgressMaxParam);
                    command.Parameters.Add(doneMaxParam);
                    command.Parameters.Add(ownerEmailParam);
                    res = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new KanbanDataException(ex.Message);
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
                return res > 0;
            }
        }


        public bool Delete(TaskDTO task)
        {
            int res = -1;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                var command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"Delete from {_tableName} where [BoardId]={task.BoardID}"
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


        public List<TaskDTO> SelectAllTasksInColumn(int boardId, int col)
        {
            List<TaskDTO> result = new List<TaskDTO>();

            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();

                string sqlQuery = $"SELECT * FROM {_tableName} WHERE [BoardId] = @boardId and [ColumnName] = @col";

                using (SQLiteCommand command = new SQLiteCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@boardId", boardId);
                    command.Parameters.AddWithValue("@col", col);
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
        public List<TaskDTO> SelectAllTasksFromBoard(int boardId)
        {
            List<TaskDTO> result = new List<TaskDTO>();

            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();

                string sqlQuery = $"SELECT * FROM {_tableName} WHERE [BoardId] = @boardId";

                using (SQLiteCommand command = new SQLiteCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@boardId", boardId);
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
        public List<string> SelectEmailsFromInProgress()
        {
            List<string> result = new List<string>();

            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();

                string sqlQuery = $"SELECT {"EmailAssignee"} FROM {_tableName} WHERE [Column] = @column";

                using (SQLiteCommand command = new SQLiteCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@column", 1);
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

        public Dictionary<String, List<TaskDTO>> SelectInProgressUser()
        {
            List<String> emails = SelectEmailsFromInProgress();
            Dictionary<String, List<TaskDTO>> result = new Dictionary<String, List<TaskDTO>>();
            foreach (String email in emails)
            {
                List<TaskDTO> tasks = new List<TaskDTO>();

                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();

                    string sqlQuery = $"SELECT * FROM {_tableName} WHERE EmailAssignee = @email";

                    using (SQLiteCommand command = new SQLiteCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@email", email);
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
                                tasks.Add(task);
                            }
                            result.Add(email, tasks);

                        }
                    }
                }
            }

            return result;
        }


        public bool Update(int boardId, string attributeName, string attributeValue)
        {
            int res = -1;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand
                {
                    Connection = connection,

                    CommandText = $"update {_tableName} set [{attributeName}]=@attributeValue where id={boardId}"
                };
                try
                {

                    command.Parameters.Add(new SQLiteParameter(attributeName, attributeValue));
                    connection.Open();
                    res = command.ExecuteNonQuery();
                }
                catch
                {
                    throw new KanbanDataException("Data exception");
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }

            }
            return res > 0;
        }

       

        public string SelectEmailOwner(int boardId)
        {
            string result="";

            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();

                string sqlQuery = $"SELECT {"EmailOwner"} FROM {"Boards"} WHERE [BoardId] = @boardId";

                using (SQLiteCommand command = new SQLiteCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@boardId", boardId);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result= reader.GetString(0);
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
            int boardId = reader.GetInt32(0);
            return new BoardDTO(reader.GetString(1), SelectAllTasksFromBoard(boardId), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5), SelectInProgressUser(), boardId, reader.GetString(6));
        }
    }
}
