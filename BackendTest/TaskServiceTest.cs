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

        public TaskServiceTest(TaskService taskService)
        {
            this.taskService = taskService;
        }

        public void runTests()
        {
            Response respo1a = JsonSerializer.Deserialize<Response>(taskService.UpdateTaskDueDate("hadas12@gmail.com", "Assignment", 1, 123, new DateTime(2023, 04, 30)));
            if (respo1a.ErrorOccured)
            {
                Console.WriteLine(respo1a.ErrorMessage);
            }
            else
            {
                Console.WriteLine("Due Date updated successfully.");
            }
            Response respo1b = JsonSerializer.Deserialize<Response>(taskService.UpdateTaskDueDate("hadas12@gmail.com", "Assignment", 1, 123, new DateTime(2013, 04, 30)));
            if (respo1b.ErrorOccured)
            {
                Console.WriteLine(respo1b.ErrorMessage);
            }
            else
            {
                Console.WriteLine("Update due date test failed- the date passed.");
            }




            Response respo2a = JsonSerializer.Deserialize<Response>(taskService.UpdateTaskTitle("hadas12@gmail.com", "Assignment", 1, 123, "Mission2"));
            if (respo2a.ErrorOccured)
            {
                Console.WriteLine(respo2a.ErrorMessage);
            }
            else
            {
                Console.WriteLine("Title updated successfully.");
            }
            Response respo2b = JsonSerializer.Deserialize<Response>(taskService.UpdateTaskTitle("hadas12@gmail.com", "Assignment", 1, 111, "Mission3"));
            if (respo2b.ErrorOccured)
            {
                Console.WriteLine(respo2b.ErrorMessage);
            }
            else
            {
                Console.WriteLine("Update title test failed- this id task does not exist.");

            }



            Response respo3a = JsonSerializer.Deserialize<Response>(taskService.UpdateTaskTitle("hadas12@gmail.com", "Assignment", 1, 123, "Mission2"));
            if (respo3a.ErrorOccured)
            {
                Console.WriteLine(respo3a.ErrorMessage);
            }
            else
            {
                Console.WriteLine("Title updated successfully.");
            }
            Response respo3b = JsonSerializer.Deserialize<Response>(taskService.UpdateTaskTitle("hadas12@gmail.com", "Assignment", 1, 111, "Mission3"));
            if (respo3b.ErrorOccured)
            {
                Console.WriteLine(respo3b.ErrorMessage);
            }
            else
            {
                Console.WriteLine("Update title test failed- this id task does not exist.");

            }



            Response respo4a = JsonSerializer.Deserialize<Response>(taskService.AdvanceTask("hadas12@gmail.com", "Assignment", 1, 123));
            if (respo4a.ErrorOccured)
            {
                Console.WriteLine(respo4a.ErrorMessage);
            }
            else
            {
                Console.WriteLine("Task advanced successfully.");
            }
            respo4a= JsonSerializer.Deserialize<Response>(taskService.AdvanceTask("hadas12@gmail.com", "Assignment", 2, 123));
            Response respo4b = JsonSerializer.Deserialize<Response>(taskService.AdvanceTask("hadas12@gmail.com", "Assignment", 3, 123));
            if (respo4b.ErrorOccured)
            {
                Console.WriteLine(respo4b.ErrorMessage);
            }
            else
            {
                Console.WriteLine("Advance task test failed- this task is in column 'Done'.");

            }
        }
