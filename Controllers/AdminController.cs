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
            var w = db.obtenerSolicitudes(model.UsuariosIdUsuario) ;
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
            if (result!=null) {
                result.EstadoSolicitud = "APROBADO";
                db.SaveChanges();
            }
            return Ok(id);
        }

        [HttpPut("actualizarSolicitudD/{id}")]
        public IActionResult actualizarProyectod(int id) {
            var result = db.ClientesXSolicitud.SingleOrDefault(g => g.SolicitudesProyectosIdTicket == id);
            if (result != null) {
                result.EstadoSolicitud = "DENEGADO";
                db.SaveChanges();
            }
            return Ok(id);
        }

        [HttpPut("proyectosAprobados/{id}")]
        public IActionResult obtenerProyectosAprobados(int id) {
            var result = db.obtenerProyectosAprobados(id);
            return Ok(result);
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