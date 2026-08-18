using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x0200094F RID: 2383
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAppointmentIconByIconInfoIdReq : BaseMessageReq
	{
		// Token: 0x1700114F RID: 4431
		// (get) Token: 0x060030C0 RID: 12480 RVA: 0x00017CB4 File Offset: 0x00015EB4
		// (set) Token: 0x060030C1 RID: 12481 RVA: 0x00017CBC File Offset: 0x00015EBC
		[DataMember]
		public int IconInfoId { get; set; }
	}
}
