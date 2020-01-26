using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ImpostoDeRenda.Model
{
	public class CalculoContribuinte
	{
		public string Cpf { get; set; }

		public string Nome { get; set; }

		public decimal ValorIR { get; set; }

	}
}