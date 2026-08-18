using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule
{
	// Token: 0x020008BD RID: 2237
	[DataContract(Namespace = "http://tpro.ca")]
	public class AvailabilityScheduleItemInfoDTO
	{
		// Token: 0x17000FF4 RID: 4084
		// (get) Token: 0x06002D49 RID: 11593 RVA: 0x000156C9 File Offset: 0x000138C9
		// (set) Token: 0x06002D4A RID: 11594 RVA: 0x000156D1 File Offset: 0x000138D1
		[DataMember]
		public AvailabilityScheduleDateAndTimeDTO DayAndTime { get; set; }
	}
}
