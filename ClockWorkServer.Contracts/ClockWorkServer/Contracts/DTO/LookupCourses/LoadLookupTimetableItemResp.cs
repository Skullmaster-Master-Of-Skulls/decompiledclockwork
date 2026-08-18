using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x0200080F RID: 2063
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadLookupTimetableItemResp
	{
		// Token: 0x17000EA9 RID: 3753
		// (get) Token: 0x060029FF RID: 10751 RVA: 0x00013ED5 File Offset: 0x000120D5
		// (set) Token: 0x06002A00 RID: 10752 RVA: 0x00013EDD File Offset: 0x000120DD
		[DataMember]
		public LookupTimetableItemDTO TimetableItem { get; set; }
	}
}
