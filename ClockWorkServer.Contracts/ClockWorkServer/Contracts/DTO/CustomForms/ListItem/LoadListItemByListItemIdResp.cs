using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Field;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.ListItem
{
	// Token: 0x02000743 RID: 1859
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadListItemByListItemIdResp
	{
		// Token: 0x17000D5F RID: 3423
		// (get) Token: 0x0600267C RID: 9852 RVA: 0x00011E41 File Offset: 0x00010041
		// (set) Token: 0x0600267D RID: 9853 RVA: 0x00011E49 File Offset: 0x00010049
		[DataMember]
		public CustomListItemDTO ListItem { get; set; }
	}
}
