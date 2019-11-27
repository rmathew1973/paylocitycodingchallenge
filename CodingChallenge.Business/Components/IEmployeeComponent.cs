using System.Collections.Generic;
using System.Threading.Tasks;
using CodingChallenge.Business.Models;

namespace CodingChallenge.Business.Components
{
    public interface IEmployeeComponent
    {
        Task<ICollection<Employee>> GetAllEmployeesAsync();

        Task<Employee> GetEmployeeByIdAsync(int id);

        Task<Employee> AddEmployeeAsync(Employee employee);

        Task<int> UpdateEmployeeAsync(Employee employee);

        Task DeleteEmployeeAsync(int id);
    }
}
