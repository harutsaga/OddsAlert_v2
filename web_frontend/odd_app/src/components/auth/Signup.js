import React from 'react';
import { Container, Row, Col } from 'reactstrap';
import SignupBox from './SignupBox';
import Header from 'components/layouts/Header'

class Signup extends React.Component {
    render() {
    return (
      <>
        <Header/>
        <Container fluid className="mt-100 mb-100">
          <Row>
              <Col md='4'/>
              <Col md='4'><SignupBox></SignupBox></Col>
          </Row>
        </Container>
      </>
    );
  }
}

export default Signup;