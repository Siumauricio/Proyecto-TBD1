using System;
using System.Collections.Generic;

namespace Gestor_Proyectos_AC.Models
{
    public partial class SolicitudesProyectos
    {
        public SolicitudesProyectos()
        {
            ClientesXSolicitud = new HashSet<ClientesXSolicitud>();
        }

        public int IdTicket { get; set; }
        public decimal Presupuesto { get; set; }
        public string Tecnologias { get; set; }
        public string Nombre { get; set; }

        public virtual EstadoSolicitud EstadoSolicitud { get; set; }
        public virtual Proyectos Proyectos { get; set; }
        public virtual ICollection<ClientesXSolicitud> ClientesXSolicitud { get; set; }
    }
}
