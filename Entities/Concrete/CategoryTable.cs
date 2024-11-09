using Core.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class CategoryTable : Entity
    {
        public string CategoryName { get; set; }
        public Guid? ParentCategoryId { get; set; }

    }

    public class CategoryTableDto : Dto
    {
        public string CategoryName { get; set; }
        public Guid? ParentCategoryId { get; set; }

    }
}
