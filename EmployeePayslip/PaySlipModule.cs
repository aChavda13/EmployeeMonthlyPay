using Ninject.Modules;
using EmployeePayslip.Interfaces;
using EmployeePayslip.Services;
using EmployeePayslip.Repositories;

namespace EmployeePayslip
{
	public class PaySlipModule : NinjectModule
	{

		public override void Load()
		{
			Bind<IPaySlipService>().To<PaySlipService>();
			Bind<ITaxCalculatorService>().To<TaxCalculatorService>();
			Bind<ITaxRepository>().To<TaxRepository>();
			Bind<ICsvService>().To<CsvService>();
		}
	}
}