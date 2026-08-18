using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000C4 RID: 196
	internal class LegacyActionPlanClientBaseProxy : ClientBase<ILegacyActionPlan>, ILegacyActionPlan, IService
	{
		// Token: 0x060007CB RID: 1995 RVA: 0x00014929 File Offset: 0x00012B29
		public LegacyActionPlanClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x00014934 File Offset: 0x00012B34
		public LegacyActionPlanClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}
	}
}
