using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters
{
	// Token: 0x02000B0B RID: 2827
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateHolidayReq : BaseMessageReq
	{
		// Token: 0x170015EC RID: 5612
		// (get) Token: 0x06003BB9 RID: 15289 RVA: 0x0001D0AB File Offset: 0x0001B2AB
		// (set) Token: 0x06003BBA RID: 15290 RVA: 0x0001D0B3 File Offset: 0x0001B2B3
		[DataMember]
		public HolidayDTO Holiday { get; set; }
	}
}
