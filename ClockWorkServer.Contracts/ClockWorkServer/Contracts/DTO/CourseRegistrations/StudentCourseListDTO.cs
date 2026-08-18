using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CourseRegistrations
{
	// Token: 0x02000834 RID: 2100
	[DataContract(Namespace = "http://tpro.ca")]
	public class StudentCourseListDTO
	{
		// Token: 0x17000F01 RID: 3841
		// (get) Token: 0x06002AD7 RID: 10967 RVA: 0x0001458F File Offset: 0x0001278F
		// (set) Token: 0x06002AD8 RID: 10968 RVA: 0x00014597 File Offset: 0x00012797
		[DataMember]
		public IList<CourseRegistrationDTO> Courses { get; set; }

		// Token: 0x17000F02 RID: 3842
		// (get) Token: 0x06002AD9 RID: 10969 RVA: 0x000145A0 File Offset: 0x000127A0
		// (set) Token: 0x06002ADA RID: 10970 RVA: 0x000145A8 File Offset: 0x000127A8
		[DataMember]
		public bool AtLeastOneCourseRemovedBecauseOfSpecialAccommodationNotAllowedToBookRestriction { get; set; }
	}
}
