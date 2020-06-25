using System;
using System.Collections.Generic;

namespace Gestor_Proyectos_AC.Models
{
    public partial class Actividades
    {
        public Actividades()
        {
            ActividadesXHistoria = new HashSet<ActividadesXHistoria>();
        }

        public int IdActividad { get; set; }
        public DateTime FechaEntrega { get; set; }
        public string Estado { get; set; }
        public decimal TiempoEstimado { get; set; }
        public string EmpleadoIdEmpleado { get; set; }
        public int EmpleadoIdUsuario { get; set; }
        public string EstadoSolicitud { get; set; }

        public virtual Empleado Empleado { get; set; }
        public virtual DetallesActividad DetallesActividad { get; set; }
        public virtual ICollection<ActividadesXHistoria> ActividadesXHistoria { get; set; }
    }
}
