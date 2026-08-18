using System;

namespace System.IO
{
	// Token: 0x020003FD RID: 1021
	public class FileSystemEventArgs : EventArgs
	{
		// Token: 0x06002664 RID: 9828 RVA: 0x000B0F51 File Offset: 0x000AF151
		public FileSystemEventArgs(WatcherChangeTypes changeType, string directory, string name)
		{
			this.changeType = changeType;
			this.name = name;
			if (!directory.EndsWith("\\", StringComparison.Ordinal))
			{
				directory += "\\";
			}
			this.fullPath = directory + name;
		}

		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x06002665 RID: 9829 RVA: 0x000B0F8F File Offset: 0x000AF18F
		public WatcherChangeTypes ChangeType
		{
			get
			{
				return this.changeType;
			}
		}

		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x06002666 RID: 9830 RVA: 0x000B0F97 File Offset: 0x000AF197
		public string FullPath
		{
			get
			{
				return this.fullPath;
			}
		}

		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x06002667 RID: 9831 RVA: 0x000B0F9F File Offset: 0x000AF19F
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x040020BC RID: 8380
		private WatcherChangeTypes changeType;

		// Token: 0x040020BD RID: 8381
		private string name;

		// Token: 0x040020BE RID: 8382
		private string fullPath;
	}
}
