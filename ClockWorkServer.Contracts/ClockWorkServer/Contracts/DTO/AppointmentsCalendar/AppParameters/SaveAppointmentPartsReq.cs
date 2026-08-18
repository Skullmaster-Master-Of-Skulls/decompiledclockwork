using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters
{
	// Token: 0x02000B12 RID: 2834
	[DataContract(Namespace = "http://tpro.ca")]
	public class SaveAppointmentPartsReq : BaseMessageReq
	{
		// Token: 0x170015F6 RID: 5622
		// (get) Token: 0x06003BD4 RID: 15316 RVA: 0x0001D155 File Offset: 0x0001B355
		// (set) Token: 0x06003BD5 RID: 15317 RVA: 0x0001D15D File Offset: 0x0001B35D
		[DataMember]
		public AppointmentDTO Appointment { get; set; }

		// Token: 0x170015F7 RID: 5623
		// (get) Token: 0x06003BD6 RID: 15318 RVA: 0x0001D166 File Offset: 0x0001B366
		// (set) Token: 0x06003BD7 RID: 15319 RVA: 0x0001D16E File Offset: 0x0001B36E
		[DataMember]
		public eAppointmentModifiedItemType PartsToUpdate { get; set; }
	}
}
