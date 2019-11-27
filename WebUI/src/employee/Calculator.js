import bankersRounding from 'bankers-rounding';

const pay = 2000;
const numberOfPaychecks = 26;
const employeeBenefitCost = 1000;
const dependentBenefitCost = 500;
const nameDiscount = 0.9;
const letterToDiscount = 'A';

const calculateCosts = (employee) => {
	if (employee.firstName.toUpperCase().split('').shift() === letterToDiscount) {
		employee.employeeTotalCostPerYear = bankersRounding(employeeBenefitCost * nameDiscount, 2);
	} else {
		employee.employeeTotalCostPerYear = bankersRounding(employeeBenefitCost, 2);
	}

	employee.employeeTotalCostPerPayPeriod = bankersRounding(employee.employeeTotalCostPerYear / numberOfPaychecks, 2);

	employee.employeeAndDependentsTotalCostPerPayPeriod = employee.employeeTotalCostPerPayPeriod;

	employee.employeeAndDependentsTotalCostPerYear = employee.employeeTotalCostPerYear;

	employee.dependents.forEach(x => {
		if (x.firstName.toUpperCase().split('').shift() === letterToDiscount) {
			x.dependentTotalCostPerYear = bankersRounding(dependentBenefitCost * nameDiscount, 2);
		} else {
			x.dependentTotalCostPerYear = bankersRounding(dependentBenefitCost, 2);
		}

		x.dependentTotalCostPerPayPeriod = bankersRounding(x.dependentTotalCostPerYear / numberOfPaychecks, 2);

		employee.employeeAndDependentsTotalCostPerPayPeriod += x.dependentTotalCostPerPayPeriod;

		employee.employeeAndDependentsTotalCostPerYear += x.dependentTotalCostPerYear;
	});
}

export { calculateCosts, pay, numberOfPaychecks, employeeBenefitCost, dependentBenefitCost, nameDiscount, letterToDiscount };