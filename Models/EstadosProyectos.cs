using System;
using System.Collections.Generic;

namespace Gestor_Proyectos_AC.Models
{
    public partial class EstadosProyectos
    {
        public string Estado { get; set; }
        public int ProyectosIdProyecto { get; set; }

        public virtual Proyectos ProyectosIdProyectoNavigation { get; set; }
    }
}
