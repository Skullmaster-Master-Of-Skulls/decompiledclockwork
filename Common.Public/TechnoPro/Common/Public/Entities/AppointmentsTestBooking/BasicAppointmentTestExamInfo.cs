using System;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.Common.Public.Entities.AppointmentsTestBooking
{
	// Token: 0x0200050D RID: 1293
	public class BasicAppointmentTestExamInfo : BusinessBase<int>
	{
		// Token: 0x17001071 RID: 4209
		// (get) Token: 0x06002770 RID: 10096 RVA: 0x00029830 File Offset: 0x00027A30
		// (set) Token: 0x06002771 RID: 10097 RVA: 0x0000E258 File Offset: 0x0000C458
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

		// Token: 0x17001072 RID: 4210
		// (get) Token: 0x06002772 RID: 10098 RVA: 0x00029848 File Offset: 0x00027A48
		// (set) Token: 0x06002773 RID: 10099 RVA: 0x00029850 File Offset: 0x00027A50
		public LookupCourseBase Course { get; set; }

		// Token: 0x17001073 RID: 4211
		// (get) Token: 0x06002774 RID: 10100 RVA: 0x00029859 File Offset: 0x00027A59
		// (set) Token: 0x06002775 RID: 10101 RVA: 0x00029861 File Offset: 0x00027A61
		public eClassTestType ClassTestType { get; set; }

		// Token: 0x17001074 RID: 4212
		// (get) Token: 0x06002776 RID: 10102 RVA: 0x0002986A File Offset: 0x00027A6A
		// (set) Token: 0x06002777 RID: 10103 RVA: 0x00029872 File Offset: 0x00027A72
		public DateTime ClassStartDateTime { get; set; }

		// Token: 0x17001075 RID: 4213
		// (get) Token: 0x06002778 RID: 10104 RVA: 0x0002987B File Offset: 0x00027A7B
		// (set) Token: 0x06002779 RID: 10105 RVA: 0x00029883 File Offset: 0x00027A83
		public DateTime ClassEndDateTime { get; set; }

		// Token: 0x17001076 RID: 4214
		// (get) Token: 0x0600277A RID: 10106 RVA: 0x0002988C File Offset: 0x00027A8C
		// (set) Token: 0x0600277B RID: 10107 RVA: 0x00029894 File Offset: 0x00027A94
		public string TestNote { get; set; }

		// Token: 0x17001077 RID: 4215
		// (get) Token: 0x0600277C RID: 10108 RVA: 0x0002989D File Offset: 0x00027A9D
		// (set) Token: 0x0600277D RID: 10109 RVA: 0x000298A5 File Offset: 0x00027AA5
		public string StudentNote { get; set; }
	}
}
