using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B86 RID: 2950
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadMediaContentDetailByIdReq : BaseMessageReq
	{
		// Token: 0x170016EE RID: 5870
		// (get) Token: 0x06003E48 RID: 15944 RVA: 0x0001E862 File Offset: 0x0001CA62
		// (set) Token: 0x06003E49 RID: 15945 RVA: 0x0001E86A File Offset: 0x0001CA6A
		[DataMember]
		public int MediaContentDetailID { get; set; }
	}
}
