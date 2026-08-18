using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007AB RID: 1963
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateLookupCourseResp
	{
		// Token: 0x17000E11 RID: 3601
		// (get) Token: 0x06002862 RID: 10338 RVA: 0x000132C2 File Offset: 0x000114C2
		// (set) Token: 0x06002863 RID: 10339 RVA: 0x000132CA File Offset: 0x000114CA
		[DataMember]
		public int LuCourseId { get; set; }
	}
}
