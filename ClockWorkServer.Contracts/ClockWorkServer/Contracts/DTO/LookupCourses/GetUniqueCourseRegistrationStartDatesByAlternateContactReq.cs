using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x0200079D RID: 1949
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetUniqueCourseRegistrationStartDatesByAlternateContactReq : BaseMessageReq
	{
		// Token: 0x17000DEC RID: 3564
		// (get) Token: 0x06002804 RID: 10244 RVA: 0x00012D51 File Offset: 0x00010F51
		// (set) Token: 0x06002805 RID: 10245 RVA: 0x00012D59 File Offset: 0x00010F59
		[DataMember]
		public int AlternateContactId { get; set; }
	}
}
