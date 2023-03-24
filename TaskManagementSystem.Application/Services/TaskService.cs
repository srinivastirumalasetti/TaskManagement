using AutoMapper;
using TaskManagementSystem.Infrastructure.Models;
using TaskManagementSystem.Core.Interfaces;
using TaskManagementSystem.Core.Entities;
using TaskManagementSystem.Infrastructure.TMSData.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace TaskManagementSystem.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly IUnitOfWork _unitOfWork = null;
        //public readonly ITaskRepository _taskRepository;
        //public readonly IGenRepository<TaskModel> _genRepository;
        TaskManagementDbContext _dbcontext = new TaskManagementDbContext();
        private readonly IMapper _mapper;
        public TaskService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            //_genRepository = genRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Method to fetch all tasks
        /// </summary>
        /// <returns>tasks</returns>
        ///   
        public List<TaskViewModel> GetAssignedTasks()
        {
            var ljoinList = from t in _dbcontext.TblTasks
                            join u in _dbcontext.TblUsers on t.AssignedFrom equals u.UserId
                            join s in _dbcontext.TblStatuses on t.StatusId equals s.StatusId
                            join to in _dbcontext.TblUsers on t.AssignedTo equals to.UserId into users
                            from user in users.DefaultIfEmpty()
                            where t.StatusId == 1
                            select new TaskViewModel
                            {
                                TaskID = t.TaskId,
                                AssignedFrom = u.Firstname + ' ' + u.Lastname,
                                AssignedTo = user.Firstname + ' ' + user.Lastname,
                                AssignedOn = t.AssignedOn,
                                Description = t.Description,
                                Status = s.StatusName,
                                DueDateTime = t.DueDateTime,
                                EscalatedBy = t.EscalatedBy,
                                DateFlagValue = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")) > DateTime.Parse(t.DueDateTime.ToString("yyyy-MM-dd")) ? 1 : 0
                            };


            //Developer Comments: there is a issue while implementing right outer join to add escalated users details and approached below details           
            var userList = _dbcontext.TblUsers;
            var taskList = ljoinList.ToList();
            foreach (var task in taskList.Where(t => t.EscalatedBy != 0))
            {
                task.EscalatedUser = userList.Where(x => x.UserId == task.EscalatedBy).Select(s => s.Firstname + " " + s.Lastname).First().ToString();
            }
            return taskList.ToList();
        }


        /// <summary>
        /// Method to update task 
        /// </summary>
        /// <returns>val</returns>
        ///  
        public string UpdateTask(TaskViewModel taskViewModel)
        {
            _unitOfWork.BeginTransactionAsync();
            //_unitOfWork.BeginTransaction();

            try
            {
                string res = "Task Updated";

                TblTask taskModel = _unitOfWork.repository<TblTask>().Get(x => x.TaskId == taskViewModel.TaskID);

                _unitOfWork.repository<TblTask>().Detach(taskModel);

                taskModel.StatusId = taskViewModel.Status == "Close" ? 2 : 1;
                taskModel.LastModifiedBy = 1;
                taskModel.LastModifiedOn = DateTime.Now;

                _unitOfWork.repository<TblTask>().Update(taskModel);

                SaveContact();
                _unitOfWork.CommitTransactionAsync();
                //_unitOfWork.Commit();
                return res;
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }


        public void SaveContact()
        {
            _unitOfWork.CompleteAsync();
            //_unitOfWork.SaveChanges();

        }
    }
}
