using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C25 RID: 3109
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadMediaVendorByIdReq : BaseMessageReq
	{
		// Token: 0x17001810 RID: 6160
		// (get) Token: 0x06004137 RID: 16695 RVA: 0x0001FEFA File Offset: 0x0001E0FA
		// (set) Token: 0x06004138 RID: 16696 RVA: 0x0001FF02 File Offset: 0x0001E102
		[DataMember]
		public int MediaVendorId { get; set; }
	}
}
