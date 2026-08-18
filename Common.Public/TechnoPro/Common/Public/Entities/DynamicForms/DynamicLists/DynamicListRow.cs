using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.Files;

namespace TechnoPro.Common.Public.Entities.DynamicForms.DynamicLists
{
	// Token: 0x0200037C RID: 892
	public class DynamicListRow
	{
		// Token: 0x17000B76 RID: 2934
		// (get) Token: 0x06001B9B RID: 7067 RVA: 0x0001F692 File Offset: 0x0001D892
		// (set) Token: 0x06001B9C RID: 7068 RVA: 0x0001F69A File Offset: 0x0001D89A
		public IList<string> CellValues { get; set; }

		// Token: 0x17000B77 RID: 2935
		// (get) Token: 0x06001B9D RID: 7069 RVA: 0x0001F6A3 File Offset: 0x0001D8A3
		// (set) Token: 0x06001B9E RID: 7070 RVA: 0x0001F6AB File Offset: 0x0001D8AB
		public string Date { get; set; }

		// Token: 0x17000B78 RID: 2936
		// (get) Token: 0x06001B9F RID: 7071 RVA: 0x0001F6B4 File Offset: 0x0001D8B4
		// (set) Token: 0x06001BA0 RID: 7072 RVA: 0x0001F6BC File Offset: 0x0001D8BC
		public BinaryFile File { get; set; }

		// Token: 0x17000B79 RID: 2937
		// (get) Token: 0x06001BA1 RID: 7073 RVA: 0x0001F6C5 File Offset: 0x0001D8C5
		// (set) Token: 0x06001BA2 RID: 7074 RVA: 0x0001F6CD File Offset: 0x0001D8CD
		public int FileId { get; set; }
	}
}
