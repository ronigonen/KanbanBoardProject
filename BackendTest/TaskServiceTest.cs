using IntroSE.ForumSystem.Backend.ServiceLayer;
using IntroSE.Kanban.Backend.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BackendTest
{
    class TaskServiceTest
    {
        private readonly TaskService taskService;

        public TaskServiceTest(WrapperClass w)
        {
            this.taskService = w.TaskService;
        }

        public void createUsersAndBoardsInTask()
        {
        }

        public void runTestSuccessfullAdvanceTask()
        {
        }

    }
}