using Employee.Core.Entity;
using Employee.Core.Interface;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Core.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<EmployeeEntity> _empRepository;
        public EmployeeService(IRepository<EmployeeEntity> empRepository)
        {
            _empRepository = empRepository;
        }
        public List<EmployeeEntity> GetAllEmployee()
        {
            var employees = _empRepository.ExecuteStoredProc("exec [sp_getEmployee]");

           
            return employees;
        }

      

        public async Task<EmployeeEntity> GetEmployeeById(int id)
        {
            return await _empRepository.GetById(id);
        }

        public async Task InsertAndUpdateEmployee(EmployeeEntity employee)
        {
            if (employee.Id != 0)
            {
                var emp = await _empRepository.GetById(employee.Id);
                emp.Name = employee.Name;
                emp.Skills = employee.Skills;
                emp.Designation = employee.Designation;
                emp.Dob = employee.Dob;
              await  _empRepository.Update(emp);
            }
            else
            {
                await _empRepository.Insert(employee);
            }
        }
        public async Task DeleteEmployee(int id)
        {
            if (id != 0)
            {
                var emp = await _empRepository.GetById(id);
                await _empRepository.Delete(emp);
            }
          
        }
    }
}
