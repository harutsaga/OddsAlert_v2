import React from 'react';
import ReactTable from "react-table";
import "react-table/react-table.css";
import { get_historical_bets } from './API'
import _ from "lodash";
import dateformat  from 'dateformat'
import {Badge} from 'reactstrap'
import WebSocket from 'react-websocket'

const requestData = async (pageSize, page, sorted, filtered) => {
    let filteredData = await get_historical_bets();
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

class HistoricalBet extends React.Component {
    constructor() {
        super();

        this.ref_tbl = null
        this.is_mounted = false
        this.is_first = true

        this.state = {
            data: [],
            pages: null
        };
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
            state.pageSize,
            state.page,
            state.sorted,
            state.filtered
        );

        if(this.is_mounted)
        {
            this.setState({
                data: res.rows,
                pages: res.pages
            });
            this.is_first = false
        }
    }

    render_result_cell = (cellInfo) => {
        let result = this.state.data[cellInfo.index].result
        let color = ""
        if (result === "Win")
            color = "success"
        else if (result === "Lose")
            color = "danger"
        else 
            color = "warning"

        return (
            <Badge pill color={color} style={{ width: '100%', height: '100%' }}>
                {result}
            </Badge>
        );
    }

    handleData = (raw_data) => {
        let data = JSON.parse(raw_data)
        if(data.content === 'update_notification')
            this.update_data()
    }

    update_data = () =>{
        if(!_.isNil(this.ref_tbl))
            this.ref_tbl.fireFetchData()
    }

    render() {
        return (
            <>
            <WebSocket 
                    url = {'ws://127.0.0.1:8000/ws/update/'} 
                    onMessage = {this.handleData}
                    debug = {true}
            />
            <ReactTable
                ref={(ref_tbl) => {this.ref_tbl = ref_tbl;}}
                manual
                onFetchData={this.fetch_data}
                data={this.state.data}
                pages={this.state.pages}
                filterable
                showPaginationTop={true}
                showPaginationBottom={true}
                showPagination={true}
                pageSizeOptions ={[5, 10, 15, 20, 50, 100]}
                defaultPageSize={15}
                defaultFilterMethod = {(filter, row, column) => {
                    const id = filter.id
                    return row[id] !== undefined ? String(row[id]).includes(filter.value) : true
                  }}
                columns={[
                    {
                        Header: "No",
                        accessor: "id",
                        filterable: false,
                        minWidth: 50,
                        style: { textAlign: 'center' }
                    },
                    {
                        Header: "Date",
                        accessor: "datetime",
                        Cell: row => dateformat(row.value, 'yyyy-mm-dd'),
                        style: { textAlign: 'center' }
                    },
                    {
                        Header: "Time",
                        accessor: "datetime",
                        Cell: row => dateformat(row.value, 'HH:MM:ss'),
                        style: { textAlign: 'center' }
                    },
                    {
                        Header: "Time to Jump",
                        accessor: "time_to_jump",
                    },
                    {
                        Header: "State",
                        accessor: "state",
                        minWidth: 80,
                        style: { textAlign: 'center' }
                    },
                    {
                        Header: "Venue",
                        accessor: "venue",
                        minWidth: 80,
                        style: { textAlign: 'center' }
                    },
                    {
                        Header: "Race No",
                        accessor: "race_number",
                        minWidth: 80,
                        style: { textAlign: 'center' }
                    },
                    {
                        Header: "Horse No",
                        accessor: "horse_number",
                        minWidth: 80,
                        style: { textAlign: 'center' }
                    },
                    {
                        Header: "Price Taken",
                        accessor: "price_taken",
                        minWidth: 100,
                        style: { textAlign: 'center' }
                    },
                    {
                        Header: "Account",
                        accessor: "account_name",
                    },
                    {
                        Header: "Bet Amount",
                        accessor: "bet_amount",
                        minWidth: 100,
                        style: { textAlign: 'center' }
                    },
                    {
                        Header: "Max Profit",
                        accessor: "max_profit",
                    },
                    {
                        Header: "Result",
                        accessor: "result",
                        minWidth: 80,
                        Cell: this.render_result_cell
                    }
                ]}
                className="-striped -highlight"
            />
            </>
        );
    }
}

export default HistoricalBet;