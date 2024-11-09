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
    public class PropertyTableManager : ManagerBase<PropertyTable, PropertyTableDto>, IPropertyTableService
    {
        public PropertyTableManager(IPropertyTableDal dal, IMapper mapper) : base(dal, mapper)
        {
        }

    }
    public interface IPropertyTableService : IServiceBase<PropertyTable, PropertyTableDto>
    {
    }
}
