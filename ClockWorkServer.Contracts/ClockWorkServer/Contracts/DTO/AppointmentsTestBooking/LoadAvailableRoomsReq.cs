using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009C3 RID: 2499
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAvailableRoomsReq : BaseMessageReq
	{
		// Token: 0x1700129E RID: 4766
		// (get) Token: 0x060033D2 RID: 13266 RVA: 0x00019342 File Offset: 0x00017542
		// (set) Token: 0x060033D3 RID: 13267 RVA: 0x0001934A File Offset: 0x0001754A
		[DataMember]
		public eTestExamSettingType TestType { get; set; }

		// Token: 0x1700129F RID: 4767
		// (get) Token: 0x060033D4 RID: 13268 RVA: 0x00019353 File Offset: 0x00017553
		// (set) Token: 0x060033D5 RID: 13269 RVA: 0x0001935B File Offset: 0x0001755B
		[DataMember]
		public string OptionalClockWorkSettingsInstanceName { get; set; }

		// Token: 0x170012A0 RID: 4768
		// (get) Token: 0x060033D6 RID: 13270 RVA: 0x00019364 File Offset: 0x00017564
		// (set) Token: 0x060033D7 RID: 13271 RVA: 0x0001936C File Offset: 0x0001756C
		[DataMember]
		public bool ClearCacheFirst { get; set; }
	}
}
