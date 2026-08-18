using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Templates
{
	// Token: 0x020001CB RID: 459
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteTemplateReq : BaseMessageReq
	{
		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000A92 RID: 2706 RVA: 0x00004E02 File Offset: 0x00003002
		// (set) Token: 0x06000A93 RID: 2707 RVA: 0x00004E0A File Offset: 0x0000300A
		[DataMember]
		public int TemplateId { get; set; }
	}
}
