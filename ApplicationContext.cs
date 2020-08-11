using CasaDoCodigo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //NOME DO CAMPO QUE VAI REPRESENTAR A CHAVE PRIMARIA DA TABELA
            modelBuilder.Entity<Produto>().HasKey(p => p.Id);

            modelBuilder.Entity<Pedido>().HasKey(p => p.Id);
            modelBuilder.Entity<Pedido>().HasMany(p => p.Itens).WithOne(p => p.Pedido);
            modelBuilder.Entity<Pedido>().HasOne(p => p.Cadastro).WithOne(p => p.Pedido).IsRequired();
           

            modelBuilder.Entity<ItemPedido>().HasKey(p => p.Id);
            modelBuilder.Entity<ItemPedido>().HasOne(p => p.Pedido);
            modelBuilder.Entity<ItemPedido>().HasOne(p => p.Produto);


            modelBuilder.Entity<Cadastro>().HasKey(p => p.Id);
            modelBuilder.Entity<Cadastro>().HasOne(p => p.Pedido);
        }
    }
}
