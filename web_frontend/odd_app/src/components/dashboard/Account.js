import React from 'react';
import { Container, Row } from 'reactstrap';
import PeriodBoard from 'components/dashboard/PeriodBoard'
import { get_account_summary } from './API'
import ReactTable from "react-table";
import "react-table/react-table.css";
import _ from "lodash";
import moment from 'moment'
import AccountPanel from './AccountPanel'


const requestData = async (start_date, end_date, pageSize, page, sorted, filtered) => {

    let filteredData = await get_account_summary(start_date, end_date);
    // You can use the filters in your request, but you are responsible for applying them.
    if (filtered.length) {
        filteredData = filtered.reduce((filteredSoFar, nextFilter) => {
            return filteredSoFar.filter(row => {
                return (row[nextFilter.id] + "").includes(nextFilter.value);
            });
        }, filteredData);
    }

    // You can also use the sorting in your request, but again, you are responsible for applying it.
    const sortedData = _.orderBy(
        filteredData,
        sorted.map(sort => {
            return row => {
                if (row[sort.id] === null || row[sort.id] === undefined) {
                    return -Infinity;
                }
                return typeof row[sort.id] === "string"
                    ? row[sort.id].toLowerCase()
                    : row[sort.id];
            };
        }),
        sorted.map(d => (d.desc ? "desc" : "asc"))
    );

    const res = {
        rows: sortedData.slice(pageSize * page, pageSize * page + pageSize),
        pages: Math.ceil(filteredData.length / pageSize),
        all_accounts: filteredData
    };

    return res;
};


class Account extends React.Component {
    constructor(props) {
        super(props);

        let now = new Date();
        let start = moment(new Date(now.getFullYear(), now.getMonth(), now.getDate(), 0, 0, 0, 1));
        let end = moment(start).add(1, "days").subtract(2, "seconds");
        this.start_date = start
        this.end_date = end

        this.is_mounted = false
        this.is_first = true
        this.ref_tbl = null
        
        this.state = {
            data: [],
            pages: null,
            accounts:[],
            user: null
        };
    }

    static getDerivedStateFromProps(props){
        return {
            user: props.user,
            accounts: props.accounts
        }
    }

    componentDidMount(){
        this.is_mounted = true
    }

    componentWillUnmount(){
        this.is_mounted = false
    }

    fetch_data = async (state, instance) => {
        if(!this.is_mounted && !this.is_first)
            return
        let res = await requestData(
            this.start_date,
            this.end_date,
            state.pageSize,
            state.page,
            state.sorted,
            state.filtered
        );

        if(!this.is_mounted)
            return
            
        this.setState({
            data: res.rows,
            pages: res.pages
        });

        this.is_first = false
        // if(!_.isNil(this.ref_tbl))
        //     _.delay(this.ref_tbl.fireFetchData, 500)
    }

    daterange_changed = (start, end) => {
        this.start_date = start
        this.end_date = end
        this.ref_tbl.fireFetchData()
    }

    account_changed = () => {
        this.ref_tbl.fireFetchData()
    }

    render() {

        return (
            <Container style={{ marginLeft: "0px" }}>
                <Row style={{ marginLeft: "10px" }}>
                    <PeriodBoard 
                        range_changed={this.daterange_changed}
                        start={this.start_date}
                        end={this.end_date} />
                </Row>
                <Row>
                    <div>
                        <ReactTable
                            ref={(ref_tbl) => {this.ref_tbl = ref_tbl;}}
                            onFetchData={this.fetch_data}
                            data={this.state.data}
                            pages={this.state.pages}
                            columns={[
                                {
                                    Header: "Account",
                                    accessor: "account",
                                },
                                {
                                    Header: "Opening Balance",
                                    accessor: "opening_balance",
                                },
                                {
                                    Header: "Deposit",
                                    accessor: "deposit",
                                },
                                {
                                    Header: "Withdrawl",
                                    accessor: "withdrawl",
                                },
                                {
                                    Header: "Account Adjustments",
                                    accessor: "adjustments",
                                },
                                {
                                    Header: "Closing Balance",
                                    accessor: "closing_balance",
                                },
                                {
                                    Header: "Actual Closing Balance",
                                    accessor: "actual_closing_balance",
                                },
                                {
                                    Header: "Movement",
                                    accessor: "movement",
                                },
                            ]}
                            defaultPageSize={10}
                            className="-striped -highlight"
                        />
                    </div>
                </Row>
                <AccountPanel 
                    user = {this.state.user}
                    fetch_accounts = {this.fetch_accounts}
                    accounts = {this.state.accounts.slice(0,10)}
                    account_changed_handler = {this.account_changed}
                />
            </Container>
        );
    }
}


export default Account;