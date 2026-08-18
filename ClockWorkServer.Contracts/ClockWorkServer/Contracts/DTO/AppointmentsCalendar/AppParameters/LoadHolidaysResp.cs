using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters
{
	// Token: 0x02000B10 RID: 2832
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadHolidaysResp
	{
		// Token: 0x170015F2 RID: 5618
		// (get) Token: 0x06003BCA RID: 15306 RVA: 0x0001D111 File Offset: 0x0001B311
		// (set) Token: 0x06003BCB RID: 15307 RVA: 0x0001D119 File Offset: 0x0001B319
		[DataMember]
		public IList<HolidayDTO> Holidays { get; set; }
	}
}
