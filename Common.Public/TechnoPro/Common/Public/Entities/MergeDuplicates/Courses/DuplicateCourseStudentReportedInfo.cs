using System;

namespace TechnoPro.Common.Public.Entities.MergeDuplicates.Courses
{
	// Token: 0x0200029E RID: 670
	public class DuplicateCourseStudentReportedInfo : BusinessBase<int>
	{
		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x0600144B RID: 5195 RVA: 0x00019DC0 File Offset: 0x00017FC0
		// (set) Token: 0x0600144C RID: 5196 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int AppointmentCourseId
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

		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x0600144D RID: 5197 RVA: 0x00019DD8 File Offset: 0x00017FD8
		// (set) Token: 0x0600144E RID: 5198 RVA: 0x00019DE0 File Offset: 0x00017FE0
		public int Lucid { get; set; }

		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x0600144F RID: 5199 RVA: 0x00019DE9 File Offset: 0x00017FE9
		// (set) Token: 0x06001450 RID: 5200 RVA: 0x00019DF1 File Offset: 0x00017FF1
		public int AppointmentId { get; set; }

		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x06001451 RID: 5201 RVA: 0x00019DFA File Offset: 0x00017FFA
		// (set) Token: 0x06001452 RID: 5202 RVA: 0x00019E02 File Offset: 0x00018002
		public DuplicateCourseMergeResult MergeResult { get; set; }
	}
}
