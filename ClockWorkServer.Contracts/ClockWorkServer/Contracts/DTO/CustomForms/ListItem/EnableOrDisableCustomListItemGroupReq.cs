using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.ListItem
{
	// Token: 0x0200074E RID: 1870
	[DataContract(Namespace = "http://tpro.ca")]
	public class EnableOrDisableCustomListItemGroupReq : BaseMessageReq
	{
		// Token: 0x17000D69 RID: 3433
		// (get) Token: 0x0600269B RID: 9883 RVA: 0x00011EEB File Offset: 0x000100EB
		// (set) Token: 0x0600269C RID: 9884 RVA: 0x00011EF3 File Offset: 0x000100F3
		[DataMember]
		public Guid GroupId { get; set; }

		// Token: 0x17000D6A RID: 3434
		// (get) Token: 0x0600269D RID: 9885 RVA: 0x00011EFC File Offset: 0x000100FC
		// (set) Token: 0x0600269E RID: 9886 RVA: 0x00011F04 File Offset: 0x00010104
		[DataMember]
		public bool Enable { get; set; }
	}
}
