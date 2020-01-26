using ImpostoDeRenda.Model;
using ImpostoDeRenda.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ImpostoDeRenda.Controllers
{
	[Route("api/[controller]")]
	public class CalculoController : Controller
	{

		#region Dependencias

		private IContribuinteRepository _contribuinte;

		public CalculoController(IContribuinteRepository contribuinte)
		{
			_contribuinte = contribuinte;
		}

		#endregion

		[HttpPost]
		public async Task<string> Post()
		{

			string conteudoAPI = "";
			// Lendo os dados do Body
			using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
			{
				conteudoAPI = await reader.ReadToEndAsync();
			}

			List<Contribuinte> contribuintes = BuscarContribuintes();

			string retornoJSONComCalculo = "";

			if (contribuintes != null && contribuintes.Count > 0)
			{
				retornoJSONComCalculo = CriarJsonContribuintesIR(contribuintes, Convert.ToDecimal(conteudoAPI));
			}

			return retornoJSONComCalculo;
		}

		#region Metodos dos Calculos IR

		/// <summary>
		/// Busca os contribuintes através do EF
		/// </summary>
		public List<Contribuinte> BuscarContribuintes()
		{
			return _contribuinte.GetAll();
		}

		/// <summary>
		/// Cria o JSON para retorno do WEB API
		/// </summary>
		/// <param name="contribuintes"> Lista de todos os contribuintes.</param>
		/// <param name="salarioMinimo"> Valor em decimal do Salario Minimo</param>
		public string CriarJsonContribuintesIR(List<Contribuinte> contribuintes, decimal salarioMinimo)
		{

			List<CalculoContribuinte> calculoContribuintes = new List<CalculoContribuinte>();

			if (contribuintes != null)
			{
				foreach (Contribuinte item in contribuintes)
				{
					CalculoContribuinte calculoContribuinte = new CalculoContribuinte();

					calculoContribuinte.Cpf = item.Cpf;
					calculoContribuinte.Nome = item.Nome;

					// Faz o calculo do IR baseado no objeto do contribuinte e Salario Minimo
					calculoContribuinte.ValorIR = CalcularIR(item,salarioMinimo);

					calculoContribuintes.Add(calculoContribuinte);
				}
			}

			calculoContribuintes = OrdenarContribuintePorIR(calculoContribuintes);

			string jsonCalculo = "";

			if (calculoContribuintes != null && calculoContribuintes.Count > 0)
			{
				jsonCalculo = JsonConvert.SerializeObject(calculoContribuintes);
			}

			return jsonCalculo;
		}

		/// <summary>
		/// Faz o calculo de imposto de renda do contribuinte selecionado
		/// </summary>
		/// <param name="contribuinte"> Contribuinte com informações preenchidas</param>
		/// <param name="salarioMinimo"> Valor em decimal do Salario Minimo</param>
		public decimal CalcularIR(Contribuinte contribuinte, decimal salarioMinimo)
		{
			decimal valorIR = 0;

			decimal rendaLiquida = RetornarRendaLiquida(contribuinte, salarioMinimo);

			decimal aliquota = RetornarAliquota(rendaLiquida, salarioMinimo);

			if (aliquota > 0) {
				valorIR = rendaLiquida * (aliquota / 100);
			}

			return valorIR;
		}

		/// <summary>
		/// Retorna o valor da aliquota de acordo com renda liquida do contribuinte
		/// </summary>
		/// <param name="rendaLiquida"> Valor em decimal da renda liquida</param>
		/// <param name="salarioMinimo"> Valor em decimal do Salario Minimo</param>
		public decimal RetornarAliquota(decimal rendaLiquida, decimal salarioMinimo)
		{
			decimal aliquota = 0;

			if (rendaLiquida > (salarioMinimo * 7))
			{
				aliquota = (decimal)27.5;
			}else if (rendaLiquida > (salarioMinimo * 5))
			{
				aliquota = (decimal)22.5;
			}
			else if (rendaLiquida > (salarioMinimo * 4))
			{
				aliquota = (decimal)15;
			}
			else if (rendaLiquida > (salarioMinimo * 2))
			{
				aliquota = (decimal)7.5;
			}
			else
			{
				aliquota = 0;
			}

			return aliquota;
		}

		/// <summary>
		/// Retorna a renda liquida
		/// </summary>
		/// <param name="contribuinte"> Contribuinte com informações preenchidas</param>
		/// <param name="salarioMinimo"> Valor em decimal do Salario Minimo</param>
		public decimal RetornarRendaLiquida(Contribuinte contribuinte, decimal salarioMinimo)
		{
			decimal descontoUnicoDependente = salarioMinimo * (decimal)0.05;

			decimal descontoPorDependente = descontoUnicoDependente * contribuinte.QuantidadeDependente;

			decimal rendaLiquida = contribuinte.Renda;

			if (descontoPorDependente > 0)
			{
				rendaLiquida = rendaLiquida - descontoPorDependente;
			}
			return rendaLiquida;
		}

		/// <summary>
		/// Ordena a lista de Contribuintes pelo valor de IR e Nome
		/// </summary>
		/// <param name="listaCalculoContribuinte"> Valor em decimal da renda liquida</param>
		public List<CalculoContribuinte> OrdenarContribuintePorIR(List<CalculoContribuinte> listaCalculoContribuinte)
		{
			// Order com Linq
			listaCalculoContribuinte = listaCalculoContribuinte.OrderBy(o => o.ValorIR).ThenBy(n => n.Nome).ToList();
			return listaCalculoContribuinte;
		}
		#endregion

	}
}
