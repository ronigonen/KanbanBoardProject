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
    }
}
