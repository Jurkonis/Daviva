import React, { Component } from "react";
import "./App.css";
import "slick-carousel/slick/slick.css";
import "slick-carousel/slick/slick-theme.css";
import Slider from "react-slick";

export default class Car extends Component {
  render() {
    const settings = {
      dots: false,
      fade: true,
      infinite: true,
      speed: 500,
      slidesToShow: 1,
      arrows: true,
      slidesToScroll: 1,
      className: "slides"
    };
    return (
      <div className="car">
        <Slider {...settings}>
          {this.props.photos.map(function(photo, index) {
            return (
              <div className="slide">
                <img key={index} src={photo} alt="car" />
              </div>
            );
          })}
        </Slider>
        <div className="autoInfo">
          <p className="carBrand">{this.props.brand}</p>
          <p>Modelis: {this.props.model}</p>
          <p>Metai: {this.props.year}</p>
          <div className="price">{this.props.price} €</div>
        </div>
      </div>
    );
  }
}
