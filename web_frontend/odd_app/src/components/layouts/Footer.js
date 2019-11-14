import React from 'react';
import { Container, Row, Col } from 'reactstrap';

class Footer extends React.Component {
    render(){
        if(!this.props.auth.loggedIn()){
            return(
                <footer>
                    <Container className="pt-20">
                        <Row>
                            <Col md="6">
                                <h5 className="title">Crypto Trading</h5>
                                <p>
                                Here you can create a portfolio and balance the own portfolio.
                                </p>
                            </Col>
                            <Col md="3">
                                <h5 className="title">Features</h5>
                                <ul>
                                <li className="list-unstyled">
                                    <a href="#!">Security</a>
                                </li>
                                <li className="list-unstyled">
                                    <a href="#!">Exchanges</a>
                                </li>
                                <li className="list-unstyled">
                                    <a href="#!">Community</a>
                                </li>
                                <li className="list-unstyled">
                                    <a href="#!">Team</a>
                                </li>
                                </ul>
                            </Col>
                            <Col md="3">
                                <h5 className="title">About Us</h5>
                                <ul>
                                <li className="list-unstyled">
                                    <a href="#!">Help</a>
                                </li>
                                <li className="list-unstyled">
                                    <a href="#!">Blog</a>
                                </li>
                                <li className="list-unstyled">
                                    <a href="#!">Terms & Conditions</a>
                                </li>
                                <li className="list-unstyled">
                                    <a href="#!">Contact</a>
                                </li>
                                </ul>
                            </Col>
                        </Row>
                    </Container>
                    <div className="footer-copyright text-center py-3">
                        <Container fluid>
                            &copy; {new Date().getFullYear()} Copyright: <a href="https://www.MDBootstrap.com"> Cryto Trading </a>
                        </Container>
                    </div>
                </footer>
            );
        }else{
            return(
                null
            );
        }
    }    
}

export default Footer;