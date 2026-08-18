using System;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006E5 RID: 1765
	public class WS2007FederationHttpBindingCollectionElement : StandardBindingCollectionElement<WS2007FederationHttpBinding, WS2007FederationHttpBindingElement>
	{
		// Token: 0x06004400 RID: 17408 RVA: 0x00100E51 File Offset: 0x000FF051
		internal static WS2007FederationHttpBindingCollectionElement GetBindingCollectionElement()
		{
			return (WS2007FederationHttpBindingCollectionElement)ConfigurationHelpers.GetBindingCollectionElement("ws2007FederationHttpBinding");
		}
	}
}
