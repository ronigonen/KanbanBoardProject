using IntroSE.Kanban.Backend.BuisnessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    public class UserInProgressTasksDTO
    { 
        private Dictionary<string, List<TaskDTO>> userTasks;
        public UserInProgressTasksDTO(Dictionary<string, List<TaskDTO>> tasks) 
        {
            userTasks = tasks;
        }

        public UserInProgressTasksDTO()
        {
            userTasks = new Dictionary<string, List<TaskDTO>>();
        }

        internal Dictionary<string, List<TaskDTO>> UserTasks
        { get => userTasks; }

    }
}
