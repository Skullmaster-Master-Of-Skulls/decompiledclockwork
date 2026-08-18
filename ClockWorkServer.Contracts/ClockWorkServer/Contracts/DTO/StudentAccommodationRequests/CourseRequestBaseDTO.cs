using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests
{
	// Token: 0x0200023E RID: 574
	[DataContract(Namespace = "http://tpro.ca")]
	public class CourseRequestBaseDTO
	{
		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000CFA RID: 3322 RVA: 0x00005F9F File Offset: 0x0000419F
		// (set) Token: 0x06000CFB RID: 3323 RVA: 0x00005FA7 File Offset: 0x000041A7
		[DataMember]
		public int CoursesId { get; set; }

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000CFC RID: 3324 RVA: 0x00005FB0 File Offset: 0x000041B0
		// (set) Token: 0x06000CFD RID: 3325 RVA: 0x00005FB8 File Offset: 0x000041B8
		[DataMember]
		public int StudentCourseAccommodationRequestId { get; set; }

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000CFE RID: 3326 RVA: 0x00005FC1 File Offset: 0x000041C1
		// (set) Token: 0x06000CFF RID: 3327 RVA: 0x00005FC9 File Offset: 0x000041C9
		[DataMember]
		public eStudentCourseAccommodationRequestStatusDTO Status { get; set; }

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000D00 RID: 3328 RVA: 0x00005FD2 File Offset: 0x000041D2
		// (set) Token: 0x06000D01 RID: 3329 RVA: 0x00005FDA File Offset: 0x000041DA
		[DataMember]
		public DateTime? DateRequested { get; set; }

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000D02 RID: 3330 RVA: 0x00005FE3 File Offset: 0x000041E3
		// (set) Token: 0x06000D03 RID: 3331 RVA: 0x00005FEB File Offset: 0x000041EB
		[DataMember]
		public PersonBaseDTO WhoEntered { get; set; }

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000D04 RID: 3332 RVA: 0x00005FF4 File Offset: 0x000041F4
		// (set) Token: 0x06000D05 RID: 3333 RVA: 0x00005FFC File Offset: 0x000041FC
		[DataMember]
		public DateTime DateEntered { get; set; }

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000D06 RID: 3334 RVA: 0x00006005 File Offset: 0x00004205
		// (set) Token: 0x06000D07 RID: 3335 RVA: 0x0000600D File Offset: 0x0000420D
		[DataMember]
		public string Note1 { get; set; }

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000D08 RID: 3336 RVA: 0x00006016 File Offset: 0x00004216
		// (set) Token: 0x06000D09 RID: 3337 RVA: 0x0000601E File Offset: 0x0000421E
		[DataMember]
		public string Note2 { get; set; }
	}
}
