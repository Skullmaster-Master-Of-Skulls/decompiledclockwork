using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B99 RID: 2969
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetMediaContentCoverImageBytesResp
	{
		// Token: 0x170016FF RID: 5887
		// (get) Token: 0x06003E7D RID: 15997 RVA: 0x0001E983 File Offset: 0x0001CB83
		// (set) Token: 0x06003E7E RID: 15998 RVA: 0x0001E98B File Offset: 0x0001CB8B
		[DataMember]
		public byte[] CoverImage { get; set; }
	}
}
