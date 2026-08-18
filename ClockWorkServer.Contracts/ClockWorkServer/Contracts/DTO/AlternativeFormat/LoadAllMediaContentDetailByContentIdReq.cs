using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B8E RID: 2958
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllMediaContentDetailByContentIdReq : BaseMessageReq
	{
		// Token: 0x170016F4 RID: 5876
		// (get) Token: 0x06003E5C RID: 15964 RVA: 0x0001E8C8 File Offset: 0x0001CAC8
		// (set) Token: 0x06003E5D RID: 15965 RVA: 0x0001E8D0 File Offset: 0x0001CAD0
		[DataMember]
		public Guid MediaContentID { get; set; }
	}
}
