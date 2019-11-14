import AuthService from "components/AuthService";

const auth = new AuthService()

export async function get_notifications() {
    let resp = await auth.fetch('/api/pendingnotifications',
        {
            method: 'GET'
        })
    return resp
}

export async function get_accounts() {
    let resp = await auth.fetch(`/api/accounts`,
        {
            method: 'GET'
        })
    return resp
}

export async function create_account(account) {
    let resp = await auth.fetch('/api/accounts',
    {
        method: 'POST',
        body: JSON.stringify(account)
    })
    return resp
}


export async function remove_all_accounts() {
    let resp = await auth.fetch(`/api/accounts`,
    {
        method: 'DELETE'
    })
    return resp
}

export async function remove_account(account) {
    let resp = await auth.fetch(`/api/accounts/${account}`,
    {
        method: 'DELETE'
    })
    return resp
}

export async function dismiss_notification(note_id, data)
{
    let resp = await auth.fetch(`/api/notifications/${note_id}`,
    {
        method: 'PUT',
        body: JSON.stringify(data)
    })
    return resp
}


export async function delete_notification(note_id)
{
    let resp = await auth.fetch(`/api/notifications/${note_id}`,
    {
        method: 'DELETE'
    })
    return resp
}

export async function get_historical_bets() {
    let resp = await auth.fetch('/api/history',
    {
        method: 'GET'
    })
    return resp
}

export async function get_summary(start_date, end_date, len = 5000) {
    let resp = await auth.fetch('/api/summary',
    {
        method: 'POST',
        body: JSON.stringify({
            'start_date': start_date,
            'end_date': end_date
        })
    })
    return resp
}

export async function add_transaction(data) {
    let resp = await auth.fetch('/api/transactions',
    {
        method: 'POST',
        body: JSON.stringify(data)
    })
    return resp
}

export async function get_account_summary(start_date, end_date, len = 5000) {
    let resp = await auth.fetch('/api/account_summary',
    {
        method: 'POST',
        body: JSON.stringify({
            'start_date': start_date,
            'end_date': end_date
        })
    })
    return resp
}
