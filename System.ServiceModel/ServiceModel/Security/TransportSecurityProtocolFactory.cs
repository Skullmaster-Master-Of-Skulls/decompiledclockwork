using System;

namespace System.ServiceModel.Security
{
	// Token: 0x020002D2 RID: 722
	internal class TransportSecurityProtocolFactory : SecurityProtocolFactory
	{
		// Token: 0x060017A1 RID: 6049 RVA: 0x0005A3BA File Offset: 0x000585BA
		public TransportSecurityProtocolFactory()
		{
		}

		// Token: 0x060017A2 RID: 6050 RVA: 0x0005A3C2 File Offset: 0x000585C2
		internal TransportSecurityProtocolFactory(TransportSecurityProtocolFactory factory) : base(factory)
		{
		}

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x060017A3 RID: 6051 RVA: 0x0005A3CB File Offset: 0x000585CB
		public override bool SupportsDuplex
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x060017A4 RID: 6052 RVA: 0x0005A3CE File Offset: 0x000585CE
		public override bool SupportsReplayDetection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060017A5 RID: 6053 RVA: 0x0005A3D1 File Offset: 0x000585D1
		protected override SecurityProtocol OnCreateSecurityProtocol(EndpointAddress target, Uri via, object listenerSecurityState, TimeSpan timeout)
		{
			return new TransportSecurityProtocol(this, target, via);
		}
	}
}
