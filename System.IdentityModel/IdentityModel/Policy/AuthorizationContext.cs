using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Claims;

namespace System.IdentityModel.Policy
{
	// Token: 0x020001B6 RID: 438
	public abstract class AuthorizationContext : IAuthorizationComponent
	{
		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000E42 RID: 3650
		public abstract string Id { get; }

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000E43 RID: 3651
		public abstract ReadOnlyCollection<ClaimSet> ClaimSets { get; }

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000E44 RID: 3652
		public abstract DateTime ExpirationTime { get; }

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000E45 RID: 3653
		public abstract IDictionary<string, object> Properties { get; }

		// Token: 0x06000E46 RID: 3654 RVA: 0x000416BA File Offset: 0x0003F8BA
		public static AuthorizationContext CreateDefaultAuthorizationContext(IList<IAuthorizationPolicy> authorizationPolicies)
		{
			return SecurityUtils.CreateDefaultAuthorizationContext(authorizationPolicies);
		}
	}
}
