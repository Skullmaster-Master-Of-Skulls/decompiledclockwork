using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x02000647 RID: 1607
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class UrlIdentityPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x060039F7 RID: 14839 RVA: 0x000C2A8F File Offset: 0x000C1A8F
		public UrlIdentityPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		// Token: 0x170009CD RID: 2509
		// (get) Token: 0x060039F8 RID: 14840 RVA: 0x000C2A98 File Offset: 0x000C1A98
		// (set) Token: 0x060039F9 RID: 14841 RVA: 0x000C2AA0 File Offset: 0x000C1AA0
		public string Url
		{
			get
			{
				return this.m_url;
			}
			set
			{
				this.m_url = value;
			}
		}

		// Token: 0x060039FA RID: 14842 RVA: 0x000C2AA9 File Offset: 0x000C1AA9
		public override IPermission CreatePermission()
		{
			if (this.m_unrestricted)
			{
				return new UrlIdentityPermission(PermissionState.Unrestricted);
			}
			if (this.m_url == null)
			{
				return new UrlIdentityPermission(PermissionState.None);
			}
			return new UrlIdentityPermission(this.m_url);
		}

		// Token: 0x04001E1B RID: 7707
		private string m_url;
	}
}
