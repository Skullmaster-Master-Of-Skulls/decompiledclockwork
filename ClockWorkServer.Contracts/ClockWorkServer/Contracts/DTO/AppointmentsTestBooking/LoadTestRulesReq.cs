using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009C5 RID: 2501
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadTestRulesReq : BaseMessageReq
	{
		// Token: 0x170012A2 RID: 4770
		// (get) Token: 0x060033DC RID: 13276 RVA: 0x00019386 File Offset: 0x00017586
		// (set) Token: 0x060033DD RID: 13277 RVA: 0x0001938E File Offset: 0x0001758E
		[DataMember]
		public eTestExamSettingType TestType { get; set; }

		// Token: 0x170012A3 RID: 4771
		// (get) Token: 0x060033DE RID: 13278 RVA: 0x00019397 File Offset: 0x00017597
		// (set) Token: 0x060033DF RID: 13279 RVA: 0x0001939F File Offset: 0x0001759F
		[DataMember]
		public string OptionalClockWorkSettingsInstanceName { get; set; }

		// Token: 0x170012A4 RID: 4772
		// (get) Token: 0x060033E0 RID: 13280 RVA: 0x000193A8 File Offset: 0x000175A8
		// (set) Token: 0x060033E1 RID: 13281 RVA: 0x000193B0 File Offset: 0x000175B0
		[DataMember]
		public bool ClearCacheFirst { get; set; }
	}
}
