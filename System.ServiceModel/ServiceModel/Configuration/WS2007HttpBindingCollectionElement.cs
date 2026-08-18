using System;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006E8 RID: 1768
	public class WS2007HttpBindingCollectionElement : StandardBindingCollectionElement<WS2007HttpBinding, WS2007HttpBindingElement>
	{
		// Token: 0x06004407 RID: 17415 RVA: 0x00100EA1 File Offset: 0x000FF0A1
		internal static WS2007HttpBindingCollectionElement GetBindingCollectionElement()
		{
			return (WS2007HttpBindingCollectionElement)ConfigurationHelpers.GetBindingCollectionElement("ws2007HttpBinding");
		}
	}
}
