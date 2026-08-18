using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DataContracts;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Licensing
{
	// Token: 0x020004C3 RID: 1219
	[DataContract(Namespace = "http://tpro.ca")]
	public class LicensingKeysResp
	{
		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x060019D6 RID: 6614 RVA: 0x0000BF43 File Offset: 0x0000A143
		// (set) Token: 0x060019D7 RID: 6615 RVA: 0x0000BF4B File Offset: 0x0000A14B
		[DataMember]
		public IList<LicenseInfoDTO> Keys { get; set; }
	}
}
