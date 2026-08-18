using System;

namespace TechnoPro.Common.Public.Entities.Vets
{
	// Token: 0x02000109 RID: 265
	public class VetsChapter : BusinessBase<Guid>
	{
		// Token: 0x17000223 RID: 547
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x0000F118 File Offset: 0x0000D318
		// (set) Token: 0x0600061E RID: 1566 RVA: 0x0000EC6C File Offset: 0x0000CE6C
		public virtual Guid ChapterId
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x0600061F RID: 1567 RVA: 0x0000F130 File Offset: 0x0000D330
		// (set) Token: 0x06000620 RID: 1568 RVA: 0x0000F138 File Offset: 0x0000D338
		public string ChapterTitle { get; set; }

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000621 RID: 1569 RVA: 0x0000F141 File Offset: 0x0000D341
		// (set) Token: 0x06000622 RID: 1570 RVA: 0x0000F149 File Offset: 0x0000D349
		public string ChapterDescription { get; set; }

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000623 RID: 1571 RVA: 0x0000F152 File Offset: 0x0000D352
		// (set) Token: 0x06000624 RID: 1572 RVA: 0x0000F15A File Offset: 0x0000D35A
		public Guid AssociatedFormId { get; set; }

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000625 RID: 1573 RVA: 0x0000F163 File Offset: 0x0000D363
		// (set) Token: 0x06000626 RID: 1574 RVA: 0x0000F16B File Offset: 0x0000D36B
		public bool IsDisabled { get; set; }

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000627 RID: 1575 RVA: 0x0000F174 File Offset: 0x0000D374
		// (set) Token: 0x06000628 RID: 1576 RVA: 0x0000F17C File Offset: 0x0000D37C
		public int OrderNum { get; set; }
	}
}
