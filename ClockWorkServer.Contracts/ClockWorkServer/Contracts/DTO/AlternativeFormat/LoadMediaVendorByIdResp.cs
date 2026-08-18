using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C26 RID: 3110
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadMediaVendorByIdResp
	{
		// Token: 0x17001811 RID: 6161
		// (get) Token: 0x0600413A RID: 16698 RVA: 0x0001FF0B File Offset: 0x0001E10B
		// (set) Token: 0x0600413B RID: 16699 RVA: 0x0001FF13 File Offset: 0x0001E113
		[DataMember]
		public MediaVendorDTO MediaVendor { get; set; }
	}
}
