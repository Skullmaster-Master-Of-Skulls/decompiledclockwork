using System;

namespace TechnoPro.Common.Public.Entities.ClockWorkServerJob
{
	// Token: 0x02000455 RID: 1109
	[Serializable]
	public class ClockWorkServerJobStep : BusinessBase<int>
	{
		// Token: 0x17000DF0 RID: 3568
		// (get) Token: 0x060021B7 RID: 8631 RVA: 0x00025924 File Offset: 0x00023B24
		// (set) Token: 0x060021B8 RID: 8632 RVA: 0x0000E258 File Offset: 0x0000C458
		public int StepId
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

		// Token: 0x17000DF1 RID: 3569
		// (get) Token: 0x060021B9 RID: 8633 RVA: 0x0002593C File Offset: 0x00023B3C
		// (set) Token: 0x060021BA RID: 8634 RVA: 0x00025944 File Offset: 0x00023B44
		public int JobId { get; set; }

		// Token: 0x17000DF2 RID: 3570
		// (get) Token: 0x060021BB RID: 8635 RVA: 0x0002594D File Offset: 0x00023B4D
		// (set) Token: 0x060021BC RID: 8636 RVA: 0x00025955 File Offset: 0x00023B55
		public string JobType { get; set; }

		// Token: 0x17000DF3 RID: 3571
		// (get) Token: 0x060021BD RID: 8637 RVA: 0x0002595E File Offset: 0x00023B5E
		// (set) Token: 0x060021BE RID: 8638 RVA: 0x00025966 File Offset: 0x00023B66
		public string Title { get; set; }

		// Token: 0x17000DF4 RID: 3572
		// (get) Token: 0x060021BF RID: 8639 RVA: 0x0002596F File Offset: 0x00023B6F
		// (set) Token: 0x060021C0 RID: 8640 RVA: 0x00025977 File Offset: 0x00023B77
		public string Notes { get; set; }

		// Token: 0x17000DF5 RID: 3573
		// (get) Token: 0x060021C1 RID: 8641 RVA: 0x00025980 File Offset: 0x00023B80
		// (set) Token: 0x060021C2 RID: 8642 RVA: 0x00025988 File Offset: 0x00023B88
		public string Parameters { get; set; }

		// Token: 0x17000DF6 RID: 3574
		// (get) Token: 0x060021C3 RID: 8643 RVA: 0x00025991 File Offset: 0x00023B91
		// (set) Token: 0x060021C4 RID: 8644 RVA: 0x00025999 File Offset: 0x00023B99
		public int OrderNum { get; set; }

		// Token: 0x17000DF7 RID: 3575
		// (get) Token: 0x060021C5 RID: 8645 RVA: 0x000259A2 File Offset: 0x00023BA2
		// (set) Token: 0x060021C6 RID: 8646 RVA: 0x000259AA File Offset: 0x00023BAA
		public bool IsActive { get; set; }
	}
}
