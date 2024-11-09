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
    public class CategoryTableDal : EfRepositoryBase<CategoryTable, EfContext>, ICategoryTableDal
    {
        public CategoryTableDal(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }
    }

    public interface ICategoryTableDal : IEntityRepository<CategoryTable> { }
}
