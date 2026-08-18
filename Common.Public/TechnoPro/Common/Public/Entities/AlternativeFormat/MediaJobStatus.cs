using System;

namespace TechnoPro.Common.Public.Entities.AlternativeFormat
{
	// Token: 0x02000583 RID: 1411
	public class MediaJobStatus : BusinessBase<int>
	{
		// Token: 0x17001319 RID: 4889
		// (get) Token: 0x06002D83 RID: 11651 RVA: 0x00032408 File Offset: 0x00030608
		// (set) Token: 0x06002D84 RID: 11652 RVA: 0x0000E258 File Offset: 0x0000C458
		public int MediaJobStatusId
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

		// Token: 0x1700131A RID: 4890
		// (get) Token: 0x06002D85 RID: 11653 RVA: 0x00032420 File Offset: 0x00030620
		// (set) Token: 0x06002D86 RID: 11654 RVA: 0x00032428 File Offset: 0x00030628
		public string JobStatusName { get; set; }

		// Token: 0x1700131B RID: 4891
		// (get) Token: 0x06002D87 RID: 11655 RVA: 0x00032431 File Offset: 0x00030631
		// (set) Token: 0x06002D88 RID: 11656 RVA: 0x00032439 File Offset: 0x00030639
		public string JobStatusDescription { get; set; }

		// Token: 0x1700131C RID: 4892
		// (get) Token: 0x06002D89 RID: 11657 RVA: 0x00032442 File Offset: 0x00030642
		// (set) Token: 0x06002D8A RID: 11658 RVA: 0x0003244A File Offset: 0x0003064A
		public MediaJobStatusGroup JobStatusGroup { get; set; }
	}
}
