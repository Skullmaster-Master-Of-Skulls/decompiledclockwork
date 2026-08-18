using System;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020005F1 RID: 1521
	public class BasicHttpsBindingCollectionElement : StandardBindingCollectionElement<BasicHttpsBinding, BasicHttpsBindingElement>
	{
		// Token: 0x06003A9D RID: 15005 RVA: 0x000E1430 File Offset: 0x000DF630
		internal static BasicHttpsBindingCollectionElement GetBindingCollectionElement()
		{
			return (BasicHttpsBindingCollectionElement)ConfigurationHelpers.GetBindingCollectionElement("basicHttpsBinding");
		}
	}
}
