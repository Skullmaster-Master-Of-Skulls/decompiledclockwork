using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataSync
{
	// Token: 0x02000705 RID: 1797
	[DataContract(Namespace = "http://tpro.ca")]
	public class DataSyncExternalCourseDTO
	{
		// Token: 0x17000C8A RID: 3210
		// (get) Token: 0x06002495 RID: 9365 RVA: 0x00010B06 File Offset: 0x0000ED06
		// (set) Token: 0x06002496 RID: 9366 RVA: 0x00010B0E File Offset: 0x0000ED0E
		[DataMember]
		public string ExternalCourseId { get; set; }

		// Token: 0x17000C8B RID: 3211
		// (get) Token: 0x06002497 RID: 9367 RVA: 0x00010B17 File Offset: 0x0000ED17
		// (set) Token: 0x06002498 RID: 9368 RVA: 0x00010B1F File Offset: 0x0000ED1F
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x17000C8C RID: 3212
		// (get) Token: 0x06002499 RID: 9369 RVA: 0x00010B28 File Offset: 0x0000ED28
		// (set) Token: 0x0600249A RID: 9370 RVA: 0x00010B30 File Offset: 0x0000ED30
		[DataMember]
		public DateTime EndDate { get; set; }

		// Token: 0x17000C8D RID: 3213
		// (get) Token: 0x0600249B RID: 9371 RVA: 0x00010B39 File Offset: 0x0000ED39
		// (set) Token: 0x0600249C RID: 9372 RVA: 0x00010B41 File Offset: 0x0000ED41
		[DataMember]
		public string Duration { get; set; }

		// Token: 0x17000C8E RID: 3214
		// (get) Token: 0x0600249D RID: 9373 RVA: 0x00010B4A File Offset: 0x0000ED4A
		// (set) Token: 0x0600249E RID: 9374 RVA: 0x00010B52 File Offset: 0x0000ED52
		[DataMember]
		public string Term { get; set; }

		// Token: 0x17000C8F RID: 3215
		// (get) Token: 0x0600249F RID: 9375 RVA: 0x00010B5B File Offset: 0x0000ED5B
		// (set) Token: 0x060024A0 RID: 9376 RVA: 0x00010B63 File Offset: 0x0000ED63
		[DataMember]
		public string Subject { get; set; }

		// Token: 0x17000C90 RID: 3216
		// (get) Token: 0x060024A1 RID: 9377 RVA: 0x00010B6C File Offset: 0x0000ED6C
		// (set) Token: 0x060024A2 RID: 9378 RVA: 0x00010B74 File Offset: 0x0000ED74
		[DataMember]
		public string SubjectLong { get; set; }

		// Token: 0x17000C91 RID: 3217
		// (get) Token: 0x060024A3 RID: 9379 RVA: 0x00010B7D File Offset: 0x0000ED7D
		// (set) Token: 0x060024A4 RID: 9380 RVA: 0x00010B85 File Offset: 0x0000ED85
		[DataMember]
		public string Course { get; set; }

		// Token: 0x17000C92 RID: 3218
		// (get) Token: 0x060024A5 RID: 9381 RVA: 0x00010B8E File Offset: 0x0000ED8E
		// (set) Token: 0x060024A6 RID: 9382 RVA: 0x00010B96 File Offset: 0x0000ED96
		[DataMember]
		public string Section { get; set; }

		// Token: 0x17000C93 RID: 3219
		// (get) Token: 0x060024A7 RID: 9383 RVA: 0x00010B9F File Offset: 0x0000ED9F
		// (set) Token: 0x060024A8 RID: 9384 RVA: 0x00010BA7 File Offset: 0x0000EDA7
		[DataMember]
		public string TimeOfDay { get; set; }

		// Token: 0x17000C94 RID: 3220
		// (get) Token: 0x060024A9 RID: 9385 RVA: 0x00010BB0 File Offset: 0x0000EDB0
		// (set) Token: 0x060024AA RID: 9386 RVA: 0x00010BB8 File Offset: 0x0000EDB8
		[DataMember]
		public string Campus { get; set; }

		// Token: 0x17000C95 RID: 3221
		// (get) Token: 0x060024AB RID: 9387 RVA: 0x00010BC1 File Offset: 0x0000EDC1
		// (set) Token: 0x060024AC RID: 9388 RVA: 0x00010BC9 File Offset: 0x0000EDC9
		[DataMember]
		public string Department { get; set; }

		// Token: 0x17000C96 RID: 3222
		// (get) Token: 0x060024AD RID: 9389 RVA: 0x00010BD2 File Offset: 0x0000EDD2
		// (set) Token: 0x060024AE RID: 9390 RVA: 0x00010BDA File Offset: 0x0000EDDA
		[DataMember]
		public string Location { get; set; }

		// Token: 0x17000C97 RID: 3223
		// (get) Token: 0x060024AF RID: 9391 RVA: 0x00010BE3 File Offset: 0x0000EDE3
		// (set) Token: 0x060024B0 RID: 9392 RVA: 0x00010BEB File Offset: 0x0000EDEB
		[DataMember]
		public string CourseNote { get; set; }

		// Token: 0x17000C98 RID: 3224
		// (get) Token: 0x060024B1 RID: 9393 RVA: 0x00010BF4 File Offset: 0x0000EDF4
		// (set) Token: 0x060024B2 RID: 9394 RVA: 0x00010BFC File Offset: 0x0000EDFC
		[DataMember]
		public List<DataSyncExternalCourseInstructorDTO> Instructors { get; set; }

		// Token: 0x17000C99 RID: 3225
		// (get) Token: 0x060024B3 RID: 9395 RVA: 0x00010C05 File Offset: 0x0000EE05
		// (set) Token: 0x060024B4 RID: 9396 RVA: 0x00010C0D File Offset: 0x0000EE0D
		[DataMember]
		public List<DataSyncExternalCourseTimetableItemDTO> TimetableItems { get; set; }

		// Token: 0x17000C9A RID: 3226
		// (get) Token: 0x060024B5 RID: 9397 RVA: 0x00010C16 File Offset: 0x0000EE16
		// (set) Token: 0x060024B6 RID: 9398 RVA: 0x00010C1E File Offset: 0x0000EE1E
		[DataMember]
		public List<DataSyncExternalCourseAltContactDTO> AlternateContacts { get; set; }

		// Token: 0x17000C9B RID: 3227
		// (get) Token: 0x060024B7 RID: 9399 RVA: 0x00010C27 File Offset: 0x0000EE27
		// (set) Token: 0x060024B8 RID: 9400 RVA: 0x00010C2F File Offset: 0x0000EE2F
		[DataMember]
		public LookupCourseDTO MatchingClockWorkLookupCourse { get; set; }

		// Token: 0x17000C9C RID: 3228
		// (get) Token: 0x060024B9 RID: 9401 RVA: 0x00010C38 File Offset: 0x0000EE38
		// (set) Token: 0x060024BA RID: 9402 RVA: 0x00010C40 File Offset: 0x0000EE40
		[DataMember]
		public IList<DataSyncExternalCourseFinalExamInfoDTO> FinalExamInfos { get; set; }

		// Token: 0x17000C9D RID: 3229
		// (get) Token: 0x060024BB RID: 9403 RVA: 0x00010C49 File Offset: 0x0000EE49
		// (set) Token: 0x060024BC RID: 9404 RVA: 0x00010C51 File Offset: 0x0000EE51
		[DataMember]
		public decimal Credits { get; set; }

		// Token: 0x17000C9E RID: 3230
		// (get) Token: 0x060024BD RID: 9405 RVA: 0x00010C5A File Offset: 0x0000EE5A
		// (set) Token: 0x060024BE RID: 9406 RVA: 0x00010C62 File Offset: 0x0000EE62
		[DataMember]
		public DataSyncExternalCourseStudentSpecificDTO StudentSpecificInfo { get; set; }
	}
}
