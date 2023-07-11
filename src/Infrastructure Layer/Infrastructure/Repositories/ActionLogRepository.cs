using Database;
using Domain.Models;
using Infrastructure.GenericRepositories;
using RepositoryInterfaces.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ActionLogRepository : GenericRepository<ActionLog>, IActionLogRepository
    {
        public ActionLogRepository(LoggingDbContext loggingDbContext) : base(loggingDbContext)
        {
        }
    }
}
