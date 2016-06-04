import React from 'react';
import ReactDOM from 'react-dom';
import Nav from './components/NavBar.js';
import ReactTable from './components/ReactTable.js';
import { ButtonToolbar, Button } from 'react-bootstrap';
import $ from 'jquery';

export default class OAuthv2 extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      fileUrls: [],
    };

    this.makeRequest = this.makeRequest.bind(this);
    this.setUrls = this.setUrls.bind(this);
  }

  setUrls(data) {
    this.setState({
      fileUrls: data.FileUrls,
    });
  }

  makeRequest(url) {
    const saveToState = this.setUrls;
    $.ajax({
      url: '/Home/' + url,
      dataType: 'json',
      success(data) {
        if (data.UserName) {
          alert('User name: ' + data.UserName + '\nUserToken: ' + data.UserToken);
        } else {
          saveToState(data);
        }
      },
      error(xhr, status, err) {
        alert(err, status);
      },
    });
  }

  render() {
    const data = this.state.fileUrls;
    console.log(data);
    return (
      <div>
        <Nav />,
        <div className="col-md-4 text-center">
          <ButtonToolbar>
            <Button
              bsStyle="success"
              bsSize="large"
              onClick={() => this.makeRequest('ShowDriveData')}
            > Show Resource Data
            </Button>
            <Button
              bsStyle="primary"
              bsSize="large"
              onClick={() => this.makeRequest('ShowAuthenticationValues')}
            > Show Authentication Values
            </Button>
          </ButtonToolbar>,
          <ReactTable urls={data} />
        </div>
      </div>
    );
  }
}

ReactDOM.render(
  <OAuthv2 />,
  document.getElementById('app')
);

OAuthv2.propTypes = { fileUrls: React.PropTypes.array };
