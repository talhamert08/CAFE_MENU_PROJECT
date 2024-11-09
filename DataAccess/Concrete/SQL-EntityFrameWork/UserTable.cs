using Core.DataAccess;
using DataAccess.Contexts;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.SQL_EntityFrameWork
{
    public class UserTableDal : EfRepositoryBase<UserTable, EfContext>, IUserTableDal
    {
        public UserTableDal(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }
    }

    public interface IUserTableDal : IEntityRepository<UserTable> { }
}
