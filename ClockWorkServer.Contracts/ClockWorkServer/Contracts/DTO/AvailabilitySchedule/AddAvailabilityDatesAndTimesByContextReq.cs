using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule
{
	// Token: 0x020008C9 RID: 2249
	[DataContract(Namespace = "http://tpro.ca")]
	public class AddAvailabilityDatesAndTimesByContextReq : BaseMessageReq
	{
		// Token: 0x1700100B RID: 4107
		// (get) Token: 0x06002D83 RID: 11651 RVA: 0x00015850 File Offset: 0x00013A50
		// (set) Token: 0x06002D84 RID: 11652 RVA: 0x00015858 File Offset: 0x00013A58
		[DataMember]
		public AvailabilityScheduleContextDTO Context { get; set; }

		// Token: 0x1700100C RID: 4108
		// (get) Token: 0x06002D85 RID: 11653 RVA: 0x00015861 File Offset: 0x00013A61
		// (set) Token: 0x06002D86 RID: 11654 RVA: 0x00015869 File Offset: 0x00013A69
		[DataMember]
		public IList<DateTime> Dates { get; set; }

		// Token: 0x1700100D RID: 4109
		// (get) Token: 0x06002D87 RID: 11655 RVA: 0x00015872 File Offset: 0x00013A72
		// (set) Token: 0x06002D88 RID: 11656 RVA: 0x0001587A File Offset: 0x00013A7A
		[DataMember]
		public IList<AvailabilityScheduleTimeDTO> Times { get; set; }

		// Token: 0x1700100E RID: 4110
		// (get) Token: 0x06002D89 RID: 11657 RVA: 0x00015883 File Offset: 0x00013A83
		// (set) Token: 0x06002D8A RID: 11658 RVA: 0x0001588B File Offset: 0x00013A8B
		[DataMember]
		public bool AbortIfAnyProblems { get; set; }
	}
}
