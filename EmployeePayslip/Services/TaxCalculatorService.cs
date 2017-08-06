using System;
using EmployeePayslip.Interfaces;

namespace EmployeePayslip.Services
{
	public class TaxCalculatorService : ITaxCalculatorService
	{
		public ITaxRepository _taxRepository;

		public TaxCalculatorService(ITaxRepository taxRepository)
		{
			_taxRepository = taxRepository;
		}

		public decimal CalculateIncomeTax(decimal annualSalary)
		{
			decimal result = 0;

			if (annualSalary < 0)
				return result;

			var incomeTaxes = _taxRepository.GetIncomeTaxCriteria();
			for (int i = 0; i < incomeTaxes.Count; i++)
			{
				if (annualSalary >= incomeTaxes[i].MinRange && annualSalary <= incomeTaxes[i].MaxRange)
				{
					result = incomeTaxes[i].MinPayableTax + incomeTaxes[i].AdditionalTax * (annualSalary - (incomeTaxes[i].MinRange - 1));
					result = Math.Round(result / 12);
					break;
				}
			}

			return result;
		}
	}
}
