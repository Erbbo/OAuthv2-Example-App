import React from 'react';
import ReactDOM from 'react-dom';
import Nav from './components/NavBar.js';

export default class OAuthv2 extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      clientId: '',
      clientSecret: '',
      loginUrl: ''
    }
  }

  render() {
    return (
      <div>
        <Nav />,
        <div>
            Hello Test!!
        </div>
      </div>
    )
  };
}

ReactDOM.render(
  <OAuthv2 />,
  document.getElementById('app')
);