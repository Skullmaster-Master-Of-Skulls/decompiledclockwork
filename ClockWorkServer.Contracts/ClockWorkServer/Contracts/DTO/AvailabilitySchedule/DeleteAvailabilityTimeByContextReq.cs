using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule
{
	// Token: 0x020008CB RID: 2251
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteAvailabilityTimeByContextReq : BaseMessageReq
	{
		// Token: 0x17001010 RID: 4112
		// (get) Token: 0x06002D8F RID: 11663 RVA: 0x000158A5 File Offset: 0x00013AA5
		// (set) Token: 0x06002D90 RID: 11664 RVA: 0x000158AD File Offset: 0x00013AAD
		[DataMember]
		public AvailabilityScheduleContextDTO Context { get; set; }

		// Token: 0x17001011 RID: 4113
		// (get) Token: 0x06002D91 RID: 11665 RVA: 0x000158B6 File Offset: 0x00013AB6
		// (set) Token: 0x06002D92 RID: 11666 RVA: 0x000158BE File Offset: 0x00013ABE
		[DataMember]
		public AvailabilityScheduleDateAndTimeDTO DayAndTime { get; set; }
	}
}
