using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Templates
{
	// Token: 0x020001C0 RID: 448
	[DataContract(Namespace = "http://tpro.ca")]
	public class TemplateCollectionDTO
	{
		// Token: 0x06000A55 RID: 2645 RVA: 0x00004B3B File Offset: 0x00002D3B
		public TemplateCollectionDTO()
		{
			this.Templates = new List<TemplateDTO>();
			this.Groups = new List<TemplateGroupDTO>();
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000A56 RID: 2646 RVA: 0x00004B5D File Offset: 0x00002D5D
		// (set) Token: 0x06000A57 RID: 2647 RVA: 0x00004B65 File Offset: 0x00002D65
		[DataMember]
		public IList<TemplateDTO> Templates { get; set; }

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000A58 RID: 2648 RVA: 0x00004B6E File Offset: 0x00002D6E
		// (set) Token: 0x06000A59 RID: 2649 RVA: 0x00004B76 File Offset: 0x00002D76
		[DataMember]
		public IList<TemplateGroupDTO> Groups { get; set; }
	}
}
