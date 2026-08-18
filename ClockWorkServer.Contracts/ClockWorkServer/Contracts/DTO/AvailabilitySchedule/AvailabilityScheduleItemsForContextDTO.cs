using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule
{
	// Token: 0x020008BE RID: 2238
	[DataContract(Namespace = "http://tpro.ca")]
	public class AvailabilityScheduleItemsForContextDTO
	{
		// Token: 0x17000FF5 RID: 4085
		// (get) Token: 0x06002D4C RID: 11596 RVA: 0x000156DA File Offset: 0x000138DA
		// (set) Token: 0x06002D4D RID: 11597 RVA: 0x000156E2 File Offset: 0x000138E2
		[DataMember]
		public AvailabilityScheduleContextDTO Context { get; set; }

		// Token: 0x17000FF6 RID: 4086
		// (get) Token: 0x06002D4E RID: 11598 RVA: 0x000156EB File Offset: 0x000138EB
		// (set) Token: 0x06002D4F RID: 11599 RVA: 0x000156F3 File Offset: 0x000138F3
		[DataMember]
		public IList<AvailabilityScheduleItemInfoDTO> AvailabilityScheduleItems { get; set; }
	}
}
