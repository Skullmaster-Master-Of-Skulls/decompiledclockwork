using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009BF RID: 2495
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAvailableAssetsReq : BaseMessageReq
	{
		// Token: 0x17001296 RID: 4758
		// (get) Token: 0x060033BE RID: 13246 RVA: 0x000192BA File Offset: 0x000174BA
		// (set) Token: 0x060033BF RID: 13247 RVA: 0x000192C2 File Offset: 0x000174C2
		[DataMember]
		public eTestExamSettingType TestType { get; set; }

		// Token: 0x17001297 RID: 4759
		// (get) Token: 0x060033C0 RID: 13248 RVA: 0x000192CB File Offset: 0x000174CB
		// (set) Token: 0x060033C1 RID: 13249 RVA: 0x000192D3 File Offset: 0x000174D3
		[DataMember]
		public string OptionalClockWorkSettingsInstanceName { get; set; }

		// Token: 0x17001298 RID: 4760
		// (get) Token: 0x060033C2 RID: 13250 RVA: 0x000192DC File Offset: 0x000174DC
		// (set) Token: 0x060033C3 RID: 13251 RVA: 0x000192E4 File Offset: 0x000174E4
		[DataMember]
		public bool ClearCacheFirst { get; set; }
	}
}
