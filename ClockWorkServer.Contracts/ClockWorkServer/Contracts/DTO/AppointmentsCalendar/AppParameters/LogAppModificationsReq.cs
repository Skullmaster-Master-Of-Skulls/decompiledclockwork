using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters
{
	// Token: 0x02000B11 RID: 2833
	[DataContract(Namespace = "http://tpro.ca")]
	public class LogAppModificationsReq : BaseMessageReq
	{
		// Token: 0x170015F3 RID: 5619
		// (get) Token: 0x06003BCD RID: 15309 RVA: 0x0001D122 File Offset: 0x0001B322
		// (set) Token: 0x06003BCE RID: 15310 RVA: 0x0001D12A File Offset: 0x0001B32A
		[DataMember]
		public int AppointmentId { get; set; }

		// Token: 0x170015F4 RID: 5620
		// (get) Token: 0x06003BCF RID: 15311 RVA: 0x0001D133 File Offset: 0x0001B333
		// (set) Token: 0x06003BD0 RID: 15312 RVA: 0x0001D13B File Offset: 0x0001B33B
		[DataMember]
		public eHowModifiedCode HowModified { get; set; }

		// Token: 0x170015F5 RID: 5621
		// (get) Token: 0x06003BD1 RID: 15313 RVA: 0x0001D144 File Offset: 0x0001B344
		// (set) Token: 0x06003BD2 RID: 15314 RVA: 0x0001D14C File Offset: 0x0001B34C
		[DataMember]
		public eAppointmentModifiedItemType ModifiedItems { get; set; }
	}
}
