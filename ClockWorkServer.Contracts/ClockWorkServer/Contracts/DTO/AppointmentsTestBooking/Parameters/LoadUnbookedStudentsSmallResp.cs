using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.TestBookingViews.ViewEntities;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters
{
	// Token: 0x02000A6C RID: 2668
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadUnbookedStudentsSmallResp
	{
		// Token: 0x1700145F RID: 5215
		// (get) Token: 0x060037FB RID: 14331 RVA: 0x0001B31B File Offset: 0x0001951B
		// (set) Token: 0x060037FC RID: 14332 RVA: 0x0001B323 File Offset: 0x00019523
		[DataMember]
		public IList<UnbookedStudentsSmallDTO> UnbookedStudentsSmall { get; set; }
	}
}
