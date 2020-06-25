import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { FormControl, FormGroup } from '@angular/forms';


@Component({
  selector: 'TablasEquipos-home',
  templateUrl: './registro.equipos.component.html',
  styleUrls: ['../Registro/registro.clientes.component.css', '../Dashboard-Admin/dashboardA.component.css'],
})

export class RegistroEquiposComponent {
  http: HttpClient;
  baseUrl: string;
  router: Router;

  constructor(http: HttpClient, @Inject("BASE_URL") baseUrl: string, rou: Router) {
    this.router = rou;
    this.http = http;
    this.baseUrl = baseUrl;
  }
  clientesForm = new FormGroup({
    nombre: new FormControl(''),
  });

  onSubmit() {
    const grupo: Equipos = <Equipos>{
      "nombre": this.clientesForm.get('nombre').value
    };
    this.http.post(this.baseUrl + "api/Admin/agregarG", grupo).subscribe(res => {
      if (res) {
        console.log(res);
        this.router.navigateByUrl("/DashboardAdmin", { skipLocationChange: true });

      } else {
        alert("Warning");
      }
    }, error => console.log(error));
  }
}
interface Equipos {
  nombre: string;
}
