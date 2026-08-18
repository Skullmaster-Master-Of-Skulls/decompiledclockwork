using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule
{
	// Token: 0x020008C4 RID: 2244
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAvailabilityItemsByContextAndDateRangeResp
	{
		// Token: 0x17001002 RID: 4098
		// (get) Token: 0x06002D6C RID: 11628 RVA: 0x000157B7 File Offset: 0x000139B7
		// (set) Token: 0x06002D6D RID: 11629 RVA: 0x000157BF File Offset: 0x000139BF
		[DataMember]
		public AvailabilityScheduleItemsForContextDTO Result { get; set; }
	}
}
