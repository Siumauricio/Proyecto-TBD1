using System;
using System.Collections.Generic;

namespace Gestor_Proyectos_AC.Models
{
    public partial class RolesXPrivilegios
    {
        public int PrivilegiosIdPrivilegio { get; set; }
        public int RolesIdRol { get; set; }

        public virtual Privilegios PrivilegiosIdPrivilegioNavigation { get; set; }
        public virtual Roles RolesIdRolNavigation { get; set; }
    }
}
