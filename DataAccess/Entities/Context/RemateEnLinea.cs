using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace Entities.Entities
{
    public partial class RemateEnLinea : DbContext
    {
        public RemateEnLinea()
        {
        }

        public RemateEnLinea(DbContextOptions<RemateEnLinea> options)
            : base(options)
        {
        }

       
      
        public virtual DbSet<ca_perfiles> ca_perfiles { get; set; }
        public virtual DbSet<ca_perfilesopciones> ca_perfilesopciones { get; set; }
  
        public virtual DbSet<ca_usuarioadministrador> ca_usuarioadministrador { get; set; }
        public virtual DbSet<ca_usuarios> ca_usuarios { get; set; }
       
     
        public virtual DbSet<tg_personas> tg_personas { get; set; }
        

        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationBuilder conf = new ConfigurationBuilder();
                conf.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));
                var root = conf.Build();
                optionsBuilder
                    .UseLazyLoadingProxies()
                    .UseSqlServer(root.GetConnectionString("DefaultConnection"), builder => builder.UseRowNumberForPaging());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

          

        

            modelBuilder.Entity<ca_perfiles>(entity =>
            {
                entity.HasKey(e => e.id_perfil)
                    .HasName("ca_perfiles_PK")
                    .ForSqlServerIsClustered(false);

                entity.Property(e => e.pe_descripcion).IsUnicode(false);

                entity.Property(e => e.pe_perfil).IsUnicode(false);

            });

            modelBuilder.Entity<ca_perfilesopciones>(entity =>
            {
                entity.HasKey(e => e.id_perfilopcion)
                    .HasName("ca_perfilesopciones_PK")
                    .ForSqlServerIsClustered(false);

            

                entity.HasOne(d => d.id_perfilNavigation)
                    .WithMany(p => p.ca_perfilesopciones)
                    .HasForeignKey(d => d.id_perfil)
                    .HasConstraintName("ca_perfiles_ca_perfilesopciones_FK1");
            });

           
            modelBuilder.Entity<ca_usuarioadministrador>(entity =>
            {
                entity.Property(e => e.usa_descripcion).IsUnicode(false);

                entity.HasOne(d => d.id_usuarioNavigation)
                    .WithMany(p => p.ca_usuarioadministrador)
                    .HasForeignKey(d => d.id_usuario)
                    .HasConstraintName("FK_ca_usuarioadministrador_ca_usuarios");
            });

            modelBuilder.Entity<ca_usuarios>(entity =>
            {
                entity.HasKey(e => e.id_usuario)
                    .HasName("tg_empleados_PK")
                    .ForSqlServerIsClustered(false);

                entity.Property(e => e.us_bloqueado).HasDefaultValueSql("((0))");

                entity.Property(e => e.us_consuser).IsUnicode(false);

                entity.Property(e => e.us_password).IsUnicode(false);

                entity.HasOne(d => d.id_personaNavigation)
                    .WithMany(p => p.ca_usuarios)
                    .HasForeignKey(d => d.id_persona)
                    .HasConstraintName("tg_personas_ca_usuarios_FK1");
            });

           
            modelBuilder.Entity<tg_personas>(entity =>
            {
                entity.Property(e => e.pe_apmaterno).IsUnicode(false);

                entity.Property(e => e.pe_appaterno).IsUnicode(false);

                entity.Property(e => e.pe_celular).IsUnicode(false);

                entity.Property(e => e.pe_ciudad).IsUnicode(false);

                entity.Property(e => e.pe_comuna).IsUnicode(false);

                entity.Property(e => e.pe_direccion).IsUnicode(false);

                entity.Property(e => e.pe_email).IsUnicode(false);

                entity.Property(e => e.pe_giro).IsUnicode(false);

                entity.Property(e => e.pe_nacionalidad).IsUnicode(false);

                entity.Property(e => e.pe_nombrecompleto).IsUnicode(false);

                entity.Property(e => e.pe_nombres).IsUnicode(false);

                entity.Property(e => e.pe_numctacte).IsUnicode(false);

                entity.Property(e => e.pe_pais).IsUnicode(false);

                entity.Property(e => e.pe_rut).IsUnicode(false);

                entity.Property(e => e.pe_syncsat).HasDefaultValueSql("((0))");

                entity.Property(e => e.pe_telefono).IsUnicode(false);

                
            });

         
        }
    }
}
