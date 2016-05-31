import React from 'react';
import { Navbar } from 'react-bootstrap';

export default class Nav extends React.Component {
  render() {
    return (
      <Navbar fluid>
        <Navbar.Header>
          <Navbar.Brand>
            <a className="container-fluid" href="#">OAuthv2 Example Logins</a>
          </Navbar.Brand>
          <Navbar.Toggle />
        </Navbar.Header>
      </Navbar>
    );
  }
}