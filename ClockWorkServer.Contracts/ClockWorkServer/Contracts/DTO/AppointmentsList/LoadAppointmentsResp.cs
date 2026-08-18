using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsList
{
	// Token: 0x02000AE3 RID: 2787
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAppointmentsResp
	{
		// Token: 0x1700159B RID: 5531
		// (get) Token: 0x06003AEF RID: 15087 RVA: 0x0001CB34 File Offset: 0x0001AD34
		// (set) Token: 0x06003AF0 RID: 15088 RVA: 0x0001CB3C File Offset: 0x0001AD3C
		[DataMember]
		public IList<ListAppointmentDTO> Appointments { get; set; }
	}
}
