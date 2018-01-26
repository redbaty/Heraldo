using System;
using System.Linq;
using Bogus;
using RaioNet.Entity.Sistema.pessoa;
using RaioNet.Entity.Sistema.pessoa.Enum;

namespace Heraldo
{
    static class Program
    {
        private static void Main(string[] args)
        {
            var context = new HeraldoContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var x = new Faker<Pessoa>()
                .RuleFor(i => i.Nome, f => f.Name.FullName())
                .RuleFor(i => i.Cpf_Cnpj, f => Guid.NewGuid().ToString())
                .RuleFor(i => i.TipoPessoa, f => f.PickRandom<EnumTipoPessoa>());
            context.Pessoas.AddRange(x.Generate(10));
            context.SaveChanges();
            var findName = context.Pessoas.First().Nome;

            var result = context.Pessoas.FindByObject(new Pessoa
            {
                Nome = findName
            });

        }
    }
}
