//import { Component, Inject } from '@angular/core';
//import { HttpClient } from '@angular/common/http';
//import { Router } from '@angular/router';
//@Component({
//  selector: 'TablasClientes-home',
//  templateUrl: './clientes.component.html',
//  styleUrls: ['../Dashboard-Admin/dashboardA.component.css'],
//})
//export class TablasClientes {
//  lstUsuarios : Clientes[];
//  enableEdit = false;
//  enableEditIndex = null;
//  enableDelete = false;
//  enableShow = true;
//  enableShowD = false;
//  constructor(http: HttpClient, @Inject("BASE_URL") baseUrl: string) {
//     http.get<Clientes[]>(baseUrl + "api/Admin/Clientes").subscribe(result => {
//       this.lstUsuarios = result;
//       console.log(this.lstUsuarios);
//    }, error => console.error(error));
//  }
//  enableEditMethod(e, i) {
//    this.enableEdit = true;
//    this.enableEditIndex = i;
//    console.log(i, e);
//  }
//  enableDeleteMethod() {
//    this.enableDelete = true;
//    this.enableShowD = true;
//  }
//  enableAll() {
//    this.enableShowD = false;
//    this.enableDelete = false;
//  }
//}
//interface Clientes {
//  Direccion: string;//
//  Email: string;
//  IdCliente: string;//
//  IdUsuario: number;//
//  Nombre: string;//
//  Pais: string;//
//  RolesIdRol: number;//
//  Telefono: number;
//  Username: string;//
//}
//# sourceMappingURL=clientes.component.js.map