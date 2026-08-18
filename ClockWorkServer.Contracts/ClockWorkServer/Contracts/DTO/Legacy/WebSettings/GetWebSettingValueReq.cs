using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.WebSettings
{
	// Token: 0x020004C8 RID: 1224
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetWebSettingValueReq : BaseMessageReq
	{
		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x060019E3 RID: 6627 RVA: 0x0000BF87 File Offset: 0x0000A187
		// (set) Token: 0x060019E4 RID: 6628 RVA: 0x0000BF8F File Offset: 0x0000A18F
		[DataMember]
		public int WebSetting { get; set; }

		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x060019E5 RID: 6629 RVA: 0x0000BF98 File Offset: 0x0000A198
		// (set) Token: 0x060019E6 RID: 6630 RVA: 0x0000BFA0 File Offset: 0x0000A1A0
		[DataMember]
		public string InstanceName { get; set; }
	}
}
