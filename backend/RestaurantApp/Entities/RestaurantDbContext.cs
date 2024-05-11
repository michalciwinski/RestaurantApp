using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RestaurantApp.Entities;

public partial class RestaurantDbContext : DbContext
{
    private string _connectionString { get; set; }
    public RestaurantDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TcompositionPosition> TcompositionPositions { get; set; }

    public virtual DbSet<TdishType> TdishTypes { get; set; }

    public virtual DbSet<Tingredient> Tingredients { get; set; }

    public virtual DbSet<Tmenu> Tmenus { get; set; }

    public virtual DbSet<Topinion> Topinions { get; set; }

    public virtual DbSet<TopinionsPosition> TopinionsPositions { get; set; }

    public virtual DbSet<Torder> Torders { get; set; }

    public virtual DbSet<TorderPosition> TorderPositions { get; set; }

    public virtual DbSet<Tstate> Tstates { get; set; }

    public virtual DbSet<Tuser> Tusers { get; set; }

    public virtual DbSet<TuserType> TuserTypes { get; set; }

    public virtual DbSet<TvMenuIngredient> TvMenuIngredients { get; set; }

    public virtual DbSet<TvMenuPricesDishType> TvMenuPricesDishTypes { get; set; }

    IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(config["ConnectionStrings:EntitiesDB"]);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TcompositionPosition>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TCompositionPosition_pkey");

            entity.ToTable("TcompositionPosition");

            entity.Property(e => e.Id).HasIdentityOptions(31L, null, null, null, null, null);
            entity.Property(e => e.TingredientsId).HasColumnName("TIngredientsId");
            entity.Property(e => e.TmenuId).HasColumnName("TMenuId");

            entity.HasOne(d => d.Tingredients).WithMany(p => p.TcompositionPositions)
                .HasForeignKey(d => d.TingredientsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("TIngredientsId");

            entity.HasOne(d => d.Tmenu).WithMany(p => p.TcompositionPositions)
                .HasForeignKey(d => d.TmenuId)
                .HasConstraintName("TMenuId");
        });

        modelBuilder.Entity<TdishType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TDishType");

            entity.ToTable("TdishType");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(25)
                .HasDefaultValueSql("''::character varying");
        });

        modelBuilder.Entity<Tingredient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TIngredients_pkey");

            entity.Property(e => e.Id).HasIdentityOptions(17L, null, null, null, null, null);
            entity.Property(e => e.NameOfIngredient).IsRequired();
        });

        modelBuilder.Entity<Tmenu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TMenu");

            entity.ToTable("Tmenu");

            entity.HasIndex(e => e.TdishTypeId, "IX_TMenu_TDishTypeId");

            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(50)
                .HasDefaultValueSql("''::character varying");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(25)
                .HasDefaultValueSql("''::character varying");
            entity.Property(e => e.SrcPict)
                .HasMaxLength(100)
                .HasDefaultValueSql("''::character varying");
            entity.Property(e => e.TdishTypeId).HasColumnName("TDishTypeId");

            entity.HasOne(d => d.TdishType).WithMany(p => p.Tmenus)
                .HasForeignKey(d => d.TdishTypeId)
                .HasConstraintName("FK_TMenu_TDishType_TDishTypeId");
        });

        modelBuilder.Entity<Topinion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TOpinions_pkey");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.TmenuId).HasColumnName("TMenuId");

            entity.HasOne(d => d.Tmenu).WithMany(p => p.Topinions)
                .HasForeignKey(d => d.TmenuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("TMenuID");
        });

        modelBuilder.Entity<TopinionsPosition>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TOpinionsPosition_pkey");

            entity.ToTable("TopinionsPosition");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.TopinionsId).HasColumnName("TOpinionsId");
            entity.Property(e => e.Tuser).HasColumnName("TUser");

            entity.HasOne(d => d.Topinions).WithMany(p => p.TopinionsPositions)
                .HasForeignKey(d => d.TopinionsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("TOpinionsId");

            entity.HasOne(d => d.TuserNavigation).WithMany(p => p.TopinionsPositions)
                .HasForeignKey(d => d.Tuser)
                .HasConstraintName("TUserId");
        });

        modelBuilder.Entity<Torder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TOrder");

            entity.ToTable("Torder");

            entity.HasIndex(e => e.TstateId, "IX_TOrder_TStateId");

            entity.HasIndex(e => e.TuserId, "IX_TOrder_TUserId");

            entity.Property(e => e.TstateId).HasColumnName("TStateId");
            entity.Property(e => e.TuserId)
                .HasDefaultValueSql("0")
                .HasColumnName("TUserId");

            entity.HasOne(d => d.Tstate).WithMany(p => p.Torders)
                .HasForeignKey(d => d.TstateId)
                .HasConstraintName("FK_TOrder_TState_TStateId");

            entity.HasOne(d => d.Tuser).WithMany(p => p.Torders)
                .HasForeignKey(d => d.TuserId)
                .HasConstraintName("FK_TOrder_TUser_TUserId");
        });

        modelBuilder.Entity<TorderPosition>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TOrderPosition");

            entity.ToTable("TorderPosition");

            entity.HasIndex(e => e.TmenuId, "IX_TOrderPosition_TMenuId");

            entity.HasIndex(e => e.TorderId, "IX_TOrderPosition_TOrderId");

            entity.Property(e => e.TmenuId).HasColumnName("TMenuId");
            entity.Property(e => e.TorderId).HasColumnName("TOrderId");

            entity.HasOne(d => d.Tmenu).WithMany(p => p.TorderPositions)
                .HasForeignKey(d => d.TmenuId)
                .HasConstraintName("FK_TOrderPosition_TMenu_TMenuId");

            entity.HasOne(d => d.Torder).WithMany(p => p.TorderPositions)
                .HasForeignKey(d => d.TorderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TOrderPosition_TOrder_TOrderId");
        });

        modelBuilder.Entity<Tstate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TState");

            entity.ToTable("Tstate");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(25)
                .HasDefaultValueSql("''::character varying");
        });

        modelBuilder.Entity<Tuser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TUser");

            entity.ToTable("Tuser");

            entity.HasIndex(e => e.TuserTypeId, "IX_TUser_TUserTypeId");

            entity.Property(e => e.Qr).HasColumnName("QR");
            entity.Property(e => e.TuserTypeId).HasColumnName("TUserTypeId");

            entity.HasOne(d => d.TuserType).WithMany(p => p.Tusers)
                .HasForeignKey(d => d.TuserTypeId)
                .HasConstraintName("FK_TUser_TUserType_TUserTypeId");
        });

        modelBuilder.Entity<TuserType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TUserType");

            entity.ToTable("TuserType");

            entity.Property(e => e.Type)
                .IsRequired()
                .HasMaxLength(25)
                .HasDefaultValueSql("''::character varying");
        });

        modelBuilder.Entity<TvMenuIngredient>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("TvMenuIngredients");

            entity.Property(e => e.Name).HasMaxLength(25);
        });

        modelBuilder.Entity<TvMenuPricesDishType>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("TvMenuPricesDishType");

            entity.Property(e => e.Description).HasMaxLength(50);
            entity.Property(e => e.Dishtype)
                .HasMaxLength(25)
                .HasColumnName("dishtype");
            entity.Property(e => e.Name).HasMaxLength(25);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
