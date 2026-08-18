using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Templates
{
	// Token: 0x020001C5 RID: 453
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadTemplateReq : BaseMessageReq
	{
		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000A78 RID: 2680 RVA: 0x00004D58 File Offset: 0x00002F58
		// (set) Token: 0x06000A79 RID: 2681 RVA: 0x00004D60 File Offset: 0x00002F60
		[DataMember]
		public int TemplateId { get; set; }

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000A7A RID: 2682 RVA: 0x00004D69 File Offset: 0x00002F69
		// (set) Token: 0x06000A7B RID: 2683 RVA: 0x00004D71 File Offset: 0x00002F71
		[DataMember]
		public bool LoadDocumentOrEmail { get; set; }
	}
}
