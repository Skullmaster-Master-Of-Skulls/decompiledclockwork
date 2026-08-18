using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C1F RID: 3103
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateMediaVendorReq : BaseMessageReq
	{
		// Token: 0x1700180B RID: 6155
		// (get) Token: 0x06004127 RID: 16679 RVA: 0x0001FEA5 File Offset: 0x0001E0A5
		// (set) Token: 0x06004128 RID: 16680 RVA: 0x0001FEAD File Offset: 0x0001E0AD
		[DataMember]
		public MediaVendorDTO MediaVendor { get; set; }
	}
}
