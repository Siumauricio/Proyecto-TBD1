import { Component, OnInit,Inject } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';


@Component({
  selector: 'registro-empleados-component-form',
  templateUrl: './registro.empleados.component.html',
  styleUrls: ['../Dashboard-Admin/dashboardA.component.css',
    '../Registro/registro.clientes.component.css'
  ]
})
export class RegistroEmpleadosComponent {
  http: HttpClient;
  baseUrl: string;
  router: Router;
  fecha = new Date();

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
    salario: new FormControl(''),
    fechan: new FormControl(''),
    grupo: new FormControl(''),
    puesto: new FormControl('')
  });

  onSubmit() {
    const Usuario: Usuario = <Usuario>{
      "Username": this.clientesForm.get('username').value,
      "Contrasenia": this.clientesForm.get('password').value,
      "Nombre": this.clientesForm.get('name').value,
      "Telefono": this.clientesForm.get('phoneNumber').value,
      "Direccion": this.clientesForm.get('address').value,
      "RolesIdRol": this.clientesForm.get('rol').value
    };
    const Empleado: Empleados = <Empleados>{
      "Salario": this.clientesForm.get('salario').value,
      "Activo": "1",
      "FechaNacimiento": this.clientesForm.get('fechan').value,
      "EquiposTrabajoIdGrupo": this.clientesForm.get('grupo').value,
      "PuestosTrabajoIdPuesto": this.clientesForm.get('puesto').value
    };
    console.log(Empleado);

    this.http.post(this.baseUrl + "api/Admin/agregar", Usuario).subscribe(res => {
      if (res) {
        console.log(res);
      } 
    }, error => { console.log(error) },
      () => {
        this.http.post(this.baseUrl + "api/Admin/agregarE", Empleado).subscribe(res => {
        }, () => {
          this.router.navigateByUrl("/DashboardAdmin", { skipLocationChange: true });
        });

      }
    );



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
interface Empleados {
  Salario: number;
  Activo: string;
  FechaNacimiento: string;
  EquiposTrabajoIdGrupo: number;
  PuestosTrabajoIdPuesto: number;
}
