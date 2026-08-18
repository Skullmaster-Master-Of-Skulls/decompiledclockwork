using System;
using System.Collections.Generic;

namespace ClockWorkWebAPI.TestBooking
{
	// Token: 0x02000033 RID: 51
	[Serializable]
	public class FindPotentialBookingsInfo
	{
		// Token: 0x0600028D RID: 653 RVA: 0x00011026 File Offset: 0x0000F226
		public FindPotentialBookingsInfo()
		{
			this.ignoreStudentAppointmentIds = new List<int>();
			this.RestrictByCampus = false;
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600028E RID: 654 RVA: 0x00011051 File Offset: 0x0000F251
		// (set) Token: 0x0600028F RID: 655 RVA: 0x00011059 File Offset: 0x0000F259
		public virtual bool RestrictByCampus { get; set; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000290 RID: 656 RVA: 0x00011062 File Offset: 0x0000F262
		// (set) Token: 0x06000291 RID: 657 RVA: 0x0001106A File Offset: 0x0000F26A
		public int BufferMinutesPre { get; set; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000292 RID: 658 RVA: 0x00011073 File Offset: 0x0000F273
		// (set) Token: 0x06000293 RID: 659 RVA: 0x0001107B File Offset: 0x0000F27B
		public int BufferMinutesPost { get; set; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000294 RID: 660 RVA: 0x00011084 File Offset: 0x0000F284
		// (set) Token: 0x06000295 RID: 661 RVA: 0x0001109C File Offset: 0x0000F29C
		public List<int> IgnoreStudentAppointmentIds
		{
			get
			{
				return this.ignoreStudentAppointmentIds;
			}
			set
			{
				this.ignoreStudentAppointmentIds = value;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000296 RID: 662 RVA: 0x000110A8 File Offset: 0x0000F2A8
		// (set) Token: 0x06000297 RID: 663 RVA: 0x000110C0 File Offset: 0x0000F2C0
		public bool IgnoreStudentsSchedule
		{
			get
			{
				return this.ignoreStudentsSchedule;
			}
			set
			{
				this.ignoreStudentsSchedule = value;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000298 RID: 664 RVA: 0x000110CC File Offset: 0x0000F2CC
		// (set) Token: 0x06000299 RID: 665 RVA: 0x000110E4 File Offset: 0x0000F2E4
		public bool IgnoreTwoTestsSameCourseSameDay
		{
			get
			{
				return this.ignoreTwoTestsSameCourseSameDay;
			}
			set
			{
				this.ignoreTwoTestsSameCourseSameDay = value;
			}
		}

		// Token: 0x04000172 RID: 370
		private List<int> ignoreStudentAppointmentIds;

		// Token: 0x04000173 RID: 371
		private bool ignoreStudentsSchedule = false;

		// Token: 0x04000174 RID: 372
		private bool ignoreTwoTestsSameCourseSameDay = false;
	}
}
