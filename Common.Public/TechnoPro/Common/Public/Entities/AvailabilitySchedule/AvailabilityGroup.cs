using System;

namespace TechnoPro.Common.Public.Entities.AvailabilitySchedule
{
	// Token: 0x02000483 RID: 1155
	public class AvailabilityGroup : BusinessBase<int>
	{
		// Token: 0x17000E59 RID: 3673
		// (get) Token: 0x060022CA RID: 8906 RVA: 0x00026978 File Offset: 0x00024B78
		// (set) Token: 0x060022CB RID: 8907 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int AvailabilityGroupId
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

		// Token: 0x17000E5A RID: 3674
		// (get) Token: 0x060022CC RID: 8908 RVA: 0x00026990 File Offset: 0x00024B90
		// (set) Token: 0x060022CD RID: 8909 RVA: 0x00026998 File Offset: 0x00024B98
		public string Title { get; set; }

		// Token: 0x17000E5B RID: 3675
		// (get) Token: 0x060022CE RID: 8910 RVA: 0x000269A1 File Offset: 0x00024BA1
		// (set) Token: 0x060022CF RID: 8911 RVA: 0x000269A9 File Offset: 0x00024BA9
		public string Description { get; set; }

		// Token: 0x17000E5C RID: 3676
		// (get) Token: 0x060022D0 RID: 8912 RVA: 0x000269B2 File Offset: 0x00024BB2
		// (set) Token: 0x060022D1 RID: 8913 RVA: 0x000269BA File Offset: 0x00024BBA
		public int ColourArgB { get; set; }

		// Token: 0x17000E5D RID: 3677
		// (get) Token: 0x060022D2 RID: 8914 RVA: 0x000269C3 File Offset: 0x00024BC3
		// (set) Token: 0x060022D3 RID: 8915 RVA: 0x000269CB File Offset: 0x00024BCB
		public int Pattern { get; set; }
	}
}
