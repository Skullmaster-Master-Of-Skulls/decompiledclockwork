using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters
{
	// Token: 0x02000B2E RID: 2862
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetNumberOfAppointmentsWithAppTypeReq : BaseMessageReq
	{
		// Token: 0x1700161E RID: 5662
		// (get) Token: 0x06003C40 RID: 15424 RVA: 0x0001D3FD File Offset: 0x0001B5FD
		// (set) Token: 0x06003C41 RID: 15425 RVA: 0x0001D405 File Offset: 0x0001B605
		[DataMember]
		public int AppTypeId { get; set; }
	}
}
