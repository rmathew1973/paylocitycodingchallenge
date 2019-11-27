import React, { Component } from 'react';
import { Modal, Button } from 'react-bootstrap';

class DeleteConfirm extends Component {
	render() {
		return (
			<Modal size='md' show={this.props.show} onHide={this.props.onHide}>
				<Modal.Header closeButton>
					<Modal.Title style={{ color: 'Red' }}>Confirm Delete</Modal.Title>
				</Modal.Header>
				<Modal.Body>
					<span style={{ color: 'Red' }}>Are you sure you want to delete this item?</span>
				</Modal.Body>
				<Modal.Footer>
					<Button onClick={this.props.hide} variant='success'>No</Button>
					<Button onClick={this.props.onConfirm} variant='danger'>Yes</Button>
				</Modal.Footer>
			</Modal>
		);
	}
}

export default DeleteConfirm;