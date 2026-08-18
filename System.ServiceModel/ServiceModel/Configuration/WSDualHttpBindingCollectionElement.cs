using System;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006E9 RID: 1769
	public class WSDualHttpBindingCollectionElement : StandardBindingCollectionElement<WSDualHttpBinding, WSDualHttpBindingElement>
	{
		// Token: 0x06004409 RID: 17417 RVA: 0x00100EBA File Offset: 0x000FF0BA
		internal static WSDualHttpBindingCollectionElement GetBindingCollectionElement()
		{
			return (WSDualHttpBindingCollectionElement)ConfigurationHelpers.GetBindingCollectionElement("wsDualHttpBinding");
		}
	}
}
