using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule
{
	// Token: 0x020008C7 RID: 2247
	[DataContract(Namespace = "http://tpro.ca")]
	public class AddAvailabilityTimesByContextAndDateReq : BaseMessageReq
	{
		// Token: 0x17001006 RID: 4102
		// (get) Token: 0x06002D77 RID: 11639 RVA: 0x000157FB File Offset: 0x000139FB
		// (set) Token: 0x06002D78 RID: 11640 RVA: 0x00015803 File Offset: 0x00013A03
		[DataMember]
		public AvailabilityScheduleContextDTO Context { get; set; }

		// Token: 0x17001007 RID: 4103
		// (get) Token: 0x06002D79 RID: 11641 RVA: 0x0001580C File Offset: 0x00013A0C
		// (set) Token: 0x06002D7A RID: 11642 RVA: 0x00015814 File Offset: 0x00013A14
		[DataMember]
		public DateTime Date { get; set; }

		// Token: 0x17001008 RID: 4104
		// (get) Token: 0x06002D7B RID: 11643 RVA: 0x0001581D File Offset: 0x00013A1D
		// (set) Token: 0x06002D7C RID: 11644 RVA: 0x00015825 File Offset: 0x00013A25
		[DataMember]
		public IList<AvailabilityScheduleTimeDTO> Times { get; set; }

		// Token: 0x17001009 RID: 4105
		// (get) Token: 0x06002D7D RID: 11645 RVA: 0x0001582E File Offset: 0x00013A2E
		// (set) Token: 0x06002D7E RID: 11646 RVA: 0x00015836 File Offset: 0x00013A36
		[DataMember]
		public bool AbortIfAnyProblems { get; set; }
	}
}
