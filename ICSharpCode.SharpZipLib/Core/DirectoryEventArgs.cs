using System;

namespace ICSharpCode.SharpZipLib.Core
{
	// Token: 0x0200005E RID: 94
	public class DirectoryEventArgs : ScanEventArgs
	{
		// Token: 0x060003F1 RID: 1009 RVA: 0x000162B2 File Offset: 0x000152B2
		public DirectoryEventArgs(string name, bool hasMatchingFiles) : base(name)
		{
			this.hasMatchingFiles_ = hasMatchingFiles;
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x000162C2 File Offset: 0x000152C2
		public bool HasMatchingFiles
		{
			get
			{
				return this.hasMatchingFiles_;
			}
		}

		// Token: 0x040002CC RID: 716
		private bool hasMatchingFiles_;
	}
}
