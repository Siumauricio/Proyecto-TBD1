import { Component, Inject, Injectable, OnInit } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { Router } from '@angular/router';
import { sign } from 'crypto';
import { Local } from 'protractor/built/driverProviders';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css',
  ],
})

export class HomeComponent  {
    nombre_Usuario: string; 
    contrasena_Usuario: string;
    http: HttpClient;
    baseUrl: string;
  router: Router;
  lstUsuarios: Usuarios[];
  login: boolean;

  constructor(http: HttpClient, @Inject("BASE_URL") baseUrl: string, rou: Router) {
    this.login = false;
    this.router = rou;
    this.http = http;
    this.baseUrl = baseUrl;
  }

  Validar() {
    this.nombre_Usuario = (document.getElementById("username") as HTMLInputElement).value;
    this.contrasena_Usuario = (document.getElementById("password") as HTMLInputElement).value;
    console.log(this.nombre_Usuario + " " + this.contrasena_Usuario);
    var body;
    let stoks: Usuarios = <Usuarios>{ "Username": this.nombre_Usuario, "Contrasenia": this.contrasena_Usuario };
    return this.http.post<Usuarios[]>(this.baseUrl + "api/Admin/login", stoks).subscribe(res => {
      body = res;
    }, error => { console.log(error); this.login = true;}
      , () => {
        var nombre: string;
        var username: string;
        var rolesIdRol: number;
        var idUsuario: number;
        var direccion: string;
        var telefono: number;

        var contador = 0;
        for (var i in body) {
          var key = i;
          var val = body[i];
          for (var j in val) {
            var sub_key = j;
            var sub_val = val[j];
            if (sub_key == "nombre") {
              nombre = sub_val;
            }
            if (sub_key == "username") {
              username = sub_val;
            }
            if (sub_key == "idUsuario") {
              idUsuario = sub_val;
            }
            if (sub_key == "direccion") {
              direccion = sub_val;
            }
            if (sub_key == "rolesIdRol") {
              rolesIdRol = sub_val;
            }
            if (sub_key == "telefono") {
              telefono = sub_val;
            }
          } 
          contador++;
        }
        localStorage.setItem("username", username);
        localStorage.setItem("nombre", nombre);
        localStorage.setItem("idUsuario", String(idUsuario));
        localStorage.setItem("direccion", direccion);
        localStorage.setItem("rolesIdRol", String(rolesIdRol));
        localStorage.setItem("telefono", String(telefono));
        if (rolesIdRol == 1) {
          this.router.navigateByUrl('/DashboardCClientes', { queryParams: { id: idUsuario, usern: username, rol: rolesIdRol, direc: direccion, telf: telefono, name: nombre } });
        } else if (rolesIdRol == 2) {
          this.router.navigateByUrl('/DashboardAdmin', { queryParams: { id: idUsuario, usern: username, rol: rolesIdRol, direc: direccion, telf: telefono, name: nombre } });
        } else if (rolesIdRol == 3) {
          this.http.post(this.baseUrl + "api/Admin/Jp/" + idUsuario, stoks).subscribe(res => { }, error => { this.router.navigateByUrl('/DashboardEmpleados', { skipLocationChange: true }); }, () => {
            this.router.navigateByUrl('/DashboardJP', { skipLocationChange: true });
          });
        }
      }
    );
   

 }
}

interface Usuarios {
  Username: string;
  Contrasenia: string;
}


    //http.get<Clientes[]>(baseUrl + "api/Admin/Clientes").subscribe(result => {
    //  this.lstUsuarios = result;
    //}, error => console.error(error));


  //  this.nombre_Usuario = (document.getElementById("username") as HTMLInputElement).value;
  //  this.contrasena_Usuario = (document.getElementById("password") as HTMLInputElement).value; 
  //  //let stoks: Clientes = <Clientes>{"IdCliente":"25","Email":"Hola@","Pais":"Hola","UsuariosIdUsuario":25};
