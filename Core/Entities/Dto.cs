﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Dto : IDto
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatorUserId { get; set; }
        public bool IsDeleted { get; set; }
    }
    public interface IDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatorUserId { get; set; }
        public bool IsDeleted { get; set; }
    }
}