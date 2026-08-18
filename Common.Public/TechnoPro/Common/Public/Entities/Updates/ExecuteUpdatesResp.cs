using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.Updates
{
	// Token: 0x0200013B RID: 315
	public class ExecuteUpdatesResp
	{
		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000789 RID: 1929 RVA: 0x00010790 File Offset: 0x0000E990
		// (set) Token: 0x0600078A RID: 1930 RVA: 0x00010798 File Offset: 0x0000E998
		public eExecuteUpdateStatus ExecuteUpdatesStatus { get; set; }

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x0600078B RID: 1931 RVA: 0x000107A1 File Offset: 0x0000E9A1
		// (set) Token: 0x0600078C RID: 1932 RVA: 0x000107A9 File Offset: 0x0000E9A9
		public IList<string> Filenames { get; set; }

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x0600078D RID: 1933 RVA: 0x000107B2 File Offset: 0x0000E9B2
		// (set) Token: 0x0600078E RID: 1934 RVA: 0x000107BA File Offset: 0x0000E9BA
		public string LastError { get; set; }
	}
}
