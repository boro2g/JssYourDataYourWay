import React from 'react';
import { Text } from '@sitecore-jss/sitecore-jss-react';

const ExampleContentDemo = (props) => (
  <div>
    <p className="header">ExampleContentDemo Component</p>
    <Text field={props.fields.heading} />
    <p>{props.fields.date}</p>
  </div>
);

export default ExampleContentDemo;
