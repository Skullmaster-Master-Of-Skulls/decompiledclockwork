using System;
using System.Runtime.Remoting.Proxies;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x02000225 RID: 549
	internal interface ICreateServiceChannel
	{
		// Token: 0x06001094 RID: 4244
		RealProxy CreateChannel();
	}
}
