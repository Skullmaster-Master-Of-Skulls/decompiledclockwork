using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Templates
{
	// Token: 0x020001C8 RID: 456
	[DataContract(Namespace = "http://tpro.ca")]
	public class ReplaceTemplateFileReq : BaseMessageReq
	{
		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000A83 RID: 2691 RVA: 0x00004D9C File Offset: 0x00002F9C
		// (set) Token: 0x06000A84 RID: 2692 RVA: 0x00004DA4 File Offset: 0x00002FA4
		[DataMember]
		public int TemplateId { get; set; }

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000A85 RID: 2693 RVA: 0x00004DAD File Offset: 0x00002FAD
		// (set) Token: 0x06000A86 RID: 2694 RVA: 0x00004DB5 File Offset: 0x00002FB5
		[DataMember]
		public BinaryFileDTO File { get; set; }
	}
}
