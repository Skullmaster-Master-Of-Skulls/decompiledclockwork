using System;
using System.Runtime.Serialization;
using TechnoPro.Common.DataStructure.Tree;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Templates
{
	// Token: 0x020001D0 RID: 464
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllTemplatesAsForestResp
	{
		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000AA3 RID: 2723 RVA: 0x00004E68 File Offset: 0x00003068
		// (set) Token: 0x06000AA4 RID: 2724 RVA: 0x00004E70 File Offset: 0x00003070
		[DataMember]
		public Forest<TemplateOrGroupDTO> Forest { get; set; }
	}
}
