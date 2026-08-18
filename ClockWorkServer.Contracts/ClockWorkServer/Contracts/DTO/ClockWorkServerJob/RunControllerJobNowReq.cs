using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerJob
{
	// Token: 0x02000847 RID: 2119
	[DataContract(Namespace = "http://tpro.ca")]
	public class RunControllerJobNowReq : ClockWorkServerJobBaseReq
	{
		// Token: 0x17000F1B RID: 3867
		// (get) Token: 0x06002B1D RID: 11037 RVA: 0x0001475B File Offset: 0x0001295B
		// (set) Token: 0x06002B1E RID: 11038 RVA: 0x00014763 File Offset: 0x00012963
		[DataMember]
		public int JobId { get; set; }
	}
}
