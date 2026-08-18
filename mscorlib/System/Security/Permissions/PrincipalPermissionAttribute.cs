using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x0200063F RID: 1599
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class PrincipalPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x0600399D RID: 14749 RVA: 0x000C2344 File Offset: 0x000C1344
		public PrincipalPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x0600399E RID: 14750 RVA: 0x000C2354 File Offset: 0x000C1354
		// (set) Token: 0x0600399F RID: 14751 RVA: 0x000C235C File Offset: 0x000C135C
		public string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x060039A0 RID: 14752 RVA: 0x000C2365 File Offset: 0x000C1365
		// (set) Token: 0x060039A1 RID: 14753 RVA: 0x000C236D File Offset: 0x000C136D
		public string Role
		{
			get
			{
				return this.m_role;
			}
			set
			{
				this.m_role = value;
			}
		}

		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x060039A2 RID: 14754 RVA: 0x000C2376 File Offset: 0x000C1376
		// (set) Token: 0x060039A3 RID: 14755 RVA: 0x000C237E File Offset: 0x000C137E
		public bool Authenticated
		{
			get
			{
				return this.m_authenticated;
			}
			set
			{
				this.m_authenticated = value;
			}
		}

		// Token: 0x060039A4 RID: 14756 RVA: 0x000C2387 File Offset: 0x000C1387
		public override IPermission CreatePermission()
		{
			if (this.m_unrestricted)
			{
				return new PrincipalPermission(PermissionState.Unrestricted);
			}
			return new PrincipalPermission(this.m_name, this.m_role, this.m_authenticated);
		}

		// Token: 0x04001E0A RID: 7690
		private string m_name;

		// Token: 0x04001E0B RID: 7691
		private string m_role;

		// Token: 0x04001E0C RID: 7692
		private bool m_authenticated = true;
	}
}
