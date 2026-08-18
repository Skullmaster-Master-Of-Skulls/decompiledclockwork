using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.PerformanceTesting;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.PerformanceTesting
{
	// Token: 0x02000026 RID: 38
	public interface IPerformanceTestClientManager : IWebService
	{
		// Token: 0x060000FB RID: 251
		SearchForPersonPerformanceTestResultDTO SearchForPersonPerformanceTest();

		// Token: 0x060000FC RID: 252
		PerformanceTestResultDTO LoadAppointmentsPerformanceTest(DateTime StartDate, DateTime EndDate, IList<int> PersonIds, out int numAppsReturned);
	}
}
