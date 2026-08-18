using System;

namespace Telerik.Web.UI
{
	// Token: 0x020002E0 RID: 736
	public class FileExplorerPathsEventArgs : EventArgs
	{
		// Token: 0x06001990 RID: 6544 RVA: 0x00054708 File Offset: 0x00052908
		public FileExplorerPathsEventArgs(string pathType, string[] paths)
		{
			this.PathType = pathType;
			this.Paths = paths;
		}

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x06001991 RID: 6545 RVA: 0x0005471E File Offset: 0x0005291E
		// (set) Token: 0x06001992 RID: 6546 RVA: 0x00054726 File Offset: 0x00052926
		public string PathType { get; private set; }

		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x06001993 RID: 6547 RVA: 0x0005472F File Offset: 0x0005292F
		// (set) Token: 0x06001994 RID: 6548 RVA: 0x00054737 File Offset: 0x00052937
		public string[] Paths { get; private set; }
	}
}
