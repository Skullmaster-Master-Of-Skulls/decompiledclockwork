using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.AlternativeFormat;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B53 RID: 2899
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadMediaContentFileByLanguageReq : BaseMessageReq
	{
		// Token: 0x170016B1 RID: 5809
		// (get) Token: 0x06003D9B RID: 15771 RVA: 0x0001E455 File Offset: 0x0001C655
		// (set) Token: 0x06003D9C RID: 15772 RVA: 0x0001E45D File Offset: 0x0001C65D
		[DataMember]
		public eMediaContentLanguage MediaContentLanguage { get; set; }
	}
}
