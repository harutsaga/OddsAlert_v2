import React from 'react';
import moment from 'moment';
import DateTimeRangeContainer from 'react-advanced-datetimerange-picker';
import dateFormat from 'dateformat'

class PeriodBoard extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            start_date: this.props.start,
            end_date: this.props.end,
        };
    }

    apply_daterange = (from, to) => {
        this.setState({
            start_date: from,
            end_date: to
        })
        this.props.range_changed(from, to);
    }

    render() {
        let now = new Date();
        let start = moment(new Date(now.getFullYear(), now.getMonth(), now.getDate(), 0, 0, 0, 0));
        let end = moment(start).add(1, "days").subtract(1, "seconds");
        let ranges = {
            "Today Only": [moment(start), moment(end)],
            "Yesterday Only": [moment(start).subtract(1, "days"), moment(end).subtract(1, "days")],
            "3 Days": [moment(start).subtract(3, "days"), moment(end)]
        }
        let local = {
            "format": "DD-MM-YYYY",
            "sundayFirst": false
        }
        let maxDate = moment(start).add(24, "hour")
        return (
                <DateTimeRangeContainer
                    style = {{display:'inline'}}
                    ranges={ranges}
                    start={this.state.start_date}
                    end={this.state.end_date}
                    local={local}
                    maxDate={maxDate}
                    applyCallback={this.apply_daterange}
                >
                    <button className="btn btn-secondary">
                        {dateFormat(this.state.start_date, "dddd, mmmm dS yyyy")} ~ {dateFormat(this.state.end_date, "dddd, mmmm dS yyyy")}
                    </button>
                </DateTimeRangeContainer>
        );
    }
}


export default PeriodBoard;