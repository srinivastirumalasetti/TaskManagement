using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Core.Interfaces;
using TaskManagementSystem.Core.Entities;

namespace TaskManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        //// Declared Readonly variable - one time initialization at runtime
        public readonly ITaskService _taskService;
        
        /////// <summary>
        ///// Contructor injection to initialize required services
        ///// </summary>
        public TaskController(ITaskService taskService)
        {
            this._taskService = taskService;
        }


        ///// <summary>
        ///// Method to fetch all records
        ///// </summary>
        ///// <returns>tasks</returns>     
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<TaskViewModel> taskData = _taskService.GetAssignedTasks();

                if (taskData == null)
                {
                    return NotFound();
                }
                return Ok(taskData);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Internal Server");
            }
        }

        [HttpPut]
        ///// <summary>
        ///// Method to update task
        ///// </summary>
        ///// <param name="taskModel"></param>
        ///// <returns>result</returns>
        public IActionResult Put(TaskViewModel taskModel)
        {
            try
            {
                _taskService.UpdateTask(taskModel);
                return Ok("Task updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Internal Server");
            }
        }
    }
}
