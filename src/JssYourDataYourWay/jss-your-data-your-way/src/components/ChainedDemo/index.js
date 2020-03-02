import React from 'react';
import { Text } from '@sitecore-jss/sitecore-jss-react';

const ChainedDemo = (props) => (
  <div>
    <p className="header">ChainedDemo Component</p>
    From datasource item: <Text field={props.fields["Example field 1"]} />
    <p>From rendering parameters item: {props.fields.RelatedFields.Title}</p>
  </div>
);

export default ChainedDemo;
