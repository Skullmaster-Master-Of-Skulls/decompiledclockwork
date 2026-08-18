using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x02000644 RID: 1604
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class ZoneIdentityPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x060039E7 RID: 14823 RVA: 0x000C2933 File Offset: 0x000C1933
		public ZoneIdentityPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x060039E8 RID: 14824 RVA: 0x000C2943 File Offset: 0x000C1943
		// (set) Token: 0x060039E9 RID: 14825 RVA: 0x000C294B File Offset: 0x000C194B
		public SecurityZone Zone
		{
			get
			{
				return this.m_flag;
			}
			set
			{
				this.m_flag = value;
			}
		}

		// Token: 0x060039EA RID: 14826 RVA: 0x000C2954 File Offset: 0x000C1954
		public override IPermission CreatePermission()
		{
			if (this.m_unrestricted)
			{
				return new ZoneIdentityPermission(PermissionState.Unrestricted);
			}
			return new ZoneIdentityPermission(this.m_flag);
		}

		// Token: 0x04001E16 RID: 7702
		private SecurityZone m_flag = SecurityZone.NoZone;
	}
}
