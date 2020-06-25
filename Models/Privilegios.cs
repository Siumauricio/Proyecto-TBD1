using System;
using System.Collections.Generic;

namespace Gestor_Proyectos_AC.Models
{
    public partial class Privilegios
    {
        public Privilegios()
        {
            RolesXPrivilegios = new HashSet<RolesXPrivilegios>();
        }

        public int IdPrivilegio { get; set; }
        public string Nombre { get; set; }
        public int ModulosIdModulo { get; set; }

        public virtual Modulos ModulosIdModuloNavigation { get; set; }
        public virtual ICollection<RolesXPrivilegios> RolesXPrivilegios { get; set; }
    }
}
