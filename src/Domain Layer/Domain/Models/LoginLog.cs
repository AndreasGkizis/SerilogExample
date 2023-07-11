using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class LoginTrafficLog
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateOnly LastLogin { get; set; }
        public DateOnly LastLogout { get; set; }
    }
}
