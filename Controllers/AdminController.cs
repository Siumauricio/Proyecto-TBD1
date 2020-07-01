using System;
using System.Collections.Generic;
using System.Linq;
using Gestor_Proyectos_AC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Identity;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Gestor_Proyectos_AC.Controllers {
    [Route("api/[controller]")]
    public class AdminController:Controller {
        public Models.dbproyecto1Context db;

        public AdminController(Models.dbproyecto1Context context) {
            db = context;
        }

        [HttpGet("[action]")]
        public IActionResult Clientes() {
            var data = (from a in db.Usuarios
                        join b in db.Clientes on a.IdUsuario equals b.UsuariosIdUsuario
                        select new { a.IdUsuario, b.IdCliente, a.RolesIdRol, a.Username, a.Nombre, a.Telefono, a.Direccion, b.Pais, b.Email }).ToList();
            return Json(data);
        }

        [HttpGet("[action]")]
        public IActionResult Equipos() {
            return Json(db.EquiposTrabajo);
        }

        [HttpGet("[action]")]
        public IActionResult Empleados() {
            var data = (from a in db.Usuarios
                        join b in db.Empleado on a.IdUsuario equals b.UsuariosIdUsuario
                        select new { a.IdUsuario, b.IdEmpleado, a.RolesIdRol, a.Username, a.Nombre, a.Telefono, a.Direccion, b.Salario, b.EquiposTrabajoIdGrupoNavigation.IdGrupo, b.FechaContratacion }).ToList();
            return Json(data);
        }

        [HttpGet("[action]")]
        public IActionResult Proyectos() {
            return Json(db.Proyectos);
        }

        [HttpPost("agregar")]
        public IActionResult RegistroUser([FromBody] Usuarios model) {
            var existe = db.usernameExist(model.Username);
            if (existe >= 1) {
                return BadRequest(new { message = "Username ya existe" });
            }
            model.Username = model.Username.ToLower();
            db.Usuarios.Add(model);
            db.SaveChanges();
            return Ok(model);
        }

        [HttpPost("agregarC")]
        public IActionResult RegistroCliente([FromBody] Clientes model) {
            model.IdCliente = "C-" + db.GetMySequenceClientes();
            model.UsuariosIdUsuario = db.sequenceUsuarios();
            db.Clientes.Add(model);
            db.SaveChanges();
            return Ok(model);
        }


        [HttpPost("agregarG")]
        public IActionResult registroEquipo([FromBody] EquiposTrabajo model) {
            db.EquiposTrabajo.Add(model);
            db.SaveChanges();
            return Ok(model);
        }

        [HttpPost("agregarE")]
        public IActionResult RegistroEmpleado([FromBody] Empleado model) {
            var Historial = new HistorialPuestos() {
                FechaInicio = DateTime.Now,
            };
            db.HistorialPuestos.Add(Historial);
            db.SaveChanges();

            model.IdEmpleado = "E-" + db.GetMySequenceEmpleados();//
            model.FechaContratacion = DateTime.Now;
            model.UsuariosIdUsuario = db.sequenceUsuarios();
            model.HistorialPuestosIdHistorial = db.getID_historial();
            db.Empleado.Add(model);
            db.SaveChanges();

            var Puestos_x_Historial = new PuestosXHistorial() {
                PuestosTrabajoIdPuesto = model.PuestosTrabajoIdPuesto,
                HistorialPuestosIdHistorial = model.HistorialPuestosIdHistorial
            };
            db.PuestosXHistorial.Add(Puestos_x_Historial);
            db.SaveChanges();

            return Ok(model);
        }

        [HttpPost("agregarProyectos")]
        public IActionResult registroProyectos([FromBody] SolicitudesProyectos model) {
            db.Add(model);
            db.SaveChanges();
            return Ok(model);
        }


        [HttpPost("agregarP_C")]

        public IActionResult registroP_C([FromBody] ClientesXSolicitud model) {
            var s = db.Clientes.Where(p => p.UsuariosIdUsuario == model.ClientesIdUsuario)
                      .Select(per => new { per.IdCliente });
            if (s == null) {
                return BadRequest(new { message = "Error" });
            }
            foreach (var element in s) {
                model.ClientesIdCliente = element.IdCliente;
            }
            model.SolicitudesProyectosIdTicket = db.sequenceTicket();
            db.ClientesXSolicitud.Add(model);
            db.SaveChanges();
            return Ok(model);
        }


        [HttpPost("mostrarSolicitudes")]
        public IActionResult mostrarSolicitudesProyectos([FromBody] Clientes model) {
            var w = db.obtenerSolicitudes(model.UsuariosIdUsuario);
            return Ok(w);
        }

        [HttpGet("mostrarSolicitudesE")]
        public IActionResult mostrarSolicitudesProyectos() {
            var w = db.obtenerSolicitudesEspera();
            return Ok(w);
        }

        [HttpPut("actualizarSolicitud/{id}")]
        public IActionResult actualizarProyecto(int id) {
            var result = db.ClientesXSolicitud.SingleOrDefault(g => g.SolicitudesProyectosIdTicket == id);
            if (result != null) {
                result.EstadoSolicitud = "APROBADO";
                db.SaveChanges();
            }
            return Ok(id);
        }

        [HttpPut("actualizarSolicitudD/{id}")]
        public IActionResult actualizarProyectod(int id) {
            var result = db.ClientesXSolicitud.SingleOrDefault(g => g.SolicitudesProyectosIdTicket == id);
            if (result == null) {
                return BadRequest(new { message = "No Existe este proyecto" });
            }
            result.EstadoSolicitud = "DENEGADO";
            db.SaveChanges();
            return Ok(id);
        }

        [HttpPut("proyectosAprobados/{id}")]
        public IActionResult obtenerProyectosAprobados(int id) {
            var result = db.obtenerProyectosAprobados(id);
            return Ok(result);
        }
        [HttpPut("reactivarSolicitud/{id}/{id2}")]
        public IActionResult reactivarSolicitudProyecto(int id, int id2) {
            var result = db.ClientesXSolicitud.SingleOrDefault(a => a.SolicitudesProyectosIdTicket == id && a.ClientesIdUsuario == id2);
            if (result == null) {
                return BadRequest(new { message = "No Existe este proyecto" });
            }
            result.EstadoSolicitud = "EN ESPERA";
            db.SaveChanges();

            return Ok(result);
        }

        [HttpPut("AsignarProyectos/{id}/{id2}")]
        public IActionResult asignarProyecto(int id, int id2) {
            var proyecto = new Proyectos() {
                EquiposTrabajoIdGrupo = id,
                SolicitudesProyectosIdTicket = id2,
                FechaAsignacion = DateTime.Now,
                FechaCreacion = DateTime.Now,
                Github = "NOMBRE LOCAL",
                Trello = "NOMBRE LOCAL"
            };
            db.Proyectos.Add(proyecto);
            db.SaveChanges();
            return Ok(id);
        }

        [HttpPost("asignarHabilidades/{id}")]
        public void asignarHabilidades(string id, [FromBody] Habilidades model) {
            var result = db.Empleado.SingleOrDefault(a => a.IdEmpleado == id);
            if (result == null) {
                return;
            }
            db.Habilidades.Add(model);
            db.SaveChanges();
            var Hab_x_Emp = new EmpleadosXHabilidades() {
                EmpleadoIdEmpleado = id,
                EmpleadoIdUsuario = result.UsuariosIdUsuario,
                HabilidadesIdHabilidad = model.IdHabilidad
            };
            db.EmpleadosXHabilidades.Add(Hab_x_Emp);
            db.SaveChanges();
        }

        [HttpPost("crearHistoria/{id}")]
        public void crearHistoriasUsuario(int id, [FromBody] HistoriasUsuarios model) {
            var result = db.Proyectos.SingleOrDefault(a => a.SolicitudesProyectosIdTicket == id);
            var result_ = db.ClientesXSolicitud.SingleOrDefault(a => a.SolicitudesProyectosIdTicket == id);

            if (result == null || result_ == null) {
                return;
            }
            model.ClientesIdCliente = result_.ClientesIdCliente;
            model.ClientesIdUsuario = result_.ClientesIdUsuario;
            model.ProyectosIdProyecto = result.IdProyecto;
            db.HistoriasUsuarios.Add(model);
            db.SaveChanges();
        }

        [HttpPost("Jp/{id}")]
        public IActionResult JP(int id) {
            var data = db.Empleado.Where(a => a.UsuariosIdUsuario == id && a.PuestosTrabajoIdPuesto == 2);
            if (data.Count() == 0) {
                return BadRequest(new { message = "Username or password is incorrect" });
            }
            return Ok(data);
        }

        [HttpGet("integrantes/{id}")]
        public IActionResult integrantesGrupo(int id) {
            var data = db.Empleado.SingleOrDefault(a => a.UsuariosIdUsuario == id);
            var result = db.Empleado.Where(a => a.EquiposTrabajoIdGrupo == data.EquiposTrabajoIdGrupo);
            var data_ = (from a in db.Usuarios
                         join b in result on a.IdUsuario equals b.UsuariosIdUsuario
                         select new { a.IdUsuario, b.IdEmpleado, a.Nombre, a.Username, b.PuestosTrabajoIdPuesto }).ToList();
            if (result == null) {
                return BadRequest(new { message = "No hay integrantes en el grupo" });
            }
            return Json(data_);
        }

        [HttpGet("obtenerHistoriasUsuario/{id}")]
        public IActionResult obtenerHistoriasUsuario(int id) {
            var data2 = db.Empleado.SingleOrDefault(a => a.UsuariosIdUsuario == id);
           
            var data = db.obtenerHistoriasUsuario(data2.EquiposTrabajoIdGrupo);
            if (data == null) {
                return BadRequest(new { message = "No hay Historias de usuario" });
            }

            return Ok(data);
        }


        [HttpPost("registrarActividad/{id}/{id2}")]
        public void asignarActividad(string id, int id2, [FromBody] Actividades model) {
            var data2 = db.Empleado.SingleOrDefault(a => a.IdEmpleado == id);
            if (data2 == null) {
                return;
            }
            var actividad = new Actividades() {
                FechaEntrega = model.FechaEntrega,
                Detalles=model.Detalles,
                TiempoEstimado = model.TiempoEstimado,
                EmpleadoIdEmpleado = id,
                EmpleadoIdUsuario = data2.UsuariosIdUsuario,
                EstadoSolicitud = "EN ESPERA"
            };
            var result = db.Actividades.Add(actividad);
            db.SaveChanges();


            var Actividadesx = new ActividadesXHistoria() {
                ActividadesIdActividad = actividad.IdActividad,
                HistoriasUsuariosIdHistoria = id2
            };
            db.ActividadesXHistoria.Add(Actividadesx);
            db.SaveChanges();
        }

        [HttpGet("obtenerActividades/{id}")]
        public IActionResult obtenerActividades(int id) {
            var data = db.Actividades.Where(a => (a.EmpleadoIdUsuario == id) && (a.EstadoSolicitud == "EN ESPERA" ||a.EstadoSolicitud == "PROCESANDO" || a.EstadoSolicitud == "DENEGADO"));
            return Ok(data);
        }

        [HttpPut("actualizarActividad/{id}")]
        public IActionResult actualizarActividad(int id) {
            var data = db.Actividades.SingleOrDefault(a => a.IdActividad == id );
            data.EstadoSolicitud = "PROCESANDO";
            db.SaveChanges();
            return Ok(data);
        }
        [HttpGet("obtenerActividadesJP/{id}")]
        public IActionResult obtenerActividadesJP(int id) {
            var data = db.Empleado.SingleOrDefault(a => a.UsuariosIdUsuario == id);
            var result = db.obtenerActividadesJP(data.EquiposTrabajoIdGrupo);
            return Ok(result);
        }


        [HttpPut("actualizarActividadJp/{id}")]
        public IActionResult actualizarActividadJp(int id) {
            var data = db.Actividades.SingleOrDefault(a => a.IdActividad == id);
            data.EstadoSolicitud = "APROBADO";
            db.SaveChanges();
            return Ok(data);
        }



        [HttpPut("actualizarActividadDJp/{id}")]
        public IActionResult actualizarActividadDJp(int id) {
            var data = db.Actividades.SingleOrDefault(a => a.IdActividad == id);
            data.EstadoSolicitud = "DENEGADO";
            db.SaveChanges();
            return Ok(data);
        }

        [HttpPost("login")]
        public IActionResult iniciar([FromBody] Usuarios model) {
            var data = db.Usuarios.Where(a => a.Username.ToLower() == model.Username.ToLower() && a.Contrasenia == model.Contrasenia);
            if (data.Count() == 0) {
                return BadRequest(new { message = "Username or password is incorrect" });
            }
            return Ok(data);
        }

    }
}