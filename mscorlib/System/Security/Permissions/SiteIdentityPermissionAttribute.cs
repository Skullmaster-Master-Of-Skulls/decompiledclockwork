using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x02000646 RID: 1606
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class SiteIdentityPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x060039F3 RID: 14835 RVA: 0x000C2A4A File Offset: 0x000C1A4A
		public SiteIdentityPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x060039F4 RID: 14836 RVA: 0x000C2A53 File Offset: 0x000C1A53
		// (set) Token: 0x060039F5 RID: 14837 RVA: 0x000C2A5B File Offset: 0x000C1A5B
		public string Site
		{
			get
			{
				return this.m_site;
			}
			set
			{
				this.m_site = value;
			}
		}

		// Token: 0x060039F6 RID: 14838 RVA: 0x000C2A64 File Offset: 0x000C1A64
		public override IPermission CreatePermission()
		{
			if (this.m_unrestricted)
			{
				return new SiteIdentityPermission(PermissionState.Unrestricted);
			}
			if (this.m_site == null)
			{
				return new SiteIdentityPermission(PermissionState.None);
			}
			return new SiteIdentityPermission(this.m_site);
		}

		// Token: 0x04001E1A RID: 7706
		private string m_site;
	}
}
