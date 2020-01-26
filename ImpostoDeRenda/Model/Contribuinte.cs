using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ImpostoDeRenda.Model
{
	[Table("Contribuinte", Schema = "dbo")]
	public class Contribuinte
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Display(Name = "ID do Contribuinte")]
		public int IdContribuinte { get; set; }

		[Required]
		[Column(TypeName = "varchar(14)")]
		[Display(Name = "CPF do Contribuinte")]
		public string Cpf { get; set; }

		[Required]
		[Column(TypeName = "varchar(255)")]
		[Display(Name = "Nome do Contribuinte")]
		public string Nome { get; set; }

		[Required]
		[Column(TypeName = "decimal")]
		[Display(Name = "Renda do Contribuinte")]
		public decimal Renda { get; set; }

		[Required]
		[Column(TypeName = "int")]
		[Display(Name = "Número de dependentes")]
		public int QuantidadeDependente { get; set; }

	}
}