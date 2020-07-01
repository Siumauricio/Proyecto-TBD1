using System;
using System.Collections.Generic;

namespace Gestor_Proyectos_AC.Models
{
    public partial class ClientesXSolicitud
    {
        public string ClientesIdCliente { get; set; }
        public int ClientesIdUsuario { get; set; }
        public int SolicitudesProyectosIdTicket { get; set; }
        public string EstadoSolicitud { get; set; }

        public virtual Clientes Clientes { get; set; }
        public virtual SolicitudesProyectos SolicitudesProyectosIdTicketNavigation { get; set; }
    }
}
