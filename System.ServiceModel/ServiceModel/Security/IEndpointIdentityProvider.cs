using System;
using System.IdentityModel.Selectors;

namespace System.ServiceModel.Security
{
	// Token: 0x02000337 RID: 823
	public interface IEndpointIdentityProvider
	{
		// Token: 0x06001DCD RID: 7629
		EndpointIdentity GetIdentityOfSelf(SecurityTokenRequirement tokenRequirement);
	}
}
