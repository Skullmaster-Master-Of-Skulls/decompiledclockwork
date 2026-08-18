using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters
{
	// Token: 0x02000B2B RID: 2859
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadBasicAppointmentInformationByUserAndDateRangeResp
	{
		// Token: 0x1700161B RID: 5659
		// (get) Token: 0x06003C37 RID: 15415 RVA: 0x0001D3CA File Offset: 0x0001B5CA
		// (set) Token: 0x06003C38 RID: 15416 RVA: 0x0001D3D2 File Offset: 0x0001B5D2
		[DataMember]
		public IList<BaseBasicAppointmentDTO> Appointments { get; set; }
	}
}
