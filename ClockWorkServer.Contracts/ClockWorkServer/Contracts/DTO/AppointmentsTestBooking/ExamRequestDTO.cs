using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Accommodations;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009B0 RID: 2480
	[DataContract(Namespace = "http://tpro.ca")]
	public class ExamRequestDTO
	{
		// Token: 0x170011F8 RID: 4600
		// (get) Token: 0x06003273 RID: 12915 RVA: 0x00018811 File Offset: 0x00016A11
		// (set) Token: 0x06003274 RID: 12916 RVA: 0x00018819 File Offset: 0x00016A19
		[DataMember]
		public int ExamRequestId { get; set; }

		// Token: 0x170011F9 RID: 4601
		// (get) Token: 0x06003275 RID: 12917 RVA: 0x00018822 File Offset: 0x00016A22
		// (set) Token: 0x06003276 RID: 12918 RVA: 0x0001882A File Offset: 0x00016A2A
		[DataMember]
		public LookupCourseBaseWithPrimaryInstructorDTO Course { get; set; }

		// Token: 0x170011FA RID: 4602
		// (get) Token: 0x06003277 RID: 12919 RVA: 0x00018833 File Offset: 0x00016A33
		// (set) Token: 0x06003278 RID: 12920 RVA: 0x0001883B File Offset: 0x00016A3B
		[DataMember]
		public PersonBaseDTO Student { get; set; }

		// Token: 0x170011FB RID: 4603
		// (get) Token: 0x06003279 RID: 12921 RVA: 0x00018844 File Offset: 0x00016A44
		// (set) Token: 0x0600327A RID: 12922 RVA: 0x0001884C File Offset: 0x00016A4C
		[DataMember]
		public DateTime DateEntered { get; set; }

		// Token: 0x170011FC RID: 4604
		// (get) Token: 0x0600327B RID: 12923 RVA: 0x00018855 File Offset: 0x00016A55
		// (set) Token: 0x0600327C RID: 12924 RVA: 0x0001885D File Offset: 0x00016A5D
		[DataMember]
		public string InstructorName { get; set; }

		// Token: 0x170011FD RID: 4605
		// (get) Token: 0x0600327D RID: 12925 RVA: 0x00018866 File Offset: 0x00016A66
		// (set) Token: 0x0600327E RID: 12926 RVA: 0x0001886E File Offset: 0x00016A6E
		[DataMember]
		public string InstructorEmail { get; set; }

		// Token: 0x170011FE RID: 4606
		// (get) Token: 0x0600327F RID: 12927 RVA: 0x00018877 File Offset: 0x00016A77
		// (set) Token: 0x06003280 RID: 12928 RVA: 0x0001887F File Offset: 0x00016A7F
		[DataMember]
		public IList<AccommodationDataDTO> AccommodationsSelected { get; set; }

		// Token: 0x170011FF RID: 4607
		// (get) Token: 0x06003281 RID: 12929 RVA: 0x00018888 File Offset: 0x00016A88
		// (set) Token: 0x06003282 RID: 12930 RVA: 0x00018890 File Offset: 0x00016A90
		[DataMember]
		public DateTime ClassTestStartDateTime { get; set; }

		// Token: 0x17001200 RID: 4608
		// (get) Token: 0x06003283 RID: 12931 RVA: 0x00018899 File Offset: 0x00016A99
		// (set) Token: 0x06003284 RID: 12932 RVA: 0x000188A1 File Offset: 0x00016AA1
		[DataMember]
		public DateTime ClassTestEndDateTime { get; set; }

		// Token: 0x17001201 RID: 4609
		// (get) Token: 0x06003285 RID: 12933 RVA: 0x000188AA File Offset: 0x00016AAA
		// (set) Token: 0x06003286 RID: 12934 RVA: 0x000188B2 File Offset: 0x00016AB2
		[DataMember]
		public string ClassTestDescription { get; set; }

		// Token: 0x17001202 RID: 4610
		// (get) Token: 0x06003287 RID: 12935 RVA: 0x000188BB File Offset: 0x00016ABB
		// (set) Token: 0x06003288 RID: 12936 RVA: 0x000188C3 File Offset: 0x00016AC3
		[DataMember]
		public string InstructorSubmittedDescription { get; set; }
	}
}
