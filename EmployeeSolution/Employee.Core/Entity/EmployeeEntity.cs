using Employee.Core.Interface;

using System;

namespace Employee.Core.Entity
{
   public class EmployeeEntity:BaseEntity
    {
        public string Name { get; set; }
        public DateTime Dob { get; set; }
        public string Designation { get; set; }
        public string Skills { get; set; }
    }
}
