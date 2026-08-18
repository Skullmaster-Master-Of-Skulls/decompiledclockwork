using System;

namespace TechnoPro.Common.Public.Entities.Emailing
{
	// Token: 0x02000345 RID: 837
	public class EmailHistoryLoggerItem
	{
		// Token: 0x17000AC1 RID: 2753
		// (get) Token: 0x060019EE RID: 6638 RVA: 0x0001E3A3 File Offset: 0x0001C5A3
		// (set) Token: 0x060019EF RID: 6639 RVA: 0x0001E3AB File Offset: 0x0001C5AB
		public string HistoryCode { get; set; }

		// Token: 0x17000AC2 RID: 2754
		// (get) Token: 0x060019F0 RID: 6640 RVA: 0x0001E3B4 File Offset: 0x0001C5B4
		// (set) Token: 0x060019F1 RID: 6641 RVA: 0x0001E3BC File Offset: 0x0001C5BC
		public int PersonId { get; set; }

		// Token: 0x17000AC3 RID: 2755
		// (get) Token: 0x060019F2 RID: 6642 RVA: 0x0001E3C5 File Offset: 0x0001C5C5
		// (set) Token: 0x060019F3 RID: 6643 RVA: 0x0001E3CD File Offset: 0x0001C5CD
		public int TemplateId { get; set; }

		// Token: 0x17000AC4 RID: 2756
		// (get) Token: 0x060019F4 RID: 6644 RVA: 0x0001E3D6 File Offset: 0x0001C5D6
		// (set) Token: 0x060019F5 RID: 6645 RVA: 0x0001E3DE File Offset: 0x0001C5DE
		public int InfoPcId { get; set; }

		// Token: 0x17000AC5 RID: 2757
		// (get) Token: 0x060019F6 RID: 6646 RVA: 0x0001E3E7 File Offset: 0x0001C5E7
		// (set) Token: 0x060019F7 RID: 6647 RVA: 0x0001E3EF File Offset: 0x0001C5EF
		public int LuCourseId { get; set; }

		// Token: 0x17000AC6 RID: 2758
		// (get) Token: 0x060019F8 RID: 6648 RVA: 0x0001E3F8 File Offset: 0x0001C5F8
		// (set) Token: 0x060019F9 RID: 6649 RVA: 0x0001E400 File Offset: 0x0001C600
		public bool WasSuccessfullySent { get; set; }

		// Token: 0x17000AC7 RID: 2759
		// (get) Token: 0x060019FA RID: 6650 RVA: 0x0001E409 File Offset: 0x0001C609
		// (set) Token: 0x060019FB RID: 6651 RVA: 0x0001E411 File Offset: 0x0001C611
		public string Note { get; set; }

		// Token: 0x17000AC8 RID: 2760
		// (get) Token: 0x060019FC RID: 6652 RVA: 0x0001E41A File Offset: 0x0001C61A
		// (set) Token: 0x060019FD RID: 6653 RVA: 0x0001E422 File Offset: 0x0001C622
		public string EmailMessage { get; set; }

		// Token: 0x17000AC9 RID: 2761
		// (get) Token: 0x060019FE RID: 6654 RVA: 0x0001E42B File Offset: 0x0001C62B
		// (set) Token: 0x060019FF RID: 6655 RVA: 0x0001E433 File Offset: 0x0001C633
		public int SentByPersonId { get; set; }
	}
}
