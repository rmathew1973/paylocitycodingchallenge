import React from 'react';
import ReactDOM from 'react-dom';
import Employees from './employees/Employees';

it('renders without crashing', () => {
	const div = document.createElement('div');
	ReactDOM.render(<Employees />, div);
	ReactDOM.unmountComponentAtNode(div);
});