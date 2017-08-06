using System;
using EmployeePayslip.Entities;
using EmployeePayslip.Interfaces;
using EmployeePayslip.Exceptions;

namespace EmployeePayslip.Services
{
	public class PaySlipService : IPaySlipService
	{
		private ITaxCalculatorService _taxCalculatorService;

		public PaySlipService(ITaxCalculatorService taxCalculatorService)
		{
			_taxCalculatorService = taxCalculatorService;
		}

		public Payslip GeneratePaySlip(Employee employee)
		{
			ValidateEmployeeDetails(employee);
			var result = new Payslip()
			{
				FirstName = employee.FirstName,
				LastName = employee.LastName,
				AnnualSalary = employee.AnnualSalary,
				SuperRate = employee.SuperRate,
				PayPeriod = employee.PayPeriod
			};
			result.IncomeTax = _taxCalculatorService.CalculateIncomeTax(employee.AnnualSalary);
			result.NetIncome = result.GrossIncome - result.IncomeTax;
			result.Super = Math.Round(result.GrossIncome * decimal.Parse(result.SuperRate.Substring(0, result.SuperRate.IndexOf('%'))) / 100);

			return result;
		}

		private void ValidateEmployeeDetails(Employee employee)
		{
			if (employee.AnnualSalary < 0)
			{
				throw new NegativeAnnualSalaryException("Annual Salary cannot be negative");
			}
			var rate = double.Parse(employee.SuperRate.Substring(0, employee.SuperRate.IndexOf('%')));
			if (rate < 0 || rate > 50)
			{
				throw new InvalidSuperRateException("Supper rate must between 0 and 50");
			}
		}

	}
}
