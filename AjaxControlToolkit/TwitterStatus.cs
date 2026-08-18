using System;

namespace AjaxControlToolkit
{
	// Token: 0x020001B6 RID: 438
	public class TwitterStatus
	{
		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06000CCF RID: 3279 RVA: 0x0002266D File Offset: 0x0002086D
		// (set) Token: 0x06000CD0 RID: 3280 RVA: 0x00022675 File Offset: 0x00020875
		public DateTime CreatedAt { get; set; }

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06000CD1 RID: 3281 RVA: 0x0002267E File Offset: 0x0002087E
		// (set) Token: 0x06000CD2 RID: 3282 RVA: 0x00022686 File Offset: 0x00020886
		public string Text { get; set; }

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06000CD3 RID: 3283 RVA: 0x0002268F File Offset: 0x0002088F
		// (set) Token: 0x06000CD4 RID: 3284 RVA: 0x00022697 File Offset: 0x00020897
		public TwitterUser User { get; set; }
	}
}
