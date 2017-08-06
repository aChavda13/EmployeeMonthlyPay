using Moq;
using NUnit.Framework;
using EmployeePayslip.Services;
using EmployeePayslip.Interfaces;
using EmployeePayslip.Entities;
using EmployeePayslip.Exceptions;

namespace EmployeePayslipTest
{
	[TestFixture]
	public class PaySlipServiceTest
	{
		private Mock<ITaxCalculatorService> _taxCalculatorServiceMock;
		private PaySlipService _paySlipService;
		private Employee _employee;

		[SetUp]
		public void Setup()
		{
			_employee = new Employee()
			{
				FirstName = "Ryan",
				LastName = "Chen",
				AnnualSalary = 120000,
				SuperRate = "10%",
				PayPeriod = "01 March – 31 March"
			};

			_taxCalculatorServiceMock = new Mock<ITaxCalculatorService>();
			_paySlipService = new PaySlipService(_taxCalculatorServiceMock.Object);
		}

		[Test]
		public void ValidateEmployeeDetails_Should_Throw_NegativeAnnualSalaryException_When_AnnualSalary_is_Negative()
		{
			Assert.Throws<NegativeAnnualSalaryException>(
				new TestDelegate(ThrowNegativeAnnualSalaryException));
		}

		[Test]
		public void ValidateEmployeeDetails_Should_Throw_InvalidSuperRateException_When_SuperRate_is_Greater_Than_50()
		{
			Assert.Throws(typeof(InvalidSuperRateException),
				new TestDelegate(ThrowInvalidSuperRateException_Greater50));
		}

		[Test]
		public void ValidateEmployeeDetails_Should_Throw_InvalidSuperRateException_When_SuperRate_is_Negative()
		{
			Assert.Throws(typeof(InvalidSuperRateException),
				new TestDelegate(ThrowInvalidSuperRateException_Negative));
		}

		[Test]
		public void GeneratePaySlip_Should_Return_Expected_FullName_For_Employee()
		{
			var paySlip = _paySlipService.GeneratePaySlip(_employee);

			Assert.AreEqual(paySlip.FullName, "Ryan Chen");
		}

		[Test]
		public void GeneratePaySlip_Should_Return_Expected_GrossIncome_For_Employee()
		{
			var paySlip = _paySlipService.GeneratePaySlip(_employee);

			Assert.AreEqual(paySlip.GrossIncome, 10000);
		}

		[Test]
		public void GeneratePaySlip_Should_Return_Expected_IncomeTax_For_Employee()
		{
			_taxCalculatorServiceMock.Setup(x => x.CalculateIncomeTax(It.IsAny<decimal>())).Returns(235);
			var paySlip = _paySlipService.GeneratePaySlip(_employee);

			Assert.AreEqual(paySlip.IncomeTax, 235);
		}

		[Test]
		public void GeneratePaySlip_Should_Return_Expected_NetIncome_For_Employee()
		{
			_taxCalculatorServiceMock.Setup(x => x.CalculateIncomeTax(It.IsAny<decimal>())).Returns(235);
			var paySlip = _paySlipService.GeneratePaySlip(_employee);

			Assert.AreEqual(paySlip.NetIncome, paySlip.GrossIncome - 235);
		}

		[Test]
		public void GeneratePaySlip_Should_Return_Expected_Super_For_Employee()
		{
			_taxCalculatorServiceMock.Setup(x => x.CalculateIncomeTax(It.IsAny<decimal>())).Returns(123);
			var paySlip = _paySlipService.GeneratePaySlip(_employee);

			Assert.AreEqual(paySlip.Super, (paySlip.GrossIncome * 0.1m));
		}




		#region Exception Helper

		private void ThrowNegativeAnnualSalaryException()
		{
			_employee.AnnualSalary = -1;
			_paySlipService.GeneratePaySlip(_employee);
		}

		private void ThrowInvalidSuperRateException_Greater50()
		{
			_employee.SuperRate = "51%";
			var paySlip = _paySlipService.GeneratePaySlip(_employee);
		}

		private void ThrowInvalidSuperRateException_Negative()
		{
			_employee.SuperRate = "-1%";
			var paySlip = _paySlipService.GeneratePaySlip(_employee);
		}

		#endregion
	}
}
