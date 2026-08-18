using System;
using System.Security.Permissions;

namespace System.IO
{
	// Token: 0x02000731 RID: 1841
	public class RenamedEventArgs : FileSystemEventArgs
	{
		// Token: 0x06003842 RID: 14402 RVA: 0x000EDA0F File Offset: 0x000ECA0F
		public RenamedEventArgs(WatcherChangeTypes changeType, string directory, string name, string oldName) : base(changeType, directory, name)
		{
			if (!directory.EndsWith("\\", StringComparison.Ordinal))
			{
				directory += "\\";
			}
			this.oldName = oldName;
			this.oldFullPath = directory + oldName;
		}

		// Token: 0x17000D10 RID: 3344
		// (get) Token: 0x06003843 RID: 14403 RVA: 0x000EDA4B File Offset: 0x000ECA4B
		public string OldFullPath
		{
			get
			{
				new FileIOPermission(FileIOPermissionAccess.Read, Path.GetPathRoot(this.oldFullPath)).Demand();
				return this.oldFullPath;
			}
		}

		// Token: 0x17000D11 RID: 3345
		// (get) Token: 0x06003844 RID: 14404 RVA: 0x000EDA69 File Offset: 0x000ECA69
		public string OldName
		{
			get
			{
				return this.oldName;
			}
		}

		// Token: 0x04003235 RID: 12853
		private string oldName;

		// Token: 0x04003236 RID: 12854
		private string oldFullPath;
	}
}
