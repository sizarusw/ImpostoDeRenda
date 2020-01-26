using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ImpostoDeRenda.Model;
using Microsoft.AspNetCore.Mvc;

namespace ImpostoDeRenda.Repository
{
	public interface IContribuinteRepository
	{
		List<Contribuinte> GetAll();

		Contribuinte Add(Contribuinte contribuinte);

		Contribuinte Find(int id);
	}
}