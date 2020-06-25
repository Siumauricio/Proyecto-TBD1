using System;
using System.Collections.Generic;

namespace Gestor_Proyectos_AC.Models
{
    public partial class Habilidades
    {
        public Habilidades()
        {
            EmpleadosXHabilidades = new HashSet<EmpleadosXHabilidades>();
        }

        public int IdHabilidad { get; set; }
        public string NombreHabilidad { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<EmpleadosXHabilidades> EmpleadosXHabilidades { get; set; }
    }
}
