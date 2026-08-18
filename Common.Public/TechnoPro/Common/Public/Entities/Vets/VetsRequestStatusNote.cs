using System;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.Vets
{
	// Token: 0x0200010B RID: 267
	public class VetsRequestStatusNote : BusinessBase<int>
	{
		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000631 RID: 1585 RVA: 0x0000F1B8 File Offset: 0x0000D3B8
		// (set) Token: 0x06000632 RID: 1586 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int BenefitApplicationStatusDetailNotesId
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

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000633 RID: 1587 RVA: 0x0000F1D0 File Offset: 0x0000D3D0
		// (set) Token: 0x06000634 RID: 1588 RVA: 0x0000F1D8 File Offset: 0x0000D3D8
		public bool ForStudent { get; set; }

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000635 RID: 1589 RVA: 0x0000F1E1 File Offset: 0x0000D3E1
		// (set) Token: 0x06000636 RID: 1590 RVA: 0x0000F1E9 File Offset: 0x0000D3E9
		public string Note { get; set; }

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000637 RID: 1591 RVA: 0x0000F1F2 File Offset: 0x0000D3F2
		// (set) Token: 0x06000638 RID: 1592 RVA: 0x0000F1FA File Offset: 0x0000D3FA
		public DateTime DateEntered { get; set; }

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000639 RID: 1593 RVA: 0x0000F203 File Offset: 0x0000D403
		// (set) Token: 0x0600063A RID: 1594 RVA: 0x0000F20B File Offset: 0x0000D40B
		public PersonBase WhoEntered { get; set; }
	}
}
