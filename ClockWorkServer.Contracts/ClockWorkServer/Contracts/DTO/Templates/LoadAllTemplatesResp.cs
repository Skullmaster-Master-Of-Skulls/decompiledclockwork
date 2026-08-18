using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Templates
{
	// Token: 0x020001CE RID: 462
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllTemplatesResp
	{
		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000A9D RID: 2717 RVA: 0x00004E46 File Offset: 0x00003046
		// (set) Token: 0x06000A9E RID: 2718 RVA: 0x00004E4E File Offset: 0x0000304E
		[DataMember]
		public TemplateCollectionDTO TemplateCollection { get; set; }
	}
}
