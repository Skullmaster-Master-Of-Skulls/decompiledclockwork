using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.PerformanceTesting
{
	// Token: 0x0200035D RID: 861
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAppointmentsPerformanceTestResp
	{
		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x060013B8 RID: 5048 RVA: 0x000093CA File Offset: 0x000075CA
		// (set) Token: 0x060013B9 RID: 5049 RVA: 0x000093D2 File Offset: 0x000075D2
		[DataMember]
		public PerformanceTestResultDTO Result { get; set; }

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x060013BA RID: 5050 RVA: 0x000093DB File Offset: 0x000075DB
		// (set) Token: 0x060013BB RID: 5051 RVA: 0x000093E3 File Offset: 0x000075E3
		[DataMember]
		public IList<AppointmentDTO> Appointments { get; set; }
	}
}
