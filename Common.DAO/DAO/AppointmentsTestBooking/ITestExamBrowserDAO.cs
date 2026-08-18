using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.TestExamBrowser;

namespace TechnoPro.Common.DAO.AppointmentsTestBooking
{
	// Token: 0x020000BD RID: 189
	public interface ITestExamBrowserDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000518 RID: 1304
		IList<TestExamRow> LoadTestExamRows(DateTime StartDate, DateTime EndDate, bool HideCancelled, eTestExamColumnGroup ColumnsToLoad);

		// Token: 0x06000519 RID: 1305
		TestExamRow LoadTestExamRow(int AppointmentId);
	}
}
