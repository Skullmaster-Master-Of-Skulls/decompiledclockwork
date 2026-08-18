using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentsCalendar;
using TechnoPro.Common.Public.Entities.PerformanceTesting;

namespace TechnoPro.Common.ICore.PerformanceTesting
{
	// Token: 0x0200004D RID: 77
	public interface IPerformanceTestManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060001E6 RID: 486
		SearchForPersonPerformanceTestResult SearchForPersonPerformanceTest(string searchString);

		// Token: 0x060001E7 RID: 487
		PerformanceTestResult LoadAppointmentsPerformanceTest(DateTime StartDate, DateTime EndDate, IList<int> PersonIds, out IList<Appointment> apps);
	}
}
