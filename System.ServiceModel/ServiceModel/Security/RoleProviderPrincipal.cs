using System;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.ServiceModel.Activation;
using System.Web.Security;

namespace System.ServiceModel.Security
{
	// Token: 0x02000335 RID: 821
	internal sealed class RoleProviderPrincipal : IPrincipal
	{
		// Token: 0x06001DC2 RID: 7618 RVA: 0x0006E67D File Offset: 0x0006C87D
		public RoleProviderPrincipal(object roleProvider, ServiceSecurityContext securityContext)
		{
			this.roleProvider = roleProvider;
			this.securityContext = securityContext;
		}

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x06001DC3 RID: 7619 RVA: 0x0006E693 File Offset: 0x0006C893
		public IIdentity Identity
		{
			get
			{
				return this.securityContext.PrimaryIdentity;
			}
		}

		// Token: 0x06001DC4 RID: 7620 RVA: 0x0006E6A0 File Offset: 0x0006C8A0
		[MethodImpl(MethodImplOptions.NoInlining)]
		public bool IsInRole(string role)
		{
			RoleProvider roleProvider = (this.roleProvider as RoleProvider) ?? SystemWebHelper.GetDefaultRoleProvider();
			return roleProvider != null && roleProvider.IsUserInRole(this.securityContext.PrimaryIdentity.Name, role);
		}

		// Token: 0x04001E37 RID: 7735
		private object roleProvider;

		// Token: 0x04001E38 RID: 7736
		private ServiceSecurityContext securityContext;
	}
}
