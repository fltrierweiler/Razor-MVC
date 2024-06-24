using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RazorMVC.Data;

namespace RazorMVC.Models
{
    public static class SeedDatabase
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new StorageContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<StorageContext>>()))
            {
                if (context.Fornecedores.Any() || context.Produtos.Any())
                {
                    return;
                }
                context.Fornecedores.AddRange(
                    new Fornecedor()
                    {
                        Nome = "Paulo",
                        Telefone = 922220000
                    },
                    new Fornecedor()
                    {
                        Nome = "Jorge",
                        Telefone = 988884444
                    },
                    new Fornecedor()
                    {
                        Nome = "João"
                    }
                );
                context.Produtos.AddRange(
                    new Produto()
                    {
                        Nome = "Cenoura",
                        Descrição = "Legume",
                        Preço = 2.0M,
                        FornecedorId = 1
                    },
                    new Produto()
                    {
                        Nome = "Maçã",
                        Descrição = "Fruta",
                        Preço = 3.50M,
                        FornecedorId = 2
                    },
                    new Produto()
                    {
                        Nome = "Alface",
                        Preço = 1.33M,
                        FornecedorId = 3
                    },
                    new Produto()
                    {
                        Nome = "Café",
                        Descrição = "Torrado",
                        Preço = 15.99M
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
