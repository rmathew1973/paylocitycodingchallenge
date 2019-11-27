import React from 'react';
import ReactDOM from 'react-dom';
import InfoPane from './InfoPane';

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

	/>, div);
	ReactDOM.unmountComponentAtNode(div);
});