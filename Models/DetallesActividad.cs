using System;
using System.Collections.Generic;

namespace Gestor_Proyectos_AC.Models
{
    public partial class DetallesActividad
    {
        public string Detalles { get; set; }
        public int ActividadesIdActividad { get; set; }

        public virtual Actividades ActividadesIdActividadNavigation { get; set; }
    }
}
