using System;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006E7 RID: 1767
	public class WSHttpBindingCollectionElement : StandardBindingCollectionElement<WSHttpBinding, WSHttpBindingElement>
	{
		// Token: 0x06004405 RID: 17413 RVA: 0x00100E88 File Offset: 0x000FF088
		internal static WSHttpBindingCollectionElement GetBindingCollectionElement()
		{
			return (WSHttpBindingCollectionElement)ConfigurationHelpers.GetBindingCollectionElement("wsHttpBinding");
		}
	}
}
