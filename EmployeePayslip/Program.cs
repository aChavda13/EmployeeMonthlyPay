using System;
using System.Collections.Generic;
using Ninject;
using EmployeePayslip.Entities;
using EmployeePayslip.Interfaces;


namespace EmployeePayslip
{
	class Program
	{
		private static IPaySlipService _paySlipService;
		private static ICsvService _csvService;

		static void Main(string[] args)
		{
			try
			{
				RegisterServices();
				var filePath = AppDomain.CurrentDomain.BaseDirectory + "Employee.csv";
				var employeeInfos = _csvService.GetEmployeeList(filePath);
				if (employeeInfos.Count <= 0)
					return;

				var paySlips = new List<Payslip>();

				foreach (var item in employeeInfos)
				{
					var paySlip = _paySlipService.GeneratePaySlip(item);
					paySlips.Add(paySlip);
				}
				filePath = AppDomain.CurrentDomain.BaseDirectory + "EmployeeMonthyPayslip.csv";
				_csvService.ExportPayslip(paySlips, filePath);
			}
			catch (Exception ex)
			{
				Console.WriteLine("Failed to generate payslip", ex);
				Console.ReadLine();
			}
		}

		private static void RegisterServices()
		{
			StandardKernel kernel = new StandardKernel();
			kernel.Load(new PaySlipModule());

			_paySlipService = kernel.Get<IPaySlipService>();
			_csvService = kernel.Get<ICsvService>();
		}
	}
}
