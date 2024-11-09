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
    public class ProductPropertyTableManager : ManagerBase<ProductPropertyTable, ProductPropertyTableDto>, IProductPropertyTableService
    {
        public ProductPropertyTableManager(IProductPropertyTableDal dal, IMapper mapper) : base(dal, mapper)
        {
        }

    }
    public interface IProductPropertyTableService : IServiceBase<ProductPropertyTable, ProductPropertyTableDto>
    {
    }
}
