using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B6B RID: 2923
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadMediaContentByIdResp
	{
		// Token: 0x170016D4 RID: 5844
		// (get) Token: 0x06003DF9 RID: 15865 RVA: 0x0001E6A8 File Offset: 0x0001C8A8
		// (set) Token: 0x06003DFA RID: 15866 RVA: 0x0001E6B0 File Offset: 0x0001C8B0
		[DataMember]
		public MediaContentDTO MediaContent { get; set; }
	}
}
