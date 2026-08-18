using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000669 RID: 1641
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadFileFromImageInfoResp
	{
		// Token: 0x17000B39 RID: 2873
		// (get) Token: 0x06002158 RID: 8536 RVA: 0x0000F22A File Offset: 0x0000D42A
		// (set) Token: 0x06002159 RID: 8537 RVA: 0x0000F232 File Offset: 0x0000D432
		[DataMember]
		public BinaryFileDTO File { get; set; }
	}
}
