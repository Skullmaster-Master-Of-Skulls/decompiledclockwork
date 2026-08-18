using System;
using System.Collections.ObjectModel;
using System.IdentityModel.Policy;
using System.IdentityModel.Tokens;

namespace System.ServiceModel.Security
{
	// Token: 0x020002C7 RID: 711
	public class SecurityTokenSpecification
	{
		// Token: 0x060016FA RID: 5882 RVA: 0x00057293 File Offset: 0x00055493
		public SecurityTokenSpecification(SecurityToken token, ReadOnlyCollection<IAuthorizationPolicy> tokenPolicies)
		{
			this.token = token;
			if (tokenPolicies == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("tokenPolicies");
			}
			this.tokenPolicies = tokenPolicies;
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x060016FB RID: 5883 RVA: 0x000572BC File Offset: 0x000554BC
		public SecurityToken SecurityToken
		{
			get
			{
				return this.token;
			}
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x060016FC RID: 5884 RVA: 0x000572C4 File Offset: 0x000554C4
		public ReadOnlyCollection<IAuthorizationPolicy> SecurityTokenPolicies
		{
			get
			{
				return this.tokenPolicies;
			}
		}

		// Token: 0x04001C03 RID: 7171
		private SecurityToken token;

		// Token: 0x04001C04 RID: 7172
		private ReadOnlyCollection<IAuthorizationPolicy> tokenPolicies;
	}
}
