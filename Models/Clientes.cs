using System;
using System.Collections.Generic;

namespace Gestor_Proyectos_AC.Models
{
    public partial class Clientes
    {
        public Clientes()
        {
            ClientesXSolicitud = new HashSet<ClientesXSolicitud>();
            HistoriasUsuarios = new HashSet<HistoriasUsuarios>();
        }

        public string IdCliente { get; set; }
        public string Email { get; set; }
        public string Pais { get; set; }
        public int UsuariosIdUsuario { get; set; }

        public virtual Usuarios UsuariosIdUsuarioNavigation { get; set; }
        public virtual ICollection<ClientesXSolicitud> ClientesXSolicitud { get; set; }
        public virtual ICollection<HistoriasUsuarios> HistoriasUsuarios { get; set; }
    }
}
