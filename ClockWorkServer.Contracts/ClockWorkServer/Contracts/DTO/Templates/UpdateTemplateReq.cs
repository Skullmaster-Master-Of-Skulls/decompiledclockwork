using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Templates
{
	// Token: 0x020001D8 RID: 472
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateTemplateReq : BaseMessageReq
	{
		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000AB9 RID: 2745 RVA: 0x00004EDF File Offset: 0x000030DF
		// (set) Token: 0x06000ABA RID: 2746 RVA: 0x00004EE7 File Offset: 0x000030E7
		[DataMember]
		public TemplateDTO Template { get; set; }
	}
}
