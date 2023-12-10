using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Services.Identity;

namespace RestaurantApp.Entities;

public partial class RestaurantDbContext : DbContext
{
    public RestaurantDbContext()
    {
    }

    public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TOrder> TOrder { get; set; }
    public virtual DbSet<TOrderPosition> TOrderPosition { get; set; }
    public virtual DbSet<TMenu> TMenu { get; set; }
    public virtual DbSet<TDishType> TDishType { get; set; }
    public virtual DbSet<TUserType> TUserType { get; set; }
    public virtual DbSet<TUser> TUser { get; set; }
    public virtual DbSet<TState> TState { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Server=localhost; Database=RestaurantDB; Port=5432;User Id=postgres; Password=123456;");
        //=> optionsBuilder.UseNpgsql("Name=ConnectionStrings:EntitiesDB");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TOrderPosition>()//Menu<->OrderPosition
            .HasOne(t => t.TMenu)
            .WithMany(y => y.TOrderPosition)
            .HasForeignKey(t => t.TMenuId);

        modelBuilder.Entity<TMenu>()//DishType<->TMenu
            .HasOne(t => t.TDishType)
            .WithMany(y => y.TMenu)
            .HasForeignKey("TDishTypeId");

        modelBuilder.Entity<TOrder>()//Order<->OrderPosition
            .HasMany(y => y.TOrderPosition)
            .WithOne(t => t.TOrder)
            .HasForeignKey(y => y.TOrderId)
            .IsRequired(false);

        modelBuilder.Entity<TUser>()//User<->Order
            .HasMany(y => y.TOrder)
            .WithOne(t => t.TUser)
            .HasForeignKey(t => t.TUserId);

        modelBuilder.Entity<TState>()//State<->Order
            .HasMany(y => y.TOrder)
            .WithOne(t => t.TState)
            .HasForeignKey(t => t.TStateId);

        modelBuilder.Entity<TUser>()//UserType<->TUser
            .HasOne(t => t.TUserType)
            .WithMany(y => y.TUser)
            .HasForeignKey("TUserTypeId");
            



        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
