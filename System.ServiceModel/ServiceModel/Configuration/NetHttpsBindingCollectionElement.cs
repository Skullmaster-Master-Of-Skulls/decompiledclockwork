using System;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000652 RID: 1618
	public class NetHttpsBindingCollectionElement : StandardBindingCollectionElement<NetHttpsBinding, NetHttpsBindingElement>
	{
		// Token: 0x06003E59 RID: 15961 RVA: 0x000ED940 File Offset: 0x000EBB40
		internal static NetHttpsBindingCollectionElement GetBindingCollectionElement()
		{
			return (NetHttpsBindingCollectionElement)ConfigurationHelpers.GetBindingCollectionElement("netHttpsBinding");
		}
	}
}
