using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Field;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.ListItem
{
	// Token: 0x02000744 RID: 1860
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateCustomListGroupReq : BaseMessageReq
	{
		// Token: 0x17000D60 RID: 3424
		// (get) Token: 0x0600267F RID: 9855 RVA: 0x00011E52 File Offset: 0x00010052
		// (set) Token: 0x06002680 RID: 9856 RVA: 0x00011E5A File Offset: 0x0001005A
		[DataMember]
		public CustomListItemGroupDTO Group { get; set; }
	}
}
