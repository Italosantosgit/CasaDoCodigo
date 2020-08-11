using CasaDoCodigo.Models;
using CasaDoCodigo.Repositories;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using static CasaDoCodigo.Repositories.ProdutoRepository;

namespace CasaDoCodigo
{
    class DataService : IDataService
    {
        private readonly ApplicationContext context;
        private readonly IProdutoRepository produtoRepository;

        public DataService(ApplicationContext context, IProdutoRepository produtoRepository)
        {
            this.context = context;
            this.produtoRepository = produtoRepository;
        }

        public void InicializarDB()
        {
            context.Database.EnsureCreated();

            List<Livro> livro = GetLivros();
            produtoRepository.SaveProdutos(livro);
        }

        private static List<Livro> GetLivros()
        {
            var json = File.ReadAllText("livros.json");
            var livro = JsonConvert.DeserializeObject<List<Livro>>(json);
            return livro;
        }
    }


}
