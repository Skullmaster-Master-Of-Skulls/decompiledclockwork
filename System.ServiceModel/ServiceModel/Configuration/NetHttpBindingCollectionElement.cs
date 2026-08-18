using System;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000650 RID: 1616
	public class NetHttpBindingCollectionElement : StandardBindingCollectionElement<NetHttpBinding, NetHttpBindingElement>
	{
		// Token: 0x06003E4B RID: 15947 RVA: 0x000ED6F4 File Offset: 0x000EB8F4
		internal static NetHttpBindingCollectionElement GetBindingCollectionElement()
		{
			return (NetHttpBindingCollectionElement)ConfigurationHelpers.GetBindingCollectionElement("netHttpBinding");
		}
	}
}
