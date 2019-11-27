using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Extras.Moq;
using AutoMapper;
using CodingChallenge.Business.Bootstrap;
using CodingChallenge.Business.Components;
using CodingChallenge.Repository;
using CodingChallenge.Repository.Models;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace CodingChallenge.Business.UnitTests
{
    public class EmployeeComponentTests
    {
        [Test]
        public async Task GetAllEmployeesAsyncShouldSucceed()
        {
            using (var mock = GetMock())
            {
                mock.Mock<ICodingChallengeRepository>()
                    .Setup(x => x.GetAllEmployeesAsync())
                    .ReturnsAsync(GetEmployees());


                var _employeeComponent = mock.Create<EmployeeComponent>();

                var results = await _employeeComponent.GetAllEmployeesAsync();

                results.Should().NotBeNull();                

                results.Count().Should().Be(1);

                results.First().Dependents.Count.Should().Be(1);
            }
        }

        [Test]
        public async Task GetEmployeeByIdAsyncShouldSucceed()
        {
            using (var mock = GetMock())
            {
                var id = 1;

                mock.Mock<ICodingChallengeRepository>()
                    .Setup(x => x.GetEmployeeAsync(id))
                    .ReturnsAsync(GetEmployee(id));

                var _employeeComponent = mock.Create<EmployeeComponent>();

                var result = await _employeeComponent.GetEmployeeByIdAsync(id);

                result.Should().NotBeNull();

                result.Id.Should().Be(id);

                result.Dependents.Count.Should().Be(1);

                result.Dependents.First().EmployeeId.Should().Be(1);
            }
        }

        [Test]
        public async Task AddEmployeeAsyncShouldSucceed()
        {
            using (var mock = GetMock())
            {
                var id = 1;

                mock.Mock<ICodingChallengeRepository>()
                    .Setup(x => x.AddEmployeeAsync(It.IsAny<Employee>()))
                    .ReturnsAsync(GetEmployee(id));

                var _employeeComponent = mock.Create<EmployeeComponent>();

                var result = await _employeeComponent.AddEmployeeAsync(GetBusinessModel(id));

                result.Should().NotBeNull();

                result.Id.Should().Be(id);

                result.Dependents.Count.Should().Be(1);

                result.Dependents.First().EmployeeId.Should().Be(id);
            }
        }

        [Test]
        public async Task UpdateEmployeeAsyncShouldSucceed()
        {
            using (var mock = GetMock())
            {
                var id = 1;

                mock.Mock<ICodingChallengeRepository>()
                    .Setup(x => x.UpdateEmployeeAsync(It.IsAny<Employee>()))
                    .ReturnsAsync(id);

                var _employeeComponent = mock.Create<EmployeeComponent>();

                var results = await _employeeComponent.UpdateEmployeeAsync(GetBusinessModel(id));

                results.Should().Be(id);
            }
        }

        [Test]
        public void DeleteEmployeeAsyncShouldSucceed()
        {
            using (var mock = GetMock())
            {
                var id = 1;

                var _employeeComponent = mock.Create<EmployeeComponent>();

                _employeeComponent.Invoking(x => x.DeleteEmployeeAsync(id))
                    .Should()
                    .NotThrow();
            }
        }

        private AutoMock GetMock()
        {
            var myProfile = new AutoMapperProfile();

            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));

            IMapper mapper = new Mapper(configuration);

            var mock = AutoMock.GetLoose();

            mock.Provide(mapper);

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
                                        DependentType = 1,
                                        EmployeeId = id
                                    }
                                }
            };
        }

        private Models.Employee GetBusinessModel(int id)
        {
            return new Models.Employee
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
                Dependents = new List<Models.Dependent>
                                {
                                    new Models.Dependent
                                    {
                                        Id = id,
                                        FirstName = "Test",
                                        LastName = "User",
                                        DependentTotalCostPerPayPeriod = 500 / 26,
                                        DependentTotalCostPerYear = 500,
                                        DependentType = Enums.DependentType.Spouse,
                                        EmployeeId = id
                                    }
                                }
            };
        }
    }
}