using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.ListItem
{
	// Token: 0x02000745 RID: 1861
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateCustomListGroupResp
	{
		// Token: 0x17000D61 RID: 3425
		// (get) Token: 0x06002682 RID: 9858 RVA: 0x00011E63 File Offset: 0x00010063
		// (set) Token: 0x06002683 RID: 9859 RVA: 0x00011E6B File Offset: 0x0001006B
		[DataMember]
		public Guid GroupId { get; set; }
	}
}
