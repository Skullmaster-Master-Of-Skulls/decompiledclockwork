using System;
using System.Runtime.CompilerServices;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020005D9 RID: 1497
	[TypeForwardedFrom("System.WorkflowServices, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	public class NetTcpContextBindingCollectionElement : StandardBindingCollectionElement<NetTcpContextBinding, NetTcpContextBindingElement>
	{
		// Token: 0x06003A17 RID: 14871 RVA: 0x000E008E File Offset: 0x000DE28E
		internal static NetTcpContextBindingCollectionElement GetBindingCollectionElement()
		{
			return (NetTcpContextBindingCollectionElement)ConfigurationHelpers.GetBindingCollectionElement("netTcpContextBinding");
		}

		// Token: 0x04002A4B RID: 10827
		internal const string netTcpContextBindingName = "netTcpContextBinding";
	}
}
