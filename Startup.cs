using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CasaDoCodigo.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CasaDoCodigo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddDistributedMemoryCache();//MATEM INFORMAÇÕES NA MEMORIA 
            services.AddSession();//MANTEM O ESTADO DE UMA MANEIRA CONTROLADA PELO SERVIDOR

            //PEGANDO A VARIAVEL DA CONNECTIONSTRINGS
            string connectionStrings = Configuration.GetConnectionString("Default");
            //ADD SERVICO SQLSERVER COM BASE NA CONNECTIONSTRINGS E CLASS APPLICATIONCONTEXT
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionStrings));

            services.AddTransient<IDataService, DataService>();
            services.AddTransient<IProdutoRepository, ProdutoRepository>();
            services.AddTransient<IItemPedidoRepository, ItemPedidoRepository>();
            services.AddTransient<IPedidoRepository, PedidoRepository>();
            services.AddTransient<ICadastroRepository, CadastroRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Pedido}/{action=Carrossel}/{codigo?}");
            });

            //CRIANDO O BANCO ATRAVES DO PARAMETOR IServiceProvider(AUTOMATICAMENTE)
            //EnsureCreated NÃO POSSIBILITA MIGRACOES
            //Migrate POSSIBILITA MIGRACOES
            serviceProvider.GetService<IDataService>().InicializarDB();

        }
    }
}
