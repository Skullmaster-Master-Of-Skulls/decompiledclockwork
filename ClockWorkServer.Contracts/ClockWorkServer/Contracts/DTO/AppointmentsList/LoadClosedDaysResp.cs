using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsList
{
	// Token: 0x02000AD6 RID: 2774
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadClosedDaysResp
	{
		// Token: 0x17001583 RID: 5507
		// (get) Token: 0x06003AB2 RID: 15026 RVA: 0x0001C99C File Offset: 0x0001AB9C
		// (set) Token: 0x06003AB3 RID: 15027 RVA: 0x0001C9A4 File Offset: 0x0001ABA4
		[DataMember]
		public IList<ClosedDayDTO> ClosedDays { get; set; }
	}
}
