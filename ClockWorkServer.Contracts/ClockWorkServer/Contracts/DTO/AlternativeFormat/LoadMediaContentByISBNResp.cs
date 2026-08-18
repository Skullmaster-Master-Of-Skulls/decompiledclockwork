using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B6F RID: 2927
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadMediaContentByISBNResp
	{
		// Token: 0x170016D8 RID: 5848
		// (get) Token: 0x06003E05 RID: 15877 RVA: 0x0001E6EC File Offset: 0x0001C8EC
		// (set) Token: 0x06003E06 RID: 15878 RVA: 0x0001E6F4 File Offset: 0x0001C8F4
		[DataMember]
		public MediaContentDTO MediaContent { get; set; }
	}
}
