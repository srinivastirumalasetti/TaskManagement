using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Infrastructure.Models;

namespace TaskManagementSystem.Infrastructure.TMSData.Interfaces
{
    public interface IDBFactory
    {
        TaskManagementDbContext Init();
    }
}