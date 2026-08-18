using System;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000663 RID: 1635
	public class NetTcpBindingCollectionElement : StandardBindingCollectionElement<NetTcpBinding, NetTcpBindingElement>
	{
		// Token: 0x06003EF3 RID: 16115 RVA: 0x000EF548 File Offset: 0x000ED748
		internal static NetTcpBindingCollectionElement GetBindingCollectionElement()
		{
			return (NetTcpBindingCollectionElement)ConfigurationHelpers.GetBindingCollectionElement("netTcpBinding");
		}
	}
}
