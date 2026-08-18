using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C23 RID: 3107
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteMediaVendorReq : BaseMessageReq
	{
		// Token: 0x1700180F RID: 6159
		// (get) Token: 0x06004133 RID: 16691 RVA: 0x0001FEE9 File Offset: 0x0001E0E9
		// (set) Token: 0x06004134 RID: 16692 RVA: 0x0001FEF1 File Offset: 0x0001E0F1
		[DataMember]
		public int MediaVendorId { get; set; }
	}
}
