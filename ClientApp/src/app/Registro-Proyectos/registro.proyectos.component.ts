import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { ReturnStatement } from '@angular/compiler';
import { error } from 'protractor';


@Component({
  selector: 'registro-component-form',
  templateUrl: './registro.proyectos.component.html',
  styleUrls: ['../Dashboard-Admin/dashboardA.component.css',
    '../Registro/registro.clientes.component.css'
  ]
})
export class RegistroProyectosComponent {
  http: HttpClient;
  baseUrl: string;
  router: Router;


  constructor(http: HttpClient, @Inject("BASE_URL") baseUrl: string, rou: Router) {
    this.router = rou;
    this.http = http;
    this.baseUrl = baseUrl;
  }
  RegistroProyectos = new FormGroup({
    Nombre: new FormControl(''),
    tecnologias: new FormControl(''),
    presupuesto: new FormControl('')
  });

  onSubmit() {
    const Proyecto: Proyecto = <Proyecto>{
      "presupuesto": this.RegistroProyectos.get('presupuesto').value,
      "tecnologias": this.RegistroProyectos.get('tecnologias').value,
      "nombre": this.RegistroProyectos.get('Nombre').value
    };
    const Clientex_x_Proyectos: ClientesXSolicitud = <ClientesXSolicitud>{
      "ClientesIdUsuario": parseInt(localStorage.getItem("idUsuario")),
      "estadoSolicitud": "EN ESPERA"
    };

    this.http.post(this.baseUrl + "api/Admin/agregarProyectos", Proyecto).subscribe(res => {
    }, error => { console.log(error) },
      () => {
        this.http.post(this.baseUrl + "api/Admin/agregarP_C", Clientex_x_Proyectos).subscribe(() => {
          this.router.navigateByUrl("/DashboardCClientes", { skipLocationChange: true });
        });
      });

  }
}
interface Proyecto {
  presupuesto: number;
  tecnologias: string;
  nombre: string;
}
interface ClientesXSolicitud {
  ClientesIdUsuario: number;
  estadoSolicitud: string ;
}
