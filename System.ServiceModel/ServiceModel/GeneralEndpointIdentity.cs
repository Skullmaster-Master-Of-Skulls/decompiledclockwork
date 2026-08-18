using System;
using System.IdentityModel.Claims;

namespace System.ServiceModel
{
	// Token: 0x020000BF RID: 191
	internal class GeneralEndpointIdentity : EndpointIdentity
	{
		// Token: 0x0600034D RID: 845 RVA: 0x000132D4 File Offset: 0x000114D4
		public GeneralEndpointIdentity(Claim identityClaim)
		{
			base.Initialize(identityClaim);
		}
	}
}
