using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000B1D RID: 2845
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class DropDownListPostBackCommand
	{
		// Token: 0x170022CD RID: 8909
		// (get) Token: 0x06006A5B RID: 27227 RVA: 0x0018EB45 File Offset: 0x0018CD45
		// (set) Token: 0x06006A5C RID: 27228 RVA: 0x0018EB4D File Offset: 0x0018CD4D
		public DropDownListCommand Type { get; set; }

		// Token: 0x170022CE RID: 8910
		// (get) Token: 0x06006A5D RID: 27229 RVA: 0x0018EB56 File Offset: 0x0018CD56
		// (set) Token: 0x06006A5E RID: 27230 RVA: 0x0018EB5E File Offset: 0x0018CD5E
		public int Index { get; set; }

		// Token: 0x170022CF RID: 8911
		// (get) Token: 0x06006A5F RID: 27231 RVA: 0x0018EB67 File Offset: 0x0018CD67
		// (set) Token: 0x06006A60 RID: 27232 RVA: 0x0018EB6F File Offset: 0x0018CD6F
		public string Text { get; set; }

		// Token: 0x170022D0 RID: 8912
		// (get) Token: 0x06006A61 RID: 27233 RVA: 0x0018EB78 File Offset: 0x0018CD78
		// (set) Token: 0x06006A62 RID: 27234 RVA: 0x0018EB80 File Offset: 0x0018CD80
		public string Value { get; set; }
	}
}
