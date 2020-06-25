using System;
using System.Collections.Generic;

namespace Gestor_Proyectos_AC.Models
{
    public partial class Roles
    {
        public Roles()
        {
            RolesXPrivilegios = new HashSet<RolesXPrivilegios>();
            Usuarios = new HashSet<Usuarios>();
        }

        public int IdRol { get; set; }
        public string NombreRol { get; set; }

        public virtual ICollection<RolesXPrivilegios> RolesXPrivilegios { get; set; }
        public virtual ICollection<Usuarios> Usuarios { get; set; }
    }
}
