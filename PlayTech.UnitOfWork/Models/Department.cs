using System;
using System.Collections.Generic;
using PlayTech.Shared.Database.Base;

namespace PlayTech.UnitOfWork.Models
{
    public class Department : BaseEntity
    {
        public string Name { get; set; }
    }
}
