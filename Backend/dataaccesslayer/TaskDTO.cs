using IntroSE.Kanban.Backend.BuisnessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    public class TaskDTO
    {
        private TaskController taskController;
        private Boolean isPersisted;
        private int id;
        private DateTime creationTime;
        private DateTime dueDate;
        private string title;
        private string description;
        private string emailAssignee;
        private int boardID;
        private int columnOrdinal;


        public TaskDTO(int id, DateTime creationTime, DateTime dueDate, string title, string description)
        {
            TaskController taskController = new TaskController();
            isPersisted = false;
            this.id = id;
            this.creationTime = creationTime;
            this.dueDate = dueDate;
            this.title = title;
            this.description = description;
            this.emailAssignee = null;
            this.boardID = null;
            this.columnOrdinal = 0;
        }

        internal int ColumnOrdinal
        {
            get => columnOrdinal;
            set
            {
                if (isPersisted)
                {
                    taskController.updateColumnOrdinal(value);
                }
                columnOrdinal = value;
            }
        }

        internal string Title
        {
            get => title;
            set
            {
                if (isPersisted)
                {
                    taskController.updateTitle(value);
                }
                title = value;
            }
        }

        internal DateTime DueDate
        {
            get => dueDate;
            set
            {
                if (isPersisted)
                {
                    taskController.updateDueDate(value);
                }
                dueDate = value;
            }
        }


        internal string Description
        {
            get => description;
            set
            {
                if (isPersisted)
                {
                    taskController.updateDescription(value);
                }
                description = value;
            }
        }

        internal DateTime CreationTime
        {
            get => creationTime;
        }

        internal int Id
        {
            get => id;
        }

        internal string EmailAssignee
        {
            get => emailAssignee;
            set
            {
                if (isPersisted)
                {
                    taskController.updateEmailAssignee(value);
                }
                description = value;
            }
        }


        internal int BoardID
        {
            get => boardID;
            set
            {
                boardID = value;
            }
        }

        public void persist()
        {
            try
            {
                taskController.insert(this);
                isPersisted = true;
            }
            catch (KanbanDataException e)
            {
                throw new KanbanDataException("didn't added to the data base");
            }
            catch (Exception ex)
            {
                throw new Exception(($"An unexpected error occured: \n {ex.Message} \nplease contact"));
            }
        }

        public void delete()
        {
            try
            {
                taskController.delete(this);
                isPersisted = false;
            }
            catch (KanbanDataException e)
            {
                throw new KanbanDataException("didn't added to the data base");
            }
            catch (Exception ex)
            {
                throw new Exception(($"An unexpected error occured: \n {ex.Message} \nplease contact"));
            }
        }
    }
}



