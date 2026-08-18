using System;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000659 RID: 1625
	public class NetNamedPipeBindingCollectionElement : StandardBindingCollectionElement<NetNamedPipeBinding, NetNamedPipeBindingElement>
	{
		// Token: 0x06003EA8 RID: 16040 RVA: 0x000EE8A0 File Offset: 0x000ECAA0
		internal static NetNamedPipeBindingCollectionElement GetBindingCollectionElement()
		{
			return (NetNamedPipeBindingCollectionElement)ConfigurationHelpers.GetBindingCollectionElement("netNamedPipeBinding");
		}
	}
}
