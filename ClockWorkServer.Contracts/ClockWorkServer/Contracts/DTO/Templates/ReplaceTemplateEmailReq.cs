using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Templates
{
	// Token: 0x020001C9 RID: 457
	[DataContract(Namespace = "http://tpro.ca")]
	public class ReplaceTemplateEmailReq : BaseMessageReq
	{
		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000A88 RID: 2696 RVA: 0x00004DBE File Offset: 0x00002FBE
		// (set) Token: 0x06000A89 RID: 2697 RVA: 0x00004DC6 File Offset: 0x00002FC6
		[DataMember]
		public int TemplateId { get; set; }

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000A8A RID: 2698 RVA: 0x00004DCF File Offset: 0x00002FCF
		// (set) Token: 0x06000A8B RID: 2699 RVA: 0x00004DD7 File Offset: 0x00002FD7
		[DataMember]
		public TPMailMessageDTO EmailTemplate { get; set; }
	}
}
