using Moq;
using NUnit.Framework;
using EmployeePayslip.Services;
using EmployeePayslip.Interfaces;
using EmployeePayslip.Entities;
using System.Collections.Generic;

namespace EmployeePayslipTest
{
	[TestFixture]
	public class TaxCalculationServiceTest
	{
		private Mock<ITaxRepository> _taxRepositoryMock;
		private TaxCalculatorService _taxCalculatorService;

		[SetUp]
		public void Setup()
		{
			var incomeTaxes = new List<IncomeTax>();
			incomeTaxes.Add(new IncomeTax() { MinRange = 0, MaxRange = 18200, MinPayableTax = 0, AdditionalTax = 0 });
			incomeTaxes.Add(new IncomeTax() { MinRange = 18201, MaxRange = 37000, MinPayableTax = 0, AdditionalTax = 0.19m});
			incomeTaxes.Add(new IncomeTax() { MinRange = 37001, MaxRange = 80000, MinPayableTax = 3572, AdditionalTax = 0.325m});
			incomeTaxes.Add(new IncomeTax() { MinRange = 80001, MaxRange = 180000, MinPayableTax = 17547, AdditionalTax = 0.37m});
			incomeTaxes.Add(new IncomeTax() { MinRange = 180001, MaxRange = int.MaxValue, MinPayableTax = 54547, AdditionalTax = 0.45m});

			_taxRepositoryMock = new Mock<ITaxRepository>();
			_taxRepositoryMock.Setup(x => x.GetIncomeTaxCriteria()).Returns(incomeTaxes);
			_taxCalculatorService = new TaxCalculatorService(_taxRepositoryMock.Object);
		}

		[Test]
		public void CalculateIncomeTax_Should_Return_Zero_When_AnnualSalary_is_Negative()
		{
			var incomeTax = _taxCalculatorService.CalculateIncomeTax(-134);
			Assert.AreEqual(incomeTax, 0);
		}

		[Test]
		public void CalculateIncomeTax_Should_Return_Zero_When_AnnualSalary_is_Zero()
		{
			var incomeTax = _taxCalculatorService.CalculateIncomeTax(0);
			Assert.AreEqual(incomeTax, 0);
		}

		[Test]
		public void CalculateIncomeTax_Should_Return_Zero_When_AnnualSalary_is_18200()
		{
			var incomeTax = _taxCalculatorService.CalculateIncomeTax(18200);
			Assert.AreEqual(incomeTax, 0);
		}

		[Test]
		public void CalculateIncomeTax_Should_Return_Two_When_AnnualSalary_is_18350()
		{
			var incomeTax = _taxCalculatorService.CalculateIncomeTax(18350);
			Assert.AreEqual(incomeTax, 2);
		}

		[Test]
		public void CalculateIncomeTax_Should_Return_298_When_AnnualSalary_is_37000()
		{
			var incomeTax = _taxCalculatorService.CalculateIncomeTax(37000);
			Assert.AreEqual(incomeTax, 298);
		}

		[Test]
		public void CalculateIncomeTax_Should_Return_298_When_AnnualSalary_is_37001()
		{
			var incomeTax = _taxCalculatorService.CalculateIncomeTax(37001);
			Assert.AreEqual(incomeTax, 298);
		}

		[Test]
		public void CalculateIncomeTax_Should_Return_1462_When_AnnualSalary_is_80000()
		{
			var incomeTax = _taxCalculatorService.CalculateIncomeTax(80000);
			Assert.AreEqual(incomeTax, 1462);
		}

		[Test]
		public void CalculateIncomeTax_Should_Return_1462_When_AnnualSalary_is_80001()
		{
			var incomeTax = _taxCalculatorService.CalculateIncomeTax(80001);
			Assert.AreEqual(incomeTax, 1462);
		}

		[Test]
		public void CalculateIncomeTax_Should_Return_4546_When_AnnualSalary_is_180000()
		{
			var incomeTax = _taxCalculatorService.CalculateIncomeTax(180000);
			Assert.AreEqual(incomeTax, 4546);
		}

		[Test]
		public void CalculateIncomeTax_Should_Return_4546_When_AnnualSalary_is_180001()
		{
			var incomeTax = _taxCalculatorService.CalculateIncomeTax(180001);
			Assert.AreEqual(incomeTax, 4546);
		}

		[Test]
		public void CalculateIncomeTax_Should_Return_5296_When_AnnualSalary_is_200000()
		{
			var incomeTax = _taxCalculatorService.CalculateIncomeTax(200000);
			Assert.AreEqual(incomeTax, 5296);
		}
	}
}
