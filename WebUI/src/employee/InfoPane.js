import React, { Component } from 'react';
import { Row, Col, FormLabel, Form } from 'react-bootstrap';

class InfoPane extends Component {
	constructor(props) {
		super(props);
		this.state = { payPerPeriod: props.payPerPeriod };
	}

	componentDidUpdate(prevProps) {
		if (prevProps.payPerPeriod !== this.props.payPerPeriod && parseFloat(this.state.payPerPeriod) !== this.props.payPerPeriod) {
			this.setState({ payPerPeriod: this.props.payPerPeriod });
		}
	}

	render() {
		return (
			<>
				<Row style={{ paddingLeft: '20px', paddingRight: '20px' }}>
					<Col size={3}>
						<FormLabel>First Name:</FormLabel>
					</Col>
					<Col size={3}>
						<Form.Control
							name='firstName'
							type='text'
							placeholder='First Name'
							value={this.props.firstName}
							onChange={this.props.onChange} />
					</Col>
					<Col size={3}>
						<FormLabel>Last Name:</FormLabel>
					</Col>
					<Col size={3}>
						<Form.Control
							name='lastName'
							type='text'
							placeholder='Last Name'
							value={this.props.lastName}
							onChange={this.props.onChange} />
					</Col>
				</Row>
				<br />
				<Row style={{ paddingLeft: '20px', paddingRight: '20px' }}>
					<Col size={3}>
						<FormLabel>Pay Per Period:</FormLabel>
					</Col>
					<Col size={3}>
						<Form.Control
							name='payPerPeriod'
							type='text'
							placeholder='Pay Per Period'
							value={this.state.payPerPeriod}
							onChange={(e) => {
								if (e.target.validity.valid) {
									this.setState({ payPerPeriod: e.target.value });
									this.props.onChange(e);
								}
								return;
							}}
							onBlur={this.props.handleChange}
							pattern='^((\d+(\.\d*)?)|(\.\d+))$' />
					</Col>
					<Col size={3}></Col>
					<Col size={3}></Col>
				</Row>
				<br />
				<hr />
				<Row style={{ paddingLeft: '20px', paddingRight: '20px' }}>
					<Col size={6}>
						<FormLabel>Employee Cost Per Year: ${this.props.employeeTotalCostPerYear.toFixed(2)}</FormLabel>
						<br />
						<FormLabel>Total Calculated Deduction Per Year: ${this.props.totalAmountDeductedPerYear.toFixed(2)}</FormLabel>
						<br />
						<FormLabel>Total Gross Pay Per Year: ${(this.props.payPerPeriod * this.props.numberOfPaychecks).toFixed(2)}</FormLabel>
						<br />
						<FormLabel>Total Net Pay Per Year: ${((this.props.payPerPeriod * this.props.numberOfPaychecks) - this.props.totalAmountDeductedPerYear).toFixed(2)}</FormLabel>
					</Col>
					<Col size={6}>
						<FormLabel>Employee Cost Per Period: ${this.props.employeeTotalCostPerPayPeriod.toFixed(2)}</FormLabel>
						<br />
						<FormLabel>Total Calculated Deduction Per Pay Period: ${this.props.totalAmountDeductedPerPeriod.toFixed(2)}</FormLabel>
						<br />
						<FormLabel>Total Gross Pay Per Pay Period: ${this.props.payPerPeriod.toFixed(2)}</FormLabel>
						<br />
						<FormLabel>Total Net Pay Per Pay Period: ${(this.props.payPerPeriod - this.props.totalAmountDeductedPerPeriod).toFixed(2)}</FormLabel>
					</Col>
				</Row>
			</>
		);
	}
}

export default InfoPane;