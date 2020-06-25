import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';


import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { DashboardAComponent } from './Dashboard-Admin/dashboardA.component';
import { TablasClientes } from './Mostrar-Clientes/clientes.component';
import { TablasEmpleados } from './Mostrar-Empleados/empleados.component';
import { TablasEquipos } from './Mostrar-Equipos/equipos.component';
import { TablasProyectos } from './Mostrar-Proyectos/Proyectos.component';
import { RegistroClientesComponent } from './Registro/registro.clientes.component';
import { RegistroEmpleadosComponent } from './Registro-Empleados/registro.empleados.component';
import { RegistroEquiposComponent } from './Registro-Equipos/registro.equipos.component';
import { DashboardCComponent } from './Dashboard-Clientes/dashboardC.component';
import { RegistroProyectosComponent } from './Registro-Proyectos/registro.proyectos.component';
import { TablasSolicitudesProyectos } from './Mostrar-Solicitudes-Proyecto/mostrar.proyectos.component';
import { SolicitudesProyectos } from './Mostrar-Admin-Solicitudes/solicitudes.admin.component';
import { ClientesProyectosAprobados } from './MostrarSolicitudesAprobadas/proyectos.aprobados.component';
@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
   // CounterComponent,
   // FetchDataComponent,
    DashboardAComponent,
    TablasClientes,
    TablasEmpleados,
    TablasEquipos,
    TablasProyectos,
    RegistroClientesComponent,
    RegistroEmpleadosComponent,
    RegistroEquiposComponent,
    DashboardCComponent,
    RegistroProyectosComponent,
    TablasSolicitudesProyectos,
    SolicitudesProyectos,
    ClientesProyectosAprobados
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
     // { path: 'counter', component: CounterComponent },
      { path: 'TablaClientes', component: TablasClientes },
      { path: 'DashboardAdmin', component: DashboardAComponent },
      { path: 'TablasEmpleados', component: TablasEmpleados },
      { path: 'TablasEquipos', component: TablasEquipos },
      { path: 'TablasProyectos', component: TablasProyectos },
      { path: 'RegistroClientes', component: RegistroClientesComponent },
      { path: 'RegistroEmpleados', component: RegistroEmpleadosComponent },
      { path: 'RegistroEquipos', component: RegistroEquiposComponent },
      { path: 'DashboardCClientes', component: DashboardCComponent },
      { path: 'RegistroProyectos', component: RegistroProyectosComponent },
      { path: 'TablasSolicitudesProyectos', component: TablasSolicitudesProyectos },
      { path: 'SolicitudesProyectos', component: SolicitudesProyectos },
      { path: 'ProyectosAprobados', component: ClientesProyectosAprobados }
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
