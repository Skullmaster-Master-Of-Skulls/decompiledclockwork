using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule
{
	// Token: 0x020008C6 RID: 2246
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAvailabilityItemsByContextAndDatesResp
	{
		// Token: 0x17001005 RID: 4101
		// (get) Token: 0x06002D74 RID: 11636 RVA: 0x000157EA File Offset: 0x000139EA
		// (set) Token: 0x06002D75 RID: 11637 RVA: 0x000157F2 File Offset: 0x000139F2
		[DataMember]
		public AvailabilityScheduleItemsForContextDTO Result { get; set; }
	}
}
