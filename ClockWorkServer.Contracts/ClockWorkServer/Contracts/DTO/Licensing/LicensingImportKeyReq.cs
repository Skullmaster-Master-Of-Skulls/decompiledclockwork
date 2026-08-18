using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DataContracts;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Licensing
{
	// Token: 0x020004BE RID: 1214
	[DataContract(Namespace = "http://tpro.ca")]
	public class LicensingImportKeyReq : BaseMessageReq
	{
		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x060019CD RID: 6605 RVA: 0x0000BF21 File Offset: 0x0000A121
		// (set) Token: 0x060019CE RID: 6606 RVA: 0x0000BF29 File Offset: 0x0000A129
		[DataMember]
		public LicenseInfoDTO License { get; set; }
	}
}
