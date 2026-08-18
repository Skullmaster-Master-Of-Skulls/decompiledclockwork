using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x0200031C RID: 796
	[DataContract(Namespace = "http://tpro.ca")]
	public class CompileCSharpScript2Req : BaseReportMessageReq
	{
		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x0600121F RID: 4639 RVA: 0x00008775 File Offset: 0x00006975
		// (set) Token: 0x06001220 RID: 4640 RVA: 0x0000877D File Offset: 0x0000697D
		[DataMember]
		public string Code { get; set; }
	}
}
