using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B4E RID: 2894
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateMediaContentFileInfoResp
	{
		// Token: 0x170016AE RID: 5806
		// (get) Token: 0x06003D90 RID: 15760 RVA: 0x0001E422 File Offset: 0x0001C622
		// (set) Token: 0x06003D91 RID: 15761 RVA: 0x0001E42A File Offset: 0x0001C62A
		[DataMember]
		public MediaContentFileWithoutDataDTO MediaContentFile { get; set; }
	}
}
