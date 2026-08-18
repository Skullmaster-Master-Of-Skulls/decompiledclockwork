using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Templates
{
	// Token: 0x020001C4 RID: 452
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadTemplateResp
	{
		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000A75 RID: 2677 RVA: 0x00004D47 File Offset: 0x00002F47
		// (set) Token: 0x06000A76 RID: 2678 RVA: 0x00004D4F File Offset: 0x00002F4F
		[DataMember]
		public TemplateDTO Template { get; set; }
	}
}
