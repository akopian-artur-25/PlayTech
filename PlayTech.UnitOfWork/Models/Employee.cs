﻿using System;
using System.Collections.Generic;
using PlayTech.Shared.Database.Base;

namespace PlayTech.UnitOfWork.Models
{
    public class Employee : BaseEntity
    {
        public int? DepartmentId { get; set; }
        public int? ManagerId { get; set; }
        public string Name { get; set; }
        public decimal? Salary { get; set; }
    }
}
