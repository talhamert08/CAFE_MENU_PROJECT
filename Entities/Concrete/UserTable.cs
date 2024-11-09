using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class UserTable : Entity
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string UserName { get; set; }
        public string HashPassword { get; set; }
        public string SaltPassword { get; set; }

    }

    public class UserTableDto : Dto
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string UserName { get; set; }
        public string HashPassword { get; set; }
        public string SaltPassword { get; set; }

    }

}
