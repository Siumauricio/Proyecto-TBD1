using System;
using System.Collections.Generic;

namespace Gestor_Proyectos_AC.Models
{
    public partial class Empleado
    {
        public Empleado()
        {
            Actividades = new HashSet<Actividades>();
            EmpleadosXHabilidades = new HashSet<EmpleadosXHabilidades>();
        }

        public string IdEmpleado { get; set; }
        public decimal Salario { get; set; }
        public string Activo { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public DateTime? FechaContratacion { get; set; }
        public int UsuariosIdUsuario { get; set; }
        public int PuestosTrabajoIdPuesto { get; set; }
        public int HistorialPuestosIdHistorial { get; set; }
        public int EquiposTrabajoIdGrupo { get; set; }

        public virtual EquiposTrabajo EquiposTrabajoIdGrupoNavigation { get; set; }
        public virtual HistorialPuestos HistorialPuestosIdHistorialNavigation { get; set; }
        public virtual PuestosTrabajo PuestosTrabajoIdPuestoNavigation { get; set; }
        public virtual Usuarios UsuariosIdUsuarioNavigation { get; set; }
        public virtual ICollection<Actividades> Actividades { get; set; }
        public virtual ICollection<EmpleadosXHabilidades> EmpleadosXHabilidades { get; set; }
    }
}
