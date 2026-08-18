using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule
{
	// Token: 0x020008D2 RID: 2258
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadDaysWithAvailabilityResp
	{
		// Token: 0x1700101C RID: 4124
		// (get) Token: 0x06002DAE RID: 11694 RVA: 0x00015971 File Offset: 0x00013B71
		// (set) Token: 0x06002DAF RID: 11695 RVA: 0x00015979 File Offset: 0x00013B79
		[DataMember]
		public IList<DateTime> DaysWithAvailability { get; set; }
	}
}
