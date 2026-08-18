using System;
using System.Collections.Generic;
using System.IdentityModel.Claims;

namespace System.ServiceModel.Security
{
	// Token: 0x0200035A RID: 858
	internal class ClaimStringValueComparer : IEqualityComparer<Claim>
	{
		// Token: 0x06001F92 RID: 8082 RVA: 0x000764B4 File Offset: 0x000746B4
		public bool Equals(Claim claim1, Claim claim2)
		{
			return claim1 == claim2 || (claim1 != null && claim2 != null && !(claim1.ClaimType != claim2.ClaimType) && !(claim1.Right != claim2.Right) && StringComparer.OrdinalIgnoreCase.Equals(claim1.Resource, claim2.Resource));
		}

		// Token: 0x06001F93 RID: 8083 RVA: 0x00076510 File Offset: 0x00074710
		public int GetHashCode(Claim claim)
		{
			if (claim == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("claim");
			}
			return claim.ClaimType.GetHashCode() ^ claim.Right.GetHashCode() ^ ((claim.Resource == null) ? 0 : claim.Resource.GetHashCode());
		}
	}
}
