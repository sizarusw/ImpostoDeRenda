using ImpostoDeRenda.Model;
using ImpostoDeRenda.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ImpostoDeRenda.Testes
{
	[TestClass]
	public class CalculoTeste
	{

		private IContribuinteRepository _contribuinte;

		[TestMethod]
		public void RetornarAliquotaTeste()
		{
			//Arrange
			decimal salarioLiquido = 6500;

			decimal salarioMinimo = 1000;

			decimal resultadoEsperado = (decimal)22.5;

			//Act
			decimal resultadoAtual = new ImpostoDeRenda.Controllers.CalculoController(_contribuinte).RetornarAliquota(salarioLiquido, salarioMinimo);

			//Assert
			Assert.AreEqual(resultadoEsperado, resultadoAtual);
		}

		[TestMethod]
		public void RetornarRendaLiquidaTeste()
		{
			//Arrange
			Contribuinte contribuinte = new Contribuinte();

			contribuinte.QuantidadeDependente = 2;
			contribuinte.Renda = 2000;

			decimal salarioMinimo = 1000;

			decimal resultadoEsperado = 1900;

			//Act
			decimal resultadoAtual = new ImpostoDeRenda.Controllers.CalculoController(_contribuinte).RetornarRendaLiquida(contribuinte, salarioMinimo);

			//Assert
			Assert.AreEqual(resultadoEsperado, resultadoAtual);
		}

		[TestMethod]
		public void CalcularIRTeste()
		{
			//Arrange
			Contribuinte contribuinte = new Contribuinte();

			contribuinte.QuantidadeDependente = 2;
			contribuinte.Renda = 4200;

			decimal salarioMinimo = 1000;

			decimal resultadoEsperado = 615;

			//Act
			decimal resultadoAtual = new ImpostoDeRenda.Controllers.CalculoController(_contribuinte).CalcularIR(contribuinte,salarioMinimo);

			//Assert
			Assert.AreEqual(resultadoEsperado, resultadoAtual);
		}

	}
}
