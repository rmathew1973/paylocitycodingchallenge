import React, { Component } from 'react';
import { Button, Navbar, Nav } from 'react-bootstrap';
import ReactTable from 'react-table';
import { FiDelete, FiEdit2 } from 'react-icons/fi';
import { Redirect } from 'react-router-dom';
import Errors from '../modals/Errors';
import DeleteConfirm from '../modals/DeleteConfirm';
import { EmployeeUrl } from '../Urls';
import 'react-table/react-table.css';
import 'bootstrap/dist/css/bootstrap.css';
import './Employees.css';

class Employees extends Component {

	constructor(props) {
		super(props);

		//Define columns for table
		const columns = [{
			Header: 'First Name',
			accessor: 'firstName',
			style: {
				textAlign: 'center'
			}
		}, {
			Header: 'Last Name',
			accessor: 'lastName',
			style: {
				textAlign: 'center'
			}
		}, {
			Header: 'Cost Per Period',
			accessor: 'employeeAndDependentsTotalCostPerPayPeriod',
			style: {
				textAlign: 'center'
			},
			Cell: props => `$${props.value.toFixed(2).toLocaleString('USD')}`
		}, {
			Header: 'Cost Per Year',
			accessor: 'employeeAndDependentsTotalCostPerYear',
			style: {
				textAlign: 'center'
			},
			Cell: props => `$${props.value.toFixed(2).toLocaleString('USD')}`
		},
		{
			Header: 'Delete',
			accessor: 'id',
			style: {
				textAlign: 'center'
			},
			Cell: props => <Button variant='link' onClick={() => {
				this.deleteEmployee(props.value)
			}} >
				<FiDelete style={{ color: 'Red' }} />
			</Button>
		}, {
			Header: 'Edit',
			accessor: 'id',
			style: {
				textAlign: 'center'
			},
			Cell: props => <Button variant='link' onClick={() => this.setState({ redirect: true, employeeId: props.value })} >
				<FiEdit2 />
			</Button>
		}];

		//Initialize state
		this.state = {
			employees: [],
			columns: columns,
			deleteId: 0,
			showDelete: false,
			showErrors: false,
			errorMessage: '',
			redirect: false,
			employeeId: 0
		};

		//Bind methods
		this.deleteEmployee = this.deleteEmployee.bind(this);
		this.showErrors = this.showErrors.bind(this);
		this.confirmDelete = this.confirmDelete.bind(this);
		this.loadEmployees = this.loadEmployees.bind(this);

		this.loadEmployees();
	}

	//Fetch employees
	loadEmployees() {
		fetch(EmployeeUrl)
			.then(res => res.json())
			.then(employees => {
				this.setState({ employees: employees })
			})
			.catch(e => {
				this.showErrors(JSON.stringify(e));
			});
	}

	//Show errors modal
	showErrors(errorMessage) {
		this.setState({ showErrors: true, errorMessage: errorMessage });
	}

	//Close errors modal
	closeErrors() {
		this.setState({ showErrors: false });
	}

	//Show delete Employee modal
	deleteEmployee(id) {
		this.setState({ showDelete: true, deleteId: id });
	}

	//Delete Employee if Yes is pressed in modal
	confirmDelete() {
		console.log(this.state.deleteId);
		fetch(`${EmployeeUrl}/${this.state.deleteId}`, { method: "DELETE" })
			.then(res => {
				this.loadEmployees();
			})
			.catch(e => {
				this.showErrors(JSON.stringify(e));
			});

		//Call close delete modal
		this.closeDelete();
	}

	//Close delete Employee/Spouse/Dependent modal
	closeDelete() {
		this.setState({ showDelete: false, deleteId: 0 });
	}

	render() {
		if (this.state.redirect) {
			return (<Redirect to={`/employee/${this.state.employeeId}`} />);
		}
		return (<>
			<Navbar bg='dark' variant='dark'>
				<Navbar.Brand>Payroll Deduction Calculator</Navbar.Brand>
				<Nav className='mr-auto'>
					<Button onClick={() => this.setState({ redirect: true, employeeId: 0 })} variant="link">Add Employee</Button>
				</Nav>
			</Navbar>
			<ReactTable
				ref='table'
				manual
				data={this.state.employees}
				columns={this.state.columns}
				className='-striped -highlight'
				showPagination={false}
			/>
			<Errors
				show={this.state.showErrors}
				onHide={this.closeValidationErrors}
				errors={this.state.errorMessage}
				heading="An error has occurred"
			/>
			<DeleteConfirm
				show={this.state.showDelete}
				onHide={this.closeDelete}
				onConfirm={this.confirmDelete}
			/>
		</>);
	}
}

export default Employees;