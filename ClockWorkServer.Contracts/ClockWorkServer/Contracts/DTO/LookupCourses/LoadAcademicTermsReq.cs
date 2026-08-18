using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007BC RID: 1980
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAcademicTermsReq : BaseMessageReq
	{
		// Token: 0x17000E26 RID: 3622
		// (get) Token: 0x0600289D RID: 10397 RVA: 0x00013427 File Offset: 0x00011627
		// (set) Token: 0x0600289E RID: 10398 RVA: 0x0001342F File Offset: 0x0001162F
		[DataMember]
		public bool IgnoreCache { get; set; }
	}
}
