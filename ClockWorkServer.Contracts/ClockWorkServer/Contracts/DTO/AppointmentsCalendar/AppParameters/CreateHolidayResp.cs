using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters
{
	// Token: 0x02000B0C RID: 2828
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateHolidayResp
	{
		// Token: 0x170015ED RID: 5613
		// (get) Token: 0x06003BBC RID: 15292 RVA: 0x0001D0BC File Offset: 0x0001B2BC
		// (set) Token: 0x06003BBD RID: 15293 RVA: 0x0001D0C4 File Offset: 0x0001B2C4
		[DataMember]
		public int HolidayId { get; set; }
	}
}
