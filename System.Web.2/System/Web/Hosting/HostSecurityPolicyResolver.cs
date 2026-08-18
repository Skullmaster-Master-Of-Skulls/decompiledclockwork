using System;
using System.Security.Permissions;
using System.Security.Policy;

namespace System.Web.Hosting
{
	// Token: 0x020007A5 RID: 1957
	[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
	public class HostSecurityPolicyResolver
	{
		// Token: 0x06005CC7 RID: 23751 RVA: 0x00007722 File Offset: 0x00005922
		public virtual HostSecurityPolicyResults ResolvePolicy(Evidence evidence)
		{
			return HostSecurityPolicyResults.DefaultPolicy;
		}
	}
}
