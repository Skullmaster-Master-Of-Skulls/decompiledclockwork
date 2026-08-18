using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestExamViews
{
	// Token: 0x020009A7 RID: 2471
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadFinalExamsLightResp
	{
		// Token: 0x170011D2 RID: 4562
		// (get) Token: 0x0600321E RID: 12830 RVA: 0x00018570 File Offset: 0x00016770
		// (set) Token: 0x0600321F RID: 12831 RVA: 0x00018578 File Offset: 0x00016778
		[DataMember]
		public IList<FinalExamsViewLightDTO> FinalExams { get; set; }
	}
}
