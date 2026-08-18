using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009C7 RID: 2503
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadBaseAutoTestBookingSettingsReq : BaseMessageReq
	{
		// Token: 0x170012A6 RID: 4774
		// (get) Token: 0x060033E6 RID: 13286 RVA: 0x000193CA File Offset: 0x000175CA
		// (set) Token: 0x060033E7 RID: 13287 RVA: 0x000193D2 File Offset: 0x000175D2
		[DataMember]
		public eTestExamSettingType TestType { get; set; }

		// Token: 0x170012A7 RID: 4775
		// (get) Token: 0x060033E8 RID: 13288 RVA: 0x000193DB File Offset: 0x000175DB
		// (set) Token: 0x060033E9 RID: 13289 RVA: 0x000193E3 File Offset: 0x000175E3
		[DataMember]
		public eAutoTestBookingContext TestBookingContext { get; set; }

		// Token: 0x170012A8 RID: 4776
		// (get) Token: 0x060033EA RID: 13290 RVA: 0x000193EC File Offset: 0x000175EC
		// (set) Token: 0x060033EB RID: 13291 RVA: 0x000193F4 File Offset: 0x000175F4
		[DataMember]
		public bool ClearCacheFirst { get; set; }

		// Token: 0x170012A9 RID: 4777
		// (get) Token: 0x060033EC RID: 13292 RVA: 0x000193FD File Offset: 0x000175FD
		// (set) Token: 0x060033ED RID: 13293 RVA: 0x00019405 File Offset: 0x00017605
		[DataMember]
		public string OptionalClockWorkSettingsInstanceName { get; set; }
	}
}
