using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule
{
	// Token: 0x020008C0 RID: 2240
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadUnbookedAvailabilityItemsByMultipleContextsAndDateRangeResp
	{
		// Token: 0x17000FFA RID: 4090
		// (get) Token: 0x06002D58 RID: 11608 RVA: 0x0001572F File Offset: 0x0001392F
		// (set) Token: 0x06002D59 RID: 11609 RVA: 0x00015737 File Offset: 0x00013937
		[DataMember]
		public IList<AvailabilityScheduleItemsForContextDTO> Result { get; set; }
	}
}
