using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataSync
{
	// Token: 0x02000709 RID: 1801
	[DataContract(Namespace = "http://tpro.ca")]
	public class DataSyncExternalCourseRowPartDTO
	{
		// Token: 0x17000CB3 RID: 3251
		// (get) Token: 0x060024EB RID: 9451 RVA: 0x00010DBF File Offset: 0x0000EFBF
		// (set) Token: 0x060024EC RID: 9452 RVA: 0x00010DC7 File Offset: 0x0000EFC7
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x17000CB4 RID: 3252
		// (get) Token: 0x060024ED RID: 9453 RVA: 0x00010DD0 File Offset: 0x0000EFD0
		// (set) Token: 0x060024EE RID: 9454 RVA: 0x00010DD8 File Offset: 0x0000EFD8
		[DataMember]
		public DateTime EndDate { get; set; }

		// Token: 0x17000CB5 RID: 3253
		// (get) Token: 0x060024EF RID: 9455 RVA: 0x00010DE1 File Offset: 0x0000EFE1
		// (set) Token: 0x060024F0 RID: 9456 RVA: 0x00010DE9 File Offset: 0x0000EFE9
		[DataMember]
		public string ExternalCourseId { get; set; }

		// Token: 0x17000CB6 RID: 3254
		// (get) Token: 0x060024F1 RID: 9457 RVA: 0x00010DF2 File Offset: 0x0000EFF2
		// (set) Token: 0x060024F2 RID: 9458 RVA: 0x00010DFA File Offset: 0x0000EFFA
		[DataMember]
		public string Duration { get; set; }

		// Token: 0x17000CB7 RID: 3255
		// (get) Token: 0x060024F3 RID: 9459 RVA: 0x00010E03 File Offset: 0x0000F003
		// (set) Token: 0x060024F4 RID: 9460 RVA: 0x00010E0B File Offset: 0x0000F00B
		[DataMember]
		public string Term { get; set; }

		// Token: 0x17000CB8 RID: 3256
		// (get) Token: 0x060024F5 RID: 9461 RVA: 0x00010E14 File Offset: 0x0000F014
		// (set) Token: 0x060024F6 RID: 9462 RVA: 0x00010E1C File Offset: 0x0000F01C
		[DataMember]
		public string Subject { get; set; }

		// Token: 0x17000CB9 RID: 3257
		// (get) Token: 0x060024F7 RID: 9463 RVA: 0x00010E25 File Offset: 0x0000F025
		// (set) Token: 0x060024F8 RID: 9464 RVA: 0x00010E2D File Offset: 0x0000F02D
		[DataMember]
		public string Course { get; set; }

		// Token: 0x17000CBA RID: 3258
		// (get) Token: 0x060024F9 RID: 9465 RVA: 0x00010E36 File Offset: 0x0000F036
		// (set) Token: 0x060024FA RID: 9466 RVA: 0x00010E3E File Offset: 0x0000F03E
		[DataMember]
		public string Section { get; set; }

		// Token: 0x17000CBB RID: 3259
		// (get) Token: 0x060024FB RID: 9467 RVA: 0x00010E47 File Offset: 0x0000F047
		// (set) Token: 0x060024FC RID: 9468 RVA: 0x00010E4F File Offset: 0x0000F04F
		[DataMember]
		public string TimeOfDay { get; set; }

		// Token: 0x17000CBC RID: 3260
		// (get) Token: 0x060024FD RID: 9469 RVA: 0x00010E58 File Offset: 0x0000F058
		// (set) Token: 0x060024FE RID: 9470 RVA: 0x00010E60 File Offset: 0x0000F060
		[DataMember]
		public string Campus { get; set; }

		// Token: 0x17000CBD RID: 3261
		// (get) Token: 0x060024FF RID: 9471 RVA: 0x00010E69 File Offset: 0x0000F069
		// (set) Token: 0x06002500 RID: 9472 RVA: 0x00010E71 File Offset: 0x0000F071
		[DataMember]
		public string Location { get; set; }

		// Token: 0x17000CBE RID: 3262
		// (get) Token: 0x06002501 RID: 9473 RVA: 0x00010E7A File Offset: 0x0000F07A
		// (set) Token: 0x06002502 RID: 9474 RVA: 0x00010E82 File Offset: 0x0000F082
		[DataMember]
		public DataSyncExternalCourseInstructorDTO Instructor { get; set; }

		// Token: 0x17000CBF RID: 3263
		// (get) Token: 0x06002503 RID: 9475 RVA: 0x00010E8B File Offset: 0x0000F08B
		// (set) Token: 0x06002504 RID: 9476 RVA: 0x00010E93 File Offset: 0x0000F093
		[DataMember]
		public DataSyncExternalCourseAltContactDTO AltContact { get; set; }

		// Token: 0x17000CC0 RID: 3264
		// (get) Token: 0x06002505 RID: 9477 RVA: 0x00010E9C File Offset: 0x0000F09C
		// (set) Token: 0x06002506 RID: 9478 RVA: 0x00010EA4 File Offset: 0x0000F0A4
		[DataMember]
		public List<DataSyncExternalCourseTimetableItemDTO> TimetableItems { get; set; }

		// Token: 0x17000CC1 RID: 3265
		// (get) Token: 0x06002507 RID: 9479 RVA: 0x00010EAD File Offset: 0x0000F0AD
		// (set) Token: 0x06002508 RID: 9480 RVA: 0x00010EB5 File Offset: 0x0000F0B5
		[DataMember]
		public IList<DataSyncExternalCourseFinalExamInfoDTO> FinalExamInfos { get; set; }

		// Token: 0x17000CC2 RID: 3266
		// (get) Token: 0x06002509 RID: 9481 RVA: 0x00010EBE File Offset: 0x0000F0BE
		// (set) Token: 0x0600250A RID: 9482 RVA: 0x00010EC6 File Offset: 0x0000F0C6
		[DataMember]
		public DataSyncExternalCourseStudentSpecificRowPartDTO StudentSpecificInfo { get; set; }

		// Token: 0x17000CC3 RID: 3267
		// (get) Token: 0x0600250B RID: 9483 RVA: 0x00010ECF File Offset: 0x0000F0CF
		// (set) Token: 0x0600250C RID: 9484 RVA: 0x00010ED7 File Offset: 0x0000F0D7
		[DataMember]
		public string CourseNote { get; set; }

		// Token: 0x17000CC4 RID: 3268
		// (get) Token: 0x0600250D RID: 9485 RVA: 0x00010EE0 File Offset: 0x0000F0E0
		// (set) Token: 0x0600250E RID: 9486 RVA: 0x00010EE8 File Offset: 0x0000F0E8
		[DataMember]
		public string Department { get; set; }

		// Token: 0x17000CC5 RID: 3269
		// (get) Token: 0x0600250F RID: 9487 RVA: 0x00010EF1 File Offset: 0x0000F0F1
		// (set) Token: 0x06002510 RID: 9488 RVA: 0x00010EF9 File Offset: 0x0000F0F9
		[DataMember]
		public decimal Credits { get; set; }
	}
}
