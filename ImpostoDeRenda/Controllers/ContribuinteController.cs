using ImpostoDeRenda.Model;
using ImpostoDeRenda.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ImpostoDeRenda.Controllers
{
	[Route("api/[controller]")]
	public class ContribuinteController : Controller
	{

		#region Dependencias

		private IContribuinteRepository _contribuinte;

		public ContribuinteController(IContribuinteRepository contribuinte)
		{
			_contribuinte = contribuinte;
		}

		#endregion

		[HttpGet]
		public string Get()
		{
			//Produto _produtoUnico = _produto.Find(1);
			List<Contribuinte> listaContribuintes = _contribuinte.GetAll();
			string jsonRetorno = JsonConvert.SerializeObject(listaContribuintes);
			return jsonRetorno;
		}

		[HttpPost]
		public async void Post()
		{
			string conteudoJSON = "";
			// Lendo os dados do Body
			using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
			{
				conteudoJSON = await reader.ReadToEndAsync();
			}

			Contribuinte contribuinte = JsonConvert.DeserializeObject<Contribuinte>(conteudoJSON);

			_contribuinte.Add(contribuinte);

		}

	}
}
