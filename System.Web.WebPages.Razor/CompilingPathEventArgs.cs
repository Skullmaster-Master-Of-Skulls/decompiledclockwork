using System;

namespace System.Web.WebPages.Razor
{
	// Token: 0x02000005 RID: 5
	public class CompilingPathEventArgs : EventArgs
	{
		// Token: 0x06000015 RID: 21 RVA: 0x00002270 File Offset: 0x00000470
		public CompilingPathEventArgs(string virtualPath, WebPageRazorHost host)
		{
			this.VirtualPath = virtualPath;
			this.Host = host;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002286 File Offset: 0x00000486
		// (set) Token: 0x06000017 RID: 23 RVA: 0x0000228E File Offset: 0x0000048E
		public string VirtualPath { get; private set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002297 File Offset: 0x00000497
		// (set) Token: 0x06000019 RID: 25 RVA: 0x0000229F File Offset: 0x0000049F
		public WebPageRazorHost Host { get; set; }
	}
}
