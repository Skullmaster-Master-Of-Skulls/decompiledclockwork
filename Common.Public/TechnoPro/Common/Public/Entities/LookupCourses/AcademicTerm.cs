using System;

namespace TechnoPro.Common.Public.Entities.LookupCourses
{
	// Token: 0x020002EA RID: 746
	public class AcademicTerm : BusinessBase<int>
	{
		// Token: 0x06001666 RID: 5734 RVA: 0x0001BC5C File Offset: 0x00019E5C
		public AcademicTerm()
		{
			this.Title = "";
			this.TermId = 0;
		}

		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x06001667 RID: 5735 RVA: 0x0001BC7A File Offset: 0x00019E7A
		// (set) Token: 0x06001668 RID: 5736 RVA: 0x0001BC82 File Offset: 0x00019E82
		public DateTime StartMonthDay { get; set; }

		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x06001669 RID: 5737 RVA: 0x0001BC8B File Offset: 0x00019E8B
		// (set) Token: 0x0600166A RID: 5738 RVA: 0x0001BC93 File Offset: 0x00019E93
		public DateTime EndMonthDay { get; set; }

		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x0600166B RID: 5739 RVA: 0x0001BC9C File Offset: 0x00019E9C
		// (set) Token: 0x0600166C RID: 5740 RVA: 0x0001BCA4 File Offset: 0x00019EA4
		public string Title { get; set; }

		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x0600166D RID: 5741 RVA: 0x0001BCB0 File Offset: 0x00019EB0
		// (set) Token: 0x0600166E RID: 5742 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int TermId
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
	}
}
