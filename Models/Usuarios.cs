using System;
using System.Collections.Generic;

namespace Gestor_Proyectos_AC.Models
{
    public partial class Usuarios
    {
        public int IdUsuario { get; set; }
        public string Username { get; set; }
        public string Contrasenia { get; set; }
        public string Nombre { get; set; }
        public decimal Telefono { get; set; }
        public string Direccion { get; set; }
        public int RolesIdRol { get; set; }

        public virtual Roles RolesIdRolNavigation { get; set; }
        public virtual Clientes Clientes { get; set; }
        public virtual Empleado Empleado { get; set; }
    }
}
