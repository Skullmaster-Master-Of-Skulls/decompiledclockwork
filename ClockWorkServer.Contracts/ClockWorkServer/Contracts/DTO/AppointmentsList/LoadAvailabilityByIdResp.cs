using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule2;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsList
{
	// Token: 0x02000AEE RID: 2798
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAvailabilityByIdResp
	{
		// Token: 0x170015B3 RID: 5555
		// (get) Token: 0x06003B2A RID: 15146 RVA: 0x0001CCCC File Offset: 0x0001AECC
		// (set) Token: 0x06003B2B RID: 15147 RVA: 0x0001CCD4 File Offset: 0x0001AED4
		[DataMember]
		public Availability2ItemDTO AvailabilityItem { get; set; }
	}
}
