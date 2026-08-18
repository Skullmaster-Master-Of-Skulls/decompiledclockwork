using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule
{
	// Token: 0x020008BF RID: 2239
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadUnbookedAvailabilityItemsByMultipleContextsAndDateRangeReq : BaseMessageReq
	{
		// Token: 0x17000FF7 RID: 4087
		// (get) Token: 0x06002D51 RID: 11601 RVA: 0x000156FC File Offset: 0x000138FC
		// (set) Token: 0x06002D52 RID: 11602 RVA: 0x00015704 File Offset: 0x00013904
		[DataMember]
		public IList<AvailabilityScheduleContextDTO> Contexts { get; set; }

		// Token: 0x17000FF8 RID: 4088
		// (get) Token: 0x06002D53 RID: 11603 RVA: 0x0001570D File Offset: 0x0001390D
		// (set) Token: 0x06002D54 RID: 11604 RVA: 0x00015715 File Offset: 0x00013915
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x17000FF9 RID: 4089
		// (get) Token: 0x06002D55 RID: 11605 RVA: 0x0001571E File Offset: 0x0001391E
		// (set) Token: 0x06002D56 RID: 11606 RVA: 0x00015726 File Offset: 0x00013926
		[DataMember]
		public int NumDays { get; set; }
	}
}
