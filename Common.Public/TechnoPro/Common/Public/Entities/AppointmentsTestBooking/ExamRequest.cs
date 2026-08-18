using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.Accommodations;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.AppointmentsTestBooking
{
	// Token: 0x02000506 RID: 1286
	public class ExamRequest : BusinessBase<int>
	{
		// Token: 0x17001049 RID: 4169
		// (get) Token: 0x06002719 RID: 10009 RVA: 0x00029548 File Offset: 0x00027748
		// (set) Token: 0x0600271A RID: 10010 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int ExamRequestId
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

		// Token: 0x1700104A RID: 4170
		// (get) Token: 0x0600271B RID: 10011 RVA: 0x00029560 File Offset: 0x00027760
		// (set) Token: 0x0600271C RID: 10012 RVA: 0x00029568 File Offset: 0x00027768
		public LookupCourseBaseWithPrimaryInstructor Course { get; set; }

		// Token: 0x1700104B RID: 4171
		// (get) Token: 0x0600271D RID: 10013 RVA: 0x00029571 File Offset: 0x00027771
		// (set) Token: 0x0600271E RID: 10014 RVA: 0x00029579 File Offset: 0x00027779
		public PersonBase Student { get; set; }

		// Token: 0x1700104C RID: 4172
		// (get) Token: 0x0600271F RID: 10015 RVA: 0x00029582 File Offset: 0x00027782
		// (set) Token: 0x06002720 RID: 10016 RVA: 0x0002958A File Offset: 0x0002778A
		public DateTime DateEntered { get; set; }

		// Token: 0x1700104D RID: 4173
		// (get) Token: 0x06002721 RID: 10017 RVA: 0x00029593 File Offset: 0x00027793
		// (set) Token: 0x06002722 RID: 10018 RVA: 0x0002959B File Offset: 0x0002779B
		public string InstructorName { get; set; }

		// Token: 0x1700104E RID: 4174
		// (get) Token: 0x06002723 RID: 10019 RVA: 0x000295A4 File Offset: 0x000277A4
		// (set) Token: 0x06002724 RID: 10020 RVA: 0x000295AC File Offset: 0x000277AC
		public string InstructorEmail { get; set; }

		// Token: 0x1700104F RID: 4175
		// (get) Token: 0x06002725 RID: 10021 RVA: 0x000295B5 File Offset: 0x000277B5
		// (set) Token: 0x06002726 RID: 10022 RVA: 0x000295BD File Offset: 0x000277BD
		public IList<AccommodationData> AccommodationsSelected { get; set; }

		// Token: 0x17001050 RID: 4176
		// (get) Token: 0x06002727 RID: 10023 RVA: 0x000295C6 File Offset: 0x000277C6
		// (set) Token: 0x06002728 RID: 10024 RVA: 0x000295CE File Offset: 0x000277CE
		public DateTime ClassTestStartDateTime { get; set; }

		// Token: 0x17001051 RID: 4177
		// (get) Token: 0x06002729 RID: 10025 RVA: 0x000295D7 File Offset: 0x000277D7
		// (set) Token: 0x0600272A RID: 10026 RVA: 0x000295DF File Offset: 0x000277DF
		public DateTime ClassTestEndDateTime { get; set; }

		// Token: 0x17001052 RID: 4178
		// (get) Token: 0x0600272B RID: 10027 RVA: 0x000295E8 File Offset: 0x000277E8
		// (set) Token: 0x0600272C RID: 10028 RVA: 0x000295F0 File Offset: 0x000277F0
		public string ClassTestDescription { get; set; }

		// Token: 0x17001053 RID: 4179
		// (get) Token: 0x0600272D RID: 10029 RVA: 0x000295F9 File Offset: 0x000277F9
		// (set) Token: 0x0600272E RID: 10030 RVA: 0x00029601 File Offset: 0x00027801
		public string InstructorSubmittedDescription { get; set; }
	}
}
