import React from 'react';
import { create_account } from './API'
import { Button, Form, FormGroup, Label, Input, Alert, Modal, ModalHeader, ModalBody, ModalFooter } from 'reactstrap'
import _ from 'lodash'

class AddAccountModal extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            show_modal: false,
            error: false,
            error_msg:'',
            account: '',
            user: null
        };
    }

    static getDerivedStateFromProps(props) {
        return { 
            user: props.user,
            show_modal: props.show_modal 
        }
    }

    validate = () => {
        if(this.state.account === "")
        {
            this.setState({
                error: true,
                error_msg: 'Account name can not be empty'
            })
            return false
        }
        return true
    }

    on_ok = async () =>{
        if(!this.validate())
            return
        
        let resp = await create_account({
            name: this.state.account
        })

        if(!_.isNil(resp.id))
        {
            this.props.refresh_accounts()
            this.props.refresh_accounts();
            this.props.toggle();    
            this.setState({
                error: false
            })
        }
        else
        {
            this.setState({
                error: true,
                error_msg: _.values(resp)[0]
            })
        }
    }

    on_cancel = () =>{
        this.props.toggle();
    }

    render() {
        return (
            <Modal isOpen={this.state.show_modal} 
                backdrop={true}>
                <ModalHeader toggle={this.toggle}>Add a new account to user {this.props.user?this.props.user.username:'null'}</ModalHeader>
                <ModalBody>
                    <Form>
                    <FormGroup className = {!this.state.error?'hidden':''}>
                        <Alert color="danger">{this.state.error_msg}</Alert>
                    </FormGroup>
                    <FormGroup>
                        <Label for="account_name">Account Name</Label>
                        <Input type="account_name" name="account_name" onChange={(e)=>{this.setState({account:e.target.value})}}/>
                    </FormGroup>
                    </Form>
                </ModalBody>
                <ModalFooter>
                    <Button color="primary" onClick={this.on_ok}>Add</Button>{' '}
                    <Button color="secondary" onClick={this.on_cancel}>Cancel</Button>
                </ModalFooter>
            </Modal>
        );
    }
}

export default AddAccountModal;