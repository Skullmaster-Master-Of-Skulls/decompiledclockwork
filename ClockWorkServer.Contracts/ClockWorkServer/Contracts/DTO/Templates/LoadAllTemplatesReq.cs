using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Templates
{
	// Token: 0x020001CF RID: 463
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllTemplatesReq : BaseMessageReq
	{
		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000AA0 RID: 2720 RVA: 0x00004E57 File Offset: 0x00003057
		// (set) Token: 0x06000AA1 RID: 2721 RVA: 0x00004E5F File Offset: 0x0000305F
		[DataMember]
		public bool LoadDocumentsOrEmails { get; set; }
	}
}
