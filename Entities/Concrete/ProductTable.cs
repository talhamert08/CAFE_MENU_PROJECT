using Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class ProductTable : Entity
    {
        public string ProductName { get; set; }
        
        public Guid CategoryId { get; set; }
        public decimal Price { get; set; }
        public string ImagePath { get; set; }
        [ForeignKey("CategoryId")]
        public CategoryTable Category { get; set; }

        [InverseProperty("Product")]
        public List<ProductPropertyTable> Properties { get; set; } 
    }
    public class ProductTableDto : Dto
    {
        public string ProductName { get; set; }
        public Guid CategoryId { get; set; }
        public decimal Price { get; set; }
        public string ImagePath { get; set; }
        public CategoryTable Category { get; set; }
        public List<ProductPropertyTable> Properties { get; set; }
    }
}
