import React, { Component } from 'react';
import { Button, Navbar, Nav } from 'react-bootstrap';
import ReactTable from 'react-table';
import { Redirect } from 'react-router-dom';
import { FiDelete } from 'react-icons/fi';
import bankersRounding from 'bankers-rounding';
import Errors from '../modals/Errors';
import DeleteConfirm from '../modals/DeleteConfirm';
import AddDependent from './AddDependent';
import InfoPane from './InfoPane';
import { EmployeeUrl } from '../Urls';
import 'react-table/react-table.css';
import 'bootstrap/dist/css/bootstrap.css';
import './Employee.css';

const pay = 2000;
const numberOfPaychecks = 26;
const employeeBenefitCost = 1000;
const dependentBenefitCost = 500;
const nameDiscount = 0.9;
const letterToDiscount = 'A';

class Employee extends Component {

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
      Header: 'Type',
      accessor: 'dependenttype',
      style: {
        textAlign: 'center'
      },
      Cell: props => props.value === 1 ? "Spouse" : "Dependent"
    }, {
      Header: 'Cost Per Period',
      accessor: 'dependentTotalCostPerPayPeriod',
      style: {
        textAlign: 'center'
      },
      Cell: props => `$${props.value.toFixed(2).toLocaleString('USD')}`
    }, {
      Header: 'Cost Per Year',
      accessor: 'dependentTotalCostPerYear',
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
      Cell: props => <Button variant='link'
        onClick={() => {
          this.deleteDependent(props.index);
        }} >
        <FiDelete style={{ color: 'Red' }} />
      </Button>
    }];

    const initialEmployee = {
      id: 0,
      firstName: '',
      lastName: '',
      employeeTotalCostPerPayPeriod: employeeBenefitCost / numberOfPaychecks,
      employeeTotalCostPerYear: employeeBenefitCost,
      employeeAndDependentsTotalCostPerPayPeriod: employeeBenefitCost / numberOfPaychecks,
      employeeAndDependentsTotalCostPerYear: employeeBenefitCost,
      payPerPeriod: pay,
      payPerYear: pay * numberOfPaychecks,
      netPayPerPeriod: pay - (employeeBenefitCost / numberOfPaychecks),
      netPayPerYear: (pay * numberOfPaychecks) - employeeBenefitCost,
      dependents: []
    };

    const { id } = this.props.match.params;

    //Initialize state
    this.state = {
      employee: initialEmployee,
      enteringDependent: false,
      showAddDependent: false,
      columns: columns,
      showValidationErrors: false,
      deleteId: 0,
      showDelete: false,
      errorMessage: '',
      dependent: {
        id: 0,
        firstName: '',
        lastName: '',
        employeeId: 0,
        dependentTotalCostPerPayPeriod: 0,
        dependentTotalCostPerYear: 0,
        dependenttype: 0
      },
      closeEmployee: false,
      onCloseErrors: () => {
        this.setState({ showValidationErrors: false });
      },
      errorHeader: "",
      title: parseInt(id) === 0 ? "Add Employee" : "Update Employee"
    };

    //Bind methods
    this.addDependent = this.addDependent.bind(this);
    this.saveDependent = this.saveDependent.bind(this);
    this.closeDependent = this.closeDependent.bind(this);
    this.deleteDependent = this.deleteDependent.bind(this);
    this.closeDelete = this.closeDelete.bind(this);
    this.confirmDelete = this.confirmDelete.bind(this);
    this.calculateCosts = this.calculateCosts.bind(this);
    this.validateDependent = this.validateDependent.bind(this);
    this.employeeChangeHandler = this.employeeChangeHandler.bind(this);
    this.dependentChangeHandler = this.dependentChangeHandler.bind(this);
    this.saveEmployee = this.saveEmployee.bind(this);
    this.validateEmployee = this.validateEmployee.bind(this);
    this.closeEmployee = this.closeEmployee.bind(this);

    if (parseInt(id) !== 0) {
      fetch(`${EmployeeUrl}/${id}`)
        .then(res => res.json())
        .then(employee => this.setState({ employee: employee }))
        .catch(e => {
          this.setState({
            showValidationErrors: true,
            errorHeader: 'An Exception Has Occurred',
            errorMessage: JSON.stringify(e),
            onCloseErrors: () => {
              this.setState({ showValidationErrors: false });
            }
          });
        });
    }
  }

  //Show add Dependent modal
  addDependent() {
    this.setState({
      showAddDependent: true,
      dependent: {
        id: 0,
        firstName: '',
        lastName: '',
        employeeId: 0,
        dependentTotalCostPerPayPeriod: 0,
        dependentTotalCostPerYear: 0,
        dependenttype: 0
      }
    });
  }

  //Validate Dependent fields
  validateDependent() {
    var invalidFields = [];

    if (this.state.dependent.firstName === '') {
      invalidFields.push('First Name');
    }

    if (this.state.dependent.lastName === '') {
      invalidFields.push('Last Name');
    }

    if (this.state.dependent.dependenttype === 0) {
      invalidFields.push('Type');
    }
    if (invalidFields.length > 0) {
      this.setState({
        showValidationErrors: true,
        showAddDependent: false,
        errorHeader: 'Validation Errors',
        errorMessage: `The following fields are required: ${invalidFields.join(', ')}`,
        onCloseErrors: () => {
          this.setState({ showValidationErrors: false, showAddDependent: true });
        }
      });
      return false;
    }

    if (this.state.dependent.dependenttype === 1 && this.state.employee.dependents.some(x => x.dependenttype === 1)) {
      this.setState({
        showValidationErrors: true,
        showAddDependent: false,
        errorHeader: 'Validation Errors',
        errorMessage: "You are only allowed one spouse.",
        onCloseErrors: () => {
          this.setState({ showValidationErrors: false, showAddDependent: true });
        }
      });
      return false;
    }

    return true;
  }

  //Helper to calculate costs and populate properties on employee record
  calculateCosts(employee) {
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

  //Add dependent to collection after validation.
  saveDependent() {
    //Validate all required fields.
    if (this.validateDependent()) {
      const employee = this.state.employee;

      //Get current collection of dependents
      let dependents = [...employee.dependents];

      //Add/Update dependent
      const dependent = this.state.dependent;

      if (dependent.id === 0) {
        dependents.push(this.state.dependent);
      } else {
        dependents = dependents.filter(x => x === dependent.id);
        dependents.push(dependent);
      }

      employee.dependents = dependents;

      //Calculate cost
      this.calculateCosts(employee);

      this.setState({ employee: employee });

      //Call close modal
      this.closeDependent();
    }
  }

  //Close Employee/Spouse/Dependent modal
  closeDependent() {
    this.setState({ showAddDependent: false });
  }

  //Show delete Employee/Spouse/Dependent modal
  deleteDependent(id) {
    this.setState({ showDelete: true, deleteId: id });
  }

  //Delete Employee/Spouse/Dependent if Yes is pressed in modal
  confirmDelete() {
    //Filter out deleted item from dependents
    const employee = this.state.employee;

    employee.dependents = employee.dependents.filter((x, i) => i !== this.state.deleteId);

    this.setState({ employee: employee });

    this.calculateCosts(employee);

    //Call close delete modal
    this.closeDelete();
  }

  //Close delete Employee/Spouse/Dependent modal
  closeDelete() {
    this.setState({ showDelete: false, deleteId: 0 });
  }

  //Validate Employee fields
  validateEmployee() {
    var invalidFields = [];

    if (this.state.employee.firstName === '') {
      invalidFields.push('First Name');
    }

    if (this.state.employee.lastName === '') {
      invalidFields.push('Last Name');
    }

    if (invalidFields.length > 0) {
      this.setState({
        showValidationErrors: true,
        errorHeader: 'Validation Errors',
        errorMessage: `The following fields are required: ${invalidFields.join(', ')}`,
        onCloseErrors: () => {
          this.setState({ showValidationErrors: false });
        }
      });
      return false;
    }

    return true;
  }

  //Save employee
  saveEmployee() {
    if (this.validateEmployee()) {
      const method = this.state.employee.id === 0 ? 'POST' : 'PUT';
      fetch(EmployeeUrl, {
        method: method,
        headers: {
          'Accept': 'application/json',
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(this.state.employee)
      })
        .then(res => this.setState({ closeEmployee: true }))
        .catch(e => {
          this.setState({
            showValidationErrors: true,
            errorHeader: 'An Exception Has Occurred',
            errorMessage: JSON.stringify(e),
            onCloseErrors: () => {
              this.setState({ showValidationErrors: false });
            }
          });
        });
    }
  }

  closeEmployee() {
    this.setState({ closeEmployee: true });
  }

  employeeChangeHandler(evt) {
    const employee = this.state.employee;
    if (evt.target.name === "payPerPeriod") {
      employee.payPerPeriod = (evt.target.validity.valid) ? parseFloat(evt.target.value) : this.state.employee.payPerPeriod;
    } else {
      employee[evt.target.name] = evt.target.value;
    }
    this.calculateCosts(employee);
    this.setState({ employee: employee });
  }

  dependentChangeHandler(evt) {
    const dependent = this.state.dependent;
    if (evt.target.name === "dependenttype") {
      dependent[evt.target.name] = parseInt(evt.target.value);
    } else {
      dependent[evt.target.name] = evt.target.value;
    }
    this.setState({ dependent: dependent });
  }

  render() {
    if (this.state.closeEmployee) {
      return (<Redirect to={'/'} />);
    }

    return (
      <>
        <Navbar bg='dark' variant='dark'>
          <Navbar.Brand>{this.state.title}</Navbar.Brand>
          <Nav className='mr-auto'>
            <Button variant='link' onClick={this.saveEmployee}>Save Employee</Button>
          </Nav>
          <Nav className='mr-auto'>
            <Button variant='link' onClick={this.addDependent}>Add Dependent</Button>
          </Nav>
          <Nav className='mr-auto'>
            <Button variant='link' onClick={this.closeEmployee}>Cancel</Button>
          </Nav>
        </Navbar>
        <br />
        <InfoPane
          onChange={this.employeeChangeHandler}
          firstName={this.state.employee.firstName}
          lastName={this.state.employee.lastName}
          employeeTotalCostPerPayPeriod={this.state.employee.employeeTotalCostPerPayPeriod}
          employeeTotalCostPerYear={this.state.employee.employeeTotalCostPerYear}
          totalAmountDeductedPerYear={this.state.employee.employeeAndDependentsTotalCostPerYear}
          payPerPeriod={this.state.employee.payPerPeriod}
          numberOfPaychecks={numberOfPaychecks}
          totalAmountDeductedPerPeriod={this.state.employee.employeeAndDependentsTotalCostPerPayPeriod}
        />
        <ReactTable
          ref='table'
          manual
          data={this.state.employee.dependents}
          columns={this.state.columns}
          className='-striped -highlight'
          showPagination={false}
        />
        <AddDependent
          show={this.state.showAddDependent}
          onHide={this.closeDependent}
          onChange={this.dependentChangeHandler}
          onSave={this.saveDependent}
          dependent={this.state.dependent}
        />
        <Errors
          heading={this.state.errorHeader}
          show={this.state.showValidationErrors}
          onHide={this.state.onCloseErrors}
          errors={this.state.errorMessage}
        />
        <DeleteConfirm
          show={this.state.showDelete}
          onHide={this.closeDelete}
          onConfirm={this.confirmDelete}
        />
      </>
    );
  }
}

export default Employee;