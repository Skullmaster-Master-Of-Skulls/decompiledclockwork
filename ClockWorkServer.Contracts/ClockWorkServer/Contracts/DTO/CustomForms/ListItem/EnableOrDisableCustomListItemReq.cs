using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.ListItem
{
	// Token: 0x0200074C RID: 1868
	[DataContract(Namespace = "http://tpro.ca")]
	public class EnableOrDisableCustomListItemReq : BaseMessageReq
	{
		// Token: 0x17000D67 RID: 3431
		// (get) Token: 0x06002695 RID: 9877 RVA: 0x00011EC9 File Offset: 0x000100C9
		// (set) Token: 0x06002696 RID: 9878 RVA: 0x00011ED1 File Offset: 0x000100D1
		[DataMember]
		public Guid ItemId { get; set; }

		// Token: 0x17000D68 RID: 3432
		// (get) Token: 0x06002697 RID: 9879 RVA: 0x00011EDA File Offset: 0x000100DA
		// (set) Token: 0x06002698 RID: 9880 RVA: 0x00011EE2 File Offset: 0x000100E2
		[DataMember]
		public bool Enable { get; set; }
	}
}
