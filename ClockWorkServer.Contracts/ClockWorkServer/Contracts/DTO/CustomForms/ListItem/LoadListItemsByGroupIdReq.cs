using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.ListItem
{
	// Token: 0x02000740 RID: 1856
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadListItemsByGroupIdReq : BaseMessageReq
	{
		// Token: 0x17000D5C RID: 3420
		// (get) Token: 0x06002673 RID: 9843 RVA: 0x00011E0E File Offset: 0x0001000E
		// (set) Token: 0x06002674 RID: 9844 RVA: 0x00011E16 File Offset: 0x00010016
		[DataMember]
		public Guid CustomListGroupId { get; set; }
	}
}
