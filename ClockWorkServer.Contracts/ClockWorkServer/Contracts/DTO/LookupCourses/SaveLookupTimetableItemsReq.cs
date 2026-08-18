using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x02000810 RID: 2064
	[DataContract(Namespace = "http://tpro.ca")]
	public class SaveLookupTimetableItemsReq : BaseMessageReq
	{
		// Token: 0x17000EAA RID: 3754
		// (get) Token: 0x06002A02 RID: 10754 RVA: 0x00013EE6 File Offset: 0x000120E6
		// (set) Token: 0x06002A03 RID: 10755 RVA: 0x00013EEE File Offset: 0x000120EE
		[DataMember]
		public int LuCourseId { get; set; }

		// Token: 0x17000EAB RID: 3755
		// (get) Token: 0x06002A04 RID: 10756 RVA: 0x00013EF7 File Offset: 0x000120F7
		// (set) Token: 0x06002A05 RID: 10757 RVA: 0x00013EFF File Offset: 0x000120FF
		[DataMember]
		public IList<LookupTimetableItemDTO> TimetableItems { get; set; }
	}
}
