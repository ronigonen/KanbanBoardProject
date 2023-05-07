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

        public void runTests()
        {
            runTestSuccessfullUpdateTaskDueDate();
            runTestSuccessfullUpdateTaskTitle();
            runTestSuccessfullUpdateTaskDescription();
        }

        /// <summary>
        /// This function tests Requirement 16
        /// </summary>
        public void runTestSuccessfullUpdateTaskDueDate()
        {
            Response resp5b = JsonSerializer.Deserialize<Response>(taskService.UpdateTaskDueDate("hadas@gmail.com", "AssignmentHadas", 1, 1, new DateTime(2023, 05, 30)));
            if (resp5b.ErrorOccured())
            {
                Console.WriteLine(resp5b.ErrorMessage);
            }
            else
            {
                Console.WriteLine("runTestSuccessfullUpdateTaskDueDate- succeeded.");
            }
        }

        /// <summary>
        /// This function tests Requirement 16
        /// </summary>
        public void runTestSuccessfullUpdateTaskTitle()
        {
            Response resp5b = JsonSerializer.Deserialize<Response>(taskService.UpdateTaskTitle("hadas@gmail.com", "AssignmentHadas", 1, 1, "changed"));
            if (resp5b.ErrorOccured())
            {
                Console.WriteLine(resp5b.ErrorMessage);
            }
            else
            {
                Console.WriteLine("runTestSuccessfullUpdateTaskTitle- succeeded.");
            }
        }

        /// <summary>
        /// This function tests Requirement 16
        /// </summary>
        public void runTestSuccessfullUpdateTaskDescription()
        {
            Response resp5b = JsonSerializer.Deserialize<Response>(taskService.UpdateTaskDescription("hadas@gmail.com", "AssignmentHadas", 1, 1, "mission 2 description changed."));
            if (resp5b.ErrorOccured())
            {
                Console.WriteLine(resp5b.ErrorMessage);
            }
            else
            {
                Console.WriteLine("runTestSuccessfullUpdateTaskDescription- succeeded.");
            }
        }
    }
}