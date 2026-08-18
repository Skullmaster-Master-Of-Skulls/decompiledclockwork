using System;
using System.IdentityModel.Claims;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A24 RID: 2596
	internal class PeerIdentityClaim
	{
		// Token: 0x06006734 RID: 26420 RVA: 0x001819EC File Offset: 0x0017FBEC
		internal static Claim Claim()
		{
			return new Claim("http://schemas.microsoft.com/net/2006/05/peer/peer", "peer", "peer");
		}

		// Token: 0x06006735 RID: 26421 RVA: 0x00181A02 File Offset: 0x0017FC02
		internal static bool IsMatch(EndpointIdentity identity)
		{
			return identity.IdentityClaim.ClaimType == "http://schemas.microsoft.com/net/2006/05/peer/peer";
		}

		// Token: 0x04003B43 RID: 15171
		private const string resourceValue = "peer";

		// Token: 0x04003B44 RID: 15172
		private const string resourceRight = "peer";

		// Token: 0x04003B45 RID: 15173
		public const string PeerClaimType = "http://schemas.microsoft.com/net/2006/05/peer/peer";
	}
}
