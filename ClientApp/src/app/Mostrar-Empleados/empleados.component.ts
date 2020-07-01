import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';


@Component({
  selector: 'TablasEmpleados-home',
  templateUrl: './empleados.component.html',
  styleUrls: ['../Dashboard-Admin/dashboardA.component.css'],
})

export class TablasEmpleados {
  lstUsuarios: TablasEmpleados[];
  enableEdit = false;
  enableEditIndex = null;
  enableDelete = false;
  enableShow = true;
  enableShowD = false;

  constructor(http: HttpClient, @Inject("BASE_URL") baseUrl: string) {
    http.get<TablasEmpleados[]>(baseUrl + "api/Admin/Empleados").subscribe(result => {
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
}
interface Empleados {
  direccion: string;//
  fechaContratacion: string;
  idEmpleado: string;//
  idUsuario: number;//
  idGrupo: number;//
  nombre: string;//
  RolesIdRol: number;//
  salario: number;
  telefono: number;//
  username: string;
}
