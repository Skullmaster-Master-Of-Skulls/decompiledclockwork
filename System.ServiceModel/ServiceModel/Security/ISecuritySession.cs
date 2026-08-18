using System;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Security
{
	// Token: 0x02000313 RID: 787
	public interface ISecuritySession : ISession
	{
		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x06001B2D RID: 6957
		EndpointIdentity RemoteIdentity { get; }
	}
}
