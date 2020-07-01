using System;
using System.Collections.Generic;

namespace Gestor_Proyectos_AC.Models
{
    public partial class HistorialPuestos
    {
        public HistorialPuestos()
        {
            Empleado = new HashSet<Empleado>();
            PuestosXHistorial = new HashSet<PuestosXHistorial>();
        }

        public int IdHistorial { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFinal { get; set; }

        public virtual ICollection<Empleado> Empleado { get; set; }
        public virtual ICollection<PuestosXHistorial> PuestosXHistorial { get; set; }
    }
}
