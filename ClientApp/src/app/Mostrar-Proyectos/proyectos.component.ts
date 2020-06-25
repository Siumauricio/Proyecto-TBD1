import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';


@Component({
  selector: 'TablasEquipos-home',
  templateUrl: './proyectos.component.html',
  styleUrls: ['../Dashboard-Admin/dashboardA.component.css'],
})

export class TablasProyectos {
  lstUsuarios: Proyectos[];
  enableEdit = false;
  enableEditIndex = null;
  enableDelete = false;
  enableShow = true;
  enableShowD = false;

  constructor(http: HttpClient, @Inject("BASE_URL") baseUrl: string) {
    http.get<Proyectos[]>(baseUrl + "api/Admin/Proyectos").subscribe(result => {
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
interface Proyectos {
  idProyecto: number;//
  github: string;
  trello: string;
  fechaCreacion: string;
  fechaAsignacion: string;
  equiposTrabajoIdGrupo: number;
}
