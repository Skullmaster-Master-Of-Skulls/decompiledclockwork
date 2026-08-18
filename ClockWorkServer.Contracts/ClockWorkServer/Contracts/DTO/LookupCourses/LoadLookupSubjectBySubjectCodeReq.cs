using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x02000805 RID: 2053
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadLookupSubjectBySubjectCodeReq : BaseMessageReq
	{
		// Token: 0x17000E9A RID: 3738
		// (get) Token: 0x060029D7 RID: 10711 RVA: 0x00013DD6 File Offset: 0x00011FD6
		// (set) Token: 0x060029D8 RID: 10712 RVA: 0x00013DDE File Offset: 0x00011FDE
		[DataMember]
		public string SubjectCode { get; set; }
	}
}
