namespace EmployeePayslip.Entities
{
	public class IncomeTax
	{
		public int MinRange { get; set; }
		public int MaxRange { get; set; }
		public decimal MinPayableTax { get; set; }
		public decimal AdditionalTax { get; set; }
	}
}
