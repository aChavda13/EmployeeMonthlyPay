namespace EmployeePayslip.Interfaces
{
	public interface ITaxCalculatorService
	{
		decimal CalculateIncomeTax(decimal annualSalary);
	}
}
