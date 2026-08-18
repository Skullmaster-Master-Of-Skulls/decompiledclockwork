using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Accommodations;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CourseRegistrations
{
	// Token: 0x02000831 RID: 2097
	[DataContract(Namespace = "http://tpro.ca")]
	public class CourseRegistrationWithAccommodationsDTO
	{
		// Token: 0x17000EF5 RID: 3829
		// (get) Token: 0x06002ABA RID: 10938 RVA: 0x00014464 File Offset: 0x00012664
		// (set) Token: 0x06002ABB RID: 10939 RVA: 0x0001446C File Offset: 0x0001266C
		[DataMember]
		public CourseRegistrationDTO CourseReg { get; set; }

		// Token: 0x17000EF6 RID: 3830
		// (get) Token: 0x06002ABC RID: 10940 RVA: 0x00014475 File Offset: 0x00012675
		// (set) Token: 0x06002ABD RID: 10941 RVA: 0x0001447D File Offset: 0x0001267D
		[DataMember]
		public IList<AccommodationDataDTO> CourseOrTemplateAccommodations { get; set; }

		// Token: 0x17000EF7 RID: 3831
		// (get) Token: 0x06002ABE RID: 10942 RVA: 0x00014486 File Offset: 0x00012686
		// (set) Token: 0x06002ABF RID: 10943 RVA: 0x0001448E File Offset: 0x0001268E
		[DataMember]
		public bool? IsUsingTemplateAccommodations { get; set; }

		// Token: 0x17000EF8 RID: 3832
		// (get) Token: 0x06002AC0 RID: 10944 RVA: 0x00014497 File Offset: 0x00012697
		// (set) Token: 0x06002AC1 RID: 10945 RVA: 0x0001449F File Offset: 0x0001269F
		[DataMember]
		public int CoursesId { get; set; }
	}
}
