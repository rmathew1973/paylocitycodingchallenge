using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodingChallenge.Repository.Models;

namespace CodingChallenge.Repository
{
    public interface ICodingChallengeRepository
    {
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        Task<Employee> GetEmployeeAsync(int id);
        Task<Employee> AddEmployeeAsync(Employee employee);
        Task<int> UpdateEmployeeAsync(Employee employee);
        Task DeleteEmployee(int id);
    }
}
