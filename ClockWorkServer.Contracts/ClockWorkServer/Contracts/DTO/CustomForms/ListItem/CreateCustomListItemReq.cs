using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Field;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.ListItem
{
	// Token: 0x02000746 RID: 1862
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateCustomListItemReq : BaseMessageReq
	{
		// Token: 0x17000D62 RID: 3426
		// (get) Token: 0x06002685 RID: 9861 RVA: 0x00011E74 File Offset: 0x00010074
		// (set) Token: 0x06002686 RID: 9862 RVA: 0x00011E7C File Offset: 0x0001007C
		[DataMember]
		public Guid GroupId { get; set; }

		// Token: 0x17000D63 RID: 3427
		// (get) Token: 0x06002687 RID: 9863 RVA: 0x00011E85 File Offset: 0x00010085
		// (set) Token: 0x06002688 RID: 9864 RVA: 0x00011E8D File Offset: 0x0001008D
		[DataMember]
		public CustomListItemDTO Item { get; set; }
	}
}
