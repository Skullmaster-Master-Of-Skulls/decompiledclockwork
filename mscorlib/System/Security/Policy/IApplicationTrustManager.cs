using System;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	// Token: 0x020004A7 RID: 1191
	[ComVisible(true)]
	public interface IApplicationTrustManager : ISecurityEncodable
	{
		// Token: 0x06002F39 RID: 12089
		ApplicationTrust DetermineApplicationTrust(ActivationContext activationContext, TrustManagerContext context);
	}
}
