using RaioNet.Entity.Sistema.pessoa.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaioNet.Entity.Sistema.pessoa
{
    [Table("Pessoa")]
    public class Pessoa
    {
        public int Id { get; set; }
        
        [Display(Name = "Nome")]
        [StringLength(200, ErrorMessage = "Máximo {1} caracteres.")]
        [Required(ErrorMessage = "Nome Obrigatório.")]
        public string Nome { get; set; }

        [Display(Name = "CPF/CNPJ")]
        [StringLength(14, ErrorMessage = "Máximo {1} caracteres.")]
        [Required(ErrorMessage ="CPF/CNPJ obrigatório.")]
        public string Cpf_Cnpj { get; set; }

        [Display(Name = "Tipo de Pessoa")]
        [Required(ErrorMessage = "Tipo de pessoa obrigatório.")]
        public EnumTipoPessoa TipoPessoa { get; set; }

        [Display(Name = "Telefone")]
        [StringLength(10, ErrorMessage = "Máximo {1} caracteres.")]
        public string Telefone { get; set; }

        [Display(Name = "Celular")]
        [StringLength(11, ErrorMessage = "Máximo {1} caracteres.")]
        public string Celular { get; set; }

        [Display(Name = "Rg")]
        [StringLength(10, ErrorMessage = "Máximo {1} caracteres.")]
        public string Rg { get; set; }

        [Display(Name = "Tipo de Incrição Estadual")]
        [Required(ErrorMessage = "Tipo de Inscrição Estadual obrigatório.")]
        public EnumTipoInscricaoEstadual TipoInscricaoEstadual { get; set; }

        [Display(Name = "Inscrição Estadual")]
        [StringLength(30, ErrorMessage = "Máximo {1} caracteres.")]
        public string InscricaoEstadual { get; set; }

        [Display(Name = "Consumidor Final")]
        [Required(ErrorMessage = "Consumidor final obrigatório.")]
        public bool ConsumidorFinal { get; set; }

        public ICollection<PessoaEndereco> Endereco { get; set; }
        public ICollection<PessoaContato> Contatos { get; set; }

    }
}
