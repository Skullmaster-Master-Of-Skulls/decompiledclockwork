using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestExamViews
{
	// Token: 0x020009A6 RID: 2470
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadFinalExamsLightReq : BaseMessageReq
	{
		// Token: 0x170011D1 RID: 4561
		// (get) Token: 0x0600321B RID: 12827 RVA: 0x0001855F File Offset: 0x0001675F
		// (set) Token: 0x0600321C RID: 12828 RVA: 0x00018567 File Offset: 0x00016767
		[DataMember]
		public FinalExamsContextDTO Context { get; set; }
	}
}
