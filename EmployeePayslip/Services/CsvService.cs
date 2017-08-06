using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using EmployeePayslip.Entities;
using EmployeePayslip.Interfaces;

namespace EmployeePayslip.Services
{
	public class CsvService : ICsvService
	{
		public List<Employee> GetEmployeeList(string filePath)
		{
			List<Employee> results = new List<Employee>();
			try
			{
				using (var fs = File.OpenRead(filePath))
				using (var reader = new StreamReader(fs))
				{
					while (!reader.EndOfStream)
					{
						var line = reader.ReadLine();
						var values = line.Split(',');
						results.Add(new Employee()
						{
							FirstName = values[0],
							LastName = values[1],
							AnnualSalary = decimal.Parse(values[2]),
							SuperRate = values[3],
							PayPeriod = values[4]
						});
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}

			return results;
		}

		public void ExportPayslip(List<Payslip> paySlips, string filePath)
		{
			var csv = new StringBuilder();

			try
			{
				foreach (var item in paySlips)
				{
					csv.AppendLine(string.Format("{0},{1},{2},{3},{4},{5}", item.FullName, item.PayPeriod, item.GrossIncome, item.IncomeTax, item.NetIncome, item.Super));
				}

				File.WriteAllText(filePath, csv.ToString());
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
		}
	}
}
