using EmployeePayslip.Entities;
using System.Collections.Generic;

namespace EmployeePayslip.Interfaces
{
	public interface ITaxRepository
	{
		List<IncomeTax> GetIncomeTaxCriteria();
	}
}
