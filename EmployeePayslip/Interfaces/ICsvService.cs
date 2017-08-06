using System.Collections.Generic;
using EmployeePayslip.Entities;

namespace EmployeePayslip.Interfaces
{
	public interface ICsvService
	{
		List<Employee> GetEmployeeList(string filePath);
		void ExportPayslip(List<Payslip> paySlips, string filePath);
	}
}
