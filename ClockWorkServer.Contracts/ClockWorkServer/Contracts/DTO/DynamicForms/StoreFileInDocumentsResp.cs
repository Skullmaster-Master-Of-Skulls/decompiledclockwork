using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000649 RID: 1609
	[DataContract(Namespace = "http://tpro.ca")]
	public class StoreFileInDocumentsResp
	{
		// Token: 0x17000B03 RID: 2819
		// (get) Token: 0x060020CC RID: 8396 RVA: 0x0000EE94 File Offset: 0x0000D094
		// (set) Token: 0x060020CD RID: 8397 RVA: 0x0000EE9C File Offset: 0x0000D09C
		[DataMember]
		public int FileId { get; set; }
	}
}
