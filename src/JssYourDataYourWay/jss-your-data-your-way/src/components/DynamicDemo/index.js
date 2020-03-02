import React from 'react';
import { Text, RichText } from '@sitecore-jss/sitecore-jss-react';

const DynamicDemo = (props) => (
  <div>
    <p className="header">DynamicDemo Component</p>
    <Text field={props.fields.pageTitle} />
    <RichText field={props.fields["Remapped Text"]} />
  </div>
);

export default DynamicDemo;
