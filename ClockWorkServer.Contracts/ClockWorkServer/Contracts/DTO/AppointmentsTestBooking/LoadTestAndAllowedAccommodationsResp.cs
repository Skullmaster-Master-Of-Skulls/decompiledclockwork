using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Accommodations;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x02000A34 RID: 2612
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadTestAndAllowedAccommodationsResp
	{
		// Token: 0x1700136E RID: 4974
		// (get) Token: 0x060035E7 RID: 13799 RVA: 0x0001A221 File Offset: 0x00018421
		// (set) Token: 0x060035E8 RID: 13800 RVA: 0x0001A229 File Offset: 0x00018429
		[DataMember]
		public IList<AccommodationDataDTO> AllowedAccommodations { get; set; }

		// Token: 0x1700136F RID: 4975
		// (get) Token: 0x060035E9 RID: 13801 RVA: 0x0001A232 File Offset: 0x00018432
		// (set) Token: 0x060035EA RID: 13802 RVA: 0x0001A23A File Offset: 0x0001843A
		[DataMember]
		public IList<AccommodationDataDTO> TestAccommodations { get; set; }

		// Token: 0x17001370 RID: 4976
		// (get) Token: 0x060035EB RID: 13803 RVA: 0x0001A243 File Offset: 0x00018443
		// (set) Token: 0x060035EC RID: 13804 RVA: 0x0001A24B File Offset: 0x0001844B
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x17001371 RID: 4977
		// (get) Token: 0x060035ED RID: 13805 RVA: 0x0001A254 File Offset: 0x00018454
		// (set) Token: 0x060035EE RID: 13806 RVA: 0x0001A25C File Offset: 0x0001845C
		[DataMember]
		public int LuCourseId { get; set; }
	}
}
