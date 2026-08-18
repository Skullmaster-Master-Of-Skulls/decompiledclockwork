using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007AA RID: 1962
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateLookupCourseReq : BaseMessageReq
	{
		// Token: 0x17000E10 RID: 3600
		// (get) Token: 0x0600285F RID: 10335 RVA: 0x000132B1 File Offset: 0x000114B1
		// (set) Token: 0x06002860 RID: 10336 RVA: 0x000132B9 File Offset: 0x000114B9
		[DataMember]
		public LookupCourseDTO Course { get; set; }
	}
}
