using Domain.Models;
using RepositoryInterfaces.GenericInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryInterfaces.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
    }
}
