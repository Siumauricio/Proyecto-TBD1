using System;
using System.Collections.Generic;

namespace Gestor_Proyectos_AC.Models
{
    public partial class HistoriasUsuarios
    {
        public HistoriasUsuarios()
        {
            ActividadesXHistoria = new HashSet<ActividadesXHistoria>();
        }

        public int IdHistoria { get; set; }
        public string Prioridad { get; set; }
        public string Funcionalidades { get; set; }
        public int ProyectosIdProyecto { get; set; }
        public string ClientesIdCliente { get; set; }
        public int ClientesIdUsuario { get; set; }

        public virtual Clientes Clientes { get; set; }
        public virtual Proyectos ProyectosIdProyectoNavigation { get; set; }
        public virtual ICollection<ActividadesXHistoria> ActividadesXHistoria { get; set; }
    }
}
