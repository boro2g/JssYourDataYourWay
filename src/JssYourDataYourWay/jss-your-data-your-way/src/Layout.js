import React from "react";
import {
  Placeholder,
  VisitorIdentification,
  withSitecoreContext,
} from "@sitecore-jss/sitecore-jss-react";
import { NavLink } from "react-router-dom";
import { withTranslation } from "react-i18next";
import Helmet from "react-helmet";

// Using bootstrap is completely optional. It's used here to provide a clean layout for samples,
// without needing extra CSS in the sample app. Remove it in package.json as well if it's removed here.
import "bootstrap/dist/css/bootstrap.css";
import "./assets/app.css";
import logo from "./assets/sc_logo.svg";

/*
  APP LAYOUT
  This is where the app's HTML structure and root placeholders should be defined.

  All routes share this root layout by default (this could be customized in RouteHandler),
  but components added to inner placeholders are route-specific.
*/

// This is boilerplate navigation for sample purposes. Most apps should throw this away and use their own navigation implementation.
// Most apps may also wish to use GraphQL for their navigation construction; this sample does not simply to support disconnected mode.
let Navigation = ({ t, i18n }) => (
  <div className="d-flex flex-column flex-md-row align-items-center p-3 px-md-4 mb-3 bg-white border-bottom">
    <h5 className="my-0 mr-md-auto font-weight-normal">
      <NavLink to="/" className="text-dark">
        <img src={logo} alt="Sitecore" />
      </NavLink>
    </h5>
    <p>JSS - Your Data, Your Way</p>
  </div>
);

let ContextDemo = (props) => (
  <div data-e2e-class="styleguide-layout-tabs-tab">
    <p className="header">Context Extension demo</p>
    <p>
      isAnonymous: {props.sitecoreContext.securityInfo.isAnonymous.toString()}
    </p>
  </div>
);

// inject dictionary props (`t`) into navigation so we can translate it
// NOTE: using this is needed instead of using i18next directly to keep
// the component state updated when i18n state (e.g. current language) changes
Navigation = withTranslation()(Navigation);

ContextDemo = withSitecoreContext()(ContextDemo);

const Layout = ({ route }) => (
  <React.Fragment>
    {/* react-helmet enables setting <head> contents, like title and OG meta tags */}
    <Helmet>
      <title>
        {(route.fields &&
          route.fields.pageTitle &&
          route.fields.pageTitle.value) ||
          "Page"}
      </title>
    </Helmet>
    <link rel="stylesheet" href="https://unpkg.com/98.css" />
    <style>{"\
        .window{\
          font-size:16px;\
        }\
      "}</style>
    {/*
      VisitorIdentification is necessary for Sitecore Analytics to determine if the visitor is a robot.
      If Sitecore XP (with xConnect/xDB) is used, this is required or else analytics will not be collected for the JSS app.
      For XM (CMS-only) apps, this should be removed.

      VI detection only runs once for a given analytics ID, so this is not a recurring operation once cookies are established.
    */}
    <VisitorIdentification />
    <div className="window" style={{ padding: 20 }}>
      <Navigation />

      {/* root placeholder for the app, which we add components to using route data */}
      <div className="container">
        <ContextDemo />

        <Placeholder name="jss-main" rendering={route} />
      </div>
    </div>
  </React.Fragment>
);

export default Layout;
