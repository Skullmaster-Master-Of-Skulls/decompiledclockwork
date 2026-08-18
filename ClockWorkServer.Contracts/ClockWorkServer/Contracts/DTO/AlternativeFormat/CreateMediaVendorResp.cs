using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C20 RID: 3104
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateMediaVendorResp
	{
		// Token: 0x1700180C RID: 6156
		// (get) Token: 0x0600412A RID: 16682 RVA: 0x0001FEB6 File Offset: 0x0001E0B6
		// (set) Token: 0x0600412B RID: 16683 RVA: 0x0001FEBE File Offset: 0x0001E0BE
		[DataMember]
		public int MediaVendorId { get; set; }
	}
}
