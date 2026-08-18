using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Templates
{
	// Token: 0x020001D5 RID: 469
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteTemplateGroupReq : BaseMessageReq
	{
		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000AB2 RID: 2738 RVA: 0x00004EBD File Offset: 0x000030BD
		// (set) Token: 0x06000AB3 RID: 2739 RVA: 0x00004EC5 File Offset: 0x000030C5
		[DataMember]
		public string TemplateGroupId { get; set; }
	}
}
