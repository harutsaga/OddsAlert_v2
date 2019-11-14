import React, { Component } from 'react';
import { BrowserRouter as Router, Route, Redirect } from 'react-router-dom';
import AuthService from './AuthService'
import Login from './auth/Login'
import Signup from './auth/Signup'
import Dashboard from './dashboard/Dashboard'

const Auth = new AuthService();

class App extends Component {
  constructor(props) {
    super(props)
    this.state = {
      user : null
    }
  }

  logged_in = (user) =>{
    this.setState({
      user:user
    })
  }

  render() {
    return (
      <div className="App">
        <Router>
          <div>
            <div className="min-height-600">
              <PrivateRoute exact path="/" component={() => <Dashboard user={this.state.user}/>}/>
              <Route path="/login" component={() => <Login logged_in={this.logged_in}/>}/>
              <Route path='/signup' component={Signup} />
              <PrivateRoute path='/dashboard' component={() => <Dashboard user={this.state.user}/>}/>
            </div>
          </div>
        </Router>
      </div>
    );
  }
}

const PrivateRoute = ({ component: Component, ...rest }) => (
  <Route
    {...rest}
    render={
      props => Auth.loggedIn() ?
        (<Component {...props} />) :
        (<Redirect to={{ pathname: "/login", state: { from: props.location } }} />)
    }
  />
);

export default App;
