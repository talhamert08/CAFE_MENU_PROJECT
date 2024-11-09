using AutoMapper;
using DataAccess.Concrete.SQL_EntityFrameWork;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class UserTableManager : ManagerBase<UserTable, UserTableDto>, IUserTableService
    {
        public UserTableManager(IUserTableDal dal, IMapper mapper) : base(dal, mapper)
        {
        }

    }
    public interface IUserTableService : IServiceBase<UserTable, UserTableDto>
    {
    }
}
