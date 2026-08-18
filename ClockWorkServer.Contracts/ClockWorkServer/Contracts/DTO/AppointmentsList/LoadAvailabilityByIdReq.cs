using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsList
{
	// Token: 0x02000AEF RID: 2799
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAvailabilityByIdReq : BaseMessageReq
	{
		// Token: 0x170015B4 RID: 5556
		// (get) Token: 0x06003B2D RID: 15149 RVA: 0x0001CCDD File Offset: 0x0001AEDD
		// (set) Token: 0x06003B2E RID: 15150 RVA: 0x0001CCE5 File Offset: 0x0001AEE5
		[DataMember]
		public int Availability2ItemId { get; set; }
	}
}
