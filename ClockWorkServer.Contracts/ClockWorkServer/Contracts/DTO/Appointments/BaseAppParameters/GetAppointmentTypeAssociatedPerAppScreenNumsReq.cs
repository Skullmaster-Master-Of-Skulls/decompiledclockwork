using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x0200097C RID: 2428
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetAppointmentTypeAssociatedPerAppScreenNumsReq : BaseMessageReq
	{
		// Token: 0x17001195 RID: 4501
		// (get) Token: 0x06003179 RID: 12665 RVA: 0x0001815A File Offset: 0x0001635A
		// (set) Token: 0x0600317A RID: 12666 RVA: 0x00018162 File Offset: 0x00016362
		[DataMember]
		public int AppTypeId { get; set; }
	}
}
