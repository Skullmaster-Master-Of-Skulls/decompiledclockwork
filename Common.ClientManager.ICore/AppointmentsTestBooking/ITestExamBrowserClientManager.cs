using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.AppointmentsTestBooking
{
	// Token: 0x0200008C RID: 140
	public interface ITestExamBrowserClientManager : IWebService
	{
		// Token: 0x06000436 RID: 1078
		IList<TestExamRowDTO> LoadTestExamRows(DateTime StartDate, DateTime EndDate, bool HideCancelled);

		// Token: 0x06000437 RID: 1079
		TestExamRowDTO LoadTestExamRow(int AppointmentId);
	}
}
