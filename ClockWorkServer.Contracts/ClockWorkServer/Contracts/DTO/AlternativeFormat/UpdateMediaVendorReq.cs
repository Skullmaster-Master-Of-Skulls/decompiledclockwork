using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C21 RID: 3105
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateMediaVendorReq : BaseMessageReq
	{
		// Token: 0x1700180D RID: 6157
		// (get) Token: 0x0600412D RID: 16685 RVA: 0x0001FEC7 File Offset: 0x0001E0C7
		// (set) Token: 0x0600412E RID: 16686 RVA: 0x0001FECF File Offset: 0x0001E0CF
		[DataMember]
		public MediaVendorDTO MediaVendor { get; set; }
	}
}
