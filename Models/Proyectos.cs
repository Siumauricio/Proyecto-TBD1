using System;
using System.Collections.Generic;

namespace Gestor_Proyectos_AC.Models
{
    public partial class Proyectos
    {
        public Proyectos()
        {
            EstadosProyectos = new HashSet<EstadosProyectos>();
            HistoriasUsuarios = new HashSet<HistoriasUsuarios>();
        }

        public int IdProyecto { get; set; }
        public string Github { get; set; }
        public string Trello { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaAsignacion { get; set; }
        public int EquiposTrabajoIdGrupo { get; set; }
        public int SolicitudesProyectosIdTicket { get; set; }

        public virtual EquiposTrabajo EquiposTrabajoIdGrupoNavigation { get; set; }
        public virtual SolicitudesProyectos SolicitudesProyectosIdTicketNavigation { get; set; }
        public virtual ICollection<EstadosProyectos> EstadosProyectos { get; set; }
        public virtual ICollection<HistoriasUsuarios> HistoriasUsuarios { get; set; }
    }
}
