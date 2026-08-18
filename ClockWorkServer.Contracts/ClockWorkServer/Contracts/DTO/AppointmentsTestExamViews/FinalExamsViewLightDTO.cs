using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestExamViews
{
	// Token: 0x020009A5 RID: 2469
	[DataContract(Namespace = "http://tpro.ca")]
	public class FinalExamsViewLightDTO : FinalExamsViewBaseDTO
	{
		// Token: 0x170011D0 RID: 4560
		// (get) Token: 0x06003218 RID: 12824 RVA: 0x00018545 File Offset: 0x00016745
		// (set) Token: 0x06003219 RID: 12825 RVA: 0x0001854D File Offset: 0x0001674D
		public IList<FinalExamsViewLightBookingDTO> Bookings { get; set; }
	}
}
