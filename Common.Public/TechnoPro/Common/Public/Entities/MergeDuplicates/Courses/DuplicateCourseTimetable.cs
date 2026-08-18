using System;

namespace TechnoPro.Common.Public.Entities.MergeDuplicates.Courses
{
	// Token: 0x0200029F RID: 671
	public class DuplicateCourseTimetable : BusinessBase<int>
	{
		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x06001454 RID: 5204 RVA: 0x00019E0C File Offset: 0x0001800C
		// (set) Token: 0x06001455 RID: 5205 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int TimetableId
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

		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x06001456 RID: 5206 RVA: 0x00019E24 File Offset: 0x00018024
		// (set) Token: 0x06001457 RID: 5207 RVA: 0x00019E2C File Offset: 0x0001802C
		public int Lucid { get; set; }

		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x06001458 RID: 5208 RVA: 0x00019E35 File Offset: 0x00018035
		// (set) Token: 0x06001459 RID: 5209 RVA: 0x00019E3D File Offset: 0x0001803D
		public DuplicateCourseMergeResult MergeResult { get; set; }
	}
}
