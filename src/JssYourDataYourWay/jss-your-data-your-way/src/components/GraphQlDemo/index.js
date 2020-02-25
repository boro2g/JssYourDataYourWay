import React from 'react';
import { Text } from '@sitecore-jss/sitecore-jss-react';

const GraphQlDemo = (props) => {
  const { datasource } = props.fields.data.item;

  return (
  <div>
    <p className="header">GraphQlDemo Component - id: {props.fields.data.item.id}</p>
    <div dangerouslySetInnerHTML={{__html:props.fields.data.item.text.value}} />
  </div>);
};

export default GraphQlDemo;
