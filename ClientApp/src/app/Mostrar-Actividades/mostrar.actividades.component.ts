import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { parse } from 'url';
import { Local } from 'protractor/built/driverProviders';


@Component({
  selector: 'TablasEmpleados-home',
  templateUrl: './mostrar.actividades.component.html',
  styleUrls: ['../Dashboard-Admin/dashboardA.component.css'],
})

export class MostrarActividadesComponent {
  lstUsuarios: Actividades[];
  enableEdit = false;
  enableEditIndex = null;
  enableDelete = false;
  enableShow = true;
  enableShowD = false;
  router: Router;
  baseUrl: string;
  http: HttpClient;


  constructor(http: HttpClient, @Inject("BASE_URL") baseUrl: string, rout: Router) {
    http.get<Actividades[]>(baseUrl + "api/Admin/obtenerActividades/" + parseInt(localStorage.getItem("idUsuario"))).subscribe(result => {
      this.lstUsuarios = result;
      console.log(this.lstUsuarios);
    }, error => console.error(error));
    this.router = rout;
    this.baseUrl = baseUrl;
    this.http = http;
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

  eliminarUser(jp) {
    if (jp == parseInt(localStorage.getItem("idUsuario"))) {
      return false;
    }
    return true;
  }

  getClient(nr) {
    localStorage.setItem("idActividad", nr);
    this.router.navigateByUrl('/DashboardEmpleados', { skipLocationChange: true });
    this.http.put(this.baseUrl + "api/Admin/actualizarActividad/" + parseInt(localStorage.getItem("idActividad")), parseInt(localStorage.getItem("idActividad"))).subscribe(result => {
      console.log(this.lstUsuarios);
    }, error => console.error(error));
  }

}
interface Actividades {
  idActividad: number;//
  fechaEntrega: string;//
  detalles: string;//
  tiempoEstimado: number;//
  estadoSolicitud: string;//
}
