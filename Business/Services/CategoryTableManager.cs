using AutoMapper;
using Core.DataAccess;
using DataAccess.Concrete.SQL_EntityFrameWork;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class CategoryTableManager : ManagerBase<CategoryTable, CategoryTableDto>, ICategoryTableService
    {
        public CategoryTableManager(ICategoryTableDal dal, IMapper mapper) : base(dal, mapper)
        {
        }

    }
    public interface ICategoryTableService : IServiceBase<CategoryTable, CategoryTableDto>
    {
    }
}
