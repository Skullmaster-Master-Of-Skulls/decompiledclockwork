using System;
using System.Collections.ObjectModel;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.IdentityModel.Tokens;

namespace System.IdentityModel.Selectors
{
	// Token: 0x020001AF RID: 431
	public class WindowsSecurityTokenAuthenticator : SecurityTokenAuthenticator
	{
		// Token: 0x06000E11 RID: 3601 RVA: 0x0003FE6A File Offset: 0x0003E06A
		public WindowsSecurityTokenAuthenticator() : this(true)
		{
		}

		// Token: 0x06000E12 RID: 3602 RVA: 0x0003FE73 File Offset: 0x0003E073
		public WindowsSecurityTokenAuthenticator(bool includeWindowsGroups)
		{
			this.includeWindowsGroups = includeWindowsGroups;
		}

		// Token: 0x06000E13 RID: 3603 RVA: 0x0003FE82 File Offset: 0x0003E082
		protected override bool CanValidateTokenCore(SecurityToken token)
		{
			return token is WindowsSecurityToken;
		}

		// Token: 0x06000E14 RID: 3604 RVA: 0x0003FE90 File Offset: 0x0003E090
		protected override ReadOnlyCollection<IAuthorizationPolicy> ValidateTokenCore(SecurityToken token)
		{
			WindowsSecurityToken windowsSecurityToken = (WindowsSecurityToken)token;
			WindowsClaimSet claimSet = new WindowsClaimSet(windowsSecurityToken.WindowsIdentity, windowsSecurityToken.AuthenticationType, this.includeWindowsGroups, windowsSecurityToken.ValidTo);
			return SecurityUtils.CreateAuthorizationPolicies(claimSet, windowsSecurityToken.ValidTo);
		}

		// Token: 0x04000CEA RID: 3306
		private bool includeWindowsGroups;
	}
}
