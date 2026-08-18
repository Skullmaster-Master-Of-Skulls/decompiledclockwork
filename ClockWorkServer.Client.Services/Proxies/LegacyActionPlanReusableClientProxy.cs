using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000C3 RID: 195
	public class LegacyActionPlanReusableClientProxy : WCFTokenBasedReusableClientProxy<ILegacyActionPlan>, ILegacyActionPlan, IService
	{
		// Token: 0x060007C9 RID: 1993 RVA: 0x00014912 File Offset: 0x00012B12
		public LegacyActionPlanReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x0001491D File Offset: 0x00012B1D
		public LegacyActionPlanReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}
	}
}
