using System;
using System.Security.Permissions;

namespace System.Security
{
	// Token: 0x02000695 RID: 1685
	[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
	[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
	public abstract class SecurityState
	{
		// Token: 0x06003D17 RID: 15639 RVA: 0x000D1178 File Offset: 0x000D0178
		public bool IsStateAvailable()
		{
			AppDomainManager currentAppDomainManager = AppDomainManager.CurrentAppDomainManager;
			return currentAppDomainManager != null && currentAppDomainManager.CheckSecuritySettings(this);
		}

		// Token: 0x06003D18 RID: 15640
		public abstract void EnsureState();
	}
}
