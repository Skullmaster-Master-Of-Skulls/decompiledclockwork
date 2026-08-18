using System;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000657 RID: 1623
	[Obsolete("PeerChannel feature is obsolete and will be removed in the future.", false)]
	public class NetPeerTcpBindingCollectionElement : StandardBindingCollectionElement<NetPeerTcpBinding, NetPeerTcpBindingElement>
	{
		// Token: 0x06003E8E RID: 16014 RVA: 0x000EE35C File Offset: 0x000EC55C
		internal static NetPeerTcpBindingCollectionElement GetBindingCollectionElement()
		{
			return (NetPeerTcpBindingCollectionElement)ConfigurationHelpers.GetBindingCollectionElement("netPeerTcpBinding");
		}
	}
}
