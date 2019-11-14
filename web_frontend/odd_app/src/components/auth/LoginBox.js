import React from 'react';
import { withRouter } from "react-router-dom";
import { Button, Form, FormGroup, Label, Input, Alert } from 'reactstrap';
import AuthService from '../AuthService';
import _ from 'lodash'

const Auth = new AuthService();

class LoginBox extends React.Component {
    constructor(props) {
        super(props)
        this.state = {
            username: '',
            password: '',
            error: '',
            error_msg: ''
        }
    }
    handleChange = (e) => {
        this.setState({
            [e.target.name]: e.target.value
        });
    }

    handleSubmit = async (e) => {
        e.preventDefault();
        if (this.validate()) {
            let res = await Auth.login(this.state.username, this.state.password)
            if (!_.isNil(res.token)) {
                this.props.history.replace('/dashboard')
                this.props.logged_in({
                    username: this.state.username,
                    id: res.id
                })
            }
            else
                this.setState({
                    error: _.first(_.keys(res)),
                    error_msg: _.first(_.values(res))
                });
        }
    }

    validate = () => {
        if (this.state.username === '') {
            this.setState({ error: 'username', error_msg: 'Please input your username.' });
            return false;
        }
        if (this.state.password === '') {
            this.setState({ error: 'password', error_msg: 'Please input your password.' });
            return false;
        }
        return true;
    }

    render() {
        return (
            <Form>
                <h1>Log in</h1>
                <FormGroup className={_.isEmpty(this.state.error) ? 'hidden' : ''}>
                    <Alert color="danger">{this.state.error_msg}</Alert>
                </FormGroup>
                <FormGroup>
                    <Label for="username">User Name</Label>
                    <Input type="username" name="username" id="username" onChange={this.handleChange} />
                </FormGroup>
                <FormGroup>
                    <Label for="password">Password</Label>
                    <Input type="password" name="password" id="password" onChange={this.handleChange} />
                </FormGroup>
                <Button onClick={this.handleSubmit}>Login</Button>
            </Form>
        );
    }
}

export default withRouter(LoginBox);