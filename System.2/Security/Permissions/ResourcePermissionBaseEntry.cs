using System;

namespace System.Security.Permissions
{
	// Token: 0x0200048B RID: 1163
	[Serializable]
	public class ResourcePermissionBaseEntry
	{
		// Token: 0x06002B34 RID: 11060 RVA: 0x000C4CC9 File Offset: 0x000C2EC9
		public ResourcePermissionBaseEntry()
		{
			this.permissionAccess = 0;
			this.accessPath = new string[0];
		}

		// Token: 0x06002B35 RID: 11061 RVA: 0x000C4CE4 File Offset: 0x000C2EE4
		public ResourcePermissionBaseEntry(int permissionAccess, string[] permissionAccessPath)
		{
			if (permissionAccessPath == null)
			{
				throw new ArgumentNullException("permissionAccessPath");
			}
			this.permissionAccess = permissionAccess;
			this.accessPath = permissionAccessPath;
		}

		// Token: 0x17000A73 RID: 2675
		// (get) Token: 0x06002B36 RID: 11062 RVA: 0x000C4D08 File Offset: 0x000C2F08
		public int PermissionAccess
		{
			get
			{
				return this.permissionAccess;
			}
		}

		// Token: 0x17000A74 RID: 2676
		// (get) Token: 0x06002B37 RID: 11063 RVA: 0x000C4D10 File Offset: 0x000C2F10
		public string[] PermissionAccessPath
		{
			get
			{
				return this.accessPath;
			}
		}

		// Token: 0x04002678 RID: 9848
		private string[] accessPath;

		// Token: 0x04002679 RID: 9849
		private int permissionAccess;
	}
}
