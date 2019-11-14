import React from 'react';
import { dismiss_notification } from './API'
import { Button, Form, FormGroup, Label, Input, Alert, Modal, ModalHeader, ModalBody, ModalFooter } from 'reactstrap'
import _ from 'lodash'
import SimpleReactValidator from 'simple-react-validator'
import Select from 'react-dropdown-select'

class DismissModal extends React.Component {
    constructor() {
        super();
        this.state = {
            notification: {},
            show_modal: false,
            error:'',
            error_msg:'',
            account:'',
            price:null,
            amount:null,
            accounts:[]
        };

        this.validator = new SimpleReactValidator()
    }

    static getDerivedStateFromProps(props) {
        return { 
            notification: props.data, 
            show_modal: props.show_modal,
            accounts: props.accounts
        }
    }

    on_ok = () =>{
        if(!this.validator.allValid())
            this.validator.showMessages()
        else
        {
            let price = parseFloat(this.state.price)
            let stake = parseFloat(this.state.amount)
            dismiss_notification(this.state.notification.id, {
                account: this.state.account.id,
                price_taken: price,
                bet_amount: stake,
                result: "Pending",
                max_profit: (price - 1) * stake,
                status: 1
            })
            this.props.toggle();    
        }
    }

    on_cancel = () =>{
        this.props.toggle();
    }

    handleChange = (e) => {
        this.setState({
            [e.target.name]: e.target.value
        })
    }

    render() {
        return (
            <Modal isOpen={this.state.show_modal} 
                data={{ name: 'OK' }}
                backdrop={true}>
                <ModalHeader toggle={this.toggle}>Dismiss notification {this.state.notification.id}</ModalHeader>
                <ModalBody>
                    <Form>
                        <FormGroup className = {_.isEmpty(this.state.error)?'hidden':''}>
                            <Alert color="danger">{this.state.error_msg}</Alert>
                        </FormGroup>
                        <FormGroup>
                            <Label for="account">Account</Label>
                            <Select options = {this.state.accounts} onChange = {(values)=>{if(values.length>0)this.setState({account:values[0]})}} placeholder='Select an account' labelField='name' valueField='id'/>
                            {this.validator.message('account', this.state.account, 'required')}
                        </FormGroup>
                        <FormGroup>
                            <Label for="price">Price taken</Label>
                            <Input type="price" name="price" id="price" onChange={this.handleChange}/>
                            {this.validator.message('price', this.state.price, 'required|currency')}
                        </FormGroup>
                        <FormGroup>
                            <Label for="amount">Bet amount</Label>
                            <Input type="amount" name="amount" id="amount" onChange={this.handleChange}/>
                            {this.validator.message('amount', this.state.amount, 'required|currency')}
                        </FormGroup>
                    </Form>
                </ModalBody>
                <ModalFooter>
                    <Button color="primary" onClick={this.on_ok}>OK</Button>{' '}
                    <Button color="secondary" onClick={this.on_cancel}>Cancel</Button>
                </ModalFooter>
            </Modal>
        );
    }
}

export default DismissModal;