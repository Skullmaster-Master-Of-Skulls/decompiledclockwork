using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Licensing
{
	// Token: 0x020004C7 RID: 1223
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetLicenseStateResp
	{
		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x060019E0 RID: 6624 RVA: 0x0000BF76 File Offset: 0x0000A176
		// (set) Token: 0x060019E1 RID: 6625 RVA: 0x0000BF7E File Offset: 0x0000A17E
		[DataMember]
		public LicenseState Status { get; set; }
	}
}
