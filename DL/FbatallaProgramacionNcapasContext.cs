using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DL;

public partial class FbatallaProgramacionNcapasContext : DbContext
{
    public FbatallaProgramacionNcapasContext()
    {
    }

    public FbatallaProgramacionNcapasContext(DbContextOptions<FbatallaProgramacionNcapasContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Colonium> Colonia { get; set; }

    public virtual DbSet<Departamento> Departamentos { get; set; }

    public virtual DbSet<Direccion> Direccions { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Estado> Estados { get; set; }

    public virtual DbSet<Libro> Libros { get; set; }

    public virtual DbSet<Municipio> Municipios { get; set; }

    public virtual DbSet<Restaurante> Restaurantes { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<UsuarioGetAllvw> UsuarioGetAllvws { get; set; }

    //DTOs for Usuario
    public virtual DbSet<UsuarioAddDTO> UsuarioAddDTOs { get; set; }

    public virtual DbSet<UsuarioDeleteDTO> UsuarioDeleteDTOs { get; set; }
    public virtual DbSet<UsuarioGetAllDTO> UsuarioGetAllDTOs { get; set; }
    public virtual DbSet<UsuarioGetByIdDTO> UsuarioGetByIdDTOs { get; set; }
    public virtual DbSet<UsuarioUpdateDTO> UsuarioUpdateDTOs { get; set; }
    //FIN DTOs for Usuario

    //DTO DE COLONIA MUNICIPIO Y ESTADO
    public virtual DbSet<ColoniaGetByIdMunicipioDTO> ColoniaGetByIdMunicipioDTOs { get; set; }
    public virtual DbSet<EstadoGetAllDTO> EstadoGetAllDTOs { get; set; }
    public virtual DbSet<MunicipioGetByIdEstadoDTO> MunicipioGetByIdEstadoDTOs { get; set; }
    // FIN DTO DE COLONIA MUNICIPIO Y ESTADO


    // DTOs for Rol
    public virtual DbSet<RolGetAllDTO> RolGetAllDTOs { get; set; }
    // FIN DTOs for Rol

    //DTO PARA SP EMPLEADO 

    public virtual DbSet<AgregarEmpleadoDTO> AgregarEmpleadoDTO { get; set; }
    public virtual DbSet<EmpleadoGetAllDTO> EmpleadoGetAllDTO { get; set; }
    public virtual DbSet<EmpleadoGetByIdDTO> EmpleadoGetByIdDTO { get; set; }
    public virtual DbSet<ActualizarEmpleadoDTO> ActualizarEmpleadoDTO { get; set; }
    public virtual DbSet<EliminarEmpleadoDTO> EliminarEmpleadoDTO { get; set; }

    //FIN DTO PARA SP EMPLEADO

    //SP DEPARTAMENTO
    public virtual DbSet<DepartamentoGetAllDTO> DepartamentoGetAllDTO { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Colonium>(entity =>
        {
            entity.HasKey(e => e.IdColonia).HasName("PK__Colonia__DF3A11324D0F8D5E");

            entity.Property(e => e.IdColonia)
                .ValueGeneratedNever()
                .HasColumnName("idColonia");
            entity.Property(e => e.CodigoPostal)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdMunicipioNavigation).WithMany(p => p.Colonia)
                .HasForeignKey(d => d.IdMunicipio)
                .HasConstraintName("FK__Colonia__IdMunic__7E37BEF6");
        });

        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.HasKey(e => e.IdDepartamento).HasName("PK__Departam__787A433D646114C7");

            entity.ToTable("Departamento");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Direccion>(entity =>
        {
            entity.HasKey(e => e.IdDireccion).HasName("PK__Direccio__1F8E0C7610DE078F");

            entity.ToTable("Direccion");

            entity.Property(e => e.Calle)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NumeroExterior)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.NumeroInterior)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.IdColoniaNavigation).WithMany(p => p.Direccions)
                .HasForeignKey(d => d.IdColonia)
                .HasConstraintName("FK__Direccion__IdCol__160F4887");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Direccions)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__Direccion__IdUsu__17036CC0");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.IdEmpleado).HasName("PK__Empleado__CE6D8B9EEECF7A7E");

            entity.ToTable("Empleado");

            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Curp)
                .HasMaxLength(18)
                .IsUnicode(false)
                .HasColumnName("CURP");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nss)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("NSS");
            entity.Property(e => e.Rfc)
                .HasMaxLength(13)
                .IsUnicode(false)
                .HasColumnName("RFC");
            entity.Property(e => e.SalarioBase).HasColumnType("money");

            entity.HasOne(d => d.IdDepartamentoNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdDepartamento)
                .HasConstraintName("FK__Empleado__IdDepa__0A688BB1");
        });

        modelBuilder.Entity<Estado>(entity =>
        {
            entity.HasKey(e => e.IdEstado).HasName("PK__Estado__FBB0EDC16FA5A075");

            entity.ToTable("Estado");

            entity.Property(e => e.IdEstado).ValueGeneratedNever();
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Libro>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Libro__3214EC075AD87D8E");

            entity.ToTable("Libro");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Autor)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Editorial)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.NumeroPagina)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Titulo)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Municipio>(entity =>
        {
            entity.HasKey(e => e.IdMunicipio).HasName("PK__Municipi__744935ACCCCB72E8");

            entity.ToTable("Municipio");

            entity.Property(e => e.IdMunicipio).ValueGeneratedNever();
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.Municipios)
                .HasForeignKey(d => d.IdEstado)
                .HasConstraintName("FK__Municipio__IdEst__03F0984C");
        });

        modelBuilder.Entity<Restaurante>(entity =>
        {
            entity.HasKey(e => e.IdRestaurante).HasName("PK__Restaura__29CE64FA7CC02F12");

            entity.ToTable("Restaurante");

            entity.Property(e => e.IdRestaurante).ValueGeneratedNever();
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Slogan)
                .HasMaxLength(60)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Rol__2A49584CEF42AD46");

            entity.ToTable("Rol");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("ID");

            entity.ToTable("Usuario");

            entity.HasIndex(e => e.Email, "CorreoUsuario").IsUnique();

            entity.HasIndex(e => e.UserName, "NameUser")
                .IsUnique()
                .HasFilter("([UserName] IS NOT NULL)");

            entity.HasIndex(e => e.UserName, "UQ_Email")
                .IsUnique()
                .HasFilter("([UserName] IS NOT NULL)");

            entity.HasIndex(e => e.UserName, "UQ_UserName").IsUnique();

            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Celular)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Curp)
                .HasMaxLength(18)
                .IsUnicode(false)
                .HasColumnName("CURP");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Sexo)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Usuario__IdRol__60A75C0F");
        });

        modelBuilder.Entity<UsuarioGetAllvw>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("UsuarioGetAllvw");

            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Calle)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Celular)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Curp)
                .HasMaxLength(18)
                .IsUnicode(false)
                .HasColumnName("CURP");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FechaNacimiento)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.NombreColonia)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NombreEstado)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NombreMunicipio)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NombreRol)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NumeroExterior)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.NumeroInterior)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Sexo)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });


        modelBuilder.Entity<UsuarioAddDTO>(entity =>
        {
            entity.HasNoKey();
        });
        modelBuilder.Entity<ColoniaGetByIdMunicipioDTO>(entity =>
        {
            entity.HasNoKey();
        });
        modelBuilder.Entity<EstadoGetAllDTO>(entity =>
        {
            entity.HasNoKey();
        });
        modelBuilder.Entity<MunicipioGetByIdEstadoDTO>(entity =>
        {
            entity.HasNoKey();
        });
        modelBuilder.Entity<RolGetAllDTO>(entity =>
        {
            entity.HasNoKey();
        });
        modelBuilder.Entity<UsuarioDeleteDTO>(entity =>
        {
            entity.HasNoKey();
        });
        modelBuilder.Entity<UsuarioGetAllDTO>(entity =>
        {
            entity.HasNoKey();
        });
        modelBuilder.Entity<UsuarioGetByIdDTO>(entity =>
        {
            entity.HasNoKey();
        });
        modelBuilder.Entity<UsuarioUpdateDTO>(entity =>
        {
            entity.HasNoKey();
        });

        modelBuilder.Entity<AgregarEmpleadoDTO>(entity =>
        {
            entity.HasNoKey();
        });
        modelBuilder.Entity<ActualizarEmpleadoDTO>(entity =>
        {
            entity.HasNoKey();
        });
        modelBuilder.Entity<EliminarEmpleadoDTO>(entity =>
        {
            entity.HasNoKey();
        });
        modelBuilder.Entity<EmpleadoGetAllDTO>(entity =>
        {
            entity.HasNoKey();
        });
        modelBuilder.Entity<EmpleadoGetByIdDTO>(entity =>
        {
            entity.HasNoKey();
        });
        modelBuilder.Entity<DepartamentoGetAllDTO>(entity =>
        {
            entity.HasNoKey();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
