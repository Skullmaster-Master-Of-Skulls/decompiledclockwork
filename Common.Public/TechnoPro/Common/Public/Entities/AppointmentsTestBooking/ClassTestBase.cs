using System;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.Common.Public.Entities.AppointmentsTestBooking
{
	// Token: 0x0200050E RID: 1294
	public class ClassTestBase : BusinessBase<int>
	{
		// Token: 0x17001078 RID: 4216
		// (get) Token: 0x06002780 RID: 10112 RVA: 0x000298B0 File Offset: 0x00027AB0
		// (set) Token: 0x06002781 RID: 10113 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int ExamId
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

		// Token: 0x17001079 RID: 4217
		// (get) Token: 0x06002782 RID: 10114 RVA: 0x000298C8 File Offset: 0x00027AC8
		// (set) Token: 0x06002783 RID: 10115 RVA: 0x000298D0 File Offset: 0x00027AD0
		public DateTime StartDateTime { get; set; }

		// Token: 0x1700107A RID: 4218
		// (get) Token: 0x06002784 RID: 10116 RVA: 0x000298D9 File Offset: 0x00027AD9
		// (set) Token: 0x06002785 RID: 10117 RVA: 0x000298E1 File Offset: 0x00027AE1
		public DateTime EndDateTime { get; set; }

		// Token: 0x1700107B RID: 4219
		// (get) Token: 0x06002786 RID: 10118 RVA: 0x000298EA File Offset: 0x00027AEA
		// (set) Token: 0x06002787 RID: 10119 RVA: 0x000298F2 File Offset: 0x00027AF2
		public eClassTestType ExamType { get; set; }

		// Token: 0x1700107C RID: 4220
		// (get) Token: 0x06002788 RID: 10120 RVA: 0x000298FB File Offset: 0x00027AFB
		// (set) Token: 0x06002789 RID: 10121 RVA: 0x00029903 File Offset: 0x00027B03
		public LookupCourseBase Course { get; set; }

		// Token: 0x1700107D RID: 4221
		// (get) Token: 0x0600278A RID: 10122 RVA: 0x0002990C File Offset: 0x00027B0C
		// (set) Token: 0x0600278B RID: 10123 RVA: 0x00029914 File Offset: 0x00027B14
		public string Location { get; set; }

		// Token: 0x1700107E RID: 4222
		// (get) Token: 0x0600278C RID: 10124 RVA: 0x0002991D File Offset: 0x00027B1D
		// (set) Token: 0x0600278D RID: 10125 RVA: 0x00029925 File Offset: 0x00027B25
		public string ExternalExamId { get; set; }
	}
}
