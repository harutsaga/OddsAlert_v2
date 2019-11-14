import React from 'react';
import { Container, Row } from 'reactstrap';
import PeriodBoard from 'components/dashboard/PeriodBoard'
import { get_summary } from './API'
import ReactTable from "react-table";
import "react-table/react-table.css";
import _ from "lodash";
import moment from 'moment'


const requestData = async (start_date, end_date, pageSize, page, sorted, filtered) => {
    let filteredData = await get_summary(start_date, end_date);
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
        pages: Math.ceil(filteredData.length / pageSize)
    };

    return res;
};


class Summary extends React.Component {
    constructor() {
        super();

        let now = new Date();
        this.start_date = moment(new Date(now.getFullYear(), now.getMonth(), now.getDate(), 0, 0, 0, 0))
        this.end_date = moment(this.start_date).add(1, "days").subtract(1, "seconds")
        this.state = {
            data: [],
            pages: null
        };

        this.ref_table = null
    }

    fetch_data = async (state, instance) => {
        let res = await requestData(
            this.start_date,
            this.end_date,
            state.pageSize,
            state.page,
            state.sorted,
            state.filtered
        );
        this.setState({
            data: res.rows,
            pages: res.pages
        });
    }

    daterange_changed = (start, end) => {
        this.start_date = start
        this.end_date = end
        this.ref_table.fireFetchData()
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
                    <div className = "summary_table">
                        <ReactTable 
                            ref = {(ref_table) => {this.ref_table = ref_table}}
                            onFetchData={this.fetch_data}
                            data={this.state.data}
                            pages={this.state.pages}
                            loading={this.state.loading}

                            columns={[
                                {
                                    Header: "Account",
                                    accessor: "account",
                                },
                                {
                                    Header: "Total Bets",
                                    accessor: "total_bets",
                                },
                                {
                                    Header: "Total Wins",
                                    accessor: "total_wins",
                                },
                                {
                                    Header: "Turnover",
                                    accessor: "turnover",
                                },
                                {
                                    Header: "Returns",
                                    accessor: "returns",
                                },
                                {
                                    Header: "Profit",
                                    accessor: "profit",
                                },
                                {
                                    Header: "Strike Rate",
                                    accessor: "strike_rate",
                                },
                                {
                                    Header: "P.O.T",
                                    accessor: "pot",
                                },
                                {
                                    Header: "Pending Bets",
                                    accessor: "pending_bets",
                                },
                                {
                                    Header: "Pending Turnover",
                                    accessor: "pending_turnover",
                                },
                                {
                                    Header: "Current Position",
                                    accessor: "cur_position",
                                },
                            ]}
                            defaultPageSize={10}
                            className="-striped -highlight"
                        />
                    </div>
                </Row>
            </Container>
        );
    }
}


export default Summary;