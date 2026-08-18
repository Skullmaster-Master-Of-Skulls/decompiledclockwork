using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.ListItem
{
	// Token: 0x02000742 RID: 1858
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadListItemByListItemIdReq : BaseMessageReq
	{
		// Token: 0x17000D5E RID: 3422
		// (get) Token: 0x06002679 RID: 9849 RVA: 0x00011E30 File Offset: 0x00010030
		// (set) Token: 0x0600267A RID: 9850 RVA: 0x00011E38 File Offset: 0x00010038
		[DataMember]
		public Guid ListItemId { get; set; }
	}
}
