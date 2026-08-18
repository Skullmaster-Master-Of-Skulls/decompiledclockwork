using System;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020005ED RID: 1517
	public class BasicHttpBindingCollectionElement : StandardBindingCollectionElement<BasicHttpBinding, BasicHttpBindingElement>
	{
		// Token: 0x06003A82 RID: 14978 RVA: 0x000E0FB4 File Offset: 0x000DF1B4
		internal static BasicHttpBindingCollectionElement GetBindingCollectionElement()
		{
			return (BasicHttpBindingCollectionElement)ConfigurationHelpers.GetBindingCollectionElement("basicHttpBinding");
		}
	}
}
