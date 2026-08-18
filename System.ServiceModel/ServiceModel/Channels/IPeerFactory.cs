using System;
using System.Net;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009F0 RID: 2544
	internal interface IPeerFactory : ITransportFactorySettings, IDefaultCommunicationTimeouts
	{
		// Token: 0x1700184A RID: 6218
		// (get) Token: 0x060064B4 RID: 25780
		IPAddress ListenIPAddress { get; }

		// Token: 0x1700184B RID: 6219
		// (get) Token: 0x060064B5 RID: 25781
		int Port { get; }

		// Token: 0x1700184C RID: 6220
		// (get) Token: 0x060064B6 RID: 25782
		XmlDictionaryReaderQuotas ReaderQuotas { get; }

		// Token: 0x1700184D RID: 6221
		// (get) Token: 0x060064B7 RID: 25783
		PeerResolver Resolver { get; }

		// Token: 0x1700184E RID: 6222
		// (get) Token: 0x060064B8 RID: 25784
		PeerSecurityManager SecurityManager { get; }

		// Token: 0x1700184F RID: 6223
		// (get) Token: 0x060064B9 RID: 25785
		// (set) Token: 0x060064BA RID: 25786
		PeerNodeImplementation PrivatePeerNode { get; set; }

		// Token: 0x17001850 RID: 6224
		// (get) Token: 0x060064BB RID: 25787
		long MaxBufferPoolSize { get; }
	}
}
