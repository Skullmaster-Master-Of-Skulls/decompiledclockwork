using System;
using System.Security;
using System.Security.Permissions;

namespace System.Configuration
{
	// Token: 0x02000030 RID: 48
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class ConfigurationPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x06000245 RID: 581 RVA: 0x00010856 File Offset: 0x0000EA56
		public ConfigurationPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00010860 File Offset: 0x0000EA60
		public override IPermission CreatePermission()
		{
			PermissionState state = base.Unrestricted ? PermissionState.Unrestricted : PermissionState.None;
			return new ConfigurationPermission(state);
		}
	}
}
