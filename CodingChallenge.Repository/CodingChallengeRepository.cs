using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodingChallenge.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace CodingChallenge.Repository
{
    public class CodingChallengeRepository : ICodingChallengeRepository
    {
        private readonly CodingChallengeContext _context;

        public CodingChallengeRepository(CodingChallengeContext context)
        {
            _context = context;
        }

        public async Task<Employee> AddEmployeeAsync(Employee employee)
        {
            var result = await _context.AddAsync<Employee>(employee);

            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await _context.Employees.Include(x => x.Dependents).ToListAsync();
        }

        public async Task<Employee> GetEmployeeAsync(int id)
        {
            var employee = await _context.Employees.Include(i => i.Dependents).Where(x => x.Id == id).SingleOrDefaultAsync();

            return employee;
        }

        public async Task<int> UpdateEmployeeAsync(Employee employee)
        {
            var existingEmployee = _context.Employees
                .Where(p => p.Id == employee.Id)
                .Include(p => p.Dependents)
                .SingleOrDefault();

            if (existingEmployee != null)
            {
                _context.Entry(existingEmployee).CurrentValues.SetValues(employee);

                foreach (var existingDependent in existingEmployee.Dependents.ToList())
                {
                    if (!employee.Dependents.Any(c => c.Id == existingDependent.Id && c.EmployeeId == existingDependent.EmployeeId))                        
                    {
                        _context.Dependents.Remove(existingDependent);
                    }
                }

                foreach (var dependent in employee.Dependents)
                {
                    var existingDependent = existingEmployee.Dependents
                        .Where(c => c.Id == dependent.Id && dependent.Id != 0)
                        .SingleOrDefault();

                    if (existingDependent != null)
                    {
                        _context.Entry(existingDependent).CurrentValues.SetValues(dependent);
                    }
                    else
                    {                     
                        existingEmployee.Dependents.Add(dependent);
                    }
                }
                return await _context.SaveChangesAsync();
            }

            return 0;
        }

        public async Task DeleteEmployee(int id)
        {
            var result = await _context.Employees.Where(x => x.Id == id).Include(i => i.Dependents).FirstOrDefaultAsync();

            _context.Dependents.RemoveRange(result.Dependents);

            _context.Employees.Remove(result);

            await _context.SaveChangesAsync();
        }
    }
}
