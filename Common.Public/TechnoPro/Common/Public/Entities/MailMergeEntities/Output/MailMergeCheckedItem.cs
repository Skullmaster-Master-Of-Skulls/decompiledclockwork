using System;

namespace TechnoPro.Common.Public.Entities.MailMergeEntities.Output
{
	// Token: 0x020002CC RID: 716
	public class MailMergeCheckedItem
	{
		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x060015B2 RID: 5554 RVA: 0x0001B119 File Offset: 0x00019319
		// (set) Token: 0x060015B3 RID: 5555 RVA: 0x0001B121 File Offset: 0x00019321
		public string Title { get; set; }

		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x060015B4 RID: 5556 RVA: 0x0001B12A File Offset: 0x0001932A
		// (set) Token: 0x060015B5 RID: 5557 RVA: 0x0001B132 File Offset: 0x00019332
		public bool IsChecked { get; set; }

		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x060015B6 RID: 5558 RVA: 0x0001B13B File Offset: 0x0001933B
		// (set) Token: 0x060015B7 RID: 5559 RVA: 0x0001B143 File Offset: 0x00019343
		public bool HideCheckboxTitle { get; set; }
	}
}
