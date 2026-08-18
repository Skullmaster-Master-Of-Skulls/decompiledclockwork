using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x02000809 RID: 2057
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadLookupSubjectReq : BaseMessageReq
	{
		// Token: 0x17000E9E RID: 3742
		// (get) Token: 0x060029E3 RID: 10723 RVA: 0x00013E1A File Offset: 0x0001201A
		// (set) Token: 0x060029E4 RID: 10724 RVA: 0x00013E22 File Offset: 0x00012022
		[DataMember]
		public string SubjectCode { get; set; }

		// Token: 0x17000E9F RID: 3743
		// (get) Token: 0x060029E5 RID: 10725 RVA: 0x00013E2B File Offset: 0x0001202B
		// (set) Token: 0x060029E6 RID: 10726 RVA: 0x00013E33 File Offset: 0x00012033
		[DataMember]
		public string SubjectDescription { get; set; }
	}
}
