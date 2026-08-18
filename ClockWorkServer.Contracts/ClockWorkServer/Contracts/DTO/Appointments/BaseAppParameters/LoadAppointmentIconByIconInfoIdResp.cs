using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x02000950 RID: 2384
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAppointmentIconByIconInfoIdResp
	{
		// Token: 0x17001150 RID: 4432
		// (get) Token: 0x060030C3 RID: 12483 RVA: 0x00017CC5 File Offset: 0x00015EC5
		// (set) Token: 0x060030C4 RID: 12484 RVA: 0x00017CCD File Offset: 0x00015ECD
		[DataMember]
		public AppointmentIconDTO Icon { get; set; }
	}
}
