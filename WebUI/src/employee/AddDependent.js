import React, { Component } from 'react';
import { Modal, Button, FormLabel, Row, Col, Form } from 'react-bootstrap';

class AddDependent extends Component {
	render() {
		return (
			<Modal size='large' show={this.props.show} onHide={this.props.onHide}>
				<Modal.Header closeButton>
					<Modal.Title>Add Employee, Spouse or Dependent</Modal.Title>
				</Modal.Header>
				<Modal.Body>
					<Row>
						<Col sm={6}>
							<FormLabel>First Name<span style={{ color: 'Red' }}>*</span></FormLabel>
						</Col>
						<Col sm={6}>
							<Form.Control name='firstName' type='text' placeholder='First Name' value={this.props.dependent.firstName} onChange={this.props.onChange} />
						</Col>
					</Row>
					<br />
					<Row>
						<Col sm={6}>
							<FormLabel>Last Name<span style={{ color: 'Red' }}>*</span></FormLabel>
						</Col>
						<Col sm={6}>
							<Form.Control name='lastName' type='text' placeholder='Last Name' value={this.props.dependent.lastName} onChange={this.props.onChange} />
						</Col>
					</Row>
					<br />
					<Row>
						<Col sm={6}>
							<FormLabel>Type<span style={{ color: 'Red' }}>*</span></FormLabel>
						</Col>
						<Col sm={6}>
							<Form.Control name='dependenttype' as='select' value={this.props.dependent.dependenttype} onChange={this.props.onChange}>
								<option value='0'>Select Type</option>
								<option value='1'>Spouse</option>
								<option value='2'>Dependent</option>
							</Form.Control>
						</Col>
					</Row>
				</Modal.Body>
				<Modal.Footer>
					<Button onClick={this.props.onHide} variant='primary' className='mr-auto'>Cancel</Button>
					<Button onClick={this.props.onSave} variant='success'>Add</Button>
				</Modal.Footer>
			</Modal>
		);
	}
}

export default AddDependent;