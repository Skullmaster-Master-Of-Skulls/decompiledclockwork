using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.AutoTestBookingHelper;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009CB RID: 2507
	[DataContract(Namespace = "http://tpro.ca")]
	public class CalculateExtraTimeReq : BaseMessageReq
	{
		// Token: 0x170012AD RID: 4781
		// (get) Token: 0x060033F8 RID: 13304 RVA: 0x00019441 File Offset: 0x00017641
		// (set) Token: 0x060033F9 RID: 13305 RVA: 0x00019449 File Offset: 0x00017649
		[DataMember]
		public eTestExamSettingType TestType { get; set; }

		// Token: 0x170012AE RID: 4782
		// (get) Token: 0x060033FA RID: 13306 RVA: 0x00019452 File Offset: 0x00017652
		// (set) Token: 0x060033FB RID: 13307 RVA: 0x0001945A File Offset: 0x0001765A
		[DataMember]
		public int ClassTestDurationInMinutes { get; set; }

		// Token: 0x170012AF RID: 4783
		// (get) Token: 0x060033FC RID: 13308 RVA: 0x00019463 File Offset: 0x00017663
		// (set) Token: 0x060033FD RID: 13309 RVA: 0x0001946B File Offset: 0x0001766B
		[DataMember]
		public IList<AccommodationDTO> AccommodationsToUse { get; set; }
	}
}
