import React from 'react';
import { withRouter} from "react-router-dom";
import AuthService from '../AuthService';
import {
    Navbar,
    Nav,
    NavItem,
    NavLink,} from 'reactstrap';
import { NavIcon } from '@trendmicro/react-sidenav';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';

const Auth = new AuthService();

class BackendHeader extends React.Component {
    constructor(){
        super();
        this.state = {
            'user': []
        }
        this.handleLogout = this.handleLogout.bind(this);
    }
    componentWillMount(){
        if(Auth.loggedIn()){
            let user = Auth.getProfile()
            Auth.fetch("users/" + user.user_id)
                .then(res=>{
                    this.setState({'user': res})
                })
        }
    }

    handleLogout(){
        Auth.logout();
		this.props.history.push('/');
    }

    onclickProfile(){
        this.props.history.push('/summary');
    }

    render(){
        return(
            <div className="shadow-box">
                <Navbar color="dark" light navbar>
                    <Nav className="ml-auto" >
                        <NavItem>
                            <NavLink href="/" onClick={this.onclickProfile} >
                                {this.state.user.first_name}{'Wang Chao'}{this.state.user.last_name}
                            </NavLink>
                        </NavItem>
                        <div>
                            
                        </div>
                        <NavItem>
                            <NavLink href="/" onClick={this.handleLogout}>Logout</NavLink>
                        </NavItem>
                    </Nav>

                </Navbar>
            </div>
        );
    }
}

export default withRouter(BackendHeader);