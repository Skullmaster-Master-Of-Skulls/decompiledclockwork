using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Email
{
	// Token: 0x02000616 RID: 1558
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAttachmentResp
	{
		// Token: 0x17000A90 RID: 2704
		// (get) Token: 0x06001FB1 RID: 8113 RVA: 0x0000E64E File Offset: 0x0000C84E
		// (set) Token: 0x06001FB2 RID: 8114 RVA: 0x0000E656 File Offset: 0x0000C856
		[DataMember]
		public TPMailAttachmentDTO Attachment { get; set; }
	}
}
