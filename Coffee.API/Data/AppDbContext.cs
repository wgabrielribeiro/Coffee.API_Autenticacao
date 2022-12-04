using Coffee.API.Base;
using Coffee.API.Data.Dtos.UserCad;
using Coffee.API.Model.Cadastro;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Coffee.API.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>
    {
        public AppDbContext() { }
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt) { }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<CreateUserInsert>().
                HasNoKey();

            builder.Entity<Usuario>().
                HasNoKey();

            builder.Entity<UserModel>()
                .HasOne(id => id.Cadastro)
                .WithOne(cad => cad.User)
                .HasForeignKey<CadastroModel>(p => p.USUARIO);

            builder.Entity<UserModel>()
                .HasOne(id => id.Endereco)
                .WithOne(end => end.UserModel)
                .HasForeignKey<EnderecoModel>(end => end.USUARIO);

        }

        public DbSet<CreateUserInsert> CreateUserInsert { get; set; }
        public DbSet<Usuario> Usuario { get; set; }

        public DbSet<CadastroModel> DadosCadastro { get; set; }
        public DbSet<EnderecoModel> DadosEndereco { get; set; }
        public DbSet<UserModel> CoffeeUser { get; set; }

        //public AppDbContext() { }
        //public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt){}


        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    builder.Entity<UserModel>()
        //        .HasOne(id => id.Cadastro)
        //        .WithOne(cad => cad.User)
        //        .HasForeignKey<CadastroModel>(p => p.USUARIO);

        //    builder.Entity<UserModel>()
        //        .HasOne(id => id.Endereco)
        //        .WithOne(end => end.UserModel)
        //        .HasForeignKey<EnderecoModel>(end => end.USUARIO);

        //}
        //public DbSet<CadastroModel> DadosCadastro { get; set; }
        //public DbSet<EnderecoModel> DadosEndereco { get; set; }
        //public DbSet<UserModel> CoffeeUser { get; set; }


    }
}
