import React from 'react';
import ReactDOM from 'react-dom';
import Employee from './employee/Employee';

it('renders without crashing', () => {
  const div = document.createElement('div');
  ReactDOM.render(<Employee match={{ params: { id: 0 } }} />, div);
  ReactDOM.unmountComponentAtNode(div);
});