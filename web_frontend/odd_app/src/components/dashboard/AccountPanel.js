import React from 'react';
import { Tab, Tabs, TabList, TabPanel } from 'react-tabs';
import 'react-tabs/style/react-tabs.css';
import { Container, Row, Col } from 'reactstrap'
import ReactTable from "react-table";
import "react-table/react-table.css";
import Select from 'react-dropdown-select'
import {  remove_all_accounts, remove_account, add_transaction } from './API'
import AddAccountModal from './AddAccountModal'
import SweetAlert from 'react-bootstrap-sweetalert'
import DatePicker from 'react-date-picker'
import {ToastsContainer, ToastsStore} from 'react-toasts'
import _ from 'lodash'

class AccountPanel extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            user: null,
            open_add_modal: false,
            accounts: [],
            cur_date: new Date(),
            alert_pop: null,
            cur_account : null,
        };

        this.ref_table = null;
        this.deposit_amount = 0;
        this.deposit_direction = 1;
        this.withdraw_amount = 0;
        this.withdraw_direction = 1;
        this.adjust_amount = 0;
        this.actual_closing = 0;
    }

    static getDerivedStateFromProps(props) {
        return {
            user: props.user,
            accounts: props.accounts
        }
    }

    refresh_accounts = () => {
        this.props.account_changed_handler()
    }

    add_new_account = () => {
        this.setState({
            open_add_modal: true
        })
    }

    remove_all_accounts = () => {
        this.show_alert_pop('Are you sure to remove all accounts?',
            async () => {
                await remove_all_accounts()
                this.setState({
                    alert_pop: null
                })
                this.refresh_accounts()
                ToastsStore.success('All accounts are removed.')
            },
            () => { this.setState({ alert: null }) }
        )
    }

    show_alert_pop = (title, on_confirm, on_cancel) => {
        this.setState({
            alert_pop: <SweetAlert title={title} onCancel={on_cancel} onConfirm={on_confirm} confirmBtnBsStyle='link' cancelBtnBsStyle='primary' showCancel={true} btnSize='sm' />
        })
    }

    render_edit_btn = (cellInfo) => {
        return (
            <button type="button"
                className="btn btn-info"
                style={{ width: '100%', height: '100%' }}
                onClick={e => {
                    alert('edit ' + cellInfo.index);
                }}
            >
                Edit
            </button>
        );
    }

    render_remove_btn = (cellInfo) => {
        return (
            <button type="button"
                className="btn btn-warning"
                style={{ width: '100%', height: '100%' }}
                onClick={(e) => {
                    this.show_alert_pop('Are you sure to remove the account?', async () => {
                        this.setState({
                            alert_pop: null
                        })
                        await remove_account(this.state.accounts[cellInfo.index].id)
                        ToastsStore.success(`The account ${this.state.accounts[cellInfo.index].name} are removed.`)
                        this.refresh_accounts()
                    },
                        () => {
                            this.setState({ alert_pop : null })
                        })
                }}
            >
                Remove
            </button>
        );
    }

    deposit = async () => {
        if(!this.state.cur_account)
        {
            ToastsStore.error("Account is not selected")
            return
        }
        let diff = this.deposit_amount * this.deposit_direction
        if (diff === 0)
            return
        let resp = await add_transaction(
            {
                account: this.state.cur_account.id,
                date: this.state.cur_date,
                amount: diff,
                action: 'Deposit'
            });
        if(!_.isNil(resp.action))
        {
            ToastsStore.success("Deposit success")
            this.refresh_accounts()
        }
        else
            ToastsStore.error("Deposit failed")
    }

    withdraw = async () => {
        if(!this.state.cur_account)
        {
            ToastsStore.error("Account is not selected")
            return
        }
        let diff = this.withdraw_amount * this.withdraw_direction
        if (diff === 0)
            return
        let resp = await add_transaction(
        {
                account: this.state.cur_account.id,
                date: this.state.cur_date,
                amount: diff,
                action: 'Withdraw'
        });
        if(!_.isNil(resp.action))
        {
            ToastsStore.success("Withdraw success")
            this.refresh_accounts()
        }
        else
            ToastsStore.error("Withdraw failed")
    }

    adjust = async () => {
        if(!this.state.cur_account)
        {
            ToastsStore.error("Account is not selected")
            return
        }
        let diff = this.adjust_amount
        if (diff === 0)
            return
        let resp = await add_transaction(
        {
                account: this.state.cur_account.id,
                date: this.state.cur_date,
                amount: diff,
                action: 'Adjust'
        });
        if(!_.isNil(resp.action))
        {
            ToastsStore.success("Adjust success")
            this.refresh_accounts()
        }
        else
            ToastsStore.error("Adjust failed")
    }

    set_actual_closing = async () => {          
        if(!this.state.cur_account)
        {
            ToastsStore.error("Account is not selected")
            return
        }
        let diff = this.actual_closing
        if (diff === 0)
            return
        let resp = await add_transaction(
        {
                account: this.state.cur_account.id,
                date: this.state.cur_date,
                amount: diff,
                action: 'Actual'
        });
        if (!_.isNil(resp.action)) {
            ToastsStore.success("Setting autual closing success")
            this.refresh_accounts()
        }
        else
            ToastsStore.error("Setting autual closing failed")
    }

    toggle_add_modal = () => {
        this.setState({
            open_add_modal: !this.state.open_add_modal
        })
    }

    render() {
        const accounts = this.state.accounts;
        return (
            <>

                {this.state.alert_pop}
                <ToastsContainer store = {ToastsStore}/>
                <AddAccountModal
                    show_modal={this.state.open_add_modal}
                    toggle={this.toggle_add_modal}
                    refresh_accounts={this.refresh_accounts}
                    user={this.state.user} />
                <Tabs>
                    <TabList>
                        <Tab>Accounts</Tab>
                        <Tab>Deposit</Tab>
                        <Tab>Withdraw</Tab>
                        <Tab>Account Adjustment</Tab>
                        <Tab>Actual Closing</Tab>
                    </TabList>
                    <TabPanel>
                        <Container>
                            <Row>
                                <Col className="col-md-2">
                                    <button className="btn btn-primary" onClick={this.add_new_account}>Add new account</button>
                                </Col>
                                <Col>
                                    <button className="btn btn-danger" onClick={this.remove_all_accounts}>Remove all</button>
                                </Col>
                            </Row>
                            <ReactTable
                                ref = {(ref_table)=>{this.ref_table = ref_table;}}
                                data={accounts}
                                showPagination={true}
                                onFetchData={this.props.fetch_accounts}
                                columns={[
                                    {
                                        Header: "No",
                                        id: "No",
                                        width: 100,
                                        Cell: (row) => {
                                            return <div>{row.index + 1}</div>;
                                        }
                                    },
                                    {
                                        Header: "Account",
                                        accessor: "name"
                                    },
                                    {
                                        Header: "Remove",
                                        Cell: this.render_remove_btn,
                                        width: 100,
                                    },
                                ]}
                                defaultPageSize={5}
                                className="-striped -highlight"
                            />
                        </Container>
                    </TabPanel>
                    <TabPanel>
                        <Row>
                            <Col className="col-md-1">
                                <div>Account</div>
                            </Col>
                            <Col className="col-md-3">
                                <Select
                                    options={this.state.accounts}
                                    values = {this.state.cur_account?[this.state.cur_account]:[]}
                                    onChange={(values) => { 
                                        if (values.length > 0) 
                                            this.setState({cur_account:values[0]})
                                        
                                    }}
                                    placeholder='Select an account'
                                    labelField='name' valueField='id' />
                            </Col>
                        </Row>
                        <br />
                        <Row>
                            <Col className="col-md-1">
                                <div>Date</div>
                            </Col>
                            <Col className="col-md-2">
                                <DatePicker clearIcon={null} value={this.state.cur_date} onChange={(date) => { this.setState({ cur_date: date }) }} />
                            </Col>
                        </Row>
                        <br />
                        <Row>
                            <Col className="col-md-1">
                                <div>Amount</div>
                            </Col>
                            <Col className="col-md-2">
                                <input type="text" onChange={(e) => { this.deposit_amount = e.target.value; }} />
                            </Col>
                        </Row>
                        <Row style={{ marginTop: '20px' }}>
                            <Col className="col-md-2">
                                <button className="btn btn-primary" onClick={(e) => { this.deposit_direction = 1; this.deposit(); }} style={{ width: '100%', height: '100%' }}>
                                    Deposit Into
                            </button>
                            </Col>
                            <Col className="col-md-2">
                                <button className="btn btn-danger" onClick={(e) => { this.deposit_direction = -1; this.deposit(); }} style={{ width: '100%', height: '100%' }}>
                                    Deposit From
                            </button>
                            </Col>
                        </Row>
                    </TabPanel>
                    <TabPanel>
                        <Row>
                            <Col className="col-md-1">
                                <div>Account</div>
                            </Col>
                            <Col className="col-md-3">
                                <Select
                                    options={this.state.accounts}
                                    values = {this.state.cur_account?[this.state.cur_account]:[]}
                                    onChange={(values) => { 
                                        if (values.length > 0) 
                                            this.setState({cur_account:values[0]})
                                        
                                    }}
                                    placeholder='Select an account'
                                    labelField='name' valueField='id' />
                            </Col>
                        </Row>
                        <br />
                        <Row>
                            <Col className="col-md-1">
                                <div>Date</div>
                            </Col>
                            <Col className="col-md-2">
                                <DatePicker clearIcon={null} value={this.state.cur_date} onChange={(date) => { this.setState({ cur_date: date }) }} />
                            </Col>
                        </Row>
                        <br />
                        <Row>
                            <Col className="col-md-1">
                                <div>Amount</div>
                            </Col>
                            <Col className="col-md-2">
                                <input type="text" onChange={(e) => { this.withdraw_amount = e.target.value; }} />
                            </Col>
                        </Row>
                        <Row style={{ marginTop: '20px' }}>
                            <Col className="col-md-2">
                                <button className="btn btn-primary" onClick={(e) => { this.withdraw_direction = 1; this.withdraw(); }} style={{ width: '100%', height: '100%' }}>
                                    Withdraw Into
                            </button>
                            </Col>
                            <Col className="col-md-2">
                                <button className="btn btn-danger" onClick={(e) => { this.withdraw_direction = -1; this.withdraw(); }} style={{ width: '100%', height: '100%' }}>
                                    Withdraw From
                            </button>
                            </Col>
                        </Row>
                    </TabPanel>
                    <TabPanel>
                        <Row>
                            <Col className="col-md-1">
                                <div>Account</div>
                            </Col>
                            <Col className="col-md-3">
                                <Select
                                    options={this.state.accounts}
                                    values = {this.state.cur_account?[this.state.cur_account]:[]}
                                    onChange={(values) => {
                                        if (values.length > 0) 
                                            this.setState({cur_account:values[0]})
                                        
                                    }}
                                    placeholder='Select an account'
                                    labelField='name' valueField='id' />
                            </Col>
                        </Row>

                        <br />
                        <Row>
                            <Col className="col-md-1">
                                <div>Date</div>
                            </Col>
                            <Col className="col-md-2">
                                <DatePicker clearIcon={null} value={this.state.cur_date} onChange={(date) => { this.setState({ cur_date: date }) }} />
                            </Col>
                        </Row>
                        <br />
                        <Row>
                            <Col className="col-md-1">
                                <div>Amount</div>
                            </Col>
                            <Col className="col-md-2">
                                <input type="text" onChange={(e) => { this.adjust_amount = e.target.value; }} />
                            </Col>
                        </Row>
                        <Row style={{ marginTop: '20px' }}>
                            <Col className="col-md-2">
                                <button className="btn btn-info" onClick={this.adjust} style={{ width: '100%', height: '100%' }}>
                                    Apply Adjustment
                            </button>
                            </Col>
                        </Row>
                    </TabPanel>
                    <TabPanel>
                        <Row>
                            <Col className="col-md-1">
                                <div>Account</div>
                            </Col>
                            <Col className="col-md-3">
                                <Select
                                    options={this.state.accounts}
                                    values = {this.state.cur_account?[this.state.cur_account]:[]}
                                    onChange={(values) => {
                                        if (values.length > 0) 
                                            this.setState({cur_account:values[0]})
                                        
                                    }}
                                    placeholder='Select an account'
                                    labelField='name' valueField='id' />
                            </Col>
                        </Row>
                        <br />
                        <Row>
                            <Col className="col-md-1">
                                <div>Date</div>
                            </Col>
                            <Col className="col-md-2">
                                <DatePicker clearIcon={null} value={this.state.cur_date} onChange={(date) => { this.setState({ cur_date: date }) }} />
                            </Col>
                        </Row>
                        <br />
                        <Row>
                            <Col className="col-md-1">
                                <div>Amount</div>
                            </Col>
                            <Col className="col-md-2">
                                <input type="text" onChange={(e) => { this.actual_closing = e.target.value; }} />
                            </Col>
                        </Row>
                        <Row style={{ marginTop: '20px' }}>
                            <Col className="col-md-3">
                                <button className="btn btn-info" onClick={this.set_actual_closing} style={{ width: '100%', height: '100%' }}>
                                    Set Actual Closing Amount
                            </button>
                            </Col>
                        </Row>
                    </TabPanel>
                </Tabs >
            </>
        );
    }
}

export default AccountPanel;