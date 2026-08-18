using System;
using System.ServiceModel.Security;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A26 RID: 2598
	internal class PeerDoNothingSecurityProtocolFactory : SecurityProtocolFactory
	{
		// Token: 0x0600673D RID: 26429 RVA: 0x00181AB6 File Offset: 0x0017FCB6
		protected override SecurityProtocol OnCreateSecurityProtocol(EndpointAddress target, Uri via, object listenerSecurityState, TimeSpan timeout)
		{
			return new PeerDoNothingSecurityProtocol(this);
		}

		// Token: 0x0600673E RID: 26430 RVA: 0x00181ABE File Offset: 0x0017FCBE
		public override void OnAbort()
		{
		}

		// Token: 0x0600673F RID: 26431 RVA: 0x00181AC0 File Offset: 0x0017FCC0
		public override void OnOpen(TimeSpan timeout)
		{
		}

		// Token: 0x06006740 RID: 26432 RVA: 0x00181AC2 File Offset: 0x0017FCC2
		public override void OnClose(TimeSpan timeout)
		{
		}
	}
}
