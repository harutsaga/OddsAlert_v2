import React from 'react';
import { Container, Row, Col } from 'reactstrap'
import Sidebar from 'components/layouts/Sidebar';
import Notification from 'components/dashboard/Notification'
import HistoricalBet from './HistoricalBet';
import Summary from './Summary';
import Account from './Account';
import Clock from 'react-live-clock';
import {get_accounts} from './API'

class Dashboard extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            user: null,
            tab: 'Notification',
            expanded: false,
            accounts: []
        }
    }

    componentDidMount() {
        this.refresh_accounts()
    }

    static getDerivedStateFromProps(props) {
        return { 
            user : props.user
        };
    }

    fetch_accounts = async () => {
        let resp = await get_accounts()
        this.setState({
            accounts: resp
        })
    }

    refresh_accounts = () => {
        this.fetch_accounts()
    }
    
    render() {
        return (
            <>
                <Container className = 'dashboard_main'>
                    <Sidebar tab={this.state.tab} className='col'
                        user = {this.state.user}
                        expanded={this.state.expanded}
                        tab_changed={this.tab_changed}
                        expanded_changed={this.expanded_changed}
                    />
                    <Container className='col dashboard_region' style={{ marginLeft: this.state.expanded ? 240 : 64, height: "100%", width: this.state.expanded ? 'calc(100% - 240px)' : 'calc(100% - 64px)' }}>
                        <Row className='dashboard_region_header jumbotron jumbotron-fluid bg-dark text-white'>
                            <Col>
                                <h2 className='col-md-6'>{this.state.tab}</h2>
                            </Col>
                            <Col style={{ textAlign: 'right' }} >
                                <Clock className='col-md-6' format={'MMMM D YYYY HH:mm:ss'} ticking={true} />
                            </Col>
                        </Row>
                        <Row>
                        {
                            (() => {
                                switch (this.state.tab) {
                                    case 'Notification':
                                        return (
                                            <Notification user = {this.state.user} accounts = {this.state.accounts}/>
                                        );
                                    case 'Historical Bets':
                                        return (
                                            <HistoricalBet user = {this.state.user} accounts = {this.state.accounts}/>
                                        );
                                    case 'Summary':
                                        return (
                                            <Summary user = {this.state.user} accounts = {this.state.accounts}/>
                                        );
                                    case 'Account':
                                        return (
                                            <Account user = {this.state.user} accounts = {this.state.accounts} refresh_accounts = {this.refresh_accounts}/>
                                        );
                                    default:
                                        return (<></>);
                                }
                            })()
                        }
                        </Row>
                    </Container>
                </Container>
            </>
        );
    }

    tab_changed = (new_tab) => {
        this.setState({
            tab: new_tab
        })
    }

    expanded_changed = (expanded) => {
        this.setState({
            expanded: expanded
        })
    }
}

export default Dashboard;