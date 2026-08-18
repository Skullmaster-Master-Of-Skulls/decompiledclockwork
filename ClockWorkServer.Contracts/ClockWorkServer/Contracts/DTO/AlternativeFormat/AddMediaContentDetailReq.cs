using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B88 RID: 2952
	[DataContract(Namespace = "http://tpro.ca")]
	public class AddMediaContentDetailReq : BaseMessageReq
	{
		// Token: 0x170016F0 RID: 5872
		// (get) Token: 0x06003E4E RID: 15950 RVA: 0x0001E884 File Offset: 0x0001CA84
		// (set) Token: 0x06003E4F RID: 15951 RVA: 0x0001E88C File Offset: 0x0001CA8C
		[DataMember]
		public MediaContentDetailDTO MediaContentDetail { get; set; }
	}
}
