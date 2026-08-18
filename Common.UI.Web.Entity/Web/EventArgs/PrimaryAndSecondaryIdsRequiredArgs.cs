using System;

namespace TechnoPro.Common.UI.Web.Entity.Web.EventArgs
{
	// Token: 0x02000018 RID: 24
	public class PrimaryAndSecondaryIdsRequiredArgs : EventArgs
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000067 RID: 103 RVA: 0x0000278B File Offset: 0x0000098B
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00002793 File Offset: 0x00000993
		public int PrimaryId { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000069 RID: 105 RVA: 0x0000279C File Offset: 0x0000099C
		// (set) Token: 0x0600006A RID: 106 RVA: 0x000027A4 File Offset: 0x000009A4
		public int SecondaryId { get; set; }
	}
}
