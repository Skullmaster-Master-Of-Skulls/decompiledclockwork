using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x0200094B RID: 2379
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAppointmentIconByIconNumReq : BaseMessageReq
	{
		// Token: 0x1700114A RID: 4426
		// (get) Token: 0x060030B2 RID: 12466 RVA: 0x00017C5F File Offset: 0x00015E5F
		// (set) Token: 0x060030B3 RID: 12467 RVA: 0x00017C67 File Offset: 0x00015E67
		[DataMember]
		public int IconNum { get; set; }
	}
}
