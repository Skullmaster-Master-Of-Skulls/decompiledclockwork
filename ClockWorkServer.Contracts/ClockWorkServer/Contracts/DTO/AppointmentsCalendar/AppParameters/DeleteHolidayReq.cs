using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters
{
	// Token: 0x02000B0E RID: 2830
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteHolidayReq : BaseMessageReq
	{
		// Token: 0x170015EF RID: 5615
		// (get) Token: 0x06003BC2 RID: 15298 RVA: 0x0001D0DE File Offset: 0x0001B2DE
		// (set) Token: 0x06003BC3 RID: 15299 RVA: 0x0001D0E6 File Offset: 0x0001B2E6
		[DataMember]
		public int HolidayId { get; set; }
	}
}
