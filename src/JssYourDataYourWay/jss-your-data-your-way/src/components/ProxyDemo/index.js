import React from 'react';
import { Text } from '@sitecore-jss/sitecore-jss-react';

const ProxyDemo = (props) => (
  <div>
    <p className="header">ProxyDemo Component</p>    

    {props.fields.success && 
      <>
        <p>Details: {props.fields.content.content}</p> 
        <p>Source: {props.fields.content.source}</p> 
      </>
    }
    
    {!props.fields.success && <p>Details: {props.fields.content.content}</p> }
  </div>
);

export default ProxyDemo;
