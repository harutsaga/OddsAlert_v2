import React from 'react';
import { Container, Row, Col } from 'reactstrap';
import LoginBox from './LoginBox';
import Header from 'components/layouts/Header';

class Login extends React.Component {
  render() {
    return (
      <>
        <Header />
        <Container fluid className="mt-100 mb-100">
          <Row>
            <Col md='4' />
            <Col md='4'><LoginBox logged_in={this.props.logged_in}></LoginBox></Col>
          </Row>
        </Container>
      </>
    );
  }
}

export default Login;