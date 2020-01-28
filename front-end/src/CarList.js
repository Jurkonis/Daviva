import React, { Component } from "react";
import Car from "./Car";
import "bootstrap/dist/css/bootstrap.min.css";

export default class CarList extends Component {
  constructor(props) {
    super(props);
    this.state = {
      auto: { nuotraukos: [] },
      autoArray: [],
      proxyUrl: "https://cors-anywhere.herokuapp.com/",
      targetUrl: "https://backend.daviva.lt/API/InformacijaTestui"
    };
  }

  componentDidMount() {
    fetch(this.state.proxyUrl + this.state.targetUrl)
      .then(response => response.json())
      .then(data => {
        this.state.autoArray.push(data);
        this.setState({
          auto: data
        });
        console.log(this.state.autoArray);
      });
  }

  addCar() {
    fetch(this.state.proxyUrl + this.state.targetUrl)
      .then(response => response.json())
      .then(data => {
        this.state.autoArray.push(data);
        this.setState({
          auto: data
        });
        console.log(this.state.autoArray);
      })
      .catch(e => {
        console.log(e);
        return e;
      });
  }

  render() {
    let car = this.state.autoArray.map(function(auto, index) {
      return <Car key={index} brand={auto.marke} model={auto.modelis} year={auto.metai} price={auto.kaina} photos={auto.nuotraukos} />;
    });
    return (
      <div>
        <div className="carList">{car}</div>
        <button type="button" class="btn btn-primary btn-sm mt-2 ml-2" onClick={this.addCar.bind(this)}>
          Pridėti
        </button>
      </div>
    );
  }
}
