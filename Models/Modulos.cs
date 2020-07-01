using System;
using System.Collections.Generic;

namespace Gestor_Proyectos_AC.Models
{
    public partial class Modulos
    {
        public Modulos()
        {
            Privilegios = new HashSet<Privilegios>();
        }

        public int IdModulo { get; set; }
        public string NombreModulo { get; set; }

        public virtual ICollection<Privilegios> Privilegios { get; set; }
    }
}
