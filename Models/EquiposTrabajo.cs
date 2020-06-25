using System;
using System.Collections.Generic;

namespace Gestor_Proyectos_AC.Models
{
    public partial class EquiposTrabajo
    {
        public EquiposTrabajo()
        {
            Empleado = new HashSet<Empleado>();
            Proyectos = new HashSet<Proyectos>();
        }

        public int IdGrupo { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Empleado> Empleado { get; set; }
        public virtual ICollection<Proyectos> Proyectos { get; set; }
    }
}
