using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009BE RID: 2494
	[DataContract(Namespace = "http://tpro.ca")]
	public class ClearAutoTestBookingCacheReq : BaseMessageReq
	{
		// Token: 0x17001294 RID: 4756
		// (get) Token: 0x060033B9 RID: 13241 RVA: 0x00019298 File Offset: 0x00017498
		// (set) Token: 0x060033BA RID: 13242 RVA: 0x000192A0 File Offset: 0x000174A0
		[DataMember]
		public eTestExamSettingType TestType { get; set; }

		// Token: 0x17001295 RID: 4757
		// (get) Token: 0x060033BB RID: 13243 RVA: 0x000192A9 File Offset: 0x000174A9
		// (set) Token: 0x060033BC RID: 13244 RVA: 0x000192B1 File Offset: 0x000174B1
		[DataMember]
		public string OptionalClockWorkSettingsInstanceName { get; set; }
	}
}
