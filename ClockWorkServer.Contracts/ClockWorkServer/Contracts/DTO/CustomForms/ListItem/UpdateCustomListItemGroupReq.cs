using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Field;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.ListItem
{
	// Token: 0x0200074A RID: 1866
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateCustomListItemGroupReq : BaseMessageReq
	{
		// Token: 0x17000D66 RID: 3430
		// (get) Token: 0x06002691 RID: 9873 RVA: 0x00011EB8 File Offset: 0x000100B8
		// (set) Token: 0x06002692 RID: 9874 RVA: 0x00011EC0 File Offset: 0x000100C0
		[DataMember]
		public CustomListItemGroupDTO Group { get; set; }
	}
}
