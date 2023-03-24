using TaskManagementSystem.Infrastructure.Models;
using TaskManagementSystem.Infrastructure.TMSData.Interfaces;

namespace TaskManagementSystem.Infrastructure.TMSData
{
    public class DBFactory : IDBFactory
    {
        TaskManagementDbContext dbContext;

        public TaskManagementDbContext Init()
        {
            dbContext = new TaskManagementDbContext();
            return dbContext;
        }
      
    }
}
