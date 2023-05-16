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



        public void runTestSuccessfullAssignTask()
        {
            Response res = JsonSerializer.Deserialize<Response>(taskService.AssignTask("noga@gmail.com", "Assignment3", 0, 3, "roni@gmail.com"));
            if (res.ErrorOccured())
            {
                Console.WriteLine(res.ErrorMessage);
            }
            else
            {
                Console.WriteLine("runTestSuccessfullAssignTask- succeeded.");
            }
        }

        public void runTestFailedAssignTaskToNotMember()
        {
            Response res = JsonSerializer.Deserialize<Response>(taskService.AssignTask("noga@gmail.com", "Assignment3", 0, 3, "hadas@gmail.com"));
            if (res.ErrorOccured())
            {
                Console.WriteLine("runTestFailedAssignTaskToNotMember- succeeded.");
            }
            else
            {
                Console.WriteLine("runTestFailedAssignTaskToNotMember- failed.");
            }
        }

        /// <summary>
        /// This function tests Requirement 16
        /// </summary>
        public void runTestSuccessfullUpdateTaskDueDate()
        {
            Response resp5b = JsonSerializer.Deserialize<Response>(taskService.UpdateTaskDueDate("roni@gmail.com", "Assignment3", 0, 0, new DateTime(2023, 05, 30)));
            if (resp5b.ErrorOccured())
            {
                Console.WriteLine(resp5b.ErrorMessage);
            }
            else
            {
                Console.WriteLine("runTestSuccessfullUpdateTaskDueDate- succeeded.");
            }
        }

        public void runTestFailedUpdateTaskDueDateByNotAssignee()
        {
            Response resp5b = JsonSerializer.Deserialize<Response>(taskService.UpdateTaskDueDate("noga@gmail.com", "Assignment3", 0, 0, new DateTime(2023, 05, 30)));
            if (resp5b.ErrorOccured())
            {
                Console.WriteLine("runTestFailedUpdateTaskDueDateByNotAssignee- succeeded.");
            }
            else
            {
                Console.WriteLine("runTestFailedUpdateTaskDueDateByNotAssignee- failed.");
            }
        }

        /// <summary>
        /// This function tests Requirement 16
        /// </summary>
        public void runTestSuccessfullUpdateTaskTitle()
        {
            Response resp5b = JsonSerializer.Deserialize<Response>(taskService.UpdateTaskTitle("roni@gmail.com", "Assignment3", 0, 0, "changed"));
            if (resp5b.ErrorOccured())
            {
                Console.WriteLine(resp5b.ErrorMessage);
            }
            else
            {
                Console.WriteLine("runTestSuccessfullUpdateTaskTitle- succeeded.");
            }
        }

        public void runTestFailedUpdateTaskTitleByNotAssignee()
        {
            Response resp5b = JsonSerializer.Deserialize<Response>(taskService.UpdateTaskTitle("noga@gmail.com", "Assignment3", 0, 0, "changed"));
            if (resp5b.ErrorOccured())
            {
                Console.WriteLine("runTestFailedUpdateTaskTitleByNotAssignee- succeeded.");
            }
            else
            {
                Console.WriteLine("runTestFailedUpdateTaskTitleByNotAssignee- failed.");
            }
        }

        /// <summary>
        /// This function tests Requirement 16
        /// </summary>
        public void runTestSuccessfullUpdateTaskDescription()
        {
            Response resp5b = JsonSerializer.Deserialize<Response>(taskService.UpdateTaskDescription("roni@gmail.com", "Assignment3", 0, 0, "mission 4 description changed."));
            if (resp5b.ErrorOccured())
            {
                Console.WriteLine(resp5b.ErrorMessage);
            }
            else
            {
                Console.WriteLine("runTestSuccessfullUpdateTaskDescription- succeeded.");
            }
        }

        public void runTestFailedUpdateTaskDescriptionByNotAssignee()
        {
            Response resp5b = JsonSerializer.Deserialize<Response>(taskService.UpdateTaskDescription("noga@gmail.com", "Assignment3", 0, 0, "mission 4 description changed."));
            if (resp5b.ErrorOccured())
            {
                Console.WriteLine("runTestFailedUpdateTaskDescriptionByNotAssignee- succeeded.");
            }
            else
            {
                Console.WriteLine("runTestFailedUpdateTaskDescriptionByNotAssignee- failed.");
            }
        }

    }
}