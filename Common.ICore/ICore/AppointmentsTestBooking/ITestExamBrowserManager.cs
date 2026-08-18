using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.TestExamBrowser;

namespace TechnoPro.Common.ICore.AppointmentsTestBooking
{
	// Token: 0x020000CB RID: 203
	public interface ITestExamBrowserManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600062E RID: 1582
		IList<TestExamRow> LoadTestExamRows(DateTime StartDate, DateTime EndDate, bool HideCancelled, eTestExamColumnGroup ColumnsToLoad);

		// Token: 0x0600062F RID: 1583
		TestExamRow LoadTestExamRow(int AppointmentId);
	}
}
