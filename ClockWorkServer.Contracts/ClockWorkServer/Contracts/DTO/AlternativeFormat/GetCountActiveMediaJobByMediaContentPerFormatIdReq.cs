using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BAE RID: 2990
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetCountActiveMediaJobByMediaContentPerFormatIdReq : BaseMessageReq
	{
		// Token: 0x17001752 RID: 5970
		// (get) Token: 0x06003F44 RID: 16196 RVA: 0x0001F25C File Offset: 0x0001D45C
		// (set) Token: 0x06003F45 RID: 16197 RVA: 0x0001F264 File Offset: 0x0001D464
		[DataMember]
		public int MediaContentPerFormatId { get; set; }

		// Token: 0x17001753 RID: 5971
		// (get) Token: 0x06003F46 RID: 16198 RVA: 0x0001F26D File Offset: 0x0001D46D
		// (set) Token: 0x06003F47 RID: 16199 RVA: 0x0001F275 File Offset: 0x0001D475
		[DataMember]
		public int StudentId { get; set; }
	}
}
