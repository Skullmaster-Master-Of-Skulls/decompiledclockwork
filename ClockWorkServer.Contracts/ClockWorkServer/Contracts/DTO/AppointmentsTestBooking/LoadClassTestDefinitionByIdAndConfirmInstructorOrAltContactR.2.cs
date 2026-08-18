using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x02000A0F RID: 2575
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadClassTestDefinitionByIdAndConfirmInstructorOrAltContactResp
	{
		// Token: 0x1700133D RID: 4925
		// (get) Token: 0x06003560 RID: 13664 RVA: 0x00019EE0 File Offset: 0x000180E0
		// (set) Token: 0x06003561 RID: 13665 RVA: 0x00019EE8 File Offset: 0x000180E8
		[DataMember]
		public ClassTestDTO Test { get; set; }
	}
}
