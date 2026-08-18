using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Field;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.ListItem
{
	// Token: 0x02000748 RID: 1864
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateCustomListItemReq : BaseMessageReq
	{
		// Token: 0x17000D65 RID: 3429
		// (get) Token: 0x0600268D RID: 9869 RVA: 0x00011EA7 File Offset: 0x000100A7
		// (set) Token: 0x0600268E RID: 9870 RVA: 0x00011EAF File Offset: 0x000100AF
		[DataMember]
		public CustomListItemDTO Item { get; set; }
	}
}
