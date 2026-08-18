using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule
{
	// Token: 0x020008C2 RID: 2242
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAvailabilityItemsByMultipleContextsAndDateRangeResp
	{
		// Token: 0x17000FFE RID: 4094
		// (get) Token: 0x06002D62 RID: 11618 RVA: 0x00015773 File Offset: 0x00013973
		// (set) Token: 0x06002D63 RID: 11619 RVA: 0x0001577B File Offset: 0x0001397B
		[DataMember]
		public IList<AvailabilityScheduleItemsForContextDTO> Result { get; set; }
	}
}
