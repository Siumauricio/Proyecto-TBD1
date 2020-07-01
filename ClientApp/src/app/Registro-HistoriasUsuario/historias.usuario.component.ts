import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { ReturnStatement } from '@angular/compiler';
import { error } from 'protractor';


@Component({
  selector: 'registro-component-form',
  templateUrl: './historias.usuario.component.html',
  styleUrls: ['../Dashboard-Admin/dashboardA.component.css',
    '../Registro/registro.clientes.component.css'
  ]
})
export class RegistroHistoriaComponent {
  http: HttpClient;
  baseUrl: string;
  router: Router;


  constructor(http: HttpClient, @Inject("BASE_URL") baseUrl: string, rou: Router) {
    this.router = rou;
    this.http = http;
    this.baseUrl = baseUrl;
  }
  RegistroHistorias = new FormGroup({
    prioridad: new FormControl(''),
    funcionalidades: new FormControl(''),
  });

  onSubmit() {
    const Historia: Historias = <Historias>{
      "prioridad": this.RegistroHistorias.get('prioridad').value,
      "funcionalidades": this.RegistroHistorias.get('funcionalidades').value
    };

    this.http.post(this.baseUrl + "api/Admin/crearHistoria/" + parseInt(localStorage.getItem("id_ticket")), Historia).subscribe(res => {
    }, error => { console.log(error) },
      () => {
        this.router.navigateByUrl("/DashboardCClientes", { skipLocationChange: true });
      });

  }
}
interface Historias {
  prioridad: number;
  funcionalidades: string;
}

