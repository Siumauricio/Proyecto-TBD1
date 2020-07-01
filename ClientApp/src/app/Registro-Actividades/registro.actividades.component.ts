import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { ReturnStatement } from '@angular/compiler';
import { error } from 'protractor';


@Component({
  selector: 'registro-component-form',
  templateUrl: './registro.actividades.component.html',
  styleUrls: ['../Dashboard-Admin/dashboardA.component.css',
    '../Registro/registro.clientes.component.css'
  ]
})
export class RegistroActividadesComponent {
  http: HttpClient;
  baseUrl: string;
  router: Router;


  constructor(http: HttpClient, @Inject("BASE_URL") baseUrl: string, rou: Router) {
    this.router = rou;
    this.http = http;
    this.baseUrl = baseUrl;
  }
  actividadesForm = new FormGroup({
    fecha: new FormControl(''),
    tiempo: new FormControl(''),
    detalles: new FormControl('')
  });


  onSubmit() {

    const actividad: Actividades = <Actividades>{
      "fechaEntrega": this.actividadesForm.get('fecha').value,
      "tiempoEstimado": this.actividadesForm.get('tiempo').value,
      "detalles": this.actividadesForm.get('detalles').value,
    };


    var estado = 0;
    this.http.post(this.baseUrl + "api/Admin/registrarActividad/" + localStorage.getItem("idEmpleado") + "/" + parseInt(localStorage.getItem("idHistoria")), actividad).subscribe(res => {
      this.router.navigateByUrl('/DashboardJP', { skipLocationChange: true });

    }, error => { console.log(error); return false; });

  }
}
interface Actividades {
  fechaEntrega: string;
  tiempoEstimado: number;
  detalles: string;
}
