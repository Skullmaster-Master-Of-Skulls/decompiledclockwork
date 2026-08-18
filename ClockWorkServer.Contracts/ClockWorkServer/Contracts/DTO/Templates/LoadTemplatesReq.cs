using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Templates
{
	// Token: 0x020001CD RID: 461
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadTemplatesReq : BaseMessageReq
	{
		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000A98 RID: 2712 RVA: 0x00004E24 File Offset: 0x00003024
		// (set) Token: 0x06000A99 RID: 2713 RVA: 0x00004E2C File Offset: 0x0000302C
		[DataMember]
		public string TemplateGroupId { get; set; }

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000A9A RID: 2714 RVA: 0x00004E35 File Offset: 0x00003035
		// (set) Token: 0x06000A9B RID: 2715 RVA: 0x00004E3D File Offset: 0x0000303D
		[DataMember]
		public bool LoadDocumentsOrEmails { get; set; }
	}
}
