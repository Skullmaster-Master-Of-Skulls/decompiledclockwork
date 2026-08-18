using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Templates
{
	// Token: 0x020001D2 RID: 466
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadTemplateGroupByIdResp
	{
		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000AA9 RID: 2729 RVA: 0x00004E8A File Offset: 0x0000308A
		// (set) Token: 0x06000AAA RID: 2730 RVA: 0x00004E92 File Offset: 0x00003092
		[DataMember]
		public TemplateGroupDTO TemplateGroup { get; set; }
	}
}
