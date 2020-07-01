import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { parse } from 'path';


@Component({
  selector: 'TablasSolicitudes-home',
  templateUrl: './proyectos.aprobados.component.html',
  styleUrls: ['../Dashboard-Admin/dashboardA.component.css'],
})

export class ClientesProyectosAprobados {
  lstUsuarios: Solicitudes[];

  constructor(http: HttpClient, @Inject("BASE_URL") baseUrl: string) {
    let stoks: Cliente = <Cliente>{ "usuariosIdUsuario": parseInt(localStorage.getItem("idUsuario")) };
    http.put<Solicitudes[]>(baseUrl + "api/Admin/proyectosAprobados/" + parseInt(localStorage.getItem("idUsuario")), parseInt(localStorage.getItem("idUsuario"))).subscribe(result => {
      this.lstUsuarios = result;
      console.log(this.lstUsuarios);
    }, error => console.error(error));
  }
  getClient(nr) {
    localStorage.setItem("id_ticket",nr);
  }

}
interface Solicitudes {
  estado_solicitud: string;
  id_ticket: number;
  presupuesto: number;
  tecnologias: string;
  nombre: string;
}
interface Cliente {
  usuariosIdUsuario: number;
}
