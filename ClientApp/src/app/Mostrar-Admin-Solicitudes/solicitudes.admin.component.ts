import { Component, Inject, Injectable, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { sign } from 'crypto';
import { Local } from 'protractor/built/driverProviders';

@Component({
  selector: 'DashboardA-app',
  templateUrl: "./solicitudes.admin.component.html",
  styleUrls: ['../Dashboard-Admin/dashboardA.component.css'],
})

export class SolicitudesProyectos {
idTicket: number;
lstUsuarios: Solicitudes[];
  enableEdit = false;
  baseUrl: string;
  router: Router;
  http: HttpClient;


  constructor(http: HttpClient, @Inject("BASE_URL") baseUrl: string, rou: Router) {
    http.get<Solicitudes[]>(baseUrl + "api/Admin/mostrarSolicitudesE").subscribe(result => {
      this.lstUsuarios = result;
      console.log(this.lstUsuarios);
    }, error => console.error(error));
      this.router = rou;
      this.http = http;
      this.baseUrl = baseUrl;
    }

    enableEditMethod() {
      this.enableEdit = true;
  }
  disableEditMethod() {
    this.enableEdit = false;
  }
  actualizarProyecto() {
    this.idTicket = parseInt((document.getElementById("ticket") as HTMLInputElement).value);
    console.log(this.idTicket);
    this.http.put(this.baseUrl + "api/Admin/actualizarSolicitud/"+this.idTicket,this.idTicket).subscribe(res => {
    }, error => console.log(error)
      , () => {
        this.router.navigateByUrl('/DashboardAdmin', { skipLocationChange: true  });
 });
  }

  denegarProyecto() {
    this.idTicket = parseInt((document.getElementById("ticket") as HTMLInputElement).value);
    console.log(this.idTicket);
    this.http.put(this.baseUrl + "api/Admin/actualizarSolicitudD/" + this.idTicket, this.idTicket).subscribe(res => {
    }, error => console.log(error)
      , () => {
        this.router.navigateByUrl('/DashboardAdmin', { skipLocationChange: true });
      });

  }

}

interface Solicitudes {
  estado_solicitud: string;
  id_ticket: number;
  presupuesto: number;
  tecnologias: string;
  nombre: string;
  Clientes_id_usuario: number;

}

