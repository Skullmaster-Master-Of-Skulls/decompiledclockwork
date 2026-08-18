using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule
{
	// Token: 0x020008CC RID: 2252
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteAvailabilityTimeByContextResp
	{
		// Token: 0x17001012 RID: 4114
		// (get) Token: 0x06002D94 RID: 11668 RVA: 0x000158C7 File Offset: 0x00013AC7
		// (set) Token: 0x06002D95 RID: 11669 RVA: 0x000158CF File Offset: 0x00013ACF
		[DataMember]
		public DeleteAvailabilityActionResultDTO Result { get; set; }
	}
}
