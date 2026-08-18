using System;
using System.Runtime.CompilerServices;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020005DD RID: 1501
	[TypeForwardedFrom("System.WorkflowServices, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	public class BasicHttpContextBindingCollectionElement : StandardBindingCollectionElement<BasicHttpContextBinding, BasicHttpContextBindingElement>
	{
		// Token: 0x06003A33 RID: 14899 RVA: 0x000E04A0 File Offset: 0x000DE6A0
		internal static BasicHttpContextBindingCollectionElement GetBindingCollectionElement()
		{
			return (BasicHttpContextBindingCollectionElement)ConfigurationHelpers.GetBindingCollectionElement("basicHttpContextBinding");
		}

		// Token: 0x04002A53 RID: 10835
		internal const string basicHttpContextBindingName = "basicHttpContextBinding";
	}
}
