import { calculateCosts, pay, numberOfPaychecks, employeeBenefitCost, dependentBenefitCost, nameDiscount, letterToDiscount } from './employee/Calculator';
import bankersRounding from 'bankers-rounding';
import assert from 'assert';

it('calculates correct values', () => {
	const employeeTotalCostPerPayPeriod = bankersRounding(employeeBenefitCost / numberOfPaychecks, 2);

	const employeeTotalCostPerYear = bankersRounding(employeeBenefitCost, 2);

	const employeeAndDependentsTotalCostPerPayPeriod = bankersRounding(employeeBenefitCost / numberOfPaychecks, 2) + bankersRounding(dependentBenefitCost / numberOfPaychecks, 2);

	const employeeAndDependentsTotalCostPerYear = bankersRounding(employeeBenefitCost, 2) + bankersRounding(dependentBenefitCost, 2);

	const dependentTotalCostPerPayPeriod = bankersRounding(dependentBenefitCost / numberOfPaychecks, 2);

	const dependentTotalCostPerYear = bankersRounding(dependentBenefitCost, 2);

	const lessCostForLastPayPeriod = bankersRounding((employeeAndDependentsTotalCostPerPayPeriod * numberOfPaychecks) - employeeAndDependentsTotalCostPerYear, 2);

	const calculatedEmployee = {
		firstName: '',
		lastName: '',
		employeeTotalCostPerPayPeriod: 0,
		employeeTotalCostPerYear: 0,
		employeeAndDependentsTotalCostPerPayPeriod: 0,
		employeeAndDependentsTotalCostPerYear: 0,
		lessCostForLastPayPeriod: 0,
		dependents: [{
			firstName: '',
			lastName: '',
			dependentTotalCostPerPayPeriod: 0,
			dependentTotalCostPerYear: 0
		}]
	};

	calculateCosts(calculatedEmployee);

	assert.equal(calculatedEmployee.employeeAndDependentsTotalCostPerPayPeriod, employeeAndDependentsTotalCostPerPayPeriod);

	assert.equal(calculatedEmployee.employeeAndDependentsTotalCostPerYear, employeeAndDependentsTotalCostPerYear);

	assert.equal(calculatedEmployee.employeeTotalCostPerPayPeriod, employeeTotalCostPerPayPeriod);

	assert.equal(calculatedEmployee.employeeTotalCostPerYear, employeeTotalCostPerYear);

	assert.equal(calculatedEmployee.dependents[0].dependentTotalCostPerPayPeriod, dependentTotalCostPerPayPeriod);

	assert.equal(calculatedEmployee.dependents[0].dependentTotalCostPerYear, dependentTotalCostPerYear);

	assert.equal(calculatedEmployee.lessCostForLastPayPeriod, lessCostForLastPayPeriod);
});

it('calculates correct values with discount', () => {
	const employeeTotalCostPerPayPeriod = bankersRounding(bankersRounding(employeeBenefitCost * nameDiscount, 2) / numberOfPaychecks, 2);

	const employeeTotalCostPerYear = bankersRounding(employeeBenefitCost * nameDiscount, 2);

	const employeeAndDependentsTotalCostPerPayPeriod = bankersRounding(bankersRounding(employeeBenefitCost * nameDiscount, 2) / numberOfPaychecks, 2) + bankersRounding(bankersRounding(dependentBenefitCost * nameDiscount, 2) / numberOfPaychecks, 2);

	const employeeAndDependentsTotalCostPerYear = bankersRounding(employeeBenefitCost * nameDiscount, 2) + bankersRounding(dependentBenefitCost * nameDiscount, 2);

	const dependentTotalCostPerPayPeriod = bankersRounding(bankersRounding(dependentBenefitCost * nameDiscount, 2) / numberOfPaychecks, 2);

	const dependentTotalCostPerYear = bankersRounding(dependentBenefitCost * nameDiscount, 2);

	const lessCostForLastPayPeriod = bankersRounding((employeeAndDependentsTotalCostPerPayPeriod * numberOfPaychecks) - employeeAndDependentsTotalCostPerYear, 2);

	const calculatedEmployee = {
		firstName: 'a',
		lastName: 'a',
		employeeTotalCostPerPayPeriod: 0,
		employeeTotalCostPerYear: 0,
		employeeAndDependentsTotalCostPerPayPeriod: 0,
		employeeAndDependentsTotalCostPerYear: 0,
		dependents: [{
			firstName: 'a',
			lastName: 'a',
			dependentTotalCostPerPayPeriod: 0,
			dependentTotalCostPerYear: 0
		}]
	};

	calculateCosts(calculatedEmployee);

	assert.equal(calculatedEmployee.employeeAndDependentsTotalCostPerPayPeriod, employeeAndDependentsTotalCostPerPayPeriod);

	assert.equal(calculatedEmployee.employeeAndDependentsTotalCostPerYear, employeeAndDependentsTotalCostPerYear);

	assert.equal(calculatedEmployee.employeeTotalCostPerPayPeriod, employeeTotalCostPerPayPeriod);

	assert.equal(calculatedEmployee.employeeTotalCostPerYear, employeeTotalCostPerYear);

	assert.equal(calculatedEmployee.dependents[0].dependentTotalCostPerPayPeriod, dependentTotalCostPerPayPeriod);

	assert.equal(calculatedEmployee.dependents[0].dependentTotalCostPerYear, dependentTotalCostPerYear);

	assert.equal(calculatedEmployee.lessCostForLastPayPeriod, lessCostForLastPayPeriod);
});
