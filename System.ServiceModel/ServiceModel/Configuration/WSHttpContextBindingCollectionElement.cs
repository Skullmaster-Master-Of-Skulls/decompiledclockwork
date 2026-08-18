using System;
using System.Runtime.CompilerServices;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020005DB RID: 1499
	[TypeForwardedFrom("System.WorkflowServices, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	public class WSHttpContextBindingCollectionElement : StandardBindingCollectionElement<WSHttpContextBinding, WSHttpContextBindingElement>
	{
		// Token: 0x06003A25 RID: 14885 RVA: 0x000E0298 File Offset: 0x000DE498
		internal static WSHttpContextBindingCollectionElement GetBindingCollectionElement()
		{
			return (WSHttpContextBindingCollectionElement)ConfigurationHelpers.GetBindingCollectionElement("wsHttpContextBinding");
		}

		// Token: 0x04002A4F RID: 10831
		internal const string wsHttpContextBindingName = "wsHttpContextBinding";
	}
}
