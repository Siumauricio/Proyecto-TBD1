import { Component, Inject, Input, OnInit, ApplicationInitStatus } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HomeComponent } from '../home/home.component';
import { Injectable } from '@angular/core';
import { Subscription } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { Local } from 'protractor/built/driverProviders';

@Component({
  selector: 'DashboardA-app',
  templateUrl: "./dashboard.empleados.component.html",
  styleUrls: ['./dashboard.empleados.component.css'],
})

export class DashboardEComponent {
  nombre: string;
  username: string;
  rolesIdRol: string;
  idUsuario: string;
  direccion: string;
  telefono: string;
  enableEdit = false;
  enableEditIndex = null;
  enableDelete = false;
  enableShow = true;
  enableShowD = false;

  constructor(private _router: Router, private activatedRoute: ActivatedRoute) { }

  ngOnInit() {
    this.nombre = localStorage.getItem("nombre");
    this.username = localStorage.getItem("username");
    this.direccion = localStorage.getItem("direccion");
    this.idUsuario = localStorage.getItem("idUsuario");
    this.rolesIdRol = localStorage.getItem("rolesIdRol");
    this.telefono = localStorage.getItem("telefono");
  }


  enableEditMethod(e, i) {
    this.enableEdit = true;

    this.enableEditIndex = i;
    console.log(i, e);
  }
  enableDeleteMethod() {
    this.enableDelete = true;
    this.enableShowD = true;

  }
  enableAll() {
    this.enableShowD = false;
    this.enableDelete = false;
  }

}
