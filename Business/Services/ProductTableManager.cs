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
    public class ProductTableManager : ManagerBase<ProductTable, ProductTableDto>, IProductTableService
    {
        public ProductTableManager(IProductTableDal dal, IMapper mapper) : base(dal, mapper)
        {
        }

    }
    public interface IProductTableService : IServiceBase<ProductTable, ProductTableDto>
    {
    }
}
