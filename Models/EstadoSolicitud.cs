using System;
using System.Collections.Generic;

namespace Gestor_Proyectos_AC.Models
{
    public partial class EstadoSolicitud
    {
        public string Estado { get; set; }
        public int SolicitudesProyectosIdTicket { get; set; }

        public virtual SolicitudesProyectos SolicitudesProyectosIdTicketNavigation { get; set; }
    }
}
