using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000653 RID: 1619
	[DataContract(Namespace = "http://tpro.ca")]
	public class UploadDocumentToDatabaseResp
	{
		// Token: 0x17000B12 RID: 2834
		// (get) Token: 0x060020F4 RID: 8436 RVA: 0x0000EF93 File Offset: 0x0000D193
		// (set) Token: 0x060020F5 RID: 8437 RVA: 0x0000EF9B File Offset: 0x0000D19B
		[DataMember]
		public int FileId { get; set; }
	}
}
