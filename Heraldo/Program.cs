using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using RaioNet.Entity.Sistema.pessoa;
using RaioNet.Entity.Sistema.pessoa.Enum;

namespace Heraldo
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var context = new HeraldoContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var x = new Faker<Pessoa>()
                .RuleFor(i => i.Nome, f => "Heraldo")
                .RuleFor(i => i.Cpf_Cnpj, f => Guid.NewGuid().ToString())
                .RuleFor(i => i.Endereco, f => new List<PessoaEndereco>
                {
                    new PessoaEndereco
                    {
                        Cidade = f.Address.City(),
                        Bairro = f.Address.State(),
                        Cep = f.Address.ZipCode(),
                        Numero = f.Address.BuildingNumber(),
                        Rua = f.Address.StreetAddress()
                    }
                })
                .RuleFor(i => i.TipoPessoa, f => f.PickRandom<EnumTipoPessoa>());
            context.Pessoas.AddRange(x.Generate(10));
            context.SaveChanges();
            var findName = context.Pessoas.First().Nome;
            var findNeighb = context.Pessoas.First().Endereco.First().Bairro;

            var result = context.Pessoas.FindByObject(new Pessoa
            {
                Endereco = new List<PessoaEndereco>
                {
                    new PessoaEndereco
                    {
                        Bairro = findNeighb
                    }
                }
            }).ToList();
        }
    }
}