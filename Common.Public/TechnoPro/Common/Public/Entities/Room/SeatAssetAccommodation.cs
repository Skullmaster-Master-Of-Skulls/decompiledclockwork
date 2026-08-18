using System;

namespace TechnoPro.Common.Public.Entities.Room
{
	// Token: 0x0200020C RID: 524
	public class SeatAssetAccommodation : BusinessBase<int>
	{
		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x06000FF9 RID: 4089 RVA: 0x0001731C File Offset: 0x0001551C
		// (set) Token: 0x06000FFA RID: 4090 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int ControlId
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

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x06000FFB RID: 4091 RVA: 0x00017334 File Offset: 0x00015534
		// (set) Token: 0x06000FFC RID: 4092 RVA: 0x0001733C File Offset: 0x0001553C
		public string Title { get; set; }

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x06000FFD RID: 4093 RVA: 0x00017345 File Offset: 0x00015545
		// (set) Token: 0x06000FFE RID: 4094 RVA: 0x0001734D File Offset: 0x0001554D
		public string LookupText { get; set; }

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x06000FFF RID: 4095 RVA: 0x00017356 File Offset: 0x00015556
		// (set) Token: 0x06001000 RID: 4096 RVA: 0x0001735E File Offset: 0x0001555E
		public string SubText { get; set; }

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x06001001 RID: 4097 RVA: 0x00017367 File Offset: 0x00015567
		// (set) Token: 0x06001002 RID: 4098 RVA: 0x0001736F File Offset: 0x0001556F
		public int Level { get; set; }
	}
}
