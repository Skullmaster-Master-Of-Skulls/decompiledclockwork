using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Templates
{
	// Token: 0x020001D4 RID: 468
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateTemplateGroupReq : BaseMessageReq
	{
		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000AAF RID: 2735 RVA: 0x00004EAC File Offset: 0x000030AC
		// (set) Token: 0x06000AB0 RID: 2736 RVA: 0x00004EB4 File Offset: 0x000030B4
		[DataMember]
		public TemplateGroupDTO TemplateGroup { get; set; }
	}
}
