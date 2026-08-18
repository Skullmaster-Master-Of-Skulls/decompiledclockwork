using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Templates
{
	// Token: 0x020001C6 RID: 454
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateNewTemplateResp
	{
		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000A7D RID: 2685 RVA: 0x00004D7A File Offset: 0x00002F7A
		// (set) Token: 0x06000A7E RID: 2686 RVA: 0x00004D82 File Offset: 0x00002F82
		[DataMember]
		public int TemplateId { get; set; }
	}
}
