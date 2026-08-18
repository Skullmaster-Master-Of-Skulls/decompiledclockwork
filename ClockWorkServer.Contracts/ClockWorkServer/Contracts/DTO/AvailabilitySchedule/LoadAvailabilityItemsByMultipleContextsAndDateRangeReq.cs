using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule
{
	// Token: 0x020008C1 RID: 2241
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAvailabilityItemsByMultipleContextsAndDateRangeReq : BaseMessageReq
	{
		// Token: 0x17000FFB RID: 4091
		// (get) Token: 0x06002D5B RID: 11611 RVA: 0x00015740 File Offset: 0x00013940
		// (set) Token: 0x06002D5C RID: 11612 RVA: 0x00015748 File Offset: 0x00013948
		[DataMember]
		public IList<AvailabilityScheduleContextDTO> Contexts { get; set; }

		// Token: 0x17000FFC RID: 4092
		// (get) Token: 0x06002D5D RID: 11613 RVA: 0x00015751 File Offset: 0x00013951
		// (set) Token: 0x06002D5E RID: 11614 RVA: 0x00015759 File Offset: 0x00013959
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x17000FFD RID: 4093
		// (get) Token: 0x06002D5F RID: 11615 RVA: 0x00015762 File Offset: 0x00013962
		// (set) Token: 0x06002D60 RID: 11616 RVA: 0x0001576A File Offset: 0x0001396A
		[DataMember]
		public int NumDays { get; set; }
	}
}
