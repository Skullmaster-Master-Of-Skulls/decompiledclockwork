using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.TestBookingViews.ViewEntities;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters
{
	// Token: 0x02000A72 RID: 2674
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadClassTestDefinitionSmallByExamIdResp
	{
		// Token: 0x17001468 RID: 5224
		// (get) Token: 0x06003813 RID: 14355 RVA: 0x0001B3B4 File Offset: 0x000195B4
		// (set) Token: 0x06003814 RID: 14356 RVA: 0x0001B3BC File Offset: 0x000195BC
		[DataMember]
		public ClassTestDefinitionSmallDTO ClassTestDefinitionsSmall { get; set; }
	}
}
