using EmployeePayslip.Entities;

namespace EmployeePayslip.Interfaces
{
	public interface IPaySlipService
	{
		Payslip GeneratePaySlip(Employee employeeInfo);
	}
}
