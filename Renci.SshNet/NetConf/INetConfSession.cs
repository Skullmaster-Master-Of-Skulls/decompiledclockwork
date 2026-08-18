using System;
using System.Xml;

namespace Renci.SshNet.NetConf
{
	// Token: 0x02000095 RID: 149
	internal interface INetConfSession : ISubsystemSession, IDisposable
	{
		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000777 RID: 1911
		XmlDocument ServerCapabilities { get; }

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000778 RID: 1912
		XmlDocument ClientCapabilities { get; }

		// Token: 0x06000779 RID: 1913
		XmlDocument SendReceiveRpc(XmlDocument rpc, bool automaticMessageIdHandling);
	}
}
