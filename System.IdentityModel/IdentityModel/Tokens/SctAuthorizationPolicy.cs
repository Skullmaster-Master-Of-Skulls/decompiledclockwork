using System;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000165 RID: 357
	internal class SctAuthorizationPolicy : IAuthorizationPolicy, IAuthorizationComponent
	{
		// Token: 0x06000B4A RID: 2890 RVA: 0x0003633D File Offset: 0x0003453D
		internal SctAuthorizationPolicy(Claim claim)
		{
			this._issuer = new DefaultClaimSet(new Claim[]
			{
				claim
			});
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x00036365 File Offset: 0x00034565
		bool IAuthorizationPolicy.Evaluate(EvaluationContext evaluationContext, ref object state)
		{
			if (evaluationContext == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("evaluationContext");
			}
			evaluationContext.AddClaimSet(this, this._issuer);
			return true;
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000B4C RID: 2892 RVA: 0x00036388 File Offset: 0x00034588
		ClaimSet IAuthorizationPolicy.Issuer
		{
			get
			{
				return this._issuer;
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000B4D RID: 2893 RVA: 0x00036390 File Offset: 0x00034590
		string IAuthorizationComponent.Id
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x04000BF8 RID: 3064
		private ClaimSet _issuer;

		// Token: 0x04000BF9 RID: 3065
		private string _id = UniqueId.CreateUniqueId();
	}
}
