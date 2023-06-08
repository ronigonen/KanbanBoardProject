using IntroSE.Kanban.Backend.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BuisnessLayer
{
    public class TaskToSend
    {
        private int id;
        private DateTime creationTime;
        private DateTime dueDate;
        private string title;
        private string description;
        private string emailAssignee;

        public TaskToSend(Task t)
        {
            this.id = t.ID;
            this.creationTime = t.CreationTime;
            this.dueDate = t.DueDate;
            this.title = t.Title;
            this.description = t.Description;
            this.emailAssignee = t.EmailAssingnee;
        }
    }
}
