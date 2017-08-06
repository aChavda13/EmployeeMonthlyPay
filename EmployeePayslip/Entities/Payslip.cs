using System;

namespace EmployeePayslip.Entities
{
	public class Payslip : Employee
	{
		public string FullName
		{
			get
			{
				return FirstName + " " + LastName;
			}
		}

		public decimal GrossIncome
		{
			get
			{
				return Math.Floor(AnnualSalary / 12);
			}
		}		

		public decimal IncomeTax { get; set; }
		public decimal NetIncome { get; set; }
		public decimal Super { get; set; }
	}
}
