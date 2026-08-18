using System;
using System.IdentityModel.Claims;

namespace System.IdentityModel.Policy
{
	// Token: 0x020001BB RID: 443
	public interface IAuthorizationPolicy : IAuthorizationComponent
	{
		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000E5C RID: 3676
		ClaimSet Issuer { get; }

		// Token: 0x06000E5D RID: 3677
		bool Evaluate(EvaluationContext evaluationContext, ref object state);
	}
}
