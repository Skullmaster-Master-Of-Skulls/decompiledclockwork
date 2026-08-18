using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters
{
	// Token: 0x02000B2C RID: 2860
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateAppointmentExternalIdReq : BaseMessageReq
	{
		// Token: 0x1700161C RID: 5660
		// (get) Token: 0x06003C3A RID: 15418 RVA: 0x0001D3DB File Offset: 0x0001B5DB
		// (set) Token: 0x06003C3B RID: 15419 RVA: 0x0001D3E3 File Offset: 0x0001B5E3
		[DataMember]
		public int AppointmentId { get; set; }

		// Token: 0x1700161D RID: 5661
		// (get) Token: 0x06003C3C RID: 15420 RVA: 0x0001D3EC File Offset: 0x0001B5EC
		// (set) Token: 0x06003C3D RID: 15421 RVA: 0x0001D3F4 File Offset: 0x0001B5F4
		[DataMember]
		public int ExternalId { get; set; }
	}
}
