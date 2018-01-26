using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaioNet.Entity.Sistema.pessoa
{
    [Table("Pessoa_Contato")]
    public class PessoaContato
    {
        public int Id { get; set; }

        [Display(Name = "Nome")]
        [StringLength(200, ErrorMessage = "Máximo {1} caracteres.")]
        [Required(ErrorMessage = "Nome obrigatório.")]
        public string Nome { get; set; }

        [Display(Name = "Telefone")]
        [StringLength(11, ErrorMessage = "Máximo {1} caracteres.")]
        public string Telefone { get; set; }

        [Display(Name = "Email")]
        [StringLength(200, ErrorMessage = "Máximo {1} caracteres.")]
        public string Email { get; set; }

        public long PessoaId { get; set; }
        public Pessoa Pessoa { get; set; }
    }
}