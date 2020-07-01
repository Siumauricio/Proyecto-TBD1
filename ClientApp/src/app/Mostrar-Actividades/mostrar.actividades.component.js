//import { Component, Inject } from '@angular/core';
//import { HttpClient } from '@angular/common/http';
//import { Router } from '@angular/router';
//import { parse } from 'url';
//import { Local } from 'protractor/built/driverProviders';
//@Component({
//  selector: 'TablasEmpleados-home',
//  templateUrl: './mostrar.historias.component.html',
//  styleUrls: ['../Dashboard-Admin/dashboardA.component.css'],
//})
//export class MostrarActividadesComponent {
//  lstUsuarios: Historias[];
//  enableEdit = false;
//  enableEditIndex = null;
//  enableDelete = false;
//  enableShow = true;
//  enableShowD = false;
//  router: Router;
//  constructor(http: HttpClient, @Inject("BASE_URL") baseUrl: string, rout: Router) {
//    this.router = rout;
//    http.get<Actividades[]>(baseUrl + "api/Admin/obtenerActividades/" + parseInt(localStorage.getItem("idUsuario"))).subscribe(result => {
//      this.lstUsuarios = result;
//      console.log(this.lstUsuarios);
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
//  eliminarUser(jp) {
//    if (jp == parseInt(localStorage.getItem("idUsuario"))) {
//      return false;
//    }
//    return true;
//  }
//  getClient(nr) {
//    localStorage.setItem("idHistoria", nr);
//    this.router.navigateByUrl('/RegistroActividades', { skipLocationChange: true });
//  }
//}
//interface Actividades {
//  idHistoria: number;//
//  prioridad: string;//
//  funcionalidades: string;//
//  Clientes_id_usuario: number;//
//  id_proyecto: number;//
//  Equipos_Trabajo_id_grupo: number;//
//}
//# sourceMappingURL=mostrar.actividades.component.js.map