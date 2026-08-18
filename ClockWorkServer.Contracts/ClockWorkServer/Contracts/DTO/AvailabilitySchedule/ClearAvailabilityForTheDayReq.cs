using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule
{
	// Token: 0x020008CF RID: 2255
	[DataContract(Namespace = "http://tpro.ca")]
	public class ClearAvailabilityForTheDayReq : BaseMessageReq
	{
		// Token: 0x17001016 RID: 4118
		// (get) Token: 0x06002D9F RID: 11679 RVA: 0x0001590B File Offset: 0x00013B0B
		// (set) Token: 0x06002DA0 RID: 11680 RVA: 0x00015913 File Offset: 0x00013B13
		[DataMember]
		public AvailabilityScheduleContextDTO Context { get; set; }

		// Token: 0x17001017 RID: 4119
		// (get) Token: 0x06002DA1 RID: 11681 RVA: 0x0001591C File Offset: 0x00013B1C
		// (set) Token: 0x06002DA2 RID: 11682 RVA: 0x00015924 File Offset: 0x00013B24
		[DataMember]
		public IList<DateTime> Days { get; set; }
	}
}
