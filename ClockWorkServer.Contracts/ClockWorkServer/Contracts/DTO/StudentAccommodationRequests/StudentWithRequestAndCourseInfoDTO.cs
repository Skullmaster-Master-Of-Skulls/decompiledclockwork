using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests
{
	// Token: 0x0200025B RID: 603
	[DataContract(Namespace = "http://tpro.ca")]
	public class StudentWithRequestAndCourseInfoDTO
	{
		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000DC7 RID: 3527 RVA: 0x000067A9 File Offset: 0x000049A9
		// (set) Token: 0x06000DC8 RID: 3528 RVA: 0x000067B1 File Offset: 0x000049B1
		[DataMember]
		public int StudentCourseAccommodationRequestId { get; set; }

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000DC9 RID: 3529 RVA: 0x000067BA File Offset: 0x000049BA
		// (set) Token: 0x06000DCA RID: 3530 RVA: 0x000067C2 File Offset: 0x000049C2
		[DataMember]
		public PersonBaseDTO Student { get; set; }

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000DCB RID: 3531 RVA: 0x000067CB File Offset: 0x000049CB
		// (set) Token: 0x06000DCC RID: 3532 RVA: 0x000067D3 File Offset: 0x000049D3
		[DataMember]
		public eStudentCourseAccommodationRequestStatusDTO Status { get; set; }

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000DCD RID: 3533 RVA: 0x000067DC File Offset: 0x000049DC
		// (set) Token: 0x06000DCE RID: 3534 RVA: 0x000067E4 File Offset: 0x000049E4
		[DataMember]
		public LookupCourseBaseDTO CourseBase { get; set; }

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000DCF RID: 3535 RVA: 0x000067ED File Offset: 0x000049ED
		// (set) Token: 0x06000DD0 RID: 3536 RVA: 0x000067F5 File Offset: 0x000049F5
		[DataMember]
		public DateTime? DateLetterReturned { get; set; }

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000DD1 RID: 3537 RVA: 0x000067FE File Offset: 0x000049FE
		// (set) Token: 0x06000DD2 RID: 3538 RVA: 0x00006806 File Offset: 0x00004A06
		[DataMember]
		public DateTime RequestDate { get; set; }

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000DD3 RID: 3539 RVA: 0x0000680F File Offset: 0x00004A0F
		// (set) Token: 0x06000DD4 RID: 3540 RVA: 0x00006817 File Offset: 0x00004A17
		[DataMember]
		public DateTime? DateApproved { get; set; }
	}
}
