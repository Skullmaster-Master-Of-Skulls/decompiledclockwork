using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x0200098A RID: 2442
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAppTypeGroupWithAppTypesByIdReq : BaseMessageReq
	{
		// Token: 0x170011A4 RID: 4516
		// (get) Token: 0x060031A5 RID: 12709 RVA: 0x00018259 File Offset: 0x00016459
		// (set) Token: 0x060031A6 RID: 12710 RVA: 0x00018261 File Offset: 0x00016461
		[DataMember]
		public int AppointmentTypeGroupId { get; set; }
	}
}
