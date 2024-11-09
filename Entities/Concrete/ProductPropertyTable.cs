using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class ProductPropertyTable:Entity
    {
        public Guid ProductId { get; set; }
        public Guid PropertyId { get; set; }

        [ForeignKey("PropertyId")]
        public PropertyTable Property { get; set; }

        [ForeignKey("ProductId")]
        public ProductTable Product { get; set; }
    }
    public class ProductPropertyTableDto : Dto
    {
        public Guid ProductId { get; set; }
        public Guid PropertyId { get; set; }
        public PropertyTable Property { get; set; }

    }
}
