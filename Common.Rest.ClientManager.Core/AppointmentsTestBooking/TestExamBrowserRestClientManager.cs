using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.Common.ClientManager.ICore.AppointmentsTestBooking;
using TechnoPro.Common.Public;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.AppointmentsTestBooking
{
	// Token: 0x0200007C RID: 124
	public class TestExamBrowserRestClientManager : BearerTokenRestProxy<ITestExamBrowserClientManager>, ITestExamBrowserClientManager, IWebService
	{
		// Token: 0x060004D5 RID: 1237 RVA: 0x0000DB01 File Offset: 0x0000BD01
		public TestExamBrowserRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x0000DB0B File Offset: 0x0000BD0B
		public TestExamBrowserRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0000DB16 File Offset: 0x0000BD16
		public IList<TestExamRowDTO> LoadTestExamRows(DateTime StartDate, DateTime EndDate, bool HideCancelled)
		{
			return base.GetMany<TestExamRowDTO>(string.Format("testexambrowser/testexamrows/range/{0}/{1}?hidecancelled={2}", StartDate, EndDate, HideCancelled), true);
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0000DB3B File Offset: 0x0000BD3B
		public TestExamRowDTO LoadTestExamRow(int AppointmentId)
		{
			return base.Get<TestExamRowDTO>(string.Format("testexambrowser/testexamrow/appid/{0}", AppointmentId), true);
		}
	}
}
