

using Employee.Core.Entity;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee.Core.Interface
{
  public  interface IEmployeeService
    {
      
            Task<EmployeeEntity> GetEmployeeById(int id);
            Task InsertAndUpdateEmployee(EmployeeEntity employee);
            List<EmployeeEntity> GetAllEmployee();
        Task DeleteEmployee(int id);



    }
}
