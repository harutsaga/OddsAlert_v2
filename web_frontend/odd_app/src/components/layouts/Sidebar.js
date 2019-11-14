import React from 'react';
import SideNav, { NavItem, NavIcon, NavText } from '@trendmicro/react-sidenav';
import { withRouter } from "react-router-dom";
import '@trendmicro/react-sidenav/dist/react-sidenav.css';
import AuthService from '../AuthService';
import { faBell, faStickyNote, faHistory, faPiggyBank, faSignOutAlt } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

const Auth = new AuthService();

class Sidebar extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            selected: 'Historical Bets',
            expanded: false,
            user: null
        }
    }

    static getDerivedStateFromProps(props, state) {
        return {
            selected: props.tab,
            expanded: props.expanded,
            user: props.user
        };
    }

    on_select = (tab) => {
        this.setState({ selected: tab });
        if (tab) {
            this.props.tab_changed(tab);
        }
    }

    on_expand_change = (expanded) => {
        this.setState({expanded: expanded});
        this.props.expanded_changed(expanded);
    }

    render() {
        const { selected } = this.state;
        return (
            <>
                <SideNav onToggle = {this.on_expand_change} onSelect={this.on_select} className='sideNav'>
                    <SideNav.Toggle />
                    <SideNav.Nav className='sideNavPanel' selected={selected}>
                        <NavItem eventKey="Notification">
                            <NavIcon>
                                <FontAwesomeIcon icon={faBell} />
                            </NavIcon>
                            <NavText>
                                Notification
                            </NavText>
                        </NavItem>
                        <NavItem eventKey="Historical Bets">
                            <NavIcon>
                                <FontAwesomeIcon icon={faHistory} />
                            </NavIcon>
                            <NavText>
                                Historical Bets
                            </NavText>
                        </NavItem>
                        <NavItem eventKey="Summary">
                            <NavIcon>
                                <FontAwesomeIcon icon={faStickyNote} />
                            </NavIcon>
                            <NavText>
                                Summary
                            </NavText>
                        </NavItem>
                        <NavItem eventKey="Account" >
                            <NavIcon>
                                <FontAwesomeIcon icon={faPiggyBank} />
                            </NavIcon>
                            <NavText>
                                Account
                            </NavText>
                        </NavItem>
                        <NavItem onClick={this.onLogout} className='LogOutNavItem'>
                            <NavIcon>
                                <FontAwesomeIcon icon={faSignOutAlt} />
                            </NavIcon>
                            <NavText>
                                Log out ({this.state.user?this.state.user.username:''})
                            </NavText>
                        </NavItem>
                    </SideNav.Nav>
                </SideNav>
            </>
        );
    }

    onLogout = () => {
        Auth.logout();
        this.props.history.push('/');
    }
}

export default withRouter(Sidebar);