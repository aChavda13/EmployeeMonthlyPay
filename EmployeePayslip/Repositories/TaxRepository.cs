using EmployeePayslip.Entities;
using EmployeePayslip.Interfaces;
using System.Collections.Generic;

namespace EmployeePayslip.Repositories
{
	class TaxRepository : ITaxRepository
	{
		public List<IncomeTax> GetIncomeTaxCriteria()
		{
			var incomeTaxes = new List<IncomeTax>();

			incomeTaxes.Add(new IncomeTax() {
					MinRange = 0,
					MaxRange = 18200,
					MinPayableTax = 0,
					AdditionalTax = 0
			});

			incomeTaxes.Add(new IncomeTax() {
				MinRange = 18201,
				MaxRange = 37000,
				MinPayableTax = 0,
				AdditionalTax = 0.19m
			});

			incomeTaxes.Add(new IncomeTax()	{
				MinRange = 37001,
				MaxRange = 80000,
				MinPayableTax = 3572,
				AdditionalTax = 0.325m
			});

			incomeTaxes.Add(new IncomeTax()	{
				MinRange = 80001,
				MaxRange = 180000,
				MinPayableTax = 17547,
				AdditionalTax = 0.37m
			});

			incomeTaxes.Add(new IncomeTax() {
				MinRange = 180001,
				MaxRange = int.MaxValue,
				MinPayableTax = 54547,
				AdditionalTax = 0.45m
			});

			return incomeTaxes;
		}
	}
}
