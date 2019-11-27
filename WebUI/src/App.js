import React, { Component } from 'react';
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';
import 'react-table/react-table.css';
import 'bootstrap/dist/css/bootstrap.css';
import './App.css';
import Employees from './employees/Employees';
import Employee from './employee/Employee';


class App extends Component {
	render() {
		return (
			<Router>
				<Switch>
					<Route path="/" exact={true} component={Employees} />
					<Route path="/employee/:id" exact={true} component={Employee} />
				</Switch>
			</Router>
		);
	}
}

export default App;