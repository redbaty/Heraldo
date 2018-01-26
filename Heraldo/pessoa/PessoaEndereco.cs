using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaioNet.Entity.Sistema.pessoa
{
    [Table("PessoaEndereco")]
    public class PessoaEndereco
    {
        public int Id { get; set; }

        [Display(Name = "Endereço Principal")]
        [Required(ErrorMessage = "Endereço Principal obrigatório.")]
        public bool EnderecoPrincipal { get; set; }

        [Display(Name = "CEP")]
        [StringLength(8, ErrorMessage = "Máximo {1} caracteres.")]
        [Required(ErrorMessage = "CEP obrigatório.")]
        public string Cep { get; set; }

        [Display(Name = "Rua")]
        [StringLength(100, ErrorMessage = "Máximo {1} caracteres.")]
        [Required(ErrorMessage = "Rua obrigatório.")]
        public string Rua { get; set; }

        [Display(Name = "Numero")]
        [StringLength(100, ErrorMessage = "Máximo {1} caracteres.")]
        [Required(ErrorMessage = "Numero obrigatório.")]
        public string Numero { get; set; }

        [Display(Name = "Complemento")]
        [StringLength(100, ErrorMessage = "Máximo {1} caracteres.")]
        public string Complemento { get; set; }

        [Display(Name = "Bairro")]
        [StringLength(100, ErrorMessage = "Máximo {1} caracteres.")]
        [Required(ErrorMessage = "Bairro obrigatório.")]
        public string Bairro { get; set; }

        [Display(Name = "Cidade")]
        [StringLength(100, ErrorMessage = "Máximo {1} caracteres.")]
        [Required(ErrorMessage = "Cidade obrigatório.")]
        public string Cidade { get; set; }

        public long PessoaId { get; set; }
        public Pessoa Pessoa { get; set; }
    }
}