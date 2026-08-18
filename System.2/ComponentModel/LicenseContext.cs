using System;
using System.Reflection;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x0200057D RID: 1405
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class LicenseContext : IServiceProvider
	{
		// Token: 0x17000CBA RID: 3258
		// (get) Token: 0x060033FE RID: 13310 RVA: 0x000E43ED File Offset: 0x000E25ED
		public virtual LicenseUsageMode UsageMode
		{
			get
			{
				return LicenseUsageMode.Runtime;
			}
		}

		// Token: 0x060033FF RID: 13311 RVA: 0x000E43F0 File Offset: 0x000E25F0
		public virtual string GetSavedLicenseKey(Type type, Assembly resourceAssembly)
		{
			return null;
		}

		// Token: 0x06003400 RID: 13312 RVA: 0x000E43F3 File Offset: 0x000E25F3
		public virtual object GetService(Type type)
		{
			return null;
		}

		// Token: 0x06003401 RID: 13313 RVA: 0x000E43F6 File Offset: 0x000E25F6
		public virtual void SetSavedLicenseKey(Type type, string key)
		{
		}
	}
}
