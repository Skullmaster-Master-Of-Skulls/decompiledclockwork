using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DataContracts;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Licensing
{
	// Token: 0x020004C1 RID: 1217
	[DataContract(Namespace = "http://tpro.ca")]
	public class LicensingSupportPlanKeyResp
	{
		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x060019D2 RID: 6610 RVA: 0x0000BF32 File Offset: 0x0000A132
		// (set) Token: 0x060019D3 RID: 6611 RVA: 0x0000BF3A File Offset: 0x0000A13A
		[DataMember]
		public LicenseInfoDTO LicenseInfo { get; set; }
	}
}
