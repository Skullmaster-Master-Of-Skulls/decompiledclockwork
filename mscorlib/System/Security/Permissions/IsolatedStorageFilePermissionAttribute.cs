using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x0200064A RID: 1610
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class IsolatedStorageFilePermissionAttribute : IsolatedStoragePermissionAttribute
	{
		// Token: 0x06003A08 RID: 14856 RVA: 0x000C2BC5 File Offset: 0x000C1BC5
		public IsolatedStorageFilePermissionAttribute(SecurityAction action) : base(action)
		{
		}

		// Token: 0x06003A09 RID: 14857 RVA: 0x000C2BD0 File Offset: 0x000C1BD0
		public override IPermission CreatePermission()
		{
			IsolatedStorageFilePermission isolatedStorageFilePermission;
			if (this.m_unrestricted)
			{
				isolatedStorageFilePermission = new IsolatedStorageFilePermission(PermissionState.Unrestricted);
			}
			else
			{
				isolatedStorageFilePermission = new IsolatedStorageFilePermission(PermissionState.None);
				isolatedStorageFilePermission.UserQuota = this.m_userQuota;
				isolatedStorageFilePermission.UsageAllowed = this.m_allowed;
			}
			return isolatedStorageFilePermission;
		}
	}
}
