using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodingChallenge.Repository.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace CodingChallenge.Repository.UnitTests
{
    public class CodingChallengeRepositoryTests
    {
        [Test]
        public async Task AddEmployeeAsyncShouldSucceed()
        {
            var options = GetContextOptions("test_database_add");

            using (var context = new CodingChallengeContext(options))
            {
                var id = 1;

                var repository = new CodingChallengeRepository(context);

                var employee = GetEmployee(id);

                var result = await repository.AddEmployeeAsync(employee);

                result.Should().NotBeNull();

                result.Id.Should().Be(id);

                result.Dependents.Count.Should().Be(1);

                result.Dependents.First().EmployeeId.Should().Be(id);
            }

            using (var context = new CodingChallengeContext(options))
            {
                context.Employees.Count().Should().Be(1);
            }
        }

        [Test]
        public async Task UpdateEmployeeAsyncShouldSucceed()
        {
            var options = GetContextOptions("test_database_update");

            var id = 1;

            var employee = GetEmployee(id);

            using (var context = new CodingChallengeContext(options))
            {
                var repository = new CodingChallengeRepository(context);

                var result = await repository.AddEmployeeAsync(employee);

                result.FirstName.Should().Be("Test");

                result.LastName.Should().Be("User");
            }

            using (var context = new CodingChallengeContext(options))
            {
                var repository = new CodingChallengeRepository(context);

                employee.FirstName = "Updated";

                employee.LastName = "Updated";

                await repository.UpdateEmployeeAsync(employee);
            }

            using (var context = new CodingChallengeContext(options))
            {
                context.Employees.First().FirstName.Should().Be("Updated");

                context.Employees.First().LastName.Should().Be("Updated");
            }
        }

        [Test]
        public async Task GetAllEmployeesAsync()
        {
            var options = GetContextOptions("test_database_get_all");

            var id = 1;

            var employee = GetEmployee(id);

            using (var context = new CodingChallengeContext(options))
            {
                var repository = new CodingChallengeRepository(context);

                var result = await repository.AddEmployeeAsync(employee);
            }

            using (var context = new CodingChallengeContext(options))
            {
                var repository = new CodingChallengeRepository(context);

                var results = await repository.GetAllEmployeesAsync();

                results.Should().NotBeNull();

                results.Count().Should().Be(1);

                results.First().Id.Should().Be(id);
            }
        }

        [Test]
        public async Task GetEmployeeAsyncShouldSucceed()
        {
            var options = GetContextOptions("test_database_get_one");

            var id = 1;

            var employee = GetEmployee(id);

            using (var context = new CodingChallengeContext(options))
            {
                var repository = new CodingChallengeRepository(context);

                var result = await repository.AddEmployeeAsync(employee);
            }

            using (var context = new CodingChallengeContext(options))
            {
                var repository = new CodingChallengeRepository(context);

                var result = await repository.GetEmployeeAsync(id);

                result.Should().NotBeNull();

                result.Id.Should().Be(id);
            }
        }

        [Test]
        public async Task DeleteEmployeeAsyncShouldSucceed()
        {
            var options = GetContextOptions("test_database_delete_one");

            var id = 1;

            var employee = GetEmployee(id);

            using (var context = new CodingChallengeContext(options))
            {
                var repository = new CodingChallengeRepository(context);

                var result = await repository.AddEmployeeAsync(employee);
            }

            using (var context = new CodingChallengeContext(options))
            {
                var repository = new CodingChallengeRepository(context);

                await repository.DeleteEmployee(id);
            }

            using (var context = new CodingChallengeContext(options))
            {
                context.Employees.Count().Should().Be(0);
            }
        }

        private DbContextOptions<CodingChallengeContext> GetContextOptions(string databaseName)
        {
            return new DbContextOptionsBuilder<CodingChallengeContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;
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
    }
}