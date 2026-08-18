using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x0200097A RID: 2426
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteShowTimeAsTypeByIdReq : BaseMessageReq
	{
		// Token: 0x17001193 RID: 4499
		// (get) Token: 0x06003173 RID: 12659 RVA: 0x00018138 File Offset: 0x00016338
		// (set) Token: 0x06003174 RID: 12660 RVA: 0x00018140 File Offset: 0x00016340
		[DataMember]
		public int AppointmentShowTimeAsId { get; set; }
	}
}
