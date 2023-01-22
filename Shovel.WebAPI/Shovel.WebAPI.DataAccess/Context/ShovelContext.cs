using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Shovel.WebAPI.Models;

public partial class ShovelContext : DbContext
{
    public ShovelContext()
    {
    }

    public ShovelContext(DbContextOptions<ShovelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ApplicationSystem> ApplicationSystems { get; set; }

    public virtual DbSet<LogicalDrive> LogicalDrives { get; set; }

    public virtual DbSet<PerformanceSystem> PerformanceSystems { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RolePermission> RolePermissions { get; set; }

    public virtual DbSet<Server> Servers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseNpgsql("Host=localhost;Database=shovel;Username=postgres;Password=postgres");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApplicationSystem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ApplicationSystem_pkey");

            entity.ToTable("ApplicationSystem");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Basepriority).HasColumnName("basepriority");
            entity.Property(e => e.Hasexited)
                .HasColumnType("boolean")
                .HasColumnName("hasexited");
            entity.Property(e => e.Machinename)
                .HasMaxLength(1024)
                .HasColumnName("machinename");
            entity.Property(e => e.Mainwindowtitle)
                .HasMaxLength(1024)
                .HasColumnName("mainwindowtitle");
            entity.Property(e => e.Maxworkingset).HasColumnName("maxworkingset");
            entity.Property(e => e.Minworkingset).HasColumnName("minworkingset");
            entity.Property(e => e.Nonpagedsystemmemorysize64).HasColumnName("nonpagedsystemmemorysize64");
            entity.Property(e => e.Pagedmemorysize64).HasColumnName("pagedmemorysize64");
            entity.Property(e => e.Pagedsystemmemorysize64).HasColumnName("pagedsystemmemorysize64");
            entity.Property(e => e.Peakvirtualmemorysize64).HasColumnName("peakvirtualmemorysize64");
            entity.Property(e => e.Peakworkingset64).HasColumnName("peakworkingset64");
            entity.Property(e => e.Priorityboostenabled)
                .HasColumnType("boolean")
                .HasColumnName("priorityboostenabled");
            entity.Property(e => e.Processname)
                .HasMaxLength(1024)
                .HasColumnName("processname");
            entity.Property(e => e.Serverid).HasColumnName("serverid");
            entity.Property(e => e.Startinfofilename)
                .HasMaxLength(1024)
                .HasColumnName("startinfofilename");
            entity.Property(e => e.Starttime).HasColumnName("starttime");
            entity.Property(e => e.Synctime).HasColumnName("synctime");
            entity.Property(e => e.InsertDate).HasColumnName("insertdate");
            entity.Property(e => e.Threadscount).HasColumnName("threadscount");
            entity.Property(e => e.Workingset64).HasColumnName("workingset64");

            entity.HasOne(d => d.Server).WithMany(p => p.ApplicationSystems)
                .HasForeignKey(d => d.Serverid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ApplicationSystem_serverid_fkey");
        });

        modelBuilder.Entity<LogicalDrive>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("LogicalDrive_pkey");

            entity.ToTable("LogicalDrive");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Availablefreespace).HasColumnName("availablefreespace");
            entity.Property(e => e.Drive)
                .HasMaxLength(1024)
                .HasColumnName("drive");
            entity.Property(e => e.Driveformat)
                .HasMaxLength(1024)
                .HasColumnName("driveformat");
            entity.Property(e => e.Drivetype)
                .HasMaxLength(1024)
                .HasColumnName("drivetype");
            entity.Property(e => e.Performancesystemid).HasColumnName("performancesystemid");
            entity.Property(e => e.Totalsize).HasColumnName("totalsize");
            entity.Property(e => e.Volumelabel)
                .HasMaxLength(1024)
                .HasColumnName("volumelabel");

            entity.HasOne(d => d.Performancesystem).WithMany(p => p.LogicalDrives)
                .HasForeignKey(d => d.Performancesystemid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("LogicalDrive_performancesystemid_fkey");
        });

        modelBuilder.Entity<PerformanceSystem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PerformanceSystem_pkey");

            entity.ToTable("PerformanceSystem");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Insertdate).HasColumnName("insertdate");
            entity.Property(e => e.Is64bitoperatingsystem)
                .HasColumnType("boolean")
                .HasColumnName("is64bitoperatingsystem");
            entity.Property(e => e.Is64bitprocess)
                .HasColumnType("boolean")
                .HasColumnName("is64bitprocess");
            entity.Property(e => e.Machinename)
                .HasMaxLength(1024)
                .HasColumnName("machinename");
            entity.Property(e => e.Operationsystem)
                .HasMaxLength(1024)
                .HasColumnName("operationsystem");
            entity.Property(e => e.Processid).HasColumnName("processid");
            entity.Property(e => e.Processorarchitecture)
                .HasMaxLength(1024)
                .HasColumnName("processorarchitecture");
            entity.Property(e => e.Processorcount).HasColumnName("processorcount");
            entity.Property(e => e.Processorlevel)
                .HasMaxLength(1024)
                .HasColumnName("processorlevel");
            entity.Property(e => e.Processormodel)
                .HasMaxLength(1024)
                .HasColumnName("processormodel");
            entity.Property(e => e.Processpath)
                .HasMaxLength(1024)
                .HasColumnName("processpath");
            entity.Property(e => e.Ramavailable).HasColumnName("ramavailable");
            entity.Property(e => e.Serverid).HasColumnName("serverid");
            entity.Property(e => e.Synctime).HasColumnName("synctime");
            entity.Property(e => e.Systemdirectory)
                .HasMaxLength(1024)
                .HasColumnName("systemdirectory");
            entity.Property(e => e.Systempagesize).HasColumnName("systempagesize");
            entity.Property(e => e.Tickcount64).HasColumnName("tickcount64");
            entity.Property(e => e.Userdomainname)
                .HasMaxLength(1024)
                .HasColumnName("userdomainname");
            entity.Property(e => e.Userinteractive)
                .HasColumnType("boolean")
                .HasColumnName("userinteractive");
            entity.Property(e => e.Username)
                .HasMaxLength(1024)
                .HasColumnName("username");
            entity.Property(e => e.Version)
                .HasMaxLength(1024)
                .HasColumnName("version");
            entity.Property(e => e.Workingset).HasColumnName("workingset");

            entity.HasOne(d => d.Server).WithMany(p => p.PerformanceSystems)
                .HasForeignKey(d => d.Serverid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("PerformanceSystem_serverid_fkey");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Permission_pkey");

            entity.ToTable("Permission");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Type)
                .HasMaxLength(100)
                .HasColumnName("type");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Role_pkey");

            entity.ToTable("Role");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<RolePermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("RolePermission_pkey");

            entity.ToTable("RolePermission");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Permissionid).HasColumnName("permissionid");
            entity.Property(e => e.Roleid).HasColumnName("roleid");

            entity.HasOne(d => d.Permission).WithMany(p => p.RolePermissions)
                .HasForeignKey(d => d.Permissionid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("RolePermission_permissionid_fkey");

            entity.HasOne(d => d.Role).WithMany(p => p.RolePermissions)
                .HasForeignKey(d => d.Roleid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("RolePermission_roleid_fkey");
        });

        modelBuilder.Entity<Server>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Server_pkey");

            entity.ToTable("Server");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Baseaddress)
                .HasMaxLength(100)
                .HasColumnName("baseaddress");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("User_pkey");

            entity.ToTable("User");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(300)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(100)
                .HasColumnName("phone");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("UserRole_pkey");

            entity.ToTable("UserRole");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Roleid).HasColumnName("roleid");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.Roleid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("UserRole_roleid_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("UserRole_userid_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
