using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000064 RID: 100
	internal class ClockWorkServerConnectionClientBaseProxy : ClientBase<IClockWorkServerConnection>, IClockWorkServerConnection, IService
	{
		// Token: 0x0600045D RID: 1117 RVA: 0x0000C6E5 File Offset: 0x0000A8E5
		public ClockWorkServerConnectionClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x0000C6F0 File Offset: 0x0000A8F0
		public ClockWorkServerConnectionClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x0000C6FC File Offset: 0x0000A8FC
		public int CheckConnection()
		{
			return base.Channel.CheckConnection();
		}
	}
}
