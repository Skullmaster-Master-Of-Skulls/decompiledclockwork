using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.AutoTestBookingHelper;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009CD RID: 2509
	[DataContract(Namespace = "http://tpro.ca")]
	public class CalculateBreakTimeReq : BaseMessageReq
	{
		// Token: 0x170012B1 RID: 4785
		// (get) Token: 0x06003402 RID: 13314 RVA: 0x00019485 File Offset: 0x00017685
		// (set) Token: 0x06003403 RID: 13315 RVA: 0x0001948D File Offset: 0x0001768D
		[DataMember]
		public eTestExamSettingType TestType { get; set; }

		// Token: 0x170012B2 RID: 4786
		// (get) Token: 0x06003404 RID: 13316 RVA: 0x00019496 File Offset: 0x00017696
		// (set) Token: 0x06003405 RID: 13317 RVA: 0x0001949E File Offset: 0x0001769E
		[DataMember]
		public int ClassTestDurationInMinutes { get; set; }

		// Token: 0x170012B3 RID: 4787
		// (get) Token: 0x06003406 RID: 13318 RVA: 0x000194A7 File Offset: 0x000176A7
		// (set) Token: 0x06003407 RID: 13319 RVA: 0x000194AF File Offset: 0x000176AF
		[DataMember]
		public IList<AccommodationDTO> AccommodationsToUse { get; set; }
	}
}
