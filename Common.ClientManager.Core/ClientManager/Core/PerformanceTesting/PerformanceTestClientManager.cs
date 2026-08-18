using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.PerformanceTesting;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.PerformanceTesting;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.PerformanceTesting
{
	// Token: 0x0200002A RID: 42
	public class PerformanceTestClientManager : IPerformanceTestClientManager, IWebService
	{
		// Token: 0x0600015F RID: 351 RVA: 0x00007864 File Offset: 0x00005A64
		public SearchForPersonPerformanceTestResultDTO SearchForPersonPerformanceTest()
		{
			SearchForPersonPerformanceTestReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SearchForPersonPerformanceTestReq>();
			return ClientServiceFactory.GetClientInstance<IPerformanceTest>().SearchForPersonPerformanceTest(request).Result;
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00007894 File Offset: 0x00005A94
		public PerformanceTestResultDTO LoadAppointmentsPerformanceTest(DateTime StartDate, DateTime EndDate, IList<int> PersonIds, out int numAppsReturned)
		{
			LoadAppointmentsPerformanceTestReq loadAppointmentsPerformanceTestReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAppointmentsPerformanceTestReq>();
			loadAppointmentsPerformanceTestReq.StartDate = StartDate;
			loadAppointmentsPerformanceTestReq.EndDate = EndDate;
			loadAppointmentsPerformanceTestReq.PersonIds = PersonIds;
			LoadAppointmentsPerformanceTestResp loadAppointmentsPerformanceTestResp = ClientServiceFactory.GetClientInstance<IPerformanceTest>().LoadAppointmentsPerformanceTest(loadAppointmentsPerformanceTestReq);
			numAppsReturned = ((loadAppointmentsPerformanceTestResp.Appointments == null) ? 0 : loadAppointmentsPerformanceTestResp.Appointments.Count);
			return loadAppointmentsPerformanceTestResp.Result;
		}
	}
}
