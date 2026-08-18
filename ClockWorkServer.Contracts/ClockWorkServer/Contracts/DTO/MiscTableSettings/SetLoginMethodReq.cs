using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Login;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MiscTableSettings
{
	// Token: 0x0200045D RID: 1117
	[DataContract(Namespace = "http://tpro.ca")]
	public class SetLoginMethodReq : BaseMessageReq
	{
		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x060017D9 RID: 6105 RVA: 0x0000B026 File Offset: 0x00009226
		// (set) Token: 0x060017DA RID: 6106 RVA: 0x0000B02E File Offset: 0x0000922E
		[DataMember]
		public eLoginMethodDTO Method { get; set; }
	}
}
