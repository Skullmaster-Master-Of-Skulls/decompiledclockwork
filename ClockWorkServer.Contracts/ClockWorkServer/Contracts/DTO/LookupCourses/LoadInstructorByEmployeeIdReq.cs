using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007E6 RID: 2022
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadInstructorByEmployeeIdReq : BaseMessageReq
	{
		// Token: 0x17000E67 RID: 3687
		// (get) Token: 0x0600294F RID: 10575 RVA: 0x000139EF File Offset: 0x00011BEF
		// (set) Token: 0x06002950 RID: 10576 RVA: 0x000139F7 File Offset: 0x00011BF7
		[DataMember]
		public string EmployeeId { get; set; }
	}
}
