using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class PropertyTable:Entity
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
    public class PropertyTableDto : Dto
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
