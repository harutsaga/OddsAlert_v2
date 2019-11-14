import React from 'react';
import { Container, Alert } from 'reactstrap';
import Header from 'components/layouts/Header'
import AuthService from 'components/AuthService'
const Auth = new AuthService();
function Home() {
    return(
        <>
            <Header auth={Auth}/>
            <Container className="mt-100 mb-100">
                <Alert color='success'>
                    Please login
                </Alert>
            </Container>
        </>
    );
}

export default Home;