using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x0200065B RID: 1627
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class GacIdentityPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x06003AB6 RID: 15030 RVA: 0x000C649F File Offset: 0x000C549F
		public GacIdentityPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		// Token: 0x06003AB7 RID: 15031 RVA: 0x000C64A8 File Offset: 0x000C54A8
		public override IPermission CreatePermission()
		{
			return new GacIdentityPermission();
		}
	}
}
