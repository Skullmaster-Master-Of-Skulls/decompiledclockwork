using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.CourseRegistrations;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000620 RID: 1568
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadStudentsRegisteredCoursesWithAccommodationsAndRequestsResp
	{
		// Token: 0x17000AA2 RID: 2722
		// (get) Token: 0x06001FDE RID: 8158 RVA: 0x0000E780 File Offset: 0x0000C980
		// (set) Token: 0x06001FDF RID: 8159 RVA: 0x0000E788 File Offset: 0x0000C988
		[DataMember]
		public IList<CourseRegistrationWithAccommodationsDTO> CourseRegistrationsWithAccommodations { get; set; }
	}
}
