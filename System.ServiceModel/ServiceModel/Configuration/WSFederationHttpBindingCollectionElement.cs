using System;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006E4 RID: 1764
	public class WSFederationHttpBindingCollectionElement : StandardBindingCollectionElement<WSFederationHttpBinding, WSFederationHttpBindingElement>
	{
		// Token: 0x060043FE RID: 17406 RVA: 0x00100E38 File Offset: 0x000FF038
		internal static WSFederationHttpBindingCollectionElement GetBindingCollectionElement()
		{
			return (WSFederationHttpBindingCollectionElement)ConfigurationHelpers.GetBindingCollectionElement("wsFederationHttpBinding");
		}
	}
}
