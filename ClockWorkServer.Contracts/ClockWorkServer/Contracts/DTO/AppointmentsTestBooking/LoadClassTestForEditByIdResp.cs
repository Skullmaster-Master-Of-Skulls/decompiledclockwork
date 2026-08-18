using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x02000A19 RID: 2585
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadClassTestForEditByIdResp
	{
		// Token: 0x1700134A RID: 4938
		// (get) Token: 0x06003584 RID: 13700 RVA: 0x00019FBD File Offset: 0x000181BD
		// (set) Token: 0x06003585 RID: 13701 RVA: 0x00019FC5 File Offset: 0x000181C5
		[DataMember]
		public ClassTestForEditDTO ClassTestForEdit { get; set; }
	}
}
