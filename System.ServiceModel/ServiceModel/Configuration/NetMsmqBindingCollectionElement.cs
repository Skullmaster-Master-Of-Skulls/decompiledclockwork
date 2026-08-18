using System;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000660 RID: 1632
	public class NetMsmqBindingCollectionElement : StandardBindingCollectionElement<NetMsmqBinding, NetMsmqBindingElement>
	{
		// Token: 0x06003ECC RID: 16076 RVA: 0x000EED38 File Offset: 0x000ECF38
		internal static NetMsmqBindingCollectionElement GetBindingCollectionElement()
		{
			return (NetMsmqBindingCollectionElement)ConfigurationHelpers.GetBindingCollectionElement("netMsmqBinding");
		}
	}
}
