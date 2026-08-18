using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007AD RID: 1965
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadCourseByLuCourseIdResp
	{
		// Token: 0x17000E13 RID: 3603
		// (get) Token: 0x06002868 RID: 10344 RVA: 0x000132E4 File Offset: 0x000114E4
		// (set) Token: 0x06002869 RID: 10345 RVA: 0x000132EC File Offset: 0x000114EC
		[DataMember]
		public LookupCourseDTO Course { get; set; }
	}
}
