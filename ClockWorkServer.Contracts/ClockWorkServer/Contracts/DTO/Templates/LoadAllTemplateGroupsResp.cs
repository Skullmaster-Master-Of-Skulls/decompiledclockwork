using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Templates
{
	// Token: 0x020001D6 RID: 470
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllTemplateGroupsResp
	{
		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000AB5 RID: 2741 RVA: 0x00004ECE File Offset: 0x000030CE
		// (set) Token: 0x06000AB6 RID: 2742 RVA: 0x00004ED6 File Offset: 0x000030D6
		[DataMember]
		public IList<TemplateGroupDTO> TemplateGroups { get; set; }
	}
}
