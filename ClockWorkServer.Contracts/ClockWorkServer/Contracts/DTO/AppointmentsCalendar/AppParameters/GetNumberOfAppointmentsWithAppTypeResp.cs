using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters
{
	// Token: 0x02000B2F RID: 2863
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetNumberOfAppointmentsWithAppTypeResp
	{
		// Token: 0x1700161F RID: 5663
		// (get) Token: 0x06003C43 RID: 15427 RVA: 0x0001D40E File Offset: 0x0001B60E
		// (set) Token: 0x06003C44 RID: 15428 RVA: 0x0001D416 File Offset: 0x0001B616
		[DataMember]
		public int NumberOfAppointments { get; set; }
	}
}
