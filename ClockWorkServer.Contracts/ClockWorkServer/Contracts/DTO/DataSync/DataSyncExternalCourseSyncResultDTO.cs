using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataSync
{
	// Token: 0x0200070B RID: 1803
	[DataContract(Namespace = "http://tpro.ca")]
	public class DataSyncExternalCourseSyncResultDTO
	{
		// Token: 0x17000CCD RID: 3277
		// (get) Token: 0x06002521 RID: 9505 RVA: 0x00010F79 File Offset: 0x0000F179
		// (set) Token: 0x06002522 RID: 9506 RVA: 0x00010F81 File Offset: 0x0000F181
		[DataMember]
		public int Lucid { get; set; }

		// Token: 0x17000CCE RID: 3278
		// (get) Token: 0x06002523 RID: 9507 RVA: 0x00010F8A File Offset: 0x0000F18A
		// (set) Token: 0x06002524 RID: 9508 RVA: 0x00010F92 File Offset: 0x0000F192
		[DataMember]
		public int InstructorId { get; set; }

		// Token: 0x17000CCF RID: 3279
		// (get) Token: 0x06002525 RID: 9509 RVA: 0x00010F9B File Offset: 0x0000F19B
		// (set) Token: 0x06002526 RID: 9510 RVA: 0x00010FA3 File Offset: 0x0000F1A3
		[DataMember]
		public DataSyncExternalCourseDTO ExternalCourse { get; set; }

		// Token: 0x17000CD0 RID: 3280
		// (get) Token: 0x06002527 RID: 9511 RVA: 0x00010FAC File Offset: 0x0000F1AC
		// (set) Token: 0x06002528 RID: 9512 RVA: 0x00010FB4 File Offset: 0x0000F1B4
		[DataMember]
		public eDataSyncCourseRegistrationActionDTO CourseRegistrationAction { get; set; }

		// Token: 0x17000CD1 RID: 3281
		// (get) Token: 0x06002529 RID: 9513 RVA: 0x00010FBD File Offset: 0x0000F1BD
		// (set) Token: 0x0600252A RID: 9514 RVA: 0x00010FC5 File Offset: 0x0000F1C5
		[DataMember]
		public eDataSyncCourseLookupCourseActionDTO LookupCourseAction { get; set; }

		// Token: 0x17000CD2 RID: 3282
		// (get) Token: 0x0600252B RID: 9515 RVA: 0x00010FCE File Offset: 0x0000F1CE
		// (set) Token: 0x0600252C RID: 9516 RVA: 0x00010FD6 File Offset: 0x0000F1D6
		[DataMember]
		public eDataSyncCourseInstructorActionDTO InstructorAction { get; set; }

		// Token: 0x17000CD3 RID: 3283
		// (get) Token: 0x0600252D RID: 9517 RVA: 0x00010FDF File Offset: 0x0000F1DF
		// (set) Token: 0x0600252E RID: 9518 RVA: 0x00010FE7 File Offset: 0x0000F1E7
		[DataMember]
		public eDataSyncCourseMiscActionDTO MiscAction { get; set; }

		// Token: 0x17000CD4 RID: 3284
		// (get) Token: 0x0600252F RID: 9519 RVA: 0x00010FF0 File Offset: 0x0000F1F0
		// (set) Token: 0x06002530 RID: 9520 RVA: 0x00010FF8 File Offset: 0x0000F1F8
		[DataMember]
		public eDataSyncCourseErrorDTO ErrorAction { get; set; }

		// Token: 0x17000CD5 RID: 3285
		// (get) Token: 0x06002531 RID: 9521 RVA: 0x00011001 File Offset: 0x0000F201
		// (set) Token: 0x06002532 RID: 9522 RVA: 0x00011009 File Offset: 0x0000F209
		[DataMember]
		public ClassTestBaseDTO FinalExamClassTestBase { get; set; }

		// Token: 0x17000CD6 RID: 3286
		// (get) Token: 0x06002533 RID: 9523 RVA: 0x00011012 File Offset: 0x0000F212
		// (set) Token: 0x06002534 RID: 9524 RVA: 0x0001101A File Offset: 0x0000F21A
		[DataMember]
		public string Msg { get; set; }
	}
}
