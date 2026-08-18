using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000063 RID: 99
	public class ClockWorkServerConnectionReusableClientProxy : WCFReusableClientProxy<IClockWorkServerConnection>, IClockWorkServerConnection, IService
	{
		// Token: 0x06000459 RID: 1113 RVA: 0x0000C69A File Offset: 0x0000A89A
		public ClockWorkServerConnectionReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0000C6A5 File Offset: 0x0000A8A5
		public ClockWorkServerConnectionReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0000C6B4 File Offset: 0x0000A8B4
		public int CheckConnection()
		{
			return this.WrapServiceMethod<int>(() => base.Proxy.CheckConnection());
		}
	}
}
