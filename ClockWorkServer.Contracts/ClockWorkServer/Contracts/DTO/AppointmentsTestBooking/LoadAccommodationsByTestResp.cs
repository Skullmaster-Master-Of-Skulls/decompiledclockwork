using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Accommodations;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x02000A2C RID: 2604
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAccommodationsByTestResp
	{
		// Token: 0x1700135E RID: 4958
		// (get) Token: 0x060035BF RID: 13759 RVA: 0x0001A111 File Offset: 0x00018311
		// (set) Token: 0x060035C0 RID: 13760 RVA: 0x0001A119 File Offset: 0x00018319
		[DataMember]
		public IList<AccommodationDataDTO> TestAccommodations { get; set; }

		// Token: 0x1700135F RID: 4959
		// (get) Token: 0x060035C1 RID: 13761 RVA: 0x0001A122 File Offset: 0x00018322
		// (set) Token: 0x060035C2 RID: 13762 RVA: 0x0001A12A File Offset: 0x0001832A
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x17001360 RID: 4960
		// (get) Token: 0x060035C3 RID: 13763 RVA: 0x0001A133 File Offset: 0x00018333
		// (set) Token: 0x060035C4 RID: 13764 RVA: 0x0001A13B File Offset: 0x0001833B
		[DataMember]
		public int LuCourseId { get; set; }
	}
}
