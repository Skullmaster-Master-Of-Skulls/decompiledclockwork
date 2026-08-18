using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Field;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.ListItem
{
	// Token: 0x02000741 RID: 1857
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadListItemsByGroupIdResp
	{
		// Token: 0x17000D5D RID: 3421
		// (get) Token: 0x06002676 RID: 9846 RVA: 0x00011E1F File Offset: 0x0001001F
		// (set) Token: 0x06002677 RID: 9847 RVA: 0x00011E27 File Offset: 0x00010027
		[DataMember]
		public List<CustomListItemDTO> ListItems { get; set; }
	}
}
