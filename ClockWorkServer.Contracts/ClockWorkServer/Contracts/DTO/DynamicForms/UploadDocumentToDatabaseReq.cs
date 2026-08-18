using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000652 RID: 1618
	[DataContract(Namespace = "http://tpro.ca")]
	public class UploadDocumentToDatabaseReq : BaseMessageReq
	{
		// Token: 0x17000B11 RID: 2833
		// (get) Token: 0x060020F1 RID: 8433 RVA: 0x0000EF82 File Offset: 0x0000D182
		// (set) Token: 0x060020F2 RID: 8434 RVA: 0x0000EF8A File Offset: 0x0000D18A
		[DataMember]
		public BinaryFileDTO File { get; set; }
	}
}
