import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Local } from 'protractor/built/driverProviders';


@Component({
  selector: 'registro-habilidades-form',
  templateUrl: './registro.habilidades.component.html',
  styleUrls: ['../Dashboard-Admin/dashboardA.component.css',
    '../Registro/registro.clientes.component.css'
  ]
})
export class RegistroHabilidadesComponent {
  http: HttpClient;
  baseUrl: string;
  router: Router;

  constructor(http: HttpClient, @Inject("BASE_URL") baseUrl: string, rou: Router) {
    this.router = rou;
    this.http = http;
    this.baseUrl = baseUrl;
  }

  clientesForm = new FormGroup({
    idemp: new FormControl(''),
    nombre: new FormControl(''),
    descricpion: new FormControl('')
  });
  onSubmit() {
    const habilidad: Habilidades = <Habilidades>{
      "nombreHabilidad": this.clientesForm.get('nombre').value,
      "Descripcion": this.clientesForm.get('descricpion').value
    };
    this.http.post(this.baseUrl + "api/Admin/asignarHabilidades/" + this.clientesForm.get('idemp').value, habilidad).subscribe(res => {

    }, error => { console.log(error) }, () => {
      this.router.navigateByUrl("/DashboardAdmin", { skipLocationChange: true });
    }
    );
  }
}
interface Habilidades {
  nombreHabilidad: string;
  Descripcion: number;
}
