using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C2A RID: 3114
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllMediaVendorsResp
	{
		// Token: 0x17001814 RID: 6164
		// (get) Token: 0x06004144 RID: 16708 RVA: 0x0001FF3E File Offset: 0x0001E13E
		// (set) Token: 0x06004145 RID: 16709 RVA: 0x0001FF46 File Offset: 0x0001E146
		[DataMember]
		public IList<MediaVendorDTO> MediaVendors { get; set; }
	}
}
