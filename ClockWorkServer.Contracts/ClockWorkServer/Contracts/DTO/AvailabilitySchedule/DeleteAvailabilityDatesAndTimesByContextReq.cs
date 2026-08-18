using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule
{
	// Token: 0x020008CD RID: 2253
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteAvailabilityDatesAndTimesByContextReq : BaseMessageReq
	{
		// Token: 0x17001013 RID: 4115
		// (get) Token: 0x06002D97 RID: 11671 RVA: 0x000158D8 File Offset: 0x00013AD8
		// (set) Token: 0x06002D98 RID: 11672 RVA: 0x000158E0 File Offset: 0x00013AE0
		[DataMember]
		public AvailabilityScheduleContextDTO Context { get; set; }

		// Token: 0x17001014 RID: 4116
		// (get) Token: 0x06002D99 RID: 11673 RVA: 0x000158E9 File Offset: 0x00013AE9
		// (set) Token: 0x06002D9A RID: 11674 RVA: 0x000158F1 File Offset: 0x00013AF1
		[DataMember]
		public IList<AvailabilityScheduleDateAndTimeDTO> DayAndTimes { get; set; }
	}
}
