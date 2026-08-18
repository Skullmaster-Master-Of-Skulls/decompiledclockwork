using System;
using System.Security.Permissions;

namespace System.IO
{
	// Token: 0x02000404 RID: 1028
	public class RenamedEventArgs : FileSystemEventArgs
	{
		// Token: 0x060026A8 RID: 9896 RVA: 0x000B2219 File Offset: 0x000B0419
		public RenamedEventArgs(WatcherChangeTypes changeType, string directory, string name, string oldName) : base(changeType, directory, name)
		{
			if (!directory.EndsWith("\\", StringComparison.Ordinal))
			{
				directory += "\\";
			}
			this.oldName = oldName;
			this.oldFullPath = directory + oldName;
		}

		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x060026A9 RID: 9897 RVA: 0x000B2255 File Offset: 0x000B0455
		public string OldFullPath
		{
			get
			{
				new FileIOPermission(FileIOPermissionAccess.Read, Path.GetPathRoot(this.oldFullPath)).Demand();
				return this.oldFullPath;
			}
		}

		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x060026AA RID: 9898 RVA: 0x000B2273 File Offset: 0x000B0473
		public string OldName
		{
			get
			{
				return this.oldName;
			}
		}

		// Token: 0x040020EA RID: 8426
		private string oldName;

		// Token: 0x040020EB RID: 8427
		private string oldFullPath;
	}
}
