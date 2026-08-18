using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.WebSettings
{
	// Token: 0x020004C9 RID: 1225
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetWebSettingValueResp
	{
		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x060019E8 RID: 6632 RVA: 0x0000BFA9 File Offset: 0x0000A1A9
		// (set) Token: 0x060019E9 RID: 6633 RVA: 0x0000BFB1 File Offset: 0x0000A1B1
		[DataMember]
		public string SettingValue { get; set; }
	}
}
