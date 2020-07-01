import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';


@Component({
  selector: 'TablasSolicitudes-home',
  templateUrl: './mostrar.proyectos.component.html',
  styleUrls: ['../Dashboard-Admin/dashboardA.component.css'],
})

export class TablasSolicitudesProyectos {
  lstUsuarios: Solicitudes[];
  enableEdit = false;
  baseUrl: string;
  router: Router;
  idTicket: number;
  http: HttpClient;
  constructor(http: HttpClient, @Inject("BASE_URL") baseUrl: string, rou: Router) {
    this.router = rou;
    this.http = http;
    this.baseUrl = baseUrl;
    let stoks: Cliente = <Cliente>{ "usuariosIdUsuario": parseInt(localStorage.getItem("idUsuario")) };
    console.log(stoks);
    http.post<Solicitudes[]>(baseUrl + "api/Admin/mostrarSolicitudes", stoks).subscribe(result => {
      this.lstUsuarios = result;
      console.log(this.lstUsuarios);
    }, error => console.error(error));
  }
  enableEditMethod() {
    this.enableEdit = true;
  }
  disableEditMethod() {
    this.enableEdit = false;
  }

  actualizarEstado() {
    this.idTicket = parseInt((document.getElementById("ticket") as HTMLInputElement).value);
    this.enableEdit = false;
    this.http.put(this.baseUrl + "api/Admin/reactivarSolicitud/" + this.idTicket + "/" + parseInt(localStorage.getItem("idUsuario")), this.idTicket).subscribe(res => {
    }, error => { console.log(error) }
      , () => {
        this.router.navigateByUrl('/DashboardCClientes', { skipLocationChange: true });
      });
  }
}
interface Solicitudes {
  estado_solicitud: string;
  id_ticket: number;
  presupuesto: number;
  tecnologias: string;
  nombre: string;
}
interface Cliente{
  usuariosIdUsuario: number;
}
