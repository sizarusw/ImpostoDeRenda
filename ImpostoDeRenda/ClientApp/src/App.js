import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { CalculoImpostoDeRenda } from './components/CalculoImpostoDeRenda';

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route  path='/Home' component={Home} />
        <Route exact path='/' component={CalculoImpostoDeRenda} />
      </Layout>
    );
  }
}
