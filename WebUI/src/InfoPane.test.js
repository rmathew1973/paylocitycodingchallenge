import React from 'react';
import ReactDOM from 'react-dom';
import InfoPane from './employee/InfoPane';
import bankersRounding from 'bankers-rounding';

it('renders without crashing', () => {
	const div = document.createElement('div');
	ReactDOM.render(<InfoPane
		firstName=''
		lastName=''
		payPerPeriod={2000}
		employeeTotalCostPerYear={1000}
		totalAmountDeductedPerYear={1000}
		numberOfPaychecks={26}
		employeeTotalCostPerPayPeriod={0}
		totalAmountDeductedPerPeriod={1000 / 26}
		lessCostForLastPayPeriod={(bankersRounding(1000 / 26, 2) * 26) - 1000}
		onChange={(e) => { console.log(2); }}

	/>, div);
	ReactDOM.unmountComponentAtNode(div);
});