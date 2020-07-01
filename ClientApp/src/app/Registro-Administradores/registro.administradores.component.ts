import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { ReturnStatement } from '@angular/compiler';
import { error } from 'protractor';


@Component({
  selector: 'registro-component-form',
  templateUrl: './registro.administradores.component.html',
  styleUrls: ['../Dashboard-Admin/dashboardA.component.css',
    '../Registro/registro.clientes.component.css'
  ]
})
export class RegistroAdminComponent {
  http: HttpClient;
  baseUrl: string;
  router: Router;


  constructor(http: HttpClient, @Inject("BASE_URL") baseUrl: string, rou: Router) {
    this.router = rou;
    this.http = http;
    this.baseUrl = baseUrl;
  }
  clientesForm = new FormGroup({
    username: new FormControl(''),
    password: new FormControl(''),
    name: new FormControl(''),
    phoneNumber: new FormControl(''),
    address: new FormControl(''),
    rol: new FormControl(''),
    pais: new FormControl(''),
    email: new FormControl('')
  });


  onSubmit() {

    const rememberUser: Usuario = <Usuario>{
      "Username": this.clientesForm.get('username').value,
      "Contrasenia": this.clientesForm.get('password').value,
      "Nombre": this.clientesForm.get('name').value,
      "Telefono": this.clientesForm.get('phoneNumber').value,
      "Direccion": this.clientesForm.get('address').value,
      "RolesIdRol": this.clientesForm.get('rol').value
    };


    var estado = 0;
    this.http.post(this.baseUrl + "api/Admin/agregar", rememberUser).subscribe(res => {
      if (res) {
        console.log(res);
      }
    }, error => { console.log(error) }, () => {
        this.router.navigateByUrl("/DashboardAdmin", { skipLocationChange: true });
      }
    );

    //console.log("estado: " + estado);
  }
}
interface Usuario {
  Username: string;
  Contrasenia: string;
  Nombre: string;
  Telefono: number;
  Direccion: string;
  RolesIdRol: number;
}
