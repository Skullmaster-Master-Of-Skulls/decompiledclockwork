using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Templates
{
	// Token: 0x020001D1 RID: 465
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllTemplatesAsForestReq : BaseMessageReq
	{
		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000AA6 RID: 2726 RVA: 0x00004E79 File Offset: 0x00003079
		// (set) Token: 0x06000AA7 RID: 2727 RVA: 0x00004E81 File Offset: 0x00003081
		[DataMember]
		public bool LoadDocumentsOrEmails { get; set; }
	}
}
