using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x0200080E RID: 2062
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadLookupTimetableItemReq : BaseMessageReq
	{
		// Token: 0x17000EA8 RID: 3752
		// (get) Token: 0x060029FC RID: 10748 RVA: 0x00013EC4 File Offset: 0x000120C4
		// (set) Token: 0x060029FD RID: 10749 RVA: 0x00013ECC File Offset: 0x000120CC
		[DataMember]
		public int TimetableId { get; set; }
	}
}
