import React, { Component } from 'react';
import { Modal, Button } from 'react-bootstrap';

class Errors extends Component {
	render() {
		return (
			<Modal size='md' show={this.props.show} onHide={this.props.onHide}>
				<Modal.Header closeButton>
					<Modal.Title style={{ color: 'Red' }}>{this.props.heading}</Modal.Title>
				</Modal.Header>
				<Modal.Body>
					<span style={{ color: 'Red' }}>{this.props.errors}</span>
				</Modal.Body>
				<Modal.Footer>
					<Button onClick={this.props.onHide} variant='success'>Ok</Button>
				</Modal.Footer>
			</Modal>
		);
	}
}

export default Errors;