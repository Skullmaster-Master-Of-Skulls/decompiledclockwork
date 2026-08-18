using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.Security.Principal;

namespace System.IdentityModel.Selectors
{
	// Token: 0x0200019F RID: 415
	public class CustomUserNameSecurityTokenAuthenticator : UserNameSecurityTokenAuthenticator
	{
		// Token: 0x06000D8B RID: 3467 RVA: 0x0003EC91 File Offset: 0x0003CE91
		public CustomUserNameSecurityTokenAuthenticator(UserNamePasswordValidator validator)
		{
			if (validator == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("validator");
			}
			this.validator = validator;
		}

		// Token: 0x06000D8C RID: 3468 RVA: 0x0003ECB3 File Offset: 0x0003CEB3
		protected override ReadOnlyCollection<IAuthorizationPolicy> ValidateUserNamePasswordCore(string userName, string password)
		{
			this.validator.Validate(userName, password);
			return SecurityUtils.CreateAuthorizationPolicies(new CustomUserNameSecurityTokenAuthenticator.UserNameClaimSet(userName, this.validator.GetType().Name));
		}

		// Token: 0x04000CD1 RID: 3281
		private UserNamePasswordValidator validator;

		// Token: 0x0200028F RID: 655
		private class UserNameClaimSet : DefaultClaimSet, IIdentityInfo
		{
			// Token: 0x06001346 RID: 4934 RVA: 0x00052678 File Offset: 0x00050878
			public UserNameClaimSet(string userName, string authType) : base(new Claim[0])
			{
				if (userName == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("userName");
				}
				this.identity = SecurityUtils.CreateIdentity(userName, authType);
				List<Claim> list = new List<Claim>(2);
				list.Add(new Claim(ClaimTypes.Name, userName, Rights.Identity));
				list.Add(Claim.CreateNameClaim(userName));
				base.Initialize(ClaimSet.System, list);
			}

			// Token: 0x17000567 RID: 1383
			// (get) Token: 0x06001347 RID: 4935 RVA: 0x000526E6 File Offset: 0x000508E6
			public IIdentity Identity
			{
				get
				{
					return this.identity;
				}
			}

			// Token: 0x0400112D RID: 4397
			private IIdentity identity;
		}
	}
}
