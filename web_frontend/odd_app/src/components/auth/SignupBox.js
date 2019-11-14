import React from 'react';
import { withRouter} from "react-router-dom";
import AuthService from '../AuthService';
import { Button, Form, FormGroup, Label, Input, Alert } from 'reactstrap';
import _ from 'lodash'

const Auth = new AuthService();

class SignupBox extends React.Component {
    constructor(){
        super();
        this.state = {
            username: '',
            password: '',
            confirm_password: '',
            error: '',
            error_msg: ''
        }
    }

    handleChange = (e) => {
        this.setState({
            [e.target.name]: e.target.value
        })
    }
    
    validate = () => {
        if(this.state.password !== this.state.confirm_password){
            this.setState({error: 'password', error_msg: 'The password does not match with confirm password.'});
            return false;
        }
        return true;
    }

    handleSubmit = async (e) => {
        e.preventDefault();
        this.setState({error:''});
        if(this.validate()){
            let data = JSON.stringify({
                username: this.state.username,
                password: this.state.password,
            })
            let res = await Auth.register(data)
                
            if(res.id === undefined){
                this.setState({
                        error: _.first(_.keys(res)), 
                        error_msg: _.first(_.values(res))
                });
            }
            else {
                this.props.history.push('/dashboard');
            }
        }
    }
    render() {
    return (
        <Form>
            <h1>Register</h1>
            <FormGroup className = {_.isEmpty(this.state.error)?'hidden':''}>
                <Alert color="danger">{this.state.error_msg}</Alert>
            </FormGroup>
            <FormGroup>
                <Label for="username">User Name</Label>
                <Input type="username" name="username" id="username" onChange={this.handleChange}/>
            </FormGroup>
            <FormGroup>
                <Label for="password">Password</Label>
                <Input type="password" name="password" id="password" onChange={this.handleChange}/>
            </FormGroup>
            <FormGroup>
                <Label for="confirm_password">Confirm Password</Label>
                <Input type="password" name="confirm_password" id="confirm_password" onChange={this.handleChange}/>
            </FormGroup>
            <Button onClick={this.handleSubmit}>Sign Up</Button>
        </Form>
    );
  }
}

export default withRouter(SignupBox);