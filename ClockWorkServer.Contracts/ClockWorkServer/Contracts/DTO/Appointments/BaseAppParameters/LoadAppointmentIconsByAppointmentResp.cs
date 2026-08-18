using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x0200094A RID: 2378
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAppointmentIconsByAppointmentResp
	{
		// Token: 0x17001149 RID: 4425
		// (get) Token: 0x060030AF RID: 12463 RVA: 0x00017C4E File Offset: 0x00015E4E
		// (set) Token: 0x060030B0 RID: 12464 RVA: 0x00017C56 File Offset: 0x00015E56
		[DataMember]
		public IList<AppointmentIconDTO> Icons { get; set; }
	}
}
