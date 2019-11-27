using NUnit.Framework;
using Moq;
using Autofac.Extras.Moq;
using CodingChallenge.Business.Models;
using AutoMapper;
using System.Collections.Generic;
using CodingChallenge.Api.Controllers;
using System.Threading.Tasks;
using FluentAssertions;
using System.Linq;
using CodingChallenge.Business.Components;
using CodingChallenge.Api.Bootstrap;
using CodingChallenge.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodingChallenge.Api.UnitTests
{
    public class EmployeeControllerTests
    {
        [Test]
        public async Task GetAllEmployeesShouldSucceed()
        {
            using (var mock = GetMock())
            {
                mock.Mock<IEmployeeComponent>()
                    .Setup(x => x.GetAllEmployeesAsync())
                    .ReturnsAsync(GetEmployees());


                var _employeeController = mock.Create<EmployeeController>();

                var results = await _employeeController.GetAllEmployees();

                results.Should().NotBeNull();

                var employees = (results.Result as OkObjectResult).Value as IEnumerable<EmployeeViewModel>;

                employees.Count().Should().Be(1);

                employees.First().Dependents.Count.Should().Be(1);              
            }
        }

        [Test]
        public async Task GetEmployeeShouldSucceed()
        {
            using (var mock = GetMock())
            {
                var id = 1;

                mock.Mock<IEmployeeComponent>()
                    .Setup(x => x.GetEmployeeByIdAsync(id))
                    .ReturnsAsync(GetEmployee(id));

                var _employeeController = mock.Create<EmployeeController>();

                var results = await _employeeController.GetEmployee(id);

                results.Should().NotBeNull();

                var employee = (results.Result as OkObjectResult).Value as EmployeeViewModel;

                employee.Should().NotBeNull();

                employee.Id.Should().Be(id);

                employee.Dependents.Count.Should().Be(1);

                employee.Dependents.First().EmployeeId.Should().Be(1);
            }
        }

        [Test]
        public async Task AddEmployeeShouldSucceed()
        {
            using (var mock = GetMock())
            {
                var id = 1;

                mock.Mock<IEmployeeComponent>()
                    .Setup(x => x.AddEmployeeAsync(It.IsAny<Employee>()))
                    .ReturnsAsync(GetEmployee(id));

                var _employeeController = mock.Create<EmployeeController>();

                var results = await _employeeController.AddEmployee(new EmployeeInputModel { Id = id });

                results.Should().NotBeNull();

                var employee = (results.Result as OkObjectResult).Value as EmployeeViewModel;

                employee.Should().NotBeNull();

                employee.Id.Should().Be(id);

                employee.Dependents.Count.Should().Be(1);

                employee.Dependents.First().EmployeeId.Should().Be(1);
            }
        }

        [Test]
        public async Task UpdateEmployeeShouldSucceed()
        {
            using (var mock = GetMock())
            {
                var id = 1;

                mock.Mock<IEmployeeComponent>()
                    .Setup(x => x.UpdateEmployeeAsync(It.IsAny<Employee>()))
                    .ReturnsAsync(id);

                var _employeeController = mock.Create<EmployeeController>();

                var results = await _employeeController.UpdateEmployee(new EmployeeInputModel { Id = id });

                results.Should().NotBeNull();

                (results.Result as OkObjectResult).Value.Should().Be(id);
            }
        }

        [Test]
        public async Task DeleteEmployeeShouldSucceed()
        {
            using (var mock = GetMock())
            {
                var id = 1;

                var _employeeController = mock.Create<EmployeeController>();

                var result = await _employeeController.DeleteEmployee(id);

                result.Should().NotBeNull();

                result.Should().BeOfType<OkResult>();
            }
        }

        private AutoMock GetMock()
        {
            var myProfile = new AutoMapperProfile();

            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));

            IMapper mapper = new Mapper(configuration);

            var mock = AutoMock.GetLoose();

            mock.Provide<IMapper>(mapper);

            return mock;
        }

        private ICollection<Employee> GetEmployees()
        {
            return new List<Employee> { GetEmployee(1) };
        }

        private Employee GetEmployee(int id)
        {
            return new Employee
            {
                Id = id,
                FirstName = "Test",
                LastName = "User",
                PayPerYear = 52000,
                PayPerPeriod = 2000,
                NetPayPerYear = 1000,
                NetPayPerPeriod = 51000,
                EmployeeAndDependentsTotalCostPerPayPeriod = 1000 / 26,
                EmployeeAndDependentsTotalCostPerYear = 1000,
                EmployeeTotalCostPerPayPeriod = 1000 / 26,
                EmployeeTotalCostPerYear = 1000,
                Dependents = new List<Dependent>
                                {
                                    new Dependent
                                    {
                                        Id = id,
                                        FirstName = "Test",
                                        LastName = "User",
                                        DependentTotalCostPerPayPeriod = 500 / 26,
                                        DependentTotalCostPerYear = 500,
                                        DependentType = Business.Enums.DependentType.Spouse,
                                        EmployeeId = id
                                    }
                                }
            };
        }
    }
}