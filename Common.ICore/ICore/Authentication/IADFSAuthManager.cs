using System;
using System.Security.Claims;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Authentication.ADFS;
using TechnoPro.Common.Security.Saml;

namespace TechnoPro.Common.ICore.Authentication
{
	// Token: 0x020000D9 RID: 217
	public interface IADFSAuthManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060006CF RID: 1743
		// (set) Token: 0x060006D0 RID: 1744
		AdfsParameters Parameters { get; set; }

		// Token: 0x060006D1 RID: 1745
		bool ValidateToken(string token, out ClaimsPrincipal claimsPrincipal);

		// Token: 0x060006D2 RID: 1746
		string GetSamlResponseFromSamlArtifact(string samlArt, string relyingPartyId, CertificateLocation privateSigningCertLocation, string artifactResolutionServiceUri);
	}
}
