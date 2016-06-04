import React from 'react';
import { BootstrapTable } from 'react-bootstrap-table';

export default class ReactTable extends React.Component {
  
  urlFormatter(cell) {
    return ('<a href="' + cell + '">' + cell + '</a>');
  }

  render() {
    const formatUrl = this.urlFormatter;
    return (
	  <BootstrapTable data={this.props.urls} striped={true} hover={true}>
        <TableHeaderColumn dataField="FileType" isKey={true} dataSort={true}>File Type</TableHeaderColumn>
		<TableHeaderColumn dataField="FileUrl" dataFormat={formatUrl}>File Url</TableHeaderColumn>
	  </BootstrapTable>
	 );
  }
}

ReactTable.propTypes = { urls: React.PropTypes.array };
