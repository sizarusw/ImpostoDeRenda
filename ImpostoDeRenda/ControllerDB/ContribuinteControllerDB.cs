using System.Collections.Generic;
using System.Linq;
using ImpostoDeRenda.DataAccess;
using ImpostoDeRenda.Model;
using ImpostoDeRenda.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ImpostoDeRenda.ControllerDB
{
	public class ContribuinteControllerDB : IContribuinteRepository
	{

		private readonly ImpostoRendaContext _contexto;

		public ContribuinteControllerDB(ImpostoRendaContext ctx)
		{
			_contexto = ctx;
		}


		/// <summary>
		/// Procura todos os dados do contribuinte.  
		/// </summary>
		public List<Contribuinte> GetAll()
		{
			List<Contribuinte> data = this._contexto.Contribuintes.ToList();
			return data;
		}

		/// <summary>
		/// Insere um contribuinte no banco da dados.  
		/// </summary>
		[HttpPost]
		public Contribuinte Add(Contribuinte model)
		{
			_contexto.Contribuintes.Add(model);
			_contexto.SaveChanges();
			return model;
		
		}

		/// <summary>
		/// Atualizar um contribuinte no banco da dados.  
		/// </summary>
		[HttpPost]
		public Contribuinte Update(Contribuinte model)
		{
			_contexto.Contribuintes.Update(model);
			_contexto.SaveChanges();

			return model;
		}

		public Contribuinte Find(int id)
		{
			Contribuinte model = _contexto.Contribuintes.FirstOrDefault(v => v.IdContribuinte == id);

			return model;
		}


	}
}