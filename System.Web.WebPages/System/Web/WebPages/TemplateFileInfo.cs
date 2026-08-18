using System;

namespace System.Web.WebPages
{
	// Token: 0x0200007D RID: 125
	public class TemplateFileInfo
	{
		// Token: 0x060003C3 RID: 963 RVA: 0x0000C7CB File Offset: 0x0000A9CB
		public TemplateFileInfo(string virtualPath)
		{
			this._virtualPath = virtualPath;
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x0000C7DA File Offset: 0x0000A9DA
		public string VirtualPath
		{
			get
			{
				return this._virtualPath;
			}
		}

		// Token: 0x04000119 RID: 281
		private readonly string _virtualPath;
	}
}
