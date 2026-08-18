using System;

namespace System.Security.Permissions
{
	// Token: 0x0200073F RID: 1855
	[Serializable]
	public class ResourcePermissionBaseEntry
	{
		// Token: 0x060038A2 RID: 14498 RVA: 0x000EF1E7 File Offset: 0x000EE1E7
		public ResourcePermissionBaseEntry()
		{
			this.permissionAccess = 0;
			this.accessPath = new string[0];
		}

		// Token: 0x060038A3 RID: 14499 RVA: 0x000EF202 File Offset: 0x000EE202
		public ResourcePermissionBaseEntry(int permissionAccess, string[] permissionAccessPath)
		{
			if (permissionAccessPath == null)
			{
				throw new ArgumentNullException("permissionAccessPath");
			}
			this.permissionAccess = permissionAccess;
			this.accessPath = permissionAccessPath;
		}

		// Token: 0x17000D23 RID: 3363
		// (get) Token: 0x060038A4 RID: 14500 RVA: 0x000EF226 File Offset: 0x000EE226
		public int PermissionAccess
		{
			get
			{
				return this.permissionAccess;
			}
		}

		// Token: 0x17000D24 RID: 3364
		// (get) Token: 0x060038A5 RID: 14501 RVA: 0x000EF22E File Offset: 0x000EE22E
		public string[] PermissionAccessPath
		{
			get
			{
				return this.accessPath;
			}
		}

		// Token: 0x04003261 RID: 12897
		private string[] accessPath;

		// Token: 0x04003262 RID: 12898
		private int permissionAccess;
	}
}
