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
    public class UserRepository 
        : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ExampleDbContext exampleDbContext)
            : base(exampleDbContext)
        {
        }
    }
}
