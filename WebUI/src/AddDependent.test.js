import React from 'react';
import ReactDOM from 'react-dom';
import AddDependent from './employee/AddDependent';

it('renders without crashing', () => {
	const div = document.createElement('div');
	ReactDOM.render(<AddDependent dependent={{ firstName: '', lastName: '', dependenttype: 0 }} />, div);
	ReactDOM.unmountComponentAtNode(div);
});