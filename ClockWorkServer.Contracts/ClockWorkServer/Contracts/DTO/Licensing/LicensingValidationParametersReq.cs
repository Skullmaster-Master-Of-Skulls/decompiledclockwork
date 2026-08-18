using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DataContracts;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Licensing
{
	// Token: 0x020004C4 RID: 1220
	[DataContract(Namespace = "http://tpro.ca")]
	public class LicensingValidationParametersReq : BaseMessageReq
	{
		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x060019D9 RID: 6617 RVA: 0x0000BF54 File Offset: 0x0000A154
		// (set) Token: 0x060019DA RID: 6618 RVA: 0x0000BF5C File Offset: 0x0000A15C
		[DataMember]
		public ValidationParameters Parameters { get; set; }
	}
}
