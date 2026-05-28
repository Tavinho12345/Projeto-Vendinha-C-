using Microsoft.EntityFrameworkCore;
using TrabalhoVendinha.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace TrabalhoVendinha.Data
{
    public class AppDbContext : DbContext
    {

        public DbSet<Cliente> Clientes => Set<Cliente>();

        public DbSet<Divida> Dividas => Set<Divida>();

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(
                "Data Source=C:\\datas\\sqlite\\vendinha.db");
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var modelCategoria = modelBuilder.Entity<Cliente>();
            var modelProduto = modelBuilder.Entity<Divida>();

            modelCategoria.ToTable("Clientes");

            modelCategoria.Property(e => e.Id).HasColumnName("id_cliente");
            modelCategoria.Property(e => e.Nome).HasColumnName("nome");
            modelCategoria.Property(e => e.CPF).HasColumnName("cpf");
            modelCategoria.Property(e => e.DataNascimento).HasColumnName("data_nascimento");
            modelCategoria.Property(e => e.Email).HasColumnName("email");
            modelCategoria.HasKey(e => e.Id);


            modelProduto.ToTable("Dividas");

            modelProduto.Property(e => e.Id).HasColumnName("id_divida");
            modelProduto.Property(e => e.Valor).HasColumnName("valor");
            modelProduto.Property(e => e.Paga).HasColumnName("paga");
            modelProduto.Property(e => e.DataCriacao).HasColumnName("data_criacao");
            modelProduto.Property(e => e.DataPagamento).HasColumnName("data_pagamento");
            modelProduto
                .HasOne(e => e.Cliente)
                .WithMany( c=> c.Dividas)
                .HasForeignKey("id_cliente");
             modelProduto.HasKey(e => e.Id);


            base.OnModelCreating(modelBuilder);
        }

        

    }


}
