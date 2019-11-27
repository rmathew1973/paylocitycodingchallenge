import React from 'react';
import ReactDOM from 'react-dom';
import DeleteConfirm from './DeleteConfirm';

it('renders without crashing', () => {
	const div = document.createElement('div');
	ReactDOM.render(<DeleteConfirm />, div);
	ReactDOM.unmountComponentAtNode(div);
});