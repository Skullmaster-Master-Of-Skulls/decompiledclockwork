using System;
using System.IdentityModel.Policy;
using System.ServiceModel.Security;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A27 RID: 2599
	internal class PeerIdentityVerifier : IdentityVerifier
	{
		// Token: 0x06006743 RID: 26435 RVA: 0x00181AD4 File Offset: 0x0017FCD4
		public override bool CheckAccess(EndpointIdentity identity, AuthorizationContext authContext)
		{
			return true;
		}

		// Token: 0x06006744 RID: 26436 RVA: 0x00181AD7 File Offset: 0x0017FCD7
		public override bool TryGetIdentity(EndpointAddress reference, out EndpointIdentity identity)
		{
			if (reference == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reference");
			}
			identity = reference.Identity;
			if (identity == null)
			{
				identity = new PeerEndpointIdentity();
			}
			return true;
		}
	}
}
