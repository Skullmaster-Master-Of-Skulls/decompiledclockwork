using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C28 RID: 3112
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadMediaVendorByNameResp
	{
		// Token: 0x17001813 RID: 6163
		// (get) Token: 0x06004140 RID: 16704 RVA: 0x0001FF2D File Offset: 0x0001E12D
		// (set) Token: 0x06004141 RID: 16705 RVA: 0x0001FF35 File Offset: 0x0001E135
		[DataMember]
		public MediaVendorDTO MediaVendor { get; set; }
	}
}
