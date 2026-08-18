using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.CourseRegistrations;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000622 RID: 1570
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadStudentsRegisteredCoursesWithAccommodationsResp
	{
		// Token: 0x17000AA8 RID: 2728
		// (get) Token: 0x06001FEC RID: 8172 RVA: 0x0000E7E6 File Offset: 0x0000C9E6
		// (set) Token: 0x06001FED RID: 8173 RVA: 0x0000E7EE File Offset: 0x0000C9EE
		[DataMember]
		public IList<CourseRegistrationWithAccommodationsDTO> CourseRegistrationsWithAccommodations { get; set; }
	}
}
