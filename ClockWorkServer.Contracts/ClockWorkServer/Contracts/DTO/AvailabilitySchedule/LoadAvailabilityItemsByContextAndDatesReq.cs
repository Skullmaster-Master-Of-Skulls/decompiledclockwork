using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule
{
	// Token: 0x020008C5 RID: 2245
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAvailabilityItemsByContextAndDatesReq : BaseMessageReq
	{
		// Token: 0x17001003 RID: 4099
		// (get) Token: 0x06002D6F RID: 11631 RVA: 0x000157C8 File Offset: 0x000139C8
		// (set) Token: 0x06002D70 RID: 11632 RVA: 0x000157D0 File Offset: 0x000139D0
		[DataMember]
		public AvailabilityScheduleContextDTO Context { get; set; }

		// Token: 0x17001004 RID: 4100
		// (get) Token: 0x06002D71 RID: 11633 RVA: 0x000157D9 File Offset: 0x000139D9
		// (set) Token: 0x06002D72 RID: 11634 RVA: 0x000157E1 File Offset: 0x000139E1
		[DataMember]
		public IList<DateTime> Days { get; set; }
	}
}
