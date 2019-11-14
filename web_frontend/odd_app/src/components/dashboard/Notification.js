import React from 'react';
import { get_notifications, delete_notification } from './API'
import ReactTable from "react-table";
import "react-table/react-table.css";
import _ from "lodash";
import DismissModal from './DismissModal';
import dateformat from 'dateformat'
import { confirmAlert } from 'react-confirm-alert'; // Import
import 'react-confirm-alert/src/react-confirm-alert.css'; // Import css
import WebSocket from 'react-websocket'
import Sound from 'react-sound'
import alertsound from '../../assets/audio/alert.wav'

const requestData = async (pageSize, page, sorted, filtered) => {
    let filteredData = await get_notifications();
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

class Notification extends React.Component {
    constructor() {
        super();
        this.refReactTable = null
        this.is_mounted = false
        this.is_first = true
        this.state = {
            auto_update: true,
            data: [],
            page: 0,
            pages: null,
            show_modal: false,
            cur_note: {},
            accounts: [],
            play:Sound.status.STOPPED,
            position: 0
        };
    }

    static getDerivedStateFromProps(props) {
        return { accounts: props.accounts }
    }

    fetch_data = async (state, instance) => {
        if (!this.is_mounted && !this.is_first)
            return

        let res = await requestData(
            state.pageSize,
            state.page,
            state.sorted,
            state.filtered
        );

        if (this.is_mounted) {
            this.setState({
                data: res.rows,
                pages: res.pages,
                page: state.page
            });
            this.is_first = false
        }
    }

    update_data = () =>{
        if(!_.isNil(this.refReactTable))
            this.refReactTable.fireFetchData()
    }

    componentDidMount() {
        this.is_mounted = true
    }

    componentWillUnmount() {
        this.is_mounted = false
    }

    render_dismiss_btn = (cellInfo) => {
        return (
            <button type="button"
                className="btn btn-primary"
                style={{ width: '100%', height: '100%' }}
                onClick={e => {
                    this.setState({
                        cur_note: this.state.data[cellInfo.index],
                        show_modal: true
                    });
                }}
            >
                Dismiss
            </button>
        );
    }

    render_delete_btn = (cellInfo) => {
        return (
            <button type="button"
                className="btn btn-danger"
                style={{ width: '100%', height: '100%' }}
                onClick={e => {
                    confirmAlert({
                        title: 'Confirm',
                        message: 'Are you sure to remove this notification?',
                        buttons: [
                            {
                                label: 'Yes',
                                onClick: async () => {
                                    await delete_notification(this.state.data[cellInfo.index].id)
                                }
                            },
                            {
                                label: 'No'
                            }
                        ]
                    })
                }}
            >
                Delete
            </button>
        );
    }

    toggle_modal = () => {
        this.setState(prevState => ({
            show_modal: !prevState.show_modal
        }));
    }

    handleData = (raw_data) => {
        console.log(raw_data)
        let data = JSON.parse(raw_data)
        if(data.content === 'update_notification')
            this.update_data()
        if(data.alert === true)
            this.setState({
                play: Sound.status.PLAYING
            })
    }

    render() {
        return (
            <>
                <Sound url = {alertsound}
                    playStatus={this.state.play}
                    autoLoad={true}
                    volume = {100}
                    loop = {false}
                    onFinishedPlaying = {()=>{this.setState({
                        play: Sound.status.STOPPED
                    })}}
                    debug = {false}
                />
                <WebSocket 
                    url = {'ws://127.0.0.1:8000/ws/update/'} 
                    onMessage = {this.handleData}
                    debug = {true}
                />
                <DismissModal data={this.state.cur_note} show_modal={this.state.show_modal} toggle={this.toggle_modal} accounts={this.state.accounts} />
                <ReactTable
                    ref={(refReactTable) => { this.refReactTable = refReactTable; }}
                    manual
                    onFetchData={this.fetch_data}
                    data={this.state.data}
                    page={this.state.page}
                    pages={this.state.pages}
                    showPaginationTop={true}
                    showPaginationBottom={true}
                    showPagination={true}
                    pageSizeOptions ={[5, 10, 15, 20, 50, 100]}
                    defaultPageSize={15}
                    filterable
                    defaultFilterMethod={(filter, row, column) => {
                        const id = filter.id
                        return row[id] !== undefined ? String(row[id]).includes(filter.value) : true
                    }}
                    columns={[
                        {
                            Header: "Dismiss",
                            filterable: false,
                            Cell: this.render_dismiss_btn
                        },
                        {
                            Header: "Delete",
                            filterable: false,
                            Cell: this.render_delete_btn
                        },
                        {
                            Header: "No",
                            accessor: "id",
                            filterable: false,
                            minWidth: 50,
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
                            Header: "Degree",
                            accessor: "degree",
                            minWidth: 70,
                            style: { textAlign: 'center' }
                        },
                        {
                            Header: "State",
                            accessor: "state",
                            minWidth: 70,
                        },
                        {
                            Header: "Venue",
                            accessor: "venue",
                        },
                        {
                            Header: "Race",
                            accessor: "race_number",
                            minWidth: 70,
                            style: { textAlign: 'center' }
                        },
                        {
                            Header: "Horse No",
                            accessor: "horse_number",
                            minWidth: 80,
                            style: { textAlign: 'center' }
                        },
                        {
                            Header: "Horse Name",
                            accessor: "horse_name",
                            minWidth: 100,
                            style: { textAlign: 'left' }
                        },
                        {
                            Header: "Previous",
                            accessor: "previous_price",
                            minWidth: 70,
                            getProps: (state, row, column) => {
                                return {
                                    style: {
                                        background: (row ? '#C8F0D1':'#FFFFFF'),
                                        textAlign: 'center' 
                                    },
                                };
                            }
                        },
                        {
                            Header: "Current",
                            accessor: "current_price",
                            minWidth: 70,
                            getProps: (state, row, column) => {
                                return {
                                    style: {
                                        background: (row ? '#4FCF6C':'#FFFFFF'),
                                        textAlign: 'center' 
                                    },
                                };
                            }
                        },
                        {
                            Header: "Bookie",
                            accessor: "bookie",
                        },
                        {
                            Header: "Suggested Stake",
                            accessor: "suggested_stake",
                            Cell: row => _.floor(parseFloat(row.value), 2),
                            style: { textAlign: 'center' }
                        },
                        {
                            Header: "Max Profit",
                            accessor: "max_profit",
                            style: { textAlign: 'center' }
                        },
                        {
                            Header: "Top Price 1",
                            accessor: "top_price_1",
                        },
                        {
                            Header: "Top Price 2",
                            accessor: "top_price_2",
                        },
                        {
                            Header: "Top Price 3",
                            accessor: "top_price_3",
                        },
                        {
                            Header: "Top Price 4",
                            accessor: "top_price_4",
                        }
                    ]}
                    className="-striped -highlight"
                />
            </>
        );
    }
}

export default Notification;