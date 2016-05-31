import React from 'react';
import ReactDOM from 'react-dom';
import Nav from './components/NavBar.js';
import { ButtonToolbar, Button } from 'react-bootstrap';
import $ from 'jquery';

export default class OAuthv2 extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      userName: '',
      accessToken: '',
    };

    this.makeRequest = this.makeRequest.bind(this);
    this.saveUser = this.saveUser.bind(this);
  }

  saveUser(data) {
    console.log(data);
    this.setState({
      userName: data.UserName,
      accessToken: data.AccessToken,
    });
    console.log(this.state.userName);
  }

  makeRequest(url) {
    let save = this.saveUser;
    $.ajax({
      url: '/Home/' + url,
      dataType: 'json',
      success(data) {
        save(data);
      },
      error(xhr, status, err) {
        console.log(err.toString());
      },
    });
  }

  render() {
    return (
      <div>
        <Nav />,
        <div className="col-md-4 text-center">
          <ButtonToolbar>
            <Button
              bsStyle="success"
              bsSize="large"
              onClick={() => this.makeRequest('GoogleDrive')}
            > Google Drive
            </Button>
            <Button
              bsStyle="primary"
              bsSize="large"
              onClick={() => this.makeRequest('Gmail')}
            > Gmail
            </Button>
          </ButtonToolbar>
        </div>
      </div>
    );
  }
}

ReactDOM.render(
  <OAuthv2 />,
  document.getElementById('app')
);
