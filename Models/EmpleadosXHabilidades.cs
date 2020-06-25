using System;
using System.Collections.Generic;

namespace Gestor_Proyectos_AC.Models
{
    public partial class EmpleadosXHabilidades
    {
        public string EmpleadoIdEmpleado { get; set; }
        public int EmpleadoIdUsuario { get; set; }
        public int HabilidadesIdHabilidad { get; set; }

        public virtual Empleado Empleado { get; set; }
        public virtual Habilidades HabilidadesIdHabilidadNavigation { get; set; }
    }
}
