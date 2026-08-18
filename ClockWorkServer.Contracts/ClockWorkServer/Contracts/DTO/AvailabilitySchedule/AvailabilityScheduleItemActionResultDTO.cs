using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.AvailabilitySchedule;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule
{
	// Token: 0x020008BC RID: 2236
	[DataContract(Namespace = "http://tpro.ca")]
	public class AvailabilityScheduleItemActionResultDTO
	{
		// Token: 0x17000FF1 RID: 4081
		// (get) Token: 0x06002D42 RID: 11586 RVA: 0x00015696 File Offset: 0x00013896
		// (set) Token: 0x06002D43 RID: 11587 RVA: 0x0001569E File Offset: 0x0001389E
		[DataMember]
		public eAvailabilityScheduleAction ActionTaken { get; set; }

		// Token: 0x17000FF2 RID: 4082
		// (get) Token: 0x06002D44 RID: 11588 RVA: 0x000156A7 File Offset: 0x000138A7
		// (set) Token: 0x06002D45 RID: 11589 RVA: 0x000156AF File Offset: 0x000138AF
		[DataMember]
		public eAvailabilityScheduleActionFailureReason FailureReason { get; set; }

		// Token: 0x17000FF3 RID: 4083
		// (get) Token: 0x06002D46 RID: 11590 RVA: 0x000156B8 File Offset: 0x000138B8
		// (set) Token: 0x06002D47 RID: 11591 RVA: 0x000156C0 File Offset: 0x000138C0
		[DataMember]
		public string PublicMessage { get; set; }
	}
}
