import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { parse } from 'url';
import { Local } from 'protractor/built/driverProviders';


@Component({
  selector: 'TablasEmpleados-home',
  templateUrl: './integrantes.component.html',
  styleUrls: ['../Dashboard-Admin/dashboardA.component.css'],
})

export class MostrarIntegrantesComponent {
  lstUsuarios: Empleados[];
  enableEdit = false;
  enableEditIndex = null;
  enableDelete = false;
  enableShow = true;
  enableShowD = false;
    router: Router;

  constructor(http: HttpClient, @Inject("BASE_URL") baseUrl: string, rout: Router) {
    this.router = rout;
    http.get<Empleados[]>(baseUrl + "api/Admin/integrantes/" + parseInt(localStorage.getItem("idUsuario"))).subscribe(result => {
      this.lstUsuarios = result;
      console.log(this.lstUsuarios);
    }, error => console.error(error));
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
    localStorage.setItem("idEmpleado", nr);
    this.router.navigateByUrl('/MostrarHistoriasDisponibles', { skipLocationChange: true });
  }

}
interface Empleados {
  idUsuario: number;//
  idEmpleado: string;//
  nombre: string;//
  username: string;//
  puestosTrabajoIdPuesto: number;//
}
