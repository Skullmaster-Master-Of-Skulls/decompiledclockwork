using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.AutoTestBookingHelper;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009BC RID: 2492
	[DataContract(Namespace = "http://tpro.ca")]
	public class ApplySpecialAccommodations2Req : BaseMessageReq
	{
		// Token: 0x17001289 RID: 4745
		// (get) Token: 0x060033A1 RID: 13217 RVA: 0x000191DD File Offset: 0x000173DD
		// (set) Token: 0x060033A2 RID: 13218 RVA: 0x000191E5 File Offset: 0x000173E5
		[DataMember]
		public eTestExamSettingType TestType { get; set; }

		// Token: 0x1700128A RID: 4746
		// (get) Token: 0x060033A3 RID: 13219 RVA: 0x000191EE File Offset: 0x000173EE
		// (set) Token: 0x060033A4 RID: 13220 RVA: 0x000191F6 File Offset: 0x000173F6
		[DataMember]
		public bool DebugMode { get; set; }

		// Token: 0x1700128B RID: 4747
		// (get) Token: 0x060033A5 RID: 13221 RVA: 0x000191FF File Offset: 0x000173FF
		// (set) Token: 0x060033A6 RID: 13222 RVA: 0x00019207 File Offset: 0x00017407
		[DataMember]
		public eAutoTestBookingContext TestBookingContext { get; set; }

		// Token: 0x1700128C RID: 4748
		// (get) Token: 0x060033A7 RID: 13223 RVA: 0x00019210 File Offset: 0x00017410
		// (set) Token: 0x060033A8 RID: 13224 RVA: 0x00019218 File Offset: 0x00017418
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x1700128D RID: 4749
		// (get) Token: 0x060033A9 RID: 13225 RVA: 0x00019221 File Offset: 0x00017421
		// (set) Token: 0x060033AA RID: 13226 RVA: 0x00019229 File Offset: 0x00017429
		[DataMember]
		public int LuCourseId { get; set; }

		// Token: 0x1700128E RID: 4750
		// (get) Token: 0x060033AB RID: 13227 RVA: 0x00019232 File Offset: 0x00017432
		// (set) Token: 0x060033AC RID: 13228 RVA: 0x0001923A File Offset: 0x0001743A
		[DataMember]
		public DateTime ClassStartDateTime { get; set; }

		// Token: 0x1700128F RID: 4751
		// (get) Token: 0x060033AD RID: 13229 RVA: 0x00019243 File Offset: 0x00017443
		// (set) Token: 0x060033AE RID: 13230 RVA: 0x0001924B File Offset: 0x0001744B
		[DataMember]
		public DateTime ClassEndDateTime { get; set; }

		// Token: 0x17001290 RID: 4752
		// (get) Token: 0x060033AF RID: 13231 RVA: 0x00019254 File Offset: 0x00017454
		// (set) Token: 0x060033B0 RID: 13232 RVA: 0x0001925C File Offset: 0x0001745C
		[DataMember]
		public IList<AccommodationDTO> AccommodationsToUse { get; set; }

		// Token: 0x17001291 RID: 4753
		// (get) Token: 0x060033B1 RID: 13233 RVA: 0x00019265 File Offset: 0x00017465
		// (set) Token: 0x060033B2 RID: 13234 RVA: 0x0001926D File Offset: 0x0001746D
		[DataMember]
		public string OptionalClockWorkSettingsInstanceName { get; set; }

		// Token: 0x17001292 RID: 4754
		// (get) Token: 0x060033B3 RID: 13235 RVA: 0x00019276 File Offset: 0x00017476
		// (set) Token: 0x060033B4 RID: 13236 RVA: 0x0001927E File Offset: 0x0001747E
		[DataMember]
		public bool ClearCacheFirst { get; set; }
	}
}
