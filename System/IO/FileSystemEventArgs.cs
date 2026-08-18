using System;

namespace System.IO
{
	// Token: 0x02000729 RID: 1833
	public class FileSystemEventArgs : EventArgs
	{
		// Token: 0x060037F9 RID: 14329 RVA: 0x000EC715 File Offset: 0x000EB715
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

		// Token: 0x17000CFF RID: 3327
		// (get) Token: 0x060037FA RID: 14330 RVA: 0x000EC753 File Offset: 0x000EB753
		public WatcherChangeTypes ChangeType
		{
			get
			{
				return this.changeType;
			}
		}

		// Token: 0x17000D00 RID: 3328
		// (get) Token: 0x060037FB RID: 14331 RVA: 0x000EC75B File Offset: 0x000EB75B
		public string FullPath
		{
			get
			{
				return this.fullPath;
			}
		}

		// Token: 0x17000D01 RID: 3329
		// (get) Token: 0x060037FC RID: 14332 RVA: 0x000EC763 File Offset: 0x000EB763
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04003206 RID: 12806
		private WatcherChangeTypes changeType;

		// Token: 0x04003207 RID: 12807
		private string name;

		// Token: 0x04003208 RID: 12808
		private string fullPath;
	}
}
