using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CodingChallenge.Business.Helpers;
using CodingChallenge.Business.Models;
using CodingChallenge.Business.Validators;
using CodingChallenge.Repository;
using FluentValidation;

namespace CodingChallenge.Business.Components
{
    public class EmployeeComponent : IEmployeeComponent
    {
        private readonly ICodingChallengeRepository _repository;
        private readonly IMapper _mapper;
        private readonly EmployeeValidator _employeeValidator;

        public EmployeeComponent(
            ICodingChallengeRepository repository,
            IMapper mapper
        )
        {
            _repository = repository;
            _mapper = mapper;
            _employeeValidator = new EmployeeValidator();
        }

        public async Task<Employee> AddEmployeeAsync(Employee employee)
        {
            PayAndBenifitCalculator.PopulateBenifitCost(employee);

            _employeeValidator.ValidateAndThrow(employee);

            PayAndBenifitCalculator.PopulateEmployeePay(employee);

            var result = await _repository.AddEmployeeAsync(_mapper.Map<Repository.Models.Employee>(employee));

            return _mapper.Map<Employee>(result);
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            await _repository.DeleteEmployee(id);
        }

        public async Task<ICollection<Employee>> GetAllEmployeesAsync()
        {
            var results = await _repository.GetAllEmployeesAsync();

            return _mapper.Map<ICollection<Employee>>(results);
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            var result = await _repository.GetEmployeeAsync(id);

            return _mapper.Map<Employee>(result);
        }

        public async Task<int> UpdateEmployeeAsync(Employee employee)
        {
            PayAndBenifitCalculator.PopulateBenifitCost(employee);

            _employeeValidator.ValidateAndThrow(employee);

            PayAndBenifitCalculator.PopulateEmployeePay(employee);

            return await _repository.UpdateEmployeeAsync(_mapper.Map<Repository.Models.Employee>(employee));
        }
    }
}
