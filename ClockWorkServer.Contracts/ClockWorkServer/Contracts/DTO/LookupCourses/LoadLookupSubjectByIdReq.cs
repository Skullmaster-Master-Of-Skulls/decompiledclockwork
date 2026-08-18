using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x02000801 RID: 2049
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadLookupSubjectByIdReq : BaseMessageReq
	{
		// Token: 0x17000E96 RID: 3734
		// (get) Token: 0x060029CB RID: 10699 RVA: 0x00013D92 File Offset: 0x00011F92
		// (set) Token: 0x060029CC RID: 10700 RVA: 0x00013D9A File Offset: 0x00011F9A
		[DataMember]
		public int SubjectId { get; set; }
	}
}
