using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x0200094E RID: 2382
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAppointmentIconResp
	{
		// Token: 0x1700114E RID: 4430
		// (get) Token: 0x060030BD RID: 12477 RVA: 0x00017CA3 File Offset: 0x00015EA3
		// (set) Token: 0x060030BE RID: 12478 RVA: 0x00017CAB File Offset: 0x00015EAB
		[DataMember]
		public AppointmentIconDTO Icon { get; set; }
	}
}
