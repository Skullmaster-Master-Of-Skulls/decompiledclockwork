using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x0200079E RID: 1950
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetUniqueCourseRegistrationStartDatesByAlternateContactResp
	{
		// Token: 0x17000DED RID: 3565
		// (get) Token: 0x06002807 RID: 10247 RVA: 0x00012D62 File Offset: 0x00010F62
		// (set) Token: 0x06002808 RID: 10248 RVA: 0x00012D6A File Offset: 0x00010F6A
		[DataMember]
		public IList<DateTime> Dates { get; set; }
	}
}
