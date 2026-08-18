using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Templates
{
	// Token: 0x020001C7 RID: 455
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateNewTemplateReq : BaseMessageReq
	{
		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000A80 RID: 2688 RVA: 0x00004D8B File Offset: 0x00002F8B
		// (set) Token: 0x06000A81 RID: 2689 RVA: 0x00004D93 File Offset: 0x00002F93
		[DataMember]
		public TemplateDTO Template { get; set; }
	}
}
