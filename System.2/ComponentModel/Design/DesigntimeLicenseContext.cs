using System;
using System.Collections;
using System.Reflection;
using System.Security.Permissions;

namespace System.ComponentModel.Design
{
	// Token: 0x020005DA RID: 1498
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class DesigntimeLicenseContext : LicenseContext
	{
		// Token: 0x17000D68 RID: 3432
		// (get) Token: 0x060037BC RID: 14268 RVA: 0x000F0CDE File Offset: 0x000EEEDE
		public override LicenseUsageMode UsageMode
		{
			get
			{
				return LicenseUsageMode.Designtime;
			}
		}

		// Token: 0x060037BD RID: 14269 RVA: 0x000F0CE1 File Offset: 0x000EEEE1
		public override string GetSavedLicenseKey(Type type, Assembly resourceAssembly)
		{
			return null;
		}

		// Token: 0x060037BE RID: 14270 RVA: 0x000F0CE4 File Offset: 0x000EEEE4
		public override void SetSavedLicenseKey(Type type, string key)
		{
			this.savedLicenseKeys[type.AssemblyQualifiedName] = key;
		}

		// Token: 0x04002B03 RID: 11011
		internal Hashtable savedLicenseKeys = new Hashtable();
	}
}
