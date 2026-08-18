using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Templates
{
	// Token: 0x020001CC RID: 460
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadTemplatesResp
	{
		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000A95 RID: 2709 RVA: 0x00004E13 File Offset: 0x00003013
		// (set) Token: 0x06000A96 RID: 2710 RVA: 0x00004E1B File Offset: 0x0000301B
		[DataMember]
		public TemplateCollectionDTO TemplateCollection { get; set; }
	}
}
