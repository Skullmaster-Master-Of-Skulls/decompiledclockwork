using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009C1 RID: 2497
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadSpecialAccommodationsReq : BaseMessageReq
	{
		// Token: 0x1700129A RID: 4762
		// (get) Token: 0x060033C8 RID: 13256 RVA: 0x000192FE File Offset: 0x000174FE
		// (set) Token: 0x060033C9 RID: 13257 RVA: 0x00019306 File Offset: 0x00017506
		[DataMember]
		public eTestExamSettingType TestType { get; set; }

		// Token: 0x1700129B RID: 4763
		// (get) Token: 0x060033CA RID: 13258 RVA: 0x0001930F File Offset: 0x0001750F
		// (set) Token: 0x060033CB RID: 13259 RVA: 0x00019317 File Offset: 0x00017517
		[DataMember]
		public string OptionalClockWorkSettingsInstanceName { get; set; }

		// Token: 0x1700129C RID: 4764
		// (get) Token: 0x060033CC RID: 13260 RVA: 0x00019320 File Offset: 0x00017520
		// (set) Token: 0x060033CD RID: 13261 RVA: 0x00019328 File Offset: 0x00017528
		[DataMember]
		public bool ClearCacheFirst { get; set; }
	}
}
