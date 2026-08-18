using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters
{
	// Token: 0x02000B0D RID: 2829
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateHolidayReq : BaseMessageReq
	{
		// Token: 0x170015EE RID: 5614
		// (get) Token: 0x06003BBF RID: 15295 RVA: 0x0001D0CD File Offset: 0x0001B2CD
		// (set) Token: 0x06003BC0 RID: 15296 RVA: 0x0001D0D5 File Offset: 0x0001B2D5
		[DataMember]
		public HolidayDTO Holiday { get; set; }
	}
}
