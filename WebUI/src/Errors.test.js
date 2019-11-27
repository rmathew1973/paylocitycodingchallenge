import React from 'react';
import ReactDOM from 'react-dom';
import Errors from './modals/Errors';

it('renders without crashing', () => {
	const div = document.createElement('div');
	ReactDOM.render(<Errors />, div);
	ReactDOM.unmountComponentAtNode(div);
});