using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DataContracts;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Licensing
{
	// Token: 0x020004C6 RID: 1222
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetLicenseStateReq : BaseMessageReq
	{
		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x060019DD RID: 6621 RVA: 0x0000BF65 File Offset: 0x0000A165
		// (set) Token: 0x060019DE RID: 6622 RVA: 0x0000BF6D File Offset: 0x0000A16D
		[DataMember]
		public LicenseInfoDTO Key { get; set; }
	}
}
