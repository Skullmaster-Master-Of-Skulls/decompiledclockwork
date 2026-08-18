using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule
{
	// Token: 0x020008C3 RID: 2243
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAvailabilityItemsByContextAndDateRangeReq : BaseMessageReq
	{
		// Token: 0x17000FFF RID: 4095
		// (get) Token: 0x06002D65 RID: 11621 RVA: 0x00015784 File Offset: 0x00013984
		// (set) Token: 0x06002D66 RID: 11622 RVA: 0x0001578C File Offset: 0x0001398C
		[DataMember]
		public AvailabilityScheduleContextDTO Context { get; set; }

		// Token: 0x17001000 RID: 4096
		// (get) Token: 0x06002D67 RID: 11623 RVA: 0x00015795 File Offset: 0x00013995
		// (set) Token: 0x06002D68 RID: 11624 RVA: 0x0001579D File Offset: 0x0001399D
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x17001001 RID: 4097
		// (get) Token: 0x06002D69 RID: 11625 RVA: 0x000157A6 File Offset: 0x000139A6
		// (set) Token: 0x06002D6A RID: 11626 RVA: 0x000157AE File Offset: 0x000139AE
		[DataMember]
		public int NumDays { get; set; }
	}
}
