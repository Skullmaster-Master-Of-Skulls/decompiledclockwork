using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Azure.Storage
{
	// Token: 0x020008B4 RID: 2228
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetBlobSasUriResp
	{
		// Token: 0x17000FDF RID: 4063
		// (get) Token: 0x06002D16 RID: 11542 RVA: 0x00015564 File Offset: 0x00013764
		// (set) Token: 0x06002D17 RID: 11543 RVA: 0x0001556C File Offset: 0x0001376C
		[DataMember]
		public Uri BlobSasUri { get; set; }
	}
}
