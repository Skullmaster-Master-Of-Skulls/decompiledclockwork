using System;

namespace TechnoPro.Common.Public.Entities.AppointmentsTestExamViews.TestBookings
{
	// Token: 0x020004F7 RID: 1271
	public class TestLabel : BusinessBase<int>
	{
		// Token: 0x17001009 RID: 4105
		// (get) Token: 0x0600268C RID: 9868 RVA: 0x000290B0 File Offset: 0x000272B0
		// (set) Token: 0x0600268D RID: 9869 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int ExamStatusLookupId
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

		// Token: 0x1700100A RID: 4106
		// (get) Token: 0x0600268E RID: 9870 RVA: 0x000290C8 File Offset: 0x000272C8
		// (set) Token: 0x0600268F RID: 9871 RVA: 0x000290D0 File Offset: 0x000272D0
		public string Title { get; set; }

		// Token: 0x1700100B RID: 4107
		// (get) Token: 0x06002690 RID: 9872 RVA: 0x000290D9 File Offset: 0x000272D9
		// (set) Token: 0x06002691 RID: 9873 RVA: 0x000290E1 File Offset: 0x000272E1
		public int ColourArgb { get; set; }

		// Token: 0x1700100C RID: 4108
		// (get) Token: 0x06002692 RID: 9874 RVA: 0x000290EA File Offset: 0x000272EA
		// (set) Token: 0x06002693 RID: 9875 RVA: 0x000290F2 File Offset: 0x000272F2
		public bool IsActive { get; set; }

		// Token: 0x1700100D RID: 4109
		// (get) Token: 0x06002694 RID: 9876 RVA: 0x000290FB File Offset: 0x000272FB
		// (set) Token: 0x06002695 RID: 9877 RVA: 0x00029103 File Offset: 0x00027303
		public int OrderNum { get; set; }

		// Token: 0x1700100E RID: 4110
		// (get) Token: 0x06002696 RID: 9878 RVA: 0x0002910C File Offset: 0x0002730C
		// (set) Token: 0x06002697 RID: 9879 RVA: 0x00029114 File Offset: 0x00027314
		public bool HideFromStudent { get; set; }
	}
}
