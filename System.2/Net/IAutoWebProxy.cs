using System;

namespace System.Net
{
	// Token: 0x020001DE RID: 478
	internal interface IAutoWebProxy : IWebProxy
	{
		// Token: 0x060012C1 RID: 4801
		ProxyChain GetProxies(Uri destination);
	}
}
