using System;
using System.Collections.Generic;

namespace Gestor_Proyectos_AC.Models
{
    public partial class PuestosXHistorial
    {
        public int PuestosTrabajoIdPuesto { get; set; }
        public int HistorialPuestosIdHistorial { get; set; }

        public virtual HistorialPuestos HistorialPuestosIdHistorialNavigation { get; set; }
        public virtual PuestosTrabajo PuestosTrabajoIdPuestoNavigation { get; set; }
    }
}
