using System;
using System.Data;
using System.Text;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json;

namespace Gestor_Proyectos_AC.Models
{
    public partial class dbproyecto1Context : DbContext
    {
        public dbproyecto1Context()
        {
        }

        public dbproyecto1Context(DbContextOptions<dbproyecto1Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Actividades> Actividades { get; set; }
        public virtual DbSet<ActividadesXHistoria> ActividadesXHistoria { get; set; }
        public virtual DbSet<Clientes> Clientes { get; set; }
        public virtual DbSet<ClientesXSolicitud> ClientesXSolicitud { get; set; }
        public virtual DbSet<DetallesActividad> DetallesActividad { get; set; }
        public virtual DbSet<Empleado> Empleado { get; set; }
        public virtual DbSet<EmpleadosXHabilidades> EmpleadosXHabilidades { get; set; }
        public virtual DbSet<EquiposTrabajo> EquiposTrabajo { get; set; }
        public virtual DbSet<EstadoSolicitud> EstadoSolicitud { get; set; }
        public virtual DbSet<EstadosProyectos> EstadosProyectos { get; set; }
        public virtual DbSet<Habilidades> Habilidades { get; set; }
        public virtual DbSet<HistorialPuestos> HistorialPuestos { get; set; }
        public virtual DbSet<HistoriasUsuarios> HistoriasUsuarios { get; set; }
        public virtual DbSet<Modulos> Modulos { get; set; }
        public virtual DbSet<Privilegios> Privilegios { get; set; }
        public virtual DbSet<Proyectos> Proyectos { get; set; }
        public virtual DbSet<PuestosTrabajo> PuestosTrabajo { get; set; }
        public virtual DbSet<PuestosXHistorial> PuestosXHistorial { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<RolesXPrivilegios> RolesXPrivilegios { get; set; }
        public virtual DbSet<SolicitudesProyectos> SolicitudesProyectos { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=LAPTOP-44FRKSCR\\SQLEXPRESS;Initial Catalog=db-proyecto1;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actividades>(entity =>
            {
                entity.HasKey(e => e.IdActividad)
                    .HasName("Actividades_PK");

                entity.Property(e => e.IdActividad).HasColumnName("id_actividad");

                entity.Property(e => e.Detalles)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EmpleadoIdEmpleado)
                    .IsRequired()
                    .HasColumnName("Empleado_id_empleado")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.EmpleadoIdUsuario).HasColumnName("Empleado_id_usuario");

                entity.Property(e => e.EstadoSolicitud)
                    .IsRequired()
                    .HasColumnName("estado_solicitud")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.FechaEntrega)
                    .HasColumnName("fecha_entrega")
                    .HasColumnType("datetime");

                entity.Property(e => e.TiempoEstimado)
                    .HasColumnName("tiempo_estimado")
                    .HasColumnType("numeric(4, 0)");

                entity.HasOne(d => d.Empleado)
                    .WithMany(p => p.Actividades)
                    .HasForeignKey(d => new { d.EmpleadoIdEmpleado, d.EmpleadoIdUsuario })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Actividades_Empleado_FK");
            });

            modelBuilder.Entity<ActividadesXHistoria>(entity =>
            {
                entity.HasKey(e => new { e.HistoriasUsuariosIdHistoria, e.ActividadesIdActividad })
                    .HasName("Actividades_x_Historia_PK");

                entity.ToTable("Actividades_x_Historia");

                entity.Property(e => e.HistoriasUsuariosIdHistoria).HasColumnName("Historias_Usuarios_id_historia");

                entity.Property(e => e.ActividadesIdActividad).HasColumnName("Actividades_id_actividad");

                entity.HasOne(d => d.ActividadesIdActividadNavigation)
                    .WithMany(p => p.ActividadesXHistoria)
                    .HasForeignKey(d => d.ActividadesIdActividad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Actividades_x_Historia_Actividades_FK");

                entity.HasOne(d => d.HistoriasUsuariosIdHistoriaNavigation)
                    .WithMany(p => p.ActividadesXHistoria)
                    .HasForeignKey(d => d.HistoriasUsuariosIdHistoria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Actividades_x_Historia_Historias_Usuarios_FK");
            });

            modelBuilder.Entity<Clientes>(entity =>
            {
                entity.HasKey(e => new { e.IdCliente, e.UsuariosIdUsuario })
                    .HasName("Clientes_PK");

                entity.HasIndex(e => e.UsuariosIdUsuario)
                    .HasName("Clientes__IDX")
                    .IsUnique();

                entity.Property(e => e.IdCliente)
                    .HasColumnName("id_cliente")
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.UsuariosIdUsuario).HasColumnName("Usuarios_id_usuario");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Pais)
                    .IsRequired()
                    .HasColumnName("pais")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.HasOne(d => d.UsuariosIdUsuarioNavigation)
                    .WithOne(p => p.Clientes)
                    .HasForeignKey<Clientes>(d => d.UsuariosIdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Clientes_Usuarios_FK");
            });

            modelBuilder.Entity<ClientesXSolicitud>(entity =>
            {
                entity.HasKey(e => new { e.ClientesIdCliente, e.ClientesIdUsuario, e.SolicitudesProyectosIdTicket })
                    .HasName("Clientes_x_Solicitud_PK");

                entity.ToTable("Clientes_x_Solicitud");

                entity.Property(e => e.ClientesIdCliente)
                    .HasColumnName("Clientes_id_cliente")
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.ClientesIdUsuario).HasColumnName("Clientes_id_usuario");

                entity.Property(e => e.SolicitudesProyectosIdTicket).HasColumnName("Solicitudes_Proyectos_id_ticket");

                entity.Property(e => e.EstadoSolicitud)
                    .IsRequired()
                    .HasColumnName("estado_solicitud")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.HasOne(d => d.SolicitudesProyectosIdTicketNavigation)
                    .WithMany(p => p.ClientesXSolicitud)
                    .HasForeignKey(d => d.SolicitudesProyectosIdTicket)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Clientes_x_Solicitud_Solicitudes_Proyectos_FK");

                entity.HasOne(d => d.Clientes)
                    .WithMany(p => p.ClientesXSolicitud)
                    .HasForeignKey(d => new { d.ClientesIdCliente, d.ClientesIdUsuario })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Clientes_x_Solicitud_Clientes_FK");
            });

            modelBuilder.Entity<DetallesActividad>(entity =>
            {
                entity.HasKey(e => e.ActividadesIdActividad)
                    .HasName("Detalles_Actividad_PK");

                entity.ToTable("Detalles_Actividad");

                entity.Property(e => e.ActividadesIdActividad)
                    .HasColumnName("Actividades_id_actividad")
                    .ValueGeneratedNever();

                entity.Property(e => e.Detalles)
                    .IsRequired()
                    .HasColumnName("detalles")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.HasOne(d => d.ActividadesIdActividadNavigation)
                    .WithOne(p => p.DetallesActividad)
                    .HasForeignKey<DetallesActividad>(d => d.ActividadesIdActividad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Detalles_Actividad_Actividades_FK");
            });

            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.HasKey(e => new { e.IdEmpleado, e.UsuariosIdUsuario })
                    .HasName("Empleado_PK");

                entity.HasIndex(e => e.UsuariosIdUsuario)
                    .HasName("Empleado__IDX")
                    .IsUnique();

                entity.Property(e => e.IdEmpleado)
                    .HasColumnName("id_empleado")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.UsuariosIdUsuario).HasColumnName("Usuarios_id_usuario");

                entity.Property(e => e.Activo)
                    .IsRequired()
                    .HasColumnName("activo")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.EquiposTrabajoIdGrupo).HasColumnName("Equipos_Trabajo_id_grupo");

                entity.Property(e => e.FechaContratacion)
                    .HasColumnName("fecha_contratacion")
                    .HasColumnType("datetime");

                entity.Property(e => e.FechaNacimiento)
                    .HasColumnName("fecha_nacimiento")
                    .HasColumnType("datetime");

                entity.Property(e => e.HistorialPuestosIdHistorial).HasColumnName("Historial_Puestos_id_historial");

                entity.Property(e => e.PuestosTrabajoIdPuesto).HasColumnName("Puestos_Trabajo_id_puesto");

                entity.Property(e => e.Salario)
                    .HasColumnName("salario")
                    .HasColumnType("numeric(15, 2)");

                entity.HasOne(d => d.EquiposTrabajoIdGrupoNavigation)
                    .WithMany(p => p.Empleado)
                    .HasForeignKey(d => d.EquiposTrabajoIdGrupo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Empleado_Equipos_Trabajo_FK");

                entity.HasOne(d => d.HistorialPuestosIdHistorialNavigation)
                    .WithMany(p => p.Empleado)
                    .HasForeignKey(d => d.HistorialPuestosIdHistorial)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Empleado_Historial_Puestos_FK");

                entity.HasOne(d => d.PuestosTrabajoIdPuestoNavigation)
                    .WithMany(p => p.Empleado)
                    .HasForeignKey(d => d.PuestosTrabajoIdPuesto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Empleado_Puestos_Trabajo_FK");

                entity.HasOne(d => d.UsuariosIdUsuarioNavigation)
                    .WithOne(p => p.Empleado)
                    .HasForeignKey<Empleado>(d => d.UsuariosIdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Empleado_Usuarios_FK");
            });

            modelBuilder.Entity<EmpleadosXHabilidades>(entity =>
            {
                entity.HasKey(e => new { e.EmpleadoIdEmpleado, e.EmpleadoIdUsuario, e.HabilidadesIdHabilidad })
                    .HasName("Empleados_x_Habilidades_PK");

                entity.ToTable("Empleados_x_Habilidades");

                entity.Property(e => e.EmpleadoIdEmpleado)
                    .HasColumnName("Empleado_id_empleado")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.EmpleadoIdUsuario).HasColumnName("Empleado_id_usuario");

                entity.Property(e => e.HabilidadesIdHabilidad).HasColumnName("Habilidades_id_habilidad");

                entity.HasOne(d => d.HabilidadesIdHabilidadNavigation)
                    .WithMany(p => p.EmpleadosXHabilidades)
                    .HasForeignKey(d => d.HabilidadesIdHabilidad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Empleados_x_Habilidades_Habilidades_FK");

                entity.HasOne(d => d.Empleado)
                    .WithMany(p => p.EmpleadosXHabilidades)
                    .HasForeignKey(d => new { d.EmpleadoIdEmpleado, d.EmpleadoIdUsuario })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Empleados_x_Habilidades_Empleado_FK");
            });

            modelBuilder.Entity<EquiposTrabajo>(entity =>
            {
                entity.HasKey(e => e.IdGrupo)
                    .HasName("Equipos_Trabajo_PK");

                entity.ToTable("Equipos_Trabajo");

                entity.Property(e => e.IdGrupo).HasColumnName("id_grupo");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre")
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EstadoSolicitud>(entity =>
            {
                entity.HasKey(e => e.SolicitudesProyectosIdTicket)
                    .HasName("Estado_Solicitud_PK");

                entity.ToTable("Estado_Solicitud");

                entity.Property(e => e.SolicitudesProyectosIdTicket)
                    .HasColumnName("Solicitudes_Proyectos_id_ticket")
                    .ValueGeneratedNever();

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasColumnName("estado")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.HasOne(d => d.SolicitudesProyectosIdTicketNavigation)
                    .WithOne(p => p.EstadoSolicitud)
                    .HasForeignKey<EstadoSolicitud>(d => d.SolicitudesProyectosIdTicket)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Estado_Solicitud_Solicitudes_Proyectos_FK");
            });

            modelBuilder.Entity<EstadosProyectos>(entity =>
            {
                entity.HasKey(e => new { e.Estado, e.ProyectosIdProyecto })
                    .HasName("estados_proyectos_PK");

                entity.ToTable("estados_proyectos");

                entity.Property(e => e.Estado)
                    .HasColumnName("estado")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.ProyectosIdProyecto).HasColumnName("Proyectos_id_proyecto");

                entity.HasOne(d => d.ProyectosIdProyectoNavigation)
                    .WithMany(p => p.EstadosProyectos)
                    .HasForeignKey(d => d.ProyectosIdProyecto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("estados_proyectos_Proyectos_FK");
            });

            modelBuilder.Entity<Habilidades>(entity =>
            {
                entity.HasKey(e => e.IdHabilidad)
                    .HasName("Habilidades_PK");

                entity.Property(e => e.IdHabilidad).HasColumnName("id_habilidad");

                entity.Property(e => e.Descripcion)
                    .HasColumnName("descripcion")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.NombreHabilidad)
                    .IsRequired()
                    .HasColumnName("nombre_habilidad")
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<HistorialPuestos>(entity =>
            {
                entity.HasKey(e => e.IdHistorial)
                    .HasName("Historial_Puestos_PK");

                entity.ToTable("Historial_Puestos");

                entity.Property(e => e.IdHistorial).HasColumnName("id_historial");

                entity.Property(e => e.FechaFinal)
                    .HasColumnName("fecha_final")
                    .HasColumnType("datetime");

                entity.Property(e => e.FechaInicio)
                    .HasColumnName("fecha_inicio")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<HistoriasUsuarios>(entity =>
            {
                entity.HasKey(e => e.IdHistoria)
                    .HasName("Historias_Usuarios_PK");

                entity.ToTable("Historias_Usuarios");

                entity.Property(e => e.IdHistoria).HasColumnName("id_historia");

                entity.Property(e => e.ClientesIdCliente)
                    .IsRequired()
                    .HasColumnName("Clientes_id_cliente")
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.ClientesIdUsuario).HasColumnName("Clientes_id_usuario");

                entity.Property(e => e.Funcionalidades)
                    .IsRequired()
                    .HasColumnName("funcionalidades")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Prioridad)
                    .HasColumnName("prioridad")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.ProyectosIdProyecto).HasColumnName("Proyectos_id_proyecto");

                entity.HasOne(d => d.ProyectosIdProyectoNavigation)
                    .WithMany(p => p.HistoriasUsuarios)
                    .HasForeignKey(d => d.ProyectosIdProyecto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Historias_Usuarios_Proyectos_FK");

                entity.HasOne(d => d.Clientes)
                    .WithMany(p => p.HistoriasUsuarios)
                    .HasForeignKey(d => new { d.ClientesIdCliente, d.ClientesIdUsuario })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Historias_Usuarios_Clientes_FK");
            });

            modelBuilder.Entity<Modulos>(entity =>
            {
                entity.HasKey(e => e.IdModulo)
                    .HasName("Modulos_PK");

                entity.Property(e => e.IdModulo).HasColumnName("id_modulo");

                entity.Property(e => e.NombreModulo)
                    .IsRequired()
                    .HasColumnName("nombre_modulo")
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Privilegios>(entity =>
            {
                entity.HasKey(e => e.IdPrivilegio)
                    .HasName("Privilegios_PK");

                entity.Property(e => e.IdPrivilegio).HasColumnName("id_privilegio");

                entity.Property(e => e.ModulosIdModulo).HasColumnName("Modulos_id_modulo");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.HasOne(d => d.ModulosIdModuloNavigation)
                    .WithMany(p => p.Privilegios)
                    .HasForeignKey(d => d.ModulosIdModulo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Privilegios_Modulos_FK");
            });

            modelBuilder.Entity<Proyectos>(entity =>
            {
                entity.HasKey(e => e.IdProyecto)
                    .HasName("Proyectos_PK");

                entity.HasIndex(e => e.SolicitudesProyectosIdTicket)
                    .HasName("Proyectos__IDX")
                    .IsUnique();

                entity.Property(e => e.IdProyecto).HasColumnName("id_proyecto");

                entity.Property(e => e.EquiposTrabajoIdGrupo).HasColumnName("Equipos_Trabajo_id_grupo");

                entity.Property(e => e.FechaAsignacion)
                    .HasColumnName("fecha_asignacion")
                    .HasColumnType("datetime");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnName("fecha_creacion")
                    .HasColumnType("datetime");

                entity.Property(e => e.Github)
                    .IsRequired()
                    .HasColumnName("github")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.SolicitudesProyectosIdTicket).HasColumnName("Solicitudes_Proyectos_id_ticket");

                entity.Property(e => e.Trello)
                    .IsRequired()
                    .HasColumnName("trello")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.HasOne(d => d.EquiposTrabajoIdGrupoNavigation)
                    .WithMany(p => p.Proyectos)
                    .HasForeignKey(d => d.EquiposTrabajoIdGrupo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Proyectos_Equipos_Trabajo_FK");

                entity.HasOne(d => d.SolicitudesProyectosIdTicketNavigation)
                    .WithOne(p => p.Proyectos)
                    .HasForeignKey<Proyectos>(d => d.SolicitudesProyectosIdTicket)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Proyectos_Solicitudes_Proyectos_FK");
            });

            modelBuilder.Entity<PuestosTrabajo>(entity =>
            {
                entity.HasKey(e => e.IdPuesto)
                    .HasName("Puestos_Trabajo_PK");

                entity.ToTable("Puestos_Trabajo");

                entity.Property(e => e.IdPuesto).HasColumnName("id_puesto");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre")
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PuestosXHistorial>(entity =>
            {
                entity.HasKey(e => new { e.PuestosTrabajoIdPuesto, e.HistorialPuestosIdHistorial })
                    .HasName("puestos_x_historial_PK");

                entity.ToTable("puestos_x_historial");

                entity.Property(e => e.PuestosTrabajoIdPuesto).HasColumnName("Puestos_Trabajo_id_puesto");

                entity.Property(e => e.HistorialPuestosIdHistorial).HasColumnName("Historial_Puestos_id_historial");

                entity.HasOne(d => d.HistorialPuestosIdHistorialNavigation)
                    .WithMany(p => p.PuestosXHistorial)
                    .HasForeignKey(d => d.HistorialPuestosIdHistorial)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("puestos_x_historial_Historial_Puestos_FK");

                entity.HasOne(d => d.PuestosTrabajoIdPuestoNavigation)
                    .WithMany(p => p.PuestosXHistorial)
                    .HasForeignKey(d => d.PuestosTrabajoIdPuesto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("puestos_x_historial_Puestos_Trabajo_FK");
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.HasKey(e => e.IdRol)
                    .HasName("Roles_PK");

                entity.Property(e => e.IdRol).HasColumnName("id_rol");

                entity.Property(e => e.NombreRol)
                    .IsRequired()
                    .HasColumnName("nombre_rol")
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RolesXPrivilegios>(entity =>
            {
                entity.HasKey(e => new { e.PrivilegiosIdPrivilegio, e.RolesIdRol })
                    .HasName("roles_x_privilegios_PK");

                entity.ToTable("roles_x_privilegios");

                entity.Property(e => e.PrivilegiosIdPrivilegio).HasColumnName("Privilegios_id_privilegio");

                entity.Property(e => e.RolesIdRol).HasColumnName("Roles_id_rol");

                entity.HasOne(d => d.PrivilegiosIdPrivilegioNavigation)
                    .WithMany(p => p.RolesXPrivilegios)
                    .HasForeignKey(d => d.PrivilegiosIdPrivilegio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("roles_x_privilegios_Privilegios_FK");

                entity.HasOne(d => d.RolesIdRolNavigation)
                    .WithMany(p => p.RolesXPrivilegios)
                    .HasForeignKey(d => d.RolesIdRol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("roles_x_privilegios_Roles_FK");
            });

            modelBuilder.Entity<SolicitudesProyectos>(entity =>
            {
                entity.HasKey(e => e.IdTicket)
                    .HasName("Solicitudes_Proyectos_PK");

                entity.ToTable("Solicitudes_Proyectos");

                entity.Property(e => e.IdTicket).HasColumnName("id_ticket");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Presupuesto)
                    .HasColumnName("presupuesto")
                    .HasColumnType("numeric(10, 2)");

                entity.Property(e => e.Tecnologias)
                    .IsRequired()
                    .HasColumnName("tecnologias")
                    .HasMaxLength(70)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usuarios>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("Usuarios_PK");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.Contrasenia)
                    .IsRequired()
                    .HasColumnName("contrasenia")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion)
                    .HasColumnName("direccion")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.RolesIdRol).HasColumnName("Roles_id_rol");

                entity.Property(e => e.Telefono)
                    .HasColumnName("telefono")
                    .HasColumnType("numeric(10, 0)");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.HasOne(d => d.RolesIdRolNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.RolesIdRol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Usuarios_Roles_FK");
            });

            modelBuilder.HasSequence<int>("ClientesSequence");

            modelBuilder.HasSequence<int>("EmpleadosSequence");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


        public decimal GetMySequence() {
            SqlParameter result = new SqlParameter("@result", System.Data.SqlDbType.Decimal) {
                Direction = System.Data.ParameterDirection.Output
            };

            Database.ExecuteSqlCommand("SELECT TOP 1 * FROM Usuarios ORDER BY id_usuario DESC;", result);
            return (decimal)result.Value;
        }
        public decimal GetMySequenceClientes() {
            SqlParameter result = new SqlParameter("@result", System.Data.SqlDbType.Decimal) {
                Direction = System.Data.ParameterDirection.Output
            };

            Database.ExecuteSqlCommand("SELECT @result = (NEXT VALUE FOR ClientesSequence)", result);
            return (decimal)result.Value;
        }
        public decimal GetMySequenceEmpleados() {
            SqlParameter result = new SqlParameter("@result", System.Data.SqlDbType.Decimal) {
                Direction = System.Data.ParameterDirection.Output
            };

            Database.ExecuteSqlCommand("SELECT @result = (NEXT VALUE FOR EmpleadosSequence)", result);
            return (decimal)result.Value;
        }

        public int sequenceUsuarios() {
            var connection = Database.GetDbConnection();
            connection.Open();
            using (var cmd = connection.CreateCommand()) {
                cmd.CommandText = "SELECT TOP 1 * FROM Usuarios ORDER BY id_usuario DESC;";
                var obj = cmd.ExecuteScalar();
                int anInt = (int)obj;
                connection.Close();
                return anInt;
            }
        }

        public int sequenceTicket() {
            var connection = Database.GetDbConnection();
            connection.Open();
            using (var cmd = connection.CreateCommand()) {
                cmd.CommandText = "SELECT TOP 1 * FROM Solicitudes_Proyectos ORDER BY id_ticket DESC;";
                var obj = cmd.ExecuteScalar();
                int anInt = (int)obj;
                connection.Close();
                return anInt;
            }
        }
        //////////////////////////////////////////////////////////////
        public decimal usernameExist(string username) {
            var connection = Database.GetDbConnection();
            connection.Open();
            using (var cmd = connection.CreateCommand()) {
                cmd.CommandText = "select count(*) from Usuarios where username = '" + username.ToLower() + "';";
                var obj = cmd.ExecuteScalar();
                int anInt = (int)obj;
                connection.Close();
                return anInt;
            }
        }
        public string obtenerSolicitudes(int cliente) {
            var connection = Database.GetDbConnection();
            connection.Open();
            using (var cmd = connection.CreateCommand()) {
                var jsonResult = new StringBuilder();
                cmd.CommandText = "SELECT * FROM Proyectos_Clientes WHERE Clientes_id_usuario = ('" + cliente + "') AND (estado_solicitud = 'EN ESPERA' OR estado_solicitud ='DENEGADO') ";
                var reader = cmd.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(reader);
                string JSONString = JsonConvert.SerializeObject(dataTable);

                return JSONString;
            }
        }

        public string obtenerSolicitudesEspera() {
            var connection = Database.GetDbConnection();
            connection.Open();
            using (var cmd = connection.CreateCommand()) {
                var jsonResult = new StringBuilder();
                cmd.CommandText = "SELECT * FROM Proyectos_Clientes WHERE estado_solicitud = 'EN ESPERA' OR estado_solicitud = 'DENEGADO'";
                var reader = cmd.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(reader);
                string JSONString = JsonConvert.SerializeObject(dataTable);

                return JSONString;
            }
        }
        public string obtenerProyectosAprobados(int id) {
            var connection = Database.GetDbConnection();
            connection.Open();
            using (var cmd = connection.CreateCommand()) {
                var jsonResult = new StringBuilder();
                cmd.CommandText = "SELECT * FROM Proyectos_Clientes WHERE estado_solicitud = 'APROBADO' AND Clientes_id_usuario = '" + id + "' ";
                var reader = cmd.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(reader);
                string JSONString = JsonConvert.SerializeObject(dataTable);
                return JSONString;
            }
        }

        public string obtenerHistoriasUsuario(int id) {
            var connection = Database.GetDbConnection();
            connection.Open();
            using (var cmd = connection.CreateCommand()) {
                var jsonResult = new StringBuilder();
                cmd.CommandText = "SELECT * FROM Historias_Usuario WHERE Equipos_Trabajo_id_grupo = '" + id + "' and NOT EXISTS (SELECT * FROM Actividades_x_Historia WHERE Historias_Usuarios_id_historia = id_historia) ";
                var reader = cmd.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(reader);
                string JSONString = JsonConvert.SerializeObject(dataTable);
                return JSONString;
            }
        }

        public string obtenerActividadesJP(int id) {
            var connection = Database.GetDbConnection();
            connection.Open();
            using (var cmd = connection.CreateCommand()) {
                var jsonResult = new StringBuilder();
                cmd.CommandText = "SELECT * FROM Mostrar_Actividades_JP WHERE Equipos_Trabajo_id_grupo = '"+id+"'";
                var reader = cmd.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(reader);
                string JSONString = JsonConvert.SerializeObject(dataTable);
                return JSONString;
            }
        }


        public int getID_historial() {
            var connection = Database.GetDbConnection();
            connection.Open();
            using (var cmd = connection.CreateCommand()) {
                cmd.CommandText = "SELECT TOP 1 * FROM Historial_Puestos ORDER BY id_historial DESC;";
                var obj = cmd.ExecuteScalar();
                int anInt = (int)obj;
                connection.Close();
                return anInt;
            }
        }
    }
}
