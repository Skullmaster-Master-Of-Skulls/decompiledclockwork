using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.ListItem
{
	// Token: 0x02000747 RID: 1863
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateCustomListItemResp
	{
		// Token: 0x17000D64 RID: 3428
		// (get) Token: 0x0600268A RID: 9866 RVA: 0x00011E96 File Offset: 0x00010096
		// (set) Token: 0x0600268B RID: 9867 RVA: 0x00011E9E File Offset: 0x0001009E
		[DataMember]
		public Guid ItemId { get; set; }
	}
}
