using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar;
using TechnoPro.ClockWorkServer.Contracts.DTO.PerformanceTesting;
using TechnoPro.Common.ClientManager.ICore.PerformanceTesting;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.PerformanceTesting
{
	// Token: 0x02000022 RID: 34
	public class PerformanceTestRestClientManager : BearerTokenRestProxy<IPerformanceTestClientManager>, IPerformanceTestClientManager, IWebService
	{
		// Token: 0x0600012F RID: 303 RVA: 0x0000517D File Offset: 0x0000337D
		public PerformanceTestRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00005187 File Offset: 0x00003387
		public PerformanceTestRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00005192 File Offset: 0x00003392
		public SearchForPersonPerformanceTestResultDTO SearchForPersonPerformanceTest()
		{
			return base.Get<SearchForPersonPerformanceTestResultDTO>("performancetesting/searchforperson", true);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x000051A0 File Offset: 0x000033A0
		public PerformanceTestResultDTO LoadAppointmentsPerformanceTest(DateTime StartDate, DateTime EndDate, IList<int> PersonIds, out int numAppsReturned)
		{
			LoadAppointmentsPerformanceTestResp loadAppointmentsPerformanceTestResp = base.Get<LoadAppointmentsPerformanceTestResp>(string.Format("performancetesting/appointments/personids/{0}/range/{1}/{2}", PersonIds.CommaSeparatedValuesWithoutSpace<int>(), StartDate, EndDate), true);
			IList<AppointmentDTO> appointments = loadAppointmentsPerformanceTestResp.Appointments;
			numAppsReturned = ((appointments != null) ? appointments.Count : 0);
			return loadAppointmentsPerformanceTestResp.Result;
		}
	}
}
