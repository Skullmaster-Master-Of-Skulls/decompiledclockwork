using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C27 RID: 3111
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadMediaVendorByNameReq : BaseMessageReq
	{
		// Token: 0x17001812 RID: 6162
		// (get) Token: 0x0600413D RID: 16701 RVA: 0x0001FF1C File Offset: 0x0001E11C
		// (set) Token: 0x0600413E RID: 16702 RVA: 0x0001FF24 File Offset: 0x0001E124
		[DataMember]
		public string MediaVendorName { get; set; }
	}
}
