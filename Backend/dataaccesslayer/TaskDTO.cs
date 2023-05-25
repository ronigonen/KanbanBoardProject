using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    internal class TaskDTO
    {
        private TaskController taskController;
        private Boolean isPersisted;
        private int id;
        private DateTime creationTime;
        private DateTime dueDate;
        private string title;
        private string description;
        private string emailAssignee;

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
        }

        internal string Title
        {
            get => title;
            private set
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
            private set
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
            private set
            {
                if (isPersisted)
                {
                    taskController.updateDescription(value);
                }
                description = value;
            }
        }

        
        public void persist()
        {
            taskController.insert(this);
            isPersisted = true;
        }

        public void delete()
        {
            taskController.delete(this);
            isPersisted = false;
        }







    }


}
