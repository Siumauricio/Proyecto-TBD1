using System;
using System.Collections.Generic;

namespace Gestor_Proyectos_AC.Models
{
    public partial class ActividadesXHistoria
    {
        public int HistoriasUsuariosIdHistoria { get; set; }
        public int ActividadesIdActividad { get; set; }

        public virtual Actividades ActividadesIdActividadNavigation { get; set; }
        public virtual HistoriasUsuarios HistoriasUsuariosIdHistoriaNavigation { get; set; }
    }
}
