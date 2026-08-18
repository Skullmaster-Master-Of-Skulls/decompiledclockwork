using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B85 RID: 2949
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetMediaContentCoursesResp
	{
		// Token: 0x170016ED RID: 5869
		// (get) Token: 0x06003E45 RID: 15941 RVA: 0x0001E851 File Offset: 0x0001CA51
		// (set) Token: 0x06003E46 RID: 15942 RVA: 0x0001E859 File Offset: 0x0001CA59
		[DataMember]
		public IList<LookupCourseBaseDTO> Courses { get; set; }
	}
}
