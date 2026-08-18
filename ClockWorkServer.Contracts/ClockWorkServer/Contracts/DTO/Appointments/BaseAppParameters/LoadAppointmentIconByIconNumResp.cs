using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x0200094C RID: 2380
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAppointmentIconByIconNumResp
	{
		// Token: 0x1700114B RID: 4427
		// (get) Token: 0x060030B5 RID: 12469 RVA: 0x00017C70 File Offset: 0x00015E70
		// (set) Token: 0x060030B6 RID: 12470 RVA: 0x00017C78 File Offset: 0x00015E78
		[DataMember]
		public AppointmentIconDTO Icon { get; set; }
	}
}
