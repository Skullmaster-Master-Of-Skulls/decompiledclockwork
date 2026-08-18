using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B7A RID: 2938
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteMediaContentReq : BaseMessageReq
	{
		// Token: 0x170016E2 RID: 5858
		// (get) Token: 0x06003E24 RID: 15908 RVA: 0x0001E796 File Offset: 0x0001C996
		// (set) Token: 0x06003E25 RID: 15909 RVA: 0x0001E79E File Offset: 0x0001C99E
		[DataMember]
		public MediaContentDTO MediaContent { get; set; }
	}
}
