using Microsoft.AspNetCore.Mvc;
using System.Net;
using TaskManagementSystem.Application.Services;
using TaskManagementSystem.Controllers;
using TaskManagementSystem.Core.Entities;
using TaskManagementSystem.Infrastructure.Repository;
using TaskManagementSystem.Infrastructure.TMSData;

namespace TaskManagementSystem.Test
{
    [TestClass]
    public class TaskControllerUnitTest
    {
        TaskController taskController = new TaskController(new TaskService(new TaskRepository(new Infrastructure.Models.TaskManagementDbContext()), new UnitOfWork(new Infrastructure.Models.TaskManagementDbContext())));
               
        [TestMethod]
        public void GetAssignedTasks()
        {
            var actionResult = taskController.Get() as ObjectResult;
                    
            //Assert
            Assert.AreEqual(HttpStatusCode.OK, (HttpStatusCode)actionResult.StatusCode);                                  
            Assert.IsNotNull(actionResult);           
        }

        [TestMethod]
        //update existing task
        public void UpdateExistingTask()
        {
            TaskViewModel taskModel = new TaskViewModel
            {
                TaskID = 74,
                Status = "Close"
            };

            var actionResult = taskController.Put(taskModel) as ObjectResult;
            
            //Assert
            Assert.AreEqual(HttpStatusCode.OK, (HttpStatusCode)actionResult.StatusCode);
            Assert.IsNotNull(actionResult);
            Assert.AreEqual("Task updated successfully", actionResult.Value);
        }

        [TestMethod]
        //update existing non correct record
        public void UpdateNonExistingTask()
        {
            TaskViewModel taskModel = new TaskViewModel
            {
                TaskID = 7411,
                Status = "Close"
            };

            var actionResult = taskController.Put(taskModel) as ObjectResult;
            
            //Assert
            Assert.IsNotNull(actionResult);
            Assert.AreEqual(HttpStatusCode.InternalServerError, (HttpStatusCode)actionResult.StatusCode);
            Assert.AreEqual("Error in Internal Server", actionResult.Value);
        }
    }
}