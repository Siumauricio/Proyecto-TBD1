using System;
using System.Collections.Generic;

namespace Gestor_Proyectos_AC.Models
{
    public partial class PuestosTrabajo
    {
        public PuestosTrabajo()
        {
            Empleado = new HashSet<Empleado>();
            PuestosXHistorial = new HashSet<PuestosXHistorial>();
        }

        public int IdPuesto { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Empleado> Empleado { get; set; }
        public virtual ICollection<PuestosXHistorial> PuestosXHistorial { get; set; }
    }
}
